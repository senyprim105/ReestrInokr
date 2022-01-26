using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Reestr2.Model;

namespace Reestr2
{
    static public class LoadXML
    {
        //----------------------------------------------------------------------
        public static LCode LCode(XDocument doc,DateTime lastwrite,long filesize)
        {
            LCode L = new LCode();
            L.ZGLV = LFile_ZGLV(doc.Descendants("ZGLV").First());
            L.PERS_List = LFile_PERSList(doc.Descendants("PERS"));
            L.LastWrite = lastwrite;
            L.FileSize = filesize;
            return L;
        }
        public static LFile_ZGLV LFile_ZGLV (XElement xe)
        {
            LFile_ZGLV zglv = new LFile_ZGLV();
            zglv.VERSION = xe.Element("VERSION").Value;
            zglv.DATA = DateTime.ParseExact(xe.Element("DATA").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            zglv.FILENAME=xe.Element("FILENAME").Value;
            zglv.FILENAME1 = xe.Element("FILENAME1").Value;
            return zglv;
        }
        public static List<LFile_PERS> LFile_PERSList(IEnumerable< XElement> xe)
        {
            List<LFile_PERS> result = new List<LFile_PERS>();
            foreach(XElement pers in xe)
                result.Add(LFile_PERS(pers));
            return result;
        }
        public static LFile_PERS LFile_PERS(XElement xe)
        {
            LFile_PERS result = new LFile_PERS();
            result.ID_PAC = xe.Element("ID_PAC")?.Value;
            result.FAM = xe.Element("FAM")?.Value;
            result.IM = xe.Element("IM")?.Value;
            result.OT = xe.Element("OT")?.Value;
            result.W = int.Parse(xe.Element("W").Value);
            result.DR = DateTime.ParseExact(xe.Element("DR").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            result.DOSTS = xe.Elements("DOST")?.Select(a => new LFile_DOST { DOST = int.Parse(a.Value) }).ToList<LFile_DOST>();
            result.TEL = xe.Element("TEL")?.Value;
            result.FAM_P = xe.Element("FAM_P")?.Value;
            result.IM_P = xe.Element("IM_P")?.Value;
            result.OT_P = xe.Element("OT_P")?.Value;
            var w_p = xe.Element("W_P");
            result.W_P = w_p==null?null:(int?)int.Parse(w_p.Value);
            var dr_p = xe.Element("DR_P");
            result.DR_P = (dr_p==null)? null:(DateTime?)(DateTime.ParseExact(dr_p.Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
            result.DOST_P = xe.Elements("DOST_P")?.Select(a => new LFile_DOST_P { DOST_P = int.Parse(a.Value) }).ToList<LFile_DOST_P>();
            result.MR = xe.Element("MR")?.Value;
            result.DOCTYPE = xe.Element("DOCTYPE")?.Value;
            result.DOCSER = xe.Element("DOCSER")?.Value;
            result.DOCNUM = xe.Element("DOCNUM")?.Value;
            result.SNILS = xe.Element("SNILS")?.Value;
            result.OKATOG= xe.Element("OKATOG")?.Value;
            result.OKATOP = xe.Element("OKATOP")?.Value;
            result.COMENTP = xe.Element("COMENTP")?.Value;
            Application.DoEvents();
            return result;
        }
        //---------------------------------------------------------------------
        public static HFile_ZGLV HFile_ZGLV(XElement xe)
        {
            HFile_ZGLV zglv = new HFile_ZGLV();
            zglv.VERSION = xe.Element("VERSION").Value;
            zglv.DATA = DateTime.ParseExact(xe.Element("DATA").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            zglv.FILENAME = xe.Element("FILENAME").Value;
            zglv.SD_Z = int.Parse(xe.Element("SD_Z").Value);
            return zglv;
        }

        public static HFile_SCHET HFile_SCHET(XElement xe)
        {
            HFile_SCHET ch = new HFile_SCHET();
            ch.CODE= int.Parse(xe.Element("CODE").Value);
            ch.CODE_MO = xe.Element("CODE_MO").Value;
            ch.YEAR=int.Parse(xe.Element("YEAR").Value);
            ch.MONTH = int.Parse(xe.Element("MONTH").Value);
            ch.NSCHET = xe.Element("NSCHET").Value;
            ch.DSCHET = DateTime.ParseExact(xe.Element("DSCHET").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            ch.PLAT = xe.Element("PLAT")?.Value;
            ch.SUMMAV = Decimal.Parse(xe.Element("SUMMAV").Value);
            ch.COMENTS = xe.Element("COMENTS")?.Value;
            ch.SUMMAP = xe.Element("SUMMAP")==null?null:(Decimal?) Decimal.Parse(xe.Element("SUMMAP").Value);
            ch.SANK_MEK = xe.Element("SANK_MEK") == null ? null : (Decimal?)Decimal.Parse(xe.Element("SANK_MEK").Value);
            ch.SANK_MEE = xe.Element("SANK_MEE") == null ? null : (Decimal?)Decimal.Parse(xe.Element("SANK_MEE").Value);
            ch.SANK_EKMP = xe.Element("SANK_EKMP") == null ? null : (Decimal?)Decimal.Parse(xe.Element("SANK_EKMP").Value);
            return ch;
        }

        public static HFile_PACIENT HFile_PACIENT(XElement xe)
        {
            HFile_PACIENT pac = new HFile_PACIENT();
            pac.ID_PAC= xe.Element("ID_PAC").Value;
            pac.VPOLIS = int.Parse(xe.Element("VPOLIS").Value);
            pac.SPOLIS = xe.Element("SPOLIS")?.Value;
            pac.NPOLIS = xe.Element("NPOLIS")?.Value;
            pac.ST_OKATO = xe.Element("ST_OKATO")?.Value;
            pac.SMO = xe.Element("SMO")?.Value;
            pac.SMO_OGRN = xe.Element("SMO_OGRN")?.Value;
            pac.SMO_OK = xe.Element("SMO_OK")?.Value;
            pac.SMO_NAM = xe.Element("SMO_NAM")?.Value;
            pac.INV =  xe.Element("INV") == null ? null : (int?)int.Parse(xe.Element("INV").Value);
            pac.MSE = xe.Element("MSE") == null ? null : (int?)int.Parse(xe.Element("MSE").Value);
            pac.NOVOR = xe.Element("NOVOR").Value;
            pac.VNOV_D = xe.Element("VNOV_D") == null ? null : (int?)int.Parse(xe.Element("VNOV_D").Value);
            return pac;
        }

        public static HFile_Z_SL HFile_Z_SL(XElement xzsl)
            {
                List<HFile_SL> List_SL(IEnumerable<XElement> setsl)
                {
                    HFile_SL SL(XElement sluch)
                    {
                        HFile_KSG_KPG HFile_KSG_KPG(XElement xe)
                        {
                            HFile_SL_KOEF SL_KOEF(XElement x)
                            {
                                HFile_SL_KOEF r = new HFile_SL_KOEF();
                                r.IDSL = int.Parse(x.Element("IDSL").Value);
                                r.Z_SL = Decimal.Parse(x.Element("Z_SL").Value);
                                return r;
                            }
                            HFile_KSG_KPG res = new HFile_KSG_KPG();
                            res.N_KSG = xe.Element("N_KSG").Value;
                            res.VER_KSG = int.Parse(xe.Element("VER_KSG").Value);
                            res.KSG_PG = int.Parse(xe.Element("KSG_PG").Value);
                        //res.N_KPG = xe.Element("N_KPG") == null ? null : (int?)int.Parse(xe.Element("N_KPG").Value);
                            res.N_KPG = xe.Element("N_KPG")?.Value;
                            res.KOEF_Z = Decimal.Parse(xe.Element("KOEF_Z").Value);
                            res.KOEF_UP = Decimal.Parse(xe.Element("KOEF_UP").Value);
                            res.BZTSZ = Decimal.Parse(xe.Element("BZTSZ").Value);
                            res.KOEF_D = Decimal.Parse(xe.Element("KOEF_D").Value);
                            res.KOEF_U = Decimal.Parse(xe.Element("KOEF_U").Value);
                            res.CRIT = xe.Elements("CRIT")?.Select(a => new HFile_CRIT { CRIT = a.Value }).ToList<HFile_CRIT>();
                            res.SL_K = int.Parse(xe.Element("SL_K").Value);
                            res.IT_SL = xe.Element("IT_SL") == null ? null : (Decimal?)Decimal.Parse(xe.Element("IT_SL").Value);
                            res.SL_KOEF = xe.Elements("SL_KOEF").Select(a => SL_KOEF(a)).ToList<HFile_SL_KOEF>();
                            return res;
                        }

                        List<HFile_USL> HFile_USL(IEnumerable<XElement> setusl)
                        {
                            HFile_USL USL(XElement x)
                            {
                                HFile_USL usl = new HFile_USL();
                                usl.IDSERV = x.Element("IDSERV").Value;
                                usl.LPU = x.Element("LPU").Value;
                                usl.LPU_1 = x.Element("LPU_1")?.Value;
                                usl.PODR = x.Element("PODR") == null ? null : (long?)long.Parse(x.Element("PODR").Value);
                                usl.PROFIL = int.Parse(x.Element("PROFIL").Value);
                                usl.LPU_1 = x.Element("VID_VME")?.Value;
                                usl.DET = int.Parse(x.Element("DET").Value);
                                usl.DATE_IN = DateTime.ParseExact(x.Element("DATE_IN").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                usl.DATE_OUT = DateTime.ParseExact(x.Element("DATE_IN").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                usl.DS = x.Element("DS").Value;
                                usl.CODE_USL = x.Element("CODE_USL").Value;
                                usl.KOL_USL = decimal.Parse(x.Element("KOL_USL").Value);
                                usl.TARIF = x.Element("TARIF") == null ? null : (decimal?)decimal.Parse(x.Element("TARIF").Value);
                                usl.SUMV_USL = decimal.Parse(x.Element("SUMV_USL").Value);
                                usl.PRVS = int.Parse(x.Element("PRVS").Value);
                                usl.CODE_MD = x.Element("CODE_MD").Value;
                                usl.NPL = x.Element("NPL") == null ? null : (int?)int.Parse(x.Element("NPL").Value);
                                usl.COMENTU = x.Element("COMENTU")?.Value;
                                return usl;
                            }
                            return setusl.Select(a => USL(a)).ToList<HFile_USL>();
                        }

                        HFile_SL sl = new HFile_SL();
                        sl.SL_ID = sluch.Element("SL_ID").Value;
                        sl.LPU_1 = sluch.Element("LPU_1")?.Value;
                        sl.PODR = sluch.Element("PODR") == null ? null : (long?)long.Parse(sluch.Element("PODR").Value);
                        sl.PROFIL = int.Parse(sluch.Element("PROFIL").Value);
                        sl.PROFIL_K = sluch.Element("PROFIL_K") == null ? null : (int?)int.Parse(sluch.Element("PROFIL_K").Value);
                        sl.DET = int.Parse(sluch.Element("DET").Value);
                        sl.P_CEL = sluch.Element("P_CEL")?.Value;
                        sl.NHISTORY = sluch.Element("NHISTORY").Value;
                        sl.P_PER = sluch.Element("P_PER") == null ? null : (int?)int.Parse(sluch.Element("P_PER").Value);
                        sl.DATE_1 = DateTime.ParseExact(sluch.Element("DATE_1").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                        sl.DATE_2 = DateTime.ParseExact(sluch.Element("DATE_2").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                        sl.KD = sluch.Element("KD") == null ? null : (int?)int.Parse(sluch.Element("KD").Value);
                        sl.DS0 = sluch.Element("DS0")?.Value;
                        sl.DS1 = sluch.Element("DS1").Value;
                        sl.DS2 = sluch.Elements("DS2")?.Select(a => new HFile_DS2 { DS2 = a.Value }).ToList<HFile_DS2>();
                        sl.DS3 = sluch.Elements("DS3")?.Select(a => new HFile_DS3 { DS3 = a.Value }).ToList<HFile_DS3>();
                        sl.C_ZAB = sluch.Element("C_ZAB") == null ? null : (int?)int.Parse(sluch.Element("C_ZAB").Value);
                        sl.DN = sluch.Element("DN") == null ? null : (int?)int.Parse(sluch.Element("DN").Value);
                        sl.CODE_MES1 = sluch.Elements("CODE_MES1")?.Select(a => new HFile_CODE_MES1 { CODE_MES1 = a.Value }).ToList<HFile_CODE_MES1>();
                        sl.CODE_MES2 = sluch.Element("CODE_MES2")?.Value;
                        sl.KSG_KPG = sluch.Element("KSG_KPG")==null?null: HFile_KSG_KPG(sluch.Element("KSG_KPG"));
                        sl.REAB = sluch.Element("REAB") == null ? null : (int?)int.Parse(sluch.Element("REAB").Value);
                        sl.PRVS = int.Parse(sluch.Element("PRVS").Value);
                        sl.VERS_SPEC = sluch.Element("VERS_SPEC").Value;
                        sl.IDDOKT = sluch.Element("IDDOKT").Value;
                        sl.ED_COL = sluch.Element("ED_COL") == null ? null : (decimal?)decimal.Parse(sluch.Element("ED_COL").Value);
                        sl.TARIF = sluch.Element("TARIF") == null ? null : (decimal?)decimal.Parse(sluch.Element("TARIF").Value);
                        sl.SUM_M = decimal.Parse(sluch.Element("SUM_M").Value);
                        sl.USL = HFile_USL(sluch.Elements("USL"));
                        sl.COMENTSL = sluch.Element("COMENTSL")?.Value;
                        return sl;

                    }
                    return setsl.Select(a => SL(a)).ToList<HFile_SL>();
                }

                List<HFile_SANK> List_SANK(IEnumerable<XElement> xsank)
                {
                    HFile_SANK SANK(XElement xe)
                    {
                        HFile_SANK res = new HFile_SANK();
                        res.S_CODE = xe.Element("S_CODE").Value;
                        res.S_SUM = decimal.Parse(xe.Element("S_SUM").Value);
                        res.S_TIP = int.Parse(xe.Element("S_TIP").Value);
                        res.S_OSN = xe.Element("S_OSN") == null ? null : (int?)int.Parse(xe.Element("S_OSN").Value);
                        res.DATE_ACT = DateTime.ParseExact(xe.Element("DATE_ACT").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                        res.NUM_ACT = xe.Element("NUM_ACT").Value;
                        res.CODE_EXP = xe.Elements("CODE_EXP").Select(a => new HFile_CODE_EXP { CODE_EXP = a.Value }).ToList<HFile_CODE_EXP>();
                        res.S_COM = xe.Element("S_COM").Value;
                        res.S_IST = int.Parse(xe.Element("S_IST").Value);
                        return res;
                    }
                    return xsank.Select(a => SANK(a)).ToList<HFile_SANK>();
                }

                HFile_Z_SL z_sl = new HFile_Z_SL();
                z_sl.IDCASE = long.Parse(xzsl.Element("IDCASE").Value);
                z_sl.USL_OK = int.Parse(xzsl.Element("USL_OK").Value);
                z_sl.VIDPOM = int.Parse(xzsl.Element("VIDPOM").Value);
                z_sl.FOR_POM = int.Parse(xzsl.Element("FOR_POM").Value);
                z_sl.NPR_MO = xzsl.Element("NPR_MO")?.Value;
                z_sl.NPR_DATE = xzsl.Element("NPR_DATE") == null ? null : (DateTime?)DateTime.ParseExact(xzsl.Element("NPR_DATE").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                z_sl.LPU = xzsl.Element("LPU").Value;
                z_sl.DATE_Z_1 = DateTime.ParseExact(xzsl.Element("DATE_Z_1").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                z_sl.DATE_Z_2 = DateTime.ParseExact(xzsl.Element("DATE_Z_2").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                z_sl.KD_Z = xzsl.Element("KD_Z") == null ? null : (int?)int.Parse(xzsl.Element("KD_Z").Value);
                z_sl.VNOV_M = xzsl.Elements("VNOV_M")?.Select(a => new HFile_VNOV_M { VNOV_M = int.Parse(a.Value) }).ToList<HFile_VNOV_M>();
                z_sl.RSLT = int.Parse(xzsl.Element("RSLT").Value);
                z_sl.ISHOD = int.Parse(xzsl.Element("ISHOD").Value);
                z_sl.OS_SLUCH = xzsl.Elements("OS_SLUCH")?.Select(a => new HFile_OS_SLUCH { OS_SLUCH = int.Parse(a.Value) }).ToList<HFile_OS_SLUCH>();
                z_sl.VB_P = xzsl.Element("VB_P") == null ? null : (int?)int.Parse(xzsl.Element("VB_P").Value);
                z_sl.SL = List_SL(xzsl.Elements("SL"));
                z_sl.IDSP = int.Parse(xzsl.Element("IDSP").Value);
                z_sl.SUMV = decimal.Parse(xzsl.Element("SUMV").Value);
                z_sl.OPLATA = xzsl.Element("OPLATA") == null ? null : (int?)int.Parse(xzsl.Element("OPLATA").Value);
                z_sl.SUMP = xzsl.Element("SUMP") == null ? null : (decimal?)decimal.Parse(xzsl.Element("SUMP").Value);
                z_sl.SANK = List_SANK(xzsl.Elements("SANK"));
                z_sl.SANK_IT = xzsl.Element("SANK_IT") == null ? null : (decimal?)decimal.Parse(xzsl.Element("SANK_IT").Value);
                return z_sl;
            }

        public static List<HFile_ZAP> HFile_ZAP(IEnumerable<XElement> setzap)
        {
            HFile_ZAP ZAP(XElement xzap)
            {
                HFile_ZAP zap = new HFile_ZAP();
                zap.N_ZAP = int.Parse(xzap.Element("N_ZAP").Value);
                zap.PR_NOV= int.Parse(xzap.Element("PR_NOV").Value);
                zap.PACIENT = HFile_PACIENT(xzap.Element("PACIENT"));
                zap.Z_SL = HFile_Z_SL(xzap.Element("Z_SL"));
                Application.DoEvents();
                return zap;
            }
            return setzap.Select(a => ZAP(a)).ToList<HFile_ZAP>();
        }

        public static HCode HCode(XDocument doc,DateTime lastwrite,long filesize)
        {
            HCode code = new HCode();
            code.ZGLV = HFile_ZGLV(doc.Descendants("ZGLV").First());
            code.SCHET = HFile_SCHET(doc.Descendants("SCHET").First());
            code.ZAP = HFile_ZAP(doc.Descendants("ZAP"));
            code.LastWrite = lastwrite;
            code.FileSize = filesize;
            return code;
        }

        //------------------------------------------------------------------------------------------
        public static PCode PCode(XDocument doc,DateTime lastwrite,long filesize)
        {
            PFile_ZGLV ZGLV(XElement xzglv)
            {
                PFile_ZGLV zglv = new PFile_ZGLV();
                zglv.DATA = DateTime.ParseExact(xzglv.Element("DATA").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                zglv.FILENAME = xzglv.Element("FILENAME").Value;
                zglv.FILENAME1 = xzglv.Element("FILENAME1").Value;
                return zglv;
            }
            PFile_SCHET SCHET(XElement xschet)
            {
                PFile_SCHET schet = new PFile_SCHET();
                schet.YEAR = xschet.Element("YEAR").Value;
                schet.MONTH = xschet.Element("MONTH").Value;
                schet.CODE_MO=xschet.Element("CODE_MO").Value;
                schet.LPU= int.Parse(xschet.Element("LPU").Value);
                return schet;
            }
            PFile_SL SL(XElement xsl)
            {
                PFile_USL USL(XElement xusl)
                {
                    PFile_USL usl = new PFile_USL();
                    usl.IDSERV = xusl.Element("IDSERV").Value;
                    usl.EXECUTOR = xusl.Element("EXECUTOR").Value;
                    usl.EX_SPEC = xusl.Element("EX_SPEC")?.Value;
                    var rl = xusl.Elements("RL");
                    usl.RL = rl == null ? null : rl.Select(a => new PFile_RL
                    {
                        IDRL = int.Parse(a.Element("IDRL").Value),
                        NAME_MNN = a.Element("NAME_MNN")?.Value,
                        ID_ATX = a.Element("ID_ATX")?.Value
                    }).ToList<PFile_RL>();
                    return usl;
                }
                PFile_STOM STOM(XElement xstom)
                {
                    PFile_STOM stom = new PFile_STOM();
                    stom.IDSTOM = long.Parse(xstom.Element("IDSTOM").Value);
                    stom.CODE_USL = xstom.Element("CODE_USL").Value;
                    stom.ZUB=xstom.Element("ZUB")?.Value;
                    stom.KOL_VIZ = int.Parse(xstom.Element("KOL_VIZ").Value);
                    stom.UET_FAKT = decimal.Parse(xstom.Element("UET_FAKT").Value);
                    return stom;
                }
                PFile_VIZOV VIZOV(XElement xvizov)
                {
                    PFile_VIZOV vizov = new PFile_VIZOV();
                    vizov.IDVIZOV = long.Parse(xvizov.Element("IDVIZOV").Value);
                    vizov.INC_TIME = DateTime.ParseExact(xvizov.Element("INC_TIME").Value, "s", System.Globalization.CultureInfo.InvariantCulture);
                    vizov.BR_TIME = DateTime.ParseExact(xvizov.Element("BR_TIME").Value, "s", System.Globalization.CultureInfo.InvariantCulture);
                    vizov.ARR_TIME = DateTime.ParseExact(xvizov.Element("ARR_TIME").Value, "s", System.Globalization.CultureInfo.InvariantCulture);
                    XElement temp = xvizov.Element("GO_TIME");
                    vizov.GO_TIME = temp == null?null:(DateTime?) DateTime.ParseExact(temp.Value, "s", System.Globalization.CultureInfo.InvariantCulture);
                    temp= xvizov.Element("MO_TIME");
                    vizov.MO_TIME = temp == null ? null : (DateTime?)DateTime.ParseExact(temp.Value, "s", System.Globalization.CultureInfo.InvariantCulture);
                    vizov.END_TIME = DateTime.ParseExact(xvizov.Element("END_TIME").Value, "s", System.Globalization.CultureInfo.InvariantCulture);
                    vizov.PLACE = int.Parse(xvizov.Element("PLACE").Value);
                    temp=xvizov.Element("OKTMO");
                    vizov.OKTMO = temp == null ? null : (long?)long.Parse(temp.Value);
                    temp = xvizov.Element("ZONA");
                    vizov.ZONA = temp == null ? null : (int?)int.Parse(temp.Value);
                    return vizov;
                }
                PFile_SL sl = new PFile_SL();
                sl.SL_ID= xsl.Element("SL_ID").Value;
                sl.IDCASE= int.Parse(xsl.Element("IDCASE").Value);
                sl.CARD = int.Parse(xsl.Element("CARD").Value);
                sl.FROM_FIRM = xsl.Element("FROM_FIRM") == null ? null : (int?)int.Parse(xsl.Element("FROM_FIRM").Value);
                sl.PURP = int.Parse(xsl.Element("PURP").Value);
                sl.VISIT_POL = int.Parse(xsl.Element("VISIT_POL").Value);
                sl.VISIT_HOM = int.Parse(xsl.Element("VISIT_HOM").Value);
                sl.NSNDHOSP = xsl.Element("NSNDHOSP")?.Value;
                sl.SPECFIC = int.Parse(xsl.Element("SPECFIC").Value);
                sl.TYPE_PAY = int.Parse(xsl.Element("TYPE_PAY").Value);
                sl.D_TYPE = xsl.Element("D_TYPE")?.Value;
                sl.NAPRLECH = xsl.Elements("NAPRLECH").Select(a => new PFile_NAPRLECH { NAPRLECH = a.Value }).ToList<PFile_NAPRLECH>();
                var set = xsl.Elements("USL") ;
                sl.USL = set==null?null:set.Select(a=>USL(a)).ToList<PFile_USL>();
                set = xsl.Elements("STOM");
                sl.STOM = set == null ? null : set.Select(a => STOM(a)).ToList<PFile_STOM>();
                set = xsl.Elements("VIZOV");
                sl.VIZOV = set == null ? null : set.Select(a => VIZOV(a)).ToList<PFile_VIZOV>();
                Application.DoEvents();
                return sl;
            }

            PCode P = new PCode();
            P.ZGLV =  ZGLV(doc.Element("ZL_LIST").Element("ZGLV"));
            P.SCHET = SCHET(doc.Element("ZL_LIST").Element("SCHET"));
            P.SL = doc.Element("ZL_LIST").Elements("SL").Select(a => SL(a)).ToList<PFile_SL>();
            P.LastWrite = lastwrite;
            P.FileSize = filesize;
            return P;
        }

        //------------------------------------------------------------------------------------------
        public static SCode SCode(XDocument doc,DateTime lastwrite,long filesize)
        {
            SFile_ZGLV ZGLV(XElement xzglv)
            {
                SFile_ZGLV zglv = new SFile_ZGLV();
                zglv.DATA = DateTime.ParseExact(xzglv.Element("DATA").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                zglv.FILENAME = xzglv.Element("FILENAME").Value;
                zglv.FILENAME1 = xzglv.Element("FILENAME1").Value;
                return zglv;
            }
            SFile_SCHET SCHET(XElement xschet)
            {
                SFile_SCHET schet = new SFile_SCHET();
                schet.YEAR = xschet.Element("YEAR").Value;
                schet.MONTH = xschet.Element("MONTH").Value;
                schet.CODE_MO = xschet.Element("CODE_MO").Value;
                schet.LPU = int.Parse(xschet.Element("LPU").Value);
                return schet;
            }
            SFile_SL SL(XElement xsl)
            {
                SFile_USL USL(XElement xusl)
                {
                    SFile_USL usl = new SFile_USL();
                    usl.IDSERV = xusl.Element("IDSERV").Value;
                    usl.PR_USL = xusl.Element("PR_USL")== null ? null : (int?)int.Parse(xusl.Element("PR_USL").Value);
                    var rl = xusl.Elements("RL");
                    usl.RL = rl == null ? null : rl.Select(a => new SFile_RL
                    {
                        IDRL = int.Parse(a.Element("IDRL").Value),
                        NAME_MNN = a.Element("NAME_MNN")?.Value,
                        ID_ATX = a.Element("ID_ATX")?.Value,
                        ID_MI = a.Element("ID_MI").Value == null ? null : (int?)int.Parse(a.Element("ID_MI").Value),
                        KOL_MI = a.Element("KOL_MI").Value == null ? null : (int?)int.Parse(a.Element("KOL_MI").Value)
                    }).ToList<SFile_RL>();
                    return usl;
                }
                SFile_SL sl = new SFile_SL();
                sl.SL_ID = xsl.Element("SL_ID").Value;
                sl.IDCASE = long.Parse(xsl.Element("IDCASE").Value);
                sl.NSNDHOSP = xsl.Element("NSNDHOSP")?.Value;
                sl.REN = xsl.Element("REN") == null ? null : (int?)int.Parse(xsl.Element("REN").Value);
                sl.SOFA = xsl.Element("SOFA") == null ? null : (int?)int.Parse(xsl.Element("SOFA").Value);
                sl.PARENT = xsl.Element("PARENT") == null ? null : (int?)int.Parse(xsl.Element("PARENT").Value);
                sl.D_TYPE = xsl.Element("D_TYPE")?.Value;
                sl.FROM_FIRM = xsl.Element("FROM_FIRM") == null ? null : (int?)int.Parse(xsl.Element("FROM_FIRM").Value);
                var set = xsl.Elements("DS1_M");
                sl.DS1_M = set == null ? null : set.Select(a => new SFile_DS1_M {DS1_M=a.Value }).ToList<SFile_DS1_M>();
                set = xsl.Elements("USL");
                sl.USL = set == null ? null : set.Select(a => USL(a)).ToList<SFile_USL>();
                Application.DoEvents();
                return sl;
            }

            SCode S = new SCode();
            S.ZGLV = ZGLV(doc.Element("ZL_LIST").Element("ZGLV"));
            S.SCHET = SCHET(doc.Element("ZL_LIST").Element("SCHET"));
            S.SL = doc.Element("ZL_LIST").Elements("SL").Select(a => SL(a)).ToList<SFile_SL>();
            S.LastWrite = lastwrite;
            S.FileSize = filesize;
            return S;
        }
        //------------------------------------------------------------------------------------------
        /*
        public static VD VERR(XDocument doc)
        {
             VERR_FLK_P FLK_P(XElement xflk)
            {
                VERR_SCHET SCHET(XElement xschet)
                {
                    VERR_SCHET schet = new VERR_SCHET();
                    schet.CODE = int.Parse(xschet.Element("CODE").Value);
                    schet.CODE_MO = xschet.Element("CODE_MO").Value;
                    schet.YEAR = int.Parse(xschet.Element("YEAR").Value);
                    schet.MONTH = int.Parse(xschet.Element("MONTH").Value);
                    schet.NSCHET = xschet.Element("NSCHET").Value;
                    schet.DSCHET= DateTime.ParseExact(xschet.Element("DSCHET").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    return schet;
                }
                VERR_ZAP ZAP(XElement xzap)
                {
                    VERR_SL SL(XElement xsl)
                    {
                        VERR_OTKAZ OTKAZ(XElement x)
                        {
                            VERR_OTKAZ otkaz = new VERR_OTKAZ();
                            otkaz.I_TYPE = int.Parse(x.Element("I_TYPE").Value);
                            otkaz.COMMENT = x.Element("COMMENT")?.Value;
                            return otkaz;
                        }
                        VERR_SL sl = new VERR_SL();
                        //sl.SL_ID= xsl.Element("SL_ID").Value;
                        sl.IDCASE = long.Parse(xsl.Element("IDCASE").Value);
                        sl.NHISTORY = xsl.Element("NHISTORY")?.Value;
                        //sl.CARD= int.Parse(xsl.Element("CARD").Value);
                        sl.SMO = xsl.Element("C_INSUR")?.Value;
                        sl.SMO_FOND = xsl.Element("INSURBASA")?.Value;
                        sl.OTKAZ = xsl.Elements("OTKAZ").Select(a => OTKAZ(a)).ToList<VERR_OTKAZ>();
                        return sl;
                    }
                    VERR_ZAP zap = new VERR_ZAP();
                    zap.N_ZAP= int.Parse(xzap.Element("N_ZAP").Value);
                    zap.SL = xzap.Elements("SLUCH").Select(a => SL(a)).ToList<VERR_SL>();
                    return zap;
                }
                VERR_FLK_P flk = new VERR_FLK_P();
                flk.FNAME = xflk.Element("FNAME").Value;
                flk.FNAME_I = xflk.Element("FNAME_I").Value;
                flk.SCHET = SCHET(xflk.Element("SCHET"));
                flk.ZAP = xflk.Elements("ZAP").Select(a => ZAP(a)).ToList<VERR_ZAP>();
                return flk;
            }
            return FLK_P(doc.Element("FLK_P"));

        }
        //------------------------------------------------------------------------------------------
        
    */
        

        

        

      

        public static CCode CCode(XDocument doc, DateTime lastwrite, long filesize)
        {
            CFile_ZGLV CFile_ZGLV(XElement xzglv)
            {
                CFile_ZGLV zglv = new CFile_ZGLV();
                zglv.VERSION = xzglv.Element("VERSION").Value;
                zglv.DATA = DateTime.ParseExact(xzglv.Element("DATA").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                zglv.FILENAME = xzglv.Element("FILENAME").Value;
                zglv.SD_Z = int.Parse(xzglv.Element("SD_Z").Value);
                return zglv;
            }
            CFile_SCHET CFile_SCHET(XElement xschet)
            {
                CFile_SCHET schet = new CFile_SCHET();
                schet.CODE = int.Parse(xschet.Element("CODE").Value);
                schet.CODE_MO = xschet.Element("CODE_MO").Value;
                schet.YEAR = int.Parse(xschet.Element("YEAR").Value);
                schet.MONTH = int.Parse(xschet.Element("MONTH").Value);
                schet.NSCHET = xschet.Element("NSCHET").Value;
                schet.DSCHET = DateTime.ParseExact(xschet.Element("DSCHET").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                schet.PLAT = xschet.Element("PLAT")?.Value;
                schet.SUMMAV = Decimal.Parse(xschet.Element("SUMMAV").Value);
                schet.COMENTS = xschet.Element("COMENTS")?.Value;
                schet.SUMMAP = xschet.Element("SUMMAP") == null ? null : (Decimal?)Decimal.Parse(xschet.Element("SUMMAP").Value);
                schet.SANK_MEK = xschet.Element("SANK_MEK") == null ? null : (Decimal?)Decimal.Parse(xschet.Element("SANK_MEK").Value);
                schet.SANK_MEE = xschet.Element("SANK_MEE") == null ? null : (Decimal?)Decimal.Parse(xschet.Element("SANK_MEE").Value);
                schet.SANK_EKMP = xschet.Element("SANK_EKMP") == null ? null : (Decimal?)Decimal.Parse(xschet.Element("SANK_EKMP").Value);
                return schet;
            }
            CFile_ZAP ZAP(XElement xzap)
            {
                CFile_PACIENT CFile_PACIENT(XElement xpacient)
                {
                    CFile_PACIENT pacient = new CFile_PACIENT();
                    pacient.ID_PAC = xpacient.Element("ID_PAC").Value;
                    pacient.VPOLIS = int.Parse(xpacient.Element("VPOLIS").Value);
                    pacient.SPOLIS = xpacient.Element("SPOLIS")?.Value;
                    pacient.NPOLIS = xpacient.Element("NPOLIS")?.Value;
                    pacient.ST_OKATO = xpacient.Element("ST_OKATO")?.Value;
                    pacient.SMO = xpacient.Element("SMO")?.Value;
                    pacient.SMO_OGRN = xpacient.Element("SMO_OGRN")?.Value;
                    pacient.SMO_OK = xpacient.Element("SMO_OK")?.Value;
                    pacient.SMO_NAM = xpacient.Element("SMO_NAM")?.Value;
                    pacient.INV = xpacient.Element("INV") == null ? null : (int?)int.Parse(xpacient.Element("INV").Value);
                    pacient.MSE = xpacient.Element("MSE") == null ? null : (int?)int.Parse(xpacient.Element("MSE").Value);
                    pacient.NOVOR = xpacient.Element("NOVOR").Value;
                    pacient.VNOV_D = xpacient.Element("VNOV_D") == null ? null : (int?)int.Parse(xpacient.Element("VNOV_D").Value);
                    return pacient;
                }

                CFile_Z_SL CFile_Z_SL(XElement xz_sl)
                {
                    List<CFile_SL> List_SL(IEnumerable<XElement> setsl)
                    {
                        CFile_SL SL(XElement xsl)
                        {
                            CFile_NAPR NAPR(XElement xnapr)
                            {
                                CFile_NAPR napr = new CFile_NAPR();
                                napr.NAPR_DATE= DateTime.ParseExact(xnapr.Element("NAPR_DATE").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                napr.NAPR_MO = xnapr.Element("NAPR_MO")?.Value;
                                napr.NAPR_V = int.Parse(xnapr.Element("NAPR_V").Value);
                                napr.MET_ISSL= xnapr.Element("MET_ISSL") == null ? null : (int?)int.Parse(xnapr.Element("MET_ISSL").Value);
                                napr.NAPR_USL = xnapr.Element("NAPR_USL")?.Value;
                                return napr;
                            }
                            CFile_CONS CONS(XElement xcons)
                            {
                                CFile_CONS cons = new CFile_CONS();
                                cons.PR_CONS = int.Parse(xcons.Element("PR_CONS").Value);
                                cons.DT_CONS= xcons.Element("DT_CONS") == null ? null : (DateTime?)DateTime.ParseExact(xz_sl.Element("DT_CONS").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                return cons;
                            }


                            CFile_ONK_SL ONK_SL(XElement xonk_sl)
                            {
                                CFile_B_DIAG B_DIAG(XElement xb_diag)
                                {
                                    CFile_B_DIAG b_diag = new CFile_B_DIAG();
                                    b_diag.DIAG_DATE= DateTime.ParseExact(xz_sl.Element("DIAG_DATE").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                    b_diag.DIAG_TIP=int.Parse(xb_diag.Element("DIAG_TIP").Value);
                                    b_diag.DIAG_CODE=int.Parse(xb_diag.Element("DIAG_CODE").Value);
                                    b_diag.DIAG_RSLT = xb_diag.Element("DIAG_RSLT") == null ? null : (int?)int.Parse(xb_diag.Element("DIAG_RSLT").Value);
                                    b_diag.REC_RSLT=xb_diag.Element("REC_RSLT") == null ? null : (int?)int.Parse(xb_diag.Element("REC_RSLT").Value);
                                    return b_diag;
                                }
                                CFile_B_PROT B_PROT(XElement xb_prot)
                                {
                                    CFile_B_PROT b_prot = new CFile_B_PROT();
                                    b_prot.PROT = int.Parse(xb_prot.Element("PROT").Value);
                                    b_prot.D_PROT= DateTime.ParseExact(xb_prot.Element("D_PROT").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                    return b_prot;
                                }
                                CFile_ONK_USL ONK_USL(XElement xonk_usl)
                                {
                                    CFile_DATE_INJ DATE_INJ(XElement xdate_inj)
                                    {
                                        CFile_DATE_INJ date_inj = new CFile_DATE_INJ();
                                        date_inj.DATE_INJ = DateTime.ParseExact(xdate_inj.Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                        return date_inj;
                                    }
                                    CFile_LEK_PR LEK_PR(XElement xlek_pr)
                                    {
                                        CFile_LEK_PR lek_pr = new CFile_LEK_PR();
                                        lek_pr.REGNUM = xlek_pr.Element("REGNUM").Value;
                                        lek_pr.DATE_INJ = xlek_pr.Elements("DATE_INJ").Select(a =>DATE_INJ(a)).ToList<CFile_DATE_INJ>();
                                        return lek_pr;
                                    }
                                    CFile_ONK_USL onk_usl = new CFile_ONK_USL();
                                    onk_usl.USL_TIP= int.Parse(xonk_usl.Element("USL_TIP").Value);
                                    onk_usl.HIR_TIP = xonk_usl.Element("HIR_TIP") == null ? null : (int?)int.Parse(xonk_usl.Element("HIR_TIP").Value);
                                    onk_usl.LEK_TIP_L = xonk_usl.Element("LEK_TIP_L") == null ? null : (int?)int.Parse(xonk_usl.Element("LEK_TIP_L").Value);
                                    onk_usl.LEK_TIP_V = xonk_usl.Element("LEK_TIP_V") == null ? null : (int?)int.Parse(xonk_usl.Element("LEK_TIP_V").Value);
                                    onk_usl.LEK_PR = xonk_usl.Elements("LEK_PR")?.Select(a => LEK_PR(a)).ToList<CFile_LEK_PR>();
                                    onk_usl.LUCH_TIP= xonk_usl.Element("LUCH_TIP") == null ? null : (int?)int.Parse(xonk_usl.Element("LUCH_TIP").Value);
                                    return onk_usl;
                                }


                                CFile_ONK_SL onk_sl = new CFile_ONK_SL();
                                onk_sl.DS1_T = int.Parse(xonk_sl.Element("DS1_T").Value);
                                onk_sl.STAD=int.Parse(xonk_sl.Element("STAD").Value);
                                onk_sl.ONK_T=int.Parse(xonk_sl.Element("ONK_T").Value);
                                onk_sl.ONK_N=int.Parse(xonk_sl.Element("ONK_N").Value);
                                onk_sl.ONK_M = int.Parse(xonk_sl.Element("ONK_M").Value);
                                onk_sl.MTSTZ= xonk_sl.Element("MTSTZ") == null ? null : (int?)int.Parse(xonk_sl.Element("MTSTZ").Value);
                                onk_sl.SOD= xonk_sl.Element("SOD") == null ? null : (Decimal?)Decimal.Parse(xonk_sl.Element("SOD").Value);
                                onk_sl.B_DIAG = xonk_sl.Elements("B_DIAG")?.Select(a => B_DIAG(a)).ToList<CFile_B_DIAG>();
                                onk_sl.B_PROT = xonk_sl.Elements("B_PROT")?.Select(a => B_PROT(a)).ToList<CFile_B_PROT>();
                                onk_sl.ONK_USL = xonk_sl.Elements("ONK_USL")?.Select(a => ONK_USL(a)).ToList<CFile_ONK_USL>();
                                return onk_sl;

                            }

                            CFile_KSG_KPG CFile_KSG_KPG(XElement xksg_kpg)
                            {
                                CFile_SL_KOEF SL_KOEF(XElement xsl_koef)
                                {
                                    CFile_SL_KOEF sl_koef = new CFile_SL_KOEF();
                                    sl_koef.IDSL = int.Parse(xsl_koef.Element("IDSL").Value);
                                    sl_koef.Z_SL = Decimal.Parse(xsl_koef.Element("Z_SL").Value);
                                    return sl_koef;
                                }
                                CFile_KSG_KPG ksg_kpg = new CFile_KSG_KPG();
                                ksg_kpg.N_KSG = xksg_kpg.Element("N_KSG").Value;
                                ksg_kpg.VER_KSG = int.Parse(xksg_kpg.Element("VER_KSG").Value);
                                ksg_kpg.KSG_PG = int.Parse(xksg_kpg.Element("KSG_PG").Value);
                                ksg_kpg.N_KPG = xksg_kpg.Element("N_KPG")?.Value;
                                ksg_kpg.KOEF_Z = Decimal.Parse(xksg_kpg.Element("KOEF_Z").Value);
                                ksg_kpg.KOEF_UP = Decimal.Parse(xksg_kpg.Element("KOEF_UP").Value);
                                ksg_kpg.BZTSZ = Decimal.Parse(xksg_kpg.Element("BZTSZ").Value);
                                ksg_kpg.KOEF_D = Decimal.Parse(xksg_kpg.Element("KOEF_D").Value);
                                ksg_kpg.KOEF_U = Decimal.Parse(xksg_kpg.Element("KOEF_U").Value);
                                ksg_kpg.CRIT = xksg_kpg.Elements("CRIT")?.Select(a => new CFile_CRIT { CRIT = a.Value }).ToList<CFile_CRIT>();
                                ksg_kpg.SL_K = int.Parse(xksg_kpg.Element("SL_K").Value);
                                ksg_kpg.IT_SL = xksg_kpg.Element("IT_SL") == null ? null : (Decimal?)Decimal.Parse(xksg_kpg.Element("IT_SL").Value);
                                ksg_kpg.SL_KOEF = xksg_kpg.Elements("SL_KOEF").Select(a => SL_KOEF(a)).ToList<CFile_SL_KOEF>();
                                return ksg_kpg;
                            }

                            List<CFile_USL> CFile_USL(IEnumerable<XElement> setusl)
                            {
                                CFile_USL USL(XElement xusl)
                                {
                                    CFile_USL usl = new CFile_USL();
                                    usl.IDSERV = xusl.Element("IDSERV").Value;
                                    usl.LPU = xusl.Element("LPU").Value;
                                    usl.LPU_1 = xusl.Element("LPU_1")?.Value;
                                    usl.PODR = xusl.Element("PODR") == null ? null : (long?)long.Parse(xusl.Element("PODR").Value);
                                    usl.PROFIL = int.Parse(xusl.Element("PROFIL").Value);
                                    usl.LPU_1 = xusl.Element("VID_VME")?.Value;
                                    usl.DET = int.Parse(xusl.Element("DET").Value);
                                    usl.DATE_IN = DateTime.ParseExact(xusl.Element("DATE_IN").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                    usl.DATE_OUT = DateTime.ParseExact(xusl.Element("DATE_IN").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                    usl.DS = xusl.Element("DS").Value;
                                    usl.CODE_USL = xusl.Element("CODE_USL").Value;
                                    usl.KOL_USL = decimal.Parse(xusl.Element("KOL_USL").Value);
                                    usl.TARIF = xusl.Element("TARIF") == null ? null : (decimal?)decimal.Parse(xusl.Element("TARIF").Value);
                                    usl.SUMV_USL = decimal.Parse(xusl.Element("SUMV_USL").Value);
                                    usl.PRVS = int.Parse(xusl.Element("PRVS").Value);
                                    usl.CODE_MD = xusl.Element("CODE_MD").Value;
                                    usl.NPL = xusl.Element("NPL") == null ? null : (int?)int.Parse(xusl.Element("NPL").Value);
                                    usl.COMENTU = xusl.Element("COMENTU")?.Value;
                                    return usl;
                                }
                                return setusl.Select(a => USL(a)).ToList<CFile_USL>();
                            }

                            CFile_SL sl = new CFile_SL();
                            sl.SL_ID = xsl.Element("SL_ID").Value;
                            sl.LPU_1 = xsl.Element("LPU_1")?.Value;
                            sl.PODR = xsl.Element("PODR") == null ? null : (long?)long.Parse(xsl.Element("PODR").Value);
                            sl.PROFIL = int.Parse(xsl.Element("PROFIL").Value);
                            sl.PROFIL_K = xsl.Element("PROFIL_K") == null ? null : (int?)int.Parse(xsl.Element("PROFIL_K").Value);
                            sl.DET = int.Parse(xsl.Element("DET").Value);
                            sl.P_CEL = xsl.Element("P_CEL")?.Value;
                            sl.NHISTORY = xsl.Element("NHISTORY").Value;
                            sl.P_PER = xsl.Element("P_PER") == null ? null : (int?)int.Parse(xsl.Element("P_PER").Value);
                            sl.DATE_1 = DateTime.ParseExact(xsl.Element("DATE_1").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                            sl.DATE_2 = DateTime.ParseExact(xsl.Element("DATE_2").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                            sl.KD = xsl.Element("KD") == null ? null : (int?)int.Parse(xsl.Element("KD").Value);
                            sl.DS0 = xsl.Element("DS0")?.Value;
                            sl.DS1 = xsl.Element("DS1").Value;
                            sl.DS2 = xsl.Elements("DS2")?.Select(a => new CFile_DS2 { DS2 = a.Value }).ToList<CFile_DS2>();
                            sl.DS3 = xsl.Elements("DS3")?.Select(a => new CFile_DS3 { DS3 = a.Value }).ToList<CFile_DS3>();
                            sl.C_ZAB = xsl.Element("C_ZAB") == null ? null : (int?)int.Parse(xsl.Element("C_ZAB").Value);
                            sl.DS_ONK = int.Parse(xsl.Element("DS_ONK").Value);//C
                            sl.DN = xsl.Element("DN") == null ? null : (int?)int.Parse(xsl.Element("DN").Value);
                            sl.CODE_MES1 = xsl.Elements("CODE_MES1")?.Select(a => new CFile_CODE_MES1 { CODE_MES1 = a.Value }).ToList<CFile_CODE_MES1>();
                            sl.CODE_MES2 = xsl.Element("CODE_MES2")?.Value;
                            sl.NAPR = xsl.Elements("NAPR")?.Select(a => NAPR(a)).ToList<CFile_NAPR>();//C
                            sl.CONS=xsl.Elements("CONS").Select(a => CONS(a)).ToList<CFile_CONS>();//C
                            sl.ONK_SL = xsl.Element("ONK_SL")==null?null:ONK_SL(xsl.Element("ONK_SL"));//C
                            sl.KSG_KPG = xsl.Element("KSG_KPG") == null ? null : CFile_KSG_KPG(xsl.Element("KSG_KPG"));
                            sl.REAB = xsl.Element("REAB") == null ? null : (int?)int.Parse(xsl.Element("REAB").Value);
                            sl.PRVS = int.Parse(xsl.Element("PRVS").Value);
                            sl.VERS_SPEC = xsl.Element("VERS_SPEC").Value;
                            sl.IDDOKT = xsl.Element("IDDOKT").Value;
                            sl.ED_COL = xsl.Element("ED_COL") == null ? null : (decimal?)decimal.Parse(xsl.Element("ED_COL").Value);
                            sl.TARIF = xsl.Element("TARIF") == null ? null : (decimal?)decimal.Parse(xsl.Element("TARIF").Value);
                            sl.SUM_M = decimal.Parse(xsl.Element("SUM_M").Value);
                            sl.USL = CFile_USL(xsl.Elements("USL"));
                            sl.COMENTSL = xsl.Element("COMENTSL")?.Value;
                            return sl;

                        }
                        return setsl.Select(a => SL(a)).ToList<CFile_SL>();
                    }

                    List<CFile_SANK> List_SANK(IEnumerable<XElement> setsank)
                    {
                        CFile_SANK SANK(XElement xsank)
                        {
                            CFile_SANK sank = new CFile_SANK();
                            sank.S_CODE = xsank.Element("S_CODE").Value;
                            sank.S_SUM = decimal.Parse(xsank.Element("S_SUM").Value);
                            sank.S_TIP = int.Parse(xsank.Element("S_TIP").Value);
                            sank.S_OSN = xsank.Element("S_OSN") == null ? null : (int?)int.Parse(xsank.Element("S_OSN").Value);
                            sank.DATE_ACT = DateTime.ParseExact(xsank.Element("DATE_ACT").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                            sank.NUM_ACT = xsank.Element("NUM_ACT").Value;
                            sank.CODE_EXP = xsank.Elements("CODE_EXP").Select(a => new CFile_CODE_EXP { CODE_EXP = a.Value }).ToList<CFile_CODE_EXP>();
                            sank.S_COM = xsank.Element("S_COM").Value;
                            sank.S_IST = int.Parse(xsank.Element("S_IST").Value);
                            return sank;
                        }
                        return setsank.Select(a => SANK(a)).ToList<CFile_SANK>();
                    }

                    CFile_Z_SL z_sl = new CFile_Z_SL();
                    z_sl.IDCASE = long.Parse(xz_sl.Element("IDCASE").Value);
                    z_sl.USL_OK = int.Parse(xz_sl.Element("USL_OK").Value);
                    z_sl.VIDPOM = int.Parse(xz_sl.Element("VIDPOM").Value);
                    z_sl.FOR_POM = int.Parse(xz_sl.Element("FOR_POM").Value);
                    z_sl.NPR_MO = xz_sl.Element("NPR_MO")?.Value;
                    z_sl.NPR_DATE = xz_sl.Element("NPR_DATE") == null ? null : (DateTime?)DateTime.ParseExact(xz_sl.Element("NPR_DATE").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    z_sl.LPU = xz_sl.Element("LPU").Value;
                    z_sl.DATE_Z_1 = DateTime.ParseExact(xz_sl.Element("DATE_Z_1").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    z_sl.DATE_Z_2 = DateTime.ParseExact(xz_sl.Element("DATE_Z_2").Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    z_sl.KD_Z = xz_sl.Element("KD_Z") == null ? null : (int?)int.Parse(xz_sl.Element("KD_Z").Value);
                    z_sl.VNOV_M = xz_sl.Elements("VNOV_M")?.Select(a => new CFile_VNOV_M { VNOV_M = int.Parse(a.Value) }).ToList<CFile_VNOV_M>();
                    z_sl.RSLT = int.Parse(xz_sl.Element("RSLT").Value);
                    z_sl.ISHOD = int.Parse(xz_sl.Element("ISHOD").Value);
                    z_sl.OS_SLUCH = xz_sl.Elements("OS_SLUCH")?.Select(a => new CFile_OS_SLUCH { OS_SLUCH = int.Parse(a.Value) }).ToList<CFile_OS_SLUCH>();
                    z_sl.VB_P = xz_sl.Element("VB_P") == null ? null : (int?)int.Parse(xz_sl.Element("VB_P").Value);
                    z_sl.SL = List_SL(xz_sl.Elements("SL"));
                    z_sl.IDSP = int.Parse(xz_sl.Element("IDSP").Value);
                    z_sl.SUMV = decimal.Parse(xz_sl.Element("SUMV").Value);
                    z_sl.OPLATA = xz_sl.Element("OPLATA") == null ? null : (int?)int.Parse(xz_sl.Element("OPLATA").Value);
                    z_sl.SUMP = xz_sl.Element("SUMP") == null ? null : (decimal?)decimal.Parse(xz_sl.Element("SUMP").Value);
                    z_sl.SANK = List_SANK(xz_sl.Elements("SANK"));
                    z_sl.SANK_IT = xz_sl.Element("SANK_IT") == null ? null : (decimal?)decimal.Parse(xz_sl.Element("SANK_IT").Value);
                    return z_sl;
                }


                CFile_ZAP zap = new CFile_ZAP();
                zap.N_ZAP = int.Parse(xzap.Element("N_ZAP").Value);
                zap.PR_NOV = int.Parse(xzap.Element("PR_NOV").Value);
                zap.PACIENT = CFile_PACIENT(xzap.Element("PACIENT"));
                zap.Z_SL = CFile_Z_SL(xzap.Element("Z_SL"));
                Application.DoEvents();
                return zap;
            }

            CCode code = new CCode();
            code.ZGLV = CFile_ZGLV(doc.Descendants("ZGLV").First());
            code.SCHET = CFile_SCHET(doc.Descendants("SCHET").First());
            code.ZAP = doc.Descendants("ZAP").Select(a => ZAP(a)).ToList<CFile_ZAP>(); ;
            code.LastWrite = lastwrite;
            code.FileSize = filesize;
            return code;
        }
    }
}
