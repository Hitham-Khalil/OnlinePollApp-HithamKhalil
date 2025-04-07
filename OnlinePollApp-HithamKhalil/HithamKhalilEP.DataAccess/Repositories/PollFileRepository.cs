using HithamKhalilEP.Domain.Interfaces;
using Newtonsoft.Json;
using YourNameEP.DataAccess.Repositories;
using YourNameEP.Domain.Models;

namespace HithamKhalilEP.DataAccess.Repositories
{
    public class PollFileRepository : IPollRepository
    {
        private readonly string _filePath = "polls.json";

        public async Task CreatePoll(Poll poll)
        {
            var polls = await GetPolls();
            poll.Id = polls.Any() ? polls.Max(p => p.Id) + 1 : 1;
            poll.DateCreated = DateTime.Now;

            polls.Add(poll);
            var json = JsonConvert.SerializeObject(polls, Formatting.Indented);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<List<Poll>> GetPolls()
        {
            if (!File.Exists(_filePath))
                return new List<Poll>();

            var json = await File.ReadAllTextAsync(_filePath);
            var polls = JsonConvert.DeserializeObject<List<Poll>>(json) ?? new List<Poll>();

            return polls.OrderByDescending(p => p.DateCreated).ToList();
        }

        public async Task<Poll?> GetPollById(int id)
        {
            var polls = await GetPolls();
            return polls.FirstOrDefault(p => p.Id == id);
        }

        public async Task<bool> Vote(int pollId, int optionNumber)
        {
            var polls = await GetPolls();
            var poll = polls.FirstOrDefault(p => p.Id == pollId);

            if (poll == null) return false;

            switch (optionNumber)
            {
                case 1: poll.Option1VotesCount++; break;
                case 2: poll.Option2VotesCount++; break;
                case 3: poll.Option3VotesCount++; break;
                default: return false;
            }

            var json = JsonConvert.SerializeObject(polls, Formatting.Indented);
            await File.WriteAllTextAsync(_filePath, json);
            return true;
        }
    }
}
