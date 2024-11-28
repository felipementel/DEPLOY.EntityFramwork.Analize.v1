using DEPLOY.EntityFramework.v1.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace DEPLOY.EntityFramework.v1.Infra.Database;

public class DeployDbContext : DbContext
{
    public DbSet<Pessoa> Pessoas { get; set; }

    public DbSet<Contrato> Contratos { get; set; }

    //public DbSet<PessoaContrato> PessoaContratos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer("Server=tcp:sql-canal-deploy.database.windows.net,1433;Initial Catalog=daploy-ef-analizer;Persist Security Info=False;User ID=felipementel;Password=Abcd1234%;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
                    p => p.EnableRetryOnFailure(
                        maxRetryCount: 20,
                        maxRetryDelay: TimeSpan.FromSeconds(3),
                        errorNumbersToAdd: null)
            .MigrationsHistoryTable("_ControleMigracoes", "dbo"))
            .EnableSensitiveDataLogging() // habilita os parametros das instrucoes sql
            .LogTo(Console.WriteLine, LogLevel.Debug);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration<PessoaContrato>(new PessoaContratoEntityConfiguration());
        modelBuilder.ApplyConfiguration<Pessoa>(new PessoaEntityConfiguration());
        modelBuilder.ApplyConfiguration<Contrato>(new ContratoEntityConfiguration());
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

public class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity<T>
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.UseTpcMappingStrategy();
    }
}

//     public class PessoaContratoEntityConfiguration : IEntityTypeConfiguration<PessoaContrato>
//     {
//         public void Configure(EntityTypeBuilder<PessoaContrato> builder)
//         {
//             builder
//                 .ToTable("PessoaContrato");

//             builder.HasKey(p => new { p.PessoaId, p.ContratoId });
//             // builder.Ignore(p => p.PessoaId);
//             // builder.Ignore(p => p.ContratoId);
//             // builder
//             //     .Property(x => x.PessoaId)
//             //     .HasColumnName("PessoaId")
//             //     .HasColumnType("uniqueidentifier")
//             //     .IsRequired();

//             // builder.Property(x => x.ContratoId)
//             //     .HasColumnName("ContratoId")
//             //     .HasColumnType("int")
//             //     .IsRequired();

//             builder
//                 .Property(x => x.PessoaId)
//                 .HasColumnName("PessoaId")
//                 .HasColumnType("uniqueidentifier")
//                 .IsRequired();

//             builder.Property(x => x.ContratoId)
//                 .HasColumnName("ContratoId")
//                 .HasColumnType("int")
//                 .IsRequired();

//             builder
//                 .HasOne(x => x.Pessoa)
//                 .WithMany()
//                 .HasForeignKey(x => x.PessoaId)
//                 .IsRequired(false)
//                 .OnDelete(DeleteBehavior.NoAction);

//             builder
//                 .HasOne(x => x.Contrato)
//                 .WithMany()
//                 .HasForeignKey(x => x.ContratoId)
//                 .IsRequired(false)
//                 .OnDelete(DeleteBehavior.NoAction);
//         }
//     }
