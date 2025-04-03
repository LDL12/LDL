using DB.Context;
using DB.Dao.Admin;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Common.Result;

namespace Web.Pages.BasePageModel
{
    [Authorize]
    public class UserPageModel : PageModel
    {

    }
}
