using AutoMapper;
using Behaviour.Interfaces;
using Entities.Entities;
using Entities.Models.AuthorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Behaviour.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _repository;
        private readonly IMapper _mapper;
        public AuthorService(IRepository<Author> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AuthorDto>> GetAll()
        {
            var authors = await _repository.GetAll();
            var authorsDto = _mapper.Map<List<AuthorDto>>(authors);

            return authorsDto;
        }
    }
}
