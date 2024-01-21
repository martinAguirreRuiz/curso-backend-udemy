using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : ICommonService<BeerSelectDto, BeerInsertDto, BeerUpdateDto>
    {
        private IRepository<Beer> _beerRepository;
        private IMapper _mapper;
        public BeerService(IRepository<Beer> beerRepository, 
            IMapper mapper) 
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BeerSelectDto>> Get()
        {
            var beers = await _beerRepository.Get();

            return beers.Select(b => _mapper.Map<BeerSelectDto>(b));
        }

        public async Task<BeerSelectDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer == null)
            {
                return null;
            }

            var beerDto = _mapper.Map<BeerSelectDto>(beer);

            return beerDto;
        }

        public async Task<BeerSelectDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = _mapper.Map<Beer>(beerInsertDto);

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerSelectDto = _mapper.Map<BeerSelectDto>(beer);

            return beerSelectDto;
        }

        public async Task<BeerSelectDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer == null)
            {
                return null;
            }

            beer = _mapper.Map<BeerUpdateDto, Beer>(beerUpdateDto, beer);

            _beerRepository.Update(beer);
            await _beerRepository.Save();

            var beerSelectDto = _mapper.Map<BeerSelectDto>(beer);

            return beerSelectDto;
        }

        public async Task<BeerSelectDto> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer == null)
            {
                return null;
            }

            var beerSelectDto = _mapper.Map<BeerSelectDto>(beer);

            _beerRepository.Delete(beer);
            await _beerRepository.Save();
            return beerSelectDto;
        }
    }
}
