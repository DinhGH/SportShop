<%@ Page Title="Thông Tin Cá Nhân" Language="C#" MasterPageFile="~/GUI/Customer/Customer.Master" AutoEventWireup="true" CodeBehind="ThongTinCaNhan.aspx.cs" Inherits="SportShop.GUI.Customer.ThongTinCaNhan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .profile-wrapper { display: grid; grid-template-columns: 1fr 1fr; gap: 40px; }
        .profile-card { background-color: #ffffff; border: 1px solid #e2e8f0; border-radius: 12px; padding: 30px; box-shadow: 0 4px 6px rgba(0,0,0,0.02); }
        .section-title { font-size: 18px; font-weight: bold; margin-bottom: 20px; color: #1e293b; border-bottom: 2px solid #f1f5f9; padding-bottom: 10px; }
        .form-group { margin-bottom: 15px; }
        .form-group label { display: block; margin-bottom: 6px; font-size: 14px; font-weight: 500; color: #475569; }
        .form-control { width: 100%; padding: 10px; border: 1px solid #cbd5e1; border-radius: 6px; background-color: #f8fafc; color: #1e293b; box-sizing: border-box; }
        .form-control:focus { border-color: #0284c7; background-color: #ffffff; outline: none; }
        .btn-save { padding: 10px 20px; background-color: #334155; color: white; border: none; border-radius: 6px; font-weight: bold; cursor: pointer; }
        .btn-save:hover { background-color: #1e293b; }
        .lbl-msg { font-size: 14px; display: block; margin-top: 10px; font-weight: 500; }
        .text-danger { color: #e11d48; }
        .text-success { color: #16a34a; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="profile-wrapper">
        
        <!-- CỘT 1: THÔNG TIN CÁ NHÂN -->
        <div class="profile-card">
            <div class="section-title"><i class="fa-solid fa-id-card"></i> Quản Lý Thông Tin</div>
            <asp:Label ID="lblInfoMsg" runat="server" CssClass="lbl-msg" Visible="false"></asp:Label>
            
            <div class="form-group">
                <label>Email tài khoản (Không thể sửa)</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Họ và Tên</label>
                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Số điện thoại</label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Địa chỉ giao hàng</label>
                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <asp:Button ID="btnUpdateInfo" runat="server" Text="Lưu Thay Đổi" CssClass="btn-save" OnClick="btnUpdateInfo_Click" />
        </div>

        <!-- CỘT 2: ĐỔI MẬT KHẨU -->
        <div class="profile-card">
            <div class="section-title"><i class="fa-solid fa-lock-keyhole"></i> Đổi Mật Khẩu</div>
            <asp:Label ID="lblPassMsg" runat="server" CssClass="lbl-msg" Visible="false"></asp:Label>
            
            <div class="form-group">
                <label>Mật khẩu hiện tại</label>
                <asp:TextBox ID="txtOldPass" runat="server" TextMode="Password" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Mật khẩu mới</label>
                <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Xác nhận mật khẩu mới</label>
                <asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <asp:Button ID="btnUpdatePass" runat="server" Text="Cập Nhật Mật Khẩu" BaseColor="#ef4444" CssClass="btn-save" OnClick="btnUpdatePass_Click" />
        </div>

    </div>
</asp:Content>