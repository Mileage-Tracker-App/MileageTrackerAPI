using MileageTrackerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MileageTrackerAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MileageTrackerAPI.Controllers
{
    [Route("/log")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly AppDbContext _db;

        public LogController(AppDbContext db)
        {
            _db = db;
        }
       
        
        
    }
}