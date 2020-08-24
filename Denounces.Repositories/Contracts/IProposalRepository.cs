using System;
namespace Denounces.Repositories.Contracts
{
    using Domain.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProposalRepository : IRepository<Proposal>
    {
        Task<List<ProposalType>> GetTypes();
        Task<Proposal> AddProposalAsync(Proposal entity);
    }
}
