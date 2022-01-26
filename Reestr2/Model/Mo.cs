using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reestr2.Model
{
    public class Mo
    {
        public virtual List<Lpu> Lpus { get; set; }
        public int MoCod { get; set; }
        public string MoName { get; set; }

        public Mo(int moCod,string moName){
            MoCod = moCod;
            MoName = moName;
            Lpus = new List<Lpu>();
        }
        public Mo() {
            Lpus = new List<Lpu>();
        }

        public Mo AddLpu(Lpu lpu){lpu.Mo = this;Lpus.Add(lpu);return this;}
        public Mo AddLpu(int klpu, string typePackets) => AddLpu(new Lpu(klpu, typePackets));
    }
}
