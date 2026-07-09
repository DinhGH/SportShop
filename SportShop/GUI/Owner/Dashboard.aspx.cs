using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SportShop.App_Code;

namespace SportShop.GUI.Owner
{
    public partial class Dashboard : System.Web.UI.Page
    {
        StatisticsDAL statsDAL = new StatisticsDAL();
        StoreDAL storeDAL = new StoreDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadDashboardData();
            }
        }

        private void LoadDashboardData()
        {
            try
            {
                // Lấy ID của Owner từ Session
                int ownerId = Convert.ToInt32(Session["UserID"]);

                // Lấy thông tin cửa hàng của Owner
                DataTable dtStore = storeDAL.LayCuaHangTheoChu(ownerId);

                if (dtStore == null || dtStore.Rows.Count == 0)
                {
                    // Owner chưa có cửa hàng
                    DisplayNoStoreMessage();
                    return;
                }

                // Lấy StoreID từ cửa hàng đầu tiên (thường một Owner chỉ có một cửa hàng)
                int storeId = Convert.ToInt32(dtStore.Rows[0]["StoreID"]);

                // Load thống kê chính
                LoadMainStatistics(storeId);

                // Load top 5 sản phẩm bán chạy
                LoadTopProducts(storeId);

                // Load doanh thu theo tháng
                LoadMonthlyRevenue(storeId);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Lỗi: {ex.Message}');", true);
            }
        }

        private void LoadMainStatistics(int storeId)
        {
            try
            {
                // Lấy thống kê chính
                DataTable dtStats = statsDAL.LayThongKeChinh(storeId);

                if (dtStats != null && dtStats.Rows.Count > 0)
                {
                    DataRow row = dtStats.Rows[0];

                    // Hiển thị doanh thu
                    decimal totalRevenue = Convert.ToDecimal(row["TotalRevenue"]);
                    lblTotalRevenue.Text = totalRevenue.ToString("N0") + " đ";

                    // Hiển thị số đơn hàng
                    int totalOrders = Convert.ToInt32(row["TotalOrders"]);
                    lblTotalOrders.Text = totalOrders.ToString();

                    // Hiển thị tổng sản phẩm
                    int totalProducts = Convert.ToInt32(row["TotalProducts"]);
                    lblTotalProducts.Text = totalProducts.ToString();

                    // Hiển thị tồn kho
                    int totalStock = Convert.ToInt32(row["TotalStock"]);
                    lblTotalStock.Text = totalStock.ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Lỗi khi tải thống kê: {ex.Message}');", true);
            }
        }

        private void LoadTopProducts(int storeId)
        {
            try
            {
                DataTable dtTopProducts = statsDAL.LayTopSanPhamBanChay(storeId, 5);

                if (dtTopProducts != null && dtTopProducts.Rows.Count > 0)
                {
                    rptTopProducts.DataSource = dtTopProducts;
                    rptTopProducts.DataBind();
                }
                else
                {
                    // Hiển thị thông báo không có dữ liệu
                    rptTopProducts.DataSource = new DataTable();
                    rptTopProducts.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Lỗi khi tải sản phẩm bán chạy: {ex.Message}');", true);
            }
        }

        private void LoadMonthlyRevenue(int storeId)
        {
            try
            {
                DataTable dtMonthlyRevenue = statsDAL.LayDoanhThuTheoThang(storeId);

                if (dtMonthlyRevenue != null && dtMonthlyRevenue.Rows.Count > 0)
                {
                    gvMonthlyRevenue.DataSource = dtMonthlyRevenue;
                    gvMonthlyRevenue.DataBind();

                    // Styling
                    gvMonthlyRevenue.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(102, 126, 234);
                    gvMonthlyRevenue.HeaderStyle.ForeColor = System.Drawing.Color.White;
                    gvMonthlyRevenue.HeaderStyle.Font.Bold = true;
                    gvMonthlyRevenue.RowStyle.BackColor = System.Drawing.Color.White;
                    gvMonthlyRevenue.AlternatingRowStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
                }
                else
                {
                    gvMonthlyRevenue.DataSource = new DataTable();
                    gvMonthlyRevenue.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Lỗi khi tải doanh thu theo tháng: {ex.Message}');", true);
            }
        }

        private void DisplayNoStoreMessage()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "alert('Bạn chưa có cửa hàng nào. Vui lòng liên hệ quản trị viên để tạo cửa hàng.');", true);
        }
    }
}
