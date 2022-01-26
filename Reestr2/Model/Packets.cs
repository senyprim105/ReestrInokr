using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;



namespace Reestr2.Model
{
    
    public class PacketAtr
    {
        public  string name;
        public  string type;
        public  int mo;
        public  int year;
        public  int month;
        public  int number;
        public List<string> errors=new List<string>();
        public PacketAtr() { }
        public PacketAtr(FileInfo file) : this(file.Name) { }
        public PacketAtr(string Name)
        {
            if (!Regex.IsMatch(Name.ToUpper(), Repo.PacketShablon)) throw new ArgumentException(@"Имя или расширение архива не соответствует шаблону:^(H|C|T|D\w)(\d{ 6})([89])(0[1-9]|1[0-2])(\d{3})(\.ZIP)$");
            var result = Regex.Match(Name.ToUpper(), Repo.PacketShablon);
            this.name = Name.ToUpper();
            this.type = result.Groups[1].Value;
            this.mo = int.Parse(result.Groups[2].Value);
            this.year = int.Parse("20" + result.Groups[3].Value);
            this.month = int.Parse(result.Groups[4].Value);
            this.number = int.Parse(result.Groups[6].Value);
        }
        public void AddErrors(string err) => errors.Add(err);
    }
  
    public class Packet:IEquatable<Packet>
    {
        [Key]
        public int PacketId { get; set; }
        [XmlIgnore,NotMapped]
        public PacketAtr PacketAtr { get
            {
                var pack = new PacketAtr();
                pack.errors = this.errors;
                pack.mo = this.mo;
                pack.month = this.month;
                pack.name = this.name;
                pack.number = this.number;
                pack.type = this.type;
                pack.year = this.year;
                return pack;
            }
            set
            {
                this.errors = value.errors;
                this.mo = value.mo;
                this.month = value.month;
                this.name = value.name;
                this.type = value.type;
                this.year = value.year;
                this.number = value.number;
            }
        }
        [XmlIgnore,Required]
        public  string fullname { get; set; }
        [XmlIgnore, Required,MaxLength(50)]
        [Index("IX_Packet", 1, IsUnique = true)]
        public  string name { get; set; }
        [XmlIgnore, Required, MaxLength(50)]
        [Index("IX_Packet", 2, IsUnique = true)]
        public string Md5 { get; private set; }
        [XmlIgnore, Required]
        public  string type { get; set; }
        [XmlIgnore, Required]
        public  int mo { get; set; }
        [XmlIgnore, Required]
        public int lpu { get; set; }
        [XmlIgnore, Required]
        public  int year { get; set; }
        [XmlIgnore, Required]
        public  int month { get; set; }
        [XmlIgnore, Required]
        public  int number { get; set; }
        [MaxLength(50),XmlIgnore]
        public string numberChet { get; set; }
        [XmlIgnore]
        [Column(TypeName = "datetime2")]
        public DateTime? dataChet { get; set; }
        [XmlIgnore]
        public double summChet { get; set; }
        [XmlIgnore]
        public List<string> errors { get; set; }
        [XmlIgnore]
        public virtual List<XmlReestr> XmlReestrs { get; set; } = new List<XmlReestr>();
        [Column(TypeName = "datetime2"), Required, XmlIgnore]
        public DateTime timeload { get; set; } = DateTime.Now;
        public int count { get; set; } = 0;
        public double MnAll { get; set; } = 0;

        
       
        public static List<string> GetErrPacket(string filename)
        {
            try
            {
                var PacketAtr = new PacketAtr(filename);
                if (!IsValidMO(PacketAtr.mo)) PacketAtr.AddErrors("Не верный код МО в пакете");
                if (!IsValidType(PacketAtr.type, PacketAtr.mo)) PacketAtr.AddErrors($"Тип пакета:{PacketAtr.type} не соответствует коду МО:{PacketAtr.mo}");
                if (PacketAtr.year < 2019 || PacketAtr.year > DateTime.Now.Year) PacketAtr.AddErrors("Год пакета не верный");
                if (PacketAtr.month < 1 || PacketAtr.month > 12) PacketAtr.AddErrors("Месяц пакета не верный");
                return PacketAtr.errors;
            }
            catch(ArgumentException e)
            {
                return new List<string> { $"Ошибка файла пакета:{e.Message}" };
            }
        }
  

        public Packet() { }
        public Packet(FileInfo filename)
        {
            this.PacketAtr  = new PacketAtr(filename);
            this.fullname = filename.FullName;
            if (!IsValidMO(this.PacketAtr.mo)) this.PacketAtr.AddErrors("Не верный код МО в пакете");
            if (!IsValidType(this.PacketAtr.type, this.PacketAtr.mo)) this.PacketAtr.AddErrors($"Тип пакета:{this.PacketAtr.type} не соответствует коду МО:{this.PacketAtr.mo}");
            if (this.PacketAtr.year < 2019 || this.PacketAtr.year > DateTime.Now.Year) this.PacketAtr.AddErrors("Год пакета не верный");
            if (this.PacketAtr.month < 1 || this.PacketAtr.month > 12) this.PacketAtr.AddErrors("Месяц пакета не верный");
            if (this.PacketAtr.errors == null || this.PacketAtr.errors.Count == 0)
            {
               Md5 = Repo.checkMD5(filename.FullName);
                using (ZipArchive archive = ZipFile.OpenRead(filename.FullName))
                {
                    var xmlfiles = archive.Entries.Select(a => a.Name.ToUpper()).ToArray();
                    if (IsValidXmlTypes(this.PacketAtr, xmlfiles))
                    {
                        var lpu = xmlfiles.Select(a => XmlReestr.GetLpu(a.ToUpper())).Distinct();
                        if (lpu.Count() == 1)
                        {
                            this.lpu = lpu.First();
                            this.timeload = DateTime.Now;
                            
                            XmlReestrs.AddRange(archive.Entries.Select(a => XmlLoad(a)));
                            this.count = this.XmlReestrs.Where(a => ("HCD".Contains(a.Type[0]))).
                                Select(a => (a.Type == "H" ? (a as HCode).ZGLV.SD_Z
                                          :  a.Type == "C" ? (a as CCode).ZGLV.SD_Z
                                                           : (a as DCode).ZGLV.SD_Z)).FirstOrDefault();
                            this.MnAll = (double)this.XmlReestrs.Where(a => ("HCD".Contains(a.Type[0]))).
                                Select(a => (a.Type == "H" ? (a as HCode).SCHET.SUMMAV
                                          : a.Type == "C" ? (a as CCode).SCHET.SUMMAV
                                                           : (a as DCode).SCHET.SUMMAV)).FirstOrDefault();
                            //this.count_errors=this.XmlReestrs.Where(a =>a.Type=="V").Cast<VERR_FLK_P>().SelectMany(a=>a.ZAP.Where(b=>b.SL.Where(a=>a.)
                            // this.Reestr = Repo.GetReestr(month, year, mo);//?? new Reestr(month, year, Repo.GetMo(mo));
                            // this.Reestr.PacketAdd(this);

                            return;
                        }
                    }
                }
            }

            
        }
        public void Validate(string nchet,DateTime dchet,double summChet)
        {
            this.numberChet = nchet;
            this.dataChet = dchet;
            this.summChet = summChet;
        }
        public void  Remove(reestr context=null)
        {
            using (var db= reestr.GetContext(context))
            {
                // this.Reestr.PacketDell(this);
                db.Packets.Attach(new Packet { PacketId = this.PacketId });
                db.Packets.Remove(this);
                db.SaveChanges();
            }
        }
        
       
        
       
        
        static bool IsValidXmlTypes(PacketAtr packet,string[] xmlnames)
        {
            if (xmlnames.All(a=>XmlReestr.IsValidReestr(packet,a)))
            {
                var availablexmltypes = Repo.TypeXml[packet.type].Select(a => string.Join("", a.Split(',').OrderBy(b => b))).ToArray();
                var xmltypes = string.Join("", xmlnames.Select(a => XmlReestr.GetType(a)).OrderBy(a => a));
                if (availablexmltypes.Any(a => a == xmltypes)) return true;
                packet.AddErrors($"Недопустимый набор типов файлов {string.Join(",", xmlnames)} для пакета {packet.name}");
            }
            return false;
        }

        private static XmlReestr GetXmlReestr(XmlReader reader,string type)
        {
            XmlReestr reestr = null;
            switch (type.ToUpper()[0])
            {
                case 'H':
                    reestr = ((HCode)new XmlSerializer(typeof(HCode)).Deserialize(reader));
                    break;
                case 'L':
                    reestr = ((LCode)new XmlSerializer(typeof(LCode)).Deserialize(reader));
                    break;
                case 'P':
                    reestr = ((PCode)new XmlSerializer(typeof(PCode)).Deserialize(reader));
                    break;
                case 'S':
                    reestr = ((SCode)new XmlSerializer(typeof(SCode)).Deserialize(reader));
                    break;
                case 'C':
                    reestr = ((CCode)new XmlSerializer(typeof(CCode)).Deserialize(reader));
                    break;
                case 'D':
                    reestr = ((DCode)new XmlSerializer(typeof(DCode)).Deserialize(reader));
                    break;
                case 'F':
                    reestr = ((FCode)new XmlSerializer(typeof(FCode)).Deserialize(reader));
                    break;
               
            }
            return reestr;
        }

        private static XmlReestr GetXmlReestr(Stream reader, string type, XmlReaderSettings settings) {
            using (XmlReader xmlreader = XmlReader.Create(reader, settings)) return GetXmlReestr(xmlreader, type);
        }
        private static XmlReestr GetXmlReestr(Stream reader, string type)
        {
            XmlReaderSettings setting = new XmlReaderSettings();
            setting.Async = true; setting.IgnoreComments = true;
            setting.IgnoreProcessingInstructions = true;
            setting.IgnoreWhitespace = true;
            setting.IgnoreWhitespace = true;
            using (XmlReader xmlreader=XmlReader.Create(reader,setting))
            return GetXmlReestr(xmlreader, type);
        }
        private static XmlReestr GetXmlReestr(FileStream reader) => GetXmlReestr(reader, Path.GetFileName(reader.Name));
        private static XmlReestr GetXmlReestr(FileStream reader,XmlReaderSettings settings) => GetXmlReestr(reader, Path.GetFileName(reader.Name),settings);
        private static XmlReestr GetXmlReestr(ZipArchiveEntry entry, XmlReaderSettings settings) =>GetXmlReestr(entry.Open(), Path.GetFileName(entry.Name), settings);
        private static XmlReestr GetXmlReestr(ZipArchiveEntry entry) => GetXmlReestr(entry.Open(), Path.GetFileName(entry.Name));

        private static XmlReestr XmlLoad(Stream stream,string type, XmlReaderSettings settings)
        {
            XmlReestr reestr =GetXmlReestr(stream, type,settings);
            reestr.FileName = type;
            reestr.LastWrite = DateTime.Now;
            reestr.FileSize = stream.Length;
            reestr.Type = XmlReestr.GetType(reestr.FileName);
            return reestr;
        }
        private static XmlReestr XmlLoad(FileStream stream,XmlReaderSettings settings=null)
        {
            XmlReestr reestr = settings == null ? GetXmlReestr(stream) : GetXmlReestr(stream, settings);
            reestr.FileName = Path.GetFileName(stream.Name);
            reestr.LastWrite = File.GetLastWriteTime(stream.Name);
            reestr.FileSize = stream.Length;
            reestr.Type = XmlReestr.GetType(reestr.FileName);
            return reestr;
        }
        private static XmlReestr XmlLoad(ZipArchiveEntry entry, XmlReaderSettings settings = null)
        {
            XmlReestr reestr = settings == null ? GetXmlReestr(entry) : GetXmlReestr(entry, settings);
            reestr.FileName = Path.GetFileName(entry.Name);
            reestr.LastWrite = entry.LastWriteTime.DateTime;
            reestr.FileSize = entry.Length;
            reestr.Type = XmlReestr.GetType(reestr.FileName)??"";
            return reestr;
        }

        public static Packet GetPacket(int PacketId,reestr context=null)
        {
            using (reestr db = reestr.GetContext(context)) return db.Packets.FirstOrDefault(a => a.PacketId == PacketId);
        }
        public static bool IsValidMO(int MoCod,reestr context=null) =>Repo.Mos.Any(a => a.MoCod== MoCod);
        public static bool IsValidType(string type, int mo) =>
            Repo.Mos.Single(a => a.MoCod == mo).Lpus.SelectMany(b => b.GetPacketsType()).Contains(type);

        public static int GetMO(string filename) => int.Parse(Regex.Match(filename, Repo.XmlShablon).Groups[2].Value);
        public static string GetType(string filename,string shablon=null) => Regex.Match(filename, shablon==null?Repo.XmlShablon:shablon).Groups[1].Value;
        public static int GetMonth(string filename) => int.Parse(Regex.Match(filename, Repo.XmlShablon).Groups[4].Value);
        public static int GetYear(string filename) => int.Parse(Regex.Match(filename, Repo.XmlShablon).Groups[3].Value);
        public static int GetNumber(string filename) => int.Parse(Regex.Match(filename, Repo.XmlShablon).Groups[5].Value);

        public bool Equals(Packet other)
        {
            if (other == null) return false;
            return (other.PacketId!=0&&this.PacketId==other.PacketId)||
                (this.name == other.name && this.Md5 == other.Md5);
                
        }


    }
}
