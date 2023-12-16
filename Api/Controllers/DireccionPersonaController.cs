using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class DireccionPersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DireccionPersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DirPersonaDto>>> Get()
    {
        var dirPersona = await _unitOfWork.DirPersonas.GetAllAsync();
        return _mapper.Map<List<DirPersonaDto>>(dirPersona);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DirPersonaDto>> Get(int id)
    {
        var dirPersona = await _unitOfWork.DirPersonas.GetByIdAsync(id);
        if (dirPersona == null)
        {
            return NotFound();
        }
        return _mapper.Map<DirPersonaDto>(dirPersona);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DirPersona>> Post(DirPersonaDto dirPersonaDto)
    {
        var dirPersona = _mapper.Map<DirPersona>(dirPersonaDto);
        _unitOfWork.DirPersonas.Add(dirPersona);
        await _unitOfWork.SaveAsync();
        if (dirPersona == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), dirPersonaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DirPersonaDto>> Put(
        int id,
        [FromBody] DirPersonaDto dirPersonaDto
    )
    {
        if (dirPersonaDto == null)
        {
            return NotFound();
        }
        var dirPersona = _mapper.Map<DirPersona>(dirPersonaDto);
        _unitOfWork.DirPersonas.Update(dirPersona);
        await _unitOfWork.SaveAsync();
        return dirPersonaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var dirPersona = await _unitOfWork.DirPersonas.GetByIdAsync(id);
        if (dirPersona == null)
        {
            return NotFound();
        }
        _unitOfWork.DirPersonas.Remove(dirPersona);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
