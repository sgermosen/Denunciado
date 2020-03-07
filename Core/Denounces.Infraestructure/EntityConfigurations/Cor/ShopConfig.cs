namespace Denounces.Infraestructure.EntityConfigurations.Cor
{
    using Domain.Entities.Cor;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ShopConfig
    {
        //public ShopConfig(EntityTypeBuilder<Shop> entityBuilder)
        //{
        //    entityBuilder.HasKey(x => x.Id);
        //    entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        //    entityBuilder.Property(x => x.Address).HasMaxLength(200);
        //    entityBuilder.Property(x => x.Email).HasMaxLength(50);
        //    //  [RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$", ErrorMessage = "Entroduzca una pagina web valida")]
        //    entityBuilder.Property(x => x.WebAddress).HasMaxLength(50);
        //    //   [DataType(DataType.PhoneNumber)]
        //    entityBuilder.Property(x => x.Tel).HasMaxLength(15);

        //    //entityBuilder.HasOne(p => p.Owner)
        //    //    .WithMany(m => m.Shops)
        //    //    .HasForeignKey(s => s.OwnerId)
        //    //    .IsRequired();

        //}
    }
}
