using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorev_Yoneticisi.Models
{
    public class YapilanIsler
    {
        public int Id { get; set; }
        public string Aciklama { get; set; }
        public string DetayliPlanlama { get; set; }
        public bool Durum { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}