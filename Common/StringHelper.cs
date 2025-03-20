using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class StringHelper
    {
        public static T[] TrySplitValue<T>(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return new T[0];

            return value.Split(new char[4] { ',', '，', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => (T)Convert.ChangeType(o, typeof(T))).ToArray();
        }
    }
}
