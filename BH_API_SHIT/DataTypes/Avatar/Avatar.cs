using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BH_API_SHIT
{
    public class Avatar
    {
        public int user_id { get; set; }
        public Items items { get; set; }
        public Colors colors { get; set; }
        public class Items
        {
            public int face { get; set; }
            public List<int> hats { get; set; }
            public int head { get; set; }
            public int tool { get; set; }
            public int pants { get; set; }
            public int shirt { get; set; }
            public int figure { get; set; }
            public int tshirt { get; set; }
        }

        public class Colors
        {
            public string head { get; set; }
            public string torso { get; set; }
            public string left_arm { get; set; }
            public string left_leg { get; set; }
            public string right_arm { get; set; }
            public string right_leg { get; set; }
        }
        public Avatar GetAvatar(int ID)
        {
            var FetchAvatar = Bot.HttpClient.GetAsync(Bot.BaseURL + "/v1/games/retrieveAvatar?id=" + ID);
            var FetchAvatarResult = FetchAvatar.Result.Content.ReadAsStringAsync().Result;
            Avatar avatar = JsonConvert.DeserializeObject<Avatar>(FetchAvatarResult);
            return avatar;
        }

        public void InfoPrint()
        {
            Console.WriteLine($"-- User ID: {user_id}, Shirt ID: {items.shirt} Pants ID: {items.pants} Head color: {colors.head} Right arm color: {colors.right_arm} Left arm color: {colors.left_arm} Left leg color: {colors.left_leg} Right leg color: {colors.right_leg} Torso color: {colors.torso}");
        }


    }
}
