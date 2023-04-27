using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using AjaxControlToolkit;

/// <summary>
/// Summary description for ResimDB
/// </summary>
public class DosyaDB
{
    string klasor = "";
    AsyncFileUpload fu = null;
    string dosyaAdi = "";
    string thumbnailDosyaAdi = "";
    HttpServerUtility server = HttpContext.Current.Server;
    public static class Uzantilar
    {
        public static string[] ResimDosyasiUzantilari
        {
            get
            {
                return new string[] { "jpg", "gif", "jpeg", "png" };
            }
        }
        public static string[] ExcelDosyasiUzantilari
        {
            get
            {
                return new string[] { "xls", "xlsx" };
            }
        }
        public static string[] SaltMetinUzantisi
        {
            get
            {
                return new string[] { "txt", "pdf" };
            }
        }
    }
    public string ThumbnailDosyaAdi
    {
        get { return thumbnailDosyaAdi; }
        set { thumbnailDosyaAdi = value; }
    }

    public string DosyaAdi
    {
        get { return dosyaAdi; }
        set { dosyaAdi = value; }
    }

    public DosyaDB(string klasor, AsyncFileUpload fu)
    {
        this.klasor = klasor;
        this.fu = fu;

        if (!Directory.Exists(server.MapPath(klasor)))
            Directory.CreateDirectory(server.MapPath(klasor));
    }

    public DosyaDB()
    {
    }
    public DosyaDB(string klasor)
    {
        this.klasor = klasor;

        if (!Directory.Exists(server.MapPath(klasor)))
            Directory.CreateDirectory(server.MapPath(klasor));
    }
    public void DosyaYukle(int maxBoyut, string[] uzantilar)
    {
        if (!fu.HasFile)
            throw new Exception("Bir dosya seçmelisiniz!");

        string ad = Path.GetFileNameWithoutExtension(fu.FileName);
        string uzanti = Path.GetExtension(fu.FileName).Substring(1);

        bool uzantiOK = uzantilar.Contains(uzanti.ToLower());

        if (!uzantiOK)
        {
            throw new Exception("Lütfen geçerli bir dosya seçiniz.");
        }
        if (fu.FileBytes.LongLength > maxBoyut * 1024)
        {
            throw new Exception(string.Format("{0}kb'den daha büyük bir dosya yükleyemezsiniz.",
                maxBoyut));
        }
        DosyaAdi = string.Format("{0}.{1}",
            string.Format("{0}_{1}", ad, Guid.NewGuid()).ToURL(),
            uzanti);

        string path = server.MapPath(string.Format("{0}{1}", klasor, dosyaAdi));
        fu.SaveAs(path);
    }


    /// <summary>
    /// File upload nesnesindeki resmi yükler. Yüklenen dosyanın adını DosyaAdı property si ile öğrenebilirsiniz.
    /// </summary>
    /// <param name="maxBoyut">kb cinsinden max dosya boyutu</param>
    /// <param name="maxWidth">Resmi otomatik ölçeklemek için kullanılıyor</param>
    /// <param name="maxHeight">Resmi otomatik ölçeklemek için kullanılıyor</param>
    public void ResimYukle(int maxBoyut, int maxWidth, int maxHeight)
    {
        if (!fu.HasFile)
            throw new Exception("Bir resim dosyası seçmelisiniz!");

        string[] gecerliUzantilar = { "jpg", "gif", "jpeg", "png" };
        string fuDosya = fu.FileName;
        fuDosya = fuDosya.Replace(";", "");
        int poz = fuDosya.LastIndexOf(".");
        string uzanti = fuDosya.Substring(poz + 1);

        bool uzantiOK = false;
        foreach (string s in gecerliUzantilar)
        {
            if (s.Equals(uzanti.ToLower())) uzantiOK = true;
        }
        if (!uzantiOK)
        {
            throw new Exception("Lütfen geçerli bir resim dosyası seçiniz.");
        }
        if (fu.FileBytes.LongLength > maxBoyut * 1024)
        {
            throw new Exception(string.Format("{0}kb'den daha büyük bir resmi yükleyemezsiniz.",
                maxBoyut));
        }
        DosyaAdi = string.Format("{0}.{1}",
            string.Format("{1}_{0}", Guid.NewGuid(), fuDosya.Substring(0, poz)).ToURL(),
            uzanti);

        string path = server.MapPath(string.Format("{0}{1}", klasor, dosyaAdi));
        fu.SaveAs(path);
        bool degisti;
        using (Bitmap yeniResim = CreateThumbnail(path, out degisti, maxWidth, maxHeight))
        {
            if (degisti)
                yeniResim.Save(path);
        }
    }

    /// <summary>
    /// Resmi thumbnail oluşturarak yükler.
    /// Yüklenen dosyanın adını DosyaAdı property si ile,
    /// Thumbnail dosyasının adını ThumbnailDosyaAdi property si ile öğrenebilirsiniz.
    /// </summary>
    /// <param name="maxBoyut">kb cinsinden max dosya boyutu</param>
    /// <param name="maxWidth">resmin max genişliği</param>
    /// <param name="maxHeight">resmin max yüksekliği</param>
    /// <param name="thumbMaxWidth">thumbnailin max genişliği</param>
    /// <param name="thumbMaxHeight">thumbnailin max yüksekliği</param>
    public void ResimYukle(int maxBoyut, int maxWidth, int maxHeight, int thumbMaxWidth, int thumbMaxHeight)
    {
        if (!fu.HasFile)
            throw new Exception("Bir resim dosyası seçmelisiniz!");

        string[] gecerliUzantilar = { "jpg", "gif", "jpeg", "png" };
        string fuDosya = fu.FileName;
        fuDosya = fuDosya.Replace(";", "");
        int poz = fuDosya.LastIndexOf(".");
        string uzanti = fuDosya.Substring(poz + 1);

        bool uzantiOK = false;
        foreach (string s in gecerliUzantilar)
        {
            if (s.Equals(uzanti.ToLower())) uzantiOK = true;
        }
        if (!uzantiOK)
        {
            throw new Exception("Lütfen geçerli bir resim dosyası seçiniz.");
        }
        if (fu.FileBytes.LongLength > maxBoyut * 1024)
        {
            throw new Exception(string.Format("{0}kb'den daha büyük bir resmi yükleyemezsiniz.",
                maxBoyut));
        }
        DosyaAdi = string.Format("{0}.{1}",
            string.Format("{1}_{0}", Guid.NewGuid(), fuDosya.Substring(0, poz)).ToURL(),
            uzanti);

        string path = server.MapPath(string.Format("{0}{1}", klasor, dosyaAdi));
        fu.SaveAs(path);

        bool degisti;
        using (Bitmap yeniResim = CreateThumbnail(path, out degisti, maxWidth, maxHeight))
        {
            if (degisti)
                yeniResim.Save(path);
        }

        using (Bitmap yeniResim = CreateThumbnail(path, out degisti, thumbMaxWidth, thumbMaxHeight))
        {
            ThumbnailDosyaAdi = string.Format("{0}_th.{1}",
            string.Format("{1}_{0}", Guid.NewGuid(), fuDosya.Substring(0, poz)).ToURL(),
            uzanti);
            path = server.MapPath(string.Format("{0}{1}", klasor, ThumbnailDosyaAdi));
            yeniResim.Save(path);
        }
    }

    public void ResimYukle(AsyncFileUpload fu, int maxBoyut, int maxWidth, int maxHeight, int thumbMaxWidth, int thumbMaxHeight)
    {
        if (!fu.HasFile)
            throw new Exception("Bir resim dosyası seçmelisiniz!");

        string[] gecerliUzantilar = { "jpg", "gif", "jpeg", "png" };
        string fuDosya = fu.FileName;
        fuDosya = fuDosya.Replace(";", "");
        int poz = fuDosya.LastIndexOf(".");
        string uzanti = fuDosya.Substring(poz + 1);

        bool uzantiOK = false;
        foreach (string s in gecerliUzantilar)
        {
            if (s.Equals(uzanti.ToLower())) uzantiOK = true;
        }
        if (!uzantiOK)
        {
            throw new Exception("Lütfen geçerli bir resim dosyası seçiniz.");
        }
        if (fu.FileBytes.LongLength > maxBoyut * 1024)
        {
            throw new Exception(string.Format("{0}kb'den daha büyük bir resmi yükleyemezsiniz.",
                maxBoyut));
        }
        DosyaAdi = string.Format("{0}.{1}",
            string.Format("{1}_{0}", Guid.NewGuid(), fuDosya.Substring(0, poz)).ToURL(),
            uzanti);

        string path = server.MapPath(string.Format("{0}{1}", klasor, dosyaAdi));
        fu.SaveAs(path);

        bool degisti;
        using (Bitmap yeniResim = CreateThumbnail(path, out degisti, maxWidth, maxHeight))
        {
            if (degisti)
                yeniResim.Save(path);
        }

        using (Bitmap yeniResim = CreateThumbnail(path, out degisti, thumbMaxWidth, thumbMaxHeight))
        {
            ThumbnailDosyaAdi = string.Format("{0}_th.{1}",
            string.Format("{1}_{0}", Guid.NewGuid(), fuDosya.Substring(0, poz)).ToURL(),
            uzanti);
            path = server.MapPath(string.Format("{0}{1}", klasor, ThumbnailDosyaAdi));
            yeniResim.Save(path);
        }
    }


    protected Bitmap CreateThumbnail(string lcFilename, out bool degisti, int lnWidth, int lnHeight)
    {

        System.Drawing.Bitmap bmpOut = null;

        try
        {
            Bitmap loBMP = new Bitmap(lcFilename);
            ImageFormat loFormat = loBMP.RawFormat;

            decimal lnRatio;
            int lnNewWidth = 0;
            int lnNewHeight = 0;

            if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
            {
                degisti = false;
                return loBMP;
            }
            degisti = true;
            if (loBMP.Width > loBMP.Height)
            {
                lnRatio = (decimal)lnWidth / loBMP.Width;
                lnNewWidth = lnWidth;
                decimal lnTemp = loBMP.Height * lnRatio;
                lnNewHeight = (int)lnTemp;
            }
            else
            {
                lnRatio = (decimal)lnHeight / loBMP.Height;
                lnNewHeight = lnHeight;
                decimal lnTemp = loBMP.Width * lnRatio;
                lnNewWidth = (int)lnTemp;
            }


            bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
            Graphics g = Graphics.FromImage(bmpOut);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
            g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

            loBMP.Dispose();
        }
        catch
        {
            degisti = false;
            return null;
        }
        return bmpOut;
    }


    public void ResimTasi(string dosya, string yeniKlasor)
    {
        int poz = dosya.LastIndexOf("/");
        string dosyaAdi = dosya.Substring(poz + 1);
        if (!Directory.Exists(server.MapPath(yeniKlasor)))
            Directory.CreateDirectory(server.MapPath(yeniKlasor));
        File.Move(server.MapPath(dosya), server.MapPath(yeniKlasor + dosyaAdi));
        DosyaAdi = yeniKlasor + dosyaAdi;
    }

    public void ResimSil(string dosya)
    {
        if (File.Exists(server.MapPath(dosya)))
            File.Delete(server.MapPath(dosya));
    }
}
