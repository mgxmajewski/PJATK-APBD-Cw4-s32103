using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw4_s32103.DTOs;
using PJATK_APBD_Cw4_s32103.Services;

namespace PJATK_APBD_Cw4_s32103.Controllers;

[ApiController]
[Route("api/pcs")]
public class PCsController : ControllerBase
{
    private readonly IPCService _pcService;

    public PCsController(IPCService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _pcService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetByIdWithComponents(int id)
    {
        var result = await _pcService.GetByIdWithComponentsAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PCCreateRequest request)
    {
        var result = await _pcService.CreateAsync(request);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PCUpdateRequest request)
    {
        var updated = await _pcService.UpdateAsync(id, request);
        if (!updated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _pcService.DeleteAsync(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }
}
