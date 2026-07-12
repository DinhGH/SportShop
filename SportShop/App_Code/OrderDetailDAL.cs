using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.App_Code
{
    public class OrderDetailDAL
    {
        ConnectionProvider db = new ConnectionProvider();

        // Chủ cửa hàng (Owner) lọc ra các đơn hàng được đặt tại Shop của mình để chuẩn bị hàng
        public DataTable LayDonHangTheoCuaHang(int storeId)
        {
            string query = string.Format("SELECT od.*, p.ProductName, o.CreatedAt FROM OrderDetails od INNER JOIN Products p ON od.ProductID = p.ProductID INNER JOIN Orders o ON od.OrderID = o.OrderID WHERE od.StoreID = {0}", storeId);
            return db.laydulieu(query);
        }

        // Cập nhật trạng thái đơn hàng (Ví dụ: Duyệt đơn, Đang giao, Đã giao)
        public int CapNhatTrangThai(int orderDetailId, string status)
        {
            string query = string.Format("UPDATE OrderDetails SET Status = N'{0}' WHERE OrderDetailID = {1}", status, orderDetailId);
            return db.thucthiketnoi(query);
        }
    }
}