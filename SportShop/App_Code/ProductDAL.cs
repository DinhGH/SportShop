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

        /// <summary>
        /// Lấy tất cả sản phẩm có sẵn cho customer (IsAvailable = 1)
        /// </summary>
        public DataTable LayTatCaSanPhamCoSan()
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = "SELECT * FROM Products WHERE IsAvailable = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            finally
            {
                db.dongketnoi();
            }
            return dt;
        }

        /// <summary>
        /// Lấy sản phẩm của cửa hàng (Owner quản lý)
        /// </summary>
        public DataTable LaySanPhamTheoCuaHang(int storeId)
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = string.Format("SELECT * FROM Products WHERE StoreID = {0} ORDER BY ProductID DESC", storeId);
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            finally
            {
                db.dongketnoi();
            }
            return dt;
        }

        /// <summary>
        /// Lấy thông tin sản phẩm theo ID
        /// </summary>
        public DataTable LaySanPhamTheoID(int productId)
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = string.Format("SELECT * FROM Products WHERE ProductID = {0}", productId);
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            finally
            {
                db.dongketnoi();
            }
            return dt;
        }

        /// <summary>
        /// Thêm mới sản phẩm (Dành cho Owner)
        /// </summary>
        public int ThemSanPham(string name, string desc, decimal price, int quantity, string imageUrl, int categoryId, int storeId)
        {
            int result = -1;
            try
            {
                db.moketnoi();
                string query = string.Format("INSERT INTO Products (ProductName, Description, Price, StockQuantity, ImageURL, CategoryID, StoreID, IsAvailable) VALUES (N'{0}', N'{1}', {2}, {3}, '{4}', {5}, {6}, 1)", name, desc, price, quantity, imageUrl, categoryId, storeId);
                SqlCommand cmd = new SqlCommand(query, db.conn);
                result = cmd.ExecuteNonQuery();
            }
            catch
            {
                result = -1;
            }
            finally
            {
                db.dongketnoi();
            }
            return result;
        }

        /// <summary>
        /// Cập nhật thông tin sản phẩm
        /// </summary>
        public int SuaSanPham(int productId, string name, string desc, decimal price, int quantity, int categoryId, string imageUrl)
        {
            int result = -1;
            try
            {
                db.moketnoi();
                string query = string.Format("UPDATE Products SET ProductName = N'{0}', Description = N'{1}', Price = {2}, StockQuantity = {3}, CategoryID = {4}, ImageURL = '{5}' WHERE ProductID = {6}", name, desc, price, quantity, categoryId, imageUrl, productId);
                SqlCommand cmd = new SqlCommand(query, db.conn);
                result = cmd.ExecuteNonQuery();
            }
            catch
            {
                result = -1;
            }
            finally
            {
                db.dongketnoi();
            }
            return result;
        }

        /// <summary>
        /// Xóa sản phẩm
        /// </summary>
        public int XoaSanPham(int productId)
        {
            int result = -1;
            try
            {
                db.moketnoi();
                string query = string.Format("DELETE FROM Products WHERE ProductID = {0}", productId);
                SqlCommand cmd = new SqlCommand(query, db.conn);
                result = cmd.ExecuteNonQuery();
            }
            catch
            {
                result = -1;
            }
            finally
            {
                db.dongketnoi();
            }
            return result;
        }

        /// <summary>
        /// Cập nhật số lượng tồn kho
        /// </summary>
        public int CapNhatTonKho(int productId, int quantity)
        {
            string query = string.Format("UPDATE Products SET StockQuantity = {0} WHERE ProductID = {1}", quantity, productId);
            int result = db.thucthiketnoi(query);
            return result;
        }

        /// <summary>
        /// Bật/Tắt trạng thái sản phẩm
        /// </summary>
        public int CapNhatTrangThai(int productId, bool isAvailable)
        {
            int result = -1;
            try
            {
                db.moketnoi();
                string query = string.Format("UPDATE Products SET IsAvailable = {0} WHERE ProductID = {1}", (isAvailable ? 1 : 0), productId);
                SqlCommand cmd = new SqlCommand(query, db.conn);
                result = cmd.ExecuteNonQuery();
            }
            catch
            {
                result = -1;
            }
            finally
            {
                db.dongketnoi();
            }
            return result;
        }

        /// <summary>
        /// Lấy danh sách danh mục
        /// </summary>
        public DataTable LayDanhMuc()
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = "SELECT CategoryID, CategoryName FROM Categories ORDER BY CategoryName";
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            finally
            {
                db.dongketnoi();
            }
            return dt;
        }
    }
}