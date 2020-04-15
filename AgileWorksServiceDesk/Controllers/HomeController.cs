using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AgileWorksServiceDesk.Models;
using AgileWorksServiceDesk.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace AgileWorksServiceDesk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRequestService _service;

        public HomeController(ILogger<HomeController> logger, IRequestService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _service.GetAllActiveRequests();
            return View(requests);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Requests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,DueDateTime")] RequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(requestDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(requestDTO);
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [ExcludeFromCodeCoverage]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var request = await _service.GetByIdAsync(id.Value);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Description,DueDateTime,Completed")] RequestDTO requestDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(requestDTO);
            }

            await _service.UpdateAsync(requestDTO);
            return RedirectToAction(nameof(Index));
        }
    }
}
