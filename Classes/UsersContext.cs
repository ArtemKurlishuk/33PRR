using System;
using System.Collections.Generic;
using System.Text;
using ChatStudents_Kurlishuk.Classes.Common;
using ChatStudents_Kurlishuk.Models;
using Microsoft.EntityFrameworkCore;


namespace ChatStudents_Kurlishuk.Classes
{
    public class UsersContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public UsersContext() =>
            Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(Config.config);
    }
}
