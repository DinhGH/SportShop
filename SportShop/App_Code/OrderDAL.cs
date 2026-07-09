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
        public DataTable LayLichSuMuaHang(int storeId)
        {
            string sql = string.Format(@"SELECT od.OrderDetailID,od.OrderID,u.FullName AS CustomerName,o.ReceiverName,o.ReceiverPhone,o.ShippingAddress,p.ProductName,
        od.Quantity,od.UnitPrice,od.Status,o.CreatedAt FROM OrderDetails od INNER JOIN Orders o ON od.OrderID = o.OrderID INNER JOIN Users u ON o.CustomerID = u.UserID
        INNER JOIN Products p ON od.ProductID = p.ProductID WHERE od.StoreID = {0} ORDER BY o.CreatedAt DESC", storeId);
            return db.laydulieu(sql);
        }

        // Tạo đơn hàng tổng khi bấm thanh toán
        public int TaoDonHangTong(int customerId, decimal totalPrice, string address, string name, string phone, string method)
        {
            // Hàm này sẽ insert một dòng mới vào bảng Orders
            string query = string.Format("INSERT INTO Orders (CustomerID, TotalPrice, ShippingAddress, ReceiverName, ReceiverPhone, PaymentMethod) VALUES ({0}, {1}, N'{2}', N'{3}', '{4}', N'{5}')", customerId, totalPrice, address, name, phone, method);
            return db.thucthiketnoi(query);
        }

        public DataTable LayDonHangTheoStore(int storeId)
        {
            string sql = string.Format(@"
    SELECT
        od.OrderDetailID,
        o.OrderID,
        o.ReceiverName,
        p.ProductName,
        od.Quantity,
        od.UnitPrice,
        od.Status,
        o.CreatedAt
    FROM OrderDetails od
    INNER JOIN Orders o ON od.OrderID = o.OrderID
    INNER JOIN Products p ON od.ProductID = p.ProductID
    WHERE od.StoreID = {0}
    ORDER BY o.OrderID, od.OrderDetailID", storeId);

            return db.laydulieu(sql);
        }

        public int CapNhatTrangThai(int orderDetailId, string status)
        {
            string sql = string.Format(@"UPDATE OrderDetails SET Status = N'{0}' WHERE OrderDetailID = {1}", status, orderDetailId);

            return db.thucthiketnoi(sql);
        }
    }
}
