using Reestr2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reestr2.VisualModel
{
    public class VisualReestrLine
    {
        [Display(Name ="Np")]
        public long? Npp { get; set; }
        //[Display(Name = "SL_ID")]
        //public string Idcase { get; set; }
        [Display(Name = "Fio")]
        public string Fio { get; set; }
        [Display(Name = "Pol")]
        public string Pol { get; set; }
        [Display(Name = "Dr")]
        public DateTime? DateBirth { get; set; }
        [Display(Name = "Place")]
        public string PlaceBirth { get; set; }
        [Display(Name = "Doc")]
        public string Document { get; set; }
        [Display(Name = "Snils")]
        public string Snils { get; set; }
        [Display(Name = "Polis")]
        public string Polis { get; set; }
        [Display(Name = "Vidpom")]
        public int? VidPom { get; set; }
        [Display(Name = "Ds")]
        public string DS { get; set; }
        [Display(Name = "Date_In")]
        public DateTime? DateIn { get; set; }
        [Display(Name = "Date_out")]
        public DateTime? DateOut { get; set; }
        [Display(Name = "Vol")]
        public double? Volume { get; set; }
        [Display(Name = "Profil")]
        public int? Profil { get; set; }
        [Display(Name = "Prvs")]
        public int? Prvs { get; set; }
        [Display(Name = "Tarif")]
        public double? Tariff { get; set; }
        [Display(Name = "Money")]
        public double? Money { get; set; }
        [Display(Name = "Result")]
        public int? Result { get; set; }
        public string[] GetArray()
        {
            //return this.GetType().GetProperties()
            //    .Where(p => p.GetCustomAttribute<DisplayAttribute>() != null)
            //    .Select(p => p.GetValue(this)==null?string.Empty:p.GetValue(this).ToString())
            //    .ToArray();
            return new string[]{
                Npp?.ToString()??"",/*Idcase?.ToString()??"",*/Fio??"",Pol??"",DateBirth?.ToString("d")??"",PlaceBirth??"",Document??"",Snils??"",
                Polis??"",VidPom?.ToString()??"",DS??"",DateIn?.ToString("d")??"",DateOut?.ToString("d")??"",Volume?.ToString()??"",
                Profil?.ToString()??"",Prvs?.ToString()??"",Tariff?.ToString()??"",Money?.ToString()??"",Result?.ToString()??""};
        }
        public string[] GetHeader()
        {
            return this.GetType().GetProperties()
                .Where(p => p.GetCustomAttribute<DisplayAttribute>() != null)
                .Select(p => $"#{p.GetCustomAttribute<DisplayAttribute>().Name.ToUpper()}").ToArray();
        }
    }
    public class VisualReestr
    {
        public List<VisualReestrLine> Lines { get; set; }
        [Display(Name = "#MO_COD")]
        public int? MoCod { get; set; }
        [Display(Name = "#MO_NAME")]
        public string MoName { get; set; }
        [Display(Name = "#D_MIN")]
        public DateTime? DateIn { get; set; }
        [Display(Name = "#D_MAX")]
        public DateTime? DateOut { get; set; }
        [Display(Name = "#NCHET")]
        public string NChet { get; set; }
        [Display(Name = "#DCHET")]
        public DateTime? DChet { get; set; }
        public int Month { get; set; }
        [Display(Name = "#MN_ALL")]
        public double? MnAll { get; set; }
        public Dictionary<string,string> GetAttrReestr()
        {
            return new Dictionary<string, string>()
            {
                {"#MO_COD",MoCod?.ToString()??"" },
                {"#MO_NAME",MoName??"" },
                {"#D_MIN",DateIn?.ToString("d")??"" },
                {"#D_MAX",DateOut?.ToString("d")??"" },
                {"#MN_ALL",MnAll?.ToString()??""},
                {"#NCHET",NChet??"" },
                {"#DCHET",DChet?.ToString("d")??"" }
            };
        }
        public VisualReestr(int PacketId,reestr db=null)
        {
            using (reestr dbc = reestr.GetContext(db))
            {
                var packet = dbc.Packets.SingleOrDefault(a => a.PacketId == PacketId);
                if (packet.type[0] == 'D') this.Lines=GetLineFromDReestr(PacketId, dbc);
                else if (packet.type[0] == 'H') this.Lines=GetLineFromHReestr(PacketId, dbc);
                else if (packet.type[0] == 'C') this.Lines=GetLineFromCReestr(PacketId, dbc);
                this.MoCod = packet.mo;
                this.MoName = Repo.Mos.SingleOrDefault(a => a.MoCod == this.MoCod).MoName;
                this.Month = packet.month;
                this.DateIn = this.Lines.Min(a => a.DateIn);
                this.DateOut = this.Lines.Max(a => a.DateOut);
                this.MnAll = this.Lines.Sum(a => a.Money);
                this.DChet = packet.dataChet;
                this.NChet = packet.numberChet;
            }

        }

        public static void FillPersData(VisualReestrLine line,LFile_PERS itemL, bool Novor=true)
        {
            if (Novor)
            {
                line.Fio = $"{itemL.FAM} {itemL.IM} {itemL.OT}";
                line.DateBirth = itemL.DR;
                line.Pol = itemL.W == 1 ? "М" : "Ж";
            }
            else
            {
                line.Fio = $"{itemL.FAM_P} {itemL.IM_P} {itemL.OT_P}".Trim();
                line.DateBirth = itemL.DR_P;
                line.Pol = itemL.W_P == 1 ? "М" : "Ж";
            }
            line.Document = $"{itemL.DOCSER ?? ""} {itemL.DOCNUM ?? ""}".Trim();
            line.PlaceBirth = itemL.MR??"";
            line.Snils = itemL.SNILS;
            
        }

        public static List<VisualReestrLine> GetLineFromHReestr(int PacketId, reestr context = null)
        {
            HCode mainreestr = null;
            LCode lreestr = null;
            var result = new List<VisualReestrLine>();
            using (reestr db = reestr.GetContext(context))
            {
                mainreestr = db.Packets?.Where(a => a.PacketId == PacketId).FirstOrDefault()?.XmlReestrs.FirstOrDefault(a => a.Type == "H") as HCode;
                lreestr = db.Packets?.Where(a => a.PacketId == PacketId).FirstOrDefault()?.XmlReestrs.FirstOrDefault(a => a.Type[0] == 'L') as LCode;
                foreach (var item in mainreestr.ZAP.SelectMany(a => a.Z_SL.SL))
                {
                    VisualReestrLine line = new VisualReestrLine();
                    FillPersData(line, lreestr?.PERS_List?.Where(a => a.ID_PAC == item.Z_SL.ZAP.PACIENT?.ID_PAC).FirstOrDefault(), item.Z_SL.ZAP.PACIENT.NOVOR == "0");
                    line.Npp = item.Z_SL.IDCASE;
                    //line.Idcase = item.SL_ID;
                    line.Polis = $"{item.Z_SL.ZAP.PACIENT.SPOLIS ?? ""} {item.Z_SL.ZAP.PACIENT.NPOLIS ?? ""}".Trim();
                    line.VidPom = item.Z_SL.VIDPOM;
                    line.Profil = item.PROFIL;
                    line.DS = item.DS1;
                    line.DateIn = item.DATE_1;
                    line.DateOut = item.DATE_2;
                    line.Prvs = item.PRVS;
                    line.Result = item.Z_SL.RSLT;
                    line.Tariff = (double)item.TARIF;
                    line.Money = (double)item.SUM_M;
                    line.Volume = 1;
                    result.Add(line);
                }
                return result;
            }
        }
        public static List<VisualReestrLine> GetLineFromCReestr(int PacketId, reestr context = null)
        {
            CCode mainreestr = null;
            LCode lreestr = null;
            var result = new List<VisualReestrLine>();
            using (reestr db = reestr.GetContext(context))
            {
                mainreestr = db.Packets?.Where(a => a.PacketId == PacketId).FirstOrDefault()?.XmlReestrs.FirstOrDefault(a => a.Type == "C") as CCode;
                lreestr = db.Packets?.Where(a => a.PacketId == PacketId).FirstOrDefault()?.XmlReestrs.FirstOrDefault(a => a.Type[0] == 'L') as LCode;
                foreach (var item in mainreestr.ZAP.SelectMany(a => a.Z_SL.SL))
                {
                    VisualReestrLine line = new VisualReestrLine();
                    FillPersData(line, lreestr?.PERS_List?.Where(a => a.ID_PAC == item.Z_SL.ZAP.PACIENT?.ID_PAC).FirstOrDefault(), item.Z_SL.ZAP.PACIENT.NOVOR == "0");
                    line.Npp = item.Z_SL.IDCASE;
                    //line.Idcase = item.SL_ID;
                    line.Polis = $"{item.Z_SL.ZAP.PACIENT.SPOLIS ?? ""} {item.Z_SL.ZAP.PACIENT.NPOLIS ?? ""}".Trim();
                    line.VidPom = item.Z_SL.VIDPOM;
                    line.Profil = item.PROFIL;
                    line.DS = item.DS1;
                    line.DateIn = item.DATE_1;
                    line.DateOut = item.DATE_2;
                    line.Prvs = item.PRVS;
                    line.Result = item.Z_SL.RSLT;
                    line.Tariff = (double)item.TARIF;
                    line.Money = (double)item.SUM_M;
                    line.Volume = 1;
                    result.Add(line);
                }
                return result;
            }
        }
        public static List<VisualReestrLine> GetLineFromDReestr(int PacketId, reestr context = null)
        {
            DCode mainreestr = null;
            LCode lreestr = null;
            var result = new List<VisualReestrLine>();
            using (reestr db = reestr.GetContext(context))
            {
                mainreestr = db.Packets?.Where(a => a.PacketId == PacketId).FirstOrDefault()?.XmlReestrs.FirstOrDefault(a => a.Type[0] == 'D') as DCode;
                lreestr = db.Packets?.Where(a => a.PacketId == PacketId).FirstOrDefault()?.XmlReestrs.FirstOrDefault(a => a.Type[0] == 'L') as LCode;
                foreach (var item in mainreestr.ZAP.SelectMany(a => a.Z_SL.SL))
                {
                    VisualReestrLine line = new VisualReestrLine();
                    FillPersData(line, lreestr?.PERS_List?.Where(a => a.ID_PAC == item.Z_SL.ZAP.PACIENT?.ID_PAC).FirstOrDefault(), item.Z_SL.ZAP.PACIENT.NOVOR == "0");
                    line.Npp = item.Z_SL.IDCASE;
                    //line.Idcase = item.SL_ID;
                    line.Polis = $"{item.Z_SL.ZAP.PACIENT.SPOLIS ?? ""} {item.Z_SL.ZAP.PACIENT.NPOLIS ?? ""}".Trim();
                    line.VidPom = item.Z_SL.VIDPOM;
                    line.Profil = new string[] { "DP", "DV", "DO" }.Contains(mainreestr.Type) ? 97 : 68;
                    line.DS = item.DS1;
                    line.DateIn = item.DATE_1;
                    line.DateOut = item.DATE_2;
                    line.Prvs = line.Profil==97?76:49;
                    line.Result = item.Z_SL.RSLT_D;
                    line.Tariff = (double)item.TARIF;
                    line.Money = (double)item.SUM_M;
                    line.Volume = 1;
                    result.Add(line);
                }
                return result;
            }
        }

    }
}
