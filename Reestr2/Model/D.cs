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
    public class DCode : XmlReestr
    {
        //[Key, XmlIgnore]
        //public int Id { get; set; }
        [Required]
        public virtual DFile_ZGLV ZGLV { get; set; }
        [Required]
        public virtual DFile_SCHET SCHET { get; set; }
        [Required, XmlElement(typeof(DFile_ZAP), ElementName = "ZAP")]
        public virtual List<DFile_ZAP> ZAP { get; set; }
        //[Column(TypeName = "datetime2"), Required, XmlIgnore]
        //public DateTime LastWrite { get; set; }
        //[Required, XmlIgnore]
        //public long FileSize { get; set; }
    }

    public class DFile_ZGLV
    {
        [Key]
        [ForeignKey("D"), XmlIgnore]
        public int ReestrId { get; set; }
        [MaxLength(5), Required]
        public string VERSION { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DATA { get; set; }
        [MaxLength(26), Required]
        public string FILENAME { get; set; }
        [Required]
        public int SD_Z { get; set; }
        [XmlIgnore]
        public virtual DCode D { get; set; }
    }

    public class DFile_SCHET
    {
        [Key]
        [ForeignKey("D"), XmlIgnore]
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
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
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
        [MaxLength(3)]
        public string DISP { get; set; }
        [XmlIgnore]
        public virtual DCode D { get; set; }
        public bool ShouldSerializeSANK_EKMP() { return SANK_EKMP.HasValue; }
        public bool ShouldSerializeSUMMAP() { return SUMMAP.HasValue; }
        public bool ShouldSerializeSANK_MEK() { return SANK_MEK.HasValue; }
        public bool ShouldSerializeSANK_MEE() { return SANK_MEE.HasValue; }
    }

    public class DFile_ZAP
    {
        [Key, XmlIgnore]
        public int ZapId { get; set; }
        [ForeignKey("D"), XmlIgnore]
        public int ReestrId { get; set; }
        [Required]
        public int N_ZAP { get; set; }
        [Required]
        public int PR_NOV { get; set; }
        [Required]
        public virtual DFile_PACIENT PACIENT { get; set; }
        [Required]
        public virtual DFile_Z_SL Z_SL { get; set; }
        [XmlIgnore]
        public virtual DCode D { get; set; }
    }

    public class DFile_PACIENT
    {
        [Key]
        [ForeignKey("ZAP"), XmlIgnore]
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
        [MaxLength(9), Required]
        public string NOVOR { get; set; }
        [XmlIgnore]
        public virtual DFile_ZAP ZAP { get; set; }
    }

    public class DFile_Z_SL
    {
        [Key]
        [ForeignKey("ZAP"), XmlIgnore]
        public int ZapId { get; set; }
        [Required]
        public long IDCASE { get; set; }
        [Required]
        public int VIDPOM { get; set; }
        [MaxLength(6), Required]
        public string LPU { get; set; }
        [Required]
        public int VBR { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_Z_1 { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_Z_2 { get; set; }
        [Required]
        public int P_OTK { get; set; }
        [Required]
        public int RSLT_D { get; set; }
        [XmlElement(typeof(DFile_OS_SLUCH), ElementName = "OS_SLUCH")]
        public virtual List<DFile_OS_SLUCH> OS_SLUCH { get; set; }
        [Required, XmlElement(typeof(DFile_SL), ElementName = "SL")]
        public virtual List<DFile_SL> SL { get; set; }
        [Required]
        public int IDSP { get; set; }
        [DataType("decimal(15,2)")]
        public decimal SUMV { get; set; }
        public int? OPLATA { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? SUMP { get; set; }
        [Required, XmlElement(typeof(DFile_SANK), ElementName = "SANK")]
        public virtual List<DFile_SANK> SANK { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? SANK_IT { get; set; }
        [XmlIgnore]
        public virtual DFile_ZAP ZAP { get; set; }
        public bool ShouldSerializeOPLATA() { return OPLATA.HasValue; }
        public bool ShouldSerializeSUMP() { return SUMP.HasValue; }
        public bool ShouldSerializeSANK_IT() { return SANK_IT.HasValue; }
    }

    public class DFile_SL
    {
        [Key, XmlIgnore]
        public int SlId { get; set; }
        [ForeignKey("Z_SL"), XmlIgnore]
        public int ZapId { get; set; }
        [Required,MaxLength(36)]
        public string SL_ID { get; set; }
        [MaxLength(8)]
        public string LPU_1 { get; set; }
        [MaxLength(50)]
        public string NHISTORY { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_1 { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_2 { get; set; }
        [MaxLength(10), Required]
        public string DS1 { get; set; }
        public int? DS1_PR { get; set; }
        [Required]
        public int DS_ONK { get; set; }
        [Required]
        public int PR_D_N { get; set; }
        [XmlElement(typeof(DFile_DS2_N), ElementName = "DS2_N")]
        public virtual List<DFile_DS2_N> DS2_N { get; set; }
        [XmlElement(typeof(DFile_NAZ), ElementName = "NAZ")]
        public virtual List<DFile_NAZ> NAZ { get; set; }
        [DataType("decimal(5,2)")]
        public decimal? ED_COL { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? TARIF { get; set; }
        [DataType("decimal(15,2)"), Required]
        public decimal SUM_M { get; set; }
        [XmlElement(typeof(DFile_USL), ElementName = "USL")]
        public virtual List<DFile_USL> USL { get; set; }
        [MaxLength(250)]
        public string COMENTSL { get; set; }
        [XmlIgnore]
        public virtual DFile_Z_SL Z_SL { get; set; }
        public bool ShouldSerializeDS1_PR() { return DS1_PR.HasValue; }
        public bool ShouldSerializeED_COL() { return ED_COL.HasValue; }
        public bool ShouldSerializeTARIF() { return TARIF.HasValue; }

    }

    public class DFile_SANK
    {
        [Key, XmlIgnore]
        public int SankId { get; set; }
        [ForeignKey("Z_SL"), XmlIgnore]
        public int ZapId { get; set; }
        [Required, MaxLength(36)]
        public string S_CODE { get; set; }
        [DataType("decimal(15,2)"), Required]
        public decimal S_SUM { get; set; }
        [Required]
        public int S_TIP { get; set; }
        [XmlElement(typeof(DFile_SL_ID), ElementName = "SL_ID")]//Добавлен
        public virtual List<DFile_SL_ID> SL_ID { get; set; }
        public int? S_OSN { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_ACT { get; set; }
        [MaxLength(30), Required]
        public string NUM_ACT { get; set; }
        [XmlElement(typeof(DFile_CODE_EXP), ElementName = "CODE_EXP")]
        public virtual List<DFile_CODE_EXP> CODE_EXP { get; set; }
        [MaxLength(250)]
        public string S_COM { get; set; }
        [Required]
        public int S_IST { get; set; }
        [XmlIgnore]
        public virtual DFile_Z_SL Z_SL { get; set; }
        public bool ShouldSerializeS_OSN() { return S_OSN.HasValue; }
    }

    public class DFile_USL
    {
        [Key, XmlIgnore]
        public int UslId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [Required,MaxLength(36)]
        public string IDSERV { get; set; }
        [Required,MaxLength(6)]
        public string LPU { get; set; }
        [MaxLength(8)]
        public string LPU_1 { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_IN { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DATE_OUT { get; set; }
        [Required]
        public int P_OTK { get; set; }
        [Required, MaxLength(20)]
        public string CODE_USL { get; set; }
        [DataType("decimal(15,2)")]
        public decimal? TARIF { get; set; }
        [DataType("decimal(15,2)"), Required]
        public decimal SUMV_USL { get; set; }
        [Required]
        public int PRVS { get; set; }
        [Required, MaxLength(25)]
        public string CODE_MD { get; set; }
        [MaxLength(250)]
        public string COMENTU { get; set; }
        [XmlIgnore]
        public virtual DFile_SL SL { get; set; }
        public bool ShouldSerializeTARIF() { return TARIF.HasValue; }
    }

    public class DFile_VNOV_M
    {
        [XmlIgnore, Key]
        public int Vnov_MId { get; set; }
        [ForeignKey("Z_SL"), XmlIgnore]
        public int ZapId { get; set; }
        [XmlText(typeof(int))]
        public int VNOV_M { get; set; }
        [XmlIgnore]
        public virtual DFile_Z_SL Z_SL { get; set; }
    }

    public class DFile_OS_SLUCH
    {
        [XmlIgnore, Key]
        public int Os_SluchId { get; set; }
        [ForeignKey("Z_SL"), XmlIgnore]
        public int ZapId { get; set; }
        [XmlText(typeof(int))]
        public int OS_SLUCH { get; set; }
        [XmlIgnore]
        public virtual DFile_Z_SL Z_SL { get; set; }
    }

    public class DFile_DS2_N
    {
        [XmlIgnore, Key]
        public int Ds2_NId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        public int? DS2_PR { get; set; }
        public int PR_DS2_N { get; set; }
        [XmlIgnore]
        public virtual DFile_SL SL { get; set; }
        public bool ShouldSerializeDS2_PR() { return DS2_PR.HasValue; }
    }

    public class DFile_NAZ
    {
        [XmlIgnore, Key]
        public int NazId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [Required]
        public int NAZ_N { get; set; }
        [Required]
        public int NAZ_R { get; set; }
        public int? NAZ_SP { get; set; }
        public int? NAZ_V { get; set; }
        [MaxLength(15)]
        public string NAZ_USL { get; set; }
        [Column(TypeName = "smalldatetime"), XmlElement(DataType = "date")]
        public DateTime? NAPR_DATE { get; set; }
        [MaxLength(6)]
        public string NAPR_MO { get; set; }
        public int? NAZ_PMP { get; set; }
        public int? NAZ_PK { get; set; }
        [XmlIgnore]
        public virtual DFile_SL SL { get; set; }
        public bool ShouldSerializeNAZ_SP() { return NAZ_SP.HasValue; }
        public bool ShouldSerializeNAZ_V() { return NAZ_V.HasValue; }
        public bool ShouldSerializeNAPR_DATE() { return NAPR_DATE.HasValue; }
        public bool ShouldSerializeNAZ_PMP() { return NAZ_PMP.HasValue; }
        public bool ShouldSerializeNAZ_PK() { return NAZ_PK.HasValue; }
    }

    public class DFile_CODE_EXP
    {
        [XmlIgnore, Key]
        public int Code_ExpId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(8), XmlText(typeof(string))]
        public string CODE_EXP { get; set; }
        [XmlIgnore]
        public virtual DFile_SL SL { get; set; }
    }
    public class DFile_SL_ID
    {
        [XmlIgnore, Key]
        public int Sl_Id_Id { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(36), XmlText(typeof(string))]
        public string SL_ID { get; set; }
        [XmlIgnore]
        public virtual DFile_SL SL { get; set; }
    }
}
