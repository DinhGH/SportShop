<%@ Page Title="Thanh toán" Language="C#" MasterPageFile="~/GUI/Customer/Customer.Master"
    AutoEventWireup="true" CodeBehind="Checkout.aspx.cs"
    Inherits="SportShop.GUI.Customer.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .checkout-title {
            font-size: 28px;
            font-weight: bold;
            color: #1e293b;
            margin-bottom: 25px;
        }

        .checkout-card {
            background: #fff;
            border-radius: 12px;
            padding: 30px;
            box-shadow: 0 3px 12px rgba(0,0,0,.08);
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-group label {
            display:block;
            font-weight:bold;
            margin-bottom:8px;
            color:#334155;
        }

        .txt {
            width:100%;
            padding:10px;
            border:1px solid #cbd5e1;
            border-radius:6px;
            font-size:15px;
            box-sizing:border-box;
        }

        .radio {
            margin-top:8px;
            margin-bottom:20px;
        }

        .total-box{
            background:#f8fafc;
            border:1px solid #e2e8f0;
            padding:18px;
            border-radius:8px;
            margin:25px 0;
            font-size:20px;
            font-weight:bold;
            color:#0284c7;
        }

        .btnCheckout{
            background:#0284c7;
            color:white;
            border:none;
            padding:12px 30px;
            border-radius:8px;
            font-size:16px;
            cursor:pointer;
            font-weight:bold;
        }

        .btnCheckout:hover{
            background:#0369a1;
        }

        .message{
            margin-top:20px;
            font-weight:bold;
            color:red;
        }

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="checkout-title">
        <i class="fa-solid fa-credit-card"></i>
        Thanh toán đơn hàng
    </div>

    <div class="checkout-card">

        <!-- Người nhận -->

        <div class="form-group">

            <label>Người nhận</label>

            <asp:TextBox
                ID="txtReceiver"
                runat="server"
                CssClass="txt">
            </asp:TextBox>

        </div>

        <!-- Số điện thoại -->

        <div class="form-group">

            <label>Số điện thoại</label>

            <asp:TextBox
                ID="txtPhone"
                runat="server"
                CssClass="txt">
            </asp:TextBox>

        </div>

        <!-- Địa chỉ -->

        <div class="form-group">

            <label>Địa chỉ giao hàng</label>

            <asp:TextBox
                ID="txtAddress"
                runat="server"
                CssClass="txt"
                TextMode="MultiLine"
                Rows="3">
            </asp:TextBox>

        </div>

        <!-- Phương thức -->

        <div class="form-group">

            <label>Phương thức thanh toán</label>

            <asp:RadioButtonList
                ID="rblPayment"
                runat="server"
                CssClass="radio">

                <asp:ListItem Selected="True" Value="COD">
                    Thanh toán khi nhận hàng (COD)
                </asp:ListItem>

                <asp:ListItem Value="Banking">
                    Chuyển khoản ngân hàng
                </asp:ListItem>

            </asp:RadioButtonList>

        </div>

        <h3>Sản phẩm sẽ thanh toán</h3>

        <asp:GridView
            ID="gvCheckout"
            runat="server"
            AutoGenerateColumns="False"
            Width="100%"
            GridLines="None">

            <Columns>

                <asp:BoundField
                    DataField="ProductName"
                    HeaderText="Sản phẩm" />

                <asp:BoundField
                    DataField="Price"
                    HeaderText="Đơn giá"
                    DataFormatString="{0:N0} đ" />

                <asp:BoundField
                    DataField="Quantity"
                    HeaderText="Số lượng" />

                <asp:TemplateField HeaderText="Thành tiền">
                    <ItemTemplate>
                        <%# (Convert.ToDecimal(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))).ToString("N0") %> đ
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>

        <br />

        <!-- Tổng tiền -->

        <div class="total-box">

            Tổng tiền:

            <asp:Label
            ID="lblTongTien"
            runat="server"
            Text="0 đ">
            </asp:Label>

        </div>

        <!-- Nút thanh toán -->

        <asp:Button
        ID="btnOrder"
        runat="server"
        Text="Xác nhận thanh toán"
        CssClass="btnCheckout"
        OnClick="btnOrder_Click"
        OnClientClick="return confirm('Bạn chắc chắn muốn đặt hàng?');" />

        <br />
        <br />

        <asp:Label
            ID="lblMessage"
            runat="server"
            CssClass="message">
        </asp:Label>

    </div>

</asp:Content>