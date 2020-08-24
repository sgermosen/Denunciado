using Denounces.Domain.Helpers;

namespace Denounces.Domain.Entities
{
    public class ProposalFile : AuditEntity, IBaseEntity
    {
        public FileType FileType { get; set; }
        public string Description { get; set; } //Resumen

        public long ProposalId { get; set; }
        public Proposal Proposal { get; set; }
    }
}
