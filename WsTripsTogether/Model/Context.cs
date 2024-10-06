using Microsoft.EntityFrameworkCore;

namespace WsTripsTogether.Model;

public class Context : DbContext
{
    public Context()
    {
    }
    
    public Context(DbContextOptions options) : base(options)
    {
    }
}