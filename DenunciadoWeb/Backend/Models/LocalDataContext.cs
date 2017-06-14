using Domain;

namespace Backend.Models
{
    public class LocalDataContext:DataContext
    {
        public System.Data.Entity.DbSet<Domain.UserType> UserTypes { get; set; }
    }
}