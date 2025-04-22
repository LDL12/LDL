using DB.Context;
using DB.Dao.Admin;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Common.Result;
using Models.DB.Admin;
using Models;

namespace Web.Pages.BasePageModel
{
    [Authorize]
    public class UserPageModel : PageModel
    {
        private UserInfo? _UserInfo;

        public UserInfo UserInfo
        {
            get
            {
                if (_UserInfo != null)
                {
                    return _UserInfo;
                }

                var isAuthenticated = HttpContext?.User?.Identity?.IsAuthenticated;
                if (!isAuthenticated.GetValueOrDefault())
                {
                    throw new Exception("用户未通过身份验证");
                }

                var userId = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userName = HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
                if (userId == null || userName == null)
                {
                    throw new Exception("用户信息存在异常");
                }

                _UserInfo = new UserInfo() { UserId = Convert.ToInt32(userId), UserName = userName };
                return _UserInfo;
            }
        }
    }
}
