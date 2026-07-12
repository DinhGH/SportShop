using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.App_Code
{
    public class TicketDAL
    {
        ConnectionProvider db = new ConnectionProvider();

        // Admin xem toàn bộ khiếu nại/báo cáo sự cố từ người dùng gửi lên
        public DataTable LayToanBoTicket()
        {
            return db.laydulieu("SELECT t.*, u.FullName FROM Tickets t INNER JOIN Users u ON t.SenderID = u.UserID ORDER BY t.CreatedAt DESC");
        }

        // Người dùng (Owner/Customer) gửi báo cáo lỗi
        public int GuiTicket(int senderId, string title, string content)
        {
            string query = string.Format("INSERT INTO Tickets (SenderID, Title, Content) VALUES ({0}, N'{1}', N'{2}')", senderId, title, content);
            return db.thucthiketnoi(query);
        }
    }
}