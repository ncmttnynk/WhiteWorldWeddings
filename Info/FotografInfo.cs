using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteWorld.Info
{
    public class FotografInfo
    {
        public int Id { get; set; }
        public int GelinlikId { get; set; }
        public string FotoKucuk { get; set; }
        public string FotoBuyuk { get; set; }
        public bool OnFoto { get; set; }
        public bool ArkaFoto { get; set; }
        public bool Anasayfa { get; set; }
        public string Etiket { get; set; }
    }
}
