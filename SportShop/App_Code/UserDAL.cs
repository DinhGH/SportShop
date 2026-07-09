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
            return db.laydulieu($"SELECT * FROM Users WHERE Email = '{email}' AND Password = '{password}' AND IsActive = 1");
        }

        // Phục vụ tính năng Đăng ký tài khoản khách hàng (RoleID của Customer thường cố định, ví dụ: 3)
        public int DangKyKhachHang(string fullName, string email, string password, string phone, string address, int roleId)
        {
            string query = $"INSERT INTO Users (FullName, Email, Password, Phone, Address, RoleID) " +
                           $"VALUES (N'{fullName}', '{email}', '{password}', '{phone}', N'{address}', {roleId})";
            return db.thucthiketnoi(query);
        }
    }
}