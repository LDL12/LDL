using Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;
using Web.Pages.BasePageModel;

namespace Web.Pages.Business.DataProcessing
{
    public class IndexModel : UserPageModel
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
            var array = input.TrySplitValue<string>();
            return new JsonResult(string.Join(",", array));
        }

        /// <summary>
        /// 换行分隔
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostSeparateByNewline(string input)
        {
            var array = input.TrySplitValue<string>();
            return new JsonResult(string.Join("\n", array));
        }

        /// <summary>
        /// 去重
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostDistinct(string input)
        {
            var array = input.TrySplitValue<string>();
            return new JsonResult(string.Join(",", array.DistinctIgnoreCase()));
        }

        /// <summary>
        /// 正序
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostSortAscending(string input)
        {
            var array = input.TrySplitValue<string>().OrderBy(o => o);
            return new JsonResult(string.Join(",", array));
        }

        /// <summary>
        /// 正序（数字）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostNumberSortAscending(string input)
        {
            var array = input.TrySplitValue<decimal>().OrderBy(o => o);
            return new JsonResult(string.Join(",", array));
        }

        /// <summary>
        /// 反转
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostReverse(string input)
        {
            var array = input.TrySplitValue<string>().Reverse();
            return new JsonResult(string.Join(",", array));
        }

        /// <summary>
        /// 加密（Sha512）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostSha512(string input)
        {
            // 创建一个 SHA512 实例
            using (SHA512 sha512 = SHA512.Create())
            {
                // 将输入字符串转换为字节数组（UTF-8 编码）
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // 计算哈希值（输出是一个字节数组）
                byte[] hashBytes = sha512.ComputeHash(inputBytes);

                // 将字节数组转换为十六进制字符串
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // "x2" 表示每个字节用两位十六进制表示
                }

                return new JsonResult(sb.ToString());// 返回十六进制字符串形式的哈希值
            }
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
            var array = array1.Intersect(array2, StringComparer.OrdinalIgnoreCase);
            return new JsonResult(string.Join(",", array));
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
            var array = array1.Concat(array2);
            return new JsonResult(string.Join(",", array));
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
            var array = array1.Except(array2, StringComparer.OrdinalIgnoreCase);
            return new JsonResult(string.Join(",", array));
        }
    }
}
