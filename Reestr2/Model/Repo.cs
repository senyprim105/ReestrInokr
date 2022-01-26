using Reestr2.VisualModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Reestr2.Model
{
    static public class Repo
    {
        public static Dictionary<string, string[]> TypeXml = new Dictionary<string, string[]>
        {
        { "H",new string[]{ "H,L,P","H,L,S"} },
        { "C",new string[]{ "C,LC,PC","C,LC,SC"} },
        { "DP",new string[]{ "DP,FP,LP"} },
        { "DV",new string[]{ "DV,FV,LV"} },
        { "DO",new string[]{ "DO,FO,LO"} },
        { "DS",new string[]{ "DS,FS,LS"} },
        { "DU",new string[]{ "DU,FU,LU"} },
        { "DF",new string[]{ "DF,FF,LF"} }};

        public static List<Mo> Mos = new List<Mo>()
        {
                new Mo(250426,"НУЗ Узловая больница").AddLpu(426,"S,SC").AddLpu(441,"P,PC,DP,DV,DO"),
                new Mo(250433,"КГБУЗ Уссурийская стомотология").AddLpu(433,"P,PC"),
                new Mo(250443,"КГБУЗ Михайловская ЦРБ").AddLpu(443,"S,SC").AddLpu(447, "P,PC,DP,DV,DO,DS,DU,DF"),
                new Mo(250452,"КГБУЗ Октябрьская ЦРБ").AddLpu(452, "S,SC").AddLpu(454, "P,PC,DP,DV,DO,DS,DU,DF"),
                new Mo(250458,"КГБУЗ Пограничная ЦРБ").AddLpu(458, "S,SC").AddLpu(461, "P,PC,DP,DV,DO,DS,DU,DF"),
                new Mo(250473,"КГБУЗ Ханкайская ЦРБ").AddLpu(473,"S,SC").AddLpu(474,"P,PC,DP,DV,DO,DS,DU,DF"),
                new Mo(250477,"КГБУЗ Хорольская ЦРБ").AddLpu(477,"S,SC").AddLpu(480,"P,PC,DP,DV,DO,DS,DU,DF"),
                new Mo(250700,"КГБУЗ Уссурийская ЦГБ").AddLpu(700,"S,SC").AddLpu(704,"P,PC,DP,DV,DO,DS,DU,DF"),
                new Mo(250718,"КГБУЗ СМП г.Уссурийска").AddLpu(718, "P"),
                new Mo(250755,"ООО Клиника лечения боль").AddLpu(755, "P,PC"),
                new Mo(250777,"ООО Сфера Мед").AddLpu(777,"P"),
                new Mo(250775,"ФГКУ 439 ВГ МО РФ").AddLpu(775,"S")
        };
        
        public static string PacketShablon = @"^(DP|DV|DO|DS|DU|DF|H|C)M(\d{6})T25_(1[89])(0[1-9]|1[0-2])(\d{3})(\d+)\.ZIP$";

        public static string XmlShablon = @"^(LC|PC|SC|LT|ST|DP|DV|DO|DS|DU|DF|LP|LV|LO|LS|LU|LF|FP|FV|FO|FS|FU|FF|VP|VV|VO|VS|VU|VF|V|H|L|P|C|T|S)M(\d{6})T25_(1[89])(0[1-9]|1[0-2])(\d{3})(\d+)\.XML$";


        //Получить Мо по номеру лпу
        public static int GetMoOfLpu(int k_lpu, reestr context = null) =>
            Repo.Mos.Where(a => a.Lpus.Any(b => b.LpuCod == k_lpu)).SingleOrDefault().MoCod;

        //Получить пакет по Id
        public static Packet GetPacket(int PacketId, reestr context = null)
        {
            using (reestr dbcon = reestr.GetContext(context))
                return dbcon.Packets.SingleOrDefault(a => a.PacketId == PacketId);
        }
        //Получить пакет по имени пакета
        public static Packet GetPacket(string packetName,reestr context = null)
        {
            using (reestr dbcon = reestr.GetContext(context))
                return dbcon.Packets.SingleOrDefault(a => packetName.EndsWith(a.name.ToUpper()));
        }
        //Существует ли пакет по имени
        public static bool ContainsPacket(string packetName,reestr context = null)
        {
            using (reestr dbcon = reestr.GetContext(context))
                return dbcon.Packets.Any(a => packetName.EndsWith(a.name.ToUpper()));
        }
        //Существует ли пакет по Id
        public static bool ContainsPacket(int PacketId, reestr context = null)
        {
            using (reestr dbcon = reestr.GetContext(context))
                return dbcon.Packets.Any(a => a.PacketId==PacketId);
        }
        //Существует этот код МО
        public static bool ContainsMoCod(int moCod, reestr context = null)
        {
            return Repo.Mos.Any(a => a.MoCod == moCod);
        }

        //Номе строки и столбца ячейки в таблице с нужным содержимым
        public static Tuple<int, int> GetRowCol(DataTable table, string fieldValue)
        {
            for (int row = 0; row < table.Rows.Count; ++row)
                for (int col = 0; col < table.Columns.Count; ++col)
                    if (table.Rows[row][col].ToString() == fieldValue) return Tuple.Create(row, col);
            return Tuple.Create(-1, -1);
        }

        //Вернуть список данных по списку имен пакетов
        public static List<PacketAtr> GetPacketAtrs(List<string> listName) => listName.Select(name => new PacketAtr(name)).ToList();
        //Сгруппировать список данных
        public static PacketAtr DistinctPacketAtrs(List<PacketAtr> packetAtrs)
        {
            var first = packetAtrs[0];
            first.errors = null;

            foreach (var item in packetAtrs)
            {
                if (first.month != item.month) first.month = 0;
                if (first.year != item.year) first.year = 0;
                if (first.mo != item.mo) first.mo = 0;
                if (first.type != item.type) first.type = null;
            }
            return first;
        }
        //MD5
        public static string checkMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return Encoding.UTF8.GetString(md5.ComputeHash(stream));
                }
            }
        }


        public static List<PacketsLine> GetPacket(Func<PacketsLine, bool> filter = null, reestr context = null)
        {
            List<PacketsLine> result = new List<PacketsLine>();
            using (reestr db = reestr.GetContext(context)) result = db.Packets.
                    Select(a => new PacketsLine
                    {
                        PacketId = a.PacketId,
                        Mo = a.mo,
                        MoName = Repo.Mos.Where(b => b.MoCod == a.mo).Select(b => b.MoName).FirstOrDefault(),
                        Year = a.year,
                        Month = a.month,
                        LoadDate = a.timeload,
                        Name = a.name,
                        Count = a.count,
                        MnAll = a.MnAll
                    }).ToList<PacketsLine>();
            return result.Where(a => filter == null ? true : filter(a)).ToList();
        }

        //Выводит комбинации но не более 32 
        public static List<List<T>> Combaine<T>(List<T> input)
        {
            var result = new List<List<T>>();
            for (int i = 1; i < (int)Math.Pow(2, input.Count); i++)
            {
                var bit = i;
                var count = 0;
                List<T> item = new List<T>();
                while (bit > 0)
                {
                    if ((bit & 1) == 1) item.Add(input[count]);
                    count++;
                    bit >>= 1;
                }
                result.Add(item);
            }
            return result;
        }
        
        public static bool Validate(XDocument xdoc, int PacketId, reestr context = null)
        {
            //Выбор из xml элементы
            

            using (var db = reestr.GetContext(context))
            {
                var currentPac = db.Packets.SingleOrDefault(a => a.PacketId == PacketId);
                var moCod = currentPac.mo;
                var month = currentPac.month;
                var year = currentPac.year;


                var chets = xdoc.Root
                    .Descendants("line")
                    .Where(line =>
                    line.Element("mo_cod").Value == moCod.ToString() &&
                    line.Element("month").Value == month.ToString() &&
                    line.Element("year").Value == year.ToString()
                    )
                    .Select(line => new
                    {
                        nchet = line.Element("nchet").Value,
                        dchet = DateTime.Parse(line.Element("dchet").Value),
                        summ = (int)Math.Round(double.Parse(line.Element("summ").Value) * 100)
                    }).ToList();

                //Выбор пакетов
                var packets = db.Packets.Where(pac => pac.mo == moCod && pac.month == month && pac.year == year)
                    .Select(pac => new
                    {
                        pac.PacketId,
                        MnAll = (int)Math.Round(pac.MnAll * 100)
                    })
                    .ToList();

                var combine = Combaine(packets).Select(a => new { summ = a.Sum(b => b.MnAll), packets = a.Select(b => b.PacketId).ToArray() }).ToList();

                var inner = combine.Join(chets,
                    comb => comb.summ,
                    ch => ch.summ,
                    (comb, ch) => new { nchet = ch.nchet, dchet = ch.dchet, summ = ch.summ / 100.0, packets = comb.packets })
                    .ToList();

                var error = inner.GroupBy(a => a.summ).Where(a => a.Count() > 1).ToList();
                if (error.Any()) throw new ArgumentException($"Ошибка определения пакета для счета {string.Join(",", error.Select(a => a.Key).ToArray())}");
                

                
                foreach(var chet in inner)
                {
                    var updatepak = db.Packets.Where(a => chet.packets.Contains(a.PacketId)).ToList();
                        updatepak.ForEach(a => { a.numberChet = chet.nchet;a.dataChet = chet.dchet;a.summChet = chet.summ; });
                }
               
                
                db.SaveChanges();
            }
            return false;
        }
    }


}
