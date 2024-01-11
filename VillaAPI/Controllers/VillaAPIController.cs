using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using VillaAPI.Data;
using VillaAPI.Models;
using VillaAPI.Models.DTO;
using VillaAPI.Repository.IRepository;

namespace VillaAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        private readonly ApiResponseModel _responseModel;
        private readonly ILogger<VillaAPIController> _logger;
        private readonly IVillaRepository _villaRepository;
        private readonly IMapper _mapper;


        public VillaAPIController(ILogger<VillaAPIController> logger, IVillaRepository villaRepository, IMapper mapper)
        {
            _logger = logger;
            _villaRepository = villaRepository;
            _mapper = mapper;
            _responseModel = new();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseModel>> GetVillas()
        {
            try
            {


                IEnumerable<VillaModel> villaList = await _villaRepository.GetAllAsync();
                _responseModel.Result = _mapper.Map<List<VillaDTO>>(villaList);
                _responseModel.StatusCode = HttpStatusCode.OK;

                return Ok(_responseModel);
            }
            catch (Exception ex)
            {
                _responseModel.IsSuccess = false;
                _responseModel.ErrorMessages = new List<string> { ex.ToString() };

            }
            return _responseModel;
        }


        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseModel>> GetVilla(int id)
        {
            try
            {


                if (id == 0)
                {
                    _logger.LogError($"invalid ID + {id}");
                    return BadRequest();
                }
                var villa = await _villaRepository.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                _responseModel.Result = _mapper.Map<VillaDTO>(villa);
                _responseModel.StatusCode = HttpStatusCode.OK;
                return Ok(_responseModel);
            }
            catch (Exception ex)
            {
                _responseModel.IsSuccess = false;
                _responseModel.ErrorMessages = new List<string> { ex.ToString() };

            }
            return _responseModel;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponseModel>> CreateVilla([FromBody] VillaCreateDTO CreateVillaDTO)
        {
            try
            {


                if (await _villaRepository.GetAsync(u => u.Name.ToLower() == CreateVillaDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("", "Villa already exists");
                    return BadRequest(ModelState);
                }

                if (CreateVillaDTO == null)
                {
                    return BadRequest(CreateVillaDTO);
                }

                VillaModel villaModel = _mapper.Map<VillaModel>(CreateVillaDTO);


                await _villaRepository.CreateAsync(villaModel);

                _responseModel.Result = _mapper.Map<VillaDTO>(villaModel);
                _responseModel.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = villaModel.Id }, _responseModel);
            }
            catch (Exception ex)
            {
                _responseModel.IsSuccess = false;
                _responseModel.ErrorMessages = new List<string> { ex.ToString() };

            }
            return _responseModel;
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseModel>> Delete(int id)
        {
            try
            {


                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _villaRepository.GetAsync(u => u.Id == id);

                if (villa == null)
                {
                    return NotFound();
                }
                await _villaRepository.DeleteAsync(villa);

                _responseModel.StatusCode = HttpStatusCode.NoContent;
                _responseModel.IsSuccess = true;

                return Ok(_responseModel);
            }
            catch (Exception ex)
            {
                _responseModel.IsSuccess = false;
                _responseModel.ErrorMessages = new List<string> { ex.ToString() };

            }
            return _responseModel;
        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseModel>> UpdateVilla(int id, [FromBody] VillaUpdateDTO UpdateVillaDTO)
        {
            try
            {


                if (id != UpdateVillaDTO.Id || UpdateVillaDTO == null)
                {
                    return BadRequest();
                }


                VillaModel villaModel = _mapper.Map<VillaModel>(UpdateVillaDTO);

                await _villaRepository.UpdateVillaAsync(villaModel);


                _responseModel.StatusCode = HttpStatusCode.NoContent;
                _responseModel.IsSuccess = true;
                return Ok(_responseModel);
            }
            catch (Exception ex)
            {
                _responseModel.IsSuccess = false;
                _responseModel.ErrorMessages = new List<string> { ex.ToString() };

            }
            return _responseModel;


        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (id == 0 || patchDTO == null)
            {
                return BadRequest();
            }

            var villaFromDb = await _villaRepository.GetAsync(u => u.Id == id, tracked: false);


            VillaUpdateDTO villaDto = _mapper.Map<VillaUpdateDTO>(villaFromDb);

            if (villaFromDb == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaDto, ModelState);

            VillaModel villaModel = _mapper.Map<VillaModel>(villaDto);

            await _villaRepository.UpdateVillaAsync(villaModel);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
