using Newtonsoft.Json;
using System;

namespace BrickHillDotNet
{
    public class Sets
    {
        public int id { get; set; }
        public Creator creator { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int playing { get; set; }
        public int visits { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string thumbnail { get; set; }
        public string genre { get; set; }

        public Sets GetSet(int ID)
        {
            var FetchSet = Bot.MakeRequest($"{APIUrls.SETS_FETCH}{ID}");
            FetchSet = FetchSet.Remove(0, 8);
            FetchSet = FetchSet.Remove(FetchSet.Length - 1, 1);
            Sets item = JsonConvert.DeserializeObject<Sets>(FetchSet);
            return item;
        }

        public void InfoPrint()
        {
            Console.WriteLine($"-- Set info: ID: {id}, Name: {name}, Creator: {creator.username}, Visits: {visits}");
        }
    }
}
