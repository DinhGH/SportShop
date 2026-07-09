using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SportShop.App_Code;

namespace SportShop
{
    public partial class Login : System.Web.UI.Page
    {
        UserDAL userBusiness = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Nếu đã đăng nhập sẵn rồi thì tự động chuyển vào trang chủ luôn
                if (Session["UserEmail"] != null)
                {
                    Response.Redirect("~/GUI/Customer/DanhSachSanPham.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Gọi hàm Đăng nhập đã viết bên UserDAL
            DataTable dtUser = userBusiness.DangNhap(email, password);

            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                // Đăng nhập thành công -> Lưu thông tin vào Session
                Session["UserID"] = dtUser.Rows[0]["UserID"].ToString();
                Session["UserEmail"] = dtUser.Rows[0]["Email"].ToString();
                Session["UserFullName"] = dtUser.Rows[0]["FullName"].ToString();
                Session["RoleID"] = dtUser.Rows[0]["RoleID"].ToString();

                // Kiểm tra phân quyền để điều hướng về đúng trang
                string roleId = dtUser.Rows[0]["RoleID"].ToString();
                if (roleId == "1") // Admin
                {
                    Response.Redirect("~/GUI/Admin/QuanLyKhieuNai.aspx");
                }
                else if (roleId == "2") // Owner
                {
                    Response.Redirect("~/GUI/Owner/QuanLySanPham.aspx");
                }
                else // Customer (Mã 3)
                {
                    Response.Redirect("~/GUI/Customer/DanhSachSanPham.aspx");
                }
            }
            else
            {
                // Đăng nhập thất bại
                lblMessage.Text = "Email hoặc mật khẩu không chính xác!";
                lblMessage.Visible = true;
            }
        }
    }
}