namespace Denounces.Repositories.Contracts.Cor
{
    using Domain.Entities.Cor;
    using System.Threading.Tasks;

    public interface IPersonRepository:IRepository<Person>
    {
        //Task<Owner> FullCreation(Owner entity);

        Task<Person> AddPersonAsync(Person entity);
    }
}
