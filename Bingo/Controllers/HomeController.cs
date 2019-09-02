using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bingo.Models;
using Bingo.Repositories.Interfaces;
using Microsoft.AspNetCore.WebSockets.Internal;

namespace Bingo.Controllers
{
    public class HomeController : Controller
    {
        private INumberRepository _numberRepository;
        private IBingoNumberRepository _bingoNumberRepository;

        public HomeController(INumberRepository numberRepository, IBingoNumberRepository bingoNumberRepository)
        {
            _numberRepository = numberRepository;
            _bingoNumberRepository = bingoNumberRepository;
        }
        public IActionResult Index()
        {
            var numbers = _numberRepository.GetAll();
            var model = new NumberModel { Numbers = numbers };
            return View(model);
        }

        public IActionResult Admin()
        {
            var numbers = _bingoNumberRepository.GetAll();
            var models = new List<BingoNumberListItem>();
            foreach (var number in numbers)
            {
                var model = new BingoNumberListItem
                {
                    Used = number.Used,
                    Value = number.Value
                };
                models.Add(model);
            }
            return View(models);
        }

        public async Task<IActionResult> NewNumber()
        {
            var usedNumbers = _bingoNumberRepository.GetUsedNumbers();
            for (int i = 1; i < Constants.TotalBingoNumbers+1; i++)
            {
                var range = Enumerable.Range(1, Constants.MaxBingoNumbers).Where(x => !usedNumbers.Contains(x));
                var random = new Random();
                int index = random.Next(0, Constants.MaxBingoNumbers - usedNumbers.Count);
                var newNumber = range.ElementAt(index);

                await _bingoNumberRepository.SetNumberToUsed(newNumber);

                await _numberRepository.SetNumber(newNumber, i);

                usedNumbers.Add(newNumber);
            }

            return RedirectToAction("Admin");
        }

        public IActionResult Reset()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetNumbers()
        {
            await _bingoNumberRepository.ResetUsedNumbers();
            return RedirectToAction("Admin");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
