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
        /// ���ŷָ�
        /// </summary>
        /// <param name="input">����</param>
        /// <returns></returns>
        public IActionResult OnPostSeparateByComma(string input)
        {
            var array = input.TrySplitValue<string>();
            return new JsonResult(string.Join(",", array));
        }

        /// <summary>
        /// ���зָ�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostSeparateByNewline(string input)
        {
            var array = input.TrySplitValue<string>();
            return new JsonResult(string.Join("\n", array));
        }

        /// <summary>
        /// ȥ��
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostDistinct(string input)
        {
            var array = input.TrySplitValue<string>();
            return new JsonResult(string.Join(",", array.DistinctIgnoreCase()));
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostSortAscending(string input)
        {
            var array = input.TrySplitValue<string>().OrderBy(o => o);
            return new JsonResult(string.Join(",", array));
        }

        /// <summary>
        /// �������֣�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostNumberSortAscending(string input)
        {
            var array = input.TrySplitValue<decimal>().OrderBy(o => o);
            return new JsonResult(string.Join(",", array));
        }

        /// <summary>
        /// ��ת
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostReverse(string input)
        {
            var array = input.TrySplitValue<string>().Reverse();
            return new JsonResult(string.Join(",", array));
        }

        /// <summary>
        /// ���ܣ�Sha512��
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult OnPostSha512(string input)
        {
            // ����һ�� SHA512 ʵ��
            using (SHA512 sha512 = SHA512.Create())
            {
                // �������ַ���ת��Ϊ�ֽ����飨UTF-8 ���룩
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // �����ϣֵ�������һ���ֽ����飩
                byte[] hashBytes = sha512.ComputeHash(inputBytes);

                // ���ֽ�����ת��Ϊʮ�������ַ���
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // "x2" ��ʾÿ���ֽ�����λʮ�����Ʊ�ʾ
                }

                return new JsonResult(sb.ToString());// ����ʮ�������ַ�����ʽ�Ĺ�ϣֵ
            }
        }


        /// <summary>
        /// ����
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
        /// ����
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
        /// �(A\B)
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
