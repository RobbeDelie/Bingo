using Bingo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bingo.Repositories.Interfaces
{
    public interface IBingoNumberRepository
    {
        List<BingoNumber> GetAll();
        List<int> GetUsedNumbers();
        Task SetNumberToUsed(int number);
        Task ResetUsedNumbers();
    }
}
