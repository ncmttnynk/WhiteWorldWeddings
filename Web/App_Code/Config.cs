using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Xml.Linq;
using System.Net;

public static class Config
{
    private static XDocument docx = null;
    static Config()
    {
        Yukle();
    }

    private static void Yukle()
    {
        if (docx == null)
            docx = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Sabitler.xml"));
    }
    public static void Sifirla()
    {
        docx = null;
    }
    public static string Title
    {
        get
        {
            Yukle();
            return docx.Root.Element("AnaSite").Element("Title").Value;
        }
    }
    public static string SiteAdresi
    {
        get
        {
            Yukle();
            return docx.Root.Element("AnaSite").Element("Adres").Value;
        }
    }
    public static string GoogleSearchKey
    {
        get
        {
            Yukle();
            return docx.Root.Element("Google").Element("SearchKey").Value;
        }
    }
    public static string GoogleMapApiKey
    {
        get
        {
            Yukle();
            return docx.Root.Element("Google").Element("MapAPIKey").Value;
        }
    }
    public static class Statistics
    {
        public static string IP
        {
            get
            {
                string ip = "";
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (!string.IsNullOrEmpty(ip))
                    {
                        string[] ipRange = ip.Split(",".ToCharArray());
                        ip = ipRange[0];
                    }
                }
                if (string.IsNullOrEmpty(ip))
                    if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                        ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                ip = ip.Trim();
                return ip;
            }
        }
    }
}
