using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using static BrickHillDotNet.Crate;

namespace BrickHillDotNet
{
    public class Bot
    {
        public static HttpClient HttpClient { get; set; }
        // brick_hill_session
        public static string SessionToken = "";

        public void SetupHttpClient()
        {
            // Make the cookie container, make the handler and assign the cookie container to it, add the session token to the cookies and make the client with the handler!
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            cookieContainer.Add(new Uri(APIUrls.BASE_URL), new Cookie("brick_hill_session", SessionToken));
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
            var User = new User().GetUser(199939); // User.GetUser("InsDel") also works, does username -> id lookup
            if (User != null) // check if things are valid by checking if they are null or id=0 (initialized but not fetched)
            {
                User.InfoPrint();
                // User.OwnsItem(itemid) - does user own itemid?
                Console.WriteLine(" -Does user own item: " + User.OwnsItem(304637).ToString());
            }

            // Get item
            var Item = new Item().GetItem(304637);
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



            // Error handling: if you don't try catch this, a exception is thrown complaining about the non authenticated api error (if you didn't set SessionToken!)
            // This works on everything, if anything throws a api error it will throw a exception and it can be try-caught
            try
            {
                // Get trade
                var Trade = new Trade().GetTrade(294964);
                Trade.InfoPrint();
            }
            catch
            {

            }

            // accepts a second parameter "type", go to it's definition to see all of them!
            var TradeList = new TradeList().GetTradeList(199939);
            foreach (var Trade in TradeList.data)
            {
                Console.WriteLine($"Trade list: Trade ID: {Trade.id} Trade user (to/from): {Trade.user.username}");
            }


            // Get clan
            var Clan = new Clan().GetClan(27); // Brick Hill Staff Clan
            // you can also search by name
            //  Clan = new Clan().GetClan("Brick Hill Staff");
            Clan.InfoPrint();
            // Get clan members
            Console.WriteLine("-- Get clan members --");
            var ClanMembers = Clan.GetMembers(50); // Get moderators rank
            foreach (var data in ClanMembers.data)
            {
                Console.WriteLine($"Username: {data.user.username}, Rank: {data.rank}");
            }


            // Get avatar info
            var avatar = new Avatar().GetAvatar(199939);
            avatar.InfoPrint();

            // Get set
            var Set = new Sets().GetSet(2);
            Set.InfoPrint();


            Console.WriteLine("-- Shop list, recent items --");
            var shoplist = new ShopList().GetShopList();
            foreach (var item in shoplist.data)
            {
                Console.WriteLine($"ID: {item.id}, Item name: {item.name}, Created at: {item.created_at}");
            }

            Console.WriteLine("-- Shop list, newest item --");
            var shoplistnewest = new ShopList().GetShopList(1, "newest");
            Console.WriteLine(shoplistnewest.data.First().name);

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
        // This also checks for API errors
        public static string MakeRequest(string APIURL)
        {
            // "/v1/user/id?username="
            var FetchRequest = Bot.HttpClient.GetAsync(APIUrls.BASE_URL + APIURL);
            var FetchRequestResult = FetchRequest.Result.Content.ReadAsStringAsync().Result;
            Bot.CheckError(FetchRequestResult);
            return FetchRequestResult;
        }

        public static Error CheckError(string response)
        {
            Error error = JsonConvert.DeserializeObject<Error>(response);
            if (error != null && error.error != null)
            {
                throw new Exception($"API returned error. Error: {error.error.prettyMessage}");
                return error;
            }
            if (error == null) return null;
            return null;
        }
    }
}
