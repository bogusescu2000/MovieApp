using AutoMapper;
using Behaviour.Interfaces;
using BLL.Interfaces;
using Entities.Entities;
using Entities.Models.GenreDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _repository;
        private readonly IMapper _mapper;

        public GenreService(IRepository<Genre> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<GenreDto>> GetAll()
        {
            var genres = await _repository.GetAll();
            var genresDto = _mapper.Map<List<GenreDto>>(genres);

            return genresDto;
        }
    }
}
