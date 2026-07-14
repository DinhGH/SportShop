<%@ Page Title="Giỏ hàng" Language="C#" MasterPageFile="~/GUI/Customer/Customer.Master"
    AutoEventWireup="true" CodeBehind="XemGioHang.aspx.cs"
    Inherits="SportShop.GUI.Customer.GioHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style>

    /* Tiêu đề */
    .page-title{
        font-size:28px;
        font-weight:700;
        color:#0284c7;
        margin-bottom:25px;
    }

    /* Khung chứa */
    .cart-container{
        background:#ffffff;
        border:1px solid #e2e8f0;
        border-radius:12px;
        padding:25px;
        box-shadow:0 2px 8px rgba(0,0,0,0.05);
    }

    /* GridView */
    .cart-grid{
        width:100%;
        border-collapse:collapse;
        margin-bottom:25px;
    }

    .cart-grid th{
        background:#0284c7;
        color:white;
        padding:14px;
        text-align:center;
        font-size:15px;
    }

    .cart-grid td{
        border-bottom:1px solid #e2e8f0;
        padding:12px;
        text-align:center;
        vertical-align:middle;
        background:white;
    }

    .cart-grid tr:hover{
        background:#f8fafc;
    }

    /* Hình sản phẩm */
    .product-img{
        width:90px;
        height:90px;
        object-fit:cover;
        border-radius:8px;
        border:1px solid #e2e8f0;
    }

    /* Tổng tiền */
    .cart-total{
        text-align:right;
        font-size:22px;
        font-weight:bold;
        color:#dc2626;
        margin-top:20px;
    }

    /* Nút */
    .btn{
        padding:10px 22px;
        border:none;
        border-radius:8px;
        cursor:pointer;
        font-size:15px;
        font-weight:600;
        transition:.2s;
    }

    .btn-delete{
        background:#ef4444;
        color:white;
    }

    .btn-delete:hover{
        background:#dc2626;
    }

    .btn-checkout{
        background:#0284c7;
        color:white;
    }

    .btn-checkout:hover{
        background:#0369a1;
    }

    .btn-continue{
        background:#64748b;
        color:white;
    }

    .btn-continue:hover{
        background:#475569;
    }

    /* Thanh nút cuối */
    .cart-action{
        display:flex;
        justify-content:space-between;
        align-items:center;
        margin-top:25px;
    }

</style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="page-title">
        <i class="fa-solid fa-cart-shopping"></i>
        Giỏ hàng của bạn
    </h2>

    <div class="cart-container">

    <asp:GridView ID="gvCart"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="cart-grid"
        DataKeyNames="CartID"
        OnRowCommand="gvCart_RowCommand">

        <Columns>

            <%-- Mã giỏ --%>
            <asp:BoundField DataField="CartID" Visible="false" />

            <%-- Hình sản phẩm --%>
            <asp:TemplateField HeaderText="Hình ảnh">
                <ItemTemplate>
                    <img src='<%# Eval("ImageURL") %>'
                        class="product-img" />
                </ItemTemplate>
            </asp:TemplateField>

            <%-- Tên sản phẩm --%>
            <asp:BoundField
                DataField="ProductName"
                HeaderText="Tên sản phẩm" />

            <%-- Giá --%>
            <asp:BoundField
                DataField="Price"
                HeaderText="Đơn giá"
                DataFormatString="{0:N0} đ" />

            <%-- Số lượng --%>
            <asp:TemplateField HeaderText="Số lượng">

            <ItemTemplate>

                <asp:Button ID="btnMinus"
                    runat="server"
                    Text="-"
                    Width="30"
                    CommandName="Minus"
                    CommandArgument='<%# Eval("CartID") %>' />

                <asp:Label ID="lblQuantity"
                    runat="server"
                    Text='<%# Eval("Quantity") %>'
                    style="padding:0 10px;font-weight:bold;" />

                <asp:Button ID="btnPlus"
                    runat="server"
                    Text="+"
                    Width="30"
                    CommandName="Plus"
                    CommandArgument='<%# Eval("CartID") %>' />

            </ItemTemplate>

        </asp:TemplateField>

            <%-- Thành tiền --%>
            <asp:TemplateField HeaderText="Thành tiền">
                <ItemTemplate>
                    <%#
                        String.Format("{0:N0} đ",
                        Convert.ToDecimal(Eval("Price")) *
                        Convert.ToInt32(Eval("Quantity")))
                    %>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- Xóa --%>
            <asp:TemplateField HeaderText="Thao tác">
                <ItemTemplate>

                    <asp:Button ID="btnDelete"
                        runat="server"
                        Text="Xóa"
                        CssClass="btn btn-delete"
                        CommandName="DeleteItem"
                        CommandArgument='<%# Eval("CartID") %>'
                        OnClientClick="return confirm('Bạn muốn xóa sản phẩm này?');" />

                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <%-- Tổng tiền --%>
    <div class="cart-total">

        Tổng tiền:

        <asp:Label ID="lblTongTien"
            runat="server"
            Text="0 đ">
        </asp:Label>

    </div>

    <%-- Nút --%>
    <div class="cart-action">

        <asp:Button ID="btnContinue"
            runat="server"
            Text="← Tiếp tục mua"
            CssClass="btn btn-continue"
            PostBackUrl="~/GUI/Customer/DanhSachSanPham.aspx" />

        <asp:Button ID="btnCheckout"
            runat="server"
            Text="Thanh toán"
            CssClass="btn btn-checkout"
            OnClick="btnCheckout_Click" />

    </div>

</div>

</asp:Content>

