using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using SportShop.App_Code;

namespace SportShop.GUI.Admin
{
    public partial class QuanLyCuaHang : System.Web.UI.Page
    {
        ConnectionProvider db = new ConnectionProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleID"] == null || Session["RoleID"].ToString() != "1") Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                LoadStores();
            }
        }

        private void LoadStores()
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                // Chuẩn hóa theo DB: Lấy u.IsActive làm trạng thái hoạt động của Shop
                string query = "SELECT s.StoreID, s.StoreName, u.FullName AS OwnerName, s.StorePhone, s.StoreAddress, u.IsActive " +
                               "FROM Stores s JOIN Users u ON s.OwnerID = u.UserID";
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
                gvStores.DataSource = dt;
                gvStores.DataBind();
            }
            catch (Exception) { }
            finally { db.dongketnoi(); }
        }

        protected void gvStores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ToggleStatus")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int storeId = Convert.ToInt32(gvStores.DataKeys[index].Value);

                try
                {
                    db.moketnoi();
                    // Chuẩn hóa theo DB: Cập nhật quyền hoạt động của Shop thông qua u.IsActive của Owner sở hữu Shop đó
                    string query = "UPDATE Users SET IsActive = CASE WHEN IsActive = 1 THEN 0 ELSE 1 END " +
                                   "WHERE UserID = (SELECT OwnerID FROM Stores WHERE StoreID = @StoreID)";
                    SqlCommand cmd = new SqlCommand(query, db.conn);
                    cmd.Parameters.AddWithValue("@StoreID", storeId);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        lblMsg.Text = "Cập nhật quyền hoạt động của cửa hàng thành công!";
                        lblMsg.Visible = true;
                        LoadStores();
                    }
                }
                catch (Exception ex) { lblMsg.Text = "Lỗi: " + ex.Message; lblMsg.Visible = true; }
                finally { db.dongketnoi(); }
            }
        }
    }
}