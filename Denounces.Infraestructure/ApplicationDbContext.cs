namespace Denounces.Infraestructure
{
    using Denounces.Domain.Helpers;
    using Denounces.Infraestructure.EntityConfigurations.Cor;
    using Domain.Entities;
    using EntityConfigurations;
    using Infraestructure.Extensions;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ICurrentUserFactory _currentUser;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserFactory currentUser = null) : base(options)
        {
            _currentUser = currentUser;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    var table = entityType.Relational().TableName;
            //    if (table.StartsWith("AspNet"))
            //    {
            //        entityType.Relational().TableName = table.Substring(6);
            //    }
            //};

            AddMyFilters(ref modelBuilder);
            //registering the configurations for all the classes

            modelBuilder.ApplyConfiguration(new ApplicationUserConfig());
            //  new ApplicationUserConfig(modelBuilder.Entity<ApplicationUser>());
            new ProposalConfig(modelBuilder.Entity<Proposal>());
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<ProposalFile> ProposalFiles { get; set; }
        public DbSet<ProposalType> ProposalTypes { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public override int SaveChanges()
        {
            MakeAudit();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            MakeAudit();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            MakeAudit();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void MakeAudit()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(
                x => x.Entity is AuditEntity
                    && (
                    x.State == EntityState.Added
                    || x.State == EntityState.Modified
                )
            );

            var user = new CurrentUser();

            if (_currentUser != null)
            {
                user = _currentUser.Get;
            }

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is AuditEntity entity)
                {
                    var date = DateTime.Now;
                    string userId = user.UserId;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = date;
                        entity.CreatedBy = userId;
                    }
                    else if (entity is ISoftDeleted && ((ISoftDeleted)entity).Deleted)
                    {
                        entity.DeletedAt = date;
                        entity.DeletedBy = userId;
                    }

                    Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    Entry(entity).Property(x => x.CreatedBy).IsModified = false;

                    entity.UpdatedAt = date;
                    entity.UpdatedBy = userId;
                }
            }
        }

        private void AddMyFilters(ref ModelBuilder modelBuilder)
        {
            var user = new CurrentUser();

            if (_currentUser != null)
            {
                user = _currentUser.Get;
                //   modelBuilder.Entity<Course>().HasQueryFilter(x => x.CreatedUser.Shop.Owner.Id == user.OwnerId && !x.Deleted);
            }

            #region SoftDeleted

            //modelBuilder.Entity<ApplicationUser>().HasQueryFilter(x => x.Owner.Id == user.OwnerId && !x.Deleted);
            modelBuilder.Entity<ProposalFile>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<Proposal>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<ProposalType>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<Vote>().HasQueryFilter(x => !x.Deleted);

            #endregion
        }

    }
}
