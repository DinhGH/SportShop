using System;
using System.Data;
using System.Data.SqlClient;
using SportShop.App_Code;

namespace SportShop.GUI.Owner
{
    public partial class DashboardOwner : System.Web.UI.Page
    {
        ConnectionProvider db = new ConnectionProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Bảo mật hệ thống: Nếu phiên làm việc hết hạn hoặc không phải Owner (RoleID = 2) thì đá ra Login
            if (Session["UserID"] == null || Session["RoleID"] == null || Session["RoleID"].ToString() != "2")
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                int ownerId = Convert.ToInt32(Session["UserID"]);
                int storeId = GetStoreIdByOwner(ownerId);

                if (storeId > 0)
                {
                    LoadStoreInfo(storeId);
                    LoadTopCounters(storeId);
                    LoadBestSellers(storeId);
                }
                else
                {
                    lblStoreName.Text = "Tài khoản của bạn chưa được liên kết hoặc cấp phép hoạt động cho bất kỳ Store nào!";
                }
            }
        }

        // Hàm phụ trợ: Tìm mã cửa hàng (StoreID) dựa trên mã chủ sở hữu (OwnerID) đang đăng nhập
        private int GetStoreIdByOwner(int ownerId)
        {
            int storeId = 0;
            try
            {
                db.moketnoi();
                string query = "SELECT StoreID FROM Stores WHERE OwnerID = @OwnerID";
                SqlCommand cmd = new SqlCommand(query, db.conn);
                cmd.Parameters.AddWithValue("@OwnerID", ownerId);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    storeId = Convert.ToInt32(result);
                }
            }
            catch (Exception) { }
            finally { db.dongketnoi(); }
            return storeId;
        }

        // 1. Lấy và hiển thị tên Cửa hàng lên Banner đầu trang
        private void LoadStoreInfo(int storeId)
        {
            try
            {
                db.moketnoi();
                string query = "SELECT StoreName FROM Stores WHERE StoreID = @StoreID";
                SqlCommand cmd = new SqlCommand(query, db.conn);
                cmd.Parameters.AddWithValue("@StoreID", storeId);
                lblStoreName.Text = cmd.ExecuteScalar()?.ToString();
            }
            catch (Exception) { }
            finally { db.dongketnoi(); }
        }

        // 2. Chạy đếm số liệu thô và tính tổng doanh thu riêng cho Shop này
        private void LoadTopCounters(int storeId)
        {
            try
            {
                db.moketnoi();

                // Đếm tổng doanh thu thực tế thu được từ các chi tiết đơn hàng của các sản phẩm thuộc Shop này
                string queryRevenue = "SELECT ISNULL(SUM(od.Quantity * od.Price), 0) " +
                                      "FROM OrderDetails od " +
                                      "JOIN Products p ON od.ProductID = p.ProductID " +
                                      "WHERE p.StoreID = @StoreID";
                SqlCommand cmdRev = new SqlCommand(queryRevenue, db.conn);
                cmdRev.Parameters.AddWithValue("@StoreID", storeId);
                decimal totalRev = Convert.ToDecimal(cmdRev.ExecuteScalar());
                lblRevenue.Text = string.Format("{0:N0} đ", totalRev);

                // Đếm tổng số đơn hàng phân biệt (Distinct Orders) chứa ít nhất 1 sản phẩm của Shop này
                string queryOrders = "SELECT COUNT(DISTINCT od.OrderID) " +
                                     "FROM OrderDetails od " +
                                     "JOIN Products p ON od.ProductID = p.ProductID " +
                                     "WHERE p.StoreID = @StoreID";
                SqlCommand cmdOrd = new SqlCommand(queryOrders, db.conn);
                cmdOrd.Parameters.AddWithValue("@StoreID", storeId);
                lblTotalOrders.Text = cmdOrd.ExecuteScalar().ToString();

                // Đếm tổng số lượng mẫu mã sản phẩm của Shop đang niêm yết
                string queryProducts = "SELECT COUNT(*) FROM Products WHERE StoreID = @StoreID";
                SqlCommand cmdProd = new SqlCommand(queryProducts, db.conn);
                cmdProd.Parameters.AddWithValue("@StoreID", storeId);
                lblTotalProducts.Text = cmdProd.ExecuteScalar().ToString();
            }
            catch (Exception) { }
            finally { db.dongketnoi(); }
        }

        // 3. Đổ bảng xếp hạng các sản phẩm có số lượng bán nhiều nhất của Shop
        private void LoadBestSellers(int storeId)
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                // Thực thi câu lệnh GROUP BY gom nhóm số lượng bán và tổng tiền từng mặt hàng đem lại
                string query = "SELECT p.ProductID, p.ProductName, p.Price, " +
                               "SUM(od.Quantity) AS TotalQuantitySold, " +
                               "SUM(od.Quantity * od.Price) AS TotalProductRevenue " +
                               "FROM OrderDetails od " +
                               "JOIN Products p ON od.ProductID = p.ProductID " +
                               "WHERE p.StoreID = @StoreID " +
                               "GROUP BY p.ProductID, p.ProductName, p.Price " +
                               "ORDER BY TotalQuantitySold DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.SelectCommand.Parameters.AddWithValue("@StoreID", storeId);
                da.Fill(dt);

                gvTopProducts.DataSource = dt;
                gvTopProducts.DataBind();
            }
            catch (Exception) { }
            finally { db.dongketnoi(); }
        }
    }
}