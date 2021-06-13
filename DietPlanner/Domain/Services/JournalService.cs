using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DietPlanner.Domain.Entities;
using DietPlanner.Domain.Interfaces;

namespace DietPlanner.Domain.Services
{
    public class JournalService : IJournalService
    {
        private readonly IDishRepository _dishRepository;
        public JournalService(IDishRepository repository)
        {
            _dishRepository = repository ?? throw new ArgumentNullException(nameof(repository));

        }

        public async Task AddEntry(JournalEntry journalEntry)
        {
             await _dishRepository.AddEntry(journalEntry);
        }
    }
}
