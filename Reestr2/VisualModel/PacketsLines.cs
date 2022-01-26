using Reestr2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reestr2.VisualModel
{
    public class PacketsLines
    {
        public int CurentMo { get; set; }
        public int CurentMonth { get; set; }
        public List<PacketsLine> DataSource
        {
            get
            {
                return GetPacketsLines(CurentMo, CurentMonth);
            }
            private set { }
        }


        public static List<PacketsLine> GetPacketsLines(int mo, int month, reestr db = null)
        {
            using (var dbcon = reestr.GetContext(db))
            {
                var packets = dbcon.Packets?.Where(a => a.mo == (mo == 0 ? a.mo : mo) && a.month == (month == 0 ? a.month : month))?.ToList();
                return packets.Select(a => new PacketsLine
                {
                    PacketId = a.PacketId,
                    Mo = a.mo,
                    MoName = Repo.Mos.SingleOrDefault(b => b.MoCod == a.mo).MoName,
                    Year = a.year,
                    Month = a.month,
                    LoadDate = a.timeload,
                    Name = a.name,
                    Count = a.count,
                    MnAll = a.MnAll,
                    Num = a.number,
                    Type=a.type,
                    NChet=a.numberChet,
                    DChet=a.dataChet,
                    SummChet=a.summChet
                   
                }).ToList();
            }
        }
    }

}
