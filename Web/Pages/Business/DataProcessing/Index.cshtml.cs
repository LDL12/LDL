using Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Business.DataProcessing
{
    public class IndexModel : PageModel
    {
        //private IHttpClientFactory _httpClientFactory;
        //public IndexModel(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}

        public void OnGet()
        {
        }

        /// <summary>
        /// 逗号分隔
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public IActionResult OnPostSeparateByComma(string input)
        {
            var rv = input.TrySplitValue<string>();
            return new JsonResult(string.Join(",", rv));
        }

        /// <summary>
        /// 换行分隔
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostSeparateByNewline(string input)
        {
            var rv = input.TrySplitValue<string>();
            return new JsonResult(string.Join("\n", rv));
        }

        /// <summary>
        /// 去重
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostDistinct(string input)
        {
            var rv = input.TrySplitValue<string>();
            return new JsonResult(string.Join(",", rv.DistinctIgnoreCase()));
        }

        /// <summary>
        /// 交集
        /// </summary>
        /// <param name="firstInput"></param>
        /// <param name="secondInput"></param>
        /// <returns></returns>
        public IActionResult OnPostIntersect(string firstInput, string secondInput)
        {
            var array1 = firstInput.TrySplitValue<string>();
            var array2 = secondInput.TrySplitValue<string>();
            var rv = array1.Intersect(array2, StringComparer.OrdinalIgnoreCase);
            return new JsonResult(string.Join(",", rv));
        }

        /// <summary>
        /// 并集
        /// </summary>
        /// <param name="firstInput"></param>
        /// <param name="secondInput"></param>
        /// <returns></returns>
        public IActionResult OnPostConcat(string firstInput, string secondInput)
        {
            var array1 = firstInput.TrySplitValue<string>();
            var array2 = secondInput.TrySplitValue<string>();
            var rv = array1.Concat(array2);
            return new JsonResult(string.Join(",", rv));
        }

        /// <summary>
        /// 差集(A\B)
        /// </summary>
        /// <param name="firstInput"></param>
        /// <param name="secondInput"></param>
        /// <returns></returns>
        public IActionResult OnPostExcept(string firstInput, string secondInput)
        {
            var array1 = firstInput.TrySplitValue<string>();
            var array2 = secondInput.TrySplitValue<string>();
            var rv = array1.Except(array2, StringComparer.OrdinalIgnoreCase);
            return new JsonResult(string.Join(",", rv));
        }
    }
}
