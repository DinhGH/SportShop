using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SportShop.App_Code;

namespace SportShop.App_Code
{
    public class TicketDAL
    {
        ConnectionProvider db = new ConnectionProvider();

        // Thêm mới một báo cáo sự cố từ người dùng gửi lên
        public int GuiBaoCao(int senderId, string title, string content)
        {
            // Thay thế dấu nháy đơn để tránh lỗi câu lệnh SQL khi người dùng nhập chuỗi
            string safeTitle = title.Replace("'", "''");
            string safeContent = content.Replace("'", "''");

            string query = string.Format(
                "INSERT INTO Tickets (SenderID, Title, Content, Status) VALUES ({0}, N'{1}', N'{2}', N'Chưa xử lý')",
                senderId, safeTitle, safeContent
            );

            return db.thucthiketnoi(query);
        }

        // Lấy danh sách lịch sử các báo cáo của riêng Khách hàng đó
        public DataTable LayLichSuTickets(int senderId)
        {
            string query = string.Format(
                "SELECT TicketID, Title, Content, Status, CreatedAt FROM Tickets WHERE SenderID = {0} ORDER BY CreatedAt DESC",
                senderId
            );
            return db.laydulieu(query);
        }
    }
}