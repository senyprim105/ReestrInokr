using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Reestr2.Model
{
    [XmlRoot("ZL_LIST")]
    public class PCode : XmlReestr
    {
        //[Key, XmlIgnore]
        //public int Id { get; set; }
        [Required]
        public virtual PFile_ZGLV ZGLV { get; set; }
        [Required]
        public virtual PFile_SCHET SCHET { get; set; }
        [Required, XmlElement(typeof(PFile_SL), ElementName = "SL")]
        public virtual List<PFile_SL> SL { get; set; }
        //[Column(TypeName = "datetime2"), Required, XmlIgnore]
        //public DateTime LastWrite { get; set; }
        //[Required, XmlIgnore]
        //public long FileSize { get; set; }
    }

    public class PFile_ZGLV
    {
        [Key, ForeignKey("PCode"), XmlIgnore]
        public int ReestId { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DATA { get; set; }
        [Required, MaxLength(50)]
        public string FILENAME { get; set; }
        [Required, MaxLength(26)]
        public string FILENAME1 { get; set; }
        [XmlIgnore]
        public virtual PCode PCode { get; set; }
    }

    public class PFile_SCHET
    {
        [Key, ForeignKey("PCode"), XmlIgnore]
        public int ReestrId { get; set; }
        [Required, MaxLength(4)]
        public string YEAR { get; set; }
        [Required, MaxLength(2)]
        public string MONTH { get; set; }
        [Required, MaxLength(6)]
        public string CODE_MO { get; set; }
        [Required]
        public int LPU { get; set; }
        [XmlIgnore]
        public virtual PCode PCode { get; set; }
    }

    public class PFile_SL
    {
        [Key, XmlIgnore]
        public int SlId { get; set; }
        [ForeignKey("PCode"), XmlIgnore]
        public int ReestrId { get; set; }
        [Required, MaxLength(36)]
        public string SL_ID { get; set; }
        [Required]
        public long IDCASE { get; set; }
        [Required]
        public int CARD { get; set; }
        public int? FROM_FIRM { get; set; }
        [Required]
        public int PURP { get; set; }
        [Required]
        public int VISIT_POL { get; set; }
        [Required]
        public int VISIT_HOM { get; set; }
        [MaxLength(10)]
        public string NSNDHOSP { get; set; }
        [XmlIgnore]
        public int? SPECFIC { get; set; }
        [XmlElement("SPECFIC"), NotMapped]
        public string PrvsAsText
        {
            get { return (SPECFIC.HasValue) ? SPECFIC.ToString() : null; }
            set { SPECFIC = !string.IsNullOrEmpty(value) ? int.Parse(value) : default(int?); }
        }
        [Required]
        public int TYPE_PAY { get; set; }
        [MaxLength(3)]
        public string D_TYPE { get; set; }
        [XmlElement(typeof(PFile_NAPRLECH), ElementName = "NAPRLECH")]
        public virtual List<PFile_NAPRLECH> NAPRLECH { get; set; }
        [XmlElement(typeof(PFile_USL), ElementName = "USL")]
        public virtual List<PFile_USL> USL { get; set; }
        [XmlElement(typeof(PFile_STOM), ElementName = "STOM")]
        public virtual List<PFile_STOM> STOM { get; set; }
        [XmlElement(typeof(PFile_VIZOV), ElementName = "VIZOV")]
        public virtual List<PFile_VIZOV> VIZOV { get; set; }
        [XmlIgnore]
        public virtual PCode PCode { get; set; }
        public bool ShouldSerializeFROM_FIRM() { return FROM_FIRM.HasValue; }
    }

    public class PFile_NAPRLECH
    {
        [Key, XmlIgnore]
        public int NaprlechId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(10), Required, XmlText(typeof(string))]
        public string NAPRLECH { get; set; }
        [XmlIgnore]
        public virtual PFile_SL SL { get; set; }
    }

    public class PFile_USL
    {
        [Key, XmlIgnore]
        public int UslId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [XmlIgnore]
        public virtual PFile_SL SL { get; set; }
        [Required, MaxLength(36)]
        public string IDSERV { get; set; }
        [Required, MaxLength(9)]
        public string EXECUTOR { get; set; }
        [MaxLength(6)]
        public string EX_SPEC { get; set; }
        [XmlElement(typeof(PFile_RL), ElementName = "RL")]
        public virtual List<PFile_RL> RL { get; set; }
    }

    public class PFile_RL
    {
        [Key, XmlIgnore]
        public int RlId { get; set; }
        [ForeignKey("USL"), XmlIgnore]
        public int UslId { get; set; }
        [XmlIgnore]
        public virtual PFile_USL USL { get; set; }
        [Required]
        public int IDRL { get; set; }
        [MaxLength(250)]
        public string NAME_MNN { get; set; }//Убрали с апреля
        [MaxLength(8)]
        public string ID_ATX { get; set; }//Убрали с апреля
        [MaxLength(6)]
        public string KOD_MNN { get; set; }
        public int? ID_MI { get; set;}
        public int? KOL_MI { get; set; }
        public int? KOL_LEK { get; set; }//Добавили с апреля
        public bool ShouldSerializeID_MI() { return ID_MI.HasValue; }
        public bool ShouldSerializeKOL_MI() { return KOL_MI.HasValue; }
        public bool ShouldSerializeKOL_LEK() { return KOL_LEK.HasValue; }//С апреля

    }

    public class PFile_STOM
    {
        [Key, XmlIgnore]
        public int StomId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [XmlIgnore]
        public virtual PFile_SL SL { get; set; }
        [Required]
        public long IDSTOM { get; set; }
        [Required, MaxLength(8)]
        public string CODE_USL { get; set; }
        [MaxLength(3)]
        public string ZUB { get; set; }
        [Required]
        public int KOL_VIZ { get; set; }
        [Required, DataType("decimal(15,2)")]
        public decimal UET_FAKT { get; set; }
    }

    public class PFile_VIZOV
    {
        [Key, XmlIgnore]
        public int VizovId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [XmlIgnore]
        public virtual PFile_SL SL { get; set; }
        [Required]
        public long IDVIZOV { get; set; }
        [Column(TypeName = "datetime2"), Required]
        public DateTime INC_TIME { get; set; }
        [Column(TypeName = "datetime2"), Required]
        public DateTime BR_TIME { get; set; }
        [Column(TypeName = "datetime2"), Required]
        public DateTime ARR_TIME { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? GO_TIME { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? MO_TIME { get; set; }
        [Column(TypeName = "datetime2"), Required]
        public DateTime END_TIME { get; set; }
        [Required]
        public int PLACE { get; set; }
        public long? OKTMO { get; set; }
        public int? ZONA { get; set; }
        public bool ShouldSerializeOKTMO() { return OKTMO.HasValue; }
        public bool ShouldSerializeZONA() { return ZONA.HasValue; }
    }
}
