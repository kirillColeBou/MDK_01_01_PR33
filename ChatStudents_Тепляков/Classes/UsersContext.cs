﻿using ChatStudents_Тепляков.Classes.Common;
using ChatStudents_Тепляков.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatStudents_Тепляков.Classes
{
    public class UsersContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public UsersContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(Config.config);
    }
}
