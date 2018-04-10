using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VTutor.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace VTutor.Web.Controllers
{
	[Produces("application/json")]
	[Route("api/ScheduleBlocks")]
	public class ScheduleBlocksController : Controller
	{
		private readonly VTutorContext _context;

		/// <summary>
		/// User manager - attached to application DB context
		/// </summary>
		protected UserManager<ApplicationUser> UserManager { get; set; }

		public ScheduleBlocksController(VTutorContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			this.UserManager = userManager;
		}

		// GET: api/ScheduleBlocks
		[HttpGet]
		public async Task<IEnumerable<ScheduleBlock>> GetScheduleBlock(DateTime? date = null, bool for_tutor = false)
		{
			var user = await this.UserManager.GetUserAsync(this.User);
			Tutor tutor = null;
			if (user == null)
			{
				for_tutor = false;
			}
			else
			{
				tutor = _context.Tutors.Where(t => t.Email == user.Email).FirstOrDefault();
			}

			var blocks = _context.ScheduleBlocks
				.Include(s => s.Tutor.Subjects)
					.ThenInclude(s => s.SubjectGrade)
				.Where(b => date == null || b.StartTime.Date == date.Value.Date)
				.Where(b => for_tutor == false || b.Tutor.Id == tutor.Id);


			//nulling out the availability schedule because of self referential issues.
			foreach(ScheduleBlock block in blocks)
			{
				if (block.Tutor != null)
				{
					//For some reason sql isnt storing the timezone... all dates are stored in UTC
					block.StartTime = block.StartTime.ToLocalTime();
					block.EndTime = block.EndTime.ToLocalTime();

					block.Tutor.AvailabilitySchedule = null;
					block.Tutor.User = null;
				}
			}

			return blocks;
		}

		// GET: api/ScheduleBlocks/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetScheduleBlock([FromRoute] Guid id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var scheduleBlock = await _context.ScheduleBlocks.SingleOrDefaultAsync(m => m.Id == id);
			//scheduleBlock.StartTime.Kind = DateTimeKind.Utc;
			if (scheduleBlock == null)
			{
				return NotFound();
			}

			return Ok(scheduleBlock);
		}

		// PUT: api/ScheduleBlocks/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutScheduleBlock([FromRoute] Guid id, [FromBody] ScheduleBlock scheduleBlock)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != scheduleBlock.Id)
			{
				return BadRequest();
			}

			_context.Entry(scheduleBlock).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ScheduleBlockExists(id))
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

		// POST: api/ScheduleBlocks
		[HttpPost]
		public async Task<IActionResult> PostScheduleBlock([FromBody] ScheduleBlock scheduleBlock)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = await this.UserManager.GetUserAsync(this.User);

			var tutor = _context.Tutors.Where(t => t.Email == user.Email).FirstOrDefault();

			if (tutor == null)
			{
				return BadRequest("non-tutor users arent allowed to create schedule blocks.");
			}

			scheduleBlock.StartTime = scheduleBlock.StartTime.ToUniversalTime();
			scheduleBlock.EndTime = scheduleBlock.EndTime.ToUniversalTime();

			scheduleBlock.Tutor = tutor;

			_context.ScheduleBlocks.Add(scheduleBlock);
			await _context.SaveChangesAsync();

			//null the tutor out for the return...
			scheduleBlock.Tutor = null;

			return CreatedAtAction("GetScheduleBlock", new { id = scheduleBlock.Id }, scheduleBlock);
		}

		// DELETE: api/ScheduleBlocks/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteScheduleBlock([FromRoute] Guid id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var scheduleBlock = await _context.ScheduleBlocks.SingleOrDefaultAsync(m => m.Id == id);
			if (scheduleBlock == null)
			{
				return NotFound();
			}

			_context.ScheduleBlocks.Remove(scheduleBlock);
			await _context.SaveChangesAsync();

			return Ok(scheduleBlock);
		}

		private bool ScheduleBlockExists(Guid id)
		{
			return _context.ScheduleBlocks.Any(e => e.Id == id);
		}
	}
}
