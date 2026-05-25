namespace PJATK_APBD_Cw4_s32103.Entities;

public class Component
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int ComponentManufacturersId { get; set; }
    public int ComponentTypesId { get; set; }

    public ComponentManufacturer ComponentManufacturer { get; set; } = null!;
    public ComponentType ComponentType { get; set; } = null!;
    public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
}
