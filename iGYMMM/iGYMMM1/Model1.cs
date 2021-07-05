using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace iGYMMM1
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Communication> Communications { get; set; }
        public virtual DbSet<Description> Descriptions { get; set; }
        public virtual DbSet<DiaryClnt> DiaryClnts { get; set; }
        public virtual DbSet<DiaryInstr> DiaryInstrs { get; set; }
        public virtual DbSet<DiaryTeam> DiaryTeams { get; set; }
        public virtual DbSet<Gym> Gyms { get; set; }
        public virtual DbSet<InstrsAttendance> InstrsAttendances { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PkgPayment> PkgPayments { get; set; }
        public virtual DbSet<PkgRequrmnt> PkgRequrmnts { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
        public virtual DbSet<TeamGroup> TeamGroups { get; set; }
        public virtual DbSet<TeamGroupsClient> TeamGroupsClients { get; set; }
        public virtual DbSet<TrainingTeam> TrainingTeams { get; set; }
        public virtual DbSet<TrnTmPackage> TrnTmPackages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<DiaryHeader> DiaryHeaders { get; set; }
        public virtual DbSet<DiaryItem> DiaryItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.PerHour1)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Client>()
                .Property(e => e.PerHour2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Client>()
                .Property(e => e.PerTrip1)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Client>()
                .Property(e => e.PerTrip2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Client>()
                .Property(e => e.UPass)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .Property(e => e.UCode)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .Property(e => e.UResetPass)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .Property(e => e.U_GUID)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .Property(e => e.OAuthLvl)
                .IsFixedLength();

            modelBuilder.Entity<DiaryClnt>()
                .Property(e => e.PerHour1)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryClnt>()
                .Property(e => e.PerHour2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryClnt>()
                .Property(e => e.PerTrip1)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryClnt>()
                .Property(e => e.PerTrip2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryClnt>()
                .Property(e => e.ChargeTot)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryClnt>()
                .Property(e => e.CreditTot)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryInstr>()
                .Property(e => e.PerHour1)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryInstr>()
                .Property(e => e.PerHour2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryInstr>()
                .Property(e => e.PerWaitHour)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryInstr>()
                .Property(e => e.PerTrip1)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryInstr>()
                .Property(e => e.PerTrip2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryInstr>()
                .Property(e => e.ChargeTot)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryInstr>()
                .Property(e => e.CreditTot)
                .HasPrecision(10, 2);

            modelBuilder.Entity<InstrsAttendance>()
                .Property(e => e.IAShiftCredit)
                .HasPrecision(10, 2);

            modelBuilder.Entity<InstrsAttendance>()
                .Property(e => e.IAShiftCharge)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Instructor>()
                .Property(e => e.PerHour1)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Instructor>()
                .Property(e => e.PerHour2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Instructor>()
                .Property(e => e.PerWaitHour)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Instructor>()
                .Property(e => e.PerTrip1)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Instructor>()
                .Property(e => e.PerTrip2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Instructor>()
                .Property(e => e.UPass)
                .IsFixedLength();

            modelBuilder.Entity<Instructor>()
                .Property(e => e.UCode)
                .IsFixedLength();

            modelBuilder.Entity<Instructor>()
                .Property(e => e.UResetPass)
                .IsFixedLength();

            modelBuilder.Entity<Instructor>()
                .Property(e => e.U_GUID)
                .IsFixedLength();

            modelBuilder.Entity<Instructor>()
                .Property(e => e.OAuthLvl)
                .IsFixedLength();

            modelBuilder.Entity<Package>()
                .Property(e => e.TotalFee1)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Package>()
                .Property(e => e.TotalFee2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Package>()
                .Property(e => e.TotalFee3)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PkgPayment>()
                .Property(e => e.TotalFee)
                .HasPrecision(10, 2);

            modelBuilder.Entity<User>()
                .Property(e => e.UPass)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.UCode)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.UResetPass)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.U_GUID)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.OAuthLvl)
                .IsFixedLength();
            modelBuilder.Entity<DiaryHeader>()
               .Property(e => e.PerHour1)
               .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryHeader>()
                .Property(e => e.PerHour2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryHeader>()
                .Property(e => e.PerWaitHour)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryHeader>()
                .Property(e => e.PerTrip1)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryHeader>()
                .Property(e => e.PerTrip2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryHeader>()
                .Property(e => e.ChargeTot)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryHeader>()
                .Property(e => e.CreditTot)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryItem>()
                .Property(e => e.PerHour1)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryItem>()
                .Property(e => e.PerHour2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryItem>()
                .Property(e => e.PerTrip1)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryItem>()
                .Property(e => e.PerTrip2)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryItem>()
                .Property(e => e.ChargeTot)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DiaryItem>()
                .Property(e => e.CreditTot)
                .HasPrecision(10, 2);

        }
    }
}
