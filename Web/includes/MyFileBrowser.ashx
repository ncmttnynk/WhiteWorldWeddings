<%@ WebHandler Language="C#" Class="MyFileBrowser" %>

using System;
using System.Web;

public class MyFileBrowser : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string uploadKlasoru = "/upload/";
        string fileTypes = "files";

        if (context.Request["Klasor"] != null)
            uploadKlasoru = context.Request["Klasor"].ToString();

        if (context.Request["DosyaTipi"] != null)
            fileTypes = context.Request["DosyaTipi"].ToString();

        context.Response.Clear();
        context.Response.ClearContent();
        context.Response.ClearHeaders();
        context.Response.ContentType = "application/x-javascript";

        context.Response.Write(string.Format(
@"
var urlobj;

function BrowseServer(obj) {{
    urlobj = obj;

    OpenServerBrowser(
""/ckeditor/plugins/simogeo/Browser.aspx?type={1}&Klasor={0}"",
screen.width * 0.7,
screen.height * 0.7);
}}

function OpenServerBrowser(url, width, height) {{
    var iLeft = (screen.width - width) / 2;
    var iTop = (screen.height - height) / 2;

    var sOptions = ""toolbar=no,status=no,resizable=yes,dependent=yes"";
    sOptions += "",width="" + width;
    sOptions += "",height="" + height;
    sOptions += "",left="" + iLeft;
    sOptions += "",top="" + iTop;

    var oWindow = window.open(url, ""BrowseWindow"", sOptions);
}}

function SetUrl(url, width, height, alt) {{
    document.getElementById(urlobj).value = url;
    oWindow = null;
}}
    ", uploadKlasoru,fileTypes));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}