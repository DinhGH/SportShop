<%@ Page Title="Quản lý Đơn hàng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="OrderManagement.aspx.cs" Inherits="SportShop.GUI.Owner.OrderManagement" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
            .order-container {
                padding: 20px;
                background: #F8FAFC;
            }

            .section-title {
                font-size: 20px;
                font-weight: 600;
                margin-bottom: 20px;
                color: #1E293B;
            }

            .grid-container {
                background: #FFFFFF;
                padding: 20px;
                border-radius: 12px;
                box-shadow: 0 1px 3px rgba(0, 0, 0, 0.08);
            }

            table {
                width: 100%;
                border-collapse: collapse;
            }

            th {
                background: #F1F5F9;
                padding: 12px;
                text-align: left;
            }

            td {
                padding: 12px;
                border-bottom: 1px solid #E2E8F0;
            }
        </style>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="order-container">
            <h1 class="section-title">📦 Quản lý Đơn hàng</h1>
            <div class="grid-container">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Font-Bold="true" />
                <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" DataKeyNames="OrderDetailID"
                    OnRowCommand="gvOrders_RowCommand" OnRowDataBound="gvOrders_RowDataBound">

                    <Columns>

                        <asp:BoundField DataField="OrderID" HeaderText="ID" />

                        <asp:BoundField DataField="ReceiverName" HeaderText="Khách hàng" />

                        <asp:BoundField DataField="ProductName" HeaderText="Sản phẩm" />

                        <asp:BoundField DataField="Quantity" HeaderText="SL" />

                        <asp:BoundField DataField="UnitPrice" HeaderText="Đơn giá" DataFormatString="{0:N0} đ" />

                        <asp:BoundField DataField="Status" HeaderText="Trạng thái" />

                        <asp:TemplateField HeaderText="Thao tác">
                            <ItemTemplate>

                                <asp:DropDownList ID="ddlStatus" runat="server">

                                    <asp:ListItem Value="Chờ duyệt">Chờ duyệt</asp:ListItem>
                                    <asp:ListItem Value="Đang giao">Đang giao</asp:ListItem>
                                    <asp:ListItem Value="Đã giao">Đã giao</asp:ListItem>
                                    <asp:ListItem Value="Hủy">Hủy</asp:ListItem>

                                </asp:DropDownList>

                                <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" CommandName="UpdateStatus"
                                    CommandArgument='<%# Container.DataItemIndex %>' />

                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>
            </div>
        </div>
    </asp:Content>