namespace Reestr2
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Reestr2.Model;

    public class reestr : DbContext
    {
        protected int UsingCount = 0;
        public static reestr GetContext(reestr context)
        {
            if (context != null)
            {
                context.UsingCount++;
            }
            else
            {
                context = new reestr();
            }
            return context;
        }

        protected bool MyDisposing = true;
        protected override void Dispose(bool disposing)
        {
            if (UsingCount == 0)
            {
                base.Dispose(MyDisposing);
                MyDisposing = false;
            }
            else
            {
                UsingCount--;
            }
        }

        public override int SaveChanges()
        {
            if (UsingCount == 0)
            {
                return base.SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public reestr(): base("name=reestr")
        {
            Database.SetInitializer<reestr>(new ReestrDbInit());
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
           
            modelBuilder.Entity<Packet>().HasKey(a => a.PacketId).HasMany(a => a.XmlReestrs).WithRequired(a => a.Packet).WillCascadeOnDelete(true);
            
            modelBuilder.Entity<HFile_ZGLV>().HasKey(a => a.ReestrId);
            modelBuilder.Entity<HFile_ZGLV>().HasRequired(a => a.H).WithRequiredDependent(a => a.ZGLV).WillCascadeOnDelete(true);
            modelBuilder.Entity<HFile_ZAP>().HasKey(a => a.ZapId);
            modelBuilder.Entity<HFile_Z_SL>().HasKey(a => a.ZapId);
            modelBuilder.Entity<HFile_Z_SL>().HasRequired(a => a.ZAP).WithRequiredDependent(a=>a.Z_SL).WillCascadeOnDelete(true);
            modelBuilder.Entity<HFile_ZAP>().HasKey(a => a.ZapId);
            modelBuilder.Entity<HFile_PACIENT>().HasRequired(a => a.ZAP).WithRequiredDependent(a => a.PACIENT).WillCascadeOnDelete(true);
            modelBuilder.Entity<HFile_SCHET>().HasKey(a => a.ReestrId);
            modelBuilder.Entity<HFile_SCHET>().HasRequired(a => a.H).WithRequiredDependent(a => a.SCHET).WillCascadeOnDelete(true);
            modelBuilder.Entity<HFile_KSG_KPG>().HasKey(a => a.SlId);
            modelBuilder.Entity<HFile_KSG_KPG>().HasRequired(a => a.SL).WithOptional(a => a.KSG_KPG).WillCascadeOnDelete(true);

            modelBuilder.Entity<LFile_ZGLV>().HasKey(a => a.ReestrId);
            modelBuilder.Entity<LFile_ZGLV>().HasRequired(a => a.LCode).WithRequiredDependent(a => a.ZGLV).WillCascadeOnDelete(true);

            modelBuilder.Entity<SFile_ZGLV>().HasKey(a => a.ReestrId);
            modelBuilder.Entity<SFile_SCHET>().HasRequired(a => a.SCode).WithRequiredDependent(a => a.SCHET).WillCascadeOnDelete(true);
            modelBuilder.Entity<SFile_ZGLV>().HasKey(a => a.ReestrId);
            modelBuilder.Entity<SFile_ZGLV>().HasRequired(a => a.SCode).WithRequiredDependent(a => a.ZGLV).WillCascadeOnDelete(true);

            modelBuilder.Entity<PFile_ZGLV>().HasKey(a => a.ReestId);
            modelBuilder.Entity<PFile_ZGLV>().HasRequired(a => a.PCode).WithRequiredDependent(a => a.ZGLV).WillCascadeOnDelete(true);
            modelBuilder.Entity<PFile_SCHET>().HasKey(a => a.ReestrId);
            modelBuilder.Entity<PFile_SCHET>().HasRequired(a => a.PCode).WithRequiredDependent(a => a.SCHET).WillCascadeOnDelete(true);

            modelBuilder.Entity<DFile_ZGLV>().HasKey(a => a.ReestrId);
            modelBuilder.Entity<DFile_ZGLV>().HasRequired(a => a.D).WithRequiredDependent(a => a.ZGLV).WillCascadeOnDelete(true);
            modelBuilder.Entity<DFile_ZAP>().HasKey(a => a.ZapId);
            modelBuilder.Entity<DFile_Z_SL>().HasKey(a => a.ZapId);
            modelBuilder.Entity<DFile_Z_SL>().HasRequired(a => a.ZAP).WithRequiredDependent(a => a.Z_SL).WillCascadeOnDelete(true);
            modelBuilder.Entity<DFile_ZAP>().HasKey(a => a.ZapId);
            modelBuilder.Entity<DFile_PACIENT>().HasRequired(a => a.ZAP).WithRequiredDependent(a => a.PACIENT).WillCascadeOnDelete(true);
            modelBuilder.Entity<DFile_SCHET>().HasKey(a => a.ReestrId);
            modelBuilder.Entity<DFile_SCHET>().HasRequired(a => a.D).WithRequiredDependent(a => a.SCHET).WillCascadeOnDelete(true);

            modelBuilder.Entity<FFile_ZGLV>().HasKey(a => a.ReestrId);
            modelBuilder.Entity<FFile_ZGLV>().HasRequired(a => a.FCode).WithRequiredDependent(a => a.ZGLV).WillCascadeOnDelete(true);
            modelBuilder.Entity<FFile_ZAP>().HasKey(a => a.ZapId);
            modelBuilder.Entity<FFile_PACIENT>().HasRequired(a => a.ZAP).WithRequiredDependent(a => a.PACIENT).WillCascadeOnDelete(true);
            modelBuilder.Entity<FFile_SCHET>().HasKey(a => a.ReestrId);
            modelBuilder.Entity<FFile_SCHET>().HasRequired(a => a.FCode).WithRequiredDependent(a => a.SCHET).WillCascadeOnDelete(true);

            
            modelBuilder.Entity<CFile_ZGLV>().HasKey(a => a.ReestrId);
            modelBuilder.Entity<CFile_ZGLV>().HasRequired(a => a.C).WithRequiredDependent(a => a.ZGLV).WillCascadeOnDelete(true);
            modelBuilder.Entity<CFile_ZAP>().HasKey(a => a.ZapId);
            modelBuilder.Entity<CFile_Z_SL>().HasKey(a => a.ZapId);
            modelBuilder.Entity<CFile_Z_SL>().HasRequired(a => a.ZAP).WithRequiredDependent(a => a.Z_SL).WillCascadeOnDelete(true);
            modelBuilder.Entity<CFile_ZAP>().HasKey(a => a.ZapId);
            modelBuilder.Entity<CFile_PACIENT>().HasRequired(a => a.ZAP).WithRequiredDependent(a => a.PACIENT).WillCascadeOnDelete(true);
            modelBuilder.Entity<CFile_SCHET>().HasKey(a => a.ReestrId);
            modelBuilder.Entity<CFile_SCHET>().HasRequired(a => a.C).WithRequiredDependent(a => a.SCHET).WillCascadeOnDelete(true);
            modelBuilder.Entity<CFile_KSG_KPG>().HasKey(a => a.SlId);
            modelBuilder.Entity<CFile_KSG_KPG>().HasRequired(a => a.SL).WithOptional(a => a.KSG_KPG).WillCascadeOnDelete(true);
            modelBuilder.Entity<CFile_ONK_SL>().HasKey(a => a.SlId);
            modelBuilder.Entity<CFile_ONK_SL>().HasRequired(a => a.SL).WithOptional(a => a.ONK_SL).WillCascadeOnDelete(true);

        }
       
        public  DbSet<Packet> Packets { get; set; }
       
       
       
        public DbSet<XmlReestr> XmlReestrs { get; set; }
        public DbSet<HCode> HFile { get; set; }
        public DbSet<LCode> LFile { get; set; }
        public DbSet<CCode> CFile { get; set; }
        public DbSet<DCode> DFile { get; set; }
        public DbSet<FCode> FFile { get; set; }
        public DbSet<PCode> PFile { get; set; }
        public DbSet<SCode> SFile { get; set; }
        
    }
    /*
    public class DBLpu: DbContext
    {
        public DBLpu(): base("name=reestr")
        {
            Database.SetInitializer<DBLpu>(new ReestrDBInit());
        }
        public virtual DbSet<Lpu> Lpus { get; set; }
    }

    */
    
    public class ReestrDbInit : DropCreateDatabaseIfModelChanges<reestr>
    {
        protected override void Seed(reestr context)
        {
        //    context.Mos.AddRange(new List<Mo>  
        //{
        //        new Mo(250426,"НУЗ Узловая больница").AddLpu(426,"S,SC").AddLpu(441,"P,PC,DP,DV,DO"),
        //        new Mo(250433,"КГБУЗ Уссурийская стомотология").AddLpu(433,"P,CP"),
        //        new Mo(250443,"КГБУЗ Михайловская ЦРБ").AddLpu(443,"S,SC").AddLpu(447, "P,PC,DP,DV,DO,DS,DU,DF"),
        //        new Mo(250452,"КГБУЗ Октябрьская ЦРБ").AddLpu(452, "S,SC").AddLpu(454, "P,PC,DP,DV,DO,DS,DU,DF"),
        //        new Mo(250458,"КГБУЗ Пограничная ЦРБ").AddLpu(458, "S,SC").AddLpu(461, "P,PC,DP,DV,DO,DS,DU,DF"),
        //        new Mo(250473,"КГБУЗ Ханкайская ЦРБ").AddLpu(473,"S,SC").AddLpu(474,"P,PC,DP,DV,DO,DS,DU,DF"),
        //        new Mo(250477,"КГБУЗ Хорольская ЦРБ").AddLpu(477,"S,SC").AddLpu(480,"P,PC,DP,DV,DO,DS,DU,DF"),
        //        new Mo(250700,"КГБУЗ Уссурийская ЦГБ").AddLpu(700,"S,SC").AddLpu(704,"P,PC,DP,DV,DO,DS,DU,DF"),
        //        new Mo(250718,"КГБУЗ СМП г.Уссурийска").AddLpu(718, "P"),
        //        new Mo(250755,"ООО Клиника лечения боль").AddLpu(755, "P,CP"),
        //        new Mo(250777,"ООО Сфера Мед").AddLpu(777,"P"),
        //        new Mo(250775,"ФГКУ 439 ВГ МО РФ").AddLpu(775,"S")
        //});
        //    base.Seed(context);
        //    context.SaveChanges();
        }
    }
}