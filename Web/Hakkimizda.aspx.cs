using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DA.MetaTagGenerator;

public partial class Hakkimizda : IPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SabitlerDB sDB = new SabitlerDB();
            ltlHakkimizda.Text = (DilKod == 1) ? sDB.TurkceIcerikGetir(Enums.Sabitler.Hakkimizda) : sDB.IngilizceIcerikGetir(Enums.Sabitler.Hakkimizda);
            MetaTagGenerator gen = MetaTagGenerator.CreateInstance(this);
            gen.MetaTags = new Tags.MetaTagsInfo
            {
                Description = "White World | Hakkımızda",
                Keywords = "white, world, hakkımızda, whiteworld hakkımızda",
                Language = Dil,
                Title = "Hakkımızda | White World",
                Topic = "Özel Dikim Gelinlik",
            };
            gen.GenerateTags();
        }
    }
}