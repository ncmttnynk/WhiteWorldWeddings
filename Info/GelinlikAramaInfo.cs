using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WhiteWorld.Info
{
    public class GelinlikAramaInfo
    {
        public int Kumas { get; set; }
        public int Renk { get; set; }
        public int Siluet { get; set; }
        public int YakaTipi { get; set; }
        public int Beden { get; set; }
        public int Tip { get; set; }
        public int Kesim { get; set; }
        public int Sezon { get; set; }
        public GelinlikAramaInfo()
        {
            Kumas = 0;
            Renk = 0;
            Siluet = 0;
            YakaTipi = 0;
            Beden = 0;
            Tip = 0;
            Kesim = 0;
            Sezon = 0;
        }

        public void FromQueryString(string s)
        {
            var s2 = new List<string>(s.Split('|'));

            int sira = s2.IndexOf("kumas");
            if (sira > -1 && sira % 2 == 1) this.Kumas = Convert.ToInt32(HttpContext.Current.Server.UrlDecode(s2[sira + 1]));

            sira = s2.IndexOf("renk");
            if (sira > -1 && sira % 2 == 1) this.Renk = Convert.ToInt32(s2[sira + 1]);

            sira = s2.IndexOf("siluet");
            if (sira > -1 && sira % 2 == 1) this.Siluet = Convert.ToInt32(s2[sira + 1]);

            sira = s2.IndexOf("yakatipi");
            if (sira > -1 && sira % 2 == 1) this.YakaTipi = Convert.ToInt32(s2[sira + 1]);

            sira = s2.IndexOf("beden");
            if (sira > -1 && sira % 2 == 1) this.Beden = Convert.ToInt16(s2[sira + 1]);

            sira = s2.IndexOf("tip");
            if (sira > -1 && sira % 2 == 1) this.Tip = Convert.ToInt16(s2[sira + 1]);

            sira = s2.IndexOf("kesim");
            if (sira > -1 && sira % 2 == 1) this.Kesim = Convert.ToInt16(s2[sira + 1]);

            sira = s2.IndexOf("sezon");
            if (sira > -1 && sira % 2 == 1) this.Sezon = Convert.ToInt16(s2[sira + 1]);
        }

        public string ToQueryString()
        {
            string s = "";

            if (this.Kumas > 0) s += "|kumas|" + HttpContext.Current.Server.UrlEncode(this.Kumas.ToString());
            if (this.Renk > 0) s += "|renk|" + this.Renk;
            if (this.Siluet > 0) s += "|siluet|" + this.Siluet;
            if (this.YakaTipi > 0) s += "|yakatipi|" + this.YakaTipi;
            if (this.Beden > 0) s += "|beden|" + this.Beden;
            if (this.Tip > 0) s += "|tip|" + this.Tip;
            if (this.Kesim > 0) s += "|kesim|" + this.Kesim;
            if (this.Sezon > 0) s += "|sezon|" + this.Sezon;
            return s;
        }
    }
}
