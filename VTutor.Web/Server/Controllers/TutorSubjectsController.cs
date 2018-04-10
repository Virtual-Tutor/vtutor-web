//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using VTutor.Model;
//using VTutor.Web.Server.Data;

//namespace VTutor.Web.Controllers
//{
//  [Produces("application/json")]
//  [Route("api/TutorSubjects")]
//  public class TutorSubjectsController : Controller
//  {
//    private readonly VTutorWebContext _context;

//    public TutorSubjectsController(VTutorWebContext context)
//    {
//      _context = context;
//    }

//    // GET: api/TutorSubjects
//    [HttpGet]
//    public IEnumerable<TutorSubject> GetTutorSubject()
//    {
//      return _context.TutorSubjects;
//    }

//    // GET: api/TutorSubjects/5
//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetTutorSubject([FromRoute] Guid id)
//    {
//      if (!ModelState.IsValid)
//      {
//        return BadRequest(ModelState);
//      }

//      var tutorSubject = await _context.TutorSubjects.SingleOrDefaultAsync(m => m.Id == id);

//      if (tutorSubject == null)
//      {
//        return NotFound();
//      }

//      return Ok(tutorSubject);
//    }

//    // PUT: api/TutorSubjects/5
//    [HttpPut("{id}")]
//    public async Task<IActionResult> PutTutorSubject([FromRoute] Guid id, [FromBody] TutorSubject tutorSubject)
//    {
//      if (!ModelState.IsValid)
//      {
//        return BadRequest(ModelState);
//      }

//      if (id != tutorSubject.Id)
//      {
//        return BadRequest();
//      }

//      _context.Entry(tutorSubject).State = EntityState.Modified;

//      try
//      {
//        await _context.SaveChangesAsync();
//      }
//      catch (DbUpdateConcurrencyException)
//      {
//        if (!TutorSubjectExists(id))
//        {
//          return NotFound();
//        }
//        else
//        {
//          throw;
//        }
//      }

//      return NoContent();
//    }

//    // POST: api/TutorSubjects
//    [HttpPost]
//    public async Task<IActionResult> PostTutorSubject([FromBody] TutorSubject tutorSubject)
//    {
//      if (!ModelState.IsValid)
//      {
//        return BadRequest(ModelState);
//      }

//      _context.TutorSubjects.Add(tutorSubject);
//      await _context.SaveChangesAsync();

//      return CreatedAtAction("GetTutorSubject", new { id = tutorSubject.Id }, tutorSubject);
//    }

//    // DELETE: api/TutorSubjects/5
//    [HttpDelete("{id}")]
//    public async Task<IActionResult> DeleteTutorSubject([FromRoute] Guid id)
//    {
//      if (!ModelState.IsValid)
//      {
//        return BadRequest(ModelState);
//      }

//      var tutorSubject = await _context.TutorSubjects.SingleOrDefaultAsync(m => m.Id == id);
//      if (tutorSubject == null)
//      {
//        return NotFound();
//      }

//      _context.TutorSubjects.Remove(tutorSubject);
//      await _context.SaveChangesAsync();

//      return Ok(tutorSubject);
//    }

//    private bool TutorSubjectExists(Guid id)
//    {
//      return _context.TutorSubjects.Any(e => e.Id == id);
//    }
//  }
//}
