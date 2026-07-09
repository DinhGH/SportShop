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
                string query = string.Format(@"SELECT ISNULL(SUM(od.Price * od.Quantity), 0) as TotalRevenue
                                    FROM OrderDetails od
                                    INNER JOIN Products p ON od.ProductID = p.ProductID
                                    WHERE p.StoreID = {0} AND od.Status = N'Đã giao'", storeId);
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
                string query = string.Format(@"SELECT COUNT(*) as OrderCount
                                    FROM OrderDetails
                                    WHERE StoreID = {0}", storeId);
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
                string query = string.Format(@"SELECT TOP {0}
                                    p.ProductID,
                                    p.ProductName,
                                    p.Price,
                                    p.ImageURL,
                                    SUM(od.Quantity) as TotalSold,
                                    COUNT(DISTINCT od.OrderID) as OrderCount
                                FROM OrderDetails od
                                INNER JOIN Products p ON od.ProductID = p.ProductID
                                WHERE p.StoreID = {1}
                                GROUP BY p.ProductID, p.ProductName, p.Price, p.ImageURL
                                ORDER BY TotalSold DESC", top, storeId);
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
                // Sử dụng UnitPrice và StoreID trực tiếp từ OrderDetails
                string query = string.Format(@"SELECT
                                    MONTH(o.CreatedAt) as Month,
                                    DATENAME(MONTH, o.CreatedAt) as MonthName,
                                    SUM(od.UnitPrice * od.Quantity) as Revenue,
                                    COUNT(od.OrderDetailID) as OrderCount
                                FROM OrderDetails od
                                INNER JOIN Orders o ON od.OrderID = o.OrderID
                                WHERE od.StoreID = {0} 
                                    AND YEAR(o.CreatedAt) = {1}
                                GROUP BY MONTH(o.CreatedAt), DATENAME(MONTH, o.CreatedAt)
                                ORDER BY MONTH(o.CreatedAt)", storeId, year);
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Lỗi SQL Doanh thu tháng: " + ex.Message);
                dt = null;
            }
            finally
            {
                db.dongketnoi();
            }
            return dt;
        }

        /// <summary>
        /// Lấy thống kê chi tiết: Doanh thu, Số đơn hàng, Số sản phẩm
        /// </summary>
        public DataTable LayThongKeChinh(int storeId)
        {
            DataTable dt = new DataTable();
            try
            {
                db.moketnoi();
                string query = string.Format(@"SELECT
                                (SELECT ISNULL(SUM(UnitPrice * Quantity), 0) FROM OrderDetails WHERE StoreID = {0}) as TotalRevenue,
                                (SELECT COUNT(*) FROM OrderDetails WHERE StoreID = {0}) as TotalOrders,
                                (SELECT COUNT(*) FROM Products WHERE StoreID = {0}) as TotalProducts,
                                (SELECT ISNULL(SUM(StockQuantity), 0) FROM Products WHERE StoreID = {0}) as TotalStock", storeId);
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Lỗi SQL: " + ex.Message);
                throw;
            }
            finally
            {
                db.dongketnoi();
            }
            return dt;
        }
    }
}
