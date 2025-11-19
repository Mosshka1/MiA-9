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
public class SessionsController : ControllerBase
{
    private readonly ISessionService _service;
    private readonly IMapper _mapper;

    public SessionsController(ISessionService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameSessionDto>>> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(_mapper.Map<List<GameSessionDto>>(list));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GameSessionDto>> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity is null) return NotFound();

        return Ok(_mapper.Map<GameSessionDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<GameSessionDto>> Create(CreateGameSessionDto dto)
    {
        var entity = _mapper.Map<GameSession>(dto);
        entity.MongoId = null!;

        var created = await _service.CreateAsync(entity);
        var result = _mapper.Map<GameSessionDto>(created);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<GameSessionDto>> Update(int id, CreateGameSessionDto dto)
    {
        var entity = _mapper.Map<GameSession>(dto);
        entity.MongoId = null!;

        var updated = await _service.UpdateAsync(id, entity);
        if (updated is null) return NotFound();

        return Ok(_mapper.Map<GameSessionDto>(updated));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
