<%@ Page Title="Quản Lý Khách Hàng" Language="C#" MasterPageFile="~/GUI/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="QuanLyKhachHang.aspx.cs" Inherits="SportShop.GUI.Admin.QuanLyKhachHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-title { font-size: 24px; font-weight: bold; margin-bottom: 20px; color: #1e293b; }
        .grid-view { width: 100%; border-collapse: collapse; background-color: #ffffff; border-radius: 8px; overflow: hidden; border: 1px solid #e2e8f0; }
        .grid-view th { background-color: #334155; color: #ffffff; padding: 12px 15px; font-weight: 600; text-align: left; }
        .grid-view td { padding: 12px 15px; border-bottom: 1px solid #e2e8f0; color: #334155; }
        .grid-view tr:hover { background-color: #f8fafc; }
        .btn-action { padding: 6px 12px; border: none; border-radius: 4px; font-weight: 600; cursor: pointer; font-size: 13px; color: white; }
        .btn-unlock { background-color: #16a34a; }
        .btn-lock { background-color: #e11d48; }
        .status-badge { padding: 4px 8px; border-radius: 12px; font-size: 12px; font-weight: 600; }
        .status-ok { background-color: #dcfce7; color: #15803d; }
        .status-ban { background-color: #ffe4e6; color: #b91c1c; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title"><i class="fa-solid fa-users"></i> Quản Lý Tài Khoản Khách Hàng</div>

    <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CssClass="grid-view" DataKeyNames="UserID" OnRowCommand="gvCustomers_RowCommand">
        <Columns>
            <asp:BoundField DataField="UserID" HeaderText="Mã KH" />
            <asp:BoundField DataField="FullName" HeaderText="Họ và Tên" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Phone" HeaderText="Số Điện Thoại" />
            <asp:BoundField DataField="Address" HeaderText="Địa Chỉ" />
            
            <asp:TemplateField HeaderText="Trạng Thái">
                <ItemTemplate>
                    <!-- Đổi IsAvailable thành IsActive -->
                    <span class='<%# Convert.ToBoolean(Eval("IsActive")) ? "status-badge status-ok" : "status-badge status-ban" %>'>
                        <%# Convert.ToBoolean(Eval("IsActive")) ? "Đang hoạt động" : "Đã khóa" %>
                    </span>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Hành Động">
                <ItemTemplate>
                    <!-- Đổi toàn bộ IsAvailable thành IsActive ở các dòng dưới đây -->
                    <asp:Button ID="btnStatus" runat="server" 
                                CommandName="ChangeStatus" 
                                CommandArgument='<%# Container.DataItemIndex %>'
                                Text='<%# Convert.ToBoolean(Eval("IsActive")) ? "Khóa Tài Khoản" : "Mở Khóa" %>' 
                                CssClass='<%# Convert.ToBoolean(Eval("IsActive")) ? "btn-action btn-lock" : "btn-action btn-unlock" %>'
                                OnClientClick="return confirm('Xác nhận thay đổi trạng thái tài khoản khách hàng này?')" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>