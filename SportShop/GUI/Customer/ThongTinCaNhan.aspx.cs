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
    public partial class ThongTinCaNhan : System.Web.UI.Page
    {
        ConnectionProvider db = new ConnectionProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                DoDuLieuForm();
            }
        }

        private void DoDuLieuForm()
        {
            try
            {
                db.moketnoi();
                string query = "SELECT FullName, Email, Phone, Address FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, db.conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtFullName.Text = dr["FullName"].ToString();
                    txtEmail.Text = dr["Email"].ToString();
                    txtPhone.Text = dr["Phone"].ToString();
                    txtAddress.Text = dr["Address"].ToString();
                }
            }
            catch (Exception) { }
            finally { db.dongketnoi(); }
        }

        protected void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            try
            {
                db.moketnoi();
                string query = "UPDATE Users SET FullName = @FullName, Phone = @Phone, Address = @Address WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, db.conn);
                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Session["UserFullName"] = txtFullName.Text.Trim(); // Cập nhật hiển thị trên Navbar
                    lblInfoMsg.Text = "Cập nhật thông tin cá nhân thành công!";
                    lblInfoMsg.CssClass = "lbl-msg text-success";
                }
            }
            catch (Exception ex) { lblInfoMsg.Text = "Lỗi: " + ex.Message; lblInfoMsg.CssClass = "lbl-msg text-danger"; }
            finally { db.dongketnoi(); lblInfoMsg.Visible = true; }
        }

        protected void btnUpdatePass_Click(object sender, EventArgs e)
        {
            if (txtNewPass.Text.Trim() != txtConfirmPass.Text.Trim())
            {
                lblPassMsg.Text = "Mật khẩu mới không trùng khớp nhau!";
                lblPassMsg.CssClass = "lbl-msg text-danger";
                lblPassMsg.Visible = true;
                return;
            }

            try
            {
                db.moketnoi();
                string query = "UPDATE Users SET Password = @NewPass WHERE UserID = @UserID AND Password = @OldPass";
                SqlCommand cmd = new SqlCommand(query, db.conn);
                cmd.Parameters.AddWithValue("@NewPass", txtNewPass.Text.Trim());
                cmd.Parameters.AddWithValue("@OldPass", txtOldPass.Text.Trim());
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    lblPassMsg.Text = "Đổi mật khẩu thành công!";
                    lblPassMsg.CssClass = "lbl-msg text-success";
                }
                else
                {
                    lblPassMsg.Text = "Mật khẩu hiện tại không chính xác!";
                    lblPassMsg.CssClass = "lbl-msg text-danger";
                }
            }
            catch (Exception ex) { lblPassMsg.Text = "Lỗi: " + ex.Message; lblPassMsg.CssClass = "lbl-msg text-danger"; }
            finally { db.dongketnoi(); lblPassMsg.Visible = true; }
        }
    }
}