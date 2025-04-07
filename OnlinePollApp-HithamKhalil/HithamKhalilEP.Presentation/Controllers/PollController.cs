using Microsoft.AspNetCore.Mvc;
using YourNameEP.DataAccess.Repositories;
using YourNameEP.Domain.Models;
using System.Threading.Tasks;

namespace YourNameEP.Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly PollRepository _pollRepository;

        // Constructor injection for PollRepository
        public PollController(PollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        // GET: Poll/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Poll/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Poll poll)
        {
            if (ModelState.IsValid)
            {
                await _pollRepository.CreatePoll(poll); // Save poll to database
                return RedirectToAction(nameof(Index)); // Redirect to the list of polls after creation
            }
            return View(poll);
        }

        // GET: Poll/Index
        public async Task<IActionResult> Index()
        {
            var polls = await _pollRepository.GetPolls();
            return View(polls);
        }
    }
}
