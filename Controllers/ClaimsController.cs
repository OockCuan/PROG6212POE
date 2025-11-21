using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROGPOEst10439216.Data;

namespace PROGPOEst10439216.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ClaimsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                return Unauthorized();
            }

            var lecturerName = $"{profile.Name} {profile.Surname}";

            var claims = await _context.Claims
                .Where(c => c.Lecturer == lecturerName)
                .OrderByDescending(c => c.ClaimId)
                .ToListAsync();

            return View(claims);
        }


        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new Models.Claims();
            var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
                if (profile != null)
                {
                    model.Rate = profile.DefaultRatePerJob;
                    model.Lecturer = $"{profile.Name} {profile.Surname}";
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Claims claim, IFormFile uploadFile)
        {
            var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
                if (profile != null)
                {
                    claim.Rate = profile.DefaultRatePerJob;
                    claim.Lecturer ??= $"{profile.Name} {profile.Surname}";
                }
            }

            if (claim.Hours.HasValue && claim.Hours.Value > 80.0)
            {
                ModelState.AddModelError(nameof(claim.Hours), "Hours cannot exceed 80");
            }

            if (uploadFile != null && uploadFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);
                var fileName = Guid.NewGuid() + Path.GetExtension(uploadFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(stream);
                }
                claim.FileName = uploadFile.FileName;
                claim.FilePath = "/uploads/" + fileName;
            }

            claim.Status = "Pending";
            claim.Documentation = claim.FileName;

            if (!ModelState.IsValid)
                return View(claim);

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
