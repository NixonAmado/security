using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Roles = "Employee")]
public class TipoContactoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoContactoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoContactoDto>>> Get()
    {
        var tipoContacto = await _unitOfWork.TipoContactos.GetAllAsync();
        return _mapper.Map<List<TipoContactoDto>>(tipoContacto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoContactoDto>> Get(int id)
    {
        var tipoContacto = await _unitOfWork.TipoContactos.GetByIdAsync(id);
        if (tipoContacto == null)
        {
            return NotFound();
        }
        return _mapper.Map<TipoContactoDto>(tipoContacto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoContacto>> Post(TipoContactoDto tipoContactoDto)
    {
        var tipoContacto = _mapper.Map<TipoContacto>(tipoContactoDto);
        _unitOfWork.TipoContactos.Add(tipoContacto);
        await _unitOfWork.SaveAsync();
        if (tipoContacto == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), tipoContactoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoContactoDto>> Put(
        int id,
        [FromBody] TipoContactoDto tipoContactoDto
    )
    {
        if (tipoContactoDto == null)
        {
            return NotFound();
        }
        var tipoContacto = _mapper.Map<TipoContacto>(tipoContactoDto);
        _unitOfWork.TipoContactos.Update(tipoContacto);
        await _unitOfWork.SaveAsync();
        return tipoContactoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var tipoContacto = await _unitOfWork.TipoContactos.GetByIdAsync(id);
        if (tipoContacto == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoContactos.Remove(tipoContacto);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
