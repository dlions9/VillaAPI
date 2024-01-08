using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using VillaAPI.Data;
using VillaAPI.Models;
using VillaAPI.Models.DTO;

namespace VillaAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {


        private readonly ILogger<VillaAPIController> _logger;
        private readonly ApplicationDbContext _context;

        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationDbContext Context)
        {
            _logger = logger;
            _context = Context;
        }

        [HttpGet]
        public  IActionResult GetVillas()
        {
            _logger.LogInformation("getting all villas");
            return Ok(_context.Villas.ToList());
        }


        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetVilla(int id)
        {
            if(id == 0)
            {
                _logger.LogError($"invalid ID + {id}");
                return BadRequest();
            }
            var villa = _context.Villas.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateVilla([FromBody] VillaModel villa)
        {
     

            if (_context.Villas.FirstOrDefault(u => u.Name.ToLower() ==  villa.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Villa already exists");
                return BadRequest(ModelState);
            }
            
            
            
            
            if (villa == null)
            {
                return BadRequest(villa);
            }
            if(villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            
            _context.Villas.Add(villa);
            _context.SaveChanges();

            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa); 
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = _context.Villas.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            _context.Villas.Remove(villa);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpPut("{id:int}", Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villa)
        {
            if(id != villa.Id || villa == null )
            {
                return BadRequest();
            }


            VillaModel villaModel = new()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };

            _context.Villas.Update(villaModel);
            _context.SaveChanges();

            return NoContent();

            
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> villa)
        {
            if(id == 0 ||  villa == null )
            {
                return BadRequest();
            }

            var villaFromDb = _context.Villas.FirstOrDefault(u => u.Id ==id);

            VillaDTO villaDto = new()
            {
                Amenity = villaFromDb.Amenity,
                Details = villaFromDb.Details,
                Id = villaFromDb.Id,
                ImageUrl = villaFromDb.ImageUrl,
                Name = villaFromDb.Name,
                Occupancy = villaFromDb.Occupancy,
                Rate = villaFromDb.Rate,
                Sqft = villaFromDb.Sqft
            };

            if(villaFromDb == null)
            {
                return BadRequest();
            }
            villa.ApplyTo(villaDto, ModelState);

            VillaModel model = new VillaModel()
            {
                Amenity = villaDto.Amenity,
                Details = villaDto.Details,
                Id = villaDto.Id,
                ImageUrl = villaDto.ImageUrl,
                Name = villaDto.Name,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft

            };

            _context.Villas.Update(model);
            _context.SaveChanges();
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
