using SportShop.App_Code;
using System;
using System.Data;
using System.Web.UI;

namespace SportShop.GUI.Customer
{
    public partial class Checkout : Page
    {
        CartDAL cartDAL = new CartDAL();
        OrderDAL orderDAL = new OrderDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Chưa đăng nhập thì quay về Login
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadCheckout();
            }
        }

        private void LoadCheckout()
        {
            int customerId = Convert.ToInt32(Session["UserID"]);

            // Hiển thị sản phẩm trong giỏ
            DataTable dt = cartDAL.LayGioHangCuaKhach(customerId);

            gvCheckout.DataSource = dt;
            gvCheckout.DataBind();

            // Hiển thị tổng tiền
            decimal tongTien = cartDAL.TinhTongTien(customerId);
            lblTongTien.Text = tongTien.ToString("N0") + " đ";

            // Tự điền thông tin người dùng nếu đã lưu Session
            if (Session["UserFullName"] != null)
                txtReceiver.Text = Session["UserFullName"].ToString();
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu
            if (txtReceiver.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Vui lòng nhập tên người nhận!');",
                    true);
                return;
            }

            if (txtPhone.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Vui lòng nhập số điện thoại!');",
                    true);
                return;
            }

            if (txtAddress.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Vui lòng nhập địa chỉ giao hàng!');",
                    true);
                return;
            }

            int customerId = Convert.ToInt32(Session["UserID"]);

            string receiver = txtReceiver.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string payment = rblPayment.SelectedValue;

            // Gọi hàm đặt hàng (sẽ viết ở bước sau)
            bool result = orderDAL.DatHang(
                customerId,
                receiver,
                phone,
                address,
                payment);

            if (result)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Đặt hàng thành công!');window.location='LichSuDonHang.aspx';",
                    true);
            }
            else
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Đặt hàng thất bại!');",
                    true);
            }
        }
    }
}