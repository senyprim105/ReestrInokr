using System;
using System.Collections.Generic;
using System.Linq;

namespace Reestr2.VisualModel
{
    public class PacketsLine
    {
        public int PacketId { get; set; }
        public int Mo { get; set; }
        public string MoName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime LoadDate { get; set; }
        public string Name { get; set; }
        public int Num { get; set; }
        public int Count { get; set; }
        public double MnAll { get; set; }
        public string Type { get; set; }
        public string NChet { get; set; }
        public DateTime? DChet { get; set; }
        public double? SummChet { get; set; } 
    }
    

}