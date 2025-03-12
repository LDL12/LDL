using Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HttpHelper
    {
        public static async Task<Result<string>> PostAsync(IHttpClientFactory httpClientFactory, string url, object data)
        {
            try
            {
                // 将对象转换为JSON格式字符串
                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                // 创建HttpContent对象
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // 发送POST请求并获取响应
                var response = await httpClientFactory.CreateClient().PostAsync(url, content);

                // 确保响应成功
                response.EnsureSuccessStatusCode();

                // 读取响应内容
                var responseBody = await response.Content.ReadAsStringAsync();

                return Result<string>.WithSuccess(responseBody);
            }
            catch (Exception e)
            {
                return Result<string>.WithError(e);
            }
        }
    }
}
