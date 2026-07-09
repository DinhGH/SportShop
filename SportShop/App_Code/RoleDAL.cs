using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.App_Code
{
    public class RoleDAL
    {
        ConnectionProvider db = new ConnectionProvider();

        public DataTable LayTatCaVaiTro()
        {
            return db.laydulieu("SELECT * FROM Roles");
        }
    }
}