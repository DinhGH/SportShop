using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SportShop
{
    public class ConnectionProvider
    {
        public SqlConnection conn;

        // Hàm mở kết nối
        public void moketnoi()
        {
            string chuoiketnoi = HttpContext.Current.Server.MapPath("~/App_Data/SportShopDB.mdf");
            string con = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0};Integrated Security=True", chuoiketnoi);
            conn = new SqlConnection(con);
            conn.Open();
        }

        // Hàm đóng kết nối an toàn
        public void dongketnoi()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        // Hàm chuyên dùng để SELECT dữ liệu (Trả về DataTable)
        public DataTable laydulieu(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                moketnoi();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            finally
            {
                dongketnoi();
            }
            return dt;
        }

        // Hàm chuyên dùng để INSERT, UPDATE, DELETE (Trả về số dòng bị ảnh hưởng)
        public int thucthiketnoi(string query)
        {
            int result = -1;
            try
            {
                moketnoi();
                SqlCommand cmd = new SqlCommand(query, conn);
                result = cmd.ExecuteNonQuery();
            }
            catch
            {
                result = -1;
            }
            finally
            {
                dongketnoi();
            }
            return result;
        }

        //ham dung de thuc hien truy suat cu the toi 1 doi tuong trong database
        public object LayGiaTri(string query)
        {
            object result = null;

            try
            {
                moketnoi();

                SqlCommand cmd = new SqlCommand(query, conn);

                result = cmd.ExecuteScalar();
            }
            catch
            {
                result = null;
            }
            finally
            {
                dongketnoi();
            }

            return result;
        }

        //trả về SqlConnection để OrderDAL tự quản lý Transaction.
        public SqlConnection GetConnection()
        {
            string chuoiketnoi = HttpContext.Current.Server.MapPath("~/App_Data/SportShopDB.mdf");
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={chuoiketnoi};Integrated Security=True";
            return new SqlConnection(con);
        }

    }
}