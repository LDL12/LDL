using Business.LotteryTicket;
using Common;
using Common.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Pages.Business.Tool
{
    public class IndexModel : PageModel
    {
        private IHttpClientFactory _httpClientFactory;
        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostCommaSeparated(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new JsonResult(string.Empty);
            }

            var data = input.Replace(" ", ",").Replace("£¬", ",").Replace("\n", ",").Replace("\r", ",");
            var array = data.Split(',').Where(o => !string.IsNullOrEmpty(o));
            return new JsonResult(string.Join(",", array));
        }

        public IActionResult OnPostIntersect(string input1, string input2)
        {
            if (string.IsNullOrEmpty(input1) || string.IsNullOrEmpty(input2))
            {
                return new JsonResult(string.Empty);
            }

            var array1 = StringHelper.TrySplitValue<string>(input1);
            var array2 = StringHelper.TrySplitValue<string>(input2);
            var rv = array1.Intersect(array2, StringComparer.OrdinalIgnoreCase);
            return new JsonResult(string.Join(",", rv));
        }
    }
}
