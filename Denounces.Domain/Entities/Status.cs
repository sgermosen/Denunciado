using Denounces.Domain.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Denounces.Domain.Entities
{
    public class Status : BaseEntity
    {
        public string Table { get; set; }

        public ICollection<Proposal> Proposals { get; set; }

        public Status()
        {
            Proposals = new Collection<Proposal>();
        }
    }
}
