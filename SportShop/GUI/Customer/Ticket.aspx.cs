using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using SportShop.App_Code;

namespace SportShop.GUI.Customer
{
    public partial class Ticket : System.Web.UI.Page
    {
        TicketDAL ticketDAL = new TicketDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra trạng thái đăng nhập của Customer
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadTicketHistory();
            }
        }

        // Tải lại danh sách lịch sử sự cố
        private void LoadTicketHistory()
        {
            int customerId = Convert.ToInt32(Session["UserID"]);
            DataTable dt = ticketDAL.LayLichSuTickets(customerId);

            gvTickets.DataSource = dt;
            gvTickets.DataBind();
        }

        // Sự kiện click nút Gửi báo cáo
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string content = txtContent.Text.Trim();

            // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                lblMessage.Text = "⚠️ Vui lòng điền đầy đủ tiêu đề và nội dung sự cố!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Visible = true;
                return;
            }

            int customerId = Convert.ToInt32(Session["UserID"]);
            int result = ticketDAL.GuiBaoCao(customerId, title, content);

            if (result > 0)
            {
                lblMessage.Text = "✅ Gửi báo cáo thành công! Ban quản trị sẽ xử lý sớm.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Visible = true;

                // Làm trống form để người dùng gửi tiếp nếu muốn
                txtTitle.Text = "";
                txtContent.Text = "";

                // Nạp lại danh sách GridView để cập nhật dòng mới vừa gửi
                LoadTicketHistory();
            }
            else
            {
                lblMessage.Text = "❌ Có lỗi hệ thống xảy ra. Vui lòng thử lại sau!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Visible = true;
            }
        }

        // Hàm helper chuyển đổi trạng thái dạng chữ thành nhãn màu (CSS Badge) trực quan
        protected string GetStatusBadge(string status)
        {
            if (status == "Chưa xử lý")
            {
                return "<span class='badge badge-pending'>Chưa xử lý</span>";
            }
            else if (status == "Đang xử lý")
            {
                return "<span class='badge badge-processing'>Đang xử lý</span>";
            }
            else if (status == "Đã giải quyết")
            {
                return "<span class='badge badge-success'>Đã giải quyết</span>";
            }
            return "<span class='badge'>" + status + "</span>";
        }
    }
}