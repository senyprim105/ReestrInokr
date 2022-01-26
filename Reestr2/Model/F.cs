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
    public class FCode : XmlReestr
    {
        //[Key, XmlIgnore]
        //public int Id { get; set; }
        [Required]
        public virtual FFile_ZGLV ZGLV { get; set; }
        [Required]
        public virtual FFile_SCHET SCHET { get; set; }
        [Required, XmlElement(typeof(FFile_ZAP), ElementName = "ZAP")]
        public virtual List<FFile_ZAP> ZAP { get; set; }
        //[Column(TypeName = "datetime2"), Required, XmlIgnore]
        //public DateTime LastWrite { get; set; }
        //[Required, XmlIgnore]
        //public long FileSize { get; set; }
    }

    public class FFile_ZGLV
    {
        [Key, ForeignKey("FCode"), XmlIgnore]
        public int ReestrId { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DATA { get; set; }
        [Required, MaxLength(50)]
        public string FILENAME { get; set; }
        [Required, MaxLength(26)]
        public string FILENAME1 { get; set; }
        [XmlIgnore]
        public virtual FCode FCode { get; set; }
    }

    public class FFile_SCHET
    {
        [Key, ForeignKey("FCode"), XmlIgnore]
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
        public virtual FCode FCode { get; set; }
    }

    public class FFile_ZAP
    {
        [Key, XmlIgnore]
        public int ZapId { get; set; }
        [ForeignKey("F"), XmlIgnore]
        public int ReestrId { get; set; }
        [Required]
        public int N_ZAP { get; set; }
        [Required]
        public int PR_NOV { get; set; }
        [Required, XmlElement(typeof(FFile_PACIENT), ElementName = "PACIENT")]
        public virtual FFile_PACIENT PACIENT { get; set; }
        [Required, XmlElement(typeof(FFile_SL), ElementName = "SL")]
        public virtual List<FFile_SL> SL { get; set; }
        [XmlIgnore]
        public virtual FCode F { get; set; }
    }

    public class FFile_PACIENT
    {
        [Key, ForeignKey("ZAP"), XmlIgnore]
        public int ZapId { get; set; }
        [XmlIgnore]
        public virtual FFile_ZAP ZAP { get; set; }
        [Required, MaxLength(36)]
        public string ID_PAC { get; set; }
        [Required, MaxLength(150)]
        public string UL_NAME { get; set; }
        [Required, MaxLength(7)]
        public string DOM { get; set; }
        [MaxLength(5)]
        public string KOR { get; set; }
        [MaxLength(5)]
        public string KV { get; set; }
        [Required]
        public int STAT_ZAN { get; set; }
        [MaxLength(150), Required]
        public string RG_NAME { get; set; }
    }

    public class FFile_SL
    {
        [Key, XmlIgnore]
        public int SlId { get; set; }
        [ForeignKey("ZAP"), XmlIgnore]
        public int ZapId { get; set; }
        [Required, MaxLength(36)]
        public string SL_ID { get; set; }
        [Required]
        public long IDCASE { get; set; }
        [Required]
        public int SPECFIC { get; set; }
        [Required]
        public int PURP { get; set; }
        //[Required]  Убран обязательный с июля
        public int? DISP_E { get; set; }
        [Required]
        public int NAPRAV { get; set; }
        [MaxLength(3)]
        public string D_TYPE { get; set; }
        public int? MPP { get; set; }
        [XmlIgnore]
        public virtual FFile_ZAP ZAP { get; set; }
        public bool ShouldSerializeMPP() { return MPP.HasValue; }
        public bool ShouldSerializeDISP_E() { return DISP_E.HasValue; }
    }
}
