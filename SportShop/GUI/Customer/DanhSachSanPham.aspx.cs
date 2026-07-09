using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.GUI.Customer
{
    public partial class DanhSachSanPham : System.Web.UI.Page
    {
        ConnectionProvider db = new ConnectionProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNoiBat();
            }
        }

        private void LoadNoiBat()
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = "SELECT TOP 20 ProductID, ProductName, Price, ImageURL FROM Products WHERE IsAvailable = 1 ORDER BY ProductID DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);

                rptProducts.DataSource = dt;
                rptProducts.DataBind();
            }
            catch (Exception) { }
            finally
            {
                db.dongketnoi();
            }
        }
    }
}