using ChatStudents_Тепляков.Classes.Common;
using ChatStudents_Тепляков.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatStudents_Тепляков.Classes
{
    public class MessagesContext : DbContext
    {
        public DbSet<Messages> Messages { get; set; }
        public MessagesContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(Config.config);
    }
}
