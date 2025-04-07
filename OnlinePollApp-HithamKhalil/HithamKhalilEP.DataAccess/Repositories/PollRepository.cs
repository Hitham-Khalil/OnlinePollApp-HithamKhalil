using Microsoft.EntityFrameworkCore;
using YourNameEP.DataAccess.Context;
using YourNameEP.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourNameEP.DataAccess.Repositories
{
    public class PollRepository
    {
        private readonly PollDbContext _context;

        // Constructor injection to inject the DbContext
        public PollRepository(PollDbContext context)
        {
            _context = context;
        }

        // CreatePoll method to add a new poll to the database
        public async Task CreatePoll(Poll poll)
        {
            if (poll == null)
                throw new ArgumentNullException(nameof(poll));

            _context.Polls.Add(poll); // Adds the poll to the DbContext
            await _context.SaveChangesAsync(); // Saves changes to the database
        }

        // GetPolls method to retrieve all polls from the database
        public async Task<List<Poll>> GetPolls()
        {
            return await _context.Polls
                .OrderByDescending(p => p.DateCreated) // Sorts polls by date (most recent first)
                .ToListAsync();
        }

        public async Task<Poll?> GetPollById(int id)
        {
            return await _context.Polls.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> Vote(int pollId, int optionNumber)
        {
            var poll = await _context.Polls.FirstOrDefaultAsync(p => p.Id == pollId);

            if (poll == null) return false;

            switch (optionNumber)
            {
                case 1:
                    poll.Option1VotesCount++;
                    break;
                case 2:
                    poll.Option2VotesCount++;
                    break;
                case 3:
                    poll.Option3VotesCount++;
                    break;
                default:
                    return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
