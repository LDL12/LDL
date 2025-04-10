using Microsoft.EntityFrameworkCore;
using Models.DB.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Context
{
    public class BusinessDBContext : DbContext
    {
        public BusinessDBContext(DbContextOptions<BusinessDBContext> options) : base(options)
        {
        }

        public required DbSet<TanShu14LotteryTicket> TanShu14LotteryTickets { get; set; }
    }
}
