using AutoMapper;
using Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ScenariosController : ControllerBase
{
    private readonly IScenarioService _service;
    private readonly IMapper _mapper;

    public ScenariosController(IScenarioService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScenarioDto>>> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(_mapper.Map<List<ScenarioDto>>(list));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ScenarioDto>> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity is null) return NotFound();

        return Ok(_mapper.Map<ScenarioDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<ScenarioDto>> Create(CreateScenarioDto dto)
    {
        var entity = _mapper.Map<Scenario>(dto);
        entity.MongoId = null!;

        var created = await _service.CreateAsync(entity);
        var result = _mapper.Map<ScenarioDto>(created);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ScenarioDto>> Update(int id, CreateScenarioDto dto)
    {
        var entity = _mapper.Map<Scenario>(dto);
        entity.MongoId = null!;

        var updated = await _service.UpdateAsync(id, entity);
        if (updated is null) return NotFound();

        return Ok(_mapper.Map<ScenarioDto>(updated));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
