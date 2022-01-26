using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reestr2.Model
{
    [XmlRoot("ZL_LIST")]
    [Serializable]

    public class CCode : XmlReestr
    {
        // [Key]
        // public int Id { get; set; }
        [Required]
        public virtual CFile_ZGLV ZGLV { get; set; }
        [Required]
        public virtual CFile_SCHET SCHET { get; set; }
        [Required, XmlElement(typeof(CFile_ZAP), ElementName = "ZAP")]
        public virtual List<CFile_ZAP> ZAP { get; set; }
        // [Column(TypeName = "datetime2"), Required]
        // public DateTime LastWrite { get; set; }
        // [Required]
        // public long FileSize { get; set; }
    }
    public class CFile_ZGLV
    {
        [Key]
        [ForeignKey("C"), XmlIgnore]
        public int ReestrId { get; set; }
        [MaxLength(5), Required]
        public string VERSION { get; set; }
        [Column(TypeName = "datetime2"), Required, XmlElement(DataType = "date")]
        public DateTime DATA { get; set; }
        [MaxLength(26), Required]
        public string FILENAME { get; set; }
        [Required]
        public int SD_Z { get; set; }
        [XmlIgnore]
        public virtual CCode C { get; set; }
    }
    public class CFile_SCHET
    {
        [Key]
        [ForeignKey("C"), XmlIgnore]
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
        [XmlIgnore]
        public virtual CCode C { get; set; }
        public bool ShouldSerializeSANK_EKMP() { return SANK_EKMP.HasValue; }
        public bool ShouldSerializeSUMMAP() { return SUMMAP.HasValue; }
        public bool ShouldSerializeSANK_MEK() { return SANK_MEK.HasValue; }
        public bool ShouldSerializeSANK_MEE() { return SANK_MEE.HasValue; }
    }
    public class CFile_ZAP
    {
        [Key, XmlIgnore]
        public int ZapId { get; set; }
        [ForeignKey("C"), XmlIgnore]
        public int ReestrId { get; set; }
        [Required]
        public int N_ZAP { get; set; }
        [Required]
        public int PR_NOV { get; set; }
        [Required, XmlElement(typeof(CFile_PACIENT), ElementName = "PACIENT")]
        public virtual CFile_PACIENT PACIENT { get; set; }
        [Required, XmlElement(typeof(CFile_Z_SL), ElementName = "Z_SL")]
        public virtual CFile_Z_SL Z_SL { get; set; }
        [XmlIgnore]
        public virtual CCode C { get; set; }
    }
    public class CFile_PACIENT
    {
        [Key]
        [ForeignKey("ZAP"), XmlIgnore, Required]
        public int ZapId { get; set; }
        [MaxLength(36), Required]
        public string ID_PAC { get; set; }
        [Required]
        public int VPOLIS { get; set; }
        [MaxLength(10)]
        public string SPOLIS { get; set; }
        [MaxLength(20), Required]
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
        [XmlIgnore, Required]
        public virtual CFile_ZAP ZAP { get; set; }
        public bool ShouldSerializeINV() { return INV.HasValue; }
        public bool ShouldSerializeMSE() { return MSE.HasValue; }
        public bool ShouldSerializeVNOV_D() { return VNOV_D.HasValue; }
    }
    public class CFile_Z_SL
    {
        [Key]
        [ForeignKey("ZAP"), XmlIgnore, Required]
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
        [Column(TypeName = "datetime2")]
        public DateTime? NPR_DATE { get; set; }
        [MaxLength(6), Required]
        public string LPU { get; set; }
        [Column(TypeName = "datetime2"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_Z_1 { get; set; }
        [Column(TypeName = "datetime2"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_Z_2 { get; set; }
        public int? KD_Z { get; set; }
        [XmlElement(typeof(CFile_VNOV_M), ElementName = "VNOV_M")]
        public virtual List<CFile_VNOV_M> VNOV_M { get; set; }
        [Required]
        public int RSLT { get; set; }
        [Required]
        public int ISHOD { get; set; }
        [XmlElement(typeof(CFile_OS_SLUCH), ElementName = "OS_SLUCH")]
        public virtual List<CFile_OS_SLUCH> OS_SLUCH { get; set; }
        public int? VB_P { get; set; }
        [Required, XmlElement(typeof(CFile_SL), ElementName = "SL")]
        public virtual List<CFile_SL> SL { get; set; }
        [Required]
        public int IDSP { get; set; }
        [DataType("decimal(15,2)")]
        public decimal SUMV { get; set; }
        public int? OPLATA { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? SUMP { get; set; }
        [XmlElement(typeof(CFile_SANK), ElementName = "SANK")]
        public virtual List<CFile_SANK> SANK { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? SANK_IT { get; set; }
        [XmlIgnore, Required]
        public virtual CFile_ZAP ZAP { get; set; }
        public bool ShouldSerializeNPR_DATE() { return NPR_DATE.HasValue; }
        public bool ShouldSerializeKD_Z() { return KD_Z.HasValue; }
        public bool ShouldSerializeVB_P() { return VB_P.HasValue; }
        public bool ShouldSerializeOPLATA() { return OPLATA.HasValue; }
        public bool ShouldSerializeSUMP() { return SUMP.HasValue; }
        public bool ShouldSerializeSANK_IT() { return SANK_IT.HasValue; }
    }
    public class CFile_SL
    {
        [Key, XmlIgnore]
        public int SlId { get; set; }
        [ForeignKey("Z_SL"), XmlIgnore, Required]
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
        [XmlElement(typeof(CFile_DS2), ElementName = "DS2")]
        public virtual List<CFile_DS2> DS2 { get; set; }
        [XmlElement(typeof(CFile_DS3), ElementName = "DS3")]
        public virtual List<CFile_DS3> DS3 { get; set; }
        public int? C_ZAB { get; set; }
        [Required]
        public int DS_ONK { get; set; }
        public int? DN { get; set; }
        [XmlElement(typeof(CFile_CODE_MES1), ElementName = "CODE_MES1")]
        public virtual List<CFile_CODE_MES1> CODE_MES1 { get; set; }
        [MaxLength(20)]
        public string CODE_MES2 { get; set; }
        [XmlElement(typeof(CFile_NAPR), ElementName = "NAPR")]
        public virtual List<CFile_NAPR> NAPR { get; set; }
        [XmlElement(typeof(CFile_CONS), ElementName = "CONS")]
        public virtual List<CFile_CONS> CONS { get; set; }
        public virtual CFile_ONK_SL ONK_SL { get; set; }
        public virtual CFile_KSG_KPG KSG_KPG { get; set; }
        public int? REAB { get; set; }
        [Required]
        public int PRVS { get; set; }
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
        [XmlElement(typeof(CFile_USL), ElementName = "USL")]
        public virtual List<CFile_USL> USL { get; set; }
        [MaxLength(250)]
        public string COMENTSL { get; set; }
        [Required, XmlIgnore]
        public virtual CFile_Z_SL Z_SL { get; set; }
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
    public class CFile_NAPR
    {
        [Key, XmlIgnore]
        public int NaprId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [XmlIgnore, Required]
        public virtual CFile_SL SL { get; set; }
        [Column(TypeName = "datetime2"), Required]
        public DateTime NAPR_DATE { get; set; }
        [MaxLength(6)]
        public string NAPR_MO { get; set; }
        [Required]
        public int NAPR_V { get; set; }
        public int? MET_ISSL { get; set; }
        [MaxLength(15)]
        public string NAPR_USL { get; set; }
        public bool ShouldSerializeMET_ISSL() { return MET_ISSL.HasValue; }
    }
    public class CFile_CONS
    {
        [Key, XmlIgnore]
        public int ConsId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [XmlIgnore, Required]
        public virtual CFile_SL SL { get; set; }
        [Required]
        public int PR_CONS { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? DT_CONS { get; set; }
        public bool ShouldSerializeDT_CONS() { return DT_CONS.HasValue; }
    }
    public class CFile_ONK_SL
    {
        [Key]
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [Required, XmlIgnore]
        public virtual CFile_SL SL { get; set; }
        public int? DS1_T { get; set; }
        public int? STAD { get; set; }//Изменен на необязательный
        public int? ONK_T { get; set; }//Изменен на необязательный
        public int? ONK_N { get; set; }//Изменен на необязательный
        public int? ONK_M { get; set; }//Изменен на необязательный
        public int? MTSTZ { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? SOD { get; set; }
        public int? K_FR { get; set; }//Добавлен
        [DataType("decimal(15,2)")]
        public decimal? WEI { get; set; }//Добавлен
        public int? HEI { get; set; }//Добавлен
        [DataType("decimal(15,2)")]
        public decimal? BSA { get; set; }//Добавлен
        [XmlElement(typeof(CFile_B_DIAG), ElementName = "B_DIAG")]
        public virtual List<CFile_B_DIAG> B_DIAG { get; set; }
        [XmlElement(typeof(CFile_B_PROT), ElementName = "B_PROT")]
        public virtual List<CFile_B_PROT> B_PROT { get; set; }
        [XmlElement(typeof(CFile_ONK_USL), ElementName = "ONK_USL")]
        public virtual List<CFile_ONK_USL> ONK_USL { get; set; }
        public bool ShouldSerializeSTAD() { return STAD.HasValue; }
        public bool ShouldSerializeONK_T() { return ONK_T.HasValue; }
        public bool ShouldSerializeONK_N() { return ONK_N.HasValue; }
        public bool ShouldSerializeONK_M() { return ONK_M.HasValue; }
        public bool ShouldSerializeMTSTZ() { return MTSTZ.HasValue; }
        public bool ShouldSerializeSOD() { return SOD.HasValue; }
        public bool ShouldSerializeK_FR() { return K_FR.HasValue; }
        public bool ShouldSerializeWEI() { return WEI.HasValue; }
        public bool ShouldSerializeHEI() { return HEI.HasValue; }
        public bool ShouldSerializeBSA() { return BSA.HasValue; }
    }
    public class CFile_B_DIAG
    {
        [Key, XmlIgnore]
        public int B_Diag_Id { get; set; }
        [ForeignKey("ONK_SL"), XmlIgnore]
        public int SlId { get; set; }
        [XmlIgnore, Required]
        public virtual CFile_ONK_SL ONK_SL { get; set; }
        [Column(TypeName = "datetime2"), Required]
        public DateTime DIAG_DATE { get; set; }
        [Required]
        public int DIAG_TIP { get; set; }
        [Required]
        public int DIAG_CODE { get; set; }
        public int? DIAG_RSLT { get; set; }
        public int? REC_RSLT { get; set; }
        public bool ShouldSerializeDIAG_RSLT() { return DIAG_RSLT.HasValue; }
        public bool ShouldSerializeREC_RSLT() { return REC_RSLT.HasValue; }
    }
    public class CFile_B_PROT
    {
        [Key, XmlIgnore]
        public int B_Prot_Id { get; set; }
        [ForeignKey("ONK_SL"), XmlIgnore]
        public int SlId { get; set; }
        [Required, XmlIgnore]
        public virtual CFile_ONK_SL ONK_SL { get; set; }
        [Required]
        public int PROT { get; set; }
        [Column(TypeName = "datetime2"), Required]
        public DateTime D_PROT { get; set; }
    }
    public class CFile_ONK_USL
    {
        [Key, XmlIgnore]
        public int Onk_UslId { get; set; }
        [ForeignKey("ONK_SL"), XmlIgnore]
        public int SlId { get; set; }
        [Required, XmlIgnore]
        public virtual CFile_ONK_SL ONK_SL { get; set; }
        [Required]
        public int USL_TIP { get; set; }
        public int? HIR_TIP { get; set; }
        public int? LEK_TIP_L { get; set; }
        public int? LEK_TIP_V { get; set; }
        [XmlElement(typeof(CFile_LEK_PR), ElementName = "LEK_PR")]
        public virtual List<CFile_LEK_PR> LEK_PR { get; set; }
        public int? PPTR { get; set; }//добавлен
        public int? LUCH_TIP { get; set; }
        public bool ShouldSerializeHIR_TIP() { return HIR_TIP.HasValue; }
        public bool ShouldSerializeLEK_TIP_L() { return LEK_TIP_L.HasValue; }
        public bool ShouldSerializeLEK_TIP_V() { return LEK_TIP_V.HasValue; }
        public bool ShouldSerializeLUCH_TIP() { return LUCH_TIP.HasValue; }
        public bool ShouldSerializePPTR() { return PPTR.HasValue; }
    }
    public class CFile_LEK_PR
    {
        [Key, XmlIgnore]
        public int Lek_PrId { get; set; }
        [ForeignKey("ONK_USL"), XmlIgnore]
        public int Onk_UslId { get; set; }
        [XmlIgnore, Required]
        public virtual CFile_ONK_USL ONK_USL { get; set; }
        [Required, MaxLength(6)]
        public string REGNUM { get; set; }
        [MaxLength(10)]
        public string CODE_SH { get; set; }
        [Required]
        [XmlElement(typeof(CFile_DATE_INJ), ElementName = "DATE_INJ")]
        public virtual List<CFile_DATE_INJ> DATE_INJ { get; set; }
    }
    public class CFile_DATE_INJ
    {
        [Key, XmlIgnore]
        public int Date_InjId { get; set; }
        [ForeignKey("LEK_PR"), XmlIgnore]
        public int Lek_PrId { get; set; }
        [XmlIgnore, Required]
        public virtual CFile_LEK_PR LEK_PR { get; set; }
        [Column(TypeName = "datetime2"), Required]
        public DateTime DATE_INJ { get; set; }
    }
    public class CFile_SANK
    {
        [Key, XmlIgnore]
        public int SankId { get; set; }
        [ForeignKey("Z_SL"), XmlIgnore]
        public int ZapId { get; set; }
        [Required]
        public string S_CODE { get; set; }
        [DataType("decimal(15,2)"), Required]
        public decimal S_SUM { get; set; }
        [Required]
        public int S_TIP { get; set; }
        [XmlElement(typeof(CFile_SL_ID), ElementName = "SL_ID")]//Добавлен
        public virtual List<CFile_SL_ID> SL_ID { get; set; }
        public int? S_OSN { get; set; }
        [Column(TypeName = "datetime2"), Required]
        public DateTime DATE_ACT { get; set; }
        [MaxLength(30), Required]
        public string NUM_ACT { get; set; }
        [XmlElement(typeof(CFile_CODE_EXP), ElementName = "CODE_EXP")]
        public virtual List<CFile_CODE_EXP> CODE_EXP { get; set; }
        [MaxLength(250)]
        public string S_COM { get; set; }
        [Required]
        public int S_IST { get; set; }
        [XmlIgnore, Required]
        public virtual CFile_Z_SL Z_SL { get; set; }
        public bool ShouldSerializeS_OSN() { return S_OSN.HasValue; }
    }
    public class CFile_USL
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
        [Required, XmlIgnore]
        public virtual CFile_SL SL { get; set; }
        public bool ShouldSerializePODR() { return PODR.HasValue; }
        public bool ShouldSerializeTARIF() { return TARIF.HasValue; }
        public bool ShouldSerializeNPL() { return NPL.HasValue; }
    }
    public class CFile_KSG_KPG
    {
        [Key]
        [ForeignKey("SL"), XmlIgnore, Required]
        public int SlId { get; set; }
        [MaxLength(20), Required]
        public string N_KSG { get; set; }
        [Required]
        public int VER_KSG { get; set; }
        [Required]
        public int KSG_PG { get; set; }
        [MaxLength(4)]
        public string N_KPG { get; set; }//Изменен
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
        [XmlElement(typeof(CFile_CRIT), ElementName = "CRIT")]
        public virtual List<CFile_CRIT> CRIT { get; set; }
        [Required]
        public int SL_K { get; set; }
        [DataType("decimal(1,5)")]
        public decimal? IT_SL { get; set; }
        [XmlElement(typeof(CFile_SL_KOEF), ElementName = "SL_KOEF")]
        public virtual List<CFile_SL_KOEF> SL_KOEF { get; set; }
        [Required, XmlIgnore]
        public virtual CFile_SL SL { get; set; }
        public bool ShouldSerializeIT_SL() { return IT_SL.HasValue; }
    }
    public class CFile_SL_KOEF
    {
        [Key, XmlIgnore]
        public int Sl_KoefId { get; set; }
        [ForeignKey("KSG_KPG"), XmlIgnore]
        public int SlId { get; set; }
        [Required]
        public int IDSL { get; set; }
        [DataType("decimal(1,5)"), Required]
        public decimal Z_SL { get; set; }
        [Required, XmlIgnore]
        public virtual CFile_KSG_KPG KSG_KPG { get; set; }
    }
    public class CFile_VNOV_M
    {
        [Key, XmlIgnore]
        public int Vnov_MId { get; set; }
        [ForeignKey("Z_SL"), XmlIgnore]
        public int ZapId { get; set; }
        public int VNOV_M { get; set; }
        [Required, XmlIgnore]
        public virtual CFile_Z_SL Z_SL { get; set; }
    }
    public class CFile_OS_SLUCH
    {
        [Key, XmlIgnore]
        public int Os_SluchId { get; set; }
        [ForeignKey("Z_SL"), XmlIgnore]
        public int ZapId { get; set; }
        public int OS_SLUCH { get; set; }
        [Required, XmlIgnore]
        public virtual CFile_Z_SL Z_SL { get; set; }
    }
    public class CFile_DS2
    {
        [Key, XmlIgnore]
        public int Ds2Id { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(10)]
        public string DS2 { get; set; }
        [Required, XmlIgnore]
        public virtual CFile_SL SL { get; set; }
    }
    public class CFile_DS3
    {
        [Key, XmlIgnore]
        public int Ds3Id { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(10)]
        public string DS3 { get; set; }
        [Required, XmlIgnore]
        public virtual CFile_SL SL { get; set; }
    }
    public class CFile_CODE_MES1
    {
        [Key, XmlIgnore]
        public int Code_Mes1Id { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(20)]
        public string CODE_MES1 { get; set; }
        [Required, XmlIgnore]
        public virtual CFile_SL SL { get; set; }
    }
    public class CFile_CODE_EXP
    {
        [Key, XmlIgnore]
        public int Code_ExpId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(8)]
        public string CODE_EXP { get; set; }
        [Required, XmlIgnore]
        public virtual CFile_SL SL { get; set; }
    }
    public class CFile_CRIT
    {
        [XmlIgnore, Key]
        public int CritId { get; set; }
        [ForeignKey("KSG_KPG"), XmlIgnore, Required]
        public int SlId { get; set; }
        [MaxLength(10), XmlText(typeof(string))]
        public string CRIT { get; set; }
        [XmlIgnore, Required]
        public virtual CFile_KSG_KPG KSG_KPG { get; set; }
    }
    public class CFile_SL_ID
    {
        [XmlIgnore, Key]
        public int Sl_Id_Id { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(36), XmlText(typeof(string))]
        public string SL_ID { get; set; }
        [XmlIgnore, Required]
        public virtual CFile_SL SL { get; set; }
    }
}
