using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BrickHillDotNet
{
    public class TradeItem2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
        public bool is_special { get; set; }
        public int average_price { get; set; }
        public string average_price_abbr { get; set; }
    }

    public class TradeItem
    {
        public int id { get; set; }
        public int serial { get; set; }
        public Item item { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class TradeData
    {
        public Creator user { get; set; }
        public int bucks { get; set; }
        public List<Item> items { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public List<TradeData> trade { get; set; }
        public int is_accepted { get; set; }
        public int is_pending { get; set; }
        public int is_cancelled { get; set; }
        public int has_errored { get; set; }
        public DateTime created_at { get; set; }
    }

    public class Trade
    {
        public Data data { get; set; }

        public Trade GetTrade(int ID, string Type = "")
        {
            var FetchTrade = Bot.MakeRequest($"{APIUrls.USER_TRADES}{ID}/{Type}");
            Trade TradeInfo = JsonConvert.DeserializeObject<Trade>(FetchTrade);
            return TradeInfo;
        }
        public void InfoPrint()
        {
            Console.WriteLine($"-- Trade info: ID: {data.id}, Accepted: {data.is_accepted}, Created at: {data.created_at}, User: {data.trade.First().user.username}, Received items: {data.trade.ElementAt(1).items.Count}, Sent items: {data.trade.ElementAt(0).items.Count}");
        }
    }
}
