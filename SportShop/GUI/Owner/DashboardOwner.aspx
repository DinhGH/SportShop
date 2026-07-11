<%@ Page Title="Tổng Quan Cửa Hàng" Language="C#" MasterPageFile="~/GUI/Owner/Owner.Master" AutoEventWireup="true" CodeBehind="DashboardOwner.aspx.cs" Inherits="SportShop.GUI.Owner.DashboardOwner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-header { margin-bottom: 25px; }
        .page-title { font-size: 24px; font-weight: bold; color: #1e293b; margin: 0; }
        .store-name-display { font-size: 15px; color: #0284c7; font-weight: 600; margin-top: 5px; display: block; }
        
        /* Cụm hộp số liệu thống kê */
        .dashboard-grid { display: grid; grid-template-columns: repeat(3, 1fr); gap: 20px; margin-bottom: 35px; }
        .stat-box { background-color: #ffffff; border: 1px solid #e2e8f0; border-radius: 12px; padding: 25px; box-shadow: 0 4px 6px rgba(0,0,0,0.01); display: flex; align-items: center; gap: 20px; }
        .stat-icon { width: 50px; height: 50px; border-radius: 10px; display: flex; justify-content: center; align-items: center; font-size: 24px; color: white; }
        .bg-blue { background-color: #0284c7; }
        .bg-green { background-color: #16a34a; }
        .bg-gray { background-color: #475569; }
        .stat-info .stat-num { font-size: 24px; font-weight: bold; color: #1e293b; margin: 0; }
        .stat-info .stat-label { font-size: 14px; color: #64748b; margin: 2px 0 0 0; font-weight: 500; }

        /* Bảng sản phẩm bán chạy */
        .section-title { font-size: 18px; font-weight: bold; margin-bottom: 15px; color: #1e293b; display: flex; align-items: center; gap: 10px; }
        .grid-view { width: 100%; border-collapse: collapse; background-color: #ffffff; border-radius: 8px; overflow: hidden; border: 1px solid #e2e8f0; box-shadow: 0 4px 6px rgba(0,0,0,0.01); }
        .grid-view th { background-color: #334155; color: #ffffff; padding: 12px 15px; font-weight: 600; text-align: left; }
        .grid-view td { padding: 12px 15px; border-bottom: 1px solid #e2e8f0; color: #334155; font-size: 14px; }
        .grid-view tr:hover { background-color: #f8fafc; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h2 class="page-title"><i class="fa-solid fa-chart-line"></i> Báo Cáo Thống Kê Cửa Hàng</h2>
        <span class="store-name-display"><i class="fa-solid fa-shop"></i> <asp:Label ID="lblStoreName" runat="server" Text="Đang tải tên cửa hàng..."></asp:Label></span>
    </div>

    <!-- KHU VỰC THỐNG KÊ SỐ LIỆU TỔNG QUAN -->
    <div class="dashboard-grid">
        <!-- 1. Tổng doanh thu của Shop -->
        <div class="stat-box">
            <div class="stat-icon bg-green"><i class="fa-solid fa-money-bill-wave"></i></div>
            <div class="stat-info">
                <p class="stat-num"><asp:Label ID="lblRevenue" runat="server" Text="0 đ"></asp:Label></p>
                <p class="stat-label">Tổng Doanh Thu</p>
            </div>
        </div>

        <!-- 2. Tổng số đơn hàng đổ về Shop -->
        <div class="stat-box">
            <div class="stat-icon bg-blue"><i class="fa-solid fa-box-open"></i></div>
            <div class="stat-info">
                <p class="stat-num"><asp:Label ID="lblTotalOrders" runat="server" Text="0"></asp:Label></p>
                <p class="stat-label">Đơn Hàng Đã Nhận</p>
            </div>
        </div>

        <!-- 3. Tổng số lượng sản phẩm đang có của Shop -->
        <div class="stat-box">
            <div class="stat-icon bg-gray"><i class="fa-solid fa-cubes"></i></div>
            <div class="stat-info">
                <p class="stat-num"><asp:Label ID="lblTotalProducts" runat="server" Text="0"></asp:Label></p>
                <p class="stat-label">Sản Phẩm Đang Bán</p>
            </div>
        </div>
    </div>

    <!-- DANH SÁCH SẢN PHẨM BÁN CHẠY NHẤT CỦA SHOP -->
    <div class="section-title">
        <i class="fa-solid fa-ranking-star" style="color: #f59e0b;"></i> Top Sản Phẩm Bán Chạy Nhất
    </div>

    <asp:GridView ID="gvTopProducts" runat="server" AutoGenerateColumns="False" CssClass="grid-view" OnSelectedIndexChanged="gvTopProducts_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="ProductID" HeaderText="Mã SP" ItemStyle-Width="80px" />
            <asp:BoundField DataField="ProductName" HeaderText="Tên Sản Phẩm" />
            <asp:BoundField DataField="Price" HeaderText="Giá Bán" DataFormatString="{0:N0} đ" ItemStyle-Width="150px" />
            <asp:BoundField DataField="TotalQuantitySold" HeaderText="Số Lượng Đã Bán" ItemStyle-Width="150px" ItemStyle-Font-Bold="true" />
            <asp:BoundField DataField="TotalProductRevenue" HeaderText="Tổng Doanh Thu Đem Lại" DataFormatString="{0:N0} đ" ItemStyle-Width="220px" ItemStyle-ForeColor="#16a34a" />
        </Columns>
    </asp:GridView>
</asp:Content>