using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using SportShop.App_Code;

namespace SportShop.GUI.Admin
{
    public partial class QuanLyTickets : System.Web.UI.Page
    {
        ConnectionProvider db = new ConnectionProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleID"] == null || Session["RoleID"].ToString() != "1") Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                LoadTickets();
            }
        }

        private void LoadTickets()
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                // Chuẩn hóa theo DB: Đổi u.UserID thành t.SenderID và t.Description thành t.Content
                string query = "SELECT t.TicketID, u.FullName AS SenderName, t.Title, t.Content, t.CreatedAt, t.Status " +
                               "FROM Tickets t JOIN Users u ON t.SenderID = u.UserID ORDER BY t.TicketID DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
                gvTickets.DataSource = dt;
                gvTickets.DataBind();
            }
            catch (Exception) { }
            finally { db.dongketnoi(); }
        }

        protected void gvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnStatus = (HiddenField)e.Row.FindControl("hdnStatus");
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");

                if (hdnStatus != null && ddlStatus != null)
                {
                    ddlStatus.SelectedValue = hdnStatus.Value;
                }
            }
        }

        protected void gvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateTicketStatus")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int ticketId = Convert.ToInt32(gvTickets.DataKeys[index].Value);

                GridViewRow row = gvTickets.Rows[index];
                DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
                string newStatus = ddlStatus.SelectedValue;

                try
                {
                    db.moketnoi();
                    string query = "UPDATE Tickets SET Status = @Status WHERE TicketID = @TicketID";
                    SqlCommand cmd = new SqlCommand(query, db.conn);
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@TicketID", ticketId);
                    cmd.ExecuteNonQuery();
                    LoadTickets();
                }
                catch (Exception) { }
                finally { db.dongketnoi(); }
            }
        }
    }
}