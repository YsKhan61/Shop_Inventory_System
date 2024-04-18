using UnityEngine;


namespace SIS.ShopInventory
{
    public struct ItemInfo
    {
        public bool IsBought;
        public string Name;
        public Sprite IconSprite;
        public string Description;
        public string Price;
        public string Rarity;
        public int QuantityToTrade;
        public int MaxQuantityToTrade;
    }
}
