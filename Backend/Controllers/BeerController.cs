using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Backend.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private StoreContext _context;
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private ICommonService<BeerSelectDto, BeerInsertDto, BeerUpdateDto> _beerService;
        public BeerController(StoreContext context,
            IValidator<BeerInsertDto> beerInsertValidator,
            IValidator<BeerUpdateDto> beerUpdateValidator, 
            [FromKeyedServices("BeerService")]ICommonService<BeerSelectDto, BeerInsertDto, BeerUpdateDto> beerService)
        {
            _context = context;
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerSelectDto>> Get() => 
            await _beerService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerSelectDto>> GetById(int id)
        {
            var beerDto = await _beerService.GetById(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerSelectDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validation = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var beerSelectDto = await _beerService.Add(beerInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = beerSelectDto.Id }, beerSelectDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerSelectDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var validator = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

            if(!validator.IsValid)
            {
                return BadRequest(validator.Errors);
            }

            var beerSelectDto = await _beerService.Update(id, beerUpdateDto);

            return beerSelectDto == null ? NotFound() : Ok(beerSelectDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerSelectDto>> Delete(int id)
        {
            var beerSelectDto = await _beerService.Delete(id);

            return beerSelectDto == null ? NotFound() : Ok(beerSelectDto);
        }
    }
}
