using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserInfo
    {
        public int UserId { get; set; }

        public required string UserName { get; set; }
    }
}
