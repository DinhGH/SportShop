<%@ Page Title="Báo Cáo Sự Cố - SportShop" Language="C#" MasterPageFile="~/GUI/Customer/Customer.Master" AutoEventWireup="true" CodeBehind="Ticket.aspx.cs" Inherits="SportShop.GUI.Customer.Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-title { font-size: 24px; font-weight: bold; margin-bottom: 20px; color: #1e293b; }
        .container-ticket { display: grid; grid-template-columns: 1fr 2fr; gap: 30px; margin-bottom: 40px; align-items: start; }
        
        /* CSS Khu vực Form gửi */
        .ticket-form { background-color: #ffffff; border: 1px solid #e2e8f0; border-radius: 12px; padding: 25px; box-shadow: 0 4px 6px rgba(0,0,0,0.02); }
        .form-group { display: flex; flex-direction: column; gap: 6px; margin-bottom: 15px; }
        .form-group label { font-size: 14px; font-weight: 600; color: #475569; }
        .form-control { padding: 10px 12px; border: 1px solid #cbd5e1; border-radius: 6px; font-size: 14px; outline: none; width: 100%; box-sizing: border-box; }
        .form-control:focus { border-color: #0284c7; box-shadow: 0 0 0 2px rgba(2, 132, 199, 0.2); }
        .btn-submit { width: 100%; padding: 10px; background-color: #ef4444; color: white; border: none; border-radius: 6px; font-weight: bold; cursor: pointer; transition: background 0.2s; font-size: 15px; }
        .btn-submit:hover { background-color: #dc2626; }
        
        /* CSS Bảng lịch sử báo cáo */
        .ticket-history { background-color: #ffffff; border: 1px solid #e2e8f0; border-radius: 12px; padding: 25px; box-shadow: 0 4px 6px rgba(0,0,0,0.02); }
        .table-ticket { width: 100%; border-collapse: collapse; margin-top: 15px; }
        .table-ticket th, .table-ticket td { padding: 12px; text-align: left; border-bottom: 1px solid #e2e8f0; font-size: 14px; }
        .table-ticket th { background-color: #f8fafc; color: #475569; font-weight: 600; }
        
        /* Nhãn trạng thái */
        .badge { padding: 4px 8px; border-radius: 4px; font-size: 12px; font-weight: bold; display: inline-block; }
        .badge-pending { background-color: #fef3c7; color: #d97706; } /* Chưa xử lý */
        .badge-processing { background-color: #e0f2fe; color: #0369a1; } /* Đang xử lý */
        .badge-success { background-color: #dcfce7; color: #15803d; } /* Đã giải quyết */
        
        .msg-alert { font-size: 14px; font-weight: 500; margin-bottom: 15px; display: block; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title"><i class="fa-solid fa-triangle-exclamation" style="color: #ef4444;"></i> Trung Tâm Báo Cáo Sự Cố & Khiếu Nại</div>
    
    <div class="container-ticket">
        <!-- BÊN TRÁI: Form Gửi Báo Cáo -->
        <div class="ticket-form">
            <h3 style="margin-top:0; color:#1e293b; margin-bottom: 15px;">Gửi phản hồi mới</h3>
            <asp:Label ID="lblMessage" runat="server" CssClass="msg-alert" Visible="false"></asp:Label>
            
            <div class="form-group">
                <label>Tiêu đề sự cố / Lỗi đơn hàng</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Placeholder="Ví dụ: Lỗi không thanh toán được đơn hàng #105"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Nội dung chi tiết sự cố</label>
                <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" Placeholder="Mô tả chi tiết lỗi gặp phải để ban quản trị hỗ trợ bạn nhanh nhất..."></asp:TextBox>
            </div>
            
            <asp:Button ID="btnSend" runat="server" Text="🚀 Gửi báo cáo cho Admin" CssClass="btn-submit" OnClick="btnSend_Click" />
        </div>

        <!-- BÊN PHẢI: Danh Sách Lịch Sử Gửi Báo Cáo -->
        <div class="ticket-history">
            <h3 style="margin-top:0; color:#1e293b; margin-bottom: 15px;">Lịch sử phản hồi của bạn</h3>
            
            <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="False" CssClass="table-ticket" GridLines="None" EmptyDataText="Bạn chưa gửi báo cáo sự cố nào trên hệ thống.">
                <Columns>
                    <asp:BoundField DataField="TicketID" HeaderText="Mã lỗi" ItemStyle-Width="70px" />
                    
                    <asp:BoundField DataField="Title" HeaderText="Tiêu đề" />
                    
                    <%-- 1. Đã chuyển cột Content thành TemplateField để khống chế chiều rộng, tự xuống dòng tránh vỡ khung --%>
                    <asp:TemplateField HeaderText="Nội dung">
                        <ItemTemplate>
                            <div style="max-width: 300px; word-wrap: break-word; overflow: hidden; text-overflow: ellipsis;">
                                <%# Eval("Content") %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="CreatedAt" HeaderText="Thời gian gửi" DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-Width="130px" />
                    
                    <%-- 2. Đã sửa lỗi TemplateField bằng cách khai báo chiều rộng thông qua thẻ con <ItemStyle> --%>
                    <asp:TemplateField HeaderText="Trạng thái xử lý">
                        <ItemStyle Width="120px" />
                        <ItemTemplate>
                            <%# GetStatusBadge(Eval("Status").ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>