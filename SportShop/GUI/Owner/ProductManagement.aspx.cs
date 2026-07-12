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
    public partial class ProductManagement : System.Web.UI.Page
    {
        ProductDAL productDAL = new ProductDAL();
        StoreDAL storeDAL = new StoreDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                LoadProducts();
            }
        }

        private void LoadCategories()
        {
            try
            {
                DataTable dtCategories = productDAL.LayDanhMuc();
                if (dtCategories != null && dtCategories.Rows.Count > 0)
                {
                    ddlCategory.DataSource = dtCategories;
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryID";
                    ddlCategory.DataBind();
                }

                // Add default item
                ddlCategory.Items.Insert(0, new ListItem("-- Chọn danh mục --", ""));
            }
            catch (Exception ex)
            {
                DisplayMessage($"Lỗi khi tải danh mục: {ex.Message}", MessageType.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                int ownerId = Convert.ToInt32(Session["UserID"]);
                DataTable dtStore = storeDAL.LayCuaHangTheoChu(ownerId);

                if (dtStore == null || dtStore.Rows.Count == 0)
                {
                    DisplayMessage("Bạn chưa có cửa hàng. Vui lòng liên hệ quản trị viên.", MessageType.Error);
                    gvProducts.DataSource = new DataTable();
                    gvProducts.DataBind();
                    return;
                }

                int storeId = Convert.ToInt32(dtStore.Rows[0]["StoreID"]);

                // Lấy sản phẩm của cửa hàng
                DataTable dtProducts = productDAL.LaySanPhamTheoCuaHang(storeId);

                // Join với Categories để lấy tên danh mục
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    // Thêm cột CategoryName nếu chưa có
                    if (!dtProducts.Columns.Contains("CategoryName"))
                    {
                        dtProducts.Columns.Add("CategoryName");
                    }

                    // Tạo bảng Categories để join
                    DataTable dtCategories = productDAL.LayDanhMuc();
                    if (dtCategories != null)
                    {
                        foreach (DataRow productRow in dtProducts.Rows)
                        {
                            int categoryId = Convert.ToInt32(productRow["CategoryID"]);
                            DataRow[] categoryRows = dtCategories.Select($"CategoryID = {categoryId}");
                            if (categoryRows.Length > 0)
                            {
                                productRow["CategoryName"] = categoryRows[0]["CategoryName"];
                            }
                        }
                    }

                    gvProducts.DataSource = dtProducts;
                    gvProducts.DataBind();

                    // Apply styling
                    ApplyGridViewStyling();
                }
                else
                {
                    gvProducts.DataSource = new DataTable();
                    gvProducts.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Lỗi khi tải sản phẩm: {ex.Message}", MessageType.Error);
            }
        }

        private void ApplyGridViewStyling()
        {
            gvProducts.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            gvProducts.HeaderStyle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            gvProducts.HeaderStyle.Font.Bold = true;
            gvProducts.RowStyle.BackColor = System.Drawing.Color.White;
            gvProducts.AlternatingRowStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
        }

        public string GetStockClass(int stock)
        {
            if (stock > 50)
                return "stock-high";
            else if (stock > 10)
                return "stock-medium";
            else
                return "stock-low";
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            ClearProductForm();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showProductModal('Thêm Sản Phẩm Mới');", true);
        }

        protected void ProductCommand_Click(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int productId = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "EditProduct")
                {
                    LoadProductForEdit(productId);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showProductModal('Chỉnh Sửa Sản Phẩm');", true);
                }
                else if (e.CommandName == "DeleteProduct")
                {
                    int result = productDAL.XoaSanPham(productId);
                    if (result > 0)
                    {
                        DisplayMessage("✅ Xóa sản phẩm thành công!", MessageType.Success);
                        LoadProducts();
                    }
                    else
                    {
                        DisplayMessage("❌ Lỗi khi xóa sản phẩm.", MessageType.Error);
                    }
                }
                else if (e.CommandName == "Stock")
                {
                    LoadProductForStock(productId);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showStockModal", "showStockModal();", true);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Lỗi: {ex.Message}", MessageType.Error);
            }
        }

        private void LoadProductForEdit(int productId)
        {
            try
            {
                DataTable dtProduct = productDAL.LaySanPhamTheoID(productId);
                if (dtProduct != null && dtProduct.Rows.Count > 0)
                {
                    DataRow row = dtProduct.Rows[0];
                    hfProductID.Value = productId.ToString();
                    txtProductName.Text = row["ProductName"].ToString();
                    txtDescription.Text = row["Description"].ToString();
                    txtPrice.Text = row["Price"].ToString();
                    txtStockQuantity.Text = row["StockQuantity"].ToString();
                    txtImageURL.Text = row["ImageURL"].ToString();
                    ddlCategory.SelectedValue = row["CategoryID"].ToString();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Lỗi khi tải sản phẩm: {ex.Message}", MessageType.Error);
            }
        }

        private void LoadProductForStock(int productId)
        {
            try
            {
                DataTable dtProduct = productDAL.LaySanPhamTheoID(productId);
                if (dtProduct != null && dtProduct.Rows.Count > 0)
                {
                    DataRow row = dtProduct.Rows[0];
                    hfStockProductID.Value = productId.ToString();
                    lblStockProductName.Text = row["ProductName"].ToString();
                    lblCurrentStock.Text = row["StockQuantity"].ToString() + " cái";
                    txtNewStock.Text = row["StockQuantity"].ToString();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Lỗi: {ex.Message}", MessageType.Error);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                int ownerId = Convert.ToInt32(Session["UserID"]);
                DataTable dtStore = storeDAL.LayCuaHangTheoChu(ownerId);

                if (dtStore == null || dtStore.Rows.Count == 0)
                {
                    DisplayMessage("Lỗi: Không tìm thấy cửa hàng của bạn.", MessageType.Error);
                    return;
                }

                int storeId = Convert.ToInt32(dtStore.Rows[0]["StoreID"]);
                int productId = Convert.ToInt32(hfProductID.Value);
                string productName = txtProductName.Text.Trim();
                string description = txtDescription.Text.Trim();
                decimal price = Convert.ToDecimal(txtPrice.Text);
                int quantity = Convert.ToInt32(txtStockQuantity.Text);
                int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                string imageUrl = txtImageURL.Text.Trim();

                if (string.IsNullOrEmpty(imageUrl))
                    imageUrl = "~/Assets/images/placeholder.png";

                int result;

                if (productId == 0)
                {
                    // Add new product
                    result = productDAL.ThemSanPham(productName, description, price, quantity, imageUrl, categoryId, storeId);
                }
                else
                {
                    // Update existing product
                    result = productDAL.SuaSanPham(productId, productName, description, price, quantity, categoryId, imageUrl);
                }

                if (result > 0)
                {
                    DisplayMessage(productId == 0 ? "✅ Thêm sản phẩm thành công!" : "✅ Cập nhật sản phẩm thành công!", MessageType.Success);
                    LoadProducts();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal", "closeProductModal();", true);
                }
                else
                {
                    DisplayMessage("❌ Lỗi khi lưu sản phẩm.", MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"❌ Lỗi: {ex.Message}", MessageType.Error);
            }
        }

        protected void btnUpdateStock_Click(object sender, EventArgs e)
        {
            try
            {
                int productId = Convert.ToInt32(hfStockProductID.Value);
                int newStock = 0;
                if (!int.TryParse(txtNewStock.Text, out newStock))
                {
                    DisplayMessage("Số lượng không hợp lệ.", MessageType.Error);
                    return;
                }

                if (newStock < 0)
                {
                    DisplayMessage("Số lượng không được âm.", MessageType.Error);
                    return;
                }

                int result = productDAL.CapNhatTonKho(productId, newStock);

                if (result > 0)
                {
                    DisplayMessage("✅ Cập nhật tồn kho thành công!", MessageType.Success);
                    LoadProducts();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "closeStockModal", "closeStockModal();", true);
                }
                else
                {
                    DisplayMessage("❌ Lỗi khi cập nhật tồn kho (DAL returned 0).", MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"❌ Lỗi hệ thống: {ex.Message}", MessageType.Error);
            }
        }

        private void ClearProductForm()
        {
            hfProductID.Value = "0";
            txtProductName.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            txtStockQuantity.Text = "";
            txtImageURL.Text = "";
            ddlCategory.SelectedValue = "";
        }

        private void DisplayMessage(string message, MessageType type)
        {
            if (type == MessageType.Success)
            {
                pnlSuccess.Visible = true;
                pnlError.Visible = false;
                lblSuccess.Text = message;
            }
            else if (type == MessageType.Error)
            {
                pnlSuccess.Visible = false;
                pnlError.Visible = true;
                lblError.Text = message;
            }
        }

        private enum MessageType { Success, Error }

        protected void gvProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
