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
    public partial class DanhSachSanPham : System.Web.UI.Page
    {
        ConnectionProvider db = new ConnectionProvider();
        CartDAL cartDAL = new CartDAL();

        // Kích thước trang (Mỗi trang hiện 12 sản phẩm)
        private const int PageSize = 12;

        // Lưu trữ vị trí trang hiện tại vào ViewState để không bị mất khi PostBack
        public int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                    return 1;
                return Convert.ToInt32(ViewState["CurrentPage"]);
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdownCategories();
                LoadDropdownStores();
                LoadAllProducts();
            }
        }

        // Tải danh sách Danh mục vào dropdown
        private void LoadDropdownCategories()
        {
            DataTable dt = db.laydulieu("SELECT CategoryID, CategoryName FROM Categories");
            if (dt != null)
            {
                ddlCategory.DataSource = dt;
                ddlCategory.DataValueField = "CategoryID";
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataBind();
            }
            ddlCategory.Items.Insert(0, new ListItem("-- Chọn Danh mục --", "0"));
        }

        // Tải danh sách Cửa hàng vào dropdown
        private void LoadDropdownStores()
        {
            DataTable dt = db.laydulieu("SELECT StoreID, StoreName FROM Stores");
            if (dt != null)
            {
                ddlStore.DataSource = dt;
                ddlStore.DataValueField = "StoreID";
                ddlStore.DataTextField = "StoreName";
                ddlStore.DataBind();
            }
            ddlStore.Items.Insert(0, new ListItem("-- Chọn Cửa hàng --", "0"));
        }

        // Xử lý nạp sản phẩm kết hợp Phân trang và Lọc dữ liệu động
        private void LoadAllProducts()
        {
            // Tạo điều kiện lọc cơ bản
            string whereClause = "WHERE IsAvailable = 1";

            if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                whereClause += string.Format(" AND ProductName LIKE N'%{0}%'", txtSearch.Text.Trim().Replace("'", "''"));
            }

            if (ddlCategory.SelectedValue != "0")
            {
                whereClause += string.Format(" AND CategoryID = {0}", ddlCategory.SelectedValue);
            }

            if (ddlStore.SelectedValue != "0")
            {
                whereClause += string.Format(" AND StoreID = {0}", ddlStore.SelectedValue);
            }

            if (!string.IsNullOrEmpty(txtMinPrice.Text))
            {
                whereClause += string.Format(" AND Price >= {0}", txtMinPrice.Text);
            }

            if (!string.IsNullOrEmpty(txtMaxPrice.Text))
            {
                whereClause += string.Format(" AND Price <= {0}", txtMaxPrice.Text);
            }

            // 1. Tính tổng số sản phẩm khớp bộ lọc để tính tổng số trang
            string countQuery = string.Format("SELECT COUNT(*) FROM Products {0}", whereClause);
            DataTable dtCount = db.laydulieu(countQuery);
            int totalRecords = 0;
            if (dtCount != null && dtCount.Rows.Count > 0)
            {
                totalRecords = Convert.ToInt32(dtCount.Rows[0][0]);
            }

            int totalPages = (int)Math.Ceiling((double)totalRecords / PageSize);
            if (totalPages == 0) totalPages = 1;

            // Đảm bảo trang hiện tại nằm trong khoảng hợp lệ
            if (CurrentPage > totalPages) CurrentPage = totalPages;
            if (CurrentPage < 1) CurrentPage = 1;

            // 2. Viết câu lệnh truy vấn phân trang OFFSET FETCH (Hỗ trợ từ SQL Server 2012 trở lên)
            int offset = (CurrentPage - 1) * PageSize;
            string productQuery = string.Format(
                "SELECT ProductID, ProductName, Price, ImageURL FROM Products {0} " +
                "ORDER BY ProductID DESC OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY",
                whereClause, offset, PageSize
            );

            DataTable dtProducts = db.laydulieu(productQuery);
            rptProducts.DataSource = dtProducts;
            rptProducts.DataBind();

            // Cập nhật trạng thái hiển thị của các nút phân trang
            lblPageInfo.Text = string.Format("Trang {0} / {1}", CurrentPage, totalPages);
            btnPrev.Enabled = (CurrentPage > 1);
            btnNext.Enabled = (CurrentPage < totalPages);
        }

        // Sự kiện click nút "Lọc"
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            CurrentPage = 1; // Reset về trang 1 khi lọc mới
            LoadAllProducts();
        }

        // Sự kiện click nút "Thiết lập lại"
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ddlCategory.SelectedValue = "0";
            ddlStore.SelectedValue = "0";
            txtMinPrice.Text = "";
            txtMaxPrice.Text = "";
            CurrentPage = 1;
            LoadAllProducts();
        }

        // Chuyển về trang trước
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                LoadAllProducts();
            }
        }

        // Chuyển sang trang sau
        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            LoadAllProducts();
        }

        // Xử lý thêm vào giỏ hàng
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
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Đã thêm vào giỏ hàng!');",
                    true);
            }
            else if (result == 0)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Số lượng sản phẩm vượt quá tồn kho!');",
                    true);
            }
            else
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Có lỗi xảy ra!');",
                    true);
            }
        }
    }
}