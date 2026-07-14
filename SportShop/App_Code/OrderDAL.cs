using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.App_Code
{
    public class OrderDAL
    {
        ConnectionProvider db = new ConnectionProvider();

        // Khách hàng xem lại lịch sử mua hàng của họ
        public DataTable LayLichSuMuaHang(int customerId)
        {
            return db.laydulieu($"SELECT * FROM Orders WHERE CustomerID = {customerId} ORDER BY CreatedAt DESC");
        }

        // Tạo đơn hàng tổng khi bấm thanh toán
        public int TaoDonHangTong(int customerId, decimal totalPrice, string address, string name, string phone, string method)
        {
            // Hàm này sẽ insert một dòng mới vào bảng Orders
            string query = $"INSERT INTO Orders (CustomerID, TotalPrice, ShippingAddress, ReceiverName, ReceiverPhone, PaymentMethod) " +
                           $"VALUES ({customerId}, {totalPrice}, N'{address}', N'{name}', '{phone}', N'{method}')";
            return db.thucthiketnoi(query);
        }

        // =========================================================================
        // BỔ SUNG 1: Lấy danh sách đơn hàng chi tiết của một cửa hàng (cho Owner xem)
        // =========================================================================
        public DataTable LayDonHangTheoStore(int storeId)
        {
            // Query này sẽ kết hợp bảng OrderDetails, Orders và Users để lấy thông tin đơn hàng 
            // và tên khách hàng mua sắm tại riêng Store đó.
            string query = @"
                SELECT 
                    od.OrderDetailID,
                    od.OrderID,
                    p.ProductName,
                    od.Quantity,
                    od.UnitPrice,
                    (od.Quantity * od.UnitPrice) AS SubTotal,
                    od.Status,
                    o.CreatedAt,
                    o.ReceiverName,
                    o.ReceiverPhone,
                    o.ShippingAddress
                FROM OrderDetails od
                INNER JOIN Products p ON od.ProductID = p.ProductID
                INNER JOIN Orders o ON od.OrderID = o.OrderID
                WHERE od.StoreID = " + storeId + @"
                ORDER BY o.CreatedAt DESC";

            return db.laydulieu(query);
        }

        // =========================================================================
        // BỔ SUNG 2: Cập nhật trạng thái của đơn hàng chi tiết (Chờ duyệt, Đang giao, Đã giao, Hủy)
        // =========================================================================
        public int CapNhatTrangThai(int orderDetailId, string newStatus)
        {
            string query = string.Format(
                "UPDATE OrderDetails SET Status = N'{0}' WHERE OrderDetailID = {1}",
                newStatus,
                orderDetailId
            );
            return db.thucthiketnoi(query);
        }

        // Hàm xử lý đặt hàng cho Khách hàng
        public bool DatHang(int customerId, string receiverName, string receiverPhone, string shippingAddress, string paymentMethod)
        {
            // Tạo kết nối mới để sử dụng Transaction
            SqlConnection conn = db.GetConnection();

            // Mở kết nối
            conn.Open();

            // Bắt đầu Transaction
            SqlTransaction tran = conn.BeginTransaction();

            try
            {
                //=====================================================
                // BƯỚC 1: Lấy toàn bộ sản phẩm trong giỏ hàng
                //=====================================================

                string queryCart = @"
        SELECT
            c.ProductID,
            c.Quantity,
            p.Price,
            p.StoreID,
            p.StockQuantity
        FROM Cart c
        INNER JOIN Products p
            ON c.ProductID = p.ProductID
        WHERE c.CustomerID = @CustomerID";

                SqlCommand cmdCart = new SqlCommand(queryCart, conn, tran);

                cmdCart.Parameters.AddWithValue("@CustomerID", customerId);

                SqlDataAdapter da = new SqlDataAdapter(cmdCart);

                DataTable dtCart = new DataTable();

                da.Fill(dtCart);

                // Nếu giỏ hàng rỗng thì không cho thanh toán
                if (dtCart.Rows.Count == 0)
                {
                    tran.Rollback();
                    conn.Close();
                    return false;
                }

                //=====================================================
                // BƯỚC 2: Kiểm tra tồn kho
                //=====================================================

                foreach (DataRow row in dtCart.Rows)
                {
                    int quantity = Convert.ToInt32(row["Quantity"]);

                    int stock = Convert.ToInt32(row["StockQuantity"]);

                    if (quantity > stock)
                    {
                        tran.Rollback();
                        conn.Close();
                        return false;
                    }
                }

                //=====================================================
                // BƯỚC 3: Tính tổng tiền
                //=====================================================

                decimal totalPrice = 0;

                foreach (DataRow row in dtCart.Rows)
                {
                    decimal price = Convert.ToDecimal(row["Price"]);

                    int quantity = Convert.ToInt32(row["Quantity"]);

                    totalPrice += price * quantity;
                }

                //=====================================================
                // BƯỚC 4: Thêm vào bảng Orders
                //=====================================================

                string queryOrder = @"
        INSERT INTO Orders
        (
            CustomerID,
            TotalPrice,
            ShippingAddress,
            ReceiverName,
            ReceiverPhone,
            PaymentMethod
        )
        VALUES
        (
            @CustomerID,
            @TotalPrice,
            @ShippingAddress,
            @ReceiverName,
            @ReceiverPhone,
            @PaymentMethod
        );

        SELECT SCOPE_IDENTITY();";

                SqlCommand cmdOrder = new SqlCommand(queryOrder, conn, tran);

                cmdOrder.Parameters.AddWithValue("@CustomerID", customerId);
                cmdOrder.Parameters.AddWithValue("@TotalPrice", totalPrice);
                cmdOrder.Parameters.AddWithValue("@ShippingAddress", shippingAddress);
                cmdOrder.Parameters.AddWithValue("@ReceiverName", receiverName);
                cmdOrder.Parameters.AddWithValue("@ReceiverPhone", receiverPhone);
                cmdOrder.Parameters.AddWithValue("@PaymentMethod", paymentMethod);

                // Lấy OrderID vừa được tạo
                int orderId = Convert.ToInt32(cmdOrder.ExecuteScalar());
                //=====================================================
                // BƯỚC 5: Thêm từng sản phẩm vào bảng OrderDetails
                //=====================================================

                foreach (DataRow row in dtCart.Rows)
                {
                    int productId = Convert.ToInt32(row["ProductID"]);
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    decimal price = Convert.ToDecimal(row["Price"]);
                    int storeId = Convert.ToInt32(row["StoreID"]);

                    string queryDetail = @"
            INSERT INTO OrderDetails
            (
                OrderID,
                ProductID,
                StoreID,
                Quantity,
                UnitPrice
            )
            VALUES
            (
                @OrderID,
                @ProductID,
                @StoreID,
                @Quantity,
                @UnitPrice
            )";

                    SqlCommand cmdDetail = new SqlCommand(queryDetail, conn, tran);

                    cmdDetail.Parameters.AddWithValue("@OrderID", orderId);
                    cmdDetail.Parameters.AddWithValue("@ProductID", productId);
                    cmdDetail.Parameters.AddWithValue("@StoreID", storeId);
                    cmdDetail.Parameters.AddWithValue("@Quantity", quantity);
                    cmdDetail.Parameters.AddWithValue("@UnitPrice", price);

                    cmdDetail.ExecuteNonQuery();
                }

                //=====================================================
                // BƯỚC 6: Trừ số lượng tồn kho của từng sản phẩm
                //=====================================================

                foreach (DataRow row in dtCart.Rows)
                {
                    int productId = Convert.ToInt32(row["ProductID"]);
                    int quantity = Convert.ToInt32(row["Quantity"]);

                    string queryUpdateStock = @"
            UPDATE Products
            SET StockQuantity = StockQuantity - @Quantity
            WHERE ProductID = @ProductID";

                    SqlCommand cmdStock = new SqlCommand(queryUpdateStock, conn, tran);

                    cmdStock.Parameters.AddWithValue("@Quantity", quantity);
                    cmdStock.Parameters.AddWithValue("@ProductID", productId);

                    cmdStock.ExecuteNonQuery();
                }

                //=====================================================
                // BƯỚC 7: Xóa toàn bộ giỏ hàng sau khi đặt hàng thành công
                //=====================================================

                string queryDeleteCart =
                    "DELETE FROM Cart WHERE CustomerID = @CustomerID";

                SqlCommand cmdDelete = new SqlCommand(queryDeleteCart, conn, tran);

                cmdDelete.Parameters.AddWithValue("@CustomerID", customerId);

                cmdDelete.ExecuteNonQuery();

                //=====================================================
                // BƯỚC 8: Hoàn tất Transaction
                //=====================================================

                tran.Commit();

                conn.Close();

                return true;
            }
            catch
            {
                // Nếu có lỗi thì phục hồi dữ liệu

                tran.Rollback();

                conn.Close();

                return false;
            }
        }

        // Xem chi tiết một đơn hàng
        public DataTable LayChiTietDonHang(int orderId)
        {
            string query = @"
    SELECT
        od.OrderDetailID,
        p.ProductName,
        od.Quantity,
        od.UnitPrice,
        od.Status
    FROM OrderDetails od
    INNER JOIN Products p
        ON od.ProductID = p.ProductID
    WHERE od.OrderID = " + orderId;

            return db.laydulieu(query);
        }


    }
}