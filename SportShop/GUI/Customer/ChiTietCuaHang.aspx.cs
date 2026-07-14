using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SportShop.App_Code;

namespace SportShop.GUI.Customer
{
    public partial class ChiTietCuaHang : System.Web.UI.Page
    {
        ConnectionProvider db = new ConnectionProvider();
        CartDAL cartDAL = new CartDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["storeId"] != null)
                {
                    int storeId;
                    if (int.TryParse(Request.QueryString["storeId"], out storeId))
                    {
                        LoadStoreInformation(storeId);
                        LoadStoreProducts(storeId);
                    }
                    else
                    {
                        ShowError("Mã cửa hàng không đúng định dạng.");
                    }
                }
                else
                {
                    ShowError("Không tìm thấy cửa hàng yêu cầu.");
                }
            }
        }

        // 1. Tải thông tin giới thiệu của Cửa hàng
        private void LoadStoreInformation(int storeId)
        {
            string query = string.Format("SELECT * FROM Stores WHERE StoreID = {0}", storeId);
            DataTable dt = db.laydulieu(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                litStoreName.Text = r["StoreName"].ToString();
                litStoreAddress.Text = r["StoreAddress"].ToString();
                litStorePhone.Text = r["StorePhone"].ToString();
                litStoreDate.Text = Convert.ToDateTime(r["CreatedAt"]).ToString("dd/MM/yyyy");
            }
            else
            {
                ShowError("Cửa hàng không tồn tại trên hệ thống.");
            }
        }

        // 2. Tải toàn bộ sản phẩm của riêng Cửa hàng đó
        private void LoadStoreProducts(int storeId)
        {
            string query = string.Format(
                "SELECT ProductID, ProductName, Price, ImageURL FROM Products WHERE StoreID = {0} AND IsAvailable = 1 ORDER BY ProductID DESC",
                storeId
            );
            DataTable dt = db.laydulieu(query);

            if (dt != null)
            {
                rptStoreProducts.DataSource = dt;
                rptStoreProducts.DataBind();
            }
        }

        private void ShowError(string msg)
        {
            phStoreContainer.Visible = false;
            lblError.Text = msg;
            lblError.Visible = true;
        }

        // 3. Cho phép khách hàng thêm nhanh sản phẩm của shop vào giỏ
        protected void btnAddCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int productId = Convert.ToInt32(btn.CommandArgument);

            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            int customerId = Convert.ToInt32(Session["UserID"]);
            int result = cartDAL.ThemVaoGio(customerId, productId, 1);

            if (result > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Đã thêm vào giỏ hàng!');", true);
            }
            else if (result == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Số lượng sản phẩm vượt quá tồn kho!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Có lỗi xảy ra!');", true);
            }
        }
    }
}