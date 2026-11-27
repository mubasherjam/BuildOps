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

    [HttpGet]
    public async Task<ActionResult<
        IReadOnlyList<EquipmentCategoryDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _service
            .GetAllAsync(cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EquipmentCategoryDto>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _service
            .GetByIdAsync(id, cancellationToken);

        if (result is null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Category Not Found",
                Detail = $"Equipment category with ID {id} was not found.",
                Instance = HttpContext.Request.Path.Value
            });
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<EquipmentCategoryDto>> Create(
        [FromBody] CreateEquipmentCategoryDto request,
        CancellationToken cancellationToken)
    {
        var result = await _service
            .CreateAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }
}