using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SportShop.App_Code
{
    public class StatisticsDAL
    {
        ConnectionProvider db = new ConnectionProvider();

        /// <summary>
        /// Lấy tổng doanh thu của cửa hàng (chỉ tính các đơn hàng đã hoàn thành)
        /// </summary>
        public decimal LayTongDoanhThu(int storeId)
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = $@"SELECT ISNULL(SUM(od.Price * od.Quantity), 0) as TotalRevenue
                                FROM OrderDetails od
                                INNER JOIN Products p ON od.ProductID = p.ProductID
                                WHERE p.StoreID = {storeId} AND od.Status = N'Đã giao'";
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToDecimal(dt.Rows[0]["TotalRevenue"]);
                }
                return 0;
            }
            catch
            {
                return 0;
            }
            finally
            {
                db.dongketnoi();
            }
        }

        /// <summary>
        /// Lấy số lượng đơn hàng của cửa hàng
        /// </summary>
        public int LaySoDonHang(int storeId)
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = $@"SELECT COUNT(DISTINCT od.OrderID) as OrderCount
                                FROM OrderDetails od
                                INNER JOIN Products p ON od.ProductID = p.ProductID
                                WHERE p.StoreID = {storeId}";
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["OrderCount"]);
                }
                return 0;
            }
            catch
            {
                return 0;
            }
            finally
            {
                db.dongketnoi();
            }
        }

        /// <summary>
        /// Lấy top 5 sản phẩm bán chạy nhất của cửa hàng
        /// </summary>
        public DataTable LayTopSanPhamBanChay(int storeId, int top = 5)
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = $@"SELECT TOP {top}
                                p.ProductID,
                                p.ProductName,
                                p.Price,
                                p.ImageURL,
                                SUM(od.Quantity) as TotalSold,
                                COUNT(DISTINCT od.OrderID) as OrderCount
                            FROM OrderDetails od
                            INNER JOIN Products p ON od.ProductID = p.ProductID
                            WHERE p.StoreID = {storeId}
                            GROUP BY p.ProductID, p.ProductName, p.Price, p.ImageURL
                            ORDER BY TotalSold DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            finally
            {
                db.dongketnoi();
            }
            return dt;
        }

        /// <summary>
        /// Lấy thống kê doanh thu theo tháng của cửa hàng
        /// </summary>
        public DataTable LayDoanhThuTheoThang(int storeId, int year = 0)
        {
            DataTable dt = new DataTable();
            try
            {
                if (year == 0)
                    year = DateTime.Now.Year;

                db.moketnoi();
                string query = $@"SELECT
                                MONTH(o.CreatedAt) as Month,
                                DATENAME(MONTH, o.CreatedAt) as MonthName,
                                SUM(od.Price * od.Quantity) as Revenue,
                                COUNT(DISTINCT od.OrderID) as OrderCount
                            FROM OrderDetails od
                            INNER JOIN Products p ON od.ProductID = p.ProductID
                            INNER JOIN Orders o ON od.OrderID = o.OrderID
                            WHERE p.StoreID = {storeId} 
                                AND od.Status = N'Đã giao'
                                AND YEAR(o.CreatedAt) = {year}
                            GROUP BY MONTH(o.CreatedAt), DATENAME(MONTH, o.CreatedAt)
                            ORDER BY MONTH(o.CreatedAt)";
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            finally
            {
                db.dongketnoi();
            }
            return dt;
        }

        /// <summary>
        /// Lấy thống kê chi tiết: Doanh thu, Số đơn hàng, Số sản phẩm, Tồn kho
        /// </summary>
        public DataTable LayThongKeChinh(int storeId)
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = $@"SELECT
                                (SELECT ISNULL(SUM(od.Price * od.Quantity), 0) FROM OrderDetails od INNER JOIN Products p ON od.ProductID = p.ProductID WHERE p.StoreID = {storeId} AND od.Status = N'Đã giao') as TotalRevenue,
                                (SELECT COUNT(DISTINCT od.OrderID) FROM OrderDetails od INNER JOIN Products p ON od.ProductID = p.ProductID WHERE p.StoreID = {storeId}) as TotalOrders,
                                (SELECT COUNT(*) FROM Products WHERE StoreID = {storeId}) as TotalProducts,
                                (SELECT ISNULL(SUM(StockQuantity), 0) FROM Products WHERE StoreID = {storeId}) as TotalStock";
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            finally
            {
                db.dongketnoi();
            }
            return dt;
        }
    }
}
