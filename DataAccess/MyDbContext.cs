using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Xml;
using Microsoft.Extensions.Options;


namespace DataAccess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        DbSet<UserData> Users { get; set; }
        DbSet<TicketData> Tickets { get; set; }

    }
}
