using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.App_Code
{
    public class StoreDAL
    {
        ConnectionProvider db = new ConnectionProvider();

        // Lấy thông tin cửa hàng dựa vào ID người chủ (Owner) công tác tại đó
        public DataTable LayCuaHangTheoChu(int ownerId)
        {
            return db.laydulieu($"SELECT * FROM Stores WHERE OwnerID = {ownerId}");
        }

        public int TaoCuaHangMoi(string storeName, int ownerId, string address, string phone)
        {
            string query = $"INSERT INTO Stores (StoreName, OwnerID, StoreAddress, StorePhone) " +
                           $"VALUES (N'{storeName}', {ownerId}, N'{address}', '{phone}')";
            return db.thucthiketnoi(query);
        }
    }
}