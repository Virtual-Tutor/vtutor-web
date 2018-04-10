using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VTutor.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using VTutor.Web.Server.Models;

namespace VTutor.Web.Controllers
{
	[Produces("application/json")]
	[Route("api/Tutors")]
	[System.Web.Http.Authorize(Roles = "Admin")]
	public class TutorsController : Controller
	{
		private readonly VTutorContext _context;
		/// <summary>
		/// User manager - attached to application DB context
		/// </summary>
		protected UserManager<ApplicationUser> UserManager { get; set; }

		public TutorsController(VTutorContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			this.UserManager = userManager;
		}

		// GET: api/Tutors
		[HttpGet]
		[System.Web.Http.Authorize(Roles = "Tutors")]
		public async Task<IActionResult> GetTutor(bool currentTutor = false)
		{
			var user = await this.UserManager.GetUserAsync(this.User);

			if (user == null && currentTutor)
			{
				return BadRequest("Cannot get current tutor profile profile if your not logged in as a tutor.");
			}
			else if (currentTutor)
			{
				var tutor = _context.Tutors.Where(t => t.Email == user.Email).FirstOrDefault();
				tutor.User = null;

				return Ok(tutor);
			}
			else
			{
				return Ok(_context.Tutors);
			}
		}

		// GET: api/Tutors/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetTutor([FromRoute] Guid id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var tutor = await _context.Tutors.SingleOrDefaultAsync(m => m.Id == id);

			if (tutor == null)
			{
				return NotFound();
			}

			return Ok(tutor);
		}

		// PUT: api/Tutors/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTutor([FromRoute] Guid id, [FromBody] Tutor tutor)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != tutor.Id)
			{
				return BadRequest();
			}

			_context.Entry(tutor).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TutorExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		[HttpPost("{id}/certification")]
		[AllowAnonymous]
		public async Task<IActionResult> PostCertification(Guid id, [FromBody] FileModel file)
		{

			var tutor = await _context.Tutors.Where(t => t.Id == id)
				.Include(t => t.Documents)
				.FirstOrDefaultAsync();

			string preBase64 = file.File.Substring(0, file.File.IndexOf(','));
			string base64 = file.File.Substring(file.File.IndexOf(',') + 1);

			byte[] bytes = Convert.FromBase64String(base64);

			tutor.Documents.Add(new Image() { Id = new Guid(), ImageData = bytes });
			await _context.SaveChangesAsync();

			return Ok();
		}

		[HttpPost("{id}/profileImage")]
		[AllowAnonymous]
		public async Task<IActionResult> PostProfileImage(Guid id, [FromBody] FileModel file)
		{

			var tutor = await _context.Tutors.Where(t => t.Id == id)
				.Include(t => t.Documents)
				.FirstOrDefaultAsync();

			string preBase64 = file.File.Substring(0, file.File.IndexOf(','));
			string base64 = file.File.Substring(file.File.IndexOf(',') + 1);

			byte[] bytes = Convert.FromBase64String(base64);

			tutor.TutorImage = new Image() { Id = new Guid(), ImageData = bytes };
			await _context.SaveChangesAsync();

			return Ok();
		}

		// POST: api/Tutors
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> PostTutor([FromBody] Tutor tutor)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			//await VTutor.Email.EmailClient.se("Isaac@knowtro.com", "Test Subject", "Test Body");

			_context.Tutors.Add(tutor);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetTutor", new { id = tutor.Id }, tutor);
		}

		// DELETE: api/Tutors/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTutor([FromRoute] Guid id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var tutor = await _context.Tutors.SingleOrDefaultAsync(m => m.Id == id);
			if (tutor == null)
			{
				return NotFound();
			}

			_context.Tutors.Remove(tutor);
			await _context.SaveChangesAsync();

			return Ok(tutor);
		}

		private bool TutorExists(Guid id)
		{
			return _context.Tutors.Any(e => e.Id == id);
		}
	}
}
