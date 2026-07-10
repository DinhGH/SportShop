<%@ Page Title="Báo cáo sự cố" Language="C#" MasterPageFile="~/GUI/Owner/Owner.Master" AutoEventWireup="true"
    CodeBehind="ReportIssue.aspx.cs" Inherits="SportShop.GUI.Owner.ReportIssue" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div
            style="max-width: 600px; margin: 0 auto; padding: 20px; background: #fff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);">
            <h2>Gửi khiếu nại / Báo cáo lỗi</h2>
            <div style="margin-bottom: 15px;">
                <label>Tiêu đề:</label><br />
                <asp:TextBox ID="txtTitle" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
            </div>
            <div style="margin-bottom: 15px;">
                <label>Nội dung chi tiết:</label><br />
                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="5" Width="100%"
                    CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="Gửi báo cáo" OnClick="btnSubmit_Click" CssClass="btn-submit"
                style="background:#0284c7; color:white; border:none; padding:10px 20px; cursor:pointer; border-radius:4px;" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green" style="display:block; margin-top:10px;">
            </asp:Label>
        </div>
    </asp:Content>