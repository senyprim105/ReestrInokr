using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Reestr2.Model
{
    [XmlRoot("ZL_LIST")]
    public class HCode : XmlReestr
    {
     //   [Key, XmlIgnore]
     //   public int Id { get; set; }
        [Required]
        public virtual HFile_ZGLV ZGLV { get; set; }
        [Required]
        public virtual HFile_SCHET SCHET { get; set; }
        [Required, XmlElement(typeof(HFile_ZAP), ElementName = "ZAP")]
        public virtual List<HFile_ZAP> ZAP { get; set; }
     //   [Column(TypeName = "datetime2"), Required, XmlIgnore]
     //   public DateTime LastWrite { get; set; }
     //   [Required, XmlIgnore]
     //   public long FileSize { get; set; }
    }

    public class HFile_ZGLV
    {
        [Key]
        [ForeignKey("H"), XmlIgnore]
        public int ReestrId { get; set; }
        [MaxLength(5), Required]
        public string VERSION { get; set; }
        [Column(TypeName = "datetime2"), Required, XmlElement(DataType = "date")]
        public DateTime DATA { get; set; }
        [MaxLength(26), Required]
        public string FILENAME { get; set; }
        [Required]
        public int SD_Z { get; set; }
        [XmlIgnore,Required]
        public virtual HCode H { get; set; }
    }

    public class HFile_SCHET
    {
        [Key]
        [ForeignKey("H"), XmlIgnore]
        public int ReestrId { get; set; }
        [Required]
        public int CODE { get; set; }
        [MaxLength(6), Required]
        public string CODE_MO { get; set; }
        [Required]
        public int YEAR { get; set; }
        [Required]
        public int MONTH { get; set; }
        [MaxLength(15), Required]
        public string NSCHET { get; set; }
        [Column(TypeName = "datetime2"), Required, XmlElement(DataType = "date")]
        public DateTime DSCHET { get; set; }
        [MaxLength(5)]
        public string PLAT { get; set; }
        [DataType("decimal(15,2)")]
        public decimal SUMMAV { get; set; }
        [MaxLength(255)]
        public string COMENTS { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? SUMMAP { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? SANK_MEK { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? SANK_MEE { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? SANK_EKMP { get; set; }
        [XmlIgnore,Required]
        public virtual HCode H { get; set; }
        public bool ShouldSerializeSANK_EKMP() { return SANK_EKMP.HasValue; }
        public bool ShouldSerializeSUMMAP() { return SUMMAP.HasValue; }
        public bool ShouldSerializeSANK_MEK() { return SANK_MEK.HasValue; }
        public bool ShouldSerializeSANK_MEE() { return SANK_MEE.HasValue; }
    }

    public class HFile_ZAP
    {
        [Key, XmlIgnore]
        public int ZapId { get; set; }
        [ForeignKey("H"), XmlIgnore,Required]
        public int ReestrId { get; set; }
        [Required]
        public int N_ZAP { get; set; }
        [Required]
        public int PR_NOV { get; set; }
        [Required, XmlElement(typeof(HFile_PACIENT), ElementName = "PACIENT")]
        public virtual HFile_PACIENT PACIENT { get; set; }
        [Required, XmlElement(typeof(HFile_Z_SL), ElementName = "Z_SL")]
        public virtual HFile_Z_SL Z_SL { get; set; }
        [XmlIgnore,Required]
        public virtual HCode H { get; set; }
    }

    public class HFile_PACIENT
    {
        [Key]
        [ForeignKey("ZAP"), XmlIgnore,Required]
        public int ZapId { get; set; }
        [MaxLength(36), Required]
        public string ID_PAC { get; set; }
        [Required]
        public int VPOLIS { get; set; }
        [MaxLength(10)]
        public string SPOLIS { get; set; }
        [MaxLength(20)]
        public string NPOLIS { get; set; }
        [MaxLength(5)]
        public string ST_OKATO { get; set; }
        [MaxLength(5)]
        public string SMO { get; set; }
        [MaxLength(15)]
        public string SMO_OGRN { get; set; }
        [MaxLength(5)]
        public string SMO_OK { get; set; }
        [MaxLength(100)]
        public string SMO_NAM { get; set; }
        public int? INV { get; set; }
        public int? MSE { get; set; }
        [MaxLength(9), Required]
        public string NOVOR { get; set; }
        public int? VNOV_D { get; set; }
        [XmlIgnore,Required]
        public virtual HFile_ZAP ZAP { get; set; }
        public bool ShouldSerializeINV() { return INV.HasValue; }
        public bool ShouldSerializeMSE() { return MSE.HasValue; }
        public bool ShouldSerializeVNOV_D() { return VNOV_D.HasValue; }
    }

    public class HFile_Z_SL
    {
        [Key]
        [ForeignKey("ZAP"), XmlIgnore,Required]
        public int ZapId { get; set; }
        [Required]
        public long IDCASE { get; set; }
        [Required]
        public int USL_OK { get; set; }
        [Required]
        public int VIDPOM { get; set; }
        [Required]
        public int FOR_POM { get; set; }
        [MaxLength(6)]
        public string NPR_MO { get; set; }
        [Column(TypeName = "datetime2"), XmlElement(DataType = "date")]
        public DateTime? NPR_DATE { get; set; }
        [MaxLength(6), Required]
        public string LPU { get; set; }
        [Column(TypeName = "datetime2"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_Z_1 { get; set; }
        [Column(TypeName = "datetime2"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_Z_2 { get; set; }
        public int? KD_Z { get; set; }
        [XmlElement(typeof(HFile_VNOV_M), ElementName = "VNOV_M")]
        public virtual List<HFile_VNOV_M> VNOV_M { get; set; }
        [Required]
        public int RSLT { get; set; }
        [Required]
        public int ISHOD { get; set; }
        [XmlElement(typeof(HFile_OS_SLUCH), ElementName = "OS_SLUCH")]
        public virtual List<HFile_OS_SLUCH> OS_SLUCH { get; set; }
        public int? VB_P { get; set; }
        [Required, XmlElement(typeof(HFile_SL), ElementName = "SL")]
        public virtual List<HFile_SL> SL { get; set; }
        [Required]
        public int IDSP { get; set; }
        [DataType("decimal(15,2)")]
        public decimal SUMV { get; set; }
        public int? OPLATA { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? SUMP { get; set; }
        [XmlElement(typeof(HFile_SANK), ElementName = "SANK")]
        public virtual List<HFile_SANK> SANK { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? SANK_IT { get; set; }
        [XmlIgnore,Required]
        public virtual HFile_ZAP ZAP { get; set; }
        public bool ShouldSerializeNPR_DATE() { return NPR_DATE.HasValue; }
        public bool ShouldSerializeKD_Z() { return KD_Z.HasValue; }
        public bool ShouldSerializeVB_P() { return VB_P.HasValue; }
        public bool ShouldSerializeOPLATA() { return OPLATA.HasValue; }
        public bool ShouldSerializeSUMP() { return SUMP.HasValue; }
        public bool ShouldSerializeSANK_IT() { return SANK_IT.HasValue; }
    }

    public class HFile_SL
    {
        [Key, XmlIgnore]
        public int SlId { get; set; }
        [ForeignKey("Z_SL"), XmlIgnore,Required]
        public int ZapId { get; set; }
        [Required]
        public string SL_ID { get; set; }
        [MaxLength(8)]
        public string LPU_1 { get; set; }
        public long? PODR { get; set; }
        [Required]
        public int PROFIL { get; set; }
        public int? PROFIL_K { get; set; }
        [Required]
        public int DET { get; set; }
        [MaxLength(3)]
        public string P_CEL { get; set; }
        [MaxLength(50)]
        public string NHISTORY { get; set; }
        public int? P_PER { get; set; }
        [Column(TypeName = "datetime2"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_1 { get; set; }
        [Column(TypeName = "datetime2"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_2 { get; set; }
        public int? KD { get; set; }
        [MaxLength(10)]
        public string DS0 { get; set; }
        [MaxLength(10)]
        public string DS1 { get; set; }
        [XmlElement(typeof(HFile_DS2), ElementName = "DS2")]
        public virtual List<HFile_DS2> DS2 { get; set; }
        [XmlElement(typeof(HFile_DS3), ElementName = "DS3")]
        public virtual List<HFile_DS3> DS3 { get; set; }
        public int? C_ZAB { get; set; }
        public int? DN { get; set; }
        [XmlElement(typeof(HFile_CODE_MES1), ElementName = "CODE_MES1")]
        public virtual List<HFile_CODE_MES1> CODE_MES1 { get; set; }
        [MaxLength(20)]
        public string CODE_MES2 { get; set; }
        [XmlElement(typeof(HFile_KSG_KPG), ElementName = "KSG_KPG")]
        public virtual HFile_KSG_KPG KSG_KPG { get; set; }
        public int? REAB { get; set; }
        [XmlIgnore]
        public int? PRVS { get; set; }
        [XmlElement("PRVS"),NotMapped]
        public string PrvsAsText
        {
            get { return (PRVS.HasValue) ? PRVS.ToString() : null; }
            set { PRVS = !string.IsNullOrEmpty(value) ? int.Parse(value) : default(int?); }
        }
        [Required, MaxLength(4)]
        public string VERS_SPEC { get; set; }
        [MaxLength(25), Required]
        public string IDDOKT { get; set; }
        [DataType("decimal(5,2)")]
        public decimal? ED_COL { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? TARIF { get; set; }
        [DataType("decimal(15,2)"), Required]
        public decimal SUM_M { get; set; }
        [XmlElement(typeof(HFile_USL), ElementName = "USL")]
        public virtual List<HFile_USL> USL { get; set; }
        [MaxLength(250)]
        public string COMENTSL { get; set; }
        [XmlIgnore,Required]
        public virtual HFile_Z_SL Z_SL { get; set; }
        public bool ShouldSerializePODR() { return PODR.HasValue; }
        public bool ShouldSerializePROFIL_K() { return PROFIL_K.HasValue; }
        public bool ShouldSerializeP_PER() { return P_PER.HasValue; }
        public bool ShouldSerializeKD() { return KD.HasValue; }
        public bool ShouldSerializeC_ZAB() { return C_ZAB.HasValue; }
        public bool ShouldSerializeDN() { return DN.HasValue; }
        public bool ShouldSerializeREAB() { return REAB.HasValue; }
        public bool ShouldSerializeED_COL() { return ED_COL.HasValue; }
        public bool ShouldSerializeTARIF() { return TARIF.HasValue; }
    }

    public class HFile_SANK
    {
        [Key, XmlIgnore]
        public int SankId { get; set; }
        [ForeignKey("Z_SL"), XmlIgnore,Required]
        public int ZapId { get; set; }
        [Required]
        public string S_CODE { get; set; }
        [DataType("decimal(15,2)"), Required]
        public decimal S_SUM { get; set; }
        [Required]
        public int S_TIP { get; set; }
        [XmlElement(typeof(HFile_SL_ID), ElementName = "SL_ID")]//Добавлен
        public virtual List<HFile_SL_ID> SL_ID { get; set; }
        public int? S_OSN { get; set; }
        [Column(TypeName = "datetime2"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_ACT { get; set; }
        [MaxLength(30), Required]
        public string NUM_ACT { get; set; }
        [MaxLength(8)]
        [XmlElement(typeof(HFile_CODE_EXP), ElementName = "CODE_EXP")]
        public virtual List<HFile_CODE_EXP> CODE_EXP { get; set; }
        [MaxLength(250)]
        public string S_COM { get; set; }
        [Required]
        public int S_IST { get; set; }
        [XmlIgnore,Required]
        public virtual HFile_Z_SL Z_SL { get; set; }
        public bool ShouldSerializeS_OSN() { return S_OSN.HasValue; }
    }

    public class HFile_USL
    {
        [Key, XmlIgnore]
        public int UslId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        public string IDSERV { get; set; }
        public string LPU { get; set; }
        public string LPU_1 { get; set; }
        public long? PODR { get; set; }
        public int PROFIL { get; set; }
        public string VID_VME { get; set; }
        public int DET { get; set; }
        [Column(TypeName = "datetime2"), XmlElement(DataType = "date")]
        public DateTime DATE_IN { get; set; }
        [Column(TypeName = "datetime2"), XmlElement(DataType = "date")]
        public DateTime DATE_OUT { get; set; }
        public string DS { get; set; }
        public string CODE_USL { get; set; }
        public decimal KOL_USL { get; set; }
        public decimal? TARIF { get; set; }
        public decimal SUMV_USL { get; set; }
        public int PRVS { get; set; }
        public string CODE_MD { get; set; }
        public int? NPL { get; set; }
        public string COMENTU { get; set; }
        [XmlIgnore]
        public virtual HFile_SL SL { get; set; }
        public bool ShouldSerializePODR() { return PODR.HasValue; }
        public bool ShouldSerializeTARIF() { return TARIF.HasValue; }
        public bool ShouldSerializeNPL() { return NPL.HasValue; }
    }

    public class HFile_KSG_KPG
    {
        [Key]
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(20), Required]
        public string N_KSG { get; set; }
        [Required]
        public int VER_KSG { get; set; }
        [Required]
        public int KSG_PG { get; set; }
        [MaxLength(4)]
        public string N_KPG { get; set; }
        [DataType("decimal(2,5)"), Required]
        public decimal KOEF_Z { get; set; }
        [DataType("decimal(2,5)"), Required]
        public decimal KOEF_UP { get; set; }
        [DataType("decimal(6,2)"), Required]
        public decimal BZTSZ { get; set; }
        [DataType("decimal(2,5)"), Required]
        public decimal KOEF_D { get; set; }
        [DataType("decimal(2,5)"), Required]
        public decimal KOEF_U { get; set; }
        // public string DKK1 { get; set; } Переименован на CRIT 
        [XmlElement(typeof(HFile_CRIT), ElementName = "CRIT")]
        public virtual List<HFile_CRIT> CRIT{ get; set; }
        [Required]
        public int SL_K { get; set; }
        [DataType("decimal(1,5)")]
        public decimal? IT_SL { get; set; }
        [XmlElement(typeof(HFile_SL_KOEF), ElementName = "SL_KOEF")]
        public virtual List<HFile_SL_KOEF> SL_KOEF { get; set; }
        [XmlIgnore,Required]
        public virtual HFile_SL SL { get; set; }
        //public bool ShouldSerializeN_KPG() { return N_KPG.HasValue; } Изменен тип на стринг
        public bool ShouldSerializeIT_SL() { return IT_SL.HasValue; }
    }

    public class HFile_SL_KOEF
    {
        [Key, XmlIgnore]
        public int Sl_KoefId { get; set; }
        [ForeignKey("KSG_KPG"), XmlIgnore]
        public int SlId { get; set; }
        [Required]
        public int IDSL { get; set; }
        [DataType("decimal(1,5)"), Required]
        public decimal Z_SL { get; set; }
        [XmlIgnore]
        public virtual HFile_KSG_KPG KSG_KPG { get; set; }
    }

    public class HFile_VNOV_M
    {
        [XmlIgnore,Key]
        public int Vnov_MId { get; set; }
        [ForeignKey("Z_SL"), XmlIgnore]
        public int ZapId { get; set; }
        [XmlText(typeof(int))]
        public int VNOV_M { get; set; }
        [XmlIgnore]
        public virtual HFile_Z_SL Z_SL { get; set; }
    }

    public class HFile_OS_SLUCH
    {
        [XmlIgnore,Key]
        public int Os_SluchId { get; set; }
        [ForeignKey("Z_SL"), XmlIgnore]
        public int ZapId { get; set; }
        [XmlText(typeof(int))]
        public int OS_SLUCH { get; set; }
        [XmlIgnore]
        public virtual HFile_Z_SL Z_SL { get; set; }
    }

    public class HFile_DS2
    {
        [XmlIgnore,Key]
        public int Ds2Id { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(10), XmlText(typeof(string))]
        public string DS2 { get; set; }
        [XmlIgnore]
        public virtual HFile_SL SL { get; set; }
    }

    public class HFile_DS3
    {
        [XmlIgnore,Key]
        public int Ds3Id { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(10), XmlText(typeof(string))]
        public string DS3 { get; set; }
        [XmlIgnore]
        public virtual HFile_SL SL { get; set; }
    }

    public class HFile_CODE_MES1
    {
        [XmlIgnore,Key]
        public int Code_Mes1Id { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(20), XmlText(typeof(string))]
        public string CODE_MES1 { get; set; }
        [XmlIgnore]
        public virtual HFile_SL SL { get; set; }
    }

    public class HFile_CODE_EXP
    {
        [XmlIgnore,Key]
        public int Code_ExpId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(8), XmlText(typeof(string))]
        public string CODE_EXP { get; set; }
        [XmlIgnore]
        public virtual HFile_SL SL { get; set; }
    }

    public class HFile_CRIT
    {
        [XmlIgnore, Key]
        public int CritId { get; set; }
        [ForeignKey("KSG_KPG"), XmlIgnore,Required]
        public int SlId { get; set; }
        [MaxLength(10), XmlText(typeof(string)),Required]
        public string CRIT { get; set; }
        [XmlIgnore,Required]
        public virtual HFile_KSG_KPG KSG_KPG { get; set; }
    }
    public class HFile_SL_ID
    {
        [XmlIgnore, Key]
        public int Sl_Id_Id { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(36), XmlText(typeof(string))]
        public string SL_ID { get; set; }
        [XmlIgnore]
        public virtual HFile_SL SL { get; set; }
    }
}
