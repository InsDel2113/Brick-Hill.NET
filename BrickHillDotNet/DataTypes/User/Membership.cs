using System;

namespace BrickHillDotNet
{
    public class Membership
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int membership { get; set; }
        public DateTime date { get; set; }
        public int length { get; set; }
        public int active { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
