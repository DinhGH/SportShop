using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.App_Code
{
    public class CartDAL
    {
        ConnectionProvider db = new ConnectionProvider();

        // Xem các món đồ đang có trong giỏ hàng của Khách hàng hiện tại
        public DataTable LayGioHangCuaKhach(int customerId)
        {
            string query = $"SELECT c.*, p.ProductName, p.Price, p.ImageURL FROM Cart c " +
                           $"INNER JOIN Products p ON c.ProductID = p.ProductID WHERE c.CustomerID = {customerId}";
            return db.laydulieu(query);
        }

        // Thêm món vào giỏ (Sử dụng lệnh kiểm tra trùng lặp dựa trên ràng buộc UC_Cart hoặc dùng câu lệnh INSERT đơn giản)
        public int ThemVaoGio(int customerId, int productId, int quantity)
        {
            string stockSql =
                $"SELECT StockQuantity FROM Products WHERE ProductID={productId}";

            DataTable dtStock = db.laydulieu(stockSql);

            int stock = Convert.ToInt32(dtStock.Rows[0][0]);

            string check =
                $"SELECT Quantity FROM Cart WHERE CustomerID={customerId} AND ProductID={productId}";

            DataTable dt = db.laydulieu(check);

            if (dt.Rows.Count > 0)
            {
                int current = Convert.ToInt32(dt.Rows[0][0]);

                if (current + quantity > stock)
                    return 0;

                return db.thucthiketnoi(
                    $"UPDATE Cart SET Quantity=Quantity+{quantity} WHERE CustomerID={customerId} AND ProductID={productId}");
            }

            if (quantity > stock)
                return 0;

            return db.thucthiketnoi(
                $"INSERT INTO Cart(CustomerID,ProductID,Quantity) VALUES({customerId},{productId},{quantity})");
        }

        //xoa khoi gio
        public int XoaKhoiGio(int cartId)
        {
            return db.thucthiketnoi($"DELETE FROM Cart WHERE CartID = {cartId}");
        }

        //tinh tong tien
        public decimal TinhTongTien(int customerId)
        {
            string query = @"SELECT ISNULL(SUM(p.Price * c.Quantity),0)
                     FROM Cart c
                     INNER JOIN Products p
                     ON c.ProductID = p.ProductID
                     WHERE c.CustomerID = " + customerId;

            DataTable dt = db.laydulieu(query);

            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToDecimal(dt.Rows[0][0]);

            return 0;
        }

        //lay so luong 
        public int LaySoLuong(int cartId)
        {
            DataTable dt = db.laydulieu(
                $"SELECT Quantity FROM Cart WHERE CartID={cartId}");

            if (dt.Rows.Count == 0)
                return 0;

            return Convert.ToInt32(dt.Rows[0][0]);
        }

        //tang so luong + kt ton kho
        public int TangSoLuong(int cartId)
        {
            string sql =
            @"SELECT c.Quantity,
             p.StockQuantity
            FROM Cart c
            INNER JOIN Products p
            ON c.ProductID=p.ProductID
            WHERE c.CartID=" + cartId;

            DataTable dt = db.laydulieu(sql);

            if (dt.Rows.Count == 0)
                return -1;

            int quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
            int stock = Convert.ToInt32(dt.Rows[0]["StockQuantity"]);

            if (quantity >= stock)
                return 0;

            return db.thucthiketnoi(
                $"UPDATE Cart SET Quantity=Quantity+1 WHERE CartID={cartId}");
        }

        //giam so luong
        public int GiamSoLuong(int cartId)
        {
            int quantity = LaySoLuong(cartId);

            if (quantity <= 1)
            {
                return db.thucthiketnoi(
                    $"DELETE FROM Cart WHERE CartID={cartId}");
            }

            return db.thucthiketnoi(
                $"UPDATE Cart SET Quantity=Quantity-1 WHERE CartID={cartId}");
        }
    }
}