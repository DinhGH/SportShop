using SportShop.App_Code;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportShop.GUI.Customer
{
    public partial class GioHang : Page
    {
        CartDAL cart = new CartDAL();

        // Tạm thời dùng CustomerID = 7
        // Sau này sẽ lấy từ Session
        //int customerId = 7;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadCart();
            }
        }

        private void LoadCart()
        {
            int customerId = Convert.ToInt32(Session["UserID"]);

            DataTable dt = cart.LayGioHangCuaKhach(customerId);

            gvCart.DataSource = dt;
            gvCart.DataBind();

            lblTongTien.Text = cart.TinhTongTien(customerId).ToString("N0") + " đ";
        }

        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int cartId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "DeleteItem")
            {
                cart.XoaKhoiGio(cartId);
            }
            else if (e.CommandName == "Plus")
            {
                int result = cart.TangSoLuong(cartId);

                if (result == 0)
                {
                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "msg",
                        "alert('Đã đạt số lượng tồn kho!');",
                        true);
                }
            }
            else if (e.CommandName == "Minus")
            {
                cart.GiamSoLuong(cartId);
            }

            LoadCart();
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Chức năng thanh toán đang được phát triển!');</script>");
        }
    }
}