<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SportShop.Register" %>

    <!DOCTYPE html>
    <html>

    <head runat="server">
        <title>SportShop - Đăng Ký</title>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
        <style>
            body {
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                background-color: #f1f5f9;
                /* Nền xám sáng */
                margin: 0;
                display: flex;
                justify-content: center;
                align-items: center;
                min-height: 100vh;
                padding: 20px 0;
            }

            .auth-container {
                background-color: #ffffff;
                /* Nền form trắng */
                padding: 40px;
                border-radius: 12px;
                box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
                width: 100%;
                max-width: 450px;
                color: #1e293b;
                border: 1px solid #e2e8f0;
            }

            .auth-header {
                text-align: center;
                margin-bottom: 25px;
            }

            .auth-header h2 {
                margin: 0;
                color: #0284c7;
                font-size: 28px;
            }

            .auth-header p {
                color: #64748b;
                margin: 5px 0 0 0;
            }

            .form-group {
                margin-bottom: 15px;
                position: relative;
            }

            .form-group i {
                position: absolute;
                left: 12px;
                top: 38px;
                color: #94a3b8;
            }

            .form-group label {
                display: block;
                margin-bottom: 6px;
                font-size: 14px;
                color: #334155;
                font-weight: 500;
            }

            .form-control {
                width: 100%;
                padding: 10px 12px 10px 38px;
                border-radius: 6px;
                border: 1px solid #cbd5e1;
                background-color: #f8fafc;
                color: #1e293b;
                font-size: 15px;
                box-sizing: border-box;
            }

            .form-control:focus {
                border-color: #0284c7;
                background-color: #ffffff;
                outline: none;
            }

            .btn-auth {
                width: 100%;
                padding: 12px;
                background-color: #334155;
                /* Nút xám tối tương thích */
                border: none;
                border-radius: 6px;
                color: white;
                font-size: 16px;
                font-weight: bold;
                cursor: pointer;
                transition: background 0.3s;
                margin-top: 15px;
            }

            .btn-auth:hover {
                background-color: #1e293b;
            }

            .lbl-msg {
                font-size: 14px;
                display: block;
                margin-bottom: 15px;
                text-align: center;
                font-weight: 500;
            }

            .lbl-error {
                color: #e11d48;
            }

            /* Màu đỏ thông báo lỗi */
            .lbl-success {
                color: #16a34a;
            }

            /* Màu xanh lá thông báo thành công */

            .auth-footer {
                text-align: center;
                margin-top: 20px;
                font-size: 14px;
                color: #64748b;
            }

            .auth-link {
                color: #0284c7;
                text-decoration: none;
                font-weight: bold;
            }

            .auth-link:hover {
                text-decoration: underline;
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="auth-container">
                <div class="auth-header">
                    <h2><i class="fa-solid fa-user-plus"></i> Tạo Tài Khoản</h2>
                    <p>Gia nhập cộng đồng SportShop</p>
                </div>

                <!-- Nhãn hiển thị thông báo -->
                <asp:Label ID="lblStatus" runat="server" CssClass="lbl-msg" Visible="false"></asp:Label>

                <div class="form-group">
                    <label>Họ và Tên</label>
                    <i class="fa-solid fa-user"></i>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Nguyễn Văn A"
                        Required="true"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Email</label>
                    <i class="fa-solid fa-envelope"></i>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"
                        placeholder="an@gmail.com" Required="true"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Số điện thoại</label>
                    <i class="fa-solid fa-phone"></i>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="0935xxxxxx"
                        Required="true"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Địa chỉ</label>
                    <i class="fa-solid fa-location-dot"></i>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"
                        placeholder="Liên Chiểu, Đà Nẵng" Required="true"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Mật khẩu</label>
                    <i class="fa-solid fa-lock"></i>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"
                        placeholder="Tối thiểu 6 ký tự" Required="true"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Xác nhận mật khẩu</label>
                    <i class="fa-solid fa-shield-halved"></i>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"
                        placeholder="Nhập lại mật khẩu" Required="true"></asp:TextBox>
                </div>

                <asp:Button ID="btnRegister" runat="server" Text="Đăng Ký Tài Khoản" CssClass="btn-auth"
                    OnClick="btnRegister_Click" />

                <div class="auth-footer">
                    Đã có tài khoản?
                    <!-- Nút chuyển đổi ngược lại trang đăng nhập -->
                    <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="~/Login.aspx" CssClass="auth-link">Đăng
                        nhập</asp:HyperLink>
                </div>
            </div>
        </form>
    </body>

    </html>