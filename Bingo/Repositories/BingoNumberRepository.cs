using Bingo.Domain;
using Bingo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bingo.Repositories
{
    public class BingoNumberRepository : IBingoNumberRepository
    {
        private BingoDbContext _context;

        public BingoNumberRepository(BingoDbContext bingoDbContext)
        {
            _context = bingoDbContext;
        }

        public List<BingoNumber> GetAll()
        {
            var numbers = _context.BingoNumbers.ToList();
            return numbers;
        }
        public List<int> GetUsedNumbers()
        {
            var numbers = _context.BingoNumbers.Where(x => x.Used == true).Select(x => x.Value).ToList();
            return numbers;
        }
        public async Task SetNumberToUsed(int number)
        {
            var bingoNumber = _context.BingoNumbers.Where(x => x.Value == number).FirstOrDefault();
            bingoNumber.Used = true;
            await _context.SaveChangesAsync();
        }
        public async Task ResetUsedNumbers()
        {
            var numbers = _context.BingoNumbers.ToList();
            numbers.ForEach(x => x.Used = false);
            await _context.SaveChangesAsync();
        }
    }
}
