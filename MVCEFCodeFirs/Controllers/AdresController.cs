using MVCEFCodeFirs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEFCodeFirs.Controllers
{
    public class AdresController : Controller
    {
        // GET: Adres
        public ActionResult Yeni()
        {

            DatabaseContext db = new DatabaseContext();

            //LinQ ile
            List<SelectListItem> kisiliste = (from kisi in db.Kisiler.ToList()
                                              select new SelectListItem()
                                              {
                                                  Text = kisi.Ad + " " + kisi.Soyad,
                                                  Value = kisi.Id.ToString()

                                              }).ToList();

            //Normal
            //List<Kisiler> kisiler= db.Kisiler.ToList();

            //List<SelectListItem> kisiliste = new List<SelectListItem>();
            //foreach (var kisi in kisiler)
            //{
            //    SelectListItem item = new SelectListItem();
            //    item.Text = kisi.Ad + " " + kisi.Soyad;
            //    item.Value = kisi.Id.ToString();
            //    kisiliste.Add(item);
            //}


            TempData["kisiler"] = kisiliste;
            ViewBag.kisiler = kisiliste;
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Adresler adres)
        {
            DatabaseContext db = new DatabaseContext();
            Kisiler kisi = db.Kisiler.Where(x => x.Id == adres.Kisi.Id).FirstOrDefault();

            adres.Kisi = kisi;
            db.Adresler.Add(adres);

            int sonuc= db.SaveChanges();
            if (sonuc > 0)
            {
                ViewBag.mesaj = "Adres Başarıyla Kaydedildi.";
                ViewBag.renk = "success";
            }

            else 
            { 
                ViewBag.mesaj = "Adres Kaydedilemedi!";
                ViewBag.renk = "danger";
            }

            ViewBag.kisiler = TempData["kisiler"];
            return View();
        }



        public ActionResult Duzenle(int adresid)
        {
            Adresler adres = new Adresler();
            DatabaseContext db = new DatabaseContext();


            if (adresid!=null)
            {
               adres = db.Adresler.Where(x => x.Id == adresid).FirstOrDefault();
            }

            List<SelectListItem> kisiliste = (from kisi in db.Kisiler.ToList()
                                              select new SelectListItem()
                                              {
                                                  Text = kisi.Ad + " " + kisi.Soyad,
                                                  Value = kisi.Id.ToString()

                                              }).ToList();

            TempData["kisiler"] = kisiliste;
            ViewBag.kisiler = kisiliste;

            return View(adres);
        }



        [HttpPost]
        public ActionResult Duzenle(Adresler adres, int adresid)
        {

            DatabaseContext db = new DatabaseContext();
            Adresler a = new Adresler();

            if (adresid != null)
            {

                a = db.Adresler.Where(x => x.Id == adresid).FirstOrDefault();
                Kisiler k = db.Kisiler.Where(x => x.Id == adres.Kisi.Id).FirstOrDefault();

                a.Kisi = k;
                a.AdresTanim = adres.AdresTanim;

                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.mesaj = "Adres Düzeni Kaydedildi.";
                    ViewBag.renk = "success";
                }
                else
                {
                    ViewBag.mesaj = "Adres Düzeni Kaydedilemedi!";
                    ViewBag.renk = "danger";
                }
            }


            ViewBag.kisiler = TempData["kisiler"];
            return View(a);
        }


        public ActionResult Sil(int? adresid)
        {

            Adresler adres = new Adresler();
            DatabaseContext db = new DatabaseContext();

            if (adresid != null)
            {
                adres = db.Adresler.Where(x => x.Id == adresid).FirstOrDefault();
            }

            return View(adres);
        }


        [HttpPost, ActionName("Sil")]  //ismi aynı iki metod tanımlanmadığı için metodun adını değiştirdik 
                                       //ve actionname sil olarak tanımladık.
        public ActionResult AdresSil(int? adresid)
        {

            Adresler adres = new Adresler();
            DatabaseContext db = new DatabaseContext();

            if (adresid != null)
            {
                adres = db.Adresler.Where(x => x.Id == adresid).FirstOrDefault();
                db.Adresler.Remove(adres);

                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.mesaj = "Seçilen Adres Silinecektir!";
                    ViewBag.renk = "success";
                }
                else
                {
                    ViewBag.mesaj = "Seçilen Adres Silinemedi!";
                    ViewBag.renk = "danger";
                }
         
            }

            return RedirectToAction("Index", "Home"); //kişi silindikten sonra anasayfaya yönlendirmek için.
        }

    }
}