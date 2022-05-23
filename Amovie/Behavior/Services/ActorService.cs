using AutoMapper;
using Behaviour.Interfaces;
using BLL.Interfaces;
using Entities.Entities;
using Entities.Models.ActorDto;

namespace BLL.Services
{
    public class ActorService : IActorService
    {
        private readonly IRepository<Actor> _repository;
        private readonly IMapper _mapper;
        public ActorService(IRepository<Actor> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ActorDto>> GetAll()
        {
            var actors = await _repository.GetAll();
            var actorsDto = _mapper.Map<List<ActorDto>>(actors);

            return actorsDto;
        }
    }
}
