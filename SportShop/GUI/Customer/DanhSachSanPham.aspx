<%@ Page Title="Trang Chủ - SportShop" Language="C#" MasterPageFile="~/GUI/Customer/Customer.Master" AutoEventWireup="true" CodeBehind="DanhSachSanPham.aspx.cs" Inherits="SportShop.GUI.Customer.DanhSachSanPham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-title { font-size: 24px; font-weight: bold; margin-bottom: 20px; color: #1e293b; }
        
        /* CSS cho Khu vực Tìm kiếm & Bộ lọc */
        .filter-section { 
            background-color: #f1f5f9; 
            border: 1px solid #cbd5e1; 
            border-radius: 12px; 
            padding: 20px; 
            margin-bottom: 30px; 
            display: flex; 
            flex-wrap: wrap; 
            gap: 15px; 
            align-items: flex-end; 
        }
        .filter-group { display: flex; flex-direction: column; gap: 6px; }
        .filter-group label { font-size: 13px; font-weight: 600; color: #475569; }
        .filter-control { 
            padding: 8px 12px; 
            border: 1px solid #cbd5e1; 
            border-radius: 6px; 
            font-size: 14px; 
            outline: none; 
            background-color: white;
            min-width: 160px;
        }
        .filter-control:focus { border-color: #0284c7; box-shadow: 0 0 0 2px rgba(2, 132, 199, 0.2); }
        .btn-search { 
            padding: 9px 20px; 
            background-color: #0284c7; 
            color: white; 
            border: none; 
            border-radius: 6px; 
            font-weight: bold; 
            cursor: pointer; 
            transition: background 0.2s; 
        }
        .btn-search:hover { background-color: #0369a1; }
        .btn-reset { 
            padding: 9px 20px; 
            background-color: #64748b; 
            color: white; 
            border: none; 
            border-radius: 6px; 
            font-weight: bold; 
            cursor: pointer; 
            transition: background 0.2s; 
        }
        .btn-reset:hover { background-color: #475569; }

        /* CSS Grid cho Danh sách Sản phẩm */
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
        
        /* CSS cho Phân trang */
        .pagination-container { display: flex; justify-content: center; align-items: center; gap: 8px; margin-top: 30px; margin-bottom: 50px; }
        .btn-page { 
            padding: 8px 14px; 
            border: 1px solid #cbd5e1; 
            background-color: white; 
            color: #334155; 
            border-radius: 6px; 
            cursor: pointer; 
            font-weight: 500; 
            transition: all 0.2s; 
        }
        .btn-page:hover { background-color: #e2e8f0; border-color: #94a3b8; }
        .btn-page.active { background-color: #0284c7; color: white; border-color: #0284c7; }
        .btn-page:disabled { background-color: #f1f5f9; color: #94a3b8; border-color: #e2e8f0; cursor: not-allowed; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title">
        <i class="fa-solid fa-store" style="color: #0284c7;"></i> Hệ Thống Cửa Hàng Thể Thao
    </div>

    <!-- Khu vực Tìm kiếm & Bộ lọc -->
    <div class="filter-section">
        <div class="filter-group">
            <label>Từ khóa tìm kiếm</label>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="filter-control" Placeholder="Tìm sản phẩm..."></asp:TextBox>
        </div>
        <div class="filter-group">
            <label>Danh mục</label>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="filter-control"></asp:DropDownList>
        </div>
        <div class="filter-group">
            <label>Cửa hàng</label>
            <asp:DropDownList ID="ddlStore" runat="server" CssClass="filter-control"></asp:DropDownList>
        </div>
        <div class="filter-group">
            <label>Giá tối thiểu</label>
            <asp:TextBox ID="txtMinPrice" runat="server" CssClass="filter-control" Placeholder="đ" TextMode="Number"></asp:TextBox>
        </div>
        <div class="filter-group">
            <label>Giá tối đa</label>
            <asp:TextBox ID="txtMaxPrice" runat="server" CssClass="filter-control" Placeholder="đ" TextMode="Number"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnFilter" runat="server" Text="🔍 Lọc" CssClass="btn-search" OnClick="btnFilter_Click" />
            <asp:Button ID="btnReset" runat="server" Text="🔄 Thiết lập lại" CssClass="btn-reset" OnClick="btnReset_Click" />
        </div>
    </div>

    <!-- Danh sách sản phẩm -->
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
                        <a href='ChiTietSanPham.aspx?id=<%# Eval("ProductID") %>' class="btn-detail">
                            Xem chi tiết
                        </a>
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

    <!-- Hệ thống điều hướng phân trang -->
    <div class="pagination-container">
        <asp:Button ID="btnPrev" runat="server" Text="◀ Trước" CssClass="btn-page" OnClick="btnPrev_Click" />
        <asp:Label ID="lblPageInfo" runat="server" Style="font-weight: 600; color: #475569; margin: 0 10px;"></asp:Label>
        <asp:Button ID="btnNext" runat="server" Text="Sau ▶" CssClass="btn-page" OnClick="btnNext_Click" />
    </div>
</asp:Content>