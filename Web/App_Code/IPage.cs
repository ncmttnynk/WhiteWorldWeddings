using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using DA.MetaTagGenerator;

public class IPage : System.Web.UI.Page
{
    public IMP_Child MP_Child
    {
        get
        {
            return this.Master as IMP_Child;
        }
    }
    public IMP_Main MP_Main

    {
        get
        {
            return this.Master.Master as IMP_Main;
        }
    }

    bool isZipSupported = true;

    public void ZipEncodePage()
    {
        if (isZipSupported)
        {
            HttpResponse response = HttpContext.Current.Response;
            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
            if (AcceptEncoding.Contains("gzip"))
            {
                response.Filter = new System.IO.Compression.GZipStream(
                                response.Filter, System.IO.Compression.CompressionMode.Compress);
                response.AppendHeader("Content-Encoding", "gzip");
            }
            else if (AcceptEncoding.Contains("deflate"))
            {
                response.Filter = new System.IO.Compression.DeflateStream(
                                    response.Filter, System.IO.Compression.CompressionMode.Compress);
                response.AppendHeader("Content-Encoding", "deflate");
            }
        }
    }

    private bool zipli = true;

    protected bool ViewstateZip
    {
        get { return zipli; }
        set { zipli = value; }
    }

    protected override void SavePageStateToPersistenceMedium(object state)
    {
        if (!zipli)
        {
            base.SavePageStateToPersistenceMedium(state);
            return;
        }
        LosFormatter formatter = new LosFormatter();
        StringWriter writer = new StringWriter();
        formatter.Serialize(writer, state);
        string viewState = writer.ToString();
        byte[] data = Convert.FromBase64String(viewState);
        byte[] compressedData = ViewstateZipProcessor.Compress(data);
        string str = Convert.ToBase64String(compressedData);
        //ClientScript.RegisterHiddenField("__CompressedVIEWSTATE", str);
        ScriptManager.RegisterHiddenField(this, "__CompressedVIEWSTATE", str);
    }

    protected override object LoadPageStateFromPersistenceMedium()
    {
        if (!zipli)
        {
            return base.LoadPageStateFromPersistenceMedium();
        }
        string viewstate = Request.Form["__CompressedVIEWSTATE"];
        byte[] data = Convert.FromBase64String(viewstate);
        byte[] uncompressedData = ViewstateZipProcessor.Decompress(data);
        string str = Convert.ToBase64String(uncompressedData);
        LosFormatter formatter = new LosFormatter();
        return formatter.Deserialize(str);
    }

    string[] diller = { "tr", "ar" };
    public string Dil { get; set; }
    public int DilKod
    {
        get
        {
            if (Dil.Equals("tr")) return 1;
            else if (Dil.Equals("ar")) return 2;
            return 1;
        }
    }
    void SayfaKontrol()
    {
        if (Request.RawUrl.Contains("admin")) return;
        try
        {
            string par = Request.RawUrl.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];

            if (!diller.Contains(par))
                throw new Exception();
        }
        catch
        {
            if (Session["Dil"] == null) Session["Dil"] = "tr";
            Dil = Session["Dil"].ToString();
            string rawUrl = Request.RawUrl;
            if (rawUrl.EndsWith("?")) rawUrl = rawUrl.Remove(rawUrl.Length - 1, 1);
            string url = string.Format("http://{0}/{1}{2}",
                                Request.Url.Authority,
                                Dil,
                                rawUrl);
            Response.Redirect(url, true);
        }
    }

    protected override void InitializeCulture()
    {
        SayfaKontrol();
        try
        {
            string par = Request.RawUrl.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            if (par.Equals("tr")) Session["Dil"] = "tr";
            else if (par.Equals("ar")) Session["Dil"] = "ar";
        }
        catch { }

        if (Session["Dil"] != null)
        {
            Dil = Session["Dil"].ToString();
        }
        else
        {
            Dil = "";
            try
            {
                string[] languages = HttpContext.Current.Request.UserLanguages;
                //if (languages == null) return;
                //if (languages.Length == 0) return;

                string language = languages[0].ToLowerInvariant().Trim();
                CultureInfo dil = CultureInfo.CreateSpecificCulture(language);
                Dil = dil.Name;
                if (!diller.Contains(Dil)) Dil = "tr";
            }
            catch
            {
                Dil = "tr";
            }
            Session["Dil"] = Dil;
        }
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Dil);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Dil);
        base.InitializeCulture();
    }

    public IPage()
    {
        Dil = "tr";
    }
}
