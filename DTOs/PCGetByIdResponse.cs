namespace PJATK_APBD_Cw4_s32103.DTOs;

public class PCGetByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
    public List<PCComponentDto> Components { get; set; } = new();
}

public class PCComponentDto
{
    public int Amount { get; set; }
    public ComponentDto Component { get; set; } = null!;
}

public class ComponentDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ManufacturerDto Manufacturer { get; set; } = null!;
    public ComponentTypeDto Type { get; set; } = null!;
}

public class ManufacturerDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string FoundationDate { get; set; } = null!;
}

public class ComponentTypeDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string Name { get; set; } = null!;
}
