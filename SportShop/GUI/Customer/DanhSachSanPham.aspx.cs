using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SportShop.App_Code;

namespace SportShop.GUI.Customer
{
    public partial class DanhSachSanPham : System.Web.UI.Page
    {
        ConnectionProvider db = new ConnectionProvider();
        CartDAL cartDAL = new CartDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNoiBat();
            }
        }

        private void LoadNoiBat()
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = "SELECT TOP 20 ProductID, ProductName, Price, ImageURL FROM Products WHERE IsAvailable = 1 ORDER BY ProductID DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);

                rptProducts.DataSource = dt;
                rptProducts.DataBind();
            }
            catch (Exception) { }
            finally
            {
                db.dongketnoi();
            }
        }

        protected void btnAddCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int productId = Convert.ToInt32(btn.CommandArgument);

            // Tạm thời dùng CustomerID = 7 để test
            //int customerId = 7;

            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            int customerId = Convert.ToInt32(Session["UserID"]);

            int result = cartDAL.ThemVaoGio(customerId, productId, 1);

            if (result > 0)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Đã thêm vào giỏ hàng!');",
                    true);
            }
            else if (result == 0)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Số lượng sản phẩm vượt quá tồn kho!');",
                    true);
            }
            else
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Có lỗi xảy ra!');",
                    true);
            }
        }

    }
}