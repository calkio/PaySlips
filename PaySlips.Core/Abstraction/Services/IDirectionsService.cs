using PaySlips.Core.Model.Lesson;

namespace PaySlips.Core.Abstraction.Services
{
    public interface IDirectionsService
    {
        Task<List<Direction>> GetAllDirections();
        Task<List<Group>> GetAllGroups();
        Task<List<Group>> GetGroupsByDirection();
    }
}
