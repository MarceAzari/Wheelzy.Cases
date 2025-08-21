using Microsoft.EntityFrameworkCore;
using Wheelzy.Cases.Domain.Entities;
using Wheelzy.Cases.Infrastructure.Persistence.Models;

namespace Wheelzy.Cases.Infrastructure;

public partial class WheelzyDbContext : DbContext
{
    public WheelzyDbContext(DbContextOptions<WheelzyDbContext> options) : base(options) { }

    public DbSet<CarCase> CarCases => Set<CarCase>();
    public DbSet<Buyer> Buyers => Set<Buyer>();
    public DbSet<Status> Statuses => Set<Status>();
    public DbSet<Make> Makes => Set<Make>();
    public DbSet<Model> Models => Set<Model>();
    public DbSet<SubModel> SubModels => Set<SubModel>();
    public DbSet<ZipCode> ZipCodes => Set<ZipCode>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CarCaseQuote> CarCaseQuotes => Set<CarCaseQuote>();
    public DbSet<CarCaseStatusHistory> CarCaseStatusHistories => Set<CarCaseStatusHistory>();
    public DbSet<BuyerZipQuote> BuyerZipQuotes => Set<BuyerZipQuote>();
    public DbSet<CaseOverview> CaseOverview => Set<CaseOverview>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WheelzyDbContext).Assembly);

        modelBuilder.Entity<CarCase>(entity =>
        {
            entity.ToTable("CarCase", "dbo");
            entity.HasKey(e => e.CarCaseId);
            entity.Property(e => e.CarCaseId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.ToTable("Buyer", "dbo");
            entity.HasKey(e => e.BuyerId);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status", "dbo");
            entity.HasKey(e => e.StatusId);
        });

        modelBuilder.Entity<Make>(entity =>
        {
            entity.ToTable("Make", "dbo");
            entity.HasKey(e => e.MakeId);
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.ToTable("Model", "dbo");
            entity.HasKey(e => e.ModelId);
        });

        modelBuilder.Entity<SubModel>(entity =>
        {
            entity.ToTable("SubModel", "dbo");
            entity.HasKey(e => e.SubModelId);
        });

        modelBuilder.Entity<ZipCode>(entity =>
        {
            entity.ToTable("ZipCode", "dbo");
            entity.HasKey(e => e.ZipCodeId);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer", "dbo");
            entity.HasKey(e => e.CustomerId);
        });

        modelBuilder.Entity<CarCaseQuote>(entity =>
        {
            entity.ToTable("CarCaseQuote", "dbo");
            entity.HasKey(e => e.CarCaseQuoteId);
        });

        modelBuilder.Entity<CarCaseStatusHistory>(entity =>
        {
            entity.ToTable("CarCaseStatusHistory", "dbo");
            entity.HasKey(e => e.CarCaseStatusHistoryId);
        });

        modelBuilder.Entity<BuyerZipQuote>(entity =>
        {
            entity.ToTable("BuyerZipQuote", "dbo");
            entity.HasKey(e => e.BuyerZipQuoteId);
        });

        modelBuilder.Entity<CaseOverview>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("vw_CaseOverview");
        });
    }
}
