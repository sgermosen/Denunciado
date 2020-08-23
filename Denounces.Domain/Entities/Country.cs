using System.Collections.Generic;
using System.Collections.ObjectModel;
using Denounces.Domain.Helpers;

namespace Denounces.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string Denomym { get; set; }

        public ICollection<Province> Provinces { get; set; }
    }
}
