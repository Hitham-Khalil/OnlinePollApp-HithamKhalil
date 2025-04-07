﻿using Microsoft.EntityFrameworkCore;
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
    }
}
