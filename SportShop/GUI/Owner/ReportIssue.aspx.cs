using System;
using System.Web.UI;
using SportShop.App_Code;

namespace SportShop.GUI.Owner
{
    public partial class ReportIssue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string content = txtContent.Text.Trim();

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                lblMessage.Text = "Vui lòng nhập đầy đủ thông tin.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                // Lấy UserID từ Session
                int senderId = Convert.ToInt32(Session["UserID"]);

                TicketDAL ticketDAL = new TicketDAL();
                int result = ticketDAL.GuiBaoCao(senderId, title, content); // Corrected method name

                if (result > 0)
                {
                    lblMessage.Text = "Cảm ơn bạn, báo cáo đã được gửi đến Admin.";
                    lblMessage.ForeColor = System.Drawing.Color.Green; // Corrected to Green for success

                    txtTitle.Text = "";
                    txtContent.Text = "";
                }
                else
                {
                    lblMessage.Text = "Gửi báo cáo thất bại. Vui lòng thử lại sau.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Đã xảy ra lỗi không mong muốn. Vui lòng thử lại sau.";
                lblMessage.ForeColor = System.Drawing.Color.Red;

                // Ghi log nếu cần
                // System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
        }
    }
}