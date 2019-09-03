using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bingo.Repositories.Interfaces
{
    public interface INumberRepository
    {
        List<int> GetAll();
        Task SetNumber(int newNumber, int index = 1);
    }
}
