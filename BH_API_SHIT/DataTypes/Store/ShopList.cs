using Newtonsoft.Json;
using System.Collections.Generic;

namespace BH_API_SHIT
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ShopList
    {
        public List<Item> data { get; set; }
        public string next_cursor { get; set; }
        public object previous_cursor { get; set; }

        public ShopList GetShopList(int limit = 10)
        {
            var FetchShop = Bot.HttpClient.GetAsync(Bot.BaseURL + "/v1/shop/list?sort=updated&limit=" + limit);
            var FetchShopResult = FetchShop.Result.Content.ReadAsStringAsync().Result;
            ShopList shoplist = JsonConvert.DeserializeObject<ShopList>(FetchShopResult);
            return shoplist;
        }
    }


}
