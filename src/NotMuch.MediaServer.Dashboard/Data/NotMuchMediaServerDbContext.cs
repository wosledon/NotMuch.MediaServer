using Microsoft.EntityFrameworkCore;
using NotMuch.MediaServer.SocketServices;

namespace NotMuch.MediaServer.Dashboard.Data
{
    public class NotMuchMediaServerDbContext:DbContext
    {
        public DbSet<SocketServerInfo> SocketServerInfos { get; set; }

        public NotMuchMediaServerDbContext(DbContextOptions<NotMuchMediaServerDbContext> options)
            : base(options)
        {
            
        }
    }
}
