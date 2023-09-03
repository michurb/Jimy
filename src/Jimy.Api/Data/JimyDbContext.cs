using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Api.Data;

public class JimyDbContext : DbContext
{
    public JimyDbContext(DbContextOptions<JimyDbContext> options) : base(options)
    {
    }

    public DbSet<Exercise> Exercises => Set<Exercise>();
}