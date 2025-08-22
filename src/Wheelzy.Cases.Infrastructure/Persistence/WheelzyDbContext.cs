using Microsoft.EntityFrameworkCore;
using Wheelzy.Cases.Domain.Entities;
using Wheelzy.Cases.Infrastructure.Persistence.Models;

namespace Wheelzy.Cases.Infrastructure.Persistence;

public partial class WheelzyDbContext(DbContextOptions<WheelzyDbContext> options) : DbContext(options)
{
    public DbSet<CarCase> CarCases => Set<CarCase>();
    public DbSet<Buyer> Buyers => Set<Buyer>();
    public DbSet<Status> Statuses => Set<Status>();
    public DbSet<Make> Makes => Set<Make>();
    public DbSet<Model> Models => Set<Model>();
    public DbSet<SubModel> SubModels => Set<SubModel>();
    public DbSet<ZipCode> ZipCodes => Set<ZipCode>();
    public DbSet<BuyerZipQuote> BuyerZipQuotes => Set<BuyerZipQuote>();
    public DbSet<CarCaseQuote> CarCaseQuotes => Set<CarCaseQuote>();
    public DbSet<CarCaseStatusHistory> CarCaseStatusHistories => Set<CarCaseStatusHistory>();
    public DbSet<CaseOverview> CaseOverview => Set<CaseOverview>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WheelzyDbContext).Assembly);

        modelBuilder.Entity<CarCase>(entity =>
        {
            entity.ToTable("CarCase");
            entity.HasKey(e => e.CarCaseId);
        });

        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.ToTable("Buyer");
            entity.HasKey(e => e.BuyerId);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");
            entity.HasKey(e => e.StatusId);
        });

        modelBuilder.Entity<Make>(entity =>
        {
            entity.ToTable("Make");
            entity.HasKey(e => e.MakeId);
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.ToTable("Model");
            entity.HasKey(e => e.ModelId);
        });

        modelBuilder.Entity<SubModel>(entity =>
        {
            entity.ToTable("SubModel");
            entity.HasKey(e => e.SubModelId);
        });

        modelBuilder.Entity<ZipCode>(entity =>
        {
            entity.ToTable("ZipCode");
            entity.HasKey(e => e.ZipCodeId);
        });

        modelBuilder.Entity<BuyerZipQuote>(entity =>
        {
            entity.ToTable("BuyerZipQuote");
            entity.HasKey(e => e.BuyerZipQuoteId);
        });

        modelBuilder.Entity<CarCaseQuote>(entity =>
        {
            entity.ToTable("CarCaseQuote");
            entity.HasKey(e => e.CarCaseQuoteId);
        });

        modelBuilder.Entity<CarCaseStatusHistory>(entity =>
        {
            entity.ToTable("CarCaseStatusHistory", t => t.HasTrigger("tr_CarCaseStatusHistory_PickedUp"));
            entity.HasKey(e => e.CarCaseStatusHistoryId);
        });

        modelBuilder.Entity<CaseOverview>(e =>
        {
            e.HasNoKey().ToView("vw_CaseOverview", "dbo");
        });
    }
}
