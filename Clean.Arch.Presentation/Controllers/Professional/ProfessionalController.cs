using Microsoft.AspNetCore.Mvc;
using Clean.Arch.Services.DTO;
using Clean.Arch.Services.ProfessionalService;
using Microsoft.AspNetCore.Authorization;

namespace Clean.Arch.Presentation.Controllers.IndividualEntity;

[Route("v1/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,EmailConfirmation")]
public class ProfessionalController : BaseController
{
    private readonly IProfessionalService _professionalService;

    public ProfessionalController(IProfessionalService professionalService)
        => _professionalService = professionalService;

    [HttpGet("ListProfessional")]
    public async Task<IActionResult> ListProfessional()
        => GetResponse(await _professionalService.ListProfessional());

    [HttpGet("GetProfessional")]
    public async Task<IActionResult> GetProfessional(Guid id)
        => GetResponse(await _professionalService.GetProfessional(id));

    [HttpPost("InsertProfessional")]
    public async Task<IActionResult> InsertProfessional([FromBody] ProfessionalDTO model)
        => ApiResponse(await _professionalService.InsertProfessional(model));

    [HttpPut("UpdateProfessional")]
    public async Task<IActionResult> UpdateProfessional([FromBody] ProfessionalDTO model)
        => ApiResponse(await _professionalService.UpdateProfessional(model));

    [HttpPatch("EnableOrDisableProfessional")]
    public async Task<IActionResult> EnableOrDisableProfessional(Guid id, bool status)
        => ApiResponse(await _professionalService.EnableOrDisableProfessional(id, status));
}
