<%@ Page Title="Trang Chủ - SportShop" Language="C#" MasterPageFile="~/GUI/Customer/Customer.Master" AutoEventWireup="true" CodeBehind="DanhSachSanPham.aspx.cs" Inherits="SportShop.GUI.Customer.DanhSachSanPham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-title { font-size: 24px; font-weight: bold; margin-bottom: 20px; color: #1e293b; }
        .product-grid { display: grid; grid-template-columns: repeat(auto-fill, minmax(220px, 1fr)); gap: 25px; margin-bottom: 40px; }
        .product-card { background-color: #ffffff; border: 1px solid #e2e8f0; border-radius: 12px; padding: 15px; box-shadow: 0 4px 6px rgba(0,0,0,0.02); transition: transform 0.2s, box-shadow 0.2s; display: flex; flex-direction: column; justify-content: space-between; }
        .product-card:hover { transform: translateY(-5px); box-shadow: 0 10px 15px rgba(0,0,0,0.05); }
        .product-img { width: 100%; height: 180px; object-fit: cover; border-radius: 8px; background-color: #f8fafc; }
        .product-name { font-size: 16px; font-weight: 600; color: #1e293b; margin: 12px 0 6px 0; height: 44px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 2; -webkit-box-orient: vertical; }
        .product-price { font-size: 16px; font-weight: bold; color: #0284c7; margin-bottom: 12px; }
        .btn-detail { display: block; text-align: center; padding: 8px; background-color: #334155; color: white; text-decoration: none; border-radius: 6px; font-size: 14px; font-weight: 500; transition: background 0.2s; }
        .btn-detail:hover { background-color: #1e293b; }

        .btn-detail{
            display:block;
            width:100%;
            box-sizing:border-box;
            text-align:center;
            padding:8px;
            background:#334155;
            color:white;
            text-decoration:none;
            border:none;
            border-radius:6px;
            cursor:pointer;
            margin-top:8px;
        }

        .btn-detail:hover{
            background:#1e293b;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title"><i class="fa-solid fa-fire" style="color: #ef4444;"></i> Sản Phẩm Nổi Bật Trên Hệ Thống</div>
    
    <!-- Repeater hiển thị danh sách sản phẩm cấu trúc ô Card -->
    <div class="product-grid">
        <asp:Repeater ID="rptProducts" runat="server">
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

                        <a href='ChiTietSanPham.aspx?id=<%# Eval("ProductID") %>'
                            class="btn-detail">
                            Xem chi tiết
                        </a>

                        <br />

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
</asp:Content>