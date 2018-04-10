using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VTutor.Model;


namespace VTutor.Web.Controllers
{
  [Produces("application/json")]
  [Route("api/Subjects")]
  public class SubjectsController : Controller
  {
    private readonly VTutorContext _context;

    public SubjectsController(VTutorContext context)
    {
      _context = context;
    }

    // GET: api/Subjects
    [HttpGet]
    public IEnumerable<Subject> GetSubject()
    {
      return _context.Subjects;
    }

    // GET: api/Subjects/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubject([FromRoute] Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var subject = await _context.Subjects.SingleOrDefaultAsync(m => m.Id == id);

      if (subject == null)
      {
        return NotFound();
      }

      return Ok(subject);
    }

    // PUT: api/Subjects/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubject([FromRoute] Guid id, [FromBody] Subject subject)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != subject.Id)
      {
        return BadRequest();
      }

      _context.Entry(subject).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!SubjectExists(id))
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

    // POST: api/Subjects
    [HttpPost]
    public async Task<IActionResult> PostSubject([FromBody] Subject subject)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Subjects.Add(subject);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetSubject", new { id = subject.Id }, subject);
    }

    // DELETE: api/Subjects/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject([FromRoute] Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var subject = await _context.Subjects.SingleOrDefaultAsync(m => m.Id == id);
      if (subject == null)
      {
        return NotFound();
      }

      _context.Subjects.Remove(subject);
      await _context.SaveChangesAsync();

      return Ok(subject);
    }

    private bool SubjectExists(Guid id)
    {
      return _context.Subjects.Any(e => e.Id == id);
    }
  }
}
