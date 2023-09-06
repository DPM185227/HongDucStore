using HongDucStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace HongDucStore.Controllers
{
    public class HomeController : Controller
    {
        XeMayHongDucEntities db = new XeMayHongDucEntities();
        public ActionResult Index()
        {
            var listProduct = db.SanPham.ToList();
            ViewBag.listColor = db.MauSac.ToList();
            ViewBag.listPost = db.BaiViet.OrderByDescending(m => m.MaBaiViet).ToList();
            return View(listProduct);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LoadMenu()
        {
            if (db.DanhMuc.Count() > 0)
            {
                var getMenu = db.DanhMuc.Select(s => new
                {
                    idCategory = s.MaDanhMuc,
                    nameCategory = s.TenDanhMuc
                }).ToList();
                return Json(new { code = 200, data = getMenu, msg = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { code = 400, msg = "Not Found" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadCategory(int idCategory)
        {
            var getProduct = db.SanPham.Where(m => m.MaDanhMuc == idCategory);
            ViewBag.listColor = db.MauSac.ToList();
            return View(getProduct);
        }

        public ActionResult ProductAll()
        {
            var getAllProduct = db.SanPham.ToList();
            ViewBag.listColor = db.MauSac.ToList();
            ViewBag.listCategory = db.DanhMuc.ToList();
            ViewBag.Hang = db.HangXe.ToList();
            return View(getAllProduct);
        }

        public ActionResult Detail(int idProduct)
        {
            if (Session["KhachHang"] != null)
            {
                var product = db.SanPham.Where(m => m.MaSanPham == idProduct).FirstOrDefault();
                if (product != null)
                {
                    ViewBag.listComment = db.BinhLuan.Where(m => m.MaSanPham == idProduct).ToList();
                    ViewBag.listColor = db.MauSac.Where(m => m.MaSanPham == idProduct).ToList();
                    return View(product);
                }
                return HttpNotFound();
            }
            else return RedirectToAction("UserLogin", "Home");
        }

        [HttpPost]
        public ActionResult changeColor(int maMau)
        {
            var getColor = db.MauSac.Where(m => m.MaMau == maMau).Select(s => new
            {
                imageColor = s.HinhAnh.Replace("\\", "/"),
                nameChange = db.SanPham.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().TenSanPham + " - " + s.TenMau,
                delta = s.Delta != null ? s.Delta : 0
            }).FirstOrDefault();
            if (getColor != null)
            {
                return Json(new { code = 200, data = getColor, msg = "Thành công" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { code = 404, data = "", msg = "Not Found" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult fitterProduct(int? maHang, int? maDanhMuc, int? priceProduct)
        {
            if (maHang > 0)
            {
                var getProduct = db.SanPham.Where(m => m.MaHang == maHang).Select(s => new
                {
                    MaSanPham = s.MaSanPham,
                    TenSanPham = s.TenSanPham,
                    GiaBan = s.GiaBan,
                    GiaGiam = s.GiaGiam != null ? s.GiaGiam : 0,
                    HinhAnh = db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh != null ? db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh : null
                }).ToList();
                // khởi tạo obj return json
                if (getProduct.Count() > 0)
                    return Json(new
                    {
                        code = 200,
                        data = getProduct,
                        msg = "Thành công"
                    }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new
                    {
                        code = 404,
                        data = "",
                        msg = "Not Found"
                    }, JsonRequestBehavior.AllowGet);
            }
            else if (maDanhMuc > 0)
            {
                var getProduct = db.SanPham.Where(m => m.MaDanhMuc == maDanhMuc).Select(s => new
                {
                    MaSanPham = s.MaSanPham,
                    TenSanPham = s.TenSanPham,
                    GiaBan = s.GiaBan,
                    GiaGiam = s.GiaGiam != null ? s.GiaGiam : 0,
                    HinhAnh = db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh != null ? db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh : null
                }).OrderByDescending(m => m.MaSanPham).ToList();
                // khởi tạo obj return json
                if (getProduct.Count() > 0)
                    return Json(new
                    {
                        code = 200,
                        data = getProduct,
                        msg = "Thành công"
                    }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new
                    {
                        code = 404,
                        data = "",
                        msg = "Not Found"
                    }, JsonRequestBehavior.AllowGet);
            }
            else if (priceProduct > 0)
            {
                var getProduct = db.SanPham.Where(m => m.GiaBan > 0 && m.GiaBan <= priceProduct || m.GiaGiam > 0 && m.GiaGiam <= priceProduct).Select(s => new
                {
                    MaSanPham = s.MaSanPham,
                    TenSanPham = s.TenSanPham,
                    GiaBan = s.GiaBan,
                    GiaGiam = s.GiaGiam != null ? s.GiaGiam : 0,
                    HinhAnh = db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh != null ? db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh : null
                }).OrderByDescending(m => m.MaSanPham).ToList();
                // khởi tạo obj return json
                if (getProduct.Count() > 0)
                    return Json(new
                    {
                        code = 200,
                        data = getProduct,
                        msg = "Thành công"
                    }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new
                    {
                        code = 404,
                        data = "",
                        msg = "Not Found"
                    }, JsonRequestBehavior.AllowGet);
            }
            else if (maHang > 0 && maDanhMuc > 0)
            {
                var getProduct = db.SanPham.Where(m => m.MaHang == maHang && m.MaDanhMuc == maDanhMuc).Select(s => new
                {
                    MaSanPham = s.MaSanPham,
                    TenSanPham = s.TenSanPham,
                    GiaBan = s.GiaBan,
                    GiaGiam = s.GiaGiam != null ? s.GiaGiam : 0,
                    HinhAnh = db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh != null ? db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh : null
                }).OrderByDescending(m => m.MaSanPham).ToList();
                // khởi tạo obj return json
                if (getProduct.Count() > 0)
                    return Json(new
                    {
                        code = 200,
                        data = getProduct,
                        msg = "Thành công"
                    }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new
                    {
                        code = 404,
                        data = "",
                        msg = "Not Found"
                    }, JsonRequestBehavior.AllowGet);
            }
            else if (maDanhMuc > 0 && priceProduct > 0)
            {
                var getProduct = db.SanPham.Where(m => m.MaDanhMuc == maDanhMuc && m.GiaBan > 0 && m.GiaBan <= priceProduct || m.GiaGiam > 0 && m.GiaGiam <= priceProduct).Select(s => new
                {
                    MaSanPham = s.MaSanPham,
                    TenSanPham = s.TenSanPham,
                    GiaBan = s.GiaBan,
                    GiaGiam = s.GiaGiam != null ? s.GiaGiam : 0,
                    HinhAnh = db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh != null ? db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh : null
                }).OrderByDescending(m => m.MaSanPham).ToList();
                // khởi tạo obj return json
                if (getProduct.Count() > 0)
                    return Json(new
                    {
                        code = 200,
                        data = getProduct,
                        msg = "Thành công"
                    }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new
                    {
                        code = 404,
                        data = "",
                        msg = "Not Found"
                    }, JsonRequestBehavior.AllowGet);
            }
            else if (maHang > 0 && priceProduct > 0)
            {
                var getProduct = db.SanPham.Where(m => m.MaHang == maHang && m.GiaBan > 0 && m.GiaBan <= priceProduct || m.GiaGiam > 0 && m.GiaGiam <= priceProduct).Select(s => new
                {
                    MaSanPham = s.MaSanPham,
                    TenSanPham = s.TenSanPham,
                    GiaBan = s.GiaBan,
                    GiaGiam = s.GiaGiam != null ? s.GiaGiam : 0,
                    HinhAnh = db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh != null ? db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh : null
                }).OrderByDescending(m => m.MaSanPham).ToList();
                // khởi tạo obj return json
                if (getProduct.Count() > 0)
                    return Json(new
                    {
                        code = 200,
                        data = getProduct,
                        msg = "Thành công"
                    }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new
                    {
                        code = 404,
                        data = "",
                        msg = "Not Found"
                    }, JsonRequestBehavior.AllowGet);
            }
            else if (maHang > 0 && maDanhMuc > 0 && priceProduct > 0)
            {
                var getProduct = db.SanPham.Where(m => m.MaHang == maHang && m.MaDanhMuc == maDanhMuc && m.GiaBan > 0 && m.GiaBan <= priceProduct || m.GiaGiam > 0 && m.GiaGiam <= priceProduct).Select(s => new
                {
                    MaSanPham = s.MaSanPham,
                    TenSanPham = s.TenSanPham,
                    GiaBan = s.GiaBan,
                    GiaGiam = s.GiaGiam != null ? s.GiaGiam : 0,
                    HinhAnh = db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh != null ? db.MauSac.Where(m => m.MaSanPham == s.MaSanPham).FirstOrDefault().HinhAnh : null
                }).OrderByDescending(m => m.MaSanPham).ToList();
                // khởi tạo obj return json
                if (getProduct.Count() > 0)
                    return Json(new
                    {
                        code = 200,
                        data = getProduct,
                        msg = "Thành công"
                    }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new
                    {
                        code = 404,
                        data = "",
                        msg = "Not Found"
                    }, JsonRequestBehavior.AllowGet);
            }
            else return Json(new { code = 404, msg = "Not Found" }, JsonRequestBehavior.AllowGet);
        }

        // customer 
        // đăng nhập
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(string userName, string passWord)
        {
            string passConvert = Library.Library.ComputeMd5Hash(passWord);
            var customer = db.KhachHang.Where(m => m.TenDangNhap == userName && m.MatKhau == passConvert).FirstOrDefault();
            if (customer != null)
            {
                Session["KhachHang"] = customer;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Đăng nhập thất bại";
                return View();
            }
        }

        // xử lý đăng ký từ modal
        [HttpPost]
        public ActionResult RegisterUser(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                khachHang.HinhAnh = Library.Library.UploadFile("Customer", khachHang.imageCustomer);
                khachHang.MatKhau = Library.Library.ComputeMd5Hash(khachHang.MatKhau);
                db.KhachHang.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("UserLogin", "Home");
            }
            return View();
        }

        //
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // them binh luan
        public JsonResult AddComment(string context, int? idProduct, int? idPost)
        {
            Object result = new Object();
            var khachHang = (KhachHang)Session["KhachHang"];
            // Khởi tạo một bình luận mới 
            if (idProduct != null)
            {
                BinhLuan binhLuan = new BinhLuan
                {
                    MaKhachHang = khachHang.MaKhachHang,
                    NoiDung = context,
                    TaoLuc = DateTime.Now,
                    TinhTrang = 0,
                    MaSanPham = idProduct
                };
                db.BinhLuan.Add(binhLuan);
                if (db.SaveChanges() > 0)
                {
                    var commnetResult = db.BinhLuan.Where(m => m.IDComment == binhLuan.IDComment).Select(s => new
                    {
                        MaBinhLuan = binhLuan.IDComment,
                        NoiDung = binhLuan.NoiDung,
                        HinhAnh = db.KhachHang.Where(m => m.MaKhachHang == binhLuan.MaKhachHang).FirstOrDefault().HinhAnh.Replace("\\", "/"),
                        CreateAt = binhLuan.TaoLuc.Value
                    }).FirstOrDefault();
                    result = new
                    {
                        code = 200,
                        commentSuccess = (commnetResult),
                        msg = "Thêm thành công"
                    };
                }
                else
                {
                    result = new
                    {
                        code = 500,
                        msg = "Thêm bình luận thất bại"
                    };
                }
            }
            else if (idPost != null)
            {
                BinhLuan binhLuan = new BinhLuan
                {
                    MaKhachHang = khachHang.MaKhachHang,
                    NoiDung = context,
                    TaoLuc = DateTime.Now,
                    TinhTrang = 0,
                    MaBaiViet = idPost
                };
                db.BinhLuan.Add(binhLuan);
                if (db.SaveChanges() > 0)
                {
                    var commnetResult = db.BinhLuan.Where(m => m.IDComment == binhLuan.IDComment).Select(s => new
                    {
                        MaBinhLuan = binhLuan.IDComment,
                        NoiDung = binhLuan.NoiDung,
                        HinhAnh = db.KhachHang.Where(m => m.MaKhachHang == binhLuan.MaKhachHang).FirstOrDefault().HinhAnh.Replace("\\", "/"),
                    }).FirstOrDefault();
                    result = new
                    {
                        code = 200,
                        commentSuccess = (commnetResult),
                        msg = "Thêm thành công"
                    };
                }
                else
                {
                    result = new
                    {
                        code = 500,
                        msg = "Thêm bình luận thất bại"
                    };
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MyOrder()
        {
            var customer = (KhachHang)Session["KhachHang"];
            if (customer != null)
            {
                var getOrder = db.DatHang.Where(m => m.MaKhachHang == customer.MaKhachHang).ToList();
                return View(getOrder);
            }
            else
            {
                return HttpNotFound();
            }
        }


        public ActionResult OrderDetail(int idOrder)
        {
            var orderDetail = db.DatHangChiTiet.Where(m => m.MaDatHang == idOrder).ToList();
            return View(orderDetail);
        }
        // HỦY ĐƠN HÀNG
        public ActionResult CancelOrder(int idOrder)
        {
            var getOrder = db.DatHang.Where(m => m.MaDatHang == idOrder).FirstOrDefault();
            if (getOrder != null)
            {
                getOrder.TinhTrangDonHang = 5;
                getOrder.TongTien = 0;
                db.Entry(getOrder).State = EntityState.Modified;
                db.SaveChanges();
                // update số lượng 
                var getOrderDetail = db.DatHangChiTiet.Where(m => m.MaDatHang == getOrder.MaDatHang).ToList();
                foreach (var item in getOrderDetail)
                {
                    var getQuantity = db.MauSac.Where(m => m.MaMau == item.MaMau).FirstOrDefault();
                    getQuantity.SoLuong += item.SoLuong;
                    db.Entry(getQuantity).State = EntityState.Modified;
                    db.DatHangChiTiet.Remove(item);
                    db.SaveChanges();
                }
                return RedirectToAction("MyOrder", "Home");
            }
            else
                return HttpNotFound();
        }


        // xem bài viết
        public ActionResult ViewPost(int idPost)
        {
            var getPost = db.BaiViet.Where(m => m.MaBaiViet == idPost).FirstOrDefault();
            getPost.LuotXem++;
            db.Entry(getPost).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                return View(getPost);
            }
            else return HttpNotFound();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaleCar(LienHeBanXe lienHeBanXe)
        {
            if(ModelState.IsValid)
            {
                if (lienHeBanXe.imageProduct != null) lienHeBanXe.HinhAnhXe = Library.Library.UploadFile("SaleCar", lienHeBanXe.imageProduct);
                db.LienHeBanXe.Add(lienHeBanXe);
                db.SaveChanges();
                ViewBag.addResult = "Thêm thành công";
                return View();
            }
            else
            {
                ViewBag.addResult = "Thêm thất bại";
                return View();
            }
        }

        // 
        public ActionResult viewAllPost()
        {
            var getAllPost = db.BaiViet.Where(m => m.XetDuyet == 0).ToList();
            return View(getAllPost);
        }

        public ActionResult Search(string key)
        {
            var getProduct = db.SanPham.Where(m => m.TenSanPham.Contains(key)).ToList();
            ViewBag.listColor = db.MauSac.ToList();
            return View(getProduct);    
        }
    }
}