using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.App_Code
{
    public class StoreDAL
    {
        ConnectionProvider db = new ConnectionProvider();

        /// <summary>
        /// Lấy thông tin cửa hàng dựa vào ID người chủ (Owner)
        /// </summary>
        public DataTable LayCuaHangTheoChu(int ownerId)
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = string.Format("SELECT * FROM Stores WHERE OwnerID = {0}", ownerId);
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
        /// Lấy thông tin cửa hàng dựa vào StoreID
        /// </summary>
        public DataTable LayCuaHangTheoID(int storeId)
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = string.Format("SELECT * FROM Stores WHERE StoreID = {0}", storeId);
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
        /// Tạo cửa hàng mới
        /// </summary>
        public int TaoCuaHangMoi(string storeName, int ownerId, string address, string phone)
        {
            int result = -1;
            try
            {
                db.moketnoi();
                string query = string.Format("INSERT INTO Stores (StoreName, OwnerID, StoreAddress, StorePhone) VALUES (N'{0}', {1}, N'{2}', '{3}')", storeName, ownerId, address, phone);
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
        /// Cập nhật thông tin cửa hàng
        /// </summary>
        public int CapNhatCuaHang(int storeId, string storeName, string address, string phone, string logoUrl = null)
        {
            int result = -1;
            try
            {
                db.moketnoi();
                string query;

                if (string.IsNullOrEmpty(logoUrl))
                {
                    query = string.Format("UPDATE Stores SET StoreName = N'{0}', StoreAddress = N'{1}', StorePhone = '{2}' WHERE StoreID = {3}", storeName, address, phone, storeId);
                }
                else
                {
                    query = string.Format("UPDATE Stores SET StoreName = N'{0}', StoreAddress = N'{1}', StorePhone = '{2}', Logo = '{3}' WHERE StoreID = {4}", storeName, address, phone, logoUrl, storeId);
                }

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
    }
}