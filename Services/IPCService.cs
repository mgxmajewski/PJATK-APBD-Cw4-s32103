using PJATK_APBD_Cw4_s32103.DTOs;

namespace PJATK_APBD_Cw4_s32103.Services;

public interface IPCService
{
    Task<List<PCGetAllResponse>> GetAllAsync();
    Task<PCGetByIdResponse?> GetByIdWithComponentsAsync(int id);
    Task<PCCreateResponse> CreateAsync(PCCreateRequest request);
    Task<bool> UpdateAsync(int id, PCUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
