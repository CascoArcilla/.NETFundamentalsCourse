using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Persisten
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
        }

        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<VisitEntity> Visits { get; set; }

        // Sobre escribir el método OnModelCreating para configurar la entidad PersonEntity,
        // esto permite definir mejor los tipos de datos que se requieren para los campos de
        // las tablas.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonEntity>(entity =>
            {
                entity.ToTable("Persons");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.Code).IsUnique();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(15);
                
                entity.Ignore(e => e.FullName);

                entity.Property<DateTime>("CreatedAT").IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property<DateTime>("UpdatedAT").IsRequired().HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<VisitEntity>(entity =>
            {
                entity.ToTable("Visits");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.PersonId).IsRequired();
                entity.Property(e => e.EntryTime).IsRequired();
                entity.Property(e => e.ExitTime).IsRequired();

                entity.Ignore(e => e.isActive);
                entity.Ignore(e => e.Duration);

                entity.HasOne(e => e.Person).WithMany().HasForeignKey(e => e.PersonId).OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.PersonId);
                entity.HasIndex(e => e.EntryTime);
                entity.HasIndex(e => new { e.PersonId, e.EntryTime });

                entity.Property<DateTime>("CreatedAT").IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property<DateTime>("UpdatedAT").IsRequired().HasDefaultValueSql("GETUTCDATE()");
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Realizar este interceptación para actualizar los campos de auditoría (CreateAT y UpdateAT)
            // antes de guardar los cambios en la base de datos u alguna otra accion que se requiera antes
            // de guardar los cambios.

            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            // Obtener las entradas que han sido modificadas o agregadas para actualizar los campos de auditoría.
            var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Metadata.FindProperty("UpdatedAt") != null);

            foreach (var entry in entries)
            {
                entry.Property("UpdatedAT").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}
