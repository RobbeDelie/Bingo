using Bingo.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bingo
{
    public class BingoDbContext : DbContext
    {
        public BingoDbContext(DbContextOptions<BingoDbContext> options)
            : base(options)
        {
        }
        public DbSet<Number> Numbers { get; set; }
        public DbSet<BingoNumber> BingoNumbers { get; set; }
    }
}
