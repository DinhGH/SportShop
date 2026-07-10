using System;
using System.Web.UI;

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

            // Logic lưu khiếu nại (giả lập lưu vào log hoặc DB)
            lblMessage.Text = "Cảm ơn bạn, báo cáo đã được gửi đến Admin.";
            lblMessage.ForeColor = System.Drawing.Color.Green;

            txtTitle.Text = "";
            txtContent.Text = "";
        }
    }
}