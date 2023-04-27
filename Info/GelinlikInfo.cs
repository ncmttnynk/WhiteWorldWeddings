using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteWorld.Info
{
    public class GelinlikInfo
    {
        public int Id { get; set; }
        public string Kategori { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string OnFoto { get; set; }
        public string OnFotoEtiket { get; set; }
        public string ArkaFoto { get; set; }
        public string ArkaFotoEtiket { get; set; }
        public int Sezon { get; set; }
        public int Renk { get; set; }
        public int Kumas { get; set; }
        public int YakaTipi { get; set; }
        public int Siluet { get; set; }
        public int Kesim { get; set; }
        public int Beden { get; set; }
        public bool YeniSezon { get; set; }
        public bool OzelUrun { get; set; }
        public bool EnCokSatan { get; set; }
        public bool Yeni { get; set; }
        public int Oncelik { get; set; }
        public bool Goster { get; set; }
        public int DilKod { get; set; }
    }
}
