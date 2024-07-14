using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using webtekno.Models.entity2;

namespace webtekno.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        hastadb2Entities3 db = new hastadb2Entities3();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(sifreler model)
        {
            if (ModelState.IsValid)
            {
                // Veritabanında böyle bir kullanıcı olup olmadığını kontrol et
                bool userExists = db.sifreler.Any(x => x.kullaniciadi == model.kullaniciadi && x.sifre == model.sifre);

                if (userExists)
                {
                    // Kullanıcı adı ve şifre doğru olduğunda giriş yap
                    FormsAuthentication.SetAuthCookie(model.kullaniciadi, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Hatalı girişte mesaj göster
                    ViewBag.Mesaj = "Geçersiz Kullanıcı. Kullanıcı adı veya şifre hatalı.";
                    return View(model);
                }
            }

            // Model doğrulama hatası varsa tekrar login sayfasını göster
            ViewBag.Mesaj = "Lütfen tüm alanları doğru doldurun.";
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(sifreler model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcı adının benzersiz olup olmadığını kontrol et
                bool userExists = db.sifreler.Any(x => x.kullaniciadi == model.kullaniciadi);
                if (!userExists)
                {
                    db.sifreler.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Login"); // Kayıttan sonra login sayfasına yönlendir
                }
                else
                {
                    ViewBag.Mesaj = "Bu kullanıcı adı zaten mevcut.";
                    return View(model);
                }
            }

            ViewBag.Mesaj = "Lütfen tüm alanları doğru doldurun.";
            return View(model);
        }
    }
}
