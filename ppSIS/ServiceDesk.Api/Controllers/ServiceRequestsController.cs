using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Api.Data;
using ServiceDesk.Api.Models;

namespace ServiceDesk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServiceRequestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/servicerequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetAll()
        {
            var items = await _context.ServiceRequests
                .Include(r => r.Equipment) // при желании подгружаем оборудование
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/servicerequests/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceRequest>> GetById(int id)
        {
            var item = await _context.ServiceRequests
                .Include(r => r.Equipment)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST: api/servicerequests
        [HttpPost]
        public async Task<ActionResult<ServiceRequest>> Create(ServiceRequest model)
        {
            model.Id = 0;                   // на всякий случай
            model.CreatedAt = DateTime.UtcNow;
            if (model.Status == null || model.Status == "")
                model.Status = "Новая";

            _context.ServiceRequests.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // PUT: api/servicerequests/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ServiceRequest model)
        {
            if (id != model.Id)
                return BadRequest("Id в пути и в теле запроса не совпадают.");

            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/servicerequests/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.ServiceRequests.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.ServiceRequests.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Дополнительно: получить только открытые заявки
        // GET: api/servicerequests/open
        [HttpGet("open")]
        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetOpen()
        {
            var items = await _context.ServiceRequests
                .Where(r => r.Status != "Выполнена" && r.Status != "Отменена")
                .ToListAsync();

            return Ok(items);
        }
    }
}
