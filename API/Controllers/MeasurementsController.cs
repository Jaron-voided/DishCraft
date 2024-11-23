using Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using SQLitePCL;

namespace API.Controllers;

public class MeasurementsController : BaseApiController
{
    private readonly DataContext _context;

    public MeasurementsController(DataContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Measurement>>> GetMeasurementsByRecipe([FromQuery] Guid recipeId)
    {
        var measurements = await _context.Measurements
            .Where(m => m.RecipeId == recipeId)
            .ToListAsync();
        
        return Ok(measurements);
    }
    
}