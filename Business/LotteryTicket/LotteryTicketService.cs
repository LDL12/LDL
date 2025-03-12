using Common;
using Common.Algorithm;
using Common.Result;
using Models.DB.Business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.LotteryTicket
{
    public class LotteryTicketService
    {
        const string Token = "24f6d9a2ecf990685691a5506cb26789";
        const string Token_2nd = "6b90a358ccafbe02982c9833fb1b14cd";
        const string Url = "https://api.tanshuapi.com/api/caipiao/v1/history";

        public async Task<Result<List<TanShu14LotteryTicket>>> LoadTanShu14LotteryTickets(IHttpClientFactory httpClientFactory)
        {
            try
            {
                var result = await LoadTanShu14LotteryTickets(httpClientFactory, Token);
                //换个token重试
                if (!result.IsSuccess)
                {
                    result = await LoadTanShu14LotteryTickets(httpClientFactory, Token_2nd);
                    if (!result.IsSuccess)
                    {
                        return Result<List<TanShu14LotteryTicket>>.WithError(result.Exception);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return Result<List<TanShu14LotteryTicket>>.WithError(ex);
            }
        }

        private async Task<Result<List<TanShu14LotteryTicket>>> LoadTanShu14LotteryTickets(IHttpClientFactory httpClientFactory, string token)
        {
            try
            {
                var postData = new Dictionary<string, object>();
                postData.Add("key", token);
                postData.Add("caipiaoid", 14);
                postData.Add("start", 0);
                postData.Add("num", 10);

                var postResult = await HttpHelper.PostAsync(httpClientFactory, Url, postData);
                if (!postResult.IsSuccess)
                {
                    return Result<List<TanShu14LotteryTicket>>.WithError(postResult.Exception);
                }

                var data = postResult.Data;
                if (string.IsNullOrEmpty(data))
                {
                    return Result<List<TanShu14LotteryTicket>>.WithError("接口返回空数据！");
                }

                var type = new { data = new { list = new List<TanShu14LotteryTicket>() } };
                var model = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(data, type);
                return Result<List<TanShu14LotteryTicket>>.WithSuccess(model?.data?.list);
            }
            catch (Exception ex)
            {
                return Result<List<TanShu14LotteryTicket>>.WithError(ex);
            }
        }

        public Result<(List<decimal>, List<decimal>)> Predicted(IEnumerable<TanShu14LotteryTicket>? datas)
        {
            try
            {
                if (datas == null || datas.Count() < 3)
                {
                    return Result<(List<decimal>, List<decimal>)>.WithError("彩票数据不足3期，拒绝预测！");
                }

                var lotteryTickets = datas?.OrderBy(o => o.Issueno)?.Select(o => o.GetNumbers());
                var tripleExponentialSmoothingAlgorithm = new TripleExponentialSmoothingAlgorithm();
                var linearRegressionAlgorithm = new LinearRegressionAlgorithm();
                var tripleExponentialSmoothingResult = new List<decimal>();
                var linearRegressionResult = new List<decimal>();

                for (var i = 0; i < 7; i++)
                {
                    var array = lotteryTickets?.Select(o => o[i]).ToArray();
                    tripleExponentialSmoothingResult.Add(tripleExponentialSmoothingAlgorithm.CalcAccuratePredictiveValue(array));
                    linearRegressionResult.Add(linearRegressionAlgorithm.CalcPredictiveValue(array));
                }

                return Result<(List<decimal>, List<decimal>)>.WithSuccess((tripleExponentialSmoothingResult, linearRegressionResult));
            }
            catch (Exception ex)
            {
                return Result<(List<decimal>, List<decimal>)>.WithError(ex);
            }
        }

        public async Task<Result<(List<decimal>, List<decimal>)>> LoadAndPredicted(IHttpClientFactory httpClientFactory)
        {
            try
            {
                var result = await LoadTanShu14LotteryTickets(httpClientFactory);

                return Predicted(result.Data);
            }
            catch (Exception ex)
            {
                return Result<(List<decimal>, List<decimal>)>.WithError(ex);
            }
        }
    }
}
