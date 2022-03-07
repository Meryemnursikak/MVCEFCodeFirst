using MVCEFCodeFirs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEFCodeFirs.Controllers
{
    public class KisiController : Controller
    {
        // GET: Kisi
        public ActionResult Yeni()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Kisiler kisi)
        {

            DatabaseContext db = new DatabaseContext();
            db.Kisiler.Add(kisi);

            int sonuc =db.SaveChanges();

            if (sonuc > 0)
            {
                ViewBag.mesaj = "Kişi Başarıyla Kaydedildi.";
                ViewBag.renk = "success";
            }
            else
            { 
                ViewBag.mesaj = "Kişi Kaydedilemedi!";
                ViewBag.renk = "danger";
            }
            return View();
        }



        public ActionResult Duzenle(int? kisiid)
        {
            Kisiler kisi = new Kisiler();
            DatabaseContext db = new DatabaseContext();

            if (kisiid!=null)
            {
                kisi = db.Kisiler.Where(x => x.Id == kisiid).FirstOrDefault();
            }


            return View(kisi);
        }



        [HttpPost]
        public ActionResult Duzenle(Kisiler kisi, int kisiid)
        {
            Kisiler k = new Kisiler();
            DatabaseContext db = new DatabaseContext();

            if (kisiid!=null)
            {
                k = db.Kisiler.Where(x => x.Id == kisiid).FirstOrDefault();

                k.Ad = kisi.Ad;
                k.Soyad = kisi.Soyad;
                k.Yas = kisi.Yas;

                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.mesaj = "Kişi Düzeni Kaydedildi.";
                    ViewBag.renk = "success";
                }
                else
                {
                    ViewBag.mesaj = "Kişi Düzeni Kaydedilemedi!";
                    ViewBag.renk = "danger";
                }
            }


            return View(k);
        }


        public ActionResult Sil(int? kisiid)
        {

            Kisiler kisi = new Kisiler();
            DatabaseContext db = new DatabaseContext();

            if (kisiid != null)
            {
                kisi = db.Kisiler.Where(x => x.Id == kisiid).FirstOrDefault();
            }

            return View(kisi);
        }


        [HttpPost,ActionName("Sil")]  //ismi aynı iki metod tanımlanmadığı için metodun adını değiştirdik 
                                      //ve actionname sil olarak tanımladık.
        public ActionResult KisiSil(int? kisiid)
        {

            Kisiler kisi = new Kisiler();
            DatabaseContext db = new DatabaseContext();

            if (kisiid != null)
            {
                kisi = db.Kisiler.Where(x => x.Id == kisiid).FirstOrDefault();
                db.Kisiler.Remove(kisi);
                db.SaveChanges();
            }

            return RedirectToAction("Index","Home"); //kişi silindikten sonra anasayfaya yönlendirmek için.
        }
    }
}