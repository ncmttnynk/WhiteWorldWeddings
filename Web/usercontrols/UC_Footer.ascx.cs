using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usercontrols_UC_Footer : IUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SabitlerDB sDB = new SabitlerDB();

            ltlFooterSol.Text = (Dil.Equals("tr") ? sDB.TurkceIcerikGetir(Enums.Sabitler.FooterSol) : sDB.IngilizceIcerikGetir(Enums.Sabitler.FooterSol));
            ltlFooterSag.Text = (Dil.Equals("tr") ? sDB.TurkceIcerikGetir(Enums.Sabitler.FooterSag) : sDB.IngilizceIcerikGetir(Enums.Sabitler.FooterSag));
            ltlFooterOrta.Text = (Dil.Equals("tr") ? sDB.TurkceIcerikGetir(Enums.Sabitler.FooterOrta) : sDB.IngilizceIcerikGetir(Enums.Sabitler.FooterOrta));
        }
    }
}