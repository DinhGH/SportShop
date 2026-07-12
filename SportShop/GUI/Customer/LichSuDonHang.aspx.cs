using SportShop.App_Code;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportShop.GUI.Customer
{
    public partial class LichSuDonHang : Page
    {
        OrderDAL orderDAL = new OrderDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        // Hiển thị danh sách đơn hàng của khách
        private void LoadOrders()
        {
            int customerId = Convert.ToInt32(Session["UserID"]);

            DataTable dt = orderDAL.LayLichSuMuaHang(customerId);

            gvOrders.DataSource = dt;

            gvOrders.DataBind();
        }

        // Khi bấm nút Chi tiết
        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                int orderId = Convert.ToInt32(gvOrders.Rows[index].Cells[0].Text);

                DataTable dt = orderDAL.LayChiTietDonHang(orderId);

                gvDetails.DataSource = dt;

                gvDetails.DataBind();
            }
        }
    }
}