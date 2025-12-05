using MileageTrackerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MileageTrackerAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MileageTrackerAPI.Controllers
{
    [Route("/managesessions")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SessionController(AppDbContext db)
        {
            _db = db;
        }
       
        [HttpGet("GetAllLogs")]
        public async Task<Microsoft.AspNetCore.Http.IResult> GetAllLogs()
        {
            var logs = await _db.Logs.ToListAsync();
            return Results.Ok(logs);
        }
        
    }
}