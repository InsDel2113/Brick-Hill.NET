using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BrickHillDotNet
{
    public class Crate
    {
        public class ItemCrate
        {
            public int id { get; set; }
            public string name { get; set; }
            public string thumbnail { get; set; }
            public bool is_special { get; set; }
            public int? average_price { get; set; }
            public string average_price_abbr { get; set; }
        }

        public class MarketInfo
        {
            public int id { get; set; }
            public int serial { get; set; }
            public ItemCrate item { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
        }

        public class UserCrate
        {
            public List<MarketInfo> data { get; set; }
            public string next_cursor { get; set; }
            public object previous_cursor { get; set; }
        }


        public UserCrate GetCrate(int ID, int Limit = 10)
        {
            var FetchCrate = Bot.MakeRequest($"{APIUrls.USER_FETCH}{ID}/{APIUrls.USER_CRATE}?limit={Limit}");
            UserCrate crate = JsonConvert.DeserializeObject<UserCrate>(FetchCrate);
            return crate;
        }


    }
}