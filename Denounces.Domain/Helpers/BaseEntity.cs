namespace Denounces.Domain.Helpers
{
    public interface IBaseEntity
    {
        long Id { get; set; }

        string Name { get; set; }
   }

    public class BaseEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }     
}
}
