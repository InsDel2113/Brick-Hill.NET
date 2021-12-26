using Newtonsoft.Json;
using System;

namespace BH_API_SHIT
{
    public class Item
    {
        public int id { get; set; }
        public Creator creator { get; set; }
        public Type type { get; set; }
        public int is_public { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? bits { get; set; }
        public int? bucks { get; set; }
        public int offsale { get; set; }
        public int special_edition { get; set; }
        public int special { get; set; }
        public int? stock { get; set; }
        public int? timer { get; set; }
        public DateTime? timer_date { get; set; }
        public int? average_price { get; set; }
        public int? stock_left { get; set; }
        public string thumbnail { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public Item GetItem(int ID)
        {
            var FetchItem = Bot.HttpClient.GetAsync(Bot.BaseURL + "/v1/shop/" + ID);
            var FetchItemResult = FetchItem.Result.Content.ReadAsStringAsync().Result;
            FetchItemResult = FetchItemResult.Remove(0, 8);
            FetchItemResult = FetchItemResult.Remove(FetchItemResult.Length - 1, 1);
            Item item = JsonConvert.DeserializeObject<Item>(FetchItemResult);
            return item;
        }

        public OwnerList GetOwnerList(int limit = 10)
        {
            var FetchItem = Bot.HttpClient.GetAsync(Bot.BaseURL + "/v1/shop/" + id + "/owners?limit=" + limit);
            var FetchItemResult = FetchItem.Result.Content.ReadAsStringAsync().Result;
            OwnerList ownerlist = JsonConvert.DeserializeObject<OwnerList>(FetchItemResult);
            return ownerlist;
        }
        public ResellerList GetResellerList(int limit = 10)
        {
            var FetchItem = Bot.HttpClient.GetAsync(Bot.BaseURL + "/v1/shop/" + id + "/resellers?limit=" + limit);
            var FetchItemResult = FetchItem.Result.Content.ReadAsStringAsync().Result;
            ResellerList ownerlist = JsonConvert.DeserializeObject<ResellerList>(FetchItemResult);
            return ownerlist;
        }


        // prints some useful info about it
        public void InfoPrint()
        {
            Console.WriteLine($"-- Item info: ID: {id}, Name: {name}, Creator: {creator.username}, Created at: {created_at}, Updated at: {updated_at}");
        }
    }
}
