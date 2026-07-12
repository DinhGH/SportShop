<%@ Page Title="Quản Lý Sự Cố" Language="C#" MasterPageFile="~/GUI/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="QuanLyTickets.aspx.cs" Inherits="SportShop.GUI.Admin.QuanLyTickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .grid-view { width: 100%; border-collapse: collapse; background: #fff; border: 1px solid #e2e8f0; border-radius: 8px; overflow: hidden; }
        .grid-view th { background-color: #334155; color: #fff; padding: 12px; text-align: left; }
        .grid-view td { padding: 12px; border-bottom: 1px solid #e2e8f0; color: #334155; font-size: 14px; }
        .btn-update { background-color: #0284c7; color: white; border: none; padding: 6px 12px; border-radius: 4px; cursor: pointer; font-weight: bold; }
        .btn-update:hover { background-color: #0369a1; }
        .ddl-status { padding: 5px; border-radius: 4px; border: 1px solid #cbd5e1; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><i class="fa-solid fa-ticket"></i> Quản Lý Sự Cố & Khiếu Nại (Tickets)</h2>
    <p style="color: #64748b; margin-bottom: 20px;">Danh sách phản hồi từ đối tác và khách hàng</p>

    <!-- Lưu ý các thuộc tính: DataKeyNames, OnRowDataBound, OnRowCommand phải chính xác -->
    <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="False" 
                  DataKeyNames="TicketID" 
                  OnRowDataBound="gvTickets_RowDataBound" 
                  OnRowCommand="gvTickets_RowCommand" 
                  CssClass="grid-view">
        <Columns>
            <asp:BoundField DataField="TicketID" HeaderText="Mã Ticket" ItemStyle-Width="80px" />
            <asp:BoundField DataField="SenderName" HeaderText="Người Gửi" ItemStyle-Width="150px" />
            <asp:BoundField DataField="Title" HeaderText="Tiêu Đề Sự Cố" />
            <asp:BoundField DataField="Content" HeaderText="Nội Dung Chi Tiết" />
            <asp:BoundField DataField="CreatedAt" HeaderText="Ngày Gửi" DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-Width="150px" />
            
            <%-- Cột trạng thái xử lý sử dụng DropDownList --%>
            <asp:TemplateField HeaderText="Trạng Thái Xử Lý" ItemStyle-Width="180px">
                <ItemTemplate>
                    <!-- Trường ẩn lưu giá trị từ DB phục vụ RowDataBound -->
                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("Status") %>' />
                    
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl-status">
                        <asp:ListItem Value="Chưa xử lý">Chưa xử lý</asp:ListItem>
                        <asp:ListItem Value="Đang xử lý">Đang xử lý</asp:ListItem>
                        <asp:ListItem Value="Đã giải quyết">Đã giải quyết</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- Cột nút bấm cập nhật --%>
            <asp:TemplateField HeaderText="Thao Tác" ItemStyle-Width="120px">
                <ItemTemplate>
                    <asp:Button ID="btnUpdate" runat="server" 
                                Text="Cập Nhật" 
                                CommandName="UpdateTicketStatus" 
                                CommandArgument='<%# Container.DataItemIndex %>' 
                                CssClass="btn-update" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>