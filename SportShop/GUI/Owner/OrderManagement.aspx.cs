using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SportShop.App_Code;

namespace SportShop.GUI.Owner
{
    public partial class OrderManagement : System.Web.UI.Page
    {
        OrderDAL orderDAL = new OrderDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        private void LoadOrders()
        {
            int storeId = 0;
            if (Session["UserID"] != null)
            {
                SportShop.App_Code.StoreDAL storeDAL = new SportShop.App_Code.StoreDAL();
                DataTable dtStore = storeDAL.LayCuaHangTheoChu(Convert.ToInt32(Session["UserID"]));
                if (dtStore != null && dtStore.Rows.Count > 0)
                {
                    storeId = Convert.ToInt32(dtStore.Rows[0]["StoreID"]);
                }
            }

            if (storeId > 0)
            {
                DataTable dt = orderDAL.LayDonHangTheoStore(storeId);
                gvOrders.DataSource = dt;
                gvOrders.DataBind();
            }
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStatus")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvOrders.Rows[index];
                int orderDetailId = Convert.ToInt32(gvOrders.DataKeys[index].Value);
                DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
                string newStatus = ddlStatus.SelectedValue;

                int result = orderDAL.CapNhatTrangThai(orderDetailId, newStatus);
                if (result > 0)
                {
                    lblMessage.Text = "Cập nhật trạng thái thành công!";
                    LoadOrders();
                }
                else
                {
                    lblMessage.Text = "Cập nhật thất bại!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                if (ddlStatus != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string status = drv["Status"].ToString();
                    if (!string.IsNullOrEmpty(status))
                    {
                        ddlStatus.SelectedValue = status;
                    }
                }
            }
        }
    }
}