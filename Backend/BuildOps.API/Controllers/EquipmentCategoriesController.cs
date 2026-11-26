
using BuildOps.Application.DTOs.EquipmentCategories;
using BuildOps.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BuildOps.API.Controllers;

[ApiController]
[Route("api/equipment-categories")]
public class EquipmentCategoriesController : ControllerBase
{
    private readonly IEquipmentCategoryService _service;

    public EquipmentCategoriesController(
        IEquipmentCategoryService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<EquipmentCategoryDto>> Create(
        [FromBody] CreateEquipmentCategoryDto request,
        CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(
            request,
            cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            result);
    }
}
