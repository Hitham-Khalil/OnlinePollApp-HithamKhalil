using YourNameEP.Domain.Models;

namespace HithamKhalilEP.Domain.Interfaces
{
    public interface IPollRepository
    {
        Task CreatePoll(Poll poll);
        Task<List<Poll>> GetPolls();
        Task<Poll?> GetPollById(int id);
        Task<bool> Vote(int pollId, int optionNumber);
    }
}