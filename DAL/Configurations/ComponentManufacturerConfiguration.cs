using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PJATK_APBD_Cw4_s32103.Entities;

namespace PJATK_APBD_Cw4_s32103.DAL.Configurations;

public class ComponentManufacturerConfiguration : IEntityTypeConfiguration<ComponentManufacturer>
{
    public void Configure(EntityTypeBuilder<ComponentManufacturer> builder)
    {
        builder.ToTable("ComponentManufacturers");
        builder.HasKey(cm => cm.Id);
        builder.Property(cm => cm.Id).ValueGeneratedOnAdd();
        builder.Property(cm => cm.Abbreviation).HasMaxLength(30).IsRequired();
        builder.Property(cm => cm.FullName).HasMaxLength(300).IsRequired();
        builder.Property(cm => cm.FoundationDate).HasColumnType("date").IsRequired();

        builder.HasData(
            new ComponentManufacturer { Id = 1, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateTime(1969, 5, 1) },
            new ComponentManufacturer { Id = 2, Abbreviation = "NV", FullName = "NVIDIA Corporation", FoundationDate = new DateTime(1993, 4, 5) },
            new ComponentManufacturer { Id = 3, Abbreviation = "COR", FullName = "Corsair Gaming Inc.", FoundationDate = new DateTime(1994, 1, 1) }
        );
    }
}
