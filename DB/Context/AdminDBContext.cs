using Microsoft.EntityFrameworkCore;
using Models.DB.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Context
{
    public class AdminDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Admin");

        public required DbSet<User> Users { get; set; }
    }
}
