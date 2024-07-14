using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webtekno.Models;
using webtekno.Models.Entity;



namespace webtekno.Controllers
{
    public class HomeController : Controller
    {
        hastadb2Entities db = new hastadb2Entities();
        

        public ActionResult Index()
        {
            var degerler = db.personelbilgi.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult Addpage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Addpage(fotoekle TBL)
        {
          personelbilgi f= new personelbilgi();
            if (TBL.Photo!= null)
            {
                var extension = Path.GetExtension(TBL.Photo.FileName);
                var newimagename=Guid.NewGuid()+extension;
                var location=Path.Combine(Directory.GetCurrentDirectory(),"~/resimler/",newimagename);
                var stream = new FileStream(location, FileMode.Create);
                TBL.Photo.CopyTo(stream);
                f.Photo = newimagename;

            }
            f.meslekid = TBL.meslekid;
            f.ad= TBL.ad;
            f.departman = TBL.departman;
            f.mevki=TBL.mevki;
            f.yas=TBL.yas;
            f.tecrube=TBL.tecrube;


          db.personelbilgi.Add(f);
          db.SaveChanges();
          return RedirectToAction("Index");
           
        }
        public ActionResult GetImage(string imageName)
        {
            var filePath = Path.Combine(Server.MapPath("~/resimler"), imageName);
            if (System.IO.File.Exists(filePath))
            {
                byte[] imageByteData = System.IO.File.ReadAllBytes(filePath);
                return File(imageByteData, "image/jpeg");
            }
            return null; // or return a default image if file not found
        }



        public ActionResult Search(string p)
        {
            var degerler = from d in db.personelbilgi select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.ad.Contains(p));
            }

            return View(degerler.ToList());
        }

        public ActionResult SIL(int id)
        {
            var kategori = db.personelbilgi.Find(id);
            if (kategori != null)
            {
                db.personelbilgi.Remove(kategori);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult HomePage()
        {
            return View();
        }
    }
}

