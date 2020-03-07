using Denounces.Domain.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Denounces.Domain.Entities.Cor
{
    public class Status : BaseEntity
    {
        public string Table { get; set; }

        public ICollection<Person> People { get; set; }

        public Status()
        {
            People = new Collection<Person>();
        }
    }
}
