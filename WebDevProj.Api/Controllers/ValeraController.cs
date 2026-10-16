using Microsoft.AspNetCore.Mvc;
using WebDevProj.Api.Models;
using WebDevProj.Api.Services;

namespace WebDevProj.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValeraController : ControllerBase
{
    private readonly IValeraService _service;

    public ValeraController(IValeraService service)
    {
        _service = service;
    }

    // GET /api/valera
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Valera>>> GetAll()
    {
        var valeras = await _service.GetAllAsync();
        return Ok(valeras);
    }

    // GET /api/valera/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Valera>> GetById(int id)
    {
        var valera = await _service.GetByIdAsync(id);
        if (valera == null)
            return NotFound();
        return Ok(valera);
    }

    // POST /api/valera
    [HttpPost]
    public async Task<ActionResult<Valera>> Create([FromBody] Valera valera)
    {
        var created = await _service.CreateAsync(valera);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/valera/1
    [HttpPut("{id}")]
    public async Task<ActionResult<Valera>> Update(int id, [FromBody] Valera valera)
    {
        var updated = await _service.UpdateAsync(id, valera);
        if (updated == null)
            return NotFound();
        return Ok(updated);
    }

    // DELETE /api/valera/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }

    [HttpPost("{id}/actions/{ActName}")]
    public async Task<ActionResult<Valera>> PerformAction(int id, string ActName)
    {
        var valera = await _service.GetByIdAsync(id);
        if (valera == null)
            return NotFound();

        try
        {
            switch (ActName.ToLower())
            {
                case "work": valera.GoToWork(); break;
                case "nature": valera.EnjoyNature(); break;
                case "wine": valera.DrinkVineWatchTv(); break;
                case "bar": valera.GoToBar(); break;
                case "marginals": valera.DrinkWithMarginals(); break;
                case "sleep": valera.Sleep(); break;
                case "sing": valera.SingInSubway(); break;
                default: return BadRequest($"Unknown action: {ActName}");
            }
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message); 
        }

        await _service.UpdateAsync(id, valera);
        return Ok(valera);
    }
}
