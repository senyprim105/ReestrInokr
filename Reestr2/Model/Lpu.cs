using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reestr2.Model
{
    public class Lpu
    {
        public virtual Mo Mo { get; set; }
        public int LpuCod { get; set; }
        public string AvailableType { get; set; }
        public DateTime D1 { get; set; }
        public DateTime D2 { get; set; }

        public Lpu(int lpu, string packets, DateTime d1=default(DateTime), DateTime d2=default(DateTime), Mo mo=null)
        {
            this.Mo = mo; this.LpuCod = lpu;this.AvailableType = packets;
            this.D1 = d1==default(DateTime)?DateTime.MinValue:d1;
            this.D2 = d2==default(DateTime)?DateTime.MaxValue:d2;
        }
        private Lpu() { }

        public string[] GetPacketsType()
        {
            return this.AvailableType?.Split(',').Select(a => (new string[] { "H", "P", "S" }.Contains(a)) ? "H" : (new string[] { "C", "LC", "PC", "SC" }.Contains(a) )? "C" : (a.StartsWith("D") )? a.Substring(0,2) : null).Distinct().ToArray();
        }

        

    }
   
   
}



