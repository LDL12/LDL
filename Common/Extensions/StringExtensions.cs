using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    /// <summary>
    /// String扩展类
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 字符串切割
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T[] TrySplitValue<T>(this string? str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new T[0];
            }

            return str.Split(new char[4] { ',', '，', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(o => (T)Convert.ChangeType(o, typeof(T))).ToArray();
        }

        /// <summary>
        /// 去重（忽略大小写）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<string> DistinctIgnoreCase(this IEnumerable<string> value) => value.Distinct(StringComparer.OrdinalIgnoreCase);


    }
}
