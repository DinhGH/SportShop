using System;

namespace SportShop.GUI.Customer
{
    public partial class CustomerMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bảo mật: Tránh trường hợp vào thẳng link mà chưa đăng nhập
                if (Session["UserEmail"] != null)
                {
                    lblUsername.Text = Session["UserFullName"]?.ToString();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Xóa sạch bộ nhớ tạm và chuyển về trang đăng nhập ngoài Root
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}