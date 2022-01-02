using Newtonsoft.Json;
using System.Collections.Generic;

namespace BrickHillDotNet
{
    public class ShopList
    {
        public List<Item> data { get; set; }
        public string next_cursor { get; set; }
        public object previous_cursor { get; set; }

        // Avalible sorts:
        // updated, newest, oldest, expensive, inexpensive
        public ShopList GetShopList(int limit = 10, string sort = "updated")
        {
            var FetchShop = Bot.MakeRequest($"{APIUrls.ITEM_FETCH}list?sort={sort}&limit={limit}");
            ShopList shoplist = JsonConvert.DeserializeObject<ShopList>(FetchShop);
            return shoplist;
        }
    }


}
