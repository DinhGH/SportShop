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
                string query = $"SELECT * FROM Stores WHERE OwnerID = {ownerId}";
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
                string query = $"SELECT * FROM Stores WHERE StoreID = {storeId}";
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
                string query = $"INSERT INTO Stores (StoreName, OwnerID, StoreAddress, StorePhone) " +
                               $"VALUES (N'{storeName}', {ownerId}, N'{address}', '{phone}')";
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
                    query = $"UPDATE Stores SET StoreName = N'{storeName}', StoreAddress = N'{address}', StorePhone = '{phone}' " +
                            $"WHERE StoreID = {storeId}";
                }
                else
                {
                    query = $"UPDATE Stores SET StoreName = N'{storeName}', StoreAddress = N'{address}', StorePhone = '{phone}', Logo = '{logoUrl}' " +
                            $"WHERE StoreID = {storeId}";
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