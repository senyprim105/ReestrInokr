using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Reestr2.Model
{
    [XmlRoot("ZL_LIST")]
    public class SCode : XmlReestr
    {
       // [Key, XmlIgnore]
       // public int Id { get; set; }
        [Required]
        public virtual SFile_ZGLV ZGLV { get; set; }
        [Required]
        public virtual SFile_SCHET SCHET { get; set; }
        [Required, XmlElement(typeof(SFile_SL), ElementName = "SL")]
        public virtual List<SFile_SL> SL { get; set; }
       // [Column(TypeName = "datetime2"), Required, XmlIgnore]
       // public DateTime LastWrite { get; set; }
       // [Required, XmlIgnore]
       // public long FileSize { get; set; }
    }
    public class SFile_ZGLV
    {
        [Key, ForeignKey("SCode"), XmlIgnore]
        public int ReestrId { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DATA { get; set; }
        [Required, MaxLength(50)]
        public string FILENAME { get; set; }
        [Required, MaxLength(26)]
        public string FILENAME1 { get; set; }
        [XmlIgnore,Required]
        public virtual SCode SCode { get; set; }
    }
    public class SFile_SCHET
    {
        [Key, ForeignKey("SCode"), XmlIgnore]
        public int ReestrId { get; set; }
        [Required, MaxLength(4)]
        public string YEAR { get; set; }
        [Required, MaxLength(2)]
        public string MONTH { get; set; }
        [Required, MaxLength(6)]
        public string CODE_MO { get; set; }
        [Required]
        public int LPU { get; set; }
        [XmlIgnore,Required]
        public virtual SCode SCode { get; set; }
    }

    public class SFile_SL
    {
        [Key, XmlIgnore]
        public int SlId { get; set; }
        [ForeignKey("SCode"), XmlIgnore]
        public int ReestrId { get; set; }
        [Required, MaxLength(36)]
        public string SL_ID { get; set; }
        [Required]
        public long IDCASE { get; set; }
        public string NSNDHOSP { get; set; }
        public int? REN { get; set; }
        public int? SOFA { get; set; }
        public int? PARENT { get; set; }
        [MaxLength(3)]
        public string D_TYPE { get; set; }
        public int? FROM_FIRM { get; set; }
        [XmlElement(typeof(SFile_DS1_M), ElementName = "DS1_M")]
        public virtual List<SFile_DS1_M> DS1_M { get; set; }
        [XmlElement(typeof(SFile_USL), ElementName = "USL")]
        public virtual List<SFile_USL> USL { get; set; }
        [XmlIgnore,Required]
        public virtual SCode SCode { get; set; }
        public bool ShouldSerializeREN() { return REN.HasValue; }
        public bool ShouldSerializeSOFA() { return SOFA.HasValue; }
        public bool ShouldSerializePARENT() { return PARENT.HasValue; }
        public bool ShouldSerializeFROM_FIRM() { return FROM_FIRM.HasValue; }

    }
    public class SFile_DS1_M
    {
        [XmlIgnore,Key]
        public int Ds1_MId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [MaxLength(10), XmlText(typeof(string))]
        public string DS1_M { get; set; }
        [XmlIgnore]
        public virtual SFile_SL SL { get; set; }
    }

    public class SFile_USL
    {
        [Key, XmlIgnore]
        public int UslId { get; set; }
        [ForeignKey("SL"), XmlIgnore]
        public int SlId { get; set; }
        [XmlIgnore]
        public SFile_SL SL { get; set; }
        [Required, MaxLength(36)]
        public string IDSERV { get; set; }
        public int? PR_USL { get; set; }
        [XmlElement(typeof(SFile_RL), ElementName = "RL")]
        public virtual List<SFile_RL> RL { get; set; }
        public bool ShouldSerializePR_USL() { return PR_USL.HasValue; }
    }
    public class SFile_RL
    {
        [Key, XmlIgnore]
        public int RlId { get; set; }
        [ForeignKey("USL"), XmlIgnore]
        public int UslId { get; set; }
        [XmlIgnore]
        public virtual SFile_USL USL { get; set; }
        [Required]
        public int IDRL { get; set; }
        [MaxLength(250)]
        public string NAME_MNN { get; set; }//Убрали с апреля
        [MaxLength(8)]
        public string ID_ATX { get; set; }//Убрали с апреля
        [MaxLength(6)]
        public string KOD_MNN { get; set; }
        public int? ID_MI { get; set; }
        public int? KOL_MI { get; set; }
        public int? KOL_LEK { get; set; }//Добавили с апреля
        public bool ShouldSerializeID_MI() { return ID_MI.HasValue; }
        public bool ShouldSerializeKOL_MI() { return KOL_MI.HasValue; }
        public bool ShouldSerializeKOL_LEK() { return KOL_LEK.HasValue; }//С апреля
    }
}
