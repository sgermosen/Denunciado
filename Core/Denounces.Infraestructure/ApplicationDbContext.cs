namespace Denounces.Infraestructure
{
    using Denounces.Domain.Helpers;
    using Domain.Entities;
    using Domain.Entities.Cor;
    using EntityConfigurations;
    using EntityConfigurations.Cor;
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
            //modelBuilder.Entity<ClassAsistant>(entity =>
            //{
            //    entity.HasKey(n => new { n.ClasseId, n.AsistanceId });

            //    entity.HasOne(n => n.Classe)
            //        .WithMany(u => u.Asistants)
            //        .HasForeignKey(x => x.ClasseId)
            //        .IsRequired();

            //    entity.HasOne(n => n.Asistance)
            //        .WithMany(u => u.Asistants)
            //        .HasForeignKey(x => x.AsistanceId)
            //        .IsRequired();
            //});

            //modelBuilder.Entity<FoodBeneficiary>(entity =>
            //{
            //    entity.HasKey(n => new { n.FoodId, n.ComensalId });

            //    entity.HasOne(n => n.Food)
            //        .WithMany(u => u.Comensals)
            //        .HasForeignKey(x => x.FoodId)
            //        .IsRequired();

            //    entity.HasOne(n => n.Comensal)
            //        .WithMany(u => u.Comensals)
            //        .HasForeignKey(x => x.ComensalId)
            //        .IsRequired();
            //});

            //modelBuilder.Entity<MaterialRequisitionDetail>(entity =>
            //{
            //    entity.HasKey(n => new { n.ManualMaterial, n.MaterialRequisitionId });

            //    entity.HasOne(n => n.MaterialRequisition)
            //        .WithMany(u => u.MaterialRequisitionDetails)
            //        .HasForeignKey(x => x.MaterialRequisitionId)
            //        .IsRequired();

            //    //entity.HasOne(n => n.ManualMaterial)
            //    //    .WithMany(u => u.MaterialRequisitionDetails)
            //    //    .HasForeignKey(x => x.MaterialId)
            //    //    .IsRequired();
            //});

            //modelBuilder.Entity<ActivityParticipant>(entity =>
            //{
            //    entity.HasKey(n => new { n.ActivityId, n.ParticipantId });

            //    entity.HasOne(n => n.Activity)
            //        .WithMany(u => u.Participants)
            //        .HasForeignKey(x => x.ActivityId)
            //        .IsRequired();

            //    entity.HasOne(n => n.Participant)
            //        .WithMany(u => u.Participants)
            //        .HasForeignKey(x => x.ParticipantId)
            //        .IsRequired();
            //});

            //modelBuilder.Entity<Beneficiary>(entity =>
            //{
            //    entity.HasKey(n => new { n.BenefitId, n.ReceiverId });

            //    entity.HasOne(n => n.Benefit)
            //        .WithMany(u => u.Beneficiaries)
            //        .HasForeignKey(x => x.BenefitId)
            //        .IsRequired();

            //    entity.HasOne(n => n.Receiver)
            //        .WithMany(u => u.Beneficiaries)
            //        .HasForeignKey(x => x.ReceiverId)
            //        .IsRequired();
            //});

            //modelBuilder.Entity<ActivityBeneficiary>(entity =>
            //{
            //    entity.HasKey(n => new { n.BenefitId, n.ActivityId });

            //    entity.HasOne(n => n.Benefit)
            //        .WithMany(u => u.ActivityBeneficiaries)
            //        .HasForeignKey(x => x.BenefitId)
            //        .IsRequired();

            //    entity.HasOne(n => n.Activity)
            //        .WithMany(u => u.ActivityBeneficiaries)
            //        .HasForeignKey(x => x.ActivityId)
            //        .IsRequired();
            //});

            //modelBuilder.Entity<SignatureMember>(entity =>
            //{
            //    entity.HasKey(n => new { n.SignatureId, n.SignatureMemberId });

            //    entity.HasOne(n => n.Member)
            //        .WithMany(u => u.Members)
            //        .HasForeignKey(x => x.SignatureMemberId)
            //        .IsRequired();

            //    entity.HasOne(n => n.Signature)
            //        .WithMany(u => u.Members)
            //        .HasForeignKey(x => x.SignatureId)
            //        .IsRequired();
            //});

            //modelBuilder.Entity<Related>(entity =>
            //{
            //    // entity.HasKey(n => new { n.RelationedPersonId, n.RelatedPersonId });

            //    entity.HasOne(n => n.RelatedPerson)
            //        .WithMany(u => u.RelatedPeople)
            //        .HasForeignKey(x => x.RelatedPersonId)
            //        .IsRequired();

            //    entity.HasOne(n => n.RelationedPerson)
            //        .WithMany(u => u.RelationedPeople)
            //        .HasForeignKey(x => x.RelationedPersonId)
            //        .IsRequired();
            //});

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
            AddMyFilters(ref modelBuilder);
            modelBuilder.ApplyConfiguration(new ApplicationUserConfig());
            //registering the configurations for all the classes
            //  new ApplicationUserConfig(modelBuilder.Entity<ApplicationUser>());
           // new OwnerConfig(modelBuilder.Entity<Owner>());
           // new ShopConfig(modelBuilder.Entity<Shop>());

        }

       
        public DbSet<Image> Images { get; set; }
       
        public DbSet<Country> Countries { get; set; }
        public DbSet<Ocupation> Ocupations { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<SchoolLevel> SchoolLevels { get; set; }
       
        public DbSet<Person> People { get; set; }
       
        public DbSet<Status> Status { get; set; }

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
               // modelBuilder.Entity<Person>().HasQueryFilter(x => x.CreatedUser.Owner.Id == user.OwnerId && !x.Deleted);
                modelBuilder.Entity<Person>().HasQueryFilter(x => !x.Deleted);
            }

            #region SoftDeleted

            //modelBuilder.Entity<ApplicationUser>().HasQueryFilter(x => x.Owner.Id == user.OwnerId && !x.Deleted);
            //modelBuilder.Entity<Wallet>().HasQueryFilter(x =>  && !x.State);
            //  modelBuilder.Entity<ApplicationUser>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<Image>().HasQueryFilter(x => !x.Deleted);           
            modelBuilder.Entity<Person>().HasQueryFilter(x => !x.Deleted);

            #endregion
        }

    }
}
