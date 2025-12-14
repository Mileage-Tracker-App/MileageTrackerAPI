using MileageTrackerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MileageTrackerAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MileageTrackerAPI.Controllers
{
    [Route("api/sessions/{sessionId}/logs")]
    [ApiController]
    public class Logs : ControllerBase
    {
        private readonly AppDbContext _db;

        public Logs(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<Microsoft.AspNetCore.Http.IResult> GetLogs([FromRoute] int sessionId)
        {
            var session = await _db.Sessions.FindAsync(sessionId);
            if (session == null)
            {
                return Results.NotFound("Session not found");
            }

            var logs = await _db.Logs
                .Include(l => l.LogItems)
                .Where(l => l.SessionId == sessionId)
                .ToListAsync();

            return Results.Ok(logs);
        }

        [HttpGet("{logId}")]
        public async Task<Microsoft.AspNetCore.Http.IResult> GetLog([FromRoute] int sessionId, [FromRoute] int logId)
        {
            var log = await _db.Logs
                .Include(l => l.LogItems)
                .FirstOrDefaultAsync(l => l.Id == logId && l.SessionId == sessionId);

            if (log == null)
            {
                return Results.NotFound("Log not found");
            }

            return Results.Ok(log);
        }

        [HttpPost]
        public async Task<Microsoft.AspNetCore.Http.IResult> CreateLog([FromRoute] int sessionId, [FromBody] Log log)
        {
            var session = await _db.Sessions.FindAsync(sessionId);
            if (session == null)
            {
                return Results.NotFound("Session not found");
            }

            log.SessionId = sessionId;
            await _db.Logs.AddAsync(log);
            await _db.SaveChangesAsync();
            return Results.Created($"/api/sessions/{sessionId}/logs/{log.Id}", log);
        }

        [HttpPut("{logId}")]
        public async Task<Microsoft.AspNetCore.Http.IResult> UpdateLog([FromRoute] int sessionId, [FromRoute] int logId, [FromBody] Log log)
        {
            var existingLog = await _db.Logs
                .FirstOrDefaultAsync(l => l.Id == logId && l.SessionId == sessionId);

            if (existingLog == null)
            {
                return Results.NotFound("Log not found");
            }

            existingLog.Vehicle = log.Vehicle;
            existingLog.StartDate = log.StartDate;
            existingLog.Description = log.Description;

            await _db.SaveChangesAsync();
            return Results.Ok(existingLog);
        }

        [HttpDelete("{logId}")]
        public async Task<Microsoft.AspNetCore.Http.IResult> DeleteLog([FromRoute] int sessionId, [FromRoute] int logId)
        {
            var existingLog = await _db.Logs
                .FirstOrDefaultAsync(l => l.Id == logId && l.SessionId == sessionId);

            if (existingLog == null)
            {
                return Results.NotFound("Log not found");
            }

            _db.Logs.Remove(existingLog);
            await _db.SaveChangesAsync();
            return Results.Ok("Log deleted successfully");
        }
    }
}