<%@ Page Title="Quản Lý Sản Phẩm & Danh Mục" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProductManagement.aspx.cs" Inherits="SportShop.GUI.Owner.ProductManagement" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
            /* Quy tắc màu sắc theo rule.md: Minimalist Light Mode */
            body {
                background-color: #F8FAFC;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                color: #1E293B;
            }

            .product-management-container {
                padding: 40px 0;
                background-color: #F8FAFC;
            }

            .page-header {
                display: flex;
                justify-content: space-between;
                align-items: center;
                margin-bottom: 40px;
                flex-wrap: wrap;
                gap: 20px;
            }

            .page-title-group h1 {
                font-size: 28px;
                font-weight: 600;
                color: #1E293B;
                margin-bottom: 8px;
            }

            .page-subtitle {
                font-size: 14px;
                color: #64748B;
            }

            .btn-add-product {
                background-color: #0284C7;
                color: white;
                padding: 10px 20px;
                border: none;
                border-radius: 8px;
                font-size: 14px;
                font-weight: 600;
                cursor: pointer;
                transition: all 0.2s ease;
            }

            .btn-add-product:hover {
                background-color: #0369A1;
                box-shadow: 0 4px 12px rgba(2, 132, 199, 0.3);
            }

            /* Alert Messages */
            .alert {
                padding: 12px 16px;
                border-radius: 8px;
                margin-bottom: 24px;
                font-size: 14px;
                border-left: 4px solid;
            }

            .alert-success {
                background-color: #F0FDF4;
                border-left-color: #16A34A;
                color: #166534;
            }

            .alert-error {
                background-color: #FEF2F2;
                border-left-color: #E11D48;
                color: #BE123C;
            }

            /* Products Table */
            .products-table-container {
                background: #FFFFFF;
                border-radius: 12px;
                padding: 24px;
                box-shadow: 0 1px 3px rgba(0, 0, 0, 0.08);
                overflow-x: auto;
            }

            .products-table-container table {
                width: 100%;
                border-collapse: collapse;
                font-size: 14px;
            }

            .products-table-container table thead {
                background-color: #F1F5F9;
                border-bottom: 2px solid #E2E8F0;
            }

            .products-table-container table thead th {
                color: #1E293B;
                font-weight: 600;
                padding: 12px;
                text-align: left;
            }

            .products-table-container table tbody tr {
                border-bottom: 1px solid #E2E8F0;
                transition: background-color 0.2s ease;
            }

            .products-table-container table tbody tr:hover {
                background-color: #F8FAFC;
            }

            .products-table-container table tbody td {
                padding: 12px;
                color: #334155;
            }

            .product-image {
                width: 50px;
                height: 50px;
                border-radius: 4px;
                object-fit: cover;
                background-color: #F1F5F9;
            }

            .stock-status {
                display: inline-block;
                padding: 4px 8px;
                border-radius: 4px;
                font-size: 12px;
                font-weight: 600;
            }

            .stock-high {
                background-color: #DCFCE7;
                color: #166534;
            }

            .stock-medium {
                background-color: #FEF3C7;
                color: #92400E;
            }

            .stock-low {
                background-color: #FEE2E2;
                color: #991B1B;
            }

            .action-buttons {
                display: flex;
                gap: 8px;
            }

            .btn-action {
                padding: 6px 12px;
                border: none;
                border-radius: 6px;
                font-size: 12px;
                font-weight: 600;
                cursor: pointer;
                transition: all 0.2s ease;
                text-decoration: none;
                display: inline-block;
            }

            .btn-edit {
                background-color: #0284C7;
                color: white;
            }

            .btn-edit:hover {
                background-color: #0369A1;
            }

            .btn-delete {
                background-color: #E11D48;
                color: white;
            }

            .btn-delete:hover {
                background-color: #BE123C;
            }

            .btn-stock {
                background-color: #64748B;
                color: white;
            }

            .btn-stock:hover {
                background-color: #475569;
            }

            /* Modal/Form Container */
            .modal-overlay {
                display: none;
                position: fixed;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background-color: rgba(0, 0, 0, 0.5);
                z-index: 999;
                justify-content: center;
                align-items: center;
            }

            .modal-overlay.show {
                display: flex;
            }

            .modal-content {
                background: #FFFFFF;
                border-radius: 12px;
                padding: 32px;
                max-width: 600px;
                width: 90%;
                max-height: 90vh;
                overflow-y: auto;
                box-shadow: 0 20px 25px rgba(0, 0, 0, 0.15);
            }

            .modal-header {
                font-size: 20px;
                font-weight: 600;
                color: #1E293B;
                margin-bottom: 24px;
                display: flex;
                justify-content: space-between;
                align-items: center;
            }

            .modal-close {
                background: none;
                border: none;
                font-size: 24px;
                color: #64748B;
                cursor: pointer;
            }

            .form-group {
                margin-bottom: 20px;
            }

            .form-group:last-child {
                margin-bottom: 0;
            }

            label {
                display: block;
                font-size: 13px;
                font-weight: 600;
                color: #1E293B;
                margin-bottom: 8px;
            }

            .form-control {
                width: 100%;
                padding: 10px 12px;
                border: 1px solid #E2E8F0;
                border-radius: 8px;
                font-size: 14px;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                color: #1E293B;
                background-color: #FFFFFF;
                transition: all 0.2s ease;
                box-sizing: border-box;
            }

            .form-control:focus {
                outline: none;
                border-color: #0284C7;
                box-shadow: 0 0 0 3px rgba(2, 132, 199, 0.1);
            }

            select.form-control {
                appearance: none;
                background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='12' viewBox='0 0 12 12'%3E%3Cpath fill='%231E293B' d='M6 9L1 4h10z'/%3E%3C/svg%3E");
                background-repeat: no-repeat;
                background-position: right 12px center;
                padding-right: 36px;
            }

            .form-text {
                font-size: 12px;
                color: #64748B;
                margin-top: 4px;
            }

            .modal-buttons {
                display: flex;
                gap: 12px;
                margin-top: 32px;
                padding-top: 24px;
                border-top: 1px solid #E2E8F0;
            }

            .btn {
                padding: 10px 20px;
                border-radius: 8px;
                font-size: 14px;
                font-weight: 600;
                border: none;
                cursor: pointer;
                transition: all 0.2s ease;
                flex: 1;
            }

            .btn-primary {
                background-color: #0284C7;
                color: white;
            }

            .btn-primary:hover {
                background-color: #0369A1;
            }

            .btn-secondary {
                background-color: #E2E8F0;
                color: #1E293B;
            }

            .btn-secondary:hover {
                background-color: #CBD5E1;
            }

            .no-data {
                text-align: center;
                padding: 48px 24px;
                color: #94A3B8;
                font-size: 16px;
            }

            /* Stock Modal */
            .stock-input-group {
                display: flex;
                gap: 8px;
            }

            .stock-input-group input {
                flex: 1;
            }

            .stock-input-group button {
                padding: 10px 16px;
                background-color: #0284C7;
                color: white;
                border: none;
                border-radius: 8px;
                font-weight: 600;
                cursor: pointer;
            }

            @media (max-width: 768px) {
                .page-header {
                    flex-direction: column;
                    align-items: flex-start;
                }

                .products-table-container table {
                    font-size: 12px;
                }

                .products-table-container table td,
                .products-table-container table th {
                    padding: 8px;
                }

                .action-buttons {
                    flex-direction: column;
                }

                .btn-action {
                    width: 100%;
                    text-align: center;
                }

                .modal-content {
                    padding: 24px;
                }
            }
        </style>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="product-management-container container">
            <div class="page-header">
                <div class="page-title-group">
                    <h1>📦 Quản Lý Sản Phẩm</h1>
                    <p class="page-subtitle">Thêm, sửa, xóa sản phẩm và quản lý tồn kho</p>
                </div>
                <asp:Button ID="btnAddProduct" runat="server" Text="➕ Thêm Sản Phẩm Mới" CssClass="btn-add-product"
                    OnClick="btnAddProduct_Click" />
            </div>

            <!-- Alert Messages -->
            <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success">
                <asp:Label ID="lblSuccess" runat="server"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-error">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </asp:Panel>

            <!-- Products Table -->
            <div class="products-table-container">
                <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" GridLines="None"
                    CssClass="products-table">
                    <Columns>
                        <asp:BoundField DataField="ProductID" HeaderText="ID" ItemStyle-Width="40px" />
                        <asp:TemplateField HeaderText="Ảnh" ItemStyle-Width="60px">
                            <ItemTemplate>
                                <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("ProductName") %>'
                                    class="product-image" onerror="this.src='~/Assets/images/placeholder.png'" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProductName" HeaderText="Tên Sản Phẩm" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="Price" HeaderText="Giá" DataFormatString="{0:N0} đ"
                            ItemStyle-Width="80px" />
                        <asp:TemplateField HeaderText="Tồn Kho" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <span class="stock-status <%# GetStockClass((int)Eval(" StockQuantity")) %>">
                                    <%# Eval("StockQuantity") %> cái
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CategoryName" HeaderText="Danh Mục" ItemStyle-Width="100px" />
                        <asp:TemplateField HeaderText="Hành Động" ItemStyle-Width="200px">
                            <ItemTemplate>
                                <div class="action-buttons">
                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn-action btn-edit"
                                        Text="✏️ Sửa" CommandName="Edit" CommandArgument='<%# Eval("ProductID") %>'
                                        OnCommand="ProductCommand_Click" />
                                    <asp:LinkButton ID="btnStock" runat="server" CssClass="btn-action btn-stock"
                                        Text="📊 Tồn Kho" CommandName="Stock" CommandArgument='<%# Eval("ProductID") %>'
                                        OnCommand="ProductCommand_Click" />
                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn-action btn-delete"
                                        Text="🗑️ Xóa" CommandName="Delete" CommandArgument='<%# Eval("ProductID") %>'
                                        OnCommand="ProductCommand_Click"
                                        OnClientClick="return confirm('Bạn chắc chắn muốn xóa sản phẩm này?');" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="no-data">
                            📭 Chưa có sản phẩm nào. Hãy thêm sản phẩm mới để bắt đầu!
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>

        <!-- Modal Form Add/Edit Product -->
        <div id="productModal" class="modal-overlay">
            <div class="modal-content">
                <div class="modal-header">
                    <span id="modalTitle">Thêm Sản Phẩm Mới</span>
                    <button type="button" class="modal-close" onclick="closeProductModal()">&times;</button>
                </div>

                <form runat="server">
                    <asp:HiddenField ID="hfProductID" runat="server" Value="0" />

                    <div class="form-group">
                        <label for="txtProductName">Tên Sản Phẩm <span style="color: #E11D48;">*</span></label>
                        <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" MaxLength="100"
                            Placeholder="Nhập tên sản phẩm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvProductName" runat="server"
                            ControlToValidate="txtProductName" ErrorMessage="Tên sản phẩm không được để trống"
                            ForeColor="#E11D48" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="txtDescription">Mô Tả</label>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" MaxLength="500"
                            Placeholder="Nhập mô tả sản phẩm" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>

                    <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 16px;">
                        <div class="form-group">
                            <label for="txtPrice">Giá Bán <span style="color: #E11D48;">*</span></label>
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" Placeholder="0"
                                TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice"
                                ErrorMessage="Giá không được để trống" ForeColor="#E11D48" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label for="txtStockQuantity">Số Lượng <span style="color: #E11D48;">*</span></label>
                            <asp:TextBox ID="txtStockQuantity" runat="server" CssClass="form-control" Placeholder="0"
                                TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStockQuantity" runat="server"
                                ControlToValidate="txtStockQuantity" ErrorMessage="Số lượng không được để trống"
                                ForeColor="#E11D48" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="ddlCategory">Danh Mục <span style="color: #E11D48;">*</span></label>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">-- Chọn danh mục --</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                            InitialValue="" ErrorMessage="Danh mục không được để trống" ForeColor="#E11D48"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="txtImageURL">URL Ảnh</label>
                        <asp:TextBox ID="txtImageURL" runat="server" CssClass="form-control" MaxLength="500"
                            Placeholder="https://..."></asp:TextBox>
                        <div class="form-text">Nhập đường link ảnh hoặc để trống dùng ảnh mặc định</div>
                    </div>

                    <div class="modal-buttons">
                        <asp:Button ID="btnSave" runat="server" Text="💾 Lưu" CssClass="btn btn-primary"
                            OnClick="btnSave_Click" />
                        <button type="button" class="btn btn-secondary" onclick="closeProductModal()">❌ Hủy</button>
                    </div>
                </form>
            </div>
        </div>

        <!-- Modal Update Stock -->
        <div id="stockModal" class="modal-overlay">
            <div class="modal-content">
                <div class="modal-header">
                    <span>Cập Nhật Tồn Kho</span>
                    <button type="button" class="modal-close" onclick="closeStockModal()">&times;</button>
                </div>

                <form runat="server">
                    <asp:HiddenField ID="hfStockProductID" runat="server" Value="0" />

                    <div class="form-group">
                        <label>Tên Sản Phẩm</label>
                        <asp:Label ID="lblStockProductName" runat="server" CssClass="form-control"
                            style="background-color: #F1F5F9; border: 1px solid #E2E8F0; padding: 10px 12px; border-radius: 8px;">
                        </asp:Label>
                    </div>

                    <div class="form-group">
                        <label>Tồn Kho Hiện Tại</label>
                        <asp:Label ID="lblCurrentStock" runat="server" CssClass="form-control"
                            style="background-color: #F1F5F9; border: 1px solid #E2E8F0; padding: 10px 12px; border-radius: 8px;">
                        </asp:Label>
                    </div>

                    <div class="form-group">
                        <label for="txtNewStock">Số Lượng Mới <span style="color: #E11D48;">*</span></label>
                        <div class="stock-input-group">
                            <asp:TextBox ID="txtNewStock" runat="server" CssClass="form-control"
                                Placeholder="Nhập số lượng mới" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>

                    <div class="modal-buttons">
                        <asp:Button ID="btnUpdateStock" runat="server" Text="✅ Cập Nhật" CssClass="btn btn-primary"
                            OnClick="btnUpdateStock_Click" />
                        <button type="button" class="btn btn-secondary" onclick="closeStockModal()">❌ Hủy</button>
                    </div>
                </form>
            </div>
        </div>

        <script>
            function closeProductModal() {
                document.getElementById('productModal').classList.remove('show');
                clearProductForm();
            }

            function closeStockModal() {
                document.getElementById('stockModal').classList.remove('show');
            }

            function showProductModal(title) {
                document.getElementById('modalTitle').textContent = title;
                document.getElementById('productModal').classList.add('show');
            }

            function showStockModal() {
                document.getElementById('stockModal').classList.add('show');
            }

            function clearProductForm() {
                document.getElementById('<%=txtProductName.ClientID%>').value = '';
                document.getElementById('<%=txtDescription.ClientID%>').value = '';
                document.getElementById('<%=txtPrice.ClientID%>').value = '';
                document.getElementById('<%=txtStockQuantity.ClientID%>').value = '';
                document.getElementById('<%=txtImageURL.ClientID%>').value = '';
                document.getElementById('<%=ddlCategory.ClientID%>').value = '';
                document.getElementById('<%=hfProductID.ClientID%>').value = '0';
            }

            // Close modal khi click ngoài
            document.getElementById('productModal').addEventListener('click', function (e) {
                if (e.target === this) closeProductModal();
            });

            document.getElementById('stockModal').addEventListener('click', function (e) {
                if (e.target === this) closeStockModal();
            });
        </script>
    </asp:Content>