
using Denounces.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Denounces.Web.Controllers
{
    using Domain.Entities;
    using Infraestructure;
    using Infraestructure.Extensions;
    public class PsBaseController : Controller
    {
        protected ApplicationDbContext Context;
        protected IUserHelper UserHelper;
        protected GenericSelectList GenericSelectList;
        public readonly ICurrentUserFactory MyCurrentUser;


        public PsBaseController(ApplicationDbContext context,
            IUserHelper userHelper, ICurrentUserFactory currentUser)
        {
            Context = context;
            UserHelper = userHelper;
            GenericSelectList = new GenericSelectList();
            MyCurrentUser = currentUser;
        }

        protected async Task<ApplicationUser> GetUserAsync()
        {
            return await UserHelper.GetUserByEmailAsync(User.Identity.Name);
        }

        //protected async Task<ApplicationUser> GetFullUserAsync()
        //{
        //    var user = await Context.Users.Include(s => s.Shop).ThenInclude(o => o.Owner)
        //        .Where(p => p.Email == User.Identity.Name).FirstOrDefaultAsync();

        //    return user;
        //}



    }
}