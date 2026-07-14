<%@ Page Title="Lịch sử đơn hàng"
    Language="C#"
    MasterPageFile="~/GUI/Customer/Customer.Master"
    AutoEventWireup="true"
    CodeBehind="LichSuDonHang.aspx.cs"
    Inherits="SportShop.GUI.Customer.LichSuDonHang" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">

    <style>
        .title {
            font-size: 28px;
            font-weight: bold;
            margin-bottom: 25px;
            color: #1e293b;
        }

        .card {
            background: white;
            padding: 25px;
            border-radius: 12px;
            box-shadow: 0 2px 10px rgba(0,0,0,.05);
        }

        .grid {
            width: 100%;
        }

            .grid th {
                background: #0284c7;
                color: white;
                padding: 12px;
            }

            .grid td {
                padding: 12px;
            }
    </style>

</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="title">

        <i class="fa fa-clock"></i>

        Lịch sử đơn hàng

    </div>

    <div class="card">

        <asp:gridview
            id="gvOrders"
            runat="server"
            autogeneratecolumns="False"
            cssclass="grid"
            gridlines="None"
            onrowcommand="gvOrders_RowCommand">

<Columns>

<asp:BoundField
DataField="OrderID"
HeaderText="Mã đơn"/>

<asp:BoundField
DataField="CreatedAt"
HeaderText="Ngày mua"
DataFormatString="{0:dd/MM/yyyy HH:mm}"/>

<asp:BoundField
DataField="TotalPrice"
HeaderText="Tổng tiền"
DataFormatString="{0:N0} đ"/>

<asp:BoundField
DataField="PaymentMethod"
HeaderText="Thanh toán"/>

<asp:ButtonField
Text="Chi tiết"
CommandName="Detail"
ButtonType="Button"/>

</Columns>

</asp:gridview>

        <br />

        <h3>Chi tiết đơn hàng</h3>

        <asp:gridview
            id="gvDetails"
            runat="server"
            autogeneratecolumns="true"
            cssclass="grid"
            gridlines="None">

</asp:gridview>

    </div>

</asp:Content>
