using Denounces.Domain.Helpers;
using System.Collections.Generic;

namespace Denounces.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string Denomym { get; set; }

        public ICollection<Province> Provinces { get; set; }
    }
}
