using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.App_Code
{
    public class UserDAL
    {
        ConnectionProvider db = new ConnectionProvider();

        // Phục vụ tính năng Đăng nhập
        public DataTable DangNhap(string email, string password)
        {
            return db.laydulieu(string.Format("SELECT * FROM Users WHERE Email = '{0}' AND Password = '{1}' AND IsActive = 1", email, password));
        }

        // Phục vụ tính năng Đăng ký tài khoản khách hàng (RoleID của Customer thường cố định, ví dụ: 3)
        public int DangKyKhachHang(string fullName, string email, string password, string phone, string address, int roleId)
        {
            string query = string.Format("INSERT INTO Users (FullName, Email, Password, Phone, Address, RoleID) VALUES (N'{0}', '{1}', '{2}', '{3}', N'{4}', {5})", fullName, email, password, phone, address, roleId);
            return db.thucthiketnoi(query);
        }
    }
}