using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers;

[ApiController]
[Route("api/VillaAPI")]
public class VillaAPIController : Controller
{
    private readonly ILogger<VillaAPIController> _logger;

    public VillaAPIController(ILogger<VillaAPIController> logger)
    {
        _logger = logger;
    }
    
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        _logger.LogInformation("Getting all villas");
        return Ok(VillaStore.villaList);
    }
    
    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id==0)
        {
            _logger.LogError("Get villa error with id" + id);
            return BadRequest();
        }

        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        if (villa==null)
        {
            return NotFound();
        }
        return Ok(villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDto)
    {
        if (VillaStore.villaList.FirstOrDefault(u=>u.Name.ToLower()==villaDto.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError","Villa already Exists!");
            return BadRequest(ModelState);
        }
        if (villaDto == null)
        {
            return BadRequest(villaDto);
        }

        if (villaDto.Id >0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        villaDto.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        VillaStore.villaList.Add(villaDto);
        return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
    }

    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        VillaStore.villaList.Remove(villa);
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDto)
    {
        if (villaDto == null || id != villaDto.Id)
        {
            return BadRequest();
        }
        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        villa.Name = villaDto.Name;
        villa.Sqft = villaDto.Sqft;
        villa.Occupancy = villaDto.Occupancy;
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
    {
        if (patchDTO == null || id ==0)
        {
            return BadRequest();
        }
        
        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return BadRequest();
        }
        
        patchDTO.ApplyTo(villa, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return NoContent();
    }
}