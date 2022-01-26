using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Reestr2.Model
{
    public abstract class XmlReestr
    {
        [Key,XmlIgnore]
        public int XmlReestrId { get; set;}
        [ForeignKey("Packet"),XmlIgnore,Required]
        public int PacketId { get; set; }
        [XmlIgnore,Required]
        public Packet Packet { get; set;}
        [XmlIgnore]
        public string FileName { get; set; }
        [XmlIgnore,Required]
        public string Type { get; set; }
        [Column(TypeName = "datetime2"), Required, XmlIgnore]
        public DateTime LastWrite { get; set; }
        [XmlIgnore,Required]
        public long FileSize { get; set; }

        public  XmlReestr() { }
        public static bool IsValidReestr(PacketAtr pack,string xmlname)
        {
            if (!IsValidName(xmlname)) { pack.AddErrors($"Имя файла xml:{xmlname} не соответствует шаблону"); return false; }
            
            if (GetYear(xmlname) != pack.year || GetMonth(xmlname) != pack.month || GetMo(xmlname) != pack.mo) pack.AddErrors($"Год, Месяц, МО пакета:{pack.name} не соответствуют Xml файлу:{xmlname}");
          //  if (!IsValidLpu(pack.mo, GetLpu(xmlname))) pack.AddErrors($"МО пакета:{pack.mo} не соответствуют ЛПУ Xml файла:{xmlname}");
            if (!IsValidXmlType(pack,xmlname)) pack.AddErrors($"Тип пакета:{pack.type} не соответствуют ЛПУ Xml файла:{xmlname}");
            if (pack.errors == null || pack.errors.Count == 0) return true;
            return false;
        }

        public static bool IsValidXmlType(PacketAtr pack, string xmlname) => Repo.TypeXml[pack.type].Any(a => a.IndexOf(GetType(xmlname)) >= 0);
       // public static bool IsValidLpu(int MO, int lpu) =>LpuList.Lpus.Any(a => a.URMO == MO && a.K_LPU == lpu); 
        public static bool IsValidName(string filename) => Regex.IsMatch(filename.ToUpper(), Repo.XmlShablon);
        public static string  GetType(string filename) => Regex.Match(filename.ToUpper(), Repo.XmlShablon).Groups[1].Value;
        public static int GetMo(string filename) => int.Parse(Regex.Match(filename.ToUpper(), Repo.XmlShablon).Groups[2].Value);
        public static int GetYear(string filename) => 2000+int.Parse(Regex.Match(filename.ToUpper(), Repo.XmlShablon).Groups[3].Value);
        public static int GetMonth(string filename) => int.Parse(Regex.Match(filename.ToUpper(), Repo.XmlShablon).Groups[4].Value);
        public static int GetLpu(string filename) => int.Parse(Regex.Match(filename.ToUpper(), Repo.XmlShablon).Groups[5].Value);
        public static int GetNumber(string filename) => int.Parse(Regex.Match(filename.ToUpper(), Repo.XmlShablon).Groups[6].Value);
        public static XmlReestr GetReestr(int ReestrId,reestr context=null)
        {
            using (reestr db = reestr.GetContext(context))
            {
                db.Configuration.ProxyCreationEnabled = true;
                db.Configuration.LazyLoadingEnabled = true;
                var type = db.XmlReestrs.Where(a => a.XmlReestrId == ReestrId).First().Type;
                XmlReestr result=null;
                switch (type[0])
                {
                    case 'H':result = (db.HFile.Where(a => a.XmlReestrId == ReestrId)
                       .Include("ZGLV")
                       .Include("SCHET")
                       .Include("ZAP.PACIENT")
                       .Include("ZAP.Z_SL.SL")
                       .Include("ZAP.Z_SL.SANK")
                       .Include("ZAP.Z_SL.VNOV_M")
                       .Include("ZAP.Z_SL.OS_SLUCH")
                       .Include("ZAP.Z_SL.SL.DS2")
                       .Include("ZAP.Z_SL.SL.DS3")
                       .Include("ZAP.Z_SL.SL.CODE_MES1")
                       .Include("ZAP.Z_SL.SL.KSG_KPG.CRIT")
                       .Include("ZAP.Z_SL.SL.KSG_KPG.SL_KOEF")
                       .Include("ZAP.Z_SL.SL.USL")
                       .First() as HCode);
                        break;
                    case 'L': result=(db.LFile.Where(a => a.XmlReestrId == ReestrId).Include("ZGLV")
                   .Include("PERS_List.DOSTS")
                   .Include("PERS_List.DOST_P")
                   .First() as LCode);
                        //result.PERS_List = result.PERS_List.OrderBy(a => a.ID_PAC).ToList();
                        break;
                    case 'C':result= (db.CFile.Where(a => a.XmlReestrId == ReestrId)
                       .Include("ZGLV")
                       .Include("SCHET")
                       .Include("ZAP.PACIENT")
                       .Include("ZAP.Z_SL.SL")
                       .Include("ZAP.Z_SL.SANK").Include("ZAP.Z_SL.SANK.SL_ID").Include("ZAP.Z_SL.SANK.CODE_EXP")
                       .Include("ZAP.Z_SL.VNOV_M")
                       .Include("ZAP.Z_SL.OS_SLUCH")
                       .Include("ZAP.Z_SL.SL.DS2")
                       .Include("ZAP.Z_SL.SL.DS3")
                       .Include("ZAP.Z_SL.SL.CODE_MES1")
                       .Include("ZAP.Z_SL.SL.NAPR")
                       .Include("ZAP.Z_SL.SL.CONS")
                       .Include("ZAP.Z_SL.SL.ONK_SL").Include("ZAP.Z_SL.SL.ONK_SL.B_DIAG").Include("ZAP.Z_SL.SL.ONK_SL.B_PROT")
                       .Include("ZAP.Z_SL.SL.ONK_SL.ONK_USL").Include("ZAP.Z_SL.SL.ONK_SL.ONK_USL.LEK_PR.DATE_INJ")
                       .Include("ZAP.Z_SL.SL.KSG_KPG").Include("ZAP.Z_SL.SL.KSG_KPG.CRIT").Include("ZAP.Z_SL.SL.KSG_KPG.SL_KOEF")
                       .Include("ZAP.Z_SL.SL.USL")
                       .Include("ZAP.Z_SL.SL.KSG_KPG.SL_KOEF")
                       .First() as CCode);
                        break;
                    case 'P':result= (db.PFile.Where(a => a.XmlReestrId == ReestrId)
                       .Include("ZGLV")
                       .Include("SCHET")
                       .Include("SL").Include("SL.NAPRLECH").Include("SL.USL").Include("SL.STOM").Include("SL.VIZOV")
                       .Include("SL.USL.RL")
                       .First() as PCode);
                        break;
                    case 'S':result = (db.SFile.Where(a => a.XmlReestrId == ReestrId)
                       .Include("ZGLV")
                       .Include("SCHET")
                       .Include("SL").Include("SL.DS1_M")
                       .Include("SL.USL").Include("SL.USL.RL")
                       .First() as SCode);
                        break;
                    case 'D':result = (db.DFile.Where(a => a.XmlReestrId == ReestrId)
                       .Include("ZGLV")
                       .Include("SCHET")
                       .Include("ZAP")
                       .Include("ZAP.PACIENT")
                       .Include("ZAP.Z_SL").Include("ZAP.Z_SL.OS_SLUCH")
                       .Include("ZAP.Z_SL.SL").Include("ZAP.Z_SL.SL.DS2_N").Include("ZAP.Z_SL.SL.NAZ")
                       .Include("ZAP.Z_SL.SANK").Include("ZAP.Z_SL.SANK.SL_ID").Include("ZAP.Z_SL.SANK.CODE_EXP")
                       .Include("ZAP.Z_SL.SL.USL")
                       .First() as DCode);
                        break;
                    case 'F':result = (db.FFile.Where(a => a.XmlReestrId == ReestrId)
                       .Include("ZGLV")
                       .Include("SCHET")
                       .Include("ZAP")
                       .Include("ZAP.SL")
                       .Include("ZAP.PACIENT")
                       .First() as FCode);
                        break;

                }
                return result;
            }
            
                
            
        }
    }
}