using System;
using System.Data;
using System.Web.UI;
using SportShop.App_Code;

namespace SportShop.GUI.Customer
{
    public partial class ChiTietSanPham : System.Web.UI.Page
    {
        ConnectionProvider db = new ConnectionProvider();
        CartDAL cartDAL = new CartDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int productId;
                    if (int.TryParse(Request.QueryString["id"], out productId))
                    {
                        LoadProductDetail(productId);
                    }
                    else
                    {
                        ShowError("Mã sản phẩm không đúng định dạng.");
                    }
                }
                else
                {
                    ShowError("Không tìm thấy sản phẩm yêu cầu.");
                }
            }
        }

        private void LoadProductDetail(int productId)
        {
            // Query kết nối bảng Products với Categories và Stores để lấy tên Danh mục và tên Store tương ứng
            string query = string.Format(@"
                SELECT p.*, c.CategoryName, s.StoreName 
                FROM Products p
                INNER JOIN Categories c ON p.CategoryID = c.CategoryID
                INNER JOIN Stores s ON p.StoreID = s.StoreID
                WHERE p.ProductID = {0} AND p.IsAvailable = 1", productId);

            DataTable dt = db.laydulieu(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];

                litProductName.Text = r["ProductName"].ToString();
                litPrice.Text = string.Format("{0:N0} đ", r["Price"]);
                litCategory.Text = r["CategoryName"].ToString();
                litDescription.Text = r["Description"].ToString();

                int stock = Convert.ToInt32(r["StockQuantity"]);
                litStock.Text = stock > 0 ? string.Format("Còn hàng ({0} sản phẩm)", stock) : "<span style='color:red;'>Hết hàng</span>";

                imgProduct.ImageUrl = r["ImageURL"].ToString();
                imgProduct.AlternateText = r["ProductName"].ToString();

                // Thiết lập link dẫn tới trang Xem Chi Tiết Cửa Hàng
                lnkStore.Text = r["StoreName"].ToString();
                lnkStore.NavigateUrl = "ChiTietCuaHang.aspx?storeId=" + r["StoreID"].ToString();

                // Lưu lại ProductID và StockQuantity vào ViewState để dùng khi bấm nút Add to Cart
                ViewState["ProductID"] = productId;
                ViewState["StockQuantity"] = stock;
            }
            else
            {
                ShowError("Sản phẩm không tồn tại hoặc đã bị ẩn khỏi hệ thống.");
            }
        }

        private void ShowError(string msg)
        {
            phProductDetail.Visible = false;
            lblError.Text = msg;
            lblError.Visible = true;
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            int customerId = Convert.ToInt32(Session["UserID"]);
            int productId = Convert.ToInt32(ViewState["ProductID"]);
            int stock = Convert.ToInt32(ViewState["StockQuantity"]);

            int qty;
            if (!int.TryParse(txtQuantity.Text, out qty) || qty <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Số lượng mua không hợp lệ!');", true);
                return;
            }

            if (qty > stock)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Số lượng mua vượt quá số lượng tồn kho hiện tại!');", true);
                return;
            }

            int result = cartDAL.ThemVaoGio(customerId, productId, qty);

            if (result > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Đã thêm sản phẩm vào giỏ hàng thành công!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Có lỗi xảy ra hoặc vượt quá giới hạn tồn kho!');", true);
            }
        }
    }
}