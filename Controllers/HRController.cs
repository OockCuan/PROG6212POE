using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROGPOEst10439216.Data;
using PROGPOEst10439216.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PROGPOEst10439216.Controllers
{
    public class HRController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HRController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Profile deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Surname,Department,DefaultRatePerJob,RoleName")] Profiles profile)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }

            await _context.AddAsync(profile);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Profile created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var profiles = await _context.Profiles.ToListAsync();
            return View(profiles);
        }
        public async Task<IActionResult> ApprovedClaims()
        {
            var claims = await _context.Claims.ToListAsync();
            return View(claims);
        }

        [HttpGet]
        public async Task<IActionResult> Export()
        {
            var claims = await _context.Claims
                .Where(c => c.Status == "Accepted")
                .OrderBy(c => c.ClaimId)
                .ToListAsync();

            var lines = new List<string>();
            lines.Add("ClaimId,Lecturer,Hours,Rate,OvertimeDue,Status");

            foreach (var c in claims)
            {
                decimal overtimeDue = 0;
                if (c.Hours.HasValue && c.Rate.HasValue)
                    overtimeDue = (decimal)c.Hours.Value * c.Rate.Value;

                var line =
                    $"{c.ClaimId}," +
                    $"{c.Lecturer}," +
                    $"{c.Hours}," +
                    $"{c.Rate}," +
                    $"{overtimeDue}," +
                    $"{c.Status}";

                lines.Add(line);
            }

            var csv = string.Join("\n", lines);
            var bytes = System.Text.Encoding.UTF8.GetBytes(csv);

            return File(bytes, "text/csv", "accepted_claims.csv");
        }





    }

}

