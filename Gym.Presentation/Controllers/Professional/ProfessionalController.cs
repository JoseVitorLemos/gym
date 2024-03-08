using Microsoft.AspNetCore.Mvc;
using Gym.Services.DTO;
using Gym.Services.ProfessionalService;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Presentation.Controllers.IndividualEntity;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Authenticated")]
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
