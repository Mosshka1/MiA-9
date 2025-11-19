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
public class BookingsController : ControllerBase
{
    private readonly IBookingService _service;
    private readonly IMapper _mapper;

    public BookingsController(IBookingService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(_mapper.Map<List<BookingDto>>(list));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookingDto>> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity is null) return NotFound();

        return Ok(_mapper.Map<BookingDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<BookingDto>> Create(CreateBookingDto dto)
    {
        var entity = _mapper.Map<Booking>(dto);
        entity.MongoId = null!;

        var created = await _service.CreateAsync(entity);
        var result = _mapper.Map<BookingDto>(created);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<BookingDto>> Update(int id, CreateBookingDto dto)
    {
        var entity = _mapper.Map<Booking>(dto);
        entity.MongoId = null!;

        var updated = await _service.UpdateAsync(id, entity);
        if (updated is null) return NotFound();

        return Ok(_mapper.Map<BookingDto>(updated));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }

    [HttpPost("{id:int}/approve")]
    public async Task<ActionResult<BookingDto>> Approve(int id)
    {
        var updated = await _service.ApproveAsync(id);
        if (updated is null) return NotFound();

        return Ok(_mapper.Map<BookingDto>(updated));
    }
}
