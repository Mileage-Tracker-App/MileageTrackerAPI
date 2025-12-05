using MileageTrackerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MileageTrackerAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MileageTrackerAPI.Controllers
{
    [Route("api/sessions/{sessionId}/logs/{logId}/items")]
    [ApiController]
    public class LogItems : ControllerBase
    {
        private readonly AppDbContext _db;

        public LogItems(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<Microsoft.AspNetCore.Http.IResult> GetLogItems([FromRoute] int sessionId, [FromRoute] int logId)
        {
            var log = await _db.Logs
                .FirstOrDefaultAsync(l => l.Id == logId && l.SessionId == sessionId);

            if (log == null)
            {
                return Results.NotFound("Log not found");
            }

            var logItems = await _db.LogItems
                .Where(li => li.LogId == logId)
                .ToListAsync();

            return Results.Ok(logItems);
        }

        [HttpGet("{itemId}")]
        public async Task<Microsoft.AspNetCore.Http.IResult> GetLogItem([FromRoute] int sessionId, [FromRoute] int logId, [FromRoute] int itemId)
        {
            var log = await _db.Logs
                .FirstOrDefaultAsync(l => l.Id == logId && l.SessionId == sessionId);

            if (log == null)
            {
                return Results.NotFound("Log not found");
            }

            var logItem = await _db.LogItems
                .FirstOrDefaultAsync(li => li.Id == itemId && li.LogId == logId);

            if (logItem == null)
            {
                return Results.NotFound("Log item not found");
            }

            return Results.Ok(logItem);
        }

        [HttpPost]
        public async Task<Microsoft.AspNetCore.Http.IResult> CreateLogItem([FromRoute] int sessionId, [FromRoute] int logId, [FromBody] LogItem logItem)
        {
            var log = await _db.Logs
                .FirstOrDefaultAsync(l => l.Id == logId && l.SessionId == sessionId);

            if (log == null)
            {
                return Results.NotFound("Log not found");
            }

            logItem.LogId = logId;
            await _db.LogItems.AddAsync(logItem);
            await _db.SaveChangesAsync();
            return Results.Created($"/api/sessions/{sessionId}/logs/{logId}/items/{logItem.Id}", logItem);
        }

        [HttpPut("{itemId}")]
        public async Task<Microsoft.AspNetCore.Http.IResult> UpdateLogItem([FromRoute] int sessionId, [FromRoute] int logId, [FromRoute] int itemId, [FromBody] LogItem logItem)
        {
            var log = await _db.Logs
                .FirstOrDefaultAsync(l => l.Id == logId && l.SessionId == sessionId);

            if (log == null)
            {
                return Results.NotFound("Log not found");
            }

            var existingLogItem = await _db.LogItems
                .FirstOrDefaultAsync(li => li.Id == itemId && li.LogId == logId);

            if (existingLogItem == null)
            {
                return Results.NotFound("Log item not found");
            }

            existingLogItem.Date = logItem.Date;
            existingLogItem.Miles = logItem.Miles;
            existingLogItem.Description = logItem.Description;
            existingLogItem.IsGas = logItem.IsGas;
            existingLogItem.Gallons = logItem.Gallons;
            existingLogItem.PricePerGallon = logItem.PricePerGallon;

            await _db.SaveChangesAsync();
            return Results.Ok(existingLogItem);
        }

        [HttpDelete("{itemId}")]
        public async Task<Microsoft.AspNetCore.Http.IResult> DeleteLogItem([FromRoute] int sessionId, [FromRoute] int logId, [FromRoute] int itemId)
        {
            var log = await _db.Logs
                .FirstOrDefaultAsync(l => l.Id == logId && l.SessionId == sessionId);

            if (log == null)
            {
                return Results.NotFound("Log not found");
            }

            var existingLogItem = await _db.LogItems
                .FirstOrDefaultAsync(li => li.Id == itemId && li.LogId == logId);

            if (existingLogItem == null)
            {
                return Results.NotFound("Log item not found");
            }

            _db.LogItems.Remove(existingLogItem);
            await _db.SaveChangesAsync();
            return Results.Ok("Log item deleted");
        }
    }
}