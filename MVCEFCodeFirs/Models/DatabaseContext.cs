using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCEFCodeFirs.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Kisiler> Kisiler { get; set; }
        public DbSet<Adresler> Adresler { get; set; }

        //database create etmek için constr. metot oluşturduk.
        public DatabaseContext()
        {
            VeriTabanıOlustur v = new VeriTabanıOlustur();
            Database.SetInitializer(v);
        }
    }


    //ayrı bir class açmak açmak yerine burada oluşturduk.Veritabanına verileri atmak için:Tag ile..
    public class VeriTabanıOlustur : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                Kisiler k = new Kisiler();
                k.Ad = FakeData.NameData.GetFirstName();
                k.Soyad = FakeData.NameData.GetSurname();
                k.Yas = FakeData.NumberData.GetNumber(10, 80);
                context.Kisiler.Add(k);
            }
            context.SaveChanges();

            List<Kisiler> kisilistesi = context.Kisiler.ToList();


            foreach (var kisi in kisilistesi)
            {
                for (int i = 0; i < 3; i++)
                {
                    Adresler a = new Adresler();
                    a.Kisi = kisi; //kişisi eşleşti
                    a.AdresTanim = FakeData.PlaceData.GetAddress(); //adresi eşleşti
                    context.Adresler.Add(a); //ekledi
                }
            }


            context.SaveChanges();
        }
    }



}