using System;
namespace Denounces.Repositories.Implementations
{
    using Denounces.Infraestructure;
    using Denounces.Repositories.Contracts;
    using Domain.Entities;

    public class ProposalTypeRepository : Repository<ProposalType>, IProposalTypeRepository
    {
        public ProposalTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
