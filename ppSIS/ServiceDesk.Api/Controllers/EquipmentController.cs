using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;     // ← ОБЯЗАТЕЛЬНО
using ServiceDesk.Api.Data;
using ServiceDesk.Api.Models;

namespace ServiceDesk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EquipmentController(AppDbContext context)
        {
            _context = context;
        }

       [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetAll()
        {
            Console.WriteLine(">>> API VERSION 2: GetAll() called");
            return await _context.Equipment.ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Equipment>> Get(int id)
        {
            var item = await _context.Equipment.FindAsync(id);
            return item == null ? NotFound() : item;
        }


        [HttpPost]
        public async Task<ActionResult<Equipment>> Create(Equipment model)
        {
            _context.Equipment.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Equipment model)
        {
            if (id != model.Id)
                return BadRequest();

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Equipment.FindAsync(id);
            if (item == null) return NotFound();

            _context.Equipment.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
