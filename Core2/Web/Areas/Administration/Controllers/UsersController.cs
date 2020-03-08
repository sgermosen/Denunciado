using CommonTasks.Data;

namespace Denounces.Web.Areas.Administration.Controllers
{
    using Denounces.Domain.Entities;
    using Denounces.Domain.Entities.Cor;
    using Denounces.Infraestructure;
    using Denounces.Infraestructure.Extensions;
    using Denounces.Web.Areas.Administration.Models;
    using Denounces.Web.Controllers;
    using Denounces.Web.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Area("Administration")]
    [Authorize(Roles = "Admin,User")]
    public class UsersController : PsBaseController
    {
        private readonly RecordsHelper recordHelper;
        public UsersController(ApplicationDbContext context, IUserHelper userHelper, ICurrentUserFactory currentUser) : base(context, userHelper, currentUser)
        {
            recordHelper = new RecordsHelper(context);
        }

        public async Task<IActionResult> ResetPass(string id)
        {
            var users = await UserHelper.GetUserByIdAsync(id);
            var token = await UserHelper.GeneratePasswordResetTokenAsync(users);
            var random = new Random();
            var pass = random.Next(100000, 999999).ToString();
            await UserHelper.ResetPasswordAsync(users, token, pass);

            return RedirectToAction(nameof(Index),
                new { msg = $"La Nueva Contraseña del Usuario: {users.FullName}, es: {pass}" });
        }

        public async Task<IActionResult> DisableUser(string id)
        {
            var users = await UserHelper.GetUserByIdAsync(id);
            await UserHelper.DisabledUserAsync(users);

            return RedirectToAction(nameof(Index),
                new { msg = $"El Usuario: {users.FullName}, Fue Deshabilitado" });
        }

        public async Task<IActionResult> Index(string msg = "")
        {
            ViewBag.Msg = msg;
            var users = await UserHelper.GetAllUsersNoLockoutAsync(MyCurrentUser.Get.OwnerId);
            //foreach (var user in users)
            //{
            //    var myUser = await _userHelper.GetUserByIdAsync(user.Id);
            //    if (myUser != null)
            //    {
            //        user.IsAdmin = await this.userHelper.IsUserInRoleAsync(myUser, "Admin");
            //    }
            //}
            return View(users.OrderBy(p => p.LockoutEnabled));

        }

        public async Task<IActionResult> Edit(string id)
        {
            var countries = await Context.Countries.OrderBy(p => p.Name).ToListAsync();
            var ocupations = await Context.Ocupations.OrderBy(p => p.Name).ToListAsync();
            var schoolLevels = await Context.SchoolLevels.OrderBy(p => p.Name).ToListAsync();

            ViewData["Day"] = Enumerable.Range(1, 31)
        .Select(n => new SelectListItem
        {
            Value = n.ToString(),
            Text = n.ToString()
        }).ToList();

            ViewData["Month"] = Enumerable.Range(1, 12)
       .Select(n => new SelectListItem
       {
           Value = n.ToString(),
           Text = n.ToString()
       }).ToList();

            ViewData["Year"] = Enumerable.Range(1919, 2019)
       .Select(n => new SelectListItem
       {
           Value = n.ToString(),
           Text = n.ToString()
       }).ToList();

            var employee = new EmployeeView();
            var user = await UserHelper.GetUserByIdAsync(id);
            var emp = await Context.Employees.FirstOrDefaultAsync(p => p.User == user);
            emp.Transfer(ref employee);
            user.Transfer(ref employee);
            employee.Countries = GenericSelectList.CreateSelectList(countries, x => x.Id, x => x.Denomym);
            employee.Ocupations = GenericSelectList.CreateSelectList(ocupations, x => x.Id, x => x.Name);
            employee.SchoolLevels = GenericSelectList.CreateSelectList(schoolLevels, x => x.Id, x => x.Name);

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeView vm)
        {
            var owner = await GetOwnerAsync();

            if (string.IsNullOrEmpty((vm.Email)))
            {

                ModelState.AddModelError(string.Empty, "El Email es Obligatorio");

            }

            if (ModelState.IsValid)
            {
                var user = await UserHelper.GetUserByIdAsync(vm.Id);

                user.Name = vm.Name;
                user.Lastname = vm.Lastname;
                user.PhoneNumber = vm.PhoneNumber;
                user.Address = vm.Address;
                user.IsDoctor = vm.IsDoctor;
                user.IsTeacher = vm.IsTeacher;
                user.Rnc = vm.Rnc;
                user.Salary = vm.Salary;

                await UserHelper.UpdateUserAsync(user);

                if (vm.Email.ToLower() != user.Email.ToLower())
                {
                    user.Email = vm.Email;
                    user.UserName = vm.Email;
                    await UserHelper.UpdateUserAsync(user);

                    var token = await UserHelper.GenerateEmailChangeTokenAsync(user, vm.Email);
                    await UserHelper.UpdateEmailAsync(user, vm.Email, token);
                }
                else
                    await UserHelper.UpdateUserAsync(user);

                var employee = await Context.Employees.Where(p => p.User == user).FirstOrDefaultAsync();
                var path = employee.Imagen;

                if (vm.ImageFile != null && vm.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Employees", file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await vm.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Patients/{vm.ImageFile.FileName}";
                }

                vm.Imagen = path;

                var schoolLevel = await Context.SchoolLevels.FindAsync(vm.SchoolLevelId);
                var ocupation = await Context.Ocupations.FindAsync(vm.OcupationId);
                var country = await Context.Countries.FindAsync(vm.CountryId);

                employee.Date = vm.Date;
                employee.Gender = vm.Gender;
                employee.SchoolLevel = schoolLevel;
                employee.Country = country;
                employee.MaritalSituation = vm.MaritalSituation;
                employee.Ocupation = ocupation;
                employee.Imagen = vm.Imagen;
                employee.Record2 = vm.Record2;

                Context.Update(employee);

                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            };

            var countries = await Context.Countries.OrderBy(p => p.Name).ToListAsync();
            var ocupations = await Context.Ocupations.OrderBy(p => p.Name).ToListAsync();
            var schoolLevels = await Context.SchoolLevels.OrderBy(p => p.Name).ToListAsync();

            vm.Countries = GenericSelectList.CreateSelectList(countries, x => x.Id, x => x.Denomym);
            vm.Ocupations = GenericSelectList.CreateSelectList(ocupations, x => x.Id, x => x.Name);
            vm.SchoolLevels = GenericSelectList.CreateSelectList(schoolLevels, x => x.Id, x => x.Name);

            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var countries = await Context.Countries.OrderBy(p => p.Name).ToListAsync();
            var ocupations = await Context.Ocupations.OrderBy(p => p.Name).ToListAsync();
            var schoolLevels = await Context.SchoolLevels.OrderBy(p => p.Name).ToListAsync();

            ViewData["Day"] = Enumerable.Range(1, 31)
        .Select(n => new SelectListItem
        {
            Value = n.ToString(),
            Text = n.ToString()
        }).ToList();

            ViewData["Month"] = Enumerable.Range(1, 12)
        .Select(n => new SelectListItem
        {
            Value = n.ToString(),
            Text = n.ToString()
        }).ToList();

            ViewData["Year"] = Enumerable.Range(1919, 2019)
        .Select(n => new SelectListItem
        {
            Value = n.ToString(),
            Text = n.ToString()
        }).ToList();

            var employee = new EmployeeView
            {
                Countries = GenericSelectList.CreateSelectList(countries, x => x.Id, x => x.Denomym),
                Ocupations = GenericSelectList.CreateSelectList(ocupations, x => x.Id, x => x.Name),
                SchoolLevels = GenericSelectList.CreateSelectList(schoolLevels, x => x.Id, x => x.Name),
            };

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeView vm)
        {
            var owner = await GetOwnerAsync();

            if (string.IsNullOrEmpty((vm.Rnc)) == false)
            {
                var emp = Context.Employees.Any(p => p.User.Rnc.ToUpper() == vm.Rnc.ToUpper()
                                                  && p.CreatedUser.Shop.Owner == owner);

                if (emp)
                {
                    ModelState.AddModelError(string.Empty, "Esta cedula ya esta registrada");
                    // return View(vm);
                }
            }
            if (string.IsNullOrEmpty((vm.Email)))
            {

                ModelState.AddModelError(string.Empty, "El Email es Obligatorio");

            }

            if (string.IsNullOrEmpty(vm.Email) == false)
            {
                var emp = Context.Employees.Any(p => p.User.Email.ToUpper() == vm.Email.ToUpper()
                                                            && p.CreatedUser.Shop.Owner == owner);

                if (emp)
                {
                    ModelState.AddModelError(string.Empty, "Este Email ya esta registrado");
                    // return View(vm);
                }
            }

            if (ModelState.IsValid)
            {
                var shop = await Context.Shops.FindAsync(MyCurrentUser.Get.ShopId);

                var user = new ApplicationUser
                {
                    Name = vm.Name,
                    Lastname = vm.Lastname,
                    PhoneNumber = vm.PhoneNumber,
                    Email = vm.Email,
                    EmailConfirmed = true,
                    UserName = vm.Email,
                    Address = vm.Address,
                    IsDoctor = vm.IsDoctor,
                    IsTeacher = vm.IsTeacher,
                    Rnc = vm.Rnc,
                    Salary = vm.Salary,
                    Shop = shop
                };

                var result = await UserHelper.AddUserAsync(user, "824455");
                if (result != IdentityResult.Success)
                {
                    ModelState.AddModelError(string.Empty, "Usuario no pudo ser creado, Actualice el navegador antes de intentarlo de nuevo");
                    return View(vm);
                }
                await UserHelper.AddClaim(user, new Claim("OwnerId", owner.Id.ToString()));
                await UserHelper.AddClaim(user, new Claim("ShopId", shop.Id.ToString()));

                await UserHelper.AddUserToRoleAsync(user, "User");

                var path = string.Empty;

                if (vm.ImageFile != null && vm.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Employees", file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await vm.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Patients/{vm.ImageFile.FileName}";
                }

                vm.Imagen = path;
                vm.Record = recordHelper.GenerateRecord(owner);

                if (string.IsNullOrEmpty(vm.Record2))
                {
                    vm.Record2 = $"{vm.Record:00000}";
                }

                var schoolLevel = await Context.SchoolLevels.FindAsync(vm.SchoolLevelId);
                var ocupation = await Context.Ocupations.FindAsync(vm.OcupationId);
                var country = await Context.Countries.FindAsync(vm.CountryId);

                var userConected = await GetUserAsync();

                DateTime bornDate;
                try
                {
                    bornDate = DateTime.Parse($"{vm.Year.ToString()}/{string.Format(vm.Month.ToString(), "00")}/{string.Format(vm.Day.ToString(), "00")}");
                }
                catch (Exception)
                {
                    bornDate = DateTime.UtcNow;
                }

                var employee = new Employee
                {
                    Date = bornDate,
                    Gender = vm.Gender,
                    SchoolLevel = schoolLevel,
                    Country = country,
                    MaritalSituation = vm.MaritalSituation,
                    Ocupation = ocupation,
                    ReligionId = 1,
                    Imagen = vm.Imagen,
                    Record2 = vm.Record2,
                    Record = vm.Record,
                    CreatedUser = userConected,
                    User = user
                };

                Context.Add(employee);

                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            var countries = await Context.Countries.OrderBy(p => p.Name).ToListAsync();
            var ocupations = await Context.Ocupations.OrderBy(p => p.Name).ToListAsync();
            var schoolLevels = await Context.SchoolLevels.OrderBy(p => p.Name).ToListAsync();

            ViewData["Day"] = Enumerable.Range(1, 31)
            .Select(n => new SelectListItem
            {
                Value = n.ToString(),
                Text = n.ToString()
            }).ToList();

            ViewData["Month"] = Enumerable.Range(1, 12)
           .Select(n => new SelectListItem
           {
               Value = n.ToString(),
               Text = n.ToString()
           }).ToList();

            ViewData["Year"] = Enumerable.Range(1919, 2019)
           .Select(n => new SelectListItem
           {
               Value = n.ToString(),
               Text = n.ToString()
           }).ToList();

            vm.Countries = GenericSelectList.CreateSelectList(countries, x => x.Id, x => x.Denomym);
            vm.Ocupations = GenericSelectList.CreateSelectList(ocupations, x => x.Id, x => x.Name);
            vm.SchoolLevels = GenericSelectList.CreateSelectList(schoolLevels, x => x.Id, x => x.Name);

            return View(vm);
        }

        public async Task<IActionResult> AdminOff(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await UserHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await UserHelper.RemoveUserFromRoleAsync(user, "Admin");
            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AdminOn(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await UserHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await UserHelper.AddUserToRoleAsync(user, "Admin");
            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await UserHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await UserHelper.DeleteUserAsync(user);
            return this.RedirectToAction(nameof(Index));
        }

    }
}