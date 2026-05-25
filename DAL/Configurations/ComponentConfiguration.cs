using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PJATK_APBD_Cw4_s32103.Entities;

namespace PJATK_APBD_Cw4_s32103.DAL.Configurations;

public class ComponentConfiguration : IEntityTypeConfiguration<Component>
{
    public void Configure(EntityTypeBuilder<Component> builder)
    {
        builder.ToTable("Components");
        builder.HasKey(c => c.Code);
        builder.Property(c => c.Code).HasColumnType("char(10)").IsRequired();
        builder.Property(c => c.Name).HasMaxLength(300).IsRequired();
        builder.Property(c => c.Description).HasColumnType("nvarchar(max)");

        builder.HasOne(c => c.ComponentManufacturer)
            .WithMany(cm => cm.Components)
            .HasForeignKey(c => c.ComponentManufacturersId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.ComponentType)
            .WithMany(ct => ct.Components)
            .HasForeignKey(c => c.ComponentTypesId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Component { Code = "CPU0000001", Name = "Ryzen 7 7800X3D", Description = "8-core gaming processor", ComponentManufacturersId = 1, ComponentTypesId = 1 },
            new Component { Code = "GPU0000001", Name = "RTX 4080 Super", Description = "High-end gaming graphics card", ComponentManufacturersId = 2, ComponentTypesId = 2 },
            new Component { Code = "RAM0000001", Name = "Corsair Vengeance DDR5 16GB", Description = "DDR5 RAM module 16GB", ComponentManufacturersId = 3, ComponentTypesId = 3 }
        );
    }
}
