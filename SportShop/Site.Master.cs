using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportShop
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kiểm tra xem User đã đăng nhập chưa thông qua Session
                if (Session["UserEmail"] != null)
                {
                    // Hiển thị tên đầy đủ của người dùng lên Navbar
                    lblUsername.Text = Session["UserFullName"]?.ToString();
                }
                else
                {
                    // Nếu chưa đăng nhập, đá văng người dùng về trang Login
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Hủy toàn bộ Session khi bấm đăng xuất
            Session.Clear();
            Session.Abandon();

            // Chuyển hướng về trang đăng nhập
            Response.Redirect("~/Login.aspx");
        }
    }
}