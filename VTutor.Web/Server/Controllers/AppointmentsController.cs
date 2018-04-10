using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using VTutor.Model;
using Microsoft.AspNetCore.Identity;

namespace VTutor.Web.Controllers
{
	[Produces("application/json")]
	[Route("api/Appointments")]
	public class AppointmentsController : Controller
	{
		/// <summary>
		/// User manager - attached to application DB context
		/// </summary>
		protected UserManager<ApplicationUser> UserManager { get; set; }
		private readonly VTutorContext _context;

		public AppointmentsController(VTutorContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			this.UserManager = userManager;
		}

		// GET: api/Appointments
		[HttpGet]
		public async Task<IActionResult> GetAppointments()
		{
			var user = await this.UserManager.GetUserAsync(this.User);

			if (user == null)
			{
				return StatusCode(401);
			}

			var student = _context.Students.Include(s => s.Appointments).Where(s => s.Email == user.Email).FirstOrDefault();

			if (student == null)
			{
				return BadRequest("Tutors are not allowed to request sessions.");
			}
			student.Appointments.ForEach(a => a.Student = null);
			return Ok(student.Appointments);
		}

		// GET: api/Appointments/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetAppointment([FromRoute] Guid id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var appointment = await _context.Appointments.SingleOrDefaultAsync(m => m.Id == id);

			if (appointment == null)
			{
				return NotFound();
			}

			return Ok(appointment);
		}

		// PUT: api/Appointments/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAppointment([FromRoute] Guid id, [FromBody] Appointment appointment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != appointment.Id)
			{
				return BadRequest();
			}

			_context.Entry(appointment).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AppointmentExists(id))
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

		// POST: api/Appointments
		[HttpPost]
		public async Task<IActionResult> PostAppointment([FromBody] Appointment appointment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.Appointments.Add(appointment);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
		}

		// DELETE: api/Appointments/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAppointment([FromRoute] Guid id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var appointment = await _context.Appointments.SingleOrDefaultAsync(m => m.Id == id);
			if (appointment == null)
			{
				return NotFound();
			}

			_context.Appointments.Remove(appointment);
			await _context.SaveChangesAsync();

			return Ok(appointment);
		}

		private bool AppointmentExists(Guid id)
		{
			return _context.Appointments.Any(e => e.Id == id);
		}
	}
}
