using System;
using System.Collections.Generic;

namespace BrickHillDotNet
{
    public class OwnerList
    {
        public List<ListData> data { get; set; }
        public string next_cursor { get; set; }
        public object previous_cursor { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class ListData
    {
        public int id { get; set; }
        public int serial { get; set; }
        public IdToUsername user { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
