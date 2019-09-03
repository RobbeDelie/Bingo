using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bingo.Domain
{
    public class Number
    {
        [Key]
        public int Id { get; set; }
        public int Value { get; set; }
    }
}
