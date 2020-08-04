namespace Denounces.Web.Controllers
{
    using Domain.Entities;
    using Domain.Entities.Cor;
    using Domain.Entities.Med;
    using Helpers;
    using Infraestructure;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Repositories.Contracts.Cor;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = "Admin")]
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;

        private readonly IUserHelper _userHelper;
        private readonly ApplicationDbContext _context;

        public OwnersController(IOwnerRepository ownerRepository,
            IUserHelper userHelper, ApplicationDbContext context)
        {
            _ownerRepository = ownerRepository;
            _userHelper = userHelper;
            _context = context;
        }

        // GET: Owners
        public IActionResult Index()
        {
            return View(_ownerRepository.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.FindByIdAsync(id.Value);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Owner owner)
        {
            if (!ModelState.IsValid) return View(owner);
            var user = await _userHelper.GetUserByEmailAsync(owner.Email);
            if (user == null)
            {
                // await _ownerRepository.AddAsync(owner);

                await _context.Owners.AddAsync(owner);

                var shop = new Shop
                {
                    Name = "Sucursal Principal",
                    Owner = owner
                };

                await _context.Shops.AddAsync(shop);
                // await _context.SaveChangesAsync();
                user = new ApplicationUser
                {
                    Name = owner.Name,
                    Lastname = owner.LastName,
                    Email = owner.Email,
                    UserName = owner.Email,
                    PhoneNumber = owner.Tel,
                    Shop = shop,
                    IsDoctor = true,
                    IsTeacher = true
                };

                var result = await _userHelper.AddUserAsync(user, "824455");
                if (result != IdentityResult.Success)
                {
                    ModelState.AddModelError(string.Empty, "Usuario no pudo ser creado");
                    return View(owner);
                }

               // await _context.SaveChangesAsync();
                await _userHelper.AddClaim(user, new Claim("OwnerId", owner.Id.ToString()));
                await _userHelper.AddClaim(user, new Claim("ShopId", shop.Id.ToString()));

                await _userHelper.AddUserToRoleAsync(user, "Admin");
                await _userHelper.AddUserToRoleAsync(user, "User");

                var doctorInf = new Doctor
                {
                    User = user,
                    Exequartur = "Mi Exquartur 999",
                    CreationDate = DateTime.Today,
                    Cmd = "Mi cmd 999"
                };

                await _context.Doctors.AddAsync(doctorInf);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "El usuario ya esta registrado.");

            return View(owner);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.FindByIdAsync(id.Value);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Owner owner)
        {
            if (id != owner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ownerRepository.UpdateAsync(owner);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.FindByIdAsync(id.Value);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _ownerRepository.FindByIdAsync(id);
            await _ownerRepository.DeleteAsync(owner);
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(long id)
        {
            return _ownerRepository.Exists(id);
        }
    }
}
