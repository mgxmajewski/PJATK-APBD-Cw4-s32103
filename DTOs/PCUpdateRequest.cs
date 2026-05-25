namespace PJATK_APBD_Cw4_s32103.DTOs;

public class PCUpdateRequest
{
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}
