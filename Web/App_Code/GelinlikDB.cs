using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteWorld.DAL;
using WhiteWorld.Info;

/// <summary>
/// Summary description for GelinlikDB
/// </summary>
public class GelinlikDB
{
    public GelinlikDB()
    {

    }

    public List<GelinlikInfo> YeniSezonGetir(int dilKod)
    {
        using (var db = new WhiteWorldEntities())
        {
            var YeniSezon = (from x in db.gelinlikler
                             let onFoto = (from t in db.gelinlikfotograflari
                                           where t.GelinlikId == x.Id && t.OnFoto && x.Goster
                                           select t).FirstOrDefault()
                             let arkaFoto = (from b in db.gelinlikfotograflari
                                             where b.GelinlikId == x.Id && b.ArkaFoto
                                             select b).FirstOrDefault()
                             where x.DilKod == dilKod && x.YeniSezon && x.Goster
                             orderby x.Oncelik descending
                             select new GelinlikInfo
                             {
                                 Id = x.Id,
                                 Baslik = x.Baslik,
                                 OnFoto = onFoto.FotoKucuk,
                                 ArkaFoto = arkaFoto.FotoKucuk,
                                 ArkaFotoEtiket = arkaFoto.Etiket,
                                 OnFotoEtiket = onFoto.Etiket,
                                 Yeni = x.Yeni
                             }).Take(8).ToList();
            return YeniSezon;
        }
    }

    public List<GelinlikInfo> OzelUrunGetir(int dilKod)
    {
        using (var db = new WhiteWorldEntities())
        {
            var OzelUrun = (from x in db.gelinlikler
                            let onFoto = (from t in db.gelinlikfotograflari
                                          where t.GelinlikId == x.Id && t.OnFoto && x.Goster
                                          select t).FirstOrDefault()
                            let arkaFoto = (from b in db.gelinlikfotograflari
                                            where b.GelinlikId == x.Id && b.ArkaFoto
                                            select b).FirstOrDefault()
                            where x.DilKod == dilKod && x.OzelUrun && x.Goster
                            orderby x.Oncelik descending
                            select new GelinlikInfo
                            {
                                Id = x.Id,
                                Baslik = x.Baslik,
                                OnFoto = onFoto.FotoKucuk,
                                ArkaFoto = arkaFoto.FotoKucuk,
                                ArkaFotoEtiket = arkaFoto.Etiket,
                                OnFotoEtiket = onFoto.Etiket,
                                Yeni = x.Yeni
                            }).Take(8).ToList();
            return OzelUrun;
        }
    }

    public List<GelinlikInfo> EnCokSatanGetir(int dilKod)
    {
        using (var db = new WhiteWorldEntities())
        {
            var EnCokSatan = (from x in db.gelinlikler
                              let onFoto = (from t in db.gelinlikfotograflari
                                            where t.GelinlikId == x.Id && t.OnFoto && x.Goster
                                            select t).FirstOrDefault()
                              let arkaFoto = (from b in db.gelinlikfotograflari
                                              where b.GelinlikId == x.Id && b.ArkaFoto
                                              select b).FirstOrDefault()
                              where x.DilKod == dilKod && x.EnCokSatan && x.Goster
                              orderby x.Oncelik descending
                              select new GelinlikInfo
                              {
                                  Id = x.Id,
                                  Baslik = x.Baslik,
                                  OnFoto = onFoto.FotoKucuk,
                                  ArkaFoto = arkaFoto.FotoKucuk,
                                  ArkaFotoEtiket = arkaFoto.Etiket,
                                  OnFotoEtiket = onFoto.Etiket,
                                  Yeni = x.Yeni
                              }).Take(8).ToList();
            return EnCokSatan;
        }
    }

    public string Renk(int gelinlikId)
    {
        using (var db = new WhiteWorldEntities())
        {
            var renk = (from x in db.gelinlikler
                        join r in db.g_renkler on x.RenkId equals r.Id
                        select r.Baslik).FirstOrDefault();
            return renk;
        }
    }

    public string Kumas(int gelinlikId)
    {
        using (var db = new WhiteWorldEntities())
        {
            var kumas = (from x in db.gelinlikler
                         join r in db.g_kumaslar on x.KumasId equals r.Id
                         select r.Baslik).FirstOrDefault();
            return kumas;
        }
    }

    public string YakaTipi(int gelinlikId)
    {
        using (var db = new WhiteWorldEntities())
        {
            var YakaTipi = (from x in db.gelinlikler
                            join r in db.g_yakatipi on x.YakaTipiId equals r.Id
                            select r.Baslik).FirstOrDefault();
            return YakaTipi;
        }
    }

    public string Siluet(int gelinlikId)
    {
        using (var db = new WhiteWorldEntities())
        {
            var siluet = (from x in db.gelinlikler
                          join r in db.g_siluet on x.SiluetId equals r.Id
                          select r.Baslik).FirstOrDefault();
            return siluet;
        }
    }
}