<%@ Page Title="Báo cáo sự cố" Language="C#" MasterPageFile="~/GUI/Owner/Owner.Master" AutoEventWireup="true"
    CodeBehind="ReportIssue.aspx.cs" Inherits="SportShop.GUI.Owner.ReportIssue" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <style>
            .report-wrapper {
                display: flex;
                justify-content: center;
                padding: 20px;
            }

            .report-container {
                width: 100%;
                max-width: 600px;
                padding: 24px;
                background: #fff;
                border-radius: 12px;
                box-shadow: 0 1px 3px rgba(0, 0, 0, 0.08);
            }

            .report-title {
                font-size: 24px;
                font-weight: 600;
                color: #1e293b;
                margin-bottom: 20px;
            }

            .form-group {
                margin-bottom: 16px;
            }

            .form-label {
                display: block;
                font-weight: 600;
                margin-bottom: 8px;
                color: #334155;
            }

            .form-control {
                width: 100%;
                padding: 10px;
                border: 1px solid #e2e8f0;
                border-radius: 8px;
                box-sizing: border-box;
            }

            .btn-submit {
                background: #0284c7;
                color: white;
                border: none;
                padding: 12px 24px;
                cursor: pointer;
                border-radius: 8px;
                font-weight: 600;
                width: 100%;
            }

            .btn-submit:hover {
                background: #0369a1;
            }
        </style>
        <div class="report-wrapper">
            <div class="report-container">
                <h2 class="report-title">Gửi khiếu nại / Báo cáo lỗi</h2>
                <div class="form-group">
                    <label class="form-label">Tiêu đề:</label>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="form-label">Nội dung chi tiết:</label>
                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control">
                    </asp:TextBox>
                </div>
                <asp:Button ID="btnSubmit" runat="server" Text="Gửi báo cáo" OnClick="btnSubmit_Click"
                    CssClass="btn-submit" />
                <asp:Label ID="lblMessage" runat="server" style="display:block; margin-top:15px; font-weight:500;">
                </asp:Label>
            </div>
        </div>
    </asp:Content>