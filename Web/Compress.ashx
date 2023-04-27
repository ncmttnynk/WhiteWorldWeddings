<%@ WebHandler Language="C#" Class="YuiCompressor" %>

// Requirements: 
// bin/Yahoo.Yui.Compressor.dll (See the project on codeplex)
// bin/EcmaScript.NET.dll
// bin/EcmaScript.NET.dll
// bin/Iesi.Collections.dll
//
// How to use this, in your templates or HTML:
//
// For CSS:
// <link rel="stylesheet" type="text/css" href="/YuiCompressor.ashx?css=reset,style" />
//
// For JS:
// <script type="text/javascript" src="/YuiCompressor.ashx?js=main,someotherscript"></script>

using System;
using System.Collections.Generic;
using System.Web;
using Yahoo.Yui.Compressor;

public class YuiCompressor : IHttpHandler
{
    public const char DELIMITER = ',';
    public const bool ENABLEHTTPCOMPRESSION = true;
    public TimeSpan CacheDuration = TimeSpan.FromDays(10);

    public class ContentTypes
    {
        public const string CSS = "text/css";
        public const string JS = "application/x-javascript";
        public const string ERROR = "text/plain";
    }

    public class Folders
    {
        // customize these to your site's paths
        public const string CSS = "~/css/";
        public const string JS = "~/js/";
    }

    public class Extensions
    {
        public const string CSS = ".css";
        public const string JS = ".js";
    }

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentEncoding = System.Text.Encoding.Default;

        context.Response.Cache.SetExpires(DateTime.Now.Add(CacheDuration));
        context.Response.Cache.SetMaxAge(CacheDuration);
        context.Response.Cache.SetCacheability(HttpCacheability.Private);

        string cssFiles = context.Request.QueryString["css"];
        string jsFiles = context.Request.QueryString["js"];
        string fileName = context.Request.QueryString["f"];

        if (!string.IsNullOrEmpty(cssFiles))
        {
            // A list of CSS files has been passed in, write each file's contents to the response object
            context.Response.ContentType = ContentTypes.CSS;
            foreach (string cssFile in cssFiles.Split(DELIMITER))
            {
                WriteCompressedFile(context, cssFile, Folders.CSS);
            }
        }
        else if (!string.IsNullOrEmpty(jsFiles))
        {
            // A list of JS files has been passed in, write each file's contents to the response object
            context.Response.ContentType = ContentTypes.JS;
            foreach (string jsFile in jsFiles.Split(DELIMITER))
            {
                WriteCompressedFile(context, jsFile, Folders.JS);
            }
        }
        else if (!string.IsNullOrEmpty(fileName))
        {
            // A specific file has been passed in, write that file's contents to the response object
            if (fileName.EndsWith(Extensions.JS))
            {
                context.Response.ContentType = ContentTypes.JS;
                WriteCompressedFile(context, fileName, Folders.JS);
            }
            else if (fileName.EndsWith(Extensions.CSS))
            {
                context.Response.ContentType = ContentTypes.CSS;
                WriteCompressedFile(context, fileName, Folders.CSS);
            }
            else
            {
                // 500?
                //Throw New System.IO.FileNotFoundException("The file specified isn't an allowed type.", fileName)
                context.Response.ContentType = ContentTypes.ERROR;
            }
        }
        else
        {
            // 404?
            //Throw New System.IO.FileNotFoundException("A filename hasn't been specified.")
            context.Response.ContentType = ContentTypes.ERROR;
            context.Response.Write(Environment.NewLine);
        }

        // compress output if enabled
        if (ENABLEHTTPCOMPRESSION)
        {
            string encodingsAccepted = context.Request.Headers["Accept-Encoding"];
            if (!string.IsNullOrEmpty(encodingsAccepted))
            {
                encodingsAccepted = encodingsAccepted.ToLowerInvariant();
                if (encodingsAccepted.Contains("gzip"))
                {
                    context.Response.AppendHeader("Content-Encoding", "gzip");
                    context.Response.Filter = new System.IO.Compression.GZipStream(context.Response.Filter, System.IO.Compression.CompressionMode.Compress);
                }
                else if (encodingsAccepted.Contains("deflate"))
                {
                    context.Response.AppendHeader("Content-Encoding", "deflate");
                    context.Response.Filter = new System.IO.Compression.DeflateStream(context.Response.Filter, System.IO.Compression.CompressionMode.Compress);
                }
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public void WriteCompressedFile(HttpContext context, string fileName, string folder)
    {
        // Add each file's compressed contents to the cache as it is read with a file dependency on itself
        if (context.Cache[fileName] == null)
        {
            string filePath = HttpContext.Current.Server.MapPath(fileName);
            string output = string.Empty;
            try
            {
                output = System.IO.File.ReadAllText(filePath);
                if (folder == Folders.JS)
                {
                    JavaScriptCompressor javascriptCompressor = new JavaScriptCompressor();
                    output = javascriptCompressor.Compress(output) + Environment.NewLine;
                }
                else
                {
                    CssCompressor cssCompressor = new CssCompressor();
                    output = cssCompressor.Compress(output) + Environment.NewLine;
                }
                context.Response.Write(output);
                context.Cache.Insert(fileName, output, new System.Web.Caching.CacheDependency(filePath),
                    System.Web.Caching.Cache.NoAbsoluteExpiration, CacheDuration);
            }
            catch (System.IO.FileNotFoundException)
            {
                // throw 404?
            }
        }
        else
        {
            context.Response.Write((string)context.Cache[fileName]);
        }
    }
}