using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteWorld.DAL;

/// <summary>
/// Summary description for SabitlerDB
/// </summary>
public class SabitlerDB
{
    public string TurkceIcerikGetir(Enums.Sabitler sabit)
    {
        return TurkceIcerikGetir(sabit.ToString());
    }
    public string TurkceIcerikGetir(string id)
    {
        string key = "SABITLER_TR" + id;
        string ret = null;
        if (HttpContext.Current.Cache[key] != null)
        {
            ret = (string)HttpContext.Current.Cache[key];
        }
        else
        {
            using (var db = new WhiteWorldEntities())
            {
                var sabit = db.sabitler.FirstOrDefault(x => x.Baslik == id && x.DilKod == 1);
                if (sabit != null)
                {
                    ret = sabit.Icerik;
                    HttpContext.Current.Cache[key] = ret;
                }
            }
        }
        return ret;
    }
    public string IngilizceIcerikGetir(Enums.Sabitler sabit)
    {
        return IngilizceIcerikGetir(sabit.ToString());
    }
    public string IngilizceIcerikGetir(string id)
    {
        string yek = "SABITLER_EN" + id;
        string ter = null;
        if (HttpContext.Current.Cache[yek] != null)
        {
            ter = (string)HttpContext.Current.Cache[yek];
        }
        else
        {
            using (var db = new WhiteWorldEntities())
            {
                var sabit = db.sabitler.FirstOrDefault(x => x.Baslik == id && x.DilKod == 2);
                if (sabit != null)
                {
                    ter = sabit.Icerik;
                    HttpContext.Current.Cache[yek] = ter;
                }
            }
        }
        return ter;
    }
    public string IcerikGetir(Enums.Sabitler sabit)
    {
        return IcerikGetir(sabit.ToString());
    }
    public string IcerikGetir(string id)
    {
        string yke = "SABITLER_" + id;
        string tre = null;
        if (HttpContext.Current.Cache[yke] != null)
        {
            tre = (string)HttpContext.Current.Cache[yke];
        }
        else
        {
            using (var db = new WhiteWorldEntities())
            {
                var sabit = db.sabitler.FirstOrDefault(x => x.Baslik == id);
                if (sabit != null)
                {
                    tre = sabit.Icerik;
                    HttpContext.Current.Cache[yke] = tre;
                }
            }
        }
        return tre;
    }
}