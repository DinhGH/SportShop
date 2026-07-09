using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using SportShop.App_Code;

namespace SportShop.GUI.Owner
{
    public partial class StoreInfo : System.Web.UI.Page
    {
        StoreDAL storeDAL = new StoreDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStoreInfo();
            }
        }

        private void LoadStoreInfo()
        {
            try
            {
                int ownerId = Convert.ToInt32(Session["UserID"]);

                // Lấy thông tin cửa hàng
                DataTable dtStore = storeDAL.LayCuaHangTheoChu(ownerId);

                if (dtStore == null || dtStore.Rows.Count == 0)
                {
                    DisplayMessage("Bạn chưa có cửa hàng nào. Vui lòng liên hệ quản trị viên.", MessageType.Info);
                    DisableForm();
                    return;
                }

                DataRow row = dtStore.Rows[0];

                // Điền dữ liệu vào form
                txtStoreName.Text = row["StoreName"].ToString();
                txtStoreAddress.Text = row["StoreAddress"].ToString();
                txtStorePhone.Text = row["StorePhone"].ToString();

                // Xử lý Logo
                if (dtStore.Columns.Contains("Logo") && row["Logo"] != null && !string.IsNullOrEmpty(row["Logo"].ToString()))
                {
                    string logoUrl = row["Logo"].ToString();
                    txtLogoUrl.Text = logoUrl;
                    lblCurrentLogo.Text = $"📷 Logo hiện tại: <a href='{logoUrl}' target='_blank'>Xem ảnh</a>";
                }
                else
                {
                    lblCurrentLogo.Text = "Chưa có logo";
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Lỗi khi tải thông tin: {ex.Message}", MessageType.Error);
                DisableForm();
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
                    DisplayMessage("Không tìm thấy cửa hàng.", MessageType.Error);
                    return;
                }

                int storeId = Convert.ToInt32(dtStore.Rows[0]["StoreID"]);
                string logoUrl = txtLogoUrl.Text.Trim();

                // Xử lý upload file logo nếu có
                if (fuLogo.HasFile)
                {
                    // Kiểm tra kích thước file (max 5MB)
                    if (fuLogo.PostedFile.ContentLength > 5 * 1024 * 1024)
                    {
                        DisplayMessage("Kích thước file quá lớn (tối đa 5MB).", MessageType.Error);
                        return;
                    }

                    // Kiểm tra loại file
                    string fileExtension = Path.GetExtension(fuLogo.FileName).ToLower();
                    if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                    {
                        DisplayMessage("Định dạng file không hợp lệ. Chỉ chấp nhận JPG, PNG.", MessageType.Error);
                        return;
                    }

                    try
                    {
                        // Tạo thư mục nếu chưa tồn tại
                        string uploadFolder = Server.MapPath("~/Assets/images/stores/");
                        if (!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }

                        // Tạo tên file duy nhất
                        string fileName = $"store_{storeId}_{DateTime.Now.Ticks}{fileExtension}";
                        string filePath = Path.Combine(uploadFolder, fileName);

                        // Lưu file
                        fuLogo.PostedFile.SaveAs(filePath);

                        // Lưu đường dẫn tương đối vào database
                        logoUrl = $"~/Assets/images/stores/{fileName}";
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"Lỗi khi upload file: {ex.Message}", MessageType.Error);
                        return;
                    }
                }

                // Cập nhật thông tin cửa hàng
                int result = storeDAL.CapNhatCuaHang(storeId, txtStoreName.Text, txtStoreAddress.Text, txtStorePhone.Text, logoUrl);

                if (result > 0)
                {
                    DisplayMessage("✅ Cập nhật thông tin cửa hàng thành công!", MessageType.Success);
                    // Tải lại thông tin để hiển thị logo mới
                    LoadStoreInfo();
                }
                else
                {
                    DisplayMessage("❌ Cập nhật thông tin không thành công. Vui lòng thử lại.", MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"❌ Lỗi: {ex.Message}", MessageType.Error);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GUI/Owner/Dashboard.aspx");
        }

        private void DisplayMessage(string message, MessageType type)
        {
            if (type == MessageType.Success)
            {
                pnlSuccess.Visible = true;
                pnlError.Visible = false;
                pnlInfo.Visible = false;
                lblSuccess.Text = message;
            }
            else if (type == MessageType.Error)
            {
                pnlSuccess.Visible = false;
                pnlError.Visible = true;
                pnlInfo.Visible = false;
                lblError.Text = message;
            }
            else if (type == MessageType.Info)
            {
                pnlSuccess.Visible = false;
                pnlError.Visible = false;
                pnlInfo.Visible = true;
                lblInfo.Text = message;
            }
        }

        private void DisableForm()
        {
            foreach (Control ctrl in Page.Form.Controls)
            {
                DisableControlsRecursive(ctrl);
            }
        }

        private void DisableControlsRecursive(Control control)
        {
            if (control is WebControl)
            {
                ((WebControl)control).Enabled = false;
            }

            foreach (Control ctrl in control.Controls)
            {
                DisableControlsRecursive(ctrl);
            }
        }

        private enum MessageType { Success, Error, Info }
    }
}
