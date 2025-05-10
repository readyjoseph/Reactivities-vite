using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options) // DbContext is the base class for Entity Framework Core. It represents a session with the database and can be used to query and save instances of your entities.
{
     public required DbSet<Activity> Activities { get; set; }
}
