<%@ WebHandler Language="C#" Class="JpegImage" %>

using System;
using System.Web;
using DevrimAltinkurt;
public class JpegImage : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        // session daki bilgi ile resmi oluşturuyoruz
        DogrulamaKodu ci =
            new DogrulamaKodu(context.Session["CaptchaMetin"].ToString(), 200, 40);

        context.Response.Clear();
        context.Response.ContentType = "image/jpeg";

        ci.Image.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        ci.Dispose();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}