using Denounces.Domain.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denounces.Domain.Entities
{
    public class Vote : AuditEntity, IBaseEntity
    {

        [Index("Vote_UserId_ProposalId_Index", IsUnique = true, Order = 1)]
        public string UserId { get; set; }

        [Index("Vote_UserId_ProposalId_Index", IsUnique = true, Order = 2)]
        public int ProposalId { get; set; }

        public int FavorVote { get; set; }

        public int NegativeVote { get; set; }

        public int NullVote { get; set; }

        public ApplicationUser User { get; set; }

        public Proposal Proposal { get; set; }
    }
}
