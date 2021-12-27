using System;
using System.Net;
using System.Net.Http;
using static BH_API_SHIT.Crate;

namespace BH_API_SHIT
{
    public class Bot
    {
        // api base url
        public static string BaseURL = "https://api.brick-hill.com";
        public static HttpClient HttpClient { get; set; }
        // brick_hill_session
        public string SessionToken = "";

        public void SetupHttpClient()
        {
            // Make the cookie container, make the handler and assign the cookie container to it, add the session token to the cookies and make the client with the handler!
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            cookieContainer.Add(new Uri(BaseURL), new Cookie("brick_hill_session", SessionToken));
            HttpClient = new HttpClient(handler);

            // just trying to not get fricked by cloudflare
            // discord quote: "Jefemy: change your useragent" - this is allowed
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:95.0) Gecko/20100101 Firefox/95.0");
            HttpClient.DefaultRequestHeaders.Add("Accept-Language", "en-us;q=0.5");
            HttpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8,application/json");
        }

        public void Run()
        {
            SetupHttpClient();

            /* Some examples */
            // Get user
            var User = new User();
            User = User.GetUser(199939); // User.GetUser("InsDel") also works, does username -> id lookup
            User.InfoPrint();
            // User.OwnsItem(itemid) - does user own itemid?
            Console.WriteLine(" -Does user own item: " + User.OwnsItem(304637).ToString());

            // Get item
            var Item = new Item();
            Item = Item.GetItem(304637);
            Item.InfoPrint();
            Console.WriteLine("--  Item owner list: --");

            // Item owner list - get who owns that item ^
            var ItemOwnerList = Item.GetOwnerList();
            foreach (var item in ItemOwnerList.data)
            {
                Console.WriteLine($"ID: {item.id}, User: {item.user.username}, Serial: {item.serial}");
            }
            Console.WriteLine("--  Item reseller list: --");

            // Item reseller list - who's selling it on the market?
            var ItemResellerList = Item.GetResellerList();
            foreach (var item in ItemResellerList.data)
            {
                Console.WriteLine($"ID: {item.crate_id}, User: {item.user.username}, Serial: {item.serial}, Price: {item.bucks}");
            }

            // Get trade
            var Trade = new Trade();
            Trade = Trade.GetTrade(294964);
            Trade.InfoPrint();


            // Get clan
            var Clan = new Clan();
            Clan = Clan.GetClan(27); // Brick Hill Staff Clan
            Clan.InfoPrint();
            // Get clan members
            Console.WriteLine("-- Get clan members --");
            var ClanMembers = Clan.GetMembers(50); // Get moderators rank
            foreach (var data in ClanMembers.data)
            {
                Console.WriteLine($"Username: {data.user.username}, Rank: {data.rank}");
            }


            // Get avatar info
            var avatar = new Avatar();
            avatar = avatar.GetAvatar(199939);
            avatar.InfoPrint();

            // Get set
            var Set = new Sets();
            Set = Set.GetSet(2);
            Set.InfoPrint();


            Console.WriteLine("-- Shop list, recent items --");
            var shoplist = new ShopList();
            shoplist = shoplist.GetShopList();
            foreach (var item in shoplist.data)
            {
                Console.WriteLine($"ID: {item.id}, Item name: {item.name}, Created at: {item.created_at}");
            }

            Console.WriteLine("--  Get user crate/inventory example  --");
            // Get user crate/inventory:
            // how crates work:
            // make a variable with type Crate and init it with a new Crate()
            Crate crate_parent = new Crate();
            // make a variable with type UserCrate, then use .GetCrate on the Crate type'd variable
            UserCrate crate = crate_parent.GetCrate(199939);
            // and now you can have fun with the data
            foreach (var item in crate.data)
            {
                Console.WriteLine($"ID: {item.id}, Item ID: {item.item.id}, Item serial: {item.serial}, Item name: {item.item.name}");
            }
        }

        // Simple function just to make simple GET->response web requests a bit shorter
        public static string MakeRequest(string APIURL)
        {
            // "/v1/user/id?username="
            var FetchRequest = Bot.HttpClient.GetAsync(Bot.BaseURL + APIURL);
            var FetchRequestResult = FetchRequest.Result.Content.ReadAsStringAsync().Result;
            return FetchRequestResult;
        }
    }
}
