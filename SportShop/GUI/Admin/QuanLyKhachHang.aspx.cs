using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using SportShop.App_Code;

namespace SportShop.GUI.Admin
{
    public partial class QuanLyKhachHang : System.Web.UI.Page
    {
        ConnectionProvider db = new ConnectionProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleID"] == null || Session["RoleID"].ToString() != "1") Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                LoadCustomers();
            }
        }

        private void LoadCustomers()
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                // Chuẩn hóa theo DB: Đổi IsAvailable thành IsActive
                string query = "SELECT UserID, FullName, Email, Phone, Address, IsActive FROM Users WHERE RoleID = 3";
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
                gvCustomers.DataSource = dt;
                gvCustomers.DataBind();
            }
            catch (Exception) { }
            finally { db.dongketnoi(); }
        }

        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ChangeStatus")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int userId = Convert.ToInt32(gvCustomers.DataKeys[index].Value);

                try
                {
                    db.moketnoi();
                    // Chuẩn hóa theo DB: Đổi IsAvailable thành IsActive
                    string query = "UPDATE Users SET IsActive = CASE WHEN IsActive = 1 THEN 0 ELSE 1 END WHERE UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, db.conn);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.ExecuteNonQuery();
                    LoadCustomers();
                }
                catch (Exception) { }
                finally { db.dongketnoi(); }
            }
        }
    }
}