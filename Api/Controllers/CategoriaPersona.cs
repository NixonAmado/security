using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class CategoriaPersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriaPersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CategoriaPerDto>>> Get()
    {
        var categoriaPer = await _unitOfWork.CategoriaPers.GetAllAsync();
        return _mapper.Map<List<CategoriaPerDto>>(categoriaPer);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaPerDto>> Get(int id)
    {
        var categoriaPer = await _unitOfWork.CategoriaPers.GetByIdAsync(id);
        if (categoriaPer == null)
        {
            return NotFound();
        }
        return _mapper.Map<CategoriaPerDto>(categoriaPer);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaPer>> Post(CategoriaPerDto categoriaPerDto)
    {
        var categoriaPer = _mapper.Map<CategoriaPer>(categoriaPerDto);
        _unitOfWork.CategoriaPers.Add(categoriaPer);
        await _unitOfWork.SaveAsync();
        if (categoriaPer == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), categoriaPerDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaPerDto>> Put(
        int id,
        [FromBody] CategoriaPerDto categoriaPerDto
    )
    {
        if (categoriaPerDto == null)
        {
            return NotFound();
        }
        var categoriaPer = _mapper.Map<CategoriaPer>(categoriaPerDto);
        _unitOfWork.CategoriaPers.Update(categoriaPer);
        await _unitOfWork.SaveAsync();
        return categoriaPerDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var categoriaPer = await _unitOfWork.CategoriaPers.GetByIdAsync(id);
        if (categoriaPer == null)
        {
            return NotFound();
        }
        _unitOfWork.CategoriaPers.Remove(categoriaPer);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
