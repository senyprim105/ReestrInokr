using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Reestr2.Model
{
    [XmlRoot("PERS_LIST")]
    
    public class LCode : XmlReestr
    {
       // [Key, XmlIgnore]
       // public int ID { get; set; }
        [XmlElement("ZGLV"), Required]
        public virtual LFile_ZGLV ZGLV { get; set; }
        [Required, XmlElement(typeof(LFile_PERS), ElementName = "PERS")]
        public virtual List<LFile_PERS> PERS_List { get; set; }
       // [Column(TypeName = "datetime2"), Required, XmlIgnore]
       // public DateTime LastWrite { get; set; }
       // [Required, XmlIgnore]
       // public long FileSize { get; set; }
    }

    public class LFile_ZGLV
    {
        [XmlElement("VERSION"), Required]
        public string VERSION { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DATA { get; set; }
        [XmlElement("FILENAME"), Required,MaxLength(26)]
        public string FILENAME { get; set; }
        [XmlElement("FILENAME1"), Required,MaxLength(26)]
        public string FILENAME1 { get; set; }
        [Key, ForeignKey("LCode"), XmlIgnore]
        public int ReestrId { get; set; }
        [XmlIgnore,Required]
        public virtual LCode LCode { get; set; }
    }


    public class LFile_PERS
    {
        [Key, XmlIgnore]
        public int PersId { get; set; }
        [ForeignKey("LCode"), XmlIgnore]
        public int ReestrId { get; set; }
        [XmlIgnore]
        public virtual LCode LCode { get; set; }
        [XmlElement("ID_PAC"), Required,MaxLength(36)]
        public string ID_PAC { get; set; }
        [XmlElement("FAM"), MaxLength(40)]
        public string FAM { get; set; }
        [XmlElement("IM"), MaxLength(40)]
        public string IM { get; set; }
        [XmlElement("OT"), MaxLength(40)]
        public string OT { get; set; }
        [XmlElement("W"), Required]
        public int W { get; set; }
        [Column(TypeName = "smalldatetime"), Required, XmlElement(DataType = "date")]
        public DateTime DR { get; set; }
        [XmlElement(typeof(LFile_DOST), ElementName = "DOST")]
        public virtual List<LFile_DOST> DOSTS { get; set; }
        [XmlElement("TEL"), MaxLength(100)]
        public string TEL { get; set; }
        [XmlElement("FAM_P"), MaxLength(40)]
        public string FAM_P { get; set; }
        [XmlElement("IM_P"), MaxLength(40)]
        public string IM_P { get; set; }
        [XmlElement("OT_P"), MaxLength(40)]
        public string OT_P { get; set; }
        [XmlElement("W_P")]
        public int? W_P { get; set; }
        [Column(TypeName = "smalldatetime"), XmlElement(DataType = "date")]
        public DateTime? DR_P { get; set; }
        [XmlElement(typeof(LFile_DOST_P), ElementName = "DOST_P")]
        public virtual List<LFile_DOST_P> DOST_P { get; set; }
        [XmlElement("MR"), MaxLength(100)]
        public string MR { get; set; }
        [XmlElement("DOCTYPE"), MaxLength(2)]
        public string DOCTYPE { get; set; }
        [XmlElement("DOCSER"), MaxLength(10)]
        public string DOCSER { get; set; }
        [XmlElement("DOCNUM"), MaxLength(20)]
        public string DOCNUM { get; set; }
        [Column(TypeName = "smalldatetime"), XmlElement(DataType = "date")]
        public DateTime? DOCDATE { get; set; }
        [XmlElement("DOCORG"), MaxLength(250)]
        public string DOCORG { get; set; }
        [XmlElement("SNILS"), MaxLength(14)]
        public string SNILS { get; set; }
        [XmlElement("OKATOG"), MaxLength(11)]
        public string OKATOG { get; set; }
        [XmlElement("OKATOP"), MaxLength(11)]
        public string OKATOP { get; set; }
        [XmlElement("COMENTP"), MaxLength(250)]
        public string COMENTP { get; set; }
        public bool ShouldSerializeDR_P() { return DR_P.HasValue; }
        public bool ShouldSerializeW_P() { return W_P.HasValue; }
        public bool ShouldSerializeDOCDATE() { return DOCDATE.HasValue; }
    }

    public class LFile_DOST
    {
        [Key, XmlIgnore]
        public int DostId { get; set; }
        [ForeignKey("PERS"),XmlIgnore]
        public int PersId { get; set; }
        [XmlText(typeof(int))]
        public int DOST { get; set; }
        [XmlIgnore]
        public virtual LFile_PERS PERS { get; set; }
    }

    public class LFile_DOST_P
    {
        [Key, XmlIgnore]
        public int Dost_PId { get; set; }
        [ForeignKey("PERS"),XmlIgnore]
        public int PersId { get; set; }
        [XmlText(typeof(int))]
        public int DOST_P { get; set; }
        [XmlIgnore]
        public virtual LFile_PERS PERS { get; set; }
    }
}
