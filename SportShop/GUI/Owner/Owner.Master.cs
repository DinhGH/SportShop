using System;

namespace SportShop.GUI.Owner
{
    public partial class OwnerMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserFullName"] != null)
                {
                    lblOwnerName.Text = Session["UserFullName"].ToString();
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}