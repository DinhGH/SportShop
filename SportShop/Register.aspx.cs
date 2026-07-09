using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportShop
{
    public partial class Register : System.Web.UI.Page
    {
        UserDAL userBusiness = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Không xử lý gì khi load trang ban đầu
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // 1. Kiểm tra xác nhận mật khẩu xem có trùng khớp không
            if (password != confirmPassword)
            {
                lblStatus.Text = "Mật khẩu xác nhận không trùng khớp!";
                lblStatus.CssClass = "lbl-msg lbl-error";
                lblStatus.Visible = true;
                return;
            }

            // 2. Gọi hàm chèn người dùng mới (Mặc định khi đăng ký tự do là Khách hàng - RoleID = 3)
            int checkInsert = userBusiness.DangKyKhachHang(fullName, email, password, phone, address, 3);

            if (checkInsert > 0)
            {
                // Đăng ký thành công
                lblStatus.Text = "Đăng ký thành công! Đang chuyển hướng về trang đăng nhập...";
                lblStatus.CssClass = "lbl-msg lbl-success";
                lblStatus.Visible = true;

                // Thêm JavaScript phụ trợ để tự động nhảy về trang đăng nhập sau 2 giây cho mượt
                string script = "setTimeout(function(){ window.location.href='Login.aspx'; }, 2000);";
                ClientScript.RegisterStartupScript(this.GetType(), "RedirectLogin", script, true);
            }
            else
            {
                // Thất bại (Có thể do Email đã có người sử dụng trong hệ thống trước đó)
                lblStatus.Text = "Đăng ký thất bại. Email này đã được đăng ký bởi tài khoản khác!";
                lblStatus.CssClass = "lbl-msg lbl-error";
                lblStatus.Visible = true;
            }
        }
    }
}