namespace Denounces.Web.Models
{
    public class ApplicationUserResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string FullName => $"{Name} {Lastname}";
    }
}
