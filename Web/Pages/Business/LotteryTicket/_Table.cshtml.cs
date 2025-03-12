using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DB.Business;

namespace Web.Pages.Business.LotteryTicket
{
    public class _TableModel : PageModel
    {
        public void OnGet()
        {
        }

        public List<TanShu14LotteryTicket> LotteryTickets { get; set; } = new List<TanShu14LotteryTicket>();
    }
}
