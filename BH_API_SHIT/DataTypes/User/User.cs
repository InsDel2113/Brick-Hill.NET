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
            var FetchUser = Bot.MakeRequest($"{APIUrls.USER_FETCH}profile?id={ID}");
            if (FetchUser.Contains("error"))
            {
                return null;
            }
            User user = JsonConvert.DeserializeObject<User>(FetchUser);
            return user;
        }
        public User GetUser(string username)
        {
            // username -> id
            var UsernameToID = Bot.MakeRequest($"{APIUrls.USER_FETCH}id?username={username}");
            IdToUsername user_to_id = JsonConvert.DeserializeObject<IdToUsername>(UsernameToID);
            // the id of the input username
            int ID = user_to_id.id;
            var FetchUserResult = Bot.MakeRequest($"{APIUrls.USER_FETCH}profile?id={id}");
            if (FetchUserResult.Contains("error"))
            {
                return null;
            }
            User user = JsonConvert.DeserializeObject<User>(FetchUserResult);
            return user;
        }

        public void InfoPrint()
        {
            Console.WriteLine($"-- User Info: ID: {id}, Username: {username}, Last online: {last_online}, Created at: {created_at}, Description: {description}");
        }

        public bool OwnsItem(int itemID)
        {
            var FetchOwnsItem = Bot.MakeRequest($"{APIUrls.USER_FETCH}{id}/owns/{itemID}");
            // we use dynamic parse here because it's legit one var one type
            dynamic result = JObject.Parse(FetchOwnsItem);
            // in case it's a invalid user
            if (result.owns == null)
                return false;
            return (bool)result.owns;
        }

        public BH_API_SHIT.Crate.UserCrate GetCrate(int Limit = 100)
        {
            var Crate = new Crate().GetCrate(id, Limit);
            return Crate;
        }
    }
}
