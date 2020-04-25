using Denounces.App.Models;
using System.Threading.Tasks;

namespace Denounces.App.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
    }
}
