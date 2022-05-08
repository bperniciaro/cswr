using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class CswrDbContext : DbContext
    {
        public CswrDbContext()
        {
        }

        public CswrDbContext(DbContextOptions<CswrDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adpcalculation> Adpcalculations { get; set; }
        public virtual DbSet<AdpplayerLog> AdpplayerLogs { get; set; }
        public virtual DbSet<AspnetApplication> AspnetApplications { get; set; }
        public virtual DbSet<AspnetMembership> AspnetMemberships { get; set; }
        public virtual DbSet<AspnetPath> AspnetPaths { get; set; }
        public virtual DbSet<AspnetPersonalizationAllUser> AspnetPersonalizationAllUsers { get; set; }
        public virtual DbSet<AspnetPersonalizationPerUser> AspnetPersonalizationPerUsers { get; set; }
        public virtual DbSet<AspnetProfile> AspnetProfiles { get; set; }
        public virtual DbSet<AspnetRole> AspnetRoles { get; set; }
        public virtual DbSet<AspnetSchemaVersion> AspnetSchemaVersions { get; set; }
        public virtual DbSet<AspnetUser> AspnetUsers { get; set; }
        public virtual DbSet<AspnetUsersInRole> AspnetUsersInRoles { get; set; }
        public virtual DbSet<AspnetWebEventEvent> AspnetWebEventEvents { get; set; }
        public virtual DbSet<CheatSheet> CheatSheets { get; set; }
        public virtual DbSet<CheatSheetItem> CheatSheetItems { get; set; }
        public virtual DbSet<CheatSheetPosition> CheatSheetPositions { get; set; }
        public virtual DbSet<CheatSheetStat> CheatSheetStats { get; set; }
        public virtual DbSet<ExpertRanker> ExpertRankers { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerStatusCode> PlayerStatusCodes { get; set; }
        public virtual DbSet<PositionCode> PositionCodes { get; set; }
        public virtual DbSet<SeasonCode> SeasonCodes { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<SportCode> SportCodes { get; set; }
        public virtual DbSet<SportPosition> SportPositions { get; set; }
        public virtual DbSet<SportPositionStat> SportPositionStats { get; set; }
        public virtual DbSet<SportSeason> SportSeasons { get; set; }
        public virtual DbSet<SportSeasonPlayerSeasonStat> SportSeasonPlayerSeasonStats { get; set; }
        public virtual DbSet<SportSeasonPlayerStatus> SportSeasonPlayerStatuses { get; set; }
        public virtual DbSet<SportSeasonPlayerTeam> SportSeasonPlayerTeams { get; set; }
        public virtual DbSet<SportSeasonPlayerWeeklyStat> SportSeasonPlayerWeeklyStats { get; set; }
        public virtual DbSet<SportSeasonSuppPlayerReview> SportSeasonSuppPlayerReviews { get; set; }
        public virtual DbSet<SportSetting> SportSettings { get; set; }
        public virtual DbSet<SportStatusCode> SportStatusCodes { get; set; }
        public virtual DbSet<SportTeamPlayer> SportTeamPlayers { get; set; }
        public virtual DbSet<SportTeamSeasonBye> SportTeamSeasonByes { get; set; }
        public virtual DbSet<StatCode> StatCodes { get; set; }
        public virtual DbSet<SupplementalSheet> SupplementalSheets { get; set; }
        public virtual DbSet<SupplementalSheetItem> SupplementalSheetItems { get; set; }
        public virtual DbSet<SupplementalSource> SupplementalSources { get; set; }
        public virtual DbSet<TeamCode> TeamCodes { get; set; }
        public virtual DbSet<UserSession> UserSessions { get; set; }
        public virtual DbSet<VDefaultConstraint> VDefaultConstraints { get; set; }
        public virtual DbSet<VwAspnetApplication> VwAspnetApplications { get; set; }
        public virtual DbSet<VwAspnetMembershipUser> VwAspnetMembershipUsers { get; set; }
        public virtual DbSet<VwAspnetProfile> VwAspnetProfiles { get; set; }
        public virtual DbSet<VwAspnetRole> VwAspnetRoles { get; set; }
        public virtual DbSet<VwAspnetUser> VwAspnetUsers { get; set; }
        public virtual DbSet<VwAspnetUsersInRole> VwAspnetUsersInRoles { get; set; }
        public virtual DbSet<VwAspnetWebPartStatePath> VwAspnetWebPartStatePaths { get; set; }
        public virtual DbSet<VwAspnetWebPartStateShared> VwAspnetWebPartStateShareds { get; set; }
        public virtual DbSet<VwAspnetWebPartStateUser> VwAspnetWebPartStateUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS01;Integrated Security=True;Max Pool Size=1000;Initial Catalog=CSWR_3-0_Main_11-06-20");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adpcalculation>(entity =>
            {
                entity.ToTable("ADPCalculations");

                entity.Property(e => e.AdpcalculationId).HasColumnName("ADPCalculationID");

                entity.Property(e => e.CalcTimestamp).HasColumnType("datetime");

                entity.Property(e => e.PositionCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SeasonCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SportCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<AdpplayerLog>(entity =>
            {
                entity.ToTable("ADPPlayerLogs");

                entity.HasIndex(e => e.AdpcalculationId, "idx_DCh_579_578_Sheets_ADPPlayerLogs");

                entity.Property(e => e.AdpplayerLogId).HasColumnName("ADPPlayerLogID");

                entity.Property(e => e.Adp).HasColumnName("ADP");

                entity.Property(e => e.AdpcalculationId).HasColumnName("ADPCalculationID");

                entity.Property(e => e.CalcTimestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.SeasonCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SportCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Adpcalculation)
                    .WithMany(p => p.AdpplayerLogs)
                    .HasForeignKey(d => d.AdpcalculationId)
                    .HasConstraintName("FK_ADPPlayerLogs_ADPCalculations");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.AdpplayerLogs)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ADPPlayerLogs_Players");

                entity.HasOne(d => d.SeasonCodeNavigation)
                    .WithMany(p => p.AdpplayerLogs)
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ADPPlayerLogs_SeasonCodes");

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.AdpplayerLogs)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ADPPlayerLogs_SportCodes");
            });

            modelBuilder.Entity<AspnetApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationId)
                    .HasName("PK__aspnet_Applicati__7E6CC920")
                    .IsClustered(false);

                entity.ToTable("aspnet_Applications");

                entity.HasIndex(e => e.ApplicationName, "UQ__aspnet_Applicati__00551192")
                    .IsUnique();

                entity.HasIndex(e => e.LoweredApplicationName, "UQ__aspnet_Applicati__7F60ED59")
                    .IsUnique();

                entity.HasIndex(e => e.LoweredApplicationName, "aspnet_Applications_Index")
                    .IsClustered();

                entity.Property(e => e.ApplicationId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.LoweredApplicationName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspnetMembership>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__aspnet_Membershi__1367E606")
                    .IsClustered(false);

                entity.ToTable("aspnet_Membership");

                entity.HasIndex(e => new { e.ApplicationId, e.LoweredEmail }, "aspnet_Membership_index")
                    .IsClustered();

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Comment).HasColumnType("ntext");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FailedPasswordAnswerAttemptWindowStart).HasColumnType("datetime");

                entity.Property(e => e.FailedPasswordAttemptWindowStart).HasColumnType("datetime");

                entity.Property(e => e.LastLockoutDate).HasColumnType("datetime");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastPasswordChangedDate).HasColumnType("datetime");

                entity.Property(e => e.LoweredEmail).HasMaxLength(256);

                entity.Property(e => e.MobilePin)
                    .HasMaxLength(16)
                    .HasColumnName("MobilePIN");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PasswordAnswer).HasMaxLength(128);

                entity.Property(e => e.PasswordQuestion).HasMaxLength(256);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspnetMemberships)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Me__Appli__145C0A3F");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AspnetMembership)
                    .HasForeignKey<AspnetMembership>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Me__UserI__15502E78");
            });

            modelBuilder.Entity<AspnetPath>(entity =>
            {
                entity.HasKey(e => e.PathId)
                    .HasName("PK__aspnet_Paths__44FF419A")
                    .IsClustered(false);

                entity.ToTable("aspnet_Paths");

                entity.HasIndex(e => new { e.ApplicationId, e.LoweredPath }, "aspnet_Paths_index")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.PathId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.LoweredPath)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspnetPaths)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Pa__Appli__45F365D3");
            });

            modelBuilder.Entity<AspnetPersonalizationAllUser>(entity =>
            {
                entity.HasKey(e => e.PathId)
                    .HasName("PK__aspnet_Personali__4AB81AF0");

                entity.ToTable("aspnet_PersonalizationAllUsers");

                entity.Property(e => e.PathId).ValueGeneratedNever();

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.PageSettings)
                    .IsRequired()
                    .HasColumnType("image");

                entity.HasOne(d => d.Path)
                    .WithOne(p => p.AspnetPersonalizationAllUser)
                    .HasForeignKey<AspnetPersonalizationAllUser>(d => d.PathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Pe__PathI__4BAC3F29");
            });

            modelBuilder.Entity<AspnetPersonalizationPerUser>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__aspnet_Personali__4D94879B")
                    .IsClustered(false);

                entity.ToTable("aspnet_PersonalizationPerUser");

                entity.HasIndex(e => new { e.PathId, e.UserId }, "aspnet_PersonalizationPerUser_index1")
                    .IsUnique()
                    .IsClustered();

                entity.HasIndex(e => new { e.UserId, e.PathId }, "aspnet_PersonalizationPerUser_ncindex2")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.PageSettings)
                    .IsRequired()
                    .HasColumnType("image");

                entity.HasOne(d => d.Path)
                    .WithMany(p => p.AspnetPersonalizationPerUsers)
                    .HasForeignKey(d => d.PathId)
                    .HasConstraintName("FK__aspnet_Pe__PathI__4F7CD00D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspnetPersonalizationPerUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__aspnet_Pe__UserI__5070F446");
            });

            modelBuilder.Entity<AspnetProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__aspnet_Profile__286302EC");

                entity.ToTable("aspnet_Profile");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.PropertyNames)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.PropertyValuesBinary)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.PropertyValuesString)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AspnetProfile)
                    .HasForeignKey<AspnetProfile>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Pr__UserI__29572725");
            });

            modelBuilder.Entity<AspnetRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__aspnet_Roles__31EC6D26")
                    .IsClustered(false);

                entity.ToTable("aspnet_Roles");

                entity.HasIndex(e => new { e.ApplicationId, e.LoweredRoleName }, "aspnet_Roles_index1")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.LoweredRoleName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspnetRoles)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Ro__Appli__32E0915F");
            });

            modelBuilder.Entity<AspnetSchemaVersion>(entity =>
            {
                entity.HasKey(e => new { e.Feature, e.CompatibleSchemaVersion })
                    .HasName("PK__aspnet_SchemaVer__08EA5793");

                entity.ToTable("aspnet_SchemaVersions");

                entity.Property(e => e.Feature).HasMaxLength(128);

                entity.Property(e => e.CompatibleSchemaVersion).HasMaxLength(128);
            });

            modelBuilder.Entity<AspnetUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__aspnet_Users__03317E3D")
                    .IsClustered(false);

                entity.ToTable("aspnet_Users");

                entity.HasIndex(e => new { e.ApplicationId, e.LoweredUserName }, "aspnet_Users_Index")
                    .IsUnique()
                    .IsClustered();

                entity.HasIndex(e => new { e.ApplicationId, e.LastActivityDate }, "aspnet_Users_Index2");

                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");

                entity.Property(e => e.LoweredUserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.MobileAlias).HasMaxLength(16);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspnetUsers)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Us__Appli__0425A276");
            });

            modelBuilder.Entity<AspnetUsersInRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK__aspnet_UsersInRo__35BCFE0A");

                entity.ToTable("aspnet_UsersInRoles");

                entity.HasIndex(e => e.RoleId, "aspnet_UsersInRoles_index");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspnetUsersInRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Us__RoleI__37A5467C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspnetUsersInRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Us__UserI__36B12243");
            });

            modelBuilder.Entity<AspnetWebEventEvent>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK__aspnet_WebEvent___5FB337D6");

                entity.ToTable("aspnet_WebEvent_Events");

                entity.Property(e => e.EventId)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ApplicationPath).HasMaxLength(256);

                entity.Property(e => e.ApplicationVirtualPath).HasMaxLength(256);

                entity.Property(e => e.Details).HasColumnType("ntext");

                entity.Property(e => e.EventOccurrence).HasColumnType("decimal(19, 0)");

                entity.Property(e => e.EventSequence).HasColumnType("decimal(19, 0)");

                entity.Property(e => e.EventTime).HasColumnType("datetime");

                entity.Property(e => e.EventTimeUtc).HasColumnType("datetime");

                entity.Property(e => e.EventType)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ExceptionType).HasMaxLength(256);

                entity.Property(e => e.MachineName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Message).HasMaxLength(1024);

                entity.Property(e => e.RequestUrl).HasMaxLength(1024);
            });

            modelBuilder.Entity<CheatSheet>(entity =>
            {
                entity.HasIndex(e => e.SportCode, "idx_DCh_2173_2172_Sheets_CheatSheets");

                entity.HasIndex(e => e.Username, "idx_DCh_31307_31306_Sheets_CheatSheets");

                entity.HasIndex(e => new { e.Username, e.SportCode }, "idx_DCh_6739_6738_Sheets_CheatSheets");

                entity.HasIndex(e => new { e.SeasonCode, e.SportCode }, "idx_DCh_788_787_Sheets_CheatSheets");

                entity.Property(e => e.CheatSheetId).HasColumnName("CheatSheetID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Pprleague).HasColumnName("PPRLeague");

                entity.Property(e => e.SeasonCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SheetName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SportCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StatsSeasonCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.SeasonCodeNavigation)
                    .WithMany(p => p.CheatSheetSeasonCodeNavigations)
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CheatSeasonCodes");

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.CheatSheets)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CheatSportCodes");

                entity.HasOne(d => d.StatsSeasonCodeNavigation)
                    .WithMany(p => p.CheatSheetStatsSeasonCodeNavigations)
                    .HasForeignKey(d => d.StatsSeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CheatSeasonCodes1");
            });

            modelBuilder.Entity<CheatSheetItem>(entity =>
            {
                entity.HasKey(e => new { e.CheatSheetId, e.PlayerId });

                entity.HasIndex(e => e.BustTag, "idx_DCh_196_195_Sheets_CheatSheetItems");

                entity.HasIndex(e => e.SleeperTag, "idx_DCh_2777_2776_Sheets_CheatSheetItems");

                entity.Property(e => e.CheatSheetId).HasColumnName("CheatSheetID");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("PlayerID")
                    .HasComment("The ID of the respective player, or NULL if the item is a tier-template");

                entity.Property(e => e.BustTag).HasDefaultValueSql("((0))");

                entity.Property(e => e.InjuredTag).HasDefaultValueSql("((0))");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SleeperTag).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CheatSheet)
                    .WithMany(p => p.CheatSheetItems)
                    .HasForeignKey(d => d.CheatSheetId)
                    .HasConstraintName("FK_CheatSheetItems_CheatSheets");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.CheatSheetItems)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CheatSheetItems_Players");
            });

            modelBuilder.Entity<CheatSheetPosition>(entity =>
            {
                entity.HasKey(e => new { e.CheatSheetId, e.PositionCode });

                entity.HasIndex(e => e.PositionCode, "idx_DCh_2484_2483_Sheets_CheatSheetPositions");

                entity.Property(e => e.CheatSheetId).HasColumnName("CheatSheetID");

                entity.Property(e => e.PositionCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CheatSheet)
                    .WithMany(p => p.CheatSheetPositions)
                    .HasForeignKey(d => d.CheatSheetId)
                    .HasConstraintName("FK_CheatSheetPositions_CheatSheets");
            });

            modelBuilder.Entity<CheatSheetStat>(entity =>
            {
                entity.HasKey(e => new { e.CheatSheetId, e.StatCode });

                entity.Property(e => e.CheatSheetId).HasColumnName("CheatSheetID");

                entity.Property(e => e.StatCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CheatSheet)
                    .WithMany(p => p.CheatSheetStats)
                    .HasForeignKey(d => d.CheatSheetId)
                    .HasConstraintName("FK_CheatSheetStats_CheatSheets");

                entity.HasOne(d => d.StatCodeNavigation)
                    .WithMany(p => p.CheatSheetStats)
                    .HasForeignKey(d => d.StatCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CheatSheetStats_StatCodes");
            });

            modelBuilder.Entity<ExpertRanker>(entity =>
            {
                entity.Property(e => e.ExpertRankerId).HasColumnName("ExpertRankerID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.WebsiteName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.WebsiteUrl)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstYear).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PositionCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SportCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StatMapId).HasColumnName("StatMapID");

                entity.Property(e => e.TeamCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TwitterUsername)
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Players_SportCodes");
            });

            modelBuilder.Entity<PlayerStatusCode>(entity =>
            {
                entity.HasKey(e => e.StatusCode);

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CountInstructions)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CountLabel)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SuppInfoExample)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SuppInfoInstructions)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SuppInfoLabel)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PositionCode>(entity =>
            {
                entity.HasKey(e => e.PositionCode1);

                entity.Property(e => e.PositionCode1)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("PositionCode")
                    .IsFixedLength(true)
                    .HasComment("The code representing a position of some sport.");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasComment("The standare abbreviation associated with some position.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SeasonCode>(entity =>
            {
                entity.HasKey(e => e.SeasonCode1);

                entity.Property(e => e.SeasonCode1)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("SeasonCode")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => e.SettingCode);

                entity.Property(e => e.SettingCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SettingName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SettingValue)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SportCode>(entity =>
            {
                entity.HasKey(e => e.SportCode1);

                entity.Property(e => e.SportCode1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SportCode")
                    .IsFixedLength(true)
                    .HasComment("Code describing the different leagues which are available.");

                entity.Property(e => e.LeagueAbbreviation)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("An abbreviation associated with the respective league.");

                entity.Property(e => e.LeagueName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("The name of the particular league.");

                entity.Property(e => e.SportName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SportPosition>(entity =>
            {
                entity.HasKey(e => new { e.PositionCode, e.SportCode });

                entity.Property(e => e.PositionCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SportCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.PositionCodeNavigation)
                    .WithMany(p => p.SportPositions)
                    .HasForeignKey(d => d.PositionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportPositions_PositionCodes");

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.SportPositions)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportPositions_SportCodes");
            });

            modelBuilder.Entity<SportPositionStat>(entity =>
            {
                entity.HasKey(e => new { e.SportCode, e.PositionCode, e.StatCode });

                entity.Property(e => e.SportCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PositionCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StatCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.PositionCodeNavigation)
                    .WithMany(p => p.SportPositionStats)
                    .HasForeignKey(d => d.PositionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportPositionStats_PositionCodes");

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.SportPositionStats)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportPositionStats_SportCodes");

                entity.HasOne(d => d.StatCodeNavigation)
                    .WithMany(p => p.SportPositionStats)
                    .HasForeignKey(d => d.StatCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportPositionStats_StatCodes");
            });

            modelBuilder.Entity<SportSeason>(entity =>
            {
                entity.HasKey(e => new { e.SportCode, e.SeasonCode });

                entity.Property(e => e.SportCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SeasonCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.SeasonCodeNavigation)
                    .WithMany(p => p.SportSeasons)
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasons_SeasonCodes");

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.SportSeasons)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasons_SportCodes");
            });

            modelBuilder.Entity<SportSeasonPlayerSeasonStat>(entity =>
            {
                entity.HasKey(e => new { e.SportCode, e.SeasonCode, e.PlayerId, e.StatCode });

                entity.HasIndex(e => new { e.SeasonCode, e.StatCode }, "idx_DCh_641_640_Sheets_SportSeasonPlayerSeasonSt");

                entity.HasIndex(e => new { e.SeasonCode, e.PlayerId, e.StatCode }, "idx_DCh_76123_76122_Sheets_SportSeasonPlayerSeasonSt");

                entity.Property(e => e.SportCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SeasonCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.StatCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.SportSeasonPlayerSeasonStats)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerSeasonStats_Players");

                entity.HasOne(d => d.SeasonCodeNavigation)
                    .WithMany(p => p.SportSeasonPlayerSeasonStats)
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerSeasonStats_SeasonCodes");

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.SportSeasonPlayerSeasonStats)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerSeasonStats_SportCodes");

                entity.HasOne(d => d.StatCodeNavigation)
                    .WithMany(p => p.SportSeasonPlayerSeasonStats)
                    .HasForeignKey(d => d.StatCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerSeasonStats_StatCodes");
            });

            modelBuilder.Entity<SportSeasonPlayerStatus>(entity =>
            {
                entity.HasKey(e => e.PlayerStatusId)
                    .HasName("PK_PlayerStatuses");

                entity.Property(e => e.CreatedByUsername)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTimestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedByUsername)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedTimestamp).HasColumnType("datetime");

                entity.Property(e => e.SeasonCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SportCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StatusCode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SuppInfo)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.SportSeasonPlayerStatuses)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayerStatuses_Players");

                entity.HasOne(d => d.SeasonCodeNavigation)
                    .WithMany(p => p.SportSeasonPlayerStatuses)
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayerStatuses_SeasonCodes");

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.SportSeasonPlayerStatuses)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerStatuses_SportCodes");

                entity.HasOne(d => d.StatusCodeNavigation)
                    .WithMany(p => p.SportSeasonPlayerStatuses)
                    .HasForeignKey(d => d.StatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayerStatuses_PlayerStatusCodes");
            });

            modelBuilder.Entity<SportSeasonPlayerTeam>(entity =>
            {
                entity.HasKey(e => new { e.SportCode, e.SeasonCode, e.PlayerId });

                entity.Property(e => e.SportCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SeasonCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.TeamCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.SportSeasonPlayerTeams)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerTeams_Players");

                entity.HasOne(d => d.SeasonCodeNavigation)
                    .WithMany(p => p.SportSeasonPlayerTeams)
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerTeams_SeasonCodes");

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.SportSeasonPlayerTeams)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerTeams_SportCodes");

                entity.HasOne(d => d.TeamCodeNavigation)
                    .WithMany(p => p.SportSeasonPlayerTeams)
                    .HasForeignKey(d => d.TeamCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerTeams_TeamCodes");
            });

            modelBuilder.Entity<SportSeasonPlayerWeeklyStat>(entity =>
            {
                entity.HasKey(e => new { e.SportCode, e.SeasonCode, e.Week, e.PlayerId, e.StatCode });

                entity.Property(e => e.SportCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SeasonCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.StatCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.SportSeasonPlayerWeeklyStats)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerWeeklyStats_Players");

                entity.HasOne(d => d.SeasonCodeNavigation)
                    .WithMany(p => p.SportSeasonPlayerWeeklyStats)
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerWeeklyStats_SeasonCodes");

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.SportSeasonPlayerWeeklyStats)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerWeeklyStats_SportCodes");

                entity.HasOne(d => d.StatCodeNavigation)
                    .WithMany(p => p.SportSeasonPlayerWeeklyStats)
                    .HasForeignKey(d => d.StatCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonPlayerWeeklyStats_StatCodes");
            });

            modelBuilder.Entity<SportSeasonSuppPlayerReview>(entity =>
            {
                entity.HasKey(e => new { e.SportCode, e.SeasonCode, e.SupplementalSourceId, e.PlayerId });

                entity.Property(e => e.SportCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SeasonCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SupplementalSourceId).HasColumnName("SupplementalSourceID");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.ReviewUrl)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("ReviewURL");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.SportSeasonSuppPlayerReviews)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonSuppPlayerReviews_Players");

                entity.HasOne(d => d.SeasonCodeNavigation)
                    .WithMany(p => p.SportSeasonSuppPlayerReviews)
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonSuppPlayerReviews_SeasonCodes");

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.SportSeasonSuppPlayerReviews)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonSuppPlayerReviews_SportCodes");

                entity.HasOne(d => d.SupplementalSource)
                    .WithMany(p => p.SportSeasonSuppPlayerReviews)
                    .HasForeignKey(d => d.SupplementalSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSeasonSuppPlayerReviews_SupplementalSources");
            });

            modelBuilder.Entity<SportSetting>(entity =>
            {
                entity.Property(e => e.SportSettingId).HasColumnName("SportSettingID");

                entity.Property(e => e.SettingCode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SettingName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SettingValue)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SportCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.SportSettings)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportSettings_SportCodes");
            });

            modelBuilder.Entity<SportStatusCode>(entity =>
            {
                entity.HasKey(e => new { e.SportCode, e.StatusCode })
                    .HasName("PK_SportPlayerStatusCodes");

                entity.Property(e => e.SportCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.SportStatusCodes)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportPlayerStatusCodes_SportCodes");

                entity.HasOne(d => d.StatusCodeNavigation)
                    .WithMany(p => p.SportStatusCodes)
                    .HasForeignKey(d => d.StatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportPlayerStatusCodes_PlayerStatusCodes");
            });

            modelBuilder.Entity<SportTeamPlayer>(entity =>
            {
                entity.HasKey(e => new { e.SeasonCode, e.SportCode, e.TeamCode, e.PlayerId });

                entity.Property(e => e.SeasonCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SportCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TeamCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            });

            modelBuilder.Entity<SportTeamSeasonBye>(entity =>
            {
                entity.HasKey(e => new { e.SportCode, e.SeasonCode, e.TeamCode });

                entity.Property(e => e.SportCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SeasonCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TeamCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.SeasonCodeNavigation)
                    .WithMany(p => p.SportTeamSeasonByes)
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportTeamSeasonByes_SeasonCodes");

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.SportTeamSeasonByes)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportTeamSeasonByes_SportCodes");

                entity.HasOne(d => d.TeamCodeNavigation)
                    .WithMany(p => p.SportTeamSeasonByes)
                    .HasForeignKey(d => d.TeamCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportTeamSeasonByes_TeamCodes");
            });

            modelBuilder.Entity<StatCode>(entity =>
            {
                entity.HasKey(e => e.StatCode1);

                entity.Property(e => e.StatCode1)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("StatCode")
                    .IsFixedLength(true)
                    .HasComment("A code representing some statistic.");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplementalSheet>(entity =>
            {
                entity.Property(e => e.SupplementalSheetId).HasColumnName("SupplementalSheetID");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.PositionCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SeasonCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SportCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SupplementalSourceId).HasColumnName("SupplementalSourceID");

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("URL");

                entity.HasOne(d => d.PositionCodeNavigation)
                    .WithMany(p => p.SupplementalSheets)
                    .HasForeignKey(d => d.PositionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplementalPositionCodes");

                entity.HasOne(d => d.SeasonCodeNavigation)
                    .WithMany(p => p.SupplementalSheets)
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplementalSeasonCodes");

                entity.HasOne(d => d.SportCodeNavigation)
                    .WithMany(p => p.SupplementalSheets)
                    .HasForeignKey(d => d.SportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplementalSportCodes");

                entity.HasOne(d => d.SupplementalSource)
                    .WithMany(p => p.SupplementalSheets)
                    .HasForeignKey(d => d.SupplementalSourceId)
                    .HasConstraintName("FK_SupplementalSupplementalSources");
            });

            modelBuilder.Entity<SupplementalSheetItem>(entity =>
            {
                entity.HasKey(e => new { e.SupplementalSheetId, e.PlayerId });

                entity.Property(e => e.SupplementalSheetId).HasColumnName("SupplementalSheetID");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.BustTag).HasDefaultValueSql("((0))");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SleeperTag).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.SupplementalSheetItems)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplementalSheetItems_Players");

                entity.HasOne(d => d.SupplementalSheet)
                    .WithMany(p => p.SupplementalSheetItems)
                    .HasForeignKey(d => d.SupplementalSheetId)
                    .HasConstraintName("FK_SupplementalSheetItems_SupplementalSheets");
            });

            modelBuilder.Entity<SupplementalSource>(entity =>
            {
                entity.Property(e => e.SupplementalSourceId).HasColumnName("SupplementalSourceID");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<TeamCode>(entity =>
            {
                entity.HasKey(e => e.TeamCode1);

                entity.Property(e => e.TeamCode1)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("TeamCode")
                    .IsFixedLength(true)
                    .HasComment("The standard team abbreviation for a team, such as NOS for New Orleans Saints");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasComment("The abbreviation associated with a team.");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasComment("The city which represents the particular team.");

                entity.Property(e => e.Mascot)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasComment("The mascot for the particular team, such as the 'Saints'.");

                entity.Property(e => e.SportCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StatMapId).HasColumnName("StatMapID");
            });

            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserActivity");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserSession)
                    .HasForeignKey<UserSession>(d => d.UserId)
                    .HasConstraintName("FK_UserSessions_aspnet_Membership");
            });

            modelBuilder.Entity<VDefaultConstraint>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_DEFAULT_CONSTRAINT");

                entity.Property(e => e.ColumnName)
                    .HasMaxLength(128)
                    .HasColumnName("COLUMN_NAME");

                entity.Property(e => e.ConstraintName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("CONSTRAINT_NAME");

                entity.Property(e => e.DefaultClause)
                    .HasMaxLength(4000)
                    .HasColumnName("DEFAULT_CLAUSE");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("TABLE_NAME");
            });

            modelBuilder.Entity<VwAspnetApplication>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_Applications");

                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.LoweredApplicationName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<VwAspnetMembershipUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_MembershipUsers");

                entity.Property(e => e.Comment).HasColumnType("ntext");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FailedPasswordAnswerAttemptWindowStart).HasColumnType("datetime");

                entity.Property(e => e.FailedPasswordAttemptWindowStart).HasColumnType("datetime");

                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");

                entity.Property(e => e.LastLockoutDate).HasColumnType("datetime");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastPasswordChangedDate).HasColumnType("datetime");

                entity.Property(e => e.LoweredEmail).HasMaxLength(256);

                entity.Property(e => e.MobileAlias).HasMaxLength(16);

                entity.Property(e => e.MobilePin)
                    .HasMaxLength(16)
                    .HasColumnName("MobilePIN");

                entity.Property(e => e.PasswordAnswer).HasMaxLength(128);

                entity.Property(e => e.PasswordQuestion).HasMaxLength(256);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<VwAspnetProfile>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_Profiles");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwAspnetRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_Roles");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.LoweredRoleName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<VwAspnetUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_Users");

                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");

                entity.Property(e => e.LoweredUserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.MobileAlias).HasMaxLength(16);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<VwAspnetUsersInRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_UsersInRoles");
            });

            modelBuilder.Entity<VwAspnetWebPartStatePath>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_WebPartState_Paths");

                entity.Property(e => e.LoweredPath)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<VwAspnetWebPartStateShared>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_WebPartState_Shared");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwAspnetWebPartStateUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_WebPartState_User");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
