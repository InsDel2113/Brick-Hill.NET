using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BH_API_SHIT
{
    public class User
    {
        public string description { get; set; }
        public string username { get; set; }
        public int id { get; set; }
        public DateTime last_online { get; set; }
        public DateTime created_at { get; set; }
        public string img { get; set; }
        public IList<AwardParent> awards { get; set; }
        public IList<Status> status { get; set; }

        public Membership membership;

        public User GetUser(int ID)
        {
            var FetchUser = Bot.MakeRequest($"/v1/user/profile?id={ID}");
            User user = JsonConvert.DeserializeObject<User>(FetchUser);
            return user;
        }
        public User GetUser(string username)
        {
            // username -> id
            var UsernameToID = Bot.MakeRequest($"/v1/user/id?username={username}");
            IdToUsername user_to_id = JsonConvert.DeserializeObject<IdToUsername>(UsernameToID);
            // the id of the input username
            int ID = user_to_id.id;
            var FetchUserResult = Bot.MakeRequest($"/v1/user/profile?id={id}");
            User user = JsonConvert.DeserializeObject<User>(FetchUserResult);
            return user;
        }

        public void InfoPrint()
        {
            Console.WriteLine($"-- User Info: ID: {id}, Username: {username}, Last online: {last_online}, Created at: {created_at}, Description: {description}");
        }

        public bool OwnsItem(int itemID)
        {
            var FetchOwnsItem = Bot.MakeRequest($"/v1/user/{id}/owns/{itemID}");
            // we use dynamic parse here because it's legit one var one type
            dynamic result = JObject.Parse(FetchOwnsItem);
            return (bool)result.owns;
        }
    }
}
