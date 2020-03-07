using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DenunciadoBackEnd.Models
{
    public class DataContext: DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }
        //stop the cascade drop
        //if i delete all the flowers, dont permit that delete the history of bills
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }

        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintType> ComplaintTypes { get; set; }

    }
}