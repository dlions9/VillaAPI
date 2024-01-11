using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VillaAPI.Models;
using VillaAPI.Models.DTO;
using VillaAPI.Repository.IRepository;

namespace VillaAPI.Controllers
{
    [Route("api/VillaNumber")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {

        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IMapper _mapper;
        private readonly ApiResponseModel _responseModel;
        private readonly IVillaRepository _villaRepository;
        public VillaNumberController(IVillaNumberRepository villaNumberRepository, IMapper mapper, IVillaRepository villaRepository)
        {
            _villaNumberRepository = villaNumberRepository;
            _mapper = mapper;
            _responseModel = new ApiResponseModel();
            _villaRepository = villaRepository;
        }

        
        [HttpGet]
        public async Task<ActionResult<ApiResponseModel>> GetAllAsync()
        {
            try
            {
                IEnumerable<VillaNumberModel> villaNumberModels = await _villaNumberRepository.GetAllAsync();
                _responseModel.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberModels);
                _responseModel.StatusCode = HttpStatusCode.OK;
                return Ok(_responseModel);
            }
            catch (Exception ex)
            {
                _responseModel.IsSuccess = false;
                _responseModel.ErrorMessages = new List<string> { ex.ToString()};
            }
            return _responseModel;


        }

        [HttpGet("{id:int}", Name = "GetAsync")]
        public async Task<ActionResult<ApiResponseModel>> GetAsync(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                VillaNumberModel villaNumberModel = await _villaNumberRepository.GetAsync(u => u.VillaNo == id);

                if(villaNumberModel == null)
                {
                    return NotFound();
                }
                _responseModel.Result = _mapper.Map<VillaNumberDTO>(villaNumberModel);
                _responseModel.StatusCode = HttpStatusCode.OK;
                return Ok(_responseModel);
            }
            catch (Exception ex)
            {
                _responseModel.IsSuccess = false;
                _responseModel.ErrorMessages = new List<string> { ex.ToString() };
            }
            return (_responseModel);

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseModel>> CreateAsync([FromBody] VillaNumberDTO villaNumberDTO)
        {
            try
            { 
                if (villaNumberDTO == null)
                {
                    return BadRequest();
                }

                
                {

                }
                VillaNumberModel villaNumberModel = _mapper.Map<VillaNumberModel>(villaNumberDTO);
                
                if(await _villaRepository.GetAsync(u => u.Id == villaNumberDTO.VNumber) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa ID is invalid!");
                    return BadRequest(ModelState);
                }

                await _villaNumberRepository.CreateAsync(villaNumberModel);
                _responseModel.Result = _mapper.Map<VillaNumberDTO>(villaNumberModel);
                _responseModel.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetAsync", new { id = villaNumberModel.VillaNo }, _responseModel);
            }
            catch (Exception ex)
            {
                _responseModel.IsSuccess = false;
                _responseModel.ErrorMessages = new List<string> { ex.ToString() };
            }
            return (_responseModel);


        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponseModel>> DeleteAsync(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return BadRequest();
                }

                VillaNumberModel villaNumberModel = await _villaNumberRepository.GetAsync(u => u.VillaNo == id);

                if (villaNumberModel == null)
                {
                    return NotFound();
                }

                await _villaNumberRepository.DeleteAsync(villaNumberModel);
                _responseModel.StatusCode = HttpStatusCode.NoContent;
                _responseModel.IsSuccess = true;
                return Ok(_responseModel);
            }
            catch (Exception ex)
            {
                _responseModel.IsSuccess = false;
                _responseModel.ErrorMessages = new List<string> { ex.ToString() };
            }
            return (_responseModel);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponseModel>> UpdateAsync(int? id, [FromBody] VillaNumberUpdateDTO villaNumberUpdateDTO)
        {
            try
            {
                if (villaNumberUpdateDTO == null || id != villaNumberUpdateDTO.VillaNo)
                {
                    return BadRequest();
                }

                if (await _villaRepository.GetAsync(u => u.Id == villaNumberUpdateDTO.VNumber) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa ID is invalid!");
                    return BadRequest(ModelState);
                }

                VillaNumberModel villaNumberModel = _mapper.Map<VillaNumberModel>(villaNumberUpdateDTO);
                await _villaNumberRepository.UpdateAsync(villaNumberModel);
                _responseModel.StatusCode= HttpStatusCode.NoContent;
                _responseModel.IsSuccess = true;
                return Ok(_responseModel);  
            }
            catch (Exception ex)
            {
                _responseModel.IsSuccess = false;
                _responseModel.ErrorMessages = new List<string> { ex.ToString() };
            }
            return (_responseModel);


        }
    }
}
