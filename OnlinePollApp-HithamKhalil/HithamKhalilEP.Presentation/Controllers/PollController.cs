using Microsoft.AspNetCore.Mvc;
using YourNameEP.DataAccess.Repositories;
using YourNameEP.Domain.Models;
using System.Threading.Tasks;
using HithamKhalilEP.Domain.Interfaces;

namespace YourNameEP.Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;

        // Constructor injection for PollRepository
        public PollController(IPollRepository pollRepository)
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

        // GET: Poll/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var poll = await _pollRepository.GetPollById(id); // Async call to fetch poll by ID

            if (poll == null)
                return NotFound(); // Return 404 if the poll is not found

            return View(poll); // Pass the poll data to the view
        }

        // GET: Poll/Vote/5
        [HttpGet]
        public async Task<IActionResult> Vote(int id)
        {
            var poll = await _pollRepository.GetPollById(id);
            if (poll == null)
                return NotFound();

            return View(poll);
        }

        // POST: Poll/Vote
        [HttpPost]
        public async Task<IActionResult> Vote(int pollId, int selectedOption)
        {
            var success = await _pollRepository.Vote(pollId, selectedOption);

            if (!success)
                return BadRequest("Unable to vote.");

            return RedirectToAction("Index");
        }
    }
}
