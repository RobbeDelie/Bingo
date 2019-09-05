using Bingo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bingo.Repositories
{
    public class NumberRepository : INumberRepository
    {
        private BingoDbContext _context;

        public NumberRepository(BingoDbContext bingoDbContext)
        {
            _context = bingoDbContext;
        }

        public List<int> GetAll()
        {
            var number = _context.Numbers.Where(x => x.Value != 0).Select(x => x.Value).ToList();
            //var number = new List<int> { 13, 22, 39, 40 };
            return number;
        }
        public async Task SetNumber(int newNumber, int index = 1)
        {
            var number = _context.Numbers.Where(x => x.Id == index).FirstOrDefault();
            number.Value = newNumber;
            await _context.SaveChangesAsync();
        }
    }
}
