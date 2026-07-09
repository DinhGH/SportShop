using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.App_Code
{
    public class ProductDAL
    {
        ConnectionProvider db = new ConnectionProvider();

        // Cho Customer coi trên trang chủ
        public DataTable LayTatCaSanPhamCoSan()
        {
            return db.laydulieu("SELECT * FROM Products WHERE IsAvailable = 1");
        }

        // Cho Chủ cửa hàng (Owner) quản lý sản phẩm của riêng shop họ
        public DataTable LaySanPhamTheoCuaHang(int storeId)
        {
            return db.laydulieu($"SELECT * FROM Products WHERE StoreID = {storeId}");
        }

        // Thêm mới sản phẩm (Dành cho Chủ shop)
        public int ThemSanPham(string name, string desc, decimal price, int quantity, string imageUrl, int categoryId, int storeId)
        {
            string query = $"INSERT INTO Products (ProductName, Description, Price, StockQuantity, ImageURL, CategoryID, StoreID) " +
                           $"VALUES (N'{name}', N'{desc}', {price}, {quantity}, '{imageUrl}', {categoryId}, {storeId})";
            return db.thucthiketnoi(query);
        }
    }
}