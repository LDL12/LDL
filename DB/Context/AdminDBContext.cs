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
        public AdminDBContext(DbContextOptions<AdminDBContext> options) : base(options)
        {
        }

        public required DbSet<User> Users { get; set; }
    }
}
