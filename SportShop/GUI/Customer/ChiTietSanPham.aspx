<%@ Page Title="Chi Tiết Sản Phẩm" Language="C#" MasterPageFile="~/GUI/Customer/Customer.Master" AutoEventWireup="true" CodeBehind="ChiTietSanPham.aspx.cs" Inherits="SportShop.GUI.Customer.ChiTietSanPham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .detail-container { display: grid; grid-template-columns: 1fr 1.2fr; gap: 40px; background: #ffffff; border: 1px solid #e2e8f0; border-radius: 16px; padding: 30px; margin-top: 20px; box-shadow: 0 4px 10px rgba(0,0,0,0.03); }
        .detail-img { width: 100%; max-height: 450px; object-fit: cover; border-radius: 12px; border: 1px solid #cbd5e1; }
        .detail-info { display: flex; flex-direction: column; justify-content: flex-start; }
        .p-name { font-size: 28px; font-weight: bold; color: #1e293b; margin-bottom: 10px; line-height: 1.3; }
        .p-price { font-size: 24px; font-weight: 800; color: #0284c7; margin-bottom: 20px; }
        
        /* Box thông tin cửa hàng */
        .store-badge { background-color: #f0fdf4; border: 1px solid #bbf7d0; border-radius: 8px; padding: 12px 15px; margin-bottom: 20px; display: inline-flex; align-items: center; gap: 8px; }
        .store-link { font-weight: 700; color: #16a34a; text-decoration: none; font-size: 15px; }
        .store-link:hover { text-decoration: underline; color: #15803d; }
        
        .p-meta { font-size: 14px; color: #64748b; margin-bottom: 15px; border-bottom: 1px dashed #e2e8f0; padding-bottom: 15px; }
        .p-meta strong { color: #334155; }
        .p-desc { font-size: 15px; color: #475569; line-height: 1.6; margin-bottom: 25px; }
        
        /* Khu vực nút chức năng */
        .action-area { display: flex; align-items: center; gap: 15px; margin-top: auto; }
        .txt-qty { width: 60px; padding: 10px; text-align: center; border: 1px solid #cbd5e1; border-radius: 6px; font-size: 16px; font-weight: bold; }
        .btn-add-cart-detail { padding: 12px 30px; background-color: #0284c7; color: white; border: none; border-radius: 6px; font-size: 16px; font-weight: bold; cursor: pointer; transition: background 0.2s; display: flex; align-items: center; gap: 8px; }
        .btn-add-cart-detail:hover { background-color: #0369a1; }
        .btn-back { padding: 12px 20px; background-color: #64748b; color: white; text-decoration: none; border-radius: 6px; font-size: 16px; font-weight: 500; text-align: center; }
        .btn-back:hover { background-color: #475569; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Style="font-size: 18px; font-weight: bold; display: block; margin-top: 20px;" Visible="false"></asp:Label>
    
    <asp:PlaceHolder ID="phProductDetail" runat="server">
        <div class="detail-container">
            <!-- Ảnh sản phẩm bên trái -->
            <div>
                <asp:Image ID="imgProduct" runat="server" CssClass="detail-img" />
            </div>

            <!-- Thông tin chi tiết bên phải -->
            <div class="detail-info">
                <h1 class="p-name"><asp:Literal ID="litProductName" runat="server"></asp:Literal></h1>
                
                <div class="p-price">
                    <asp:Literal ID="litPrice" runat="server"></asp:Literal>
                </div>

                <!-- HIỂN THỊ TÊN CỬA HÀNG (Yêu cầu đề bài) -->
                <div class="store-badge">
                    <i class="fa-solid fa-store" style="color: #16a34a;"></i>
                    <span>Cửa hàng: </span>
                    <asp:HyperLink ID="lnkStore" runat="server" CssClass="store-link"></asp:HyperLink>
                </div>

                <div class="p-meta">
                    Danh mục: <strong><asp:Literal ID="litCategory" runat="server"></asp:Literal></strong> &nbsp;|&nbsp; 
                    Tình trạng: <strong><asp:Literal ID="litStock" runat="server"></asp:Literal></strong>
                </div>

                <div class="p-desc">
                    <asp:Literal ID="litDescription" runat="server"></asp:Literal>
                </div>

                <!-- Thao tác mua hàng -->
                <div class="action-area">
                    <asp:TextBox ID="txtQuantity" runat="server" Text="1" TextMode="Number" CssClass="txt-qty" min="1"></asp:TextBox>
                    <asp:Button ID="btnAddToCart" runat="server" Text="🛒 Thêm vào giỏ hàng" CssClass="btn-add-cart-detail" OnClick="btnAddToCart_Click" />
                    <a href="DanhSachSanPham.aspx" class="btn-back">Quay lại</a>
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>