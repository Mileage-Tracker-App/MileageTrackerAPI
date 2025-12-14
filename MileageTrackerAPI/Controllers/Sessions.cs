using MileageTrackerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MileageTrackerAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MileageTrackerAPI.Controllers
{
    [Route("api/sessions")]
    [ApiController]
    public class Sessions : ControllerBase
    {
        private readonly AppDbContext _db;

        public Sessions(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<Microsoft.AspNetCore.Http.IResult> GetAllSessions()
        {
            var sessions = await _db.Sessions
                .Include(s => s.Logs)
                .ThenInclude(l => l.LogItems)
                .ToListAsync();
            return Results.Ok(sessions);
        }

        [HttpGet("{sessionId}")]
        public async Task<Microsoft.AspNetCore.Http.IResult> GetSession([FromRoute] int sessionId)
        {
            var session = await _db.Sessions
                .Include(s => s.Logs)
                .ThenInclude(l => l.LogItems)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
            {
                return Results.NotFound("Session not found");
            }

            return Results.Ok(session);
        }

        [HttpPost]
        public async Task<Microsoft.AspNetCore.Http.IResult> CreateSession()
        {
            var newSession = new Session();
            await _db.Sessions.AddAsync(newSession);
            await _db.SaveChangesAsync();
            return Results.Created($"/api/sessions/{newSession.Id}", newSession);
        }

        [HttpDelete("{sessionId}")]
        public async Task<Microsoft.AspNetCore.Http.IResult> DeleteSession([FromRoute] int sessionId)
        {
            var existingSession = await _db.Sessions.FindAsync(sessionId);
            if (existingSession == null)
            {
                return Results.NotFound("Session not found");
            }

            _db.Sessions.Remove(existingSession);
            await _db.SaveChangesAsync();
            return Results.Ok("Session deleted successfully");
        }


    }
}