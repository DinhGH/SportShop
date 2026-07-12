2. Quy Chuẩn Màu Sắc Giao Diện (UI/UX Color Palette)
Hệ thống áp dụng phong cách Minimalist Light Mode (Tối giản, nền sáng). Tất cả các trang con tạo mới bắt buộc phải tuân theo hệ mã màu này:
Thành phần
Mã màu HEX
Vai trò hiển thị
Nền chính (Background)
#F1F5F9 đến #F8FAFC
Nền lớn toàn trang, tạo cảm giác dễ chịu, sạch sẽ.
Nền Container/Form
#FFFFFF
Nền các hộp bo góc, Form Đăng nhập/Đăng ký, Navbar, Footer.
Chữ chủ đạo (Text Main)
#1E293B
Màu đen xám tối, dùng cho các tiêu đề chính, chữ nội dung chính.
Chữ phụ (Text Muted)
#64748B
Màu xám nhẹ, dùng cho mô tả phụ, ghi chú, icon hoặc chân trang.
Nút bấm / Thẻ chính
#334155
Màu xám tối (Trạng thái bình thường). Khi hover đổi sang #1E293B.
Điểm nhấn (Accent Link)
#0284C7
Màu xanh dương Sky. Dùng cho Logo, các link chuyển trang, các nút CTA quan trọng.
Thông báo Thành công
#16A34A
Màu xanh lá, dùng cho thông báo Đăng ký/Đặt hàng thành công.
Thông báo Thất bại/Lỗi
#E11D48
Màu đỏ/hồng sậm, dùng cho báo lỗi mật khẩu, lỗi đăng nhập hoặc nút Đăng xuất.

📌 Prompt mẫu cho Frontend/UI: "Hãy thiết kế giao diện trang [Tên Trang] bằng HTML/CSS tương thích với ASP.NET Web Forms. Sử dụng nền màu #F8FAFC, phông chữ Segoe UI, các bảng/form có nền trắng #FFFFFF bo góc 12px. Các nút hành động chính sử dụng màu xám tối #334155 với chữ trắng, các liên kết quan trọng dùng màu xanh dương #0284C7."
3. Quy Chuẩn Cấu Trúc Thư Mục (Folder Structure)
Cấm tạo file lộn xộn ở thư mục gốc. Project phải chia đúng 3 lớp (3-Layer đơn giản):
📁 App_Data: Nơi chứa file database vật lý SportShopDB.mdf.
📁 DAL: Nơi chứa toàn bộ class C# thuần kết nối cơ sở dữ liệu (ConnectionProvider.cs, ProductDAL.cs, UserDAL.cs,...).
📁 Assets: Chứa tài nguyên tĩnh (/css, /js, /images).
📁 GUI: Chứa các trang giao diện .aspx chia theo quyền hạn:
/Admin: Các trang cho admin.
/Owner: Các trang cho chủ shop.
/Customer: Các trang cho khách hàng (Tất cả trang trong này bắt buộc chọn kế thừa Site.Master).
4. Tư Duy Lấy Dữ Liệu DB (ADO.NET Thuần & DataTable)
Toàn bộ dự án không sử dụng class mô hình (Model/Entity ánh xạ) cho từng bảng để tiết kiệm thời gian cấu hình ban đầu.
Sử dụng một đối tượng chung duy nhất là DataTable (co giãn động trên RAM) để chứa dữ liệu thô đổ về từ SQL.
Nguyên tắc đóng mở cổng: Mọi tương tác qua database phải chạy qua lớp trung gian ConnectionProvider. Bắt buộc dùng cấu trúc mở cổng muộn nhất trước khi chạy lệnh (moketnoi()) và đóng cổng sớm nhất ngay sau khi thực thi (dongketnoi()) trong khối finally để tránh lỗi sập ngầm hệ thống (NullReferenceException).
📌 Prompt mẫu cho Backend (C#): "Hãy viết một hàm trong lớp [Name]DAL bằng ngôn ngữ C# (.NET Framework). Hàm này nhận vào tham số là [...] và thực thi câu lệnh SQL thuần. Hãy khởi tạo đối tượng ConnectionProvider db = new ConnectionProvider();, gọi hàm mở kết nối, dùng SqlDataAdapter để fill dữ liệu vào một DataTable rồi đóng kết nối an toàn ở khối finally trước khi return DataTable."
