using System;
using System.Threading.Tasks;
using Denounces.Domain.Entities; 
using Denounces.Infraestructure;
using Denounces.Repositories.Contracts.Cor;

namespace Denounces.Repositories.Implementations.Cor
{
    using Domain.Entities.Cor;
    
    //public class OwnerRepository : Repository<Owner>,IOwnerRepository
    //{
    //    public OwnerRepository(ApplicationDbContext context) : base(context)
    //    {
    //    }
    //    public async Task<Owner> FullCreation(Owner entity)
    //    {
    //        await Context.Owners.AddAsync(entity);
          
    //        var shop = new Shop
    //        {
    //            Name = "Sucursal Principal",
    //            Owner = entity
    //        };

    //        await Context.Shops.AddAsync(shop);

    //        var user = new ApplicationUser()
    //        {
    //            Name = entity.Name,
    //            Lastname = entity.LastName,
    //            PhoneNumber= entity.Tel,
    //            Email = entity.Email,
    //            Shop = shop
    //        };
    //        await Context.Users.AddAsync(user);

    //        //var rol = new Rol
    //        //var rol = new Rol
    //        //{
    //        //    AuthorId = author.AuthorId,
    //        //    Name = "AdMiDocSubCaj",
    //        //    Description = "Administrador, Doctor, Supervisor y Cajero",
    //        //    StatusId = 1,
    //        //    IsCashier = true,
    //        //    IsDoctor = true
    //        //};
    //        // await Context.Roles.AddAsync(rol);
    //        //Db.Rols.Add(rol);

    //        //await Db.SaveChangesAsync();


    //        var doctorInf = new Doctor
    //        {
    //            User = user,
    //            Exequartur = "Mi Exquartur 999",
    //            CreationDate = DateTime.Today,
    //            Cmd = "Mi cmd 999"
    //        };

    //        await Context.Doctors.AddAsync(doctorInf);

    //        //var brand = new Brand { AuthorId = author.AuthorId, Name = "_N/A" };

    //        //Db.Brands.Add(brand);

    //        //var category = new Category { AuthorId = author.AuthorId, Name = "_N/A" };

    //        //Db.Categories.Add(category);

    //        //var unids = new Measure { AuthorId = author.AuthorId, Name = "_N/A" };

    //        //Db.Measures.Add(unids);

    //        //var suplier = new Supplier { AuthorId = author.AuthorId, Name = "_N/A" };

    //        //Db.Suppliers.Add(suplier);
    //        //var shopUser = new ShopUser
    //        //{
    //        //    ShopId = shop.ShopId,
    //        //    UserId = user.UserId
    //        //};

    //        //_db.ShopUsers.Add(shopUser);

    //       // await Db.SaveChangesAsync();

    //        // var optionList = await _db.Options.ToListAsync();

    //        //var planOptions = await Db.AuthorPlanOptions.Where(p => p.AuthorPlanId == author.AuthorPlanId)
    //        //    .ToListAsync();

    //        //foreach (var item in planOptions)
    //        //{
    //        //    var optionforRol = new OptionRol
    //        //    {
    //        //        Create = true,
    //        //        Delete = true,
    //        //        Details = true,
    //        //        Edit = true,
    //        //        Index = true,
    //        //        FromDate = DateTime.Today,
    //        //        OptionId = item.OptionId,
    //        //        RolId = rol.RolId,
    //        //        ToDate = DateTime.Today,
    //        //        Undefined = true,
    //        //        StatusId = 1
    //        //    };

    //        //    Db.OptionRols.Add(optionforRol);
    //        //}

    //        //for (var i = 1; i < 4; i++)
    //        //{
    //        //    var asignSegOptions = new OptionRol
    //        //    {
    //        //        Create = true,
    //        //        Delete = true,
    //        //        Details = true,
    //        //        Edit = true,
    //        //        Index = true,
    //        //        FromDate = DateTime.Today,
    //        //        OptionId = i,
    //        //        RolId = rol.RolId,
    //        //        ToDate = DateTime.Today,
    //        //        Undefined = true,
    //        //        StatusId = 1
    //        //    };

    //        //    Db.OptionRols.Add(asignSegOptions);
    //        //}

    //        //await Db.SaveChangesAsync();
           
    //        await SaveAllAsync();
    //        return entity;
    //    }

        
    //}
}
