<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SportShop.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>SportShop - Đăng Nhập</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <style>
        body { 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
            background-color: #f1f5f9; /* Nền xám sáng */
            margin: 0; 
            display: flex; 
            justify-content: center; 
            align-items: center; 
            height: 100vh; 
        }
        .auth-container { 
            background-color: #ffffff; /* Nền form trắng */
            padding: 40px; 
            border-radius: 12px; 
            box-shadow: 0 4px 12px rgba(0,0,0,0.05); 
            width: 100%; 
            max-width: 400px; 
            color: #1e293b; /* Chữ tối màu */
            border: 1px solid #e2e8f0;
        }
        .auth-header { text-align: center; margin-bottom: 30px; }
        .auth-header h2 { margin: 0; color: #0284c7; font-size: 28px; } /* Xanh dương điểm nhấn */
        .auth-header p { color: #64748b; margin: 5px 0 0 0; }

        .form-group { margin-bottom: 20px; position: relative; }
        .form-group i { position: absolute; left: 12px; top: 38px; color: #94a3b8; }
        .form-group label { display: block; margin-bottom: 8px; font-size: 14px; color: #334155; font-weight: 500; }
        .form-control { 
            width: 100%; 
            padding: 10px 12px 10px 38px; 
            border-radius: 6px; 
            border: 1px solid #cbd5e1; 
            background-color: #f8fafc; /* Ô nhập liệu xám nhạt */
            color: #1e293b; 
            font-size: 15px; 
            box-sizing: border-box; 
        }
        .form-control:focus { border-color: #0284c7; background-color: #ffffff; outline: none; }

        .btn-auth { 
            width: 100%; 
            padding: 12px; 
            background-color: #334155; /* Nút màu xám tối */
            border: none; 
            border-radius: 6px; 
            color: #ffffff; /* Chữ trắng nổi bật trên nút xám */
            font-size: 16px; 
            font-weight: bold; 
            cursor: pointer; 
            transition: background 0.3s; 
            margin-top: 10px; 
        }
        .btn-auth:hover { background-color: #1e293b; }

        .lbl-error { color: #e11d48; font-size: 14px; display: block; margin-bottom: 15px; text-align: center; font-weight: 500; }
        .auth-footer { text-align: center; margin-top: 25px; font-size: 14px; color: #64748b; }
        .auth-link { color: #0284c7; text-decoration: none; font-weight: bold; }
        .auth-link:hover { text-decoration: underline; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auth-container">
            <div class="auth-header">
                <h2><i class="fa-solid fa-dumbbell"></i> SportShop</h2>
                <p>Đăng nhập hệ thống cửa hàng</p>
            </div>

            <!-- Thông báo lỗi nếu có -->
            <asp:Label ID="lblMessage" runat="server" CssClass="lbl-error" Visible="false"></asp:Label>

            <div class="form-group">
                <label>Email tài khoản</label>
                <i class="fa-solid fa-envelope"></i>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="example@gmail.com" Required="true"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Mật khẩu</label>
                <i class="fa-solid fa-lock"></i>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="••••••••" Required="true"></asp:TextBox>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Đăng Nhập" CssClass="btn-auth" OnClick="btnLogin_Click" />

            <div class="auth-footer">
                Chưa có tài khoản? 
                <!-- Nút chuyển đổi sang trang đăng ký -->
                <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="~/Register.aspx" CssClass="auth-link">Đăng ký ngay</asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>