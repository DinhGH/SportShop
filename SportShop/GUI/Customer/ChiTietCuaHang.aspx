<%@ Page Title="Chi Tiết Cửa Hàng" Language="C#" MasterPageFile="~/GUI/Customer/Customer.Master" AutoEventWireup="true" CodeBehind="ChiTietCuaHang.aspx.cs" Inherits="SportShop.GUI.Customer.ChiTietCuaHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* CSS cho phần Header giới thiệu Cửa hàng */
        .store-header { background: linear-gradient(135deg, #0284c7, #0369a1); color: white; padding: 30px; border-radius: 16px; margin-bottom: 30px; box-shadow: 0 4px 15px rgba(2, 132, 199, 0.15); }
        .store-title { font-size: 28px; font-weight: bold; margin-bottom: 10px; display: flex; align-items: center; gap: 10px; }
        .store-meta { font-size: 15px; display: flex; flex-direction: column; gap: 8px; opacity: 0.9; }
        .store-meta i { width: 20px; }

        /* Grid hiển thị sản phẩm của riêng shop */
        .product-grid { display: grid; grid-template-columns: repeat(auto-fill, minmax(220px, 1fr)); gap: 25px; margin-bottom: 40px; }
        .product-card { background-color: #ffffff; border: 1px solid #e2e8f0; border-radius: 12px; padding: 15px; box-shadow: 0 4px 6px rgba(0,0,0,0.02); transition: transform 0.2s, box-shadow 0.2s; display: flex; flex-direction: column; justify-content: space-between; }
        .product-card:hover { transform: translateY(-5px); box-shadow: 0 10px 15px rgba(0,0,0,0.05); }
        .product-img { width: 100%; height: 180px; object-fit: cover; border-radius: 8px; background-color: #f8fafc; }
        .product-name { font-size: 16px; font-weight: 600; color: #1e293b; margin: 12px 0 6px 0; height: 44px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 2; -webkit-box-orient: vertical; }
        .product-price { font-size: 16px; font-weight: bold; color: #0284c7; margin-bottom: 12px; }
        
        .btn-detail { 
            display: block; 
            width: 100%; 
            box-sizing: border-box; 
            text-align: center; 
            padding: 8px; 
            background: #334155; 
            color: white; 
            text-decoration: none; 
            border: none; 
            border-radius: 6px; 
            cursor: pointer; 
            margin-top: 8px; 
            font-size: 14px; 
            font-weight: 500; 
        }
        .btn-detail:hover { background: #1e293b; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Style="font-size: 18px; font-weight: bold; display: block;" Visible="false"></asp:Label>

    <asp:PlaceHolder ID="phStoreContainer" runat="server">
        <!-- Thông tin Cửa hàng -->
        <div class="store-header">
            <div class="store-title">
                <i class="fa-solid fa-shop"></i> <asp:Literal ID="litStoreName" runat="server"></asp:Literal>
            </div>
            <div class="store-meta">
                <div><i class="fa-solid fa-location-dot"></i> Địa chỉ: <asp:Literal ID="litStoreAddress" runat="server"></asp:Literal></div>
                <div><i class="fa-solid fa-phone"></i> Hotline hỗ trợ: <asp:Literal ID="litStorePhone" runat="server"></asp:Literal></div>
                <div><i class="fa-solid fa-calendar-check"></i> Ngày tham gia hệ thống: <asp:Literal ID="litStoreDate" runat="server"></asp:Literal></div>
            </div>
        </div>

        <h2 style="font-size: 20px; font-weight: bold; color: #1e293b; margin-bottom: 20px;">
            <i class="fa-solid fa-tags" style="color: #0284c7;"></i> Sản phẩm của cửa hàng
        </h2>

        <!-- Danh sách sản phẩm thuộc về Store này -->
        <div class="product-grid">
            <asp:Repeater ID="rptStoreProducts" runat="server">
                <ItemTemplate>
                    <div class="product-card">
                        <div>
                            <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("ProductName") %>' class="product-img" />
                            <div class="product-name"><%# Eval("ProductName") %></div>
                        </div>
                        <div>
                            <div class="product-price">
                                <%# string.Format("{0:N0} đ", Eval("Price")) %>
                            </div>
                            
                            <!-- Vẫn có thể xem chi tiết sản phẩm cụ thể -->
                            <a href='ChiTietSanPham.aspx?id=<%# Eval("ProductID") %>' class="btn-detail">
                                Xem chi tiết
                            </a>
                            
                            <!-- Vẫn có thể thêm nhanh vào giỏ hàng -->
                            <asp:Button ID="btnAddCart"
                                runat="server"
                                Text="🛒 Thêm vào giỏ"
                                CssClass="btn-detail"
                                CommandArgument='<%# Eval("ProductID") %>'
                                OnClick="btnAddCart_Click" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </asp:PlaceHolder>
</asp:Content>