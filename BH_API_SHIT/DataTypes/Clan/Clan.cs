using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BH_API_SHIT
{
    public class Clan
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string tag { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }

        public Clan GetClan(int ID, bool remove_breaks = true)
        {
            var FetchClan = Bot.MakeRequest($"/v1/clan/clan?id={ID}");
            Clan clan = JsonConvert.DeserializeObject<Clan>(FetchClan);
            if (remove_breaks)
                clan.title = clan.title.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
            return clan;
        }

        public void InfoPrint()
        {
            Console.WriteLine($"-- Clan info: ID: {id}, Owner ID: {owner_id}, Tag: {tag}, Title: {title}, Created at: {created_at}");
        }

        public MemberListData GetMembers(int RankNum = 1, int PageNum = 1)
        {
            var FetchClan = Bot.HttpClient.GetAsync($"https://www.brick-hill.com/api/clans/members/{id}/{RankNum}/{PageNum}");
            var FetchClanResult = FetchClan.Result.Content.ReadAsStringAsync().Result;
            MemberListData memberlist = JsonConvert.DeserializeObject<MemberListData>(FetchClanResult);
            return memberlist;
        }

        public class MemberListData
        {
            public List<ListData> data { get; set; }
            public Pages pages { get; set; }
        }


        public class ListData
        {
            public int id { get; set; }
            public int clan_id { get; set; }
            public int user_id { get; set; }
            public int rank { get; set; }
            public string status { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public Creator user { get; set; }
        }

        public class Pages
        {
            public int pageCount { get; set; }
            public int current { get; set; }

            // these are both ints and bools... string should work!
            public string next { get; set; }
            public string previous { get; set; }
            public List<int> pages { get; set; }
        }
    }
}
