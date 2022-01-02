using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHillDotNet
{
    public class TradeInfo
    {
        public int id { get; set; }
        public int is_accepted { get; set; }
        public int is_pending { get; set; }
        public int is_cancelled { get; set; }
        public int has_errored { get; set; }
        public DateTime updated_at { get; set; }
        public string human_time { get; set; }
        public Creator user { get; set; }
    }

    public class TradeList
    {
        public List<TradeInfo> data { get; set; }
        public object next_cursor { get; set; }
        public object previous_cursor { get; set; }

        // inbound, outbound, history, accepted, declined
        public TradeList GetTradeList(int UserID, string Type = "history")
        {
            var FetchTradeList = Bot.MakeRequest($"{APIUrls.USER_TRADES}{UserID}/{Type}");
            TradeList TradeInfo = JsonConvert.DeserializeObject<TradeList>(FetchTradeList);
            return TradeInfo;
        }
    }
}
