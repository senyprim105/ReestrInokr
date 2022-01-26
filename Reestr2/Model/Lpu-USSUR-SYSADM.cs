using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reestr2.Model
{
    public class Lpu
    {
        public int  URMO { get; set; }
        public int K_LPU { get; set; }
        public string IK_LPU { get; set; }
        public string[] AvailableType { get; set; }
        public DateTime D1 { get; set; }
        public DateTime D2 { get; set; }
        public Lpu(int ur,int lpu,string ik_lpu,string packets,DateTime d1,DateTime d2)
        {
            this.URMO = ur;this.K_LPU = lpu; IK_LPU = ik_lpu; AvailableType = packets.Split(',');this.D1 = d1;this.D2 = d2;
        }
        public string[] GetPacketType()
        {
            return AvailableType.Select(a => new string[] { "H", "P", "S" }.Contains(a) ? "H" : new string[] { "C", "LC", "PC", "SC" }.Contains(a) ? "C" : a.StartsWith("D") ? a.Substring(0,2):null).Distinct().ToArray();
        }
        static public List<Lpu> Lpus = new List<Lpu>()
        { new Lpu(250426, 426, "НУЗ Узловая больница", "S,CS", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
          new Lpu(250426, 441, "НУЗ Узловая больница", "P,CP,DP,DV,DO", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250433, 433, "КГБУЗ Уссурийская стомотология", "P,CP", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250443, 443, "КГБУЗ Михайловская ЦРБ", "S,CS", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250443, 447, "КГБУЗ Михайловская ЦРБ", "P,CP,DP,DV,DO,DS,DU,DF", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250452, 452, "КГБУЗ Октябрьская ЦРБ", "S,CS", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250452, 454, "КГБУЗ Октябрьская ЦРБ", "P,CP,DP,DV,DO,DS,DU,DF", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250458, 458, "КГБУЗ Пограничная ЦРБ", "S,CS", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250458, 461, "КГБУЗ Пограничная ЦРБ", "P,CP,DP,DV,DO,DS,DU,DF", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250473, 473, "КГБУЗ Ханкайская ЦРБ", "S,CS", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250473, 474, "КГБУЗ Ханкайская ЦРБ", "P,CP,DP,DV,DO,DS,DU,DF", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250477, 477, "КГБУЗ Хорольская ЦРБ", "S,CS", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250477, 480, "КГБУЗ Хорольская ЦРБ", "P,CP,DP,DV,DO,DS,DU,DF", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250700, 700, "КГБУЗ Уссурийская ЦГБ", "S,CS", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250700, 704, "КГБУЗ Уссурийская ЦГБ", "P,CP,DP,DV,DO,DS,DU,DF", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
            new Lpu(250718, 718, "КГБУЗ СМП г.Уссурийска", "P", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250755, 755, "ООО Клиника лечения боль", "P,CP", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250777, 777, "ООО Сфера Мед", "P", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")),
                new Lpu(250775, 775, "ФГКУ 439 ВГ МО РФ", "S", DateTime.Parse("1901-01-01"), DateTime.Parse("2025-01-01")) };
                
            }
        }


