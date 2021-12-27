namespace BrickHillDotNet
{
    public class AwardParent
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int award_id { get; set; }
        public int own { get; set; }
        public AwardChild award { get; set; }

    }
}
