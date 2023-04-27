using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;

/// <summary>
/// Summary description for MessageBox
/// </summary>
public class MessageBox
{
    public enum MesajTipleri
    {
        Error,
        Info,
        Warning,
        Success
    }

    public class MessageBoxInfo
    {
        MesajTipleri mesajTipi = MesajTipleri.Info;

        bool onDOMReady = false;
        bool otoKapa = true;
        int sure = 3500;
        string mesaj = "";

        public string Mesaj
        {
            get
            {
                mesaj = mesaj.Replace(Environment.NewLine, "<br/>");
                return mesaj;
            }
            set { mesaj = value; }
        }

        public int Sure
        {
            get { return sure; }
            set { sure = value; }
        }

        public bool OtoKapa
        {
            get { return otoKapa; }
            set { otoKapa = value; }
        }

        public MesajTipleri MesajTipi
        {
            get
            {
                return mesajTipi;
            }
            set { mesajTipi = value; }
        }

        public bool OnDOMReady
        {
            get { return onDOMReady; }
            set { onDOMReady = value; }
        }
    }

    public MessageBox()
    {
    }

    public static void Show(string mesaj, MesajTipleri mesajTipi)
    {
        MessageBoxInfo _mesaj = new MessageBoxInfo
        {
            Mesaj = mesaj,
            MesajTipi = mesajTipi
        };
        Show(_mesaj);
    }
    public static void Show(string mesaj, MesajTipleri mesajTipi, bool otoKapa)
    {
        MessageBoxInfo _mesaj = new MessageBoxInfo
        {
            Mesaj = mesaj,
            MesajTipi = mesajTipi,
            OtoKapa = otoKapa
        };
        Show(_mesaj);
    }
    public static void Show(string mesaj, MesajTipleri mesajTipi, bool otoKapa, int sure)
    {
        MessageBoxInfo _mesaj = new MessageBoxInfo
        {
            Mesaj = mesaj,
            MesajTipi = mesajTipi,
            OtoKapa = otoKapa,
            Sure = sure
        };
        Show(_mesaj);
    }
    public static void Show(string mesaj, MesajTipleri mesajTipi, int sure)
    {
        MessageBoxInfo _mesaj = new MessageBoxInfo
        {
            Mesaj = mesaj,
            MesajTipi = mesajTipi,
            Sure = sure
        };
        Show(_mesaj);
    }
    public static void Show(MessageBoxInfo mesaj)
    {
        string _mesaj = string.Format(
            @"notif({{
                msg: ""{0}"",
                type: ""{1}"",
                autohide: {2},
                timeout: {3},
                position: ""center"",
                width: ""all"",
                heigt: 100
            }});",
                             mesaj.Mesaj,
                             mesaj.MesajTipi.ToString().ToLower(CultureInfo.InvariantCulture),
                             mesaj.OtoKapa ? "true" : "false",
                             mesaj.Sure);

        Page page = HttpContext.Current.Handler as Page;
        bool async = false;
        try
        {
            async = ScriptManager.GetCurrent(page).IsInAsyncPostBack;
        }
        catch { }

        if (mesaj.OnDOMReady)
        {
            if (async)
            {
                ScriptManager.RegisterClientScriptBlock(page, page.GetType(), Guid.NewGuid().ToString(),
                            _mesaj, true);
            }
            else
            {
                ClientScriptManager csm = page.Page.ClientScript;
                csm.RegisterClientScriptBlock(page.GetType(), Guid.NewGuid().ToString(),
                           _mesaj, true);
            }
        }
        else
        {
            if (async)
            {
                ScriptManager.RegisterClientScriptBlock(page, page.GetType(), Guid.NewGuid().ToString(),
                        _mesaj, true);
            }
            else
            {
                ClientScriptManager csm = page.Page.ClientScript;
                csm.RegisterClientScriptBlock(page.GetType(), Guid.NewGuid().ToString(),
                    _mesaj, true);
            }
        }
    }
}