using System;

namespace BrickHillDotNet
{
    public class Status
    {
        public int id { get; set; }
        public int? clan_id { get; set; }
        public int owner_id { get; set; }
        public string body { get; set; }
        public DateTime date { get; set; }
        public string type { get; set; }
    }
}
