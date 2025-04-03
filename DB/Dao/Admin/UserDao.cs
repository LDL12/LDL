using DB.Context;
using Microsoft.EntityFrameworkCore;
using Models.DB.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Dao.Admin
{
    public class UserDao
    {
        private readonly AdminDBContext _adminDBContext;

        public UserDao(AdminDBContext adminDBContext)
        {
            _adminDBContext = adminDBContext;
        }

        /// <summary>
        /// 加载用户信息（按用户名）
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User?> LoadUserByUserName(string? userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }

            return await _adminDBContext.Users.Where(o => o.UserName == userName).FirstAsync();
        }
    }
}
