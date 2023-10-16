using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Roles = "Employee")]
public class TipoDireccionController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoDireccionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoDireccionDto>>> Get()
    {
        var tipoDireccion = await _unitOfWork.TipoDirecciones.GetAllAsync();
        return _mapper.Map<List<TipoDireccionDto>>(tipoDireccion);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoDireccionDto>> Get(int id)
    {
        var tipoDireccion = await _unitOfWork.TipoDirecciones.GetByIdAsync(id);
        if (tipoDireccion == null)
        {
            return NotFound();
        }
        return _mapper.Map<TipoDireccionDto>(tipoDireccion);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoDireccion>> Post(TipoDireccionDto tipoDireccionDto)
    {
        var tipoDireccion = _mapper.Map<TipoDireccion>(tipoDireccionDto);
        _unitOfWork.TipoDirecciones.Add(tipoDireccion);
        await _unitOfWork.SaveAsync();
        if (tipoDireccion == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), tipoDireccionDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoDireccionDto>> Put(
        int id,
        [FromBody] TipoDireccionDto tipoDireccionDto
    )
    {
        if (tipoDireccionDto == null)
        {
            return NotFound();
        }
        var tipoDireccion = _mapper.Map<TipoDireccion>(tipoDireccionDto);
        _unitOfWork.TipoDirecciones.Update(tipoDireccion);
        await _unitOfWork.SaveAsync();
        return tipoDireccionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var tipoDireccion = await _unitOfWork.TipoDirecciones.GetByIdAsync(id);
        if (tipoDireccion == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoDirecciones.Remove(tipoDireccion);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
