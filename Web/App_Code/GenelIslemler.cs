using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

public static class GenelIslemler
{
    public const string SatirSonu = "<br/>";
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }
    public static String ToTemizMetin(this String s, bool satirSonu = false)
    {
        s = s.Replace("<", "&lt;");
        s = s.Replace(">", "&gt;");
        s = s.Replace("script", "scr_ipt");
        s = s.Replace("'", "`");
        s = s.Replace("\"", "`");

        if (satirSonu)
            s = s.Replace(Environment.NewLine, SatirSonu);
        s = s.Trim();
        return s;
    }
    public static int ToInt32(this object sayi)
    {
        try
        {
            if (sayi == null) throw new Exception();
            int x = Convert.ToInt32(sayi);
            return x;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public static bool IsInteger(this Object sayi)
    {
        try
        {
            if (sayi == null) throw new Exception();
            Convert.ToInt32(sayi);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public static string ToURL(this string s)
    {
        if (string.IsNullOrEmpty(s)) return "";
        if (s.Length > 80)
            s = s.Substring(0, 80);
        s = s.Replace("ş", "s");
        s = s.Replace("Ş", "S");
        s = s.Replace("ğ", "g");
        s = s.Replace("Ğ", "G");
        s = s.Replace("İ", "I");
        s = s.Replace("ı", "i");
        s = s.Replace("ç", "c");
        s = s.Replace("Ç", "C");
        s = s.Replace("ö", "o");
        s = s.Replace("Ö", "O");
        s = s.Replace("ü", "u");
        s = s.Replace("Ü", "U");
        s = s.Replace("'", "");
        s = s.Replace("\"", "");
        Regex r = new Regex("[^a-zA-Z0-9_-]");
        //if (r.IsMatch(s))
        s = r.Replace(s, "-");
        if (!string.IsNullOrEmpty(s))
            while (s.IndexOf("--") > -1)
                s = s.Replace("--", "-");
        if (s.StartsWith("-")) s = s.Substring(1);
        if (s.EndsWith("-")) s = s.Substring(0, s.Length - 1);
        return s;
    }

    public static double ToDouble(this object sayi)
    {
        try
        {
            if (sayi == null) throw new Exception();
            System.Globalization.CultureInfo culInfo = new System.Globalization.CultureInfo("tr-TR");
            var decValue = Convert.ToDouble(sayi);
            return decValue;
        }
        catch (Exception)
        {
            return double.NaN;
        }
    }

    public static String TarihYaz(DateTime tarih, bool full)
    {
        if (full) return TarihYaz(tarih);
        //DateTime tarih = Convert.ToDateTime(obj);
        DateTime simdi = DateTime.Now;
        String s;

        if (simdi.Date == tarih.Date)
            s = "Bugün";
        else if (tarih.Date.AddDays(1.0).Date == simdi.Date)
            s = "Dün";
        else
            s = String.Format("{0:00}.{1:00}.{2}", tarih.Day, tarih.Month, tarih.Year);

        return s.ToString();
    }

    public static String TarihYaz(DateTime tarih)
    {
        //DateTime tarih = Convert.ToDateTime(obj);
        DateTime simdi = DateTime.Now;
        String s;
        if (tarih.Hour == 0 && tarih.Minute == 0 && tarih.Second == 0)
        {
            if (simdi.Date == tarih.Date)
                s = "Bugün";
            else if (tarih.Date.AddDays(1.0).Day == simdi.Date.Day)
                s = "Dün";
            else
                s = String.Format("{0:00}.{1:00}.{2}", tarih.Day, tarih.Month, tarih.Year);
        }
        else
        {
            if (simdi.Date == tarih.Date)
                s = String.Format("Bugün {0:00}:{1:00}", tarih.Hour, tarih.Minute);
            else if (tarih.Date.AddDays(1.0).Day == simdi.Date.Day)
                s = String.Format("Dün {0:00}:{1:00}", tarih.Hour, tarih.Minute);
            else
                s = String.Format("{0:00}.{1:00}.{2} {3:00}:{4:00}", tarih.Day, tarih.Month, tarih.Year, tarih.Hour, tarih.Minute);
        }
        return s.ToString();
    }

    public static string SoldanMetinAl(this string metin, int uzunluk)
    {
        if (metin.Length < uzunluk)
            return metin;
        else
            return metin.Substring(0, uzunluk) + "..";
    }

    public static void Sort(this ListControl lb, bool desc = false)
    {
        var list = lb.Items.Cast<ListItem>().ToArray();
        list = desc
                    ? list.OrderByDescending(x => x.Text).ToArray()
                    : list.OrderBy(x => x.Text).ToArray();
        lb.Items.Clear();
        lb.Items.AddRange(list);
    }
    public static void SortByValue(this ListControl lb, bool desc = false)
    {
        var list = lb.Items.Cast<ListItem>().ToArray();
        list = desc
                    ? list.OrderByDescending(x => x.Value).ToArray()
                    : list.OrderBy(x => x.Value).ToArray();
        lb.Items.Clear();
        lb.Items.AddRange(list);
    }
    public static void SortByText(this ListControl lb, bool desc = false)
    {
        lb.Sort(desc);
    }
    public static void SortRandom(this ListControl lb)
    {
        var list = lb.Items.Cast<ListItem>()
                            .OrderBy(x => Guid.NewGuid().ToString())
                            .ToArray();
        lb.Items.Clear();
        lb.Items.AddRange(list);
    }
    public static void ImageBoyutlandir(System.Web.UI.WebControls.Image image, int MaxWidth, int MaxHeight)
    {
        try
        {
            using (System.Drawing.Image img =
                                        System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(image.ImageUrl)))
            {
                if (img.Width > MaxWidth || img.Height > MaxHeight)
                {
                    double widthRatio = 1.0;
                    if ((int)MaxWidth > 0)
                        widthRatio = (double)img.Width / (double)MaxWidth;

                    double heightRatio = 1.0;
                    if ((int)MaxHeight > 0)
                        heightRatio = (double)img.Height / (double)MaxHeight;

                    double ratio = Math.Max(widthRatio, heightRatio);
                    int newWidth = (int)(img.Width / ratio);
                    int newHeight = (int)(img.Height / ratio);

                    image.Width = Unit.Pixel(newWidth);
                    image.Height = Unit.Pixel(newHeight);
                }
            }
        }
        catch (Exception)
        { }
    }

    public static bool IsCookie(String c)
    {
        int i;
        string[] keys;
        keys = HttpContext.Current.Request.Cookies.AllKeys;
        for (i = 0; i < keys.Length; i++)
        {
            if (keys[i] == c)
            {
                return true;
            }

        }
        return false;
    }

    public static string IP()
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