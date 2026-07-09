<%@ Page Title="Quản Lý Thông Tin Cửa Hàng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="StoreInfo.aspx.cs" Inherits="SportShop.GUI.Owner.StoreInfo" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
            /* Quy tắc màu sắc theo rule.md: Minimalist Light Mode */
            body {
                background-color: #F8FAFC;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                color: #1E293B;
            }

            .store-info-container {
                padding: 40px 0;
                background-color: #F8FAFC;
            }

            .page-header {
                margin-bottom: 40px;
            }

            .page-title {
                font-size: 28px;
                font-weight: 600;
                color: #1E293B;
                margin-bottom: 8px;
            }

            .page-subtitle {
                font-size: 14px;
                color: #64748B;
            }

            /* Form Container */
            .form-container {
                background: #FFFFFF;
                border-radius: 12px;
                padding: 32px;
                box-shadow: 0 1px 3px rgba(0, 0, 0, 0.08);
                max-width: 600px;
            }

            .form-section {
                margin-bottom: 32px;
            }

            .form-section:last-child {
                margin-bottom: 0;
            }

            .section-title {
                font-size: 14px;
                font-weight: 600;
                color: #1E293B;
                text-transform: uppercase;
                letter-spacing: 0.5px;
                margin-bottom: 16px;
                padding-bottom: 12px;
                border-bottom: 2px solid #E2E8F0;
            }

            /* Form Group */
            .form-group {
                margin-bottom: 20px;
            }

            .form-group:last-child {
                margin-bottom: 0;
            }

            label {
                display: block;
                font-size: 13px;
                font-weight: 600;
                color: #1E293B;
                margin-bottom: 8px;
            }

            .form-control {
                width: 100%;
                padding: 10px 12px;
                border: 1px solid #E2E8F0;
                border-radius: 8px;
                font-size: 14px;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                color: #1E293B;
                background-color: #FFFFFF;
                transition: all 0.2s ease;
                box-sizing: border-box;
            }

            .form-control:focus {
                outline: none;
                border-color: #0284C7;
                box-shadow: 0 0 0 3px rgba(2, 132, 199, 0.1);
                background-color: #FFFFFF;
            }

            .form-control:disabled {
                background-color: #F1F5F9;
                color: #64748B;
                cursor: not-allowed;
            }

            .form-text {
                font-size: 12px;
                color: #64748B;
                margin-top: 4px;
            }

            /* Logo Preview */
            .logo-preview-container {
                margin-bottom: 16px;
            }

            .logo-preview {
                width: 120px;
                height: 120px;
                border-radius: 8px;
                background-color: #F1F5F9;
                border: 2px dashed #E2E8F0;
                display: flex;
                align-items: center;
                justify-content: center;
                font-size: 48px;
                overflow: hidden;
            }

            .logo-preview img {
                width: 100%;
                height: 100%;
                object-fit: cover;
            }

            .logo-preview.empty {
                color: #CBD5E1;
                font-size: 36px;
            }

            /* Buttons */
            .button-group {
                display: flex;
                gap: 12px;
                margin-top: 32px;
                padding-top: 24px;
                border-top: 1px solid #E2E8F0;
            }

            .btn {
                padding: 10px 20px;
                border-radius: 8px;
                font-size: 14px;
                font-weight: 600;
                border: none;
                cursor: pointer;
                transition: all 0.2s ease;
                text-align: center;
                text-decoration: none;
                display: inline-block;
            }

            .btn-primary {
                background-color: #0284C7;
                color: white;
            }

            .btn-primary:hover {
                background-color: #0369A1;
                box-shadow: 0 4px 12px rgba(2, 132, 199, 0.3);
            }

            .btn-primary:active {
                transform: scale(0.98);
            }

            .btn-secondary {
                background-color: #E2E8F0;
                color: #1E293B;
            }

            .btn-secondary:hover {
                background-color: #CBD5E1;
            }

            /* Alert Messages */
            .alert {
                padding: 12px 16px;
                border-radius: 8px;
                margin-bottom: 24px;
                font-size: 14px;
                border-left: 4px solid;
            }

            .alert-success {
                background-color: #F0FDF4;
                border-left-color: #16A34A;
                color: #166534;
            }

            .alert-error {
                background-color: #FEF2F2;
                border-left-color: #E11D48;
                color: #BE123C;
            }

            .alert-info {
                background-color: #F0F9FF;
                border-left-color: #0284C7;
                color: #0C4A6E;
            }

            /* Responsive */
            @media (max-width: 768px) {
                .store-info-container {
                    padding: 20px 0;
                }

                .form-container {
                    padding: 24px;
                }

                .page-title {
                    font-size: 24px;
                }

                .button-group {
                    flex-direction: column;
                }

                .btn {
                    width: 100%;
                }
            }

            /* Disabled state */
            .form-section.disabled {
                opacity: 0.6;
                pointer-events: none;
            }
        </style>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="store-info-container container">
            <div class="page-header">
                <h1 class="page-title">⚙️ Quản Lý Thông Tin Cửa Hàng</h1>
                <p class="page-subtitle">Cập nhật tên shop, logo, địa chỉ và số điện thoại</p>
            </div>

            <!-- Alert Messages -->
            <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success">
                <asp:Label ID="lblSuccess" runat="server"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-error">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="pnlInfo" runat="server" Visible="false" CssClass="alert alert-info">
                <asp:Label ID="lblInfo" runat="server"></asp:Label>
            </asp:Panel>

            <!-- Form Container -->
            <div class="form-container">
                <!-- Thông Tin Cơ Bản -->
                <div class="form-section">
                    <div class="section-title">Thông Tin Cơ Bản</div>

                    <div class="form-group">
                        <label for="txtStoreName">Tên Cửa Hàng <span style="color: #E11D48;">*</span></label>
                        <asp:TextBox ID="txtStoreName" runat="server" CssClass="form-control" MaxLength="100"
                            Placeholder="Nhập tên cửa hàng"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvStoreName" runat="server" ControlToValidate="txtStoreName"
                            ErrorMessage="Tên cửa hàng không được để trống" CssClass="form-text" ForeColor="#E11D48"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <div class="form-text">Tên cửa hàng sẽ hiển thị trên website</div>
                    </div>

                    <div class="form-group">
                        <label for="txtStoreAddress">Địa Chỉ <span style="color: #E11D48;">*</span></label>
                        <asp:TextBox ID="txtStoreAddress" runat="server" CssClass="form-control" MaxLength="200"
                            Placeholder="Nhập địa chỉ cửa hàng" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvStoreAddress" runat="server"
                            ControlToValidate="txtStoreAddress" ErrorMessage="Địa chỉ không được để trống"
                            CssClass="form-text" ForeColor="#E11D48" Display="Dynamic"></asp:RequiredFieldValidator>
                        <div class="form-text">Ví dụ: 123 Đường ABC, Quận 1, TP. HCM</div>
                    </div>

                    <div class="form-group">
                        <label for="txtStorePhone">Số Điện Thoại <span style="color: #E11D48;">*</span></label>
                        <asp:TextBox ID="txtStorePhone" runat="server" CssClass="form-control" MaxLength="20"
                            Placeholder="Nhập số điện thoại"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvStorePhone" runat="server" ControlToValidate="txtStorePhone"
                            ErrorMessage="Số điện thoại không được để trống" CssClass="form-text" ForeColor="#E11D48"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revStorePhone" runat="server"
                            ControlToValidate="txtStorePhone" ValidationExpression="^[0-9\-\+\s\(\)]{10,}$"
                            ErrorMessage="Số điện thoại không hợp lệ" CssClass="form-text" ForeColor="#E11D48"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                        <div class="form-text">Ví dụ: 0123 456 789 hoặc +84 123 456 789</div>
                    </div>
                </div>

                <!-- Logo -->
                <div class="form-section">
                    <div class="section-title">Logo Cửa Hàng</div>

                    <div class="logo-preview-container">
                        <div class="logo-preview" id="logoPreview">
                            <span class="empty">🏪</span>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="fuLogo">Tải Lên Logo</label>
                        <asp:FileUpload ID="fuLogo" runat="server" CssClass="form-control" Accept="image/*" />
                        <div class="form-text">Định dạng: JPG, PNG (Tối đa 5MB)</div>
                        <asp:Label ID="lblCurrentLogo" runat="server" CssClass="form-text"
                            style="display: block; margin-top: 8px;"></asp:Label>
                    </div>

                    <div class="form-group">
                        <label for="txtLogoUrl">URL Logo (Nếu có)</label>
                        <asp:TextBox ID="txtLogoUrl" runat="server" CssClass="form-control"
                            Placeholder="Hoặc dán link ảnh trực tiếp" MaxLength="500"></asp:TextBox>
                        <div class="form-text">Nếu không tải file, bạn có thể dán URL của ảnh</div>
                    </div>
                </div>

                <!-- Action Buttons -->
                <div class="button-group">
                    <asp:Button ID="btnSave" runat="server" Text="💾 Lưu Thay Đổi" CssClass="btn btn-primary"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="❌ Hủy" CssClass="btn btn-secondary"
                        OnClick="btnCancel_Click" CausesValidation="false" />
                </div>
                </form>
            </div>
        </div>

        <script>
            // Preview logo khi chọn file
            function previewLogo(event) {
                var file = event.target.files[0];
                if (file) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var preview = document.getElementById('logoPreview');
                        preview.innerHTML = '<img src="' + e.target.result + '" />';
                        preview.classList.remove('empty');
                    };
                    reader.readAsDataURL(file);
                }
            }

            // Gắn sự kiện vào file upload
            document.getElementById('<%= fuLogo.ClientID %>').addEventListener('change', previewLogo);
        </script>
    </asp:Content>