using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Roles = "Employee")]
public class ContactoPersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ContactoPersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ContactoPerDto>>> Get()
    {
        var contactoPer = await _unitOfWork.ContactoPers.GetAllAsync();
        return _mapper.Map<List<ContactoPerDto>>(contactoPer);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactoPerDto>> Get(int id)
    {
        var contactoPer = await _unitOfWork.ContactoPers.GetByIdAsync(id);
        if (contactoPer == null)
        {
            return NotFound();
        }
        return _mapper.Map<ContactoPerDto>(contactoPer);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContactoPer>> Post(ContactoPerDto contactoPerDto)
    {
        var contactoPer = _mapper.Map<ContactoPer>(contactoPerDto);
        _unitOfWork.ContactoPers.Add(contactoPer);
        await _unitOfWork.SaveAsync();
        if (contactoPer == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), contactoPerDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactoPerDto>> Put(
        int id,
        [FromBody] ContactoPerDto contactoPerDto
    )
    {
        if (contactoPerDto == null)
        {
            return NotFound();
        }
        var contactoPer = _mapper.Map<ContactoPer>(contactoPerDto);
        _unitOfWork.ContactoPers.Update(contactoPer);
        await _unitOfWork.SaveAsync();
        return contactoPerDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var contactoPer = await _unitOfWork.ContactoPers.GetByIdAsync(id);
        if (contactoPer == null)
        {
            return NotFound();
        }
        _unitOfWork.ContactoPers.Remove(contactoPer);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
