using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;
//This is using the new C# 9.0 feature of using a constructor to inject dependencies directly into the class
public class ActivitiesController(AppDbContext context) : BaseApiController
{
    // private readonly AppDbContext _context;

    // //Old fashsioned way of injecting dependencies
    // public ActivitiesController(AppDbContext context)
    // {
    //     _context = context;
    // }
    
    // GET api/activities
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities(CancellationToken ct)
    {
        return await context.Activities.ToListAsync(ct);
    }

    // GET api/activities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(string id, CancellationToken ct)
    {
        var activity = await context.Activities.FindAsync(id, ct);
        if (activity == null) return NotFound();
        return activity;
    }

}
