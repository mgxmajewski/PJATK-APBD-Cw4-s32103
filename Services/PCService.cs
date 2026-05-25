using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw4_s32103.DAL;
using PJATK_APBD_Cw4_s32103.DTOs;
using PJATK_APBD_Cw4_s32103.Entities;

namespace PJATK_APBD_Cw4_s32103.Services;

public class PCService : IPCService
{
    private readonly AppDbContext _context;

    public PCService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PCGetAllResponse>> GetAllAsync()
    {
        return await _context.PCs
            .Select(p => new PCGetAllResponse
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock
            })
            .ToListAsync();
    }

    public async Task<PCGetByIdResponse?> GetByIdWithComponentsAsync(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PCComponents)
                .ThenInclude(pc => pc.Component)
                    .ThenInclude(c => c.ComponentManufacturer)
            .Include(p => p.PCComponents)
                .ThenInclude(pc => pc.Component)
                    .ThenInclude(c => c.ComponentType)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc == null)
            return null;

        return new PCGetByIdResponse
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,
            Components = pc.PCComponents.Select(pcComp => new PCComponentDto
            {
                Amount = pcComp.Amount,
                Component = new ComponentDto
                {
                    Code = pcComp.Component.Code,
                    Name = pcComp.Component.Name,
                    Description = pcComp.Component.Description,
                    Manufacturer = new ManufacturerDto
                    {
                        Id = pcComp.Component.ComponentManufacturer.Id,
                        Abbreviation = pcComp.Component.ComponentManufacturer.Abbreviation,
                        FullName = pcComp.Component.ComponentManufacturer.FullName,
                        FoundationDate = pcComp.Component.ComponentManufacturer.FoundationDate.ToString("yyyy-MM-dd")
                    },
                    Type = new ComponentTypeDto
                    {
                        Id = pcComp.Component.ComponentType.Id,
                        Abbreviation = pcComp.Component.ComponentType.Abbreviation,
                        Name = pcComp.Component.ComponentType.Name
                    }
                }
            }).ToList()
        };
    }

    public async Task<PCCreateResponse> CreateAsync(PCCreateRequest request)
    {
        var pc = new PC
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return new PCCreateResponse
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> UpdateAsync(int id, PCUpdateRequest request)
    {
        var pc = await _context.PCs.FirstOrDefaultAsync(p => p.Id == id);
        if (pc == null)
            return false;

        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.CreatedAt = request.CreatedAt;
        pc.Stock = request.Stock;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.PCs.FirstOrDefaultAsync(p => p.Id == id);
        if (pc == null)
            return false;

        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();
        return true;
    }
}
