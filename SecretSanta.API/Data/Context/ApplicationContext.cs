using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Data.Context;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<Participant> Participants { get; set; } = null!;
    public DbSet<Match> Matches { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}