using Denounces.Domain.Helpers;
using System.Collections.Generic;

namespace Denounces.Domain.Entities
{
    public class Status : BaseEntity
    {
        public string Table { get; set; }

        public ICollection<Proposal> Proposals { get; set; }

    }
}
