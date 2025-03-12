using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB.Business
{
    [Table("TanShu14LotteryTicket")]
    public class TanShu14LotteryTicket
    {
        [Column("issueno"), JsonProperty("issueno")]
        public int Issueno { get; set; }

        [Column("open_date"), JsonProperty("opendate")]
        public DateTime OpenDate { get; set; }

        [Column("number"), JsonProperty("number")]
        public required string FirstNumber { get; set; }

        [Column("refer_number"), JsonProperty("refernumber")]
        public required string BehindNumber { get; set; }

        public decimal[] GetNumbers()
        {
            var firstArray = FirstNumber.Split(" ");
            var behindArray = BehindNumber.Split(" ");

            return firstArray.Concat(behindArray).Select(o => Convert.ToDecimal(o)).ToArray();
        }
    }
}
