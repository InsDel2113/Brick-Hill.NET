using System.Collections.Generic;

namespace BrickHillDotNet
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class Datum
    {
        public int crate_id { get; set; }
        public int serial { get; set; }
        public Creator user { get; set; }
        public int bucks { get; set; }
    }

    public class ResellerList
    {
        public List<Datum> data { get; set; }
        public string next_cursor { get; set; }
        public object previous_cursor { get; set; }
    }


}
