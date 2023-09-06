using HongDucStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HongDucStore.Controllers
{
    public class CartController : Controller
    {
        private XeMayHongDucEntities db = new XeMayHongDucEntities();
        // GET: Cart
        public ActionResult Index()
        {
            if (Session["KhachHang"] == null) return RedirectToAction("UserLogin", "Home");
            ViewBag.danhSachChiNhanh = db.ChiNhanh.ToList();
            return View();
        }
        // add to cart
        [HttpPost]
        public ActionResult AddToCart(int maSanPham, int? maMau, int quantityBuy)
        {
            List<CartItem> cart;
            if (Session["Cart"] != null)
            {
                cart = (List<CartItem>)Session["Cart"];
            }
            else
            {
                cart = new List<CartItem>();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            Object jsonResult = new Object();
            var getProductQuantity = db.MauSac.Where(m => m.MaMau == maMau).FirstOrDefault().SoLuong;
            var customerSession = (KhachHang)Session["KhachHang"];
            var getCustomer = db.KhachHang.Where(m => m.MaKhachHang == customerSession.MaKhachHang).FirstOrDefault();
            var cartFullExit = cart.Where(m => m.sanPham.MaSanPham == maSanPham && m.mauSac.MaMau == maMau).FirstOrDefault();
            //////////////////////////////////
            if (quantityBuy < getProductQuantity)
            {
                if (cartFullExit != null)
                {
                    cartFullExit.SoLuongMua += quantityBuy;
                    jsonResult = new
                    {
                        code = 200,
                        msg = "Thêm vào giỏ thành công"
                    };
                }
                else
                {
                    if (maSanPham > 0 && maMau > 0)
                    {
                        var sanPhamAdd = db.SanPham.Where(m => m.MaSanPham == maSanPham).FirstOrDefault();
                        var colorAdd = db.MauSac.Where(m => m.MaMau == maMau).FirstOrDefault();
                        CartItem item = new CartItem()
                        {
                            IdItem = Guid.NewGuid(),
                            sanPham = sanPhamAdd,
                            mauSac = colorAdd,
                            SoLuongMua = quantityBuy
                        };
                        cart.Add(item);
                        //
                        jsonResult = new
                        {
                            code = 200,
                            msg = "Thêm vào giỏ thành công"
                        };
                    }
                    else
                    {
                        jsonResult = new
                        {
                            code = 500,
                            msg = "Thêm vào giỏ thất bại"
                        };
                    }
                }
            }
            else
            {
                jsonResult = new
                {
                    code = 500,
                    msg = "Chân thành xin lỗi, vì sản phẩm này hiện không còn đủ " + quantityBuy + " sản phẩm"
                };
            }

            // set value session
            Session["Cart"] = cart;
            return Json(jsonResult, "application/json", JsonRequestBehavior.AllowGet);
        }



        // delete item
        [HttpPost]
        public JsonResult DeleteItem(Guid idCart)
        {
            Object result;
            List<CartItem> cart = (List<CartItem>)Session["Cart"];
            var item = cart.Where(m => m.IdItem == idCart).FirstOrDefault();
            if (cart.Remove(item))
            {
                result = new
                {
                    code = 200,
                    msg = "Xóa thành công"
                };
            }
            else
            {
                result = new
                {
                    code = 500,
                    msg = "Xóa thất bại"
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateTang(Guid idCart, int quantity)
        {
            List<CartItem> cart = (List<CartItem>)Session["Cart"];
            Object result = new object();
            var cartItem = cart.Where(m => m.IdItem == idCart).FirstOrDefault();
            if (cartItem.mauSac.SoLuong >= quantity + 1)
            {
                cartItem.SoLuongMua++;
                result = new
                {
                    code = 200,
                    msg = "Cập nhật thành công"
                };
            }
            else
            {
                result = new
                {
                    code = 404,
                    msg = "Cập nhật thất bại"
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //
        [HttpPost]
        public JsonResult UpdateGiam(Guid idCart)
        {
            List<CartItem> cart = (List<CartItem>)Session["Cart"];
            Object result = new object();
            var cartItem = cart.Where(m => m.IdItem == idCart).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.SoLuongMua--;
                result = new
                {
                    code = 200,
                    msg = "Cập nhật thành công"
                };
            }
            else
            {
                result = new
                {
                    code = 404,
                    msg = "Cập nhật thất bại"
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // check out
        [HttpGet]
        public JsonResult CheckOutCart(int maKhachHang, string tenKhachHang, string soDienThoai, int chiNhanh, string thoiGianNhanHang)
        {
            try
            {
                Object result = new Object();
                int allPrice = 0;
                ///
                DatHang order = new DatHang()
                {
                    MaKhachHang = maKhachHang,
                    MaChiNhanh = chiNhanh,
                    ThoiGianNhanHang = thoiGianNhanHang,
                    HoTenNguoiNhan = tenKhachHang,
                    SoDienThoai = soDienThoai,
                    TongTien = 0,
                    TinhTrangDonHang = 0,
                    NgayLap = DateTime.Now
                };
                db.DatHang.Add(order);
                db.SaveChanges();
                // get cart item 
                List<CartItem> cart = (List<CartItem>)Session["Cart"];
                foreach (var item in cart)
                {
                    DatHangChiTiet orderDetail = new DatHangChiTiet
                    {
                        MaSanPham = item.sanPham.MaSanPham,
                        MaMau = item.mauSac.MaMau,
                        SoLuong = item.SoLuongMua,
                        GiaBan = item.sanPham.GiaGiam != null ? item.sanPham.GiaGiam : item.sanPham.GiaBan,
                        TongTien = item.sanPham.GiaGiam != null ? item.SoLuongMua * item.sanPham.GiaGiam : item.SoLuongMua * item.sanPham.GiaBan,
                        MaDatHang = order.MaDatHang
                    };
                    allPrice += Convert.ToInt32(orderDetail.TongTien);
                    // update 
                    var getProductQuantity = db.MauSac.Where(m => m.MaMau == orderDetail.MaMau).FirstOrDefault();
                    getProductQuantity.SoLuong -= item.SoLuongMua;
                    db.Entry(getProductQuantity).State = EntityState.Modified;
                    //
                    db.DatHangChiTiet.Add(orderDetail);
                    db.SaveChanges();
                }
                order.TongTien = allPrice;
                db.Entry(order).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    Session.Remove("Cart");
                    result = new
                    {
                        code = 200,
                        msg = "Đặt hàng thành công cảm ơn bạn đã mua hàng tại cửa hàng. Vui lòng xác nhận thông tin khi có nhân viên gọi đến, Xin chân thành cảm ơn"
                    };
                }
                else
                {
                    result = new
                    {
                        code = 500,
                        msg = "Đặt hàng thất bại !!! Xin chân thành xin lỗi vì sự bất tiện này"
                    };
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
            
        }
    }
}