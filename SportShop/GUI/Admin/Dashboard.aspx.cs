using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.GUI.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        ConnectionProvider db = new ConnectionProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Bảo mật: Nếu không phải Admin (RoleID = 1) thì đá ra ngoài
            if (Session["RoleID"] == null || Session["RoleID"].ToString() != "1")
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                ThongKeHeThong();
            }
        }

        private void ThongKeHeThong()
        {
            try
            {
                db.moketnoi();

                // 1. Đếm tổng số cửa hàng
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Stores", db.conn);
                lblTotalStores.Text = cmd1.ExecuteScalar().ToString();

                // 2. Đếm tổng số lượng Owner (RoleID = 2)
                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM Users WHERE RoleID = 2", db.conn);
                lblTotalOwners.Text = cmd2.ExecuteScalar().ToString();

                // 3. Đếm tổng số lượng Customer (RoleID = 3)
                SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM Users WHERE RoleID = 3", db.conn);
                lblTotalCustomers.Text = cmd3.ExecuteScalar().ToString();

                // 4. Tính toán doanh thu chiết khấu hệ thống (Lấy tổng tiền sản phẩm nhân 10% phí vận hành)
                // Trong thực tế bạn sẽ JOIN thêm bảng Orders để check trạng thái đã thanh toán.
                SqlCommand cmd4 = new SqlCommand("SELECT ISNULL(SUM(Price * StockQuantity) * 0.1, 0) FROM Products", db.conn);
                decimal revenue = Convert.ToDecimal(cmd4.ExecuteScalar());
                lblRevenue.Text = string.Format("{0:N0} đ", revenue);
            }
            catch (Exception) { }
            finally
            {
                db.dongketnoi();
            }
        }
    }
}