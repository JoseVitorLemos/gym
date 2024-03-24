using Microsoft.AspNetCore.Mvc;
using Gym.Services.DTO;
using Gym.Services.IndividualEntityService;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Presentation.Controllers.IndividualEntity;

[Route("api/[controller]")]
[Authorize(Policy = "AllValidUsers")]
public class IndividualEntityController : BaseController
{
    private readonly IIndividualEntityService _individualEntityService;

    public IndividualEntityController(IHttpContextAccessor httpContext, 
            IIndividualEntityService individualEntityService) : base(httpContext)
        => _individualEntityService = individualEntityService;

    [HttpGet("ListIndividualEntity")]
    public async Task<IActionResult> ListIndividualEntity()
        => GetResponse(await _individualEntityService.ListIndividualEntity());

    [HttpGet("GetIndividualEntity")]
    public async Task<IActionResult> GetIndividualEntity(Guid id)
        => GetResponse(await _individualEntityService.GetIndividualEntity(id));

    [HttpGet("FindIndividualEntityByName")]
    public async Task<List<IndividualEntityDTO>> FindIndividualEntityByName(string name, int page, int pageSize)
        => await _individualEntityService.FindIndividualEntityByName(name, page, pageSize);

    [HttpPost("InsertFitnessClient")]
    public async Task InsertFitnessClient([FromBody] IndividualEntityDTO model)
        => await _individualEntityService.InsertFitnessClient(model);

    [HttpPut("UpdateIndividualEntity")]
    public async Task UpdateIndividualEntity([FromBody] IndividualEntityDTO model)
        => await _individualEntityService.UpdateIndividualEntity(model);

    [HttpPatch("EnableOrDisableIndividualEntity")]
    public async Task EnableOrDisableIndividualEntity(Guid id, bool status)
        => await _individualEntityService.EnableOrDisableIndividualEntity(id, status);
}
