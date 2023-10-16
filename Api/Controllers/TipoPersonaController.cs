using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Roles = "Employee")]
public class TipoPersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoPersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoPersonaDto>>> Get()
    {
        var tipoPersona = await _unitOfWork.TipoPersonas.GetAllAsync();
        return _mapper.Map<List<TipoPersonaDto>>(tipoPersona);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoPersonaDto>> Get(int id)
    {
        var tipoPersona = await _unitOfWork.TipoPersonas.GetByIdAsync(id);
        if (tipoPersona == null)
        {
            return NotFound();
        }
        return _mapper.Map<TipoPersonaDto>(tipoPersona);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoPersona>> Post(TipoPersonaDto tipoPersonaDto)
    {
        var tipoPersona = _mapper.Map<TipoPersona>(tipoPersonaDto);
        _unitOfWork.TipoPersonas.Add(tipoPersona);
        await _unitOfWork.SaveAsync();
        if (tipoPersona == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), tipoPersonaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoPersonaDto>> Put(
        int id,
        [FromBody] TipoPersonaDto tipoPersonaDto
    )
    {
        if (tipoPersonaDto == null)
        {
            return NotFound();
        }
        var tipoPersona = _mapper.Map<TipoPersona>(tipoPersonaDto);
        _unitOfWork.TipoPersonas.Update(tipoPersona);
        await _unitOfWork.SaveAsync();
        return tipoPersonaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var tipoPersona = await _unitOfWork.TipoPersonas.GetByIdAsync(id);
        if (tipoPersona == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoPersonas.Remove(tipoPersona);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
