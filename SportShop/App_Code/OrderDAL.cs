using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.App_Code
{
    public class OrderDAL
    {
        ConnectionProvider db = new ConnectionProvider();

        // Khách hàng xem lại lịch sử mua hàng của họ
        public DataTable LayLichSuMuaHang(int customerId)
        {
            return db.laydulieu($"SELECT * FROM Orders WHERE CustomerID = {customerId} ORDER BY CreatedAt DESC");
        }

        // Tạo đơn hàng tổng khi bấm thanh toán
        public int TaoDonHangTong(int customerId, decimal totalPrice, string address, string name, string phone, string method)
        {
            // Hàm này sẽ insert một dòng mới vào bảng Orders
            string query = $"INSERT INTO Orders (CustomerID, TotalPrice, ShippingAddress, ReceiverName, ReceiverPhone, PaymentMethod) " +
                           $"VALUES ({customerId}, {totalPrice}, N'{address}', N'{name}', '{phone}', N'{method}')";
            return db.thucthiketnoi(query);
        }
    }
}