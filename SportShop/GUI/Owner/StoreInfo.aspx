<%@ Page Title="Quản Lý Thông Tin Cửa Hàng" Language="C#" MasterPageFile="~/GUI/Owner/Owner.Master"
    AutoEventWireup="true" CodeBehind="StoreInfo.aspx.cs" Inherits="SportShop.GUI.Owner.StoreInfo" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
            body {
                background-color: #F8FAFC;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                color: #1E293B;
            }

            .store-info-container {
                padding: 0px 40px;
                display: block;
            }

            .store-info-wrapper {
                width: 100%;
                max-width: 980px;
            }

            .page-header {
                margin-bottom: 30px;
                padding-bottom: 20px;
                border-bottom: 1px solid #E5E7EB;
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

            .form-container {
                background: #FFFFFF;
                border-radius: 18px;
                padding: 30px;
                box-shadow: 0 10px 25px rgba(15, 23, 42, 0.05);
                border: 1px solid #E5E7EB;
                width: 100%;
            }

            .form-grid {
                display: grid;
                grid-template-columns: 1fr 320px;
                gap: 30px;
            }

            .logo-card {
                border-left: 1px solid #E5E7EB;
                padding-left: 30px;
            }

            .form-section {
                margin-bottom: 24px;
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

            .form-group {
                margin-bottom: 16px;
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
                height: 46px;
                padding: 12px 15px;
                border: 1px solid #D1D5DB;
                border-radius: 10px;
                font-size: 14px;
                transition: .25s;
                box-sizing: border-box;
            }

            textarea.form-control {
                height: 110px;
                resize: vertical;
            }

            .form-control:focus {
                outline: none;
                border-color: #0284C7;
                box-shadow: 0 0 0 3px rgba(2, 132, 199, 0.1);
            }

            .form-text {
                font-size: 12px;
                color: #64748B;
                margin-top: 4px;
            }

            .logo-preview {
                width: 180px;
                height: 180px;
                margin: auto;
                border-radius: 16px;
                border: 2px dashed #CBD5E1;
                display: flex;
                align-items: center;
                justify-content: center;
                font-size: 48px;
                overflow: hidden;
                margin-bottom: 16px;
            }

            .logo-preview img {
                width: 100%;
                height: 100%;
                object-fit: cover;
            }

            .button-group {
                display: flex;
                gap: 12px;
                margin-top: 24px;
                padding-top: 20px;
                border-top: 1px solid #E2E8F0;
                justify-content: flex-end;
            }

            .btn {
                padding: 10px 20px;
                border-radius: 10px;
                font-size: 14px;
                font-weight: 600;
                border: none;
                cursor: pointer;
                min-width: 150px;
                height: 46px;
                text-align: center;
            }

            .btn-primary {
                background-color: #0284C7;
                color: white;
            }

            .btn-secondary {
                background-color: #E2E8F0;
                color: #1E293B;
            }

            @media(max-width: 900px) {
                .form-grid {
                    grid-template-columns: 1fr;
                }

                .logo-card {
                    border-left: none;
                    border-top: 1px solid #eee;
                    padding-left: 0;
                    padding-top: 25px;
                }
            }
        </style>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="store-info-container">
            <div class="store-info-wrapper">
                <div class="page-header">
                    <h1 class="page-title">⚙️ Quản Lý Thông Tin Cửa Hàng</h1>
                    <p class="page-subtitle">Cập nhật tên shop, logo, địa chỉ và số điện thoại</p>
                </div>

                <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success"
                    style="padding:15px; background:#dcfce7; color:#166534; border-radius:10px; margin-bottom:20px;">
                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger"
                    style="padding:15px; background:#fee2e2; color:#991b1b; border-radius:10px; margin-bottom:20px;">
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlInfo" runat="server" Visible="false" CssClass="alert alert-info"
                    style="padding:15px; background:#e0f2fe; color:#075985; border-radius:10px; margin-bottom:20px;">
                    <asp:Label ID="lblInfo" runat="server"></asp:Label>
                </asp:Panel>

                <div class="form-container">
                    <div class="form-grid">
                        <div>
                            <div class="form-section">
                                <div class="section-title">Thông Tin Cơ Bản</div>
                                <div class="form-group">
                                    <label for="txtStoreName">Tên Cửa Hàng *</label>
                                    <asp:TextBox ID="txtStoreName" runat="server" CssClass="form-control"
                                        Placeholder="Nhập tên cửa hàng"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtStoreAddress">Địa Chỉ *</label>
                                    <asp:TextBox ID="txtStoreAddress" runat="server" CssClass="form-control"
                                        TextMode="MultiLine" Placeholder="Nhập địa chỉ"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtStorePhone">Số Điện Thoại *</label>
                                    <asp:TextBox ID="txtStorePhone" runat="server" CssClass="form-control"
                                        Placeholder="Nhập số điện thoại"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="logo-card">
                            <div class="section-title">Logo Cửa Hàng</div>
                            <div class="logo-preview" id="logoPreview">
                                <asp:Image ID="imgLogo" runat="server" AlternateText="Logo" />
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblCurrentLogo" runat="server" CssClass="form-text"></asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:FileUpload ID="fuLogo" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtLogoUrl" runat="server" CssClass="form-control"
                                    Placeholder="URL Logo"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="button-group">
                        <asp:Button ID="btnCancel" runat="server" Text="Hủy" CssClass="btn btn-secondary"
                            OnClick="btnCancel_Click" CausesValidation="false" />
                        <asp:Button ID="btnSave" runat="server" Text="Lưu Thay Đổi" CssClass="btn btn-primary"
                            OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Content>