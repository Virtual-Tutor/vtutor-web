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
  [Route("api/Grades")]
  public class GradesController : Controller
  {
    private readonly VTutorContext _context;

    public GradesController(VTutorContext context)
    {
      _context = context;
    }

    // GET: api/Grades
    [HttpGet]
    public IEnumerable<Grade> GetGrade()
    {
      return _context.Grades;
    }

    // GET: api/Grades/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGrade([FromRoute] Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var grade = await _context.Grades.SingleOrDefaultAsync(m => m.Id == id);

      if (grade == null)
      {
        return NotFound();
      }

      return Ok(grade);
    }

    // PUT: api/Grades/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGrade([FromRoute] Guid id, [FromBody] Grade grade)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != grade.Id)
      {
        return BadRequest();
      }

      _context.Entry(grade).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!GradeExists(id))
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

    // POST: api/Grades
    [HttpPost]
    public async Task<IActionResult> PostGrade([FromBody] Grade grade)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Grades.Add(grade);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetGrade", new { id = grade.Id }, grade);
    }

    // DELETE: api/Grades/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGrade([FromRoute] Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var grade = await _context.Grades.SingleOrDefaultAsync(m => m.Id == id);
      if (grade == null)
      {
        return NotFound();
      }

      _context.Grades.Remove(grade);
      await _context.SaveChangesAsync();

      return Ok(grade);
    }

    private bool GradeExists(Guid id)
    {
      return _context.Grades.Any(e => e.Id == id);
    }
  }
}
