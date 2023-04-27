using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteWorld.Info;

namespace WhiteWorld.DAL
{
    public class KategoriDB
    {
        public List<KategoriInfo> KumaslariGetir(int dilKod)
        {
            using (var db = new WhiteWorldEntities())
            {
                var kumaslar = (from x in db.g_kumaslar
                                where x.DilKod == dilKod
                                orderby x.Oncelik
                                select new KategoriInfo
                                {
                                    DilKod = x.DilKod,
                                    Baslik = x.Baslik,
                                    Goster = x.Goster,
                                    Oncelik = x.Oncelik,
                                    Id = x.Id
                                }).ToList();
                return kumaslar;
            }
        }

        public List<KategoriInfo> RenkleriGetir(int dilKod)
        {
            using (var db = new WhiteWorldEntities())
            {
                var renkler = (from x in db.g_renkler
                               where x.DilKod == dilKod && x.Goster
                               orderby x.Oncelik 
                               select new KategoriInfo
                               {
                                   DilKod = x.DilKod,
                                   Baslik = x.Baslik,
                                   Goster = x.Goster,
                                   Oncelik = x.Oncelik,
                                   Id = x.Id
                               }).ToList();
                return renkler;
            }
        }

        public List<KategoriInfo> SiluetleriGetir(int dilKod)
        {
            using (var db = new WhiteWorldEntities())
            {
                var siluetler = (from x in db.g_siluet
                                 where x.DilKod == dilKod && x.Goster
                                 orderby x.Oncelik 
                                 select new KategoriInfo
                                 {
                                     DilKod = x.DilKod,
                                     Baslik = x.Baslik,
                                     Goster = x.Goster,
                                     Oncelik = x.Oncelik,
                                     Id = x.Id
                                 }).ToList();
                return siluetler;
            }
        }

        public List<KategoriInfo> YakaTipiGetir(int dilKod)
        {
            using (var db = new WhiteWorldEntities())
            {
                var yakaTipi = (from x in db.g_yakatipi
                                where x.DilKod == dilKod && x.Goster
                                orderby x.Oncelik 
                                select new KategoriInfo
                                {
                                    DilKod = x.DilKod,
                                    Baslik = x.Baslik,
                                    Goster = x.Goster,
                                    Oncelik = x.Oncelik,
                                    Id = x.Id
                                }).ToList();
                return yakaTipi;
            }
        }

        public List<KategoriInfo> KesimleriGetir(int dilKod)
        {
            using (var db = new WhiteWorldEntities())
            {
                var kumaslar = (from x in db.g_kesim
                                where x.DilKod == dilKod && x.Goster
                                orderby x.Oncelik 
                                select new KategoriInfo
                                {
                                    DilKod = x.DilKod,
                                    Baslik = x.Baslik,
                                    Goster = x.Goster,
                                    Oncelik = x.Oncelik,
                                    Id = x.Id
                                }).ToList();
                return kumaslar;
            }
        }
    }
}
