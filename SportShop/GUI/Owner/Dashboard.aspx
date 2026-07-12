<%@ Page Title="Dashboard Cửa Hàng" Language="C#" MasterPageFile="~/GUI/Owner/Owner.Master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="SportShop.GUI.Owner.Dashboard" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
            /* Quy tắc màu sắc theo rule.md: Minimalist Light Mode */
            body {
                background-color: #F8FAFC;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                color: #1E293B;
            }

            .dashboard-container {
                padding: 20px;
                background-color: #F8FAFC;
                max-width: 1200px;
                margin: 0 auto;
            }

            h1 {
                color: #1E293B;
                font-weight: 600;
                margin-bottom: 30px;
            }

            /* Stat Cards */
            .stat-card {
                background: #FFFFFF;
                border-radius: 12px;
                padding: 24px;
                box-shadow: 0 1px 3px rgba(0, 0, 0, 0.08);
                transition: all 0.3s ease;
                border-left: 4px solid #0284C7;
                min-height: 140px;
                display: flex;
                flex-direction: column;
                justify-content: center;
            }

            .stat-card:hover {
                box-shadow: 0 4px 12px rgba(0, 0, 0, 0.12);
                transform: translateY(-2px);
            }

            .stat-icon {
                font-size: 32px;
                margin-bottom: 12px;
            }

            .stat-label {
                font-size: 13px;
                color: #64748B;
                font-weight: 500;
                margin-bottom: 8px;
                text-transform: uppercase;
                letter-spacing: 0.5px;
            }

            .stat-value {
                font-size: 28px;
                font-weight: 700;
                color: #1E293B;
                margin: 0;
            }

            /* Section Title */
            .section-title {
                font-size: 20px;
                font-weight: 600;
                margin: 40px 0 20px 0;
                color: #1E293B;
                padding-bottom: 12px;
                border-bottom: 2px solid #E2E8F0;
            }

            /* Top Products Container */
            .top-products {
                background: #FFFFFF;
                border-radius: 12px;
                padding: 24px;
                box-shadow: 0 1px 3px rgba(0, 0, 0, 0.08);
            }

            .product-item {
                display: flex;
                align-items: center;
                padding: 16px 12px;
                border-bottom: 1px solid #E2E8F0;
                transition: background-color 0.2s ease;
            }

            .product-item:hover {
                background-color: #F8FAFC;
            }

            .product-item:last-child {
                border-bottom: none;
            }

            .product-image {
                width: 64px;
                height: 64px;
                margin-right: 16px;
                border-radius: 8px;
                object-fit: cover;
                background-color: #F1F5F9;
                border: 1px solid #E2E8F0;
            }

            .product-info {
                flex: 1;
            }

            .product-name {
                font-weight: 600;
                font-size: 15px;
                color: #1E293B;
                margin-bottom: 6px;
            }

            .product-stats {
                font-size: 12px;
                color: #64748B;
                line-height: 1.5;
            }

            .product-stats strong {
                color: #1E293B;
            }

            .product-sold {
                background: #0284C7;
                color: white;
                padding: 6px 14px;
                border-radius: 20px;
                font-weight: 600;
                font-size: 13px;
                white-space: nowrap;
                margin-left: 12px;
            }

            /* Chart Container */
            .chart-container {
                background: #FFFFFF;
                border-radius: 12px;
                padding: 24px;
                box-shadow: 0 1px 3px rgba(0, 0, 0, 0.08);
                margin-top: 24px;
            }

            /* GridView Styling */
            .chart-container table {
                width: 100%;
                border-collapse: collapse;
                font-size: 14px;
                text-align: center;
            }

            .chart-container table thead {
                background-color: #F1F5F9;
                border-bottom: 2px solid #E2E8F0;
            }

            .chart-container table thead th {
                color: #1E293B;
                font-weight: 600;
                padding: 12px;
                text-align: center;
            }

            .chart-container table tbody tr {
                border-bottom: 1px solid #E2E8F0;
            }

            .chart-container table tbody tr:hover {
                background-color: #F8FAFC;
            }

            .chart-container table tbody td {
                padding: 12px;
                color: #334155;
                text-align: center;
            }

            /* Empty Data */
            .no-data {
                text-align: center;
                padding: 48px 24px;
                color: #94A3B8;
                font-size: 15px;
                background-color: #F8FAFC;
                border-radius: 8px;
            }

            /* Stats Grid */
            .stats-grid {
                display: grid;
                grid-template-columns: repeat(4, 1fr);
                gap: 24px;
                margin-bottom: 40px;
            }

            @media (max-width: 768px) {
                .stats-grid {
                    grid-template-columns: repeat(2, 1fr);
                    gap: 16px;
                }

                .product-item {
                    flex-direction: column;
                    align-items: flex-start;
                }

                .product-sold {
                    margin-left: 0;
                    margin-top: 8px;
                    align-self: flex-start;
                }
            }

            @media (max-width: 480px) {
                .stats-grid {
                    grid-template-columns: 1fr;
                }

                .stat-card {
                    min-height: 120px;
                }

                .stat-value {
                    font-size: 24px;
                }

                .stat-icon {
                    font-size: 24px;
                }

                .section-title {
                    font-size: 18px;
                }
            }
        </style>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="dashboard-container container">
            <h1 style="margin-bottom: 30px; color: #333;">📊 Dashboard Cửa Hàng</h1>

            <!-- Thống kê chính -->
            <div class="stats-grid">
                <div class="stat-card revenue">
                    <div class="stat-icon">💰</div>
                    <div class="stat-label">Doanh Thu Tháng Này</div>
                    <div class="stat-value">
                        <asp:Label ID="lblTotalRevenue" runat="server" Text="0 đ"></asp:Label>
                    </div>
                </div>

                <div class="stat-card orders">
                    <div class="stat-icon">📦</div>
                    <div class="stat-label">Tổng Đơn Hàng</div>
                    <div class="stat-value">
                        <asp:Label ID="lblTotalOrders" runat="server" Text="0"></asp:Label>
                    </div>
                </div>

                <div class="stat-card products">
                    <div class="stat-icon">🛍️</div>
                    <div class="stat-label">Tổng Sản Phẩm</div>
                    <div class="stat-value">
                        <asp:Label ID="lblTotalProducts" runat="server" Text="0"></asp:Label>
                    </div>
                </div>

                <div class="stat-card stock">
                    <div class="stat-icon">📈</div>
                    <div class="stat-label">Tồn Kho Toàn Bộ</div>
                    <div class="stat-value">
                        <asp:Label ID="lblTotalStock" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
            </div>

            <!-- Sản phẩm bán chạy -->
            <div class="section-title">🔥 Top 5 Sản Phẩm Bán Chạy</div>
            <div class="top-products">
                <asp:Repeater ID="rptTopProducts" runat="server">
                    <ItemTemplate>
                        <div class="product-item">
                            <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("ProductName") %>' class="product-image"
                                onerror="handleImageError(this);" />
                            <div class="product-info">
                                <div class="product-name">
                                    <%# Eval("ProductName") %>
                                </div>
                                <div class="product-stats">
                                    Giá: <strong>
                                        <%# string.Format("{0:N0}", Eval("Price")) %> đ
                                    </strong>
                                    | Đã bán: <strong>
                                        <%# Eval("TotalSold") %>
                                    </strong> sản phẩm
                                    | Số đơn hàng: <strong>
                                        <%# Eval("OrderCount") %>
                                    </strong>
                                </div>
                            </div>
                            <div class="product-sold">
                                <%# Eval("TotalSold") %> Đơn
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlEmptyProducts" runat="server" Visible="false">
                    <div class="no-data">
                        Chưa có dữ liệu sản phẩm bán chạy
                    </div>
                </asp:Panel>
            </div>

            <!-- Biểu đồ doanh thu theo tháng -->
            <div class="section-title">📊 Doanh Thu Theo Tháng</div>
            <div class="chart-container">
                <asp:GridView ID="gvMonthlyRevenue" runat="server" CssClass="table table-striped"
                    AutoGenerateColumns="false" GridLines="None" HeaderStyle-BackColor="#64748B"
                    HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" RowStyle-BackColor="White"
                    AlternatingRowStyle-BackColor="#F1F5F9" PagerStyle-HorizontalAlign="Center"
                    PagerStyle-ForeColor="White" PagerStyle-BackColor="#0284C7" PagerStyle-Font-Bold="true">
                    <Columns>
                        <asp:BoundField DataField="Month" HeaderText="Tháng" ItemStyle-Width="50" />
                        <asp:BoundField DataField="MonthName" HeaderText="Tên Tháng" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Revenue" HeaderText="Doanh Thu" DataFormatString="{0:N0} đ"
                            ItemStyle-Width="200" />
                        <asp:BoundField DataField="OrderCount" HeaderText="Số Đơn Hàng" ItemStyle-Width="150" />
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="no-data">Chưa có dữ liệu doanh thu</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </asp:Content>