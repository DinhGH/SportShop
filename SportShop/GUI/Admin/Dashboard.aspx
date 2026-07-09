<%@ Page Title="Hệ Thống Thống Kê" Language="C#" MasterPageFile="~/GUI/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SportShop.GUI.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .dashboard-grid { display: grid; grid-template-columns: repeat(4, 1fr); gap: 20px; margin-top: 20px; }
        .stat-box { background-color: #ffffff; border: 1px solid #e2e8f0; border-radius: 12px; padding: 25px; box-shadow: 0 4px 6px rgba(0,0,0,0.01); display: flex; align-items: center; gap: 20px; }
        .stat-icon { width: 50px; height: 50px; border-radius: 10px; display: flex; justify-content: center; align-items: center; font-size: 24px; color: white; }
        .bg-blue { background-color: #0284c7; }
        .bg-orange { background-color: #f97316; }
        .bg-green { background-color: #16a34a; }
        .bg-purple { background-color: #7c3aed; }
        .stat-info .stat-num { font-size: 24px; font-weight: bold; color: #1e293b; margin: 0; }
        .stat-info .stat-label { font-size: 14px; color: #64748b; margin: 2px 0 0 0; font-weight: 500; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><i class="fa-solid fa-chart-pie"></i> Bảng Điều Khiển Hệ Thống</h2>
    <p style="color: #64748b;">Số liệu thống kê thời gian thực của SportShop</p>

    <div class="dashboard-grid">
        <!-- Tổng số Store -->
        <div class="stat-box">
            <div class="stat-icon bg-blue"><i class="fa-solid fa-shop"></i></div>
            <div class="stat-info">
                <p class="stat-num"><asp:Label ID="lblTotalStores" runat="server" Text="0"></asp:Label></p>
                <p class="stat-label">Cửa Hàng Hoạt Động</p>
            </div>
        </div>

        <!-- Tổng số Owner -->
        <div class="stat-box">
            <div class="stat-icon bg-orange"><i class="fa-solid fa-user-tie"></i></div>
            <div class="stat-info">
                <p class="stat-num"><asp:Label ID="lblTotalOwners" runat="server" Text="0"></asp:Label></p>
                <p class="stat-label">Đối Tác Chủ Shop</p>
            </div>
        </div>

        <!-- Tổng số Customer -->
        <div class="stat-box">
            <div class="stat-icon bg-green"><i class="fa-solid fa-users"></i></div>
            <div class="stat-info">
                <p class="stat-num"><asp:Label ID="lblTotalCustomers" runat="server" Text="0"></asp:Label></p>
                <p class="stat-label">Khách Hàng Đăng Ký</p>
            </div>
        </div>

        <!-- Doanh thu chiết khấu hệ thống (Ví dụ sàn thu 10% tổng đơn hàng thành công) -->
        <div class="stat-box">
            <div class="stat-icon bg-purple"><i class="fa-solid fa-wallet"></i></div>
            <div class="stat-info">
                <p class="stat-num"><asp:Label ID="lblRevenue" runat="server" Text="0 đ"></asp:Label></p>
                <p class="stat-label">Doanh Thu Hệ Thống (10%)</p>
            </div>
        </div>
    </div>
</asp:Content>