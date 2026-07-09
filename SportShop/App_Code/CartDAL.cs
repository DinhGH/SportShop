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
            string query = $"INSERT INTO Cart (CustomerID, ProductID, Quantity) VALUES ({customerId}, {productId}, {quantity})";
            return db.thucthiketnoi(query);
        }

        public int XoaKhoiGio(int cartId)
        {
            return db.thucthiketnoi($"DELETE FROM Cart WHERE CartID = {cartId}");
        }
    }
}