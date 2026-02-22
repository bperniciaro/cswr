using System;
using System.Collections.Generic;
using Cswr.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cswr.Dal.Context;

public partial class CswrDbContext : DbContext
{
    public CswrDbContext()
    {
    }

    public CswrDbContext(DbContextOptions<CswrDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SheetsCheatSheet> SheetsCheatSheets { get; set; }

    public virtual DbSet<SheetsCheatSheetItem> SheetsCheatSheetItems { get; set; }

    public virtual DbSet<SheetsCheatSheetPosition> SheetsCheatSheetPositions { get; set; }

    public virtual DbSet<SheetsPlayer> SheetsPlayers { get; set; }

    public virtual DbSet<SheetsPlayerStatusCode> SheetsPlayerStatusCodes { get; set; }

    public virtual DbSet<SheetsPositionCode> SheetsPositionCodes { get; set; }

    public virtual DbSet<SheetsSeasonCode> SheetsSeasonCodes { get; set; }

    public virtual DbSet<SheetsSetting> SheetsSettings { get; set; }

    public virtual DbSet<SheetsSportCode> SheetsSportCodes { get; set; }

    public virtual DbSet<SheetsSportPosition> SheetsSportPositions { get; set; }

    public virtual DbSet<SheetsSportPositionStat> SheetsSportPositionStats { get; set; }

    public virtual DbSet<SheetsSportSeason> SheetsSportSeasons { get; set; }

    public virtual DbSet<SheetsSportSeasonPlayerSeasonStat> SheetsSportSeasonPlayerSeasonStats { get; set; }

    public virtual DbSet<SheetsSportSeasonPlayerStatus> SheetsSportSeasonPlayerStatuses { get; set; }

    public virtual DbSet<SheetsSportSeasonPlayerTeam> SheetsSportSeasonPlayerTeams { get; set; }

    public virtual DbSet<SheetsSportSeasonPlayerWeeklyStat> SheetsSportSeasonPlayerWeeklyStats { get; set; }

    public virtual DbSet<SheetsSportSeasonSuppPlayerReview> SheetsSportSeasonSuppPlayerReviews { get; set; }

    public virtual DbSet<SheetsSportSetting> SheetsSportSettings { get; set; }

    public virtual DbSet<SheetsSportTeamPlayer> SheetsSportTeamPlayers { get; set; }

    public virtual DbSet<SheetsSportTeamSeasonBye> SheetsSportTeamSeasonByes { get; set; }

    public virtual DbSet<SheetsStatCode> SheetsStatCodes { get; set; }

    public virtual DbSet<SheetsSupplementalSheet> SheetsSupplementalSheets { get; set; }

    public virtual DbSet<SheetsSupplementalSheetItem> SheetsSupplementalSheetItems { get; set; }

    public virtual DbSet<SheetsSupplementalSource> SheetsSupplementalSources { get; set; }

    public virtual DbSet<SheetsTeamCode> SheetsTeamCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SheetsCheatSheet>(entity =>
        {
            entity.HasKey(e => e.CheatSheetId);

            entity.ToTable("Sheets_CheatSheets");

            entity.HasIndex(e => e.SportCode, "idx_DCh_2173_2172_Sheets_CheatSheets");

            entity.HasIndex(e => e.Username, "idx_DCh_31307_31306_Sheets_CheatSheets");

            entity.HasIndex(e => new { e.Username, e.SportCode }, "idx_DCh_6739_6738_Sheets_CheatSheets");

            entity.HasIndex(e => new { e.SeasonCode, e.SportCode }, "idx_DCh_788_787_Sheets_CheatSheets");

            entity.Property(e => e.CheatSheetId).HasColumnName("CheatSheetID");
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.Pprleague).HasColumnName("PPRLeague");
            entity.Property(e => e.SeasonCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SheetName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StatsSeasonCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Username)
                .HasMaxLength(256)
                .IsUnicode(false);

            entity.HasOne(d => d.SeasonCodeNavigation).WithMany(p => p.SheetsCheatSheetSeasonCodeNavigations)
                .HasForeignKey(d => d.SeasonCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_CheatSheets_Sheets_SeasonCodes");

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsCheatSheets)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_CheatSheets_Sheets_SportCodes");

            entity.HasOne(d => d.StatsSeasonCodeNavigation).WithMany(p => p.SheetsCheatSheetStatsSeasonCodeNavigations)
                .HasForeignKey(d => d.StatsSeasonCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_CheatSheets_Sheets_SeasonCodes1");

            entity.HasMany(d => d.StatCodes).WithMany(p => p.CheatSheets)
                .UsingEntity<Dictionary<string, object>>(
                    "SheetsCheatSheetStat",
                    r => r.HasOne<SheetsStatCode>().WithMany()
                        .HasForeignKey("StatCode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Sheets_CheatSheetStats_Sheets_StatCodes"),
                    l => l.HasOne<SheetsCheatSheet>().WithMany()
                        .HasForeignKey("CheatSheetId")
                        .HasConstraintName("FK_Sheets_CheatSheetStats_Sheets_CheatSheets"),
                    j =>
                    {
                        j.HasKey("CheatSheetId", "StatCode");
                        j.ToTable("Sheets_CheatSheetStats");
                        j.IndexerProperty<int>("CheatSheetId").HasColumnName("CheatSheetID");
                        j.IndexerProperty<string>("StatCode")
                            .HasMaxLength(4)
                            .IsUnicode(false)
                            .IsFixedLength();
                    });
        });

        modelBuilder.Entity<SheetsCheatSheetItem>(entity =>
        {
            entity.HasKey(e => new { e.CheatSheetId, e.PlayerId });

            entity.ToTable("Sheets_CheatSheetItems");

            entity.HasIndex(e => e.BustTag, "idx_DCh_196_195_Sheets_CheatSheetItems");

            entity.HasIndex(e => e.SleeperTag, "idx_DCh_2777_2776_Sheets_CheatSheetItems");

            entity.Property(e => e.CheatSheetId).HasColumnName("CheatSheetID");
            entity.Property(e => e.PlayerId)
                .HasComment("The ID of the respective player, or NULL if the item is a tier-template")
                .HasColumnName("PlayerID");
            entity.Property(e => e.BustTag).HasDefaultValueSql("((0))");
            entity.Property(e => e.InjuredTag).HasDefaultValueSql("((0))");
            entity.Property(e => e.Note)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.SleeperTag).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.CheatSheet).WithMany(p => p.SheetsCheatSheetItems)
                .HasForeignKey(d => d.CheatSheetId)
                .HasConstraintName("FK_Sheets_CheatSheetItems_Sheets_CheatSheets");

            entity.HasOne(d => d.Player).WithMany(p => p.SheetsCheatSheetItems)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_CheatSheetItems_Sheets_Players");
        });

        modelBuilder.Entity<SheetsCheatSheetPosition>(entity =>
        {
            entity.HasKey(e => new { e.CheatSheetId, e.PositionCode });

            entity.ToTable("Sheets_CheatSheetPositions");

            entity.HasIndex(e => e.PositionCode, "idx_DCh_2484_2483_Sheets_CheatSheetPositions");

            entity.Property(e => e.CheatSheetId).HasColumnName("CheatSheetID");
            entity.Property(e => e.PositionCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.CheatSheet).WithMany(p => p.SheetsCheatSheetPositions)
                .HasForeignKey(d => d.CheatSheetId)
                .HasConstraintName("FK_Sheets_CheatSheetPositions_Sheets_CheatSheets");
        });

        modelBuilder.Entity<SheetsPlayer>(entity =>
        {
            entity.HasKey(e => e.PlayerId);

            entity.ToTable("Sheets_Players");

            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstYear).HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PositionCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StatMapId).HasColumnName("StatMapID");
            entity.Property(e => e.TeamCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TwitterUsername)
                .HasMaxLength(320)
                .IsUnicode(false);

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsPlayers)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_Players_Sheets_SportCodes");
        });

        modelBuilder.Entity<SheetsPlayerStatusCode>(entity =>
        {
            entity.HasKey(e => e.StatusCode);

            entity.ToTable("Sheets_PlayerStatusCodes");

            entity.Property(e => e.StatusCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CountInstructions)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.CountLabel)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false)
                .IsFixedLength();
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

        modelBuilder.Entity<SheetsPositionCode>(entity =>
        {
            entity.HasKey(e => e.PositionCode);

            entity.ToTable("Sheets_PositionCodes");

            entity.Property(e => e.PositionCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("The code representing a position of some sport.");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasComment("The standare abbreviation associated with some position.");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SheetsSeasonCode>(entity =>
        {
            entity.HasKey(e => e.SeasonCode);

            entity.ToTable("Sheets_SeasonCodes");

            entity.Property(e => e.SeasonCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SheetsSetting>(entity =>
        {
            entity.HasKey(e => e.SettingCode);

            entity.ToTable("Sheets_Settings");

            entity.Property(e => e.SettingCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SettingName)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.SettingValue)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SheetsSportCode>(entity =>
        {
            entity.HasKey(e => e.SportCode);

            entity.ToTable("Sheets_SportCodes");

            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Code describing the different leagues which are available.");
            entity.Property(e => e.LeagueAbbreviation)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasComment("An abbreviation associated with the respective league.");
            entity.Property(e => e.LeagueName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("The name of the particular league.");
            entity.Property(e => e.SportName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.StatusCodes).WithMany(p => p.SportCodes)
                .UsingEntity<Dictionary<string, object>>(
                    "SheetsSportStatusCode",
                    r => r.HasOne<SheetsPlayerStatusCode>().WithMany()
                        .HasForeignKey("StatusCode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Sheets_SportPlayerStatusCodes_Sheets_PlayerStatusCodes"),
                    l => l.HasOne<SheetsSportCode>().WithMany()
                        .HasForeignKey("SportCode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Sheets_SportPlayerStatusCodes_Sheets_SportCodes"),
                    j =>
                    {
                        j.HasKey("SportCode", "StatusCode").HasName("PK_Sheets_SportPlayerStatusCodes");
                        j.ToTable("Sheets_SportStatusCodes");
                        j.IndexerProperty<string>("SportCode")
                            .HasMaxLength(3)
                            .IsUnicode(false)
                            .IsFixedLength();
                        j.IndexerProperty<string>("StatusCode")
                            .HasMaxLength(6)
                            .IsUnicode(false)
                            .IsFixedLength();
                    });
        });

        modelBuilder.Entity<SheetsSportPosition>(entity =>
        {
            entity.HasKey(e => new { e.PositionCode, e.SportCode });

            entity.ToTable("Sheets_SportPositions");

            entity.Property(e => e.PositionCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.PositionCodeNavigation).WithMany(p => p.SheetsSportPositions)
                .HasForeignKey(d => d.PositionCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportPositions_Sheets_PositionCodes");

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsSportPositions)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportPositions_Sheets_SportCodes");
        });

        modelBuilder.Entity<SheetsSportPositionStat>(entity =>
        {
            entity.HasKey(e => new { e.SportCode, e.PositionCode, e.StatCode });

            entity.ToTable("Sheets_SportPositionStats");

            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PositionCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StatCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.PositionCodeNavigation).WithMany(p => p.SheetsSportPositionStats)
                .HasForeignKey(d => d.PositionCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportPositionStats_Sheets_PositionCodes");

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsSportPositionStats)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportPositionStats_Sheets_SportCodes");

            entity.HasOne(d => d.StatCodeNavigation).WithMany(p => p.SheetsSportPositionStats)
                .HasForeignKey(d => d.StatCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportPositionStats_Sheets_StatCodes");
        });

        modelBuilder.Entity<SheetsSportSeason>(entity =>
        {
            entity.HasKey(e => new { e.SportCode, e.SeasonCode });

            entity.ToTable("Sheets_SportSeasons");

            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SeasonCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.SeasonCodeNavigation).WithMany(p => p.SheetsSportSeasons)
                .HasForeignKey(d => d.SeasonCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasons_Sheets_SeasonCodes");

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsSportSeasons)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasons_Sheets_SportCodes");
        });

        modelBuilder.Entity<SheetsSportSeasonPlayerSeasonStat>(entity =>
        {
            entity.HasKey(e => new { e.SportCode, e.SeasonCode, e.PlayerId, e.StatCode });

            entity.ToTable("Sheets_SportSeasonPlayerSeasonStats");

            entity.HasIndex(e => new { e.SeasonCode, e.StatCode }, "idx_DCh_641_640_Sheets_SportSeasonPlayerSeasonSt");

            entity.HasIndex(e => new { e.SeasonCode, e.PlayerId, e.StatCode }, "idx_DCh_76123_76122_Sheets_SportSeasonPlayerSeasonSt");

            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SeasonCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.StatCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Player).WithMany(p => p.SheetsSportSeasonPlayerSeasonStats)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerSeasonStats_Sheets_Players");

            entity.HasOne(d => d.SeasonCodeNavigation).WithMany(p => p.SheetsSportSeasonPlayerSeasonStats)
                .HasForeignKey(d => d.SeasonCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerSeasonStats_Sheets_SeasonCodes");

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsSportSeasonPlayerSeasonStats)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerSeasonStats_Sheets_SportCodes");

            entity.HasOne(d => d.StatCodeNavigation).WithMany(p => p.SheetsSportSeasonPlayerSeasonStats)
                .HasForeignKey(d => d.StatCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerSeasonStats_Sheets_StatCodes");
        });

        modelBuilder.Entity<SheetsSportSeasonPlayerStatus>(entity =>
        {
            entity.HasKey(e => e.PlayerStatusId).HasName("PK_Sheets_PlayerStatuses");

            entity.ToTable("Sheets_SportSeasonPlayerStatuses");

            entity.Property(e => e.CreatedByUsername)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUsername)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedTimestamp).HasColumnType("datetime");
            entity.Property(e => e.SeasonCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StatusCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SuppInfo)
                .HasMaxLength(2048)
                .IsUnicode(false);

            entity.HasOne(d => d.Player).WithMany(p => p.SheetsSportSeasonPlayerStatuses)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_PlayerStatuses_Sheets_Players");

            entity.HasOne(d => d.SeasonCodeNavigation).WithMany(p => p.SheetsSportSeasonPlayerStatuses)
                .HasForeignKey(d => d.SeasonCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_PlayerStatuses_Sheets_SeasonCodes");

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsSportSeasonPlayerStatuses)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerStatuses_Sheets_SportCodes");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.SheetsSportSeasonPlayerStatuses)
                .HasForeignKey(d => d.StatusCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_PlayerStatuses_Sheets_PlayerStatusCodes");
        });

        modelBuilder.Entity<SheetsSportSeasonPlayerTeam>(entity =>
        {
            entity.HasKey(e => new { e.SportCode, e.SeasonCode, e.PlayerId });

            entity.ToTable("Sheets_SportSeasonPlayerTeams");

            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SeasonCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.TeamCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Player).WithMany(p => p.SheetsSportSeasonPlayerTeams)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerTeams_Sheets_Players");

            entity.HasOne(d => d.SeasonCodeNavigation).WithMany(p => p.SheetsSportSeasonPlayerTeams)
                .HasForeignKey(d => d.SeasonCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerTeams_Sheets_SeasonCodes");

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsSportSeasonPlayerTeams)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerTeams_Sheets_SportCodes");

            entity.HasOne(d => d.TeamCodeNavigation).WithMany(p => p.SheetsSportSeasonPlayerTeams)
                .HasForeignKey(d => d.TeamCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerTeams_Sheets_TeamCodes");
        });

        modelBuilder.Entity<SheetsSportSeasonPlayerWeeklyStat>(entity =>
        {
            entity.HasKey(e => new { e.SportCode, e.SeasonCode, e.Week, e.PlayerId, e.StatCode });

            entity.ToTable("Sheets_SportSeasonPlayerWeeklyStats");

            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SeasonCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.StatCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Player).WithMany(p => p.SheetsSportSeasonPlayerWeeklyStats)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerWeeklyStats_Sheets_Players");

            entity.HasOne(d => d.SeasonCodeNavigation).WithMany(p => p.SheetsSportSeasonPlayerWeeklyStats)
                .HasForeignKey(d => d.SeasonCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerWeeklyStats_Sheets_SeasonCodes");

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsSportSeasonPlayerWeeklyStats)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerWeeklyStats_Sheets_SportCodes");

            entity.HasOne(d => d.StatCodeNavigation).WithMany(p => p.SheetsSportSeasonPlayerWeeklyStats)
                .HasForeignKey(d => d.StatCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonPlayerWeeklyStats_Sheets_StatCodes");
        });

        modelBuilder.Entity<SheetsSportSeasonSuppPlayerReview>(entity =>
        {
            entity.HasKey(e => new { e.SportCode, e.SeasonCode, e.SupplementalSourceId, e.PlayerId });

            entity.ToTable("Sheets_SportSeasonSuppPlayerReviews");

            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SeasonCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SupplementalSourceId).HasColumnName("SupplementalSourceID");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.ReviewUrl)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("ReviewURL");

            entity.HasOne(d => d.Player).WithMany(p => p.SheetsSportSeasonSuppPlayerReviews)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonSuppPlayerReviews_Sheets_Players");

            entity.HasOne(d => d.SeasonCodeNavigation).WithMany(p => p.SheetsSportSeasonSuppPlayerReviews)
                .HasForeignKey(d => d.SeasonCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonSuppPlayerReviews_Sheets_SeasonCodes");

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsSportSeasonSuppPlayerReviews)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonSuppPlayerReviews_Sheets_SportCodes");

            entity.HasOne(d => d.SupplementalSource).WithMany(p => p.SheetsSportSeasonSuppPlayerReviews)
                .HasForeignKey(d => d.SupplementalSourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSeasonSuppPlayerReviews_Sheets_SupplementalSources");
        });

        modelBuilder.Entity<SheetsSportSetting>(entity =>
        {
            entity.HasKey(e => e.SportSettingId);

            entity.ToTable("Sheets_SportSettings");

            entity.Property(e => e.SportSettingId).HasColumnName("SportSettingID");
            entity.Property(e => e.SettingCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SettingName)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.SettingValue)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsSportSettings)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportSettings_Sheets_SportCodes");
        });

        modelBuilder.Entity<SheetsSportTeamPlayer>(entity =>
        {
            entity.HasKey(e => new { e.SeasonCode, e.SportCode, e.TeamCode, e.PlayerId });

            entity.ToTable("Sheets_SportTeamPlayers");

            entity.Property(e => e.SeasonCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TeamCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
        });

        modelBuilder.Entity<SheetsSportTeamSeasonBye>(entity =>
        {
            entity.HasKey(e => new { e.SportCode, e.SeasonCode, e.TeamCode });

            entity.ToTable("Sheets_SportTeamSeasonByes");

            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SeasonCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TeamCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.SeasonCodeNavigation).WithMany(p => p.SheetsSportTeamSeasonByes)
                .HasForeignKey(d => d.SeasonCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportTeamSeasonByes_Sheets_SeasonCodes");

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsSportTeamSeasonByes)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportTeamSeasonByes_Sheets_SportCodes");

            entity.HasOne(d => d.TeamCodeNavigation).WithMany(p => p.SheetsSportTeamSeasonByes)
                .HasForeignKey(d => d.TeamCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SportTeamSeasonByes_Sheets_TeamCodes");
        });

        modelBuilder.Entity<SheetsStatCode>(entity =>
        {
            entity.HasKey(e => e.StatCode);

            entity.ToTable("Sheets_StatCodes");

            entity.Property(e => e.StatCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("A code representing some statistic.");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SheetsSupplementalSheet>(entity =>
        {
            entity.HasKey(e => e.SupplementalSheetId);

            entity.ToTable("Sheets_SupplementalSheets");

            entity.Property(e => e.SupplementalSheetId).HasColumnName("SupplementalSheetID");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.PositionCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SeasonCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SupplementalSourceId).HasColumnName("SupplementalSourceID");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("URL");

            entity.HasOne(d => d.PositionCodeNavigation).WithMany(p => p.SheetsSupplementalSheets)
                .HasForeignKey(d => d.PositionCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SupplementalSheets_Sheets_PositionCodes");

            entity.HasOne(d => d.SeasonCodeNavigation).WithMany(p => p.SheetsSupplementalSheets)
                .HasForeignKey(d => d.SeasonCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SupplementalSheets_Sheets_SeasonCodes");

            entity.HasOne(d => d.SportCodeNavigation).WithMany(p => p.SheetsSupplementalSheets)
                .HasForeignKey(d => d.SportCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SupplementalSheets_Sheets_SportCodes");

            entity.HasOne(d => d.SupplementalSource).WithMany(p => p.SheetsSupplementalSheets)
                .HasForeignKey(d => d.SupplementalSourceId)
                .HasConstraintName("FK_Sheets_SupplementalSheets_Sheets_SupplementalSources");
        });

        modelBuilder.Entity<SheetsSupplementalSheetItem>(entity =>
        {
            entity.HasKey(e => new { e.SupplementalSheetId, e.PlayerId });

            entity.ToTable("Sheets_SupplementalSheetItems");

            entity.Property(e => e.SupplementalSheetId).HasColumnName("SupplementalSheetID");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.BustTag).HasDefaultValueSql("((0))");
            entity.Property(e => e.Note)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.SleeperTag).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Player).WithMany(p => p.SheetsSupplementalSheetItems)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sheets_SupplementalSheetItems_Sheets_Players");

            entity.HasOne(d => d.SupplementalSheet).WithMany(p => p.SheetsSupplementalSheetItems)
                .HasForeignKey(d => d.SupplementalSheetId)
                .HasConstraintName("FK_Sheets_SupplementalSheetItems_Sheets_SupplementalSheets");
        });

        modelBuilder.Entity<SheetsSupplementalSource>(entity =>
        {
            entity.HasKey(e => e.SupplementalSourceId);

            entity.ToTable("Sheets_SupplementalSources");

            entity.Property(e => e.SupplementalSourceId).HasColumnName("SupplementalSourceID");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<SheetsTeamCode>(entity =>
        {
            entity.HasKey(e => e.TeamCode);

            entity.ToTable("Sheets_TeamCodes");

            entity.Property(e => e.TeamCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("The standard team abbreviation for a team, such as NOS for New Orleans Saints");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasComment("The abbreviation associated with a team.");
            entity.Property(e => e.City)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasComment("The city which represents the particular team.");
            entity.Property(e => e.Mascot)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasComment("The mascot for the particular team, such as the 'Saints'.");
            entity.Property(e => e.SportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StatMapId).HasColumnName("StatMapID");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("logs");

            entity.Property(e => e.AdditionalInfo)
                .IsUnicode(false)
                .HasColumnName("additional_info");
            entity.Property(e => e.AuthenticatedAs)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("authenticated_as");
            entity.Property(e => e.CallSite)
                .IsUnicode(false)
                .HasColumnName("call_site");
            entity.Property(e => e.ExceptionType)
                .IsUnicode(false)
                .HasColumnName("exception_type");
            entity.Property(e => e.Impersonating)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("impersonating");
            entity.Property(e => e.InnerException)
                .IsUnicode(false)
                .HasColumnName("inner_exception");
            entity.Property(e => e.Level)
                .IsUnicode(false)
                .HasColumnName("level");
            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.LoggedOnDate)
                .HasColumnType("datetime")
                .HasColumnName("logged_on_date");
            entity.Property(e => e.Logger)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("logger");
            entity.Property(e => e.MachineName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("machine_name");
            entity.Property(e => e.Message)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.SessionId)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("session_id");
            entity.Property(e => e.StackTrace)
                .IsUnicode(false)
                .HasColumnName("stack_trace");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    /// <summary>
    /// Updates Created and LastUpdated properties for entities that contain these columns
    /// </summary>
    private void UpdateAuditData()
    {
        foreach (var changedEntity in ChangeTracker.Entries().Where(changedEntity => changedEntity.Entity is SheetsCheatSheet))
        {
            if(changedEntity.Entity is SheetsCheatSheet entity)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        entity.Created = DateTime.Now;
                        entity.LastUpdated = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        Entry(entity).Property(x => x.Created).IsModified = false;
                        entity.LastUpdated = DateTime.Now;
                        break;
                }
            }
        }
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateAuditData();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}