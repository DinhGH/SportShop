<%@ Page Title="Quản Lý Cửa Hàng" Language="C#" MasterPageFile="~/GUI/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="QuanLyCuaHang.aspx.cs" Inherits="SportShop.GUI.Admin.QuanLyCuaHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-header { display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px; }
        .page-title { font-size: 24px; font-weight: bold; color: #1e293b; }
        .grid-view { width: 100%; border-collapse: collapse; background-color: #ffffff; border-radius: 8px; overflow: hidden; border: 1px solid #e2e8f0; box-shadow: 0 4px 6px rgba(0,0,0,0.01); }
        .grid-view th { background-color: #334155; color: #ffffff; padding: 12px 15px; font-weight: 600; text-align: left; }
        .grid-view td { padding: 12px 15px; border-bottom: 1px solid #e2e8f0; color: #334155; }
        .grid-view tr:hover { background-color: #f8fafc; }
        .btn-status { padding: 6px 12px; border: none; border-radius: 4px; font-weight: 600; cursor: pointer; font-size: 13px; color: #ffffff; }
        .btn-approve { background-color: #16a34a; } /* Xanh lá cho Duyệt */
        .btn-lock { background-color: #e11d48; }    /* Đỏ cho Khóa */
        .status-badge { padding: 4px 8px; border-radius: 12px; font-size: 12px; font-weight: 600; }
        .status-active { background-color: #dcfce7; color: #15803d; }
        .status-locked { background-color: #ffe4e6; color: #b91c1c; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <div class="page-title"><i class="fa-solid fa-store"></i> Quản Lý Cửa Hàng & Chủ Cửa Hàng</div>
    </div>

    <asp:Label ID="lblMsg" runat="server" CssClass="lbl-msg" Visible="false" style="display:block; margin-bottom:15px; font-weight:600; color:#16a34a;"></asp:Label>

    <asp:GridView ID="gvStores" runat="server" AutoGenerateColumns="False" CssClass="grid-view" DataKeyNames="StoreID" OnRowCommand="gvStores_RowCommand">
        <Columns>
            <asp:BoundField DataField="StoreID" HeaderText="Mã Shop" />
            <asp:BoundField DataField="StoreName" HeaderText="Tên Cửa Hàng" />
            <asp:BoundField DataField="OwnerName" HeaderText="Chủ Sở Hữu (Owner)" />
            <asp:BoundField DataField="StorePhone" HeaderText="Số Điện Thoại" />
            <asp:BoundField DataField="StoreAddress" HeaderText="Địa Chỉ" />
            
            <asp:TemplateField HeaderText="Trạng Thái">
                <ItemTemplate>
                    <span class='<%# Convert.ToBoolean(Eval("IsActive")) ? "status-badge status-active" : "status-badge status-locked" %>'>
                        <%# Convert.ToBoolean(Eval("IsActive")) ? "Đang hoạt động" : "Bị khóa" %>
                    </span>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Hành Động">
                <ItemTemplate>
                    <asp:Button ID="btnToggle" runat="server" 
                                CommandName="ToggleStatus" 
                                CommandArgument='<%# Container.DataItemIndex %>'
                                Text='<%# Convert.ToBoolean(Eval("IsActive")) ? "Khóa Shop" : "Duyệt Hoạt Động" %>' 
                                CssClass='<%# Convert.ToBoolean(Eval("IsActive")) ? "btn-status btn-lock" : "btn-status btn-approve" %>'
                                OnClientClick="return confirm('Bạn có chắc chắn muốn thay đổi trạng thái cửa hàng này?')" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>