<%@ WebHandler Language="C#" Class="filemanager_config" %>

using System;
using System.Web;

public class filemanager_config : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string uploadKlasoru = "/upload/";
        
        if (context.Request["Klasor"] != null)
            uploadKlasoru = context.Request["Klasor"].ToString();
        
        context.Response.Clear();
        context.Response.ClearContent();
        context.Response.ClearHeaders();
        context.Response.ContentType = "application/x-javascript";


        context.Response.Write(string.Format(
        @"
/*---------------------------------------------------------
  Configuration
---------------------------------------------------------*/

// Set culture to display localized messages
var culture = 'tr';

// Set default view mode : 'grid' or 'list'
var defaultViewMode = 'grid';

// Autoload text in GUI
// If set to false, set values manually into the HTML file
var autoload = true;

// Display full path - default : false
var showFullPath = false;

// Browse only - default : false
var browseOnly = false;

// Set this to the server side language you wish to use.
var lang = 'ashx'; // options: php, jsp, lasso, asp, cfm, ashx, asp // we are looking for contributors for lasso, python connectors (partially developed)

var am = document.location.pathname.substring(1, document.location.pathname.lastIndexOf('/') + 1);

// Set this to the directory you wish to manage.
//var fileRoot = '/' + am + 'upload/';

var fileRoot = '{0}';

//Path to the manage directory on the HTTP server
var relPath = window.location.protocol + '//' + document.domain;

// Show image previews in grid views?
var showThumbs = true;

// Allowed image extensions when type is 'image'
var imagesExt = ['jpg', 'jpeg', 'gif', 'png'];

// Allowed file extensions when type is 'file'
var filesExt = [ 'doc','docx','fla','flv','gif','jpeg','jpg','mp3',
                 'mp4','mpeg','mpg','pdf','png','ppt','pptx','rar',
                 'swf','txt','wav','wma','wmv','xls','xlsx', 'xml','zip'];
", uploadKlasoru));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}