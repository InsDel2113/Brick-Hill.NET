using Newtonsoft.Json;
using System;

namespace BrickHillDotNet
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
            var FetchItem = Bot.MakeRequest($"{APIUrls.ITEM_FETCH}{ID}");
            if (FetchItem.Contains("error"))
            {
                return null;
            }
            FetchItem = FetchItem.Remove(0, 8);
            FetchItem = FetchItem.Remove(FetchItem.Length - 1, 1);
            Item item = JsonConvert.DeserializeObject<Item>(FetchItem);
            return item;
        }

        public OwnerList GetOwnerList(int limit = 10)
        {
            var FetchItem = Bot.MakeRequest($"{APIUrls.ITEM_FETCH}{id}/owners?limit={limit}");
            OwnerList ownerlist = JsonConvert.DeserializeObject<OwnerList>(FetchItem);
            return ownerlist;
        }
        public ResellerList GetResellerList(int limit = 10)
        {
            var FetchItem = Bot.MakeRequest($"{APIUrls.ITEM_FETCH}{id}/resellers?limit={limit}");
            ResellerList ownerlist = JsonConvert.DeserializeObject<ResellerList>(FetchItem);
            return ownerlist;
        }


        // prints some useful info about it
        public void InfoPrint()
        {
            Console.WriteLine($"-- Item info: ID: {id}, Name: {name}, Creator: {creator.username}, Created at: {created_at}, Updated at: {updated_at}");
        }
    }
}
