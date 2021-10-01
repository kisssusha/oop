namespace Shops.Models
{
    public class ShopProductInfo
    {
        public ShopProductInfo(uint price = 0, uint count = 0)
        {
            Price = price;
            Count = count;
        }

        public ShopProductInfo(uint count)
        {
            Count = count;
        }

        public uint Price
        {
            get;
            set;
        }

        public uint Count
        {
            get;
            set;
        }
    }
}