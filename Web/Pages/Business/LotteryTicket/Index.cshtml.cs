using Business.LotteryTicket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DB.Business;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Web.Pages.Business.LotteryTicket
{
    public class IndexModel : PageModel
    {
        private IHttpClientFactory _httpClientFactory;
        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public required _TableModel _TableModel { get; set; }

        public async Task OnGetAsync()
        {
            var lotteryTicketsResult = await new LotteryTicketService().LoadTanShu14LotteryTickets(_httpClientFactory);
            _TableModel = new _TableModel() { LotteryTickets = lotteryTicketsResult?.Data ?? new List<TanShu14LotteryTicket>() };
        }

        //public async Task<PartialViewResult> OnPostRefreshTable()
        //{
        //    var result = await new LotteryTicketService().LoadTanShu14LotteryTickets(_httpClientFactory);
        //    if (!result.IsSuccess)
        //    {
        //        return Partial("_Table", new _TableModel());
        //    }

        //    return Partial("_Table", new _TableModel() { LotteryTickets = result?.Data ?? new List<TanShu14LotteryTicket>() });
        //}

        public async Task<IActionResult> OnPostPredicted()
        {
            var result = await new LotteryTicketService().LoadAndPredicted(_httpClientFactory);
            if (!result.IsSuccess)
            {
                return new JsonResult(result.Message);
            }

            var str = string.Join(",", result.Data.Item1.Select(o => Convert.ToInt32(o))) + "\n" + string.Join(",", result.Data.Item2.Select(o => Convert.ToInt32(o)));
            return new JsonResult(str);
        }
    }
}
