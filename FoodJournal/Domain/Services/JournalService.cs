using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodJournal.Domain.Entities;
using FoodJournal.Domain.Interfaces;

namespace FoodJournal.Domain.Services
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
