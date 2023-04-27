using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteWorld.Info
{
    public class TekSayfaInfo
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Kod { get; set; }
        public string Fotograf{ get; set; }
        public string Icerik { get; set; }
        public DateTime Tarih { get; set; }
        public int Oncelik { get; set; }
        public bool Goster { get; set; }
        public int DilKod { get; set; }
    }
}
