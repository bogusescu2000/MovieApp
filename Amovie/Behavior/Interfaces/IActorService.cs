using Entities.Models.ActorDto;

namespace BLL.Interfaces
{
    public interface IActorService
    {
        Task<List<ActorDto>> GetAll();
    }
}
