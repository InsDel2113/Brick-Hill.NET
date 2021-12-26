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
            var FetchUser = Bot.HttpClient.GetAsync(Bot.BaseURL + "/v1/user/profile?id=" + ID);
            var FetchUserResult = FetchUser.Result.Content.ReadAsStringAsync().Result;
            User user = JsonConvert.DeserializeObject<User>(FetchUserResult);
            return user;
        }
        public User GetUser(string username)
        {
            var UsernameToId = Bot.HttpClient.GetAsync(Bot.BaseURL + "/v1/user/id?username=" + username);
            var UsernameToIdResult = UsernameToId.Result.Content.ReadAsStringAsync().Result;
            IdToUsername user_to_id = JsonConvert.DeserializeObject<IdToUsername>(UsernameToIdResult);

            int ID = user_to_id.id;
            var FetchUser = Bot.HttpClient.GetAsync(Bot.BaseURL + "/v1/user/profile?id=" + ID);
            var FetchUserResult = FetchUser.Result.Content.ReadAsStringAsync().Result;
            User user = JsonConvert.DeserializeObject<User>(FetchUserResult);
            return user;
        }

        public void InfoPrint()
        {
            Console.WriteLine($"-- User Info: ID: {id}, Username: {username}, Last online: {last_online}, Created at: {created_at}, Description: {description}");
        }

        public bool OwnsItem(int itemID)
        {
            var FetchOwnsItem = Bot.HttpClient.GetAsync(Bot.BaseURL + $"/v1/user/{id}/owns/{itemID}");
            var FetchOwnsItemResult = FetchOwnsItem.Result.Content.ReadAsStringAsync().Result;
            // we use dynamic parse here because it's legit one data type
            dynamic result = JObject.Parse(FetchOwnsItemResult);
            return (bool)result.owns;
        }
    }
}
