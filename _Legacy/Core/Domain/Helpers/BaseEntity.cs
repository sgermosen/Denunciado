namespace Denounces.Domain.Helpers
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        string Name { get; set; }

       // bool Deleted { get; set; }      
    }

    public class BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

       // public bool Deleted { get; set; }       

    }
}
