using SIS.Utilities;
using UnityEngine;


namespace SIS.ShopInventory
{
    public struct ItemInfo
    {
        public TagSO Tag;
        public bool IsInInventory;
        public bool IsBuyable;
        public string Name;
        public Sprite IconSprite;
        public string Description;
        public string Price;
        public string Rarity;
        public int QuantityToTrade;
        public int MaxQuantityToTrade;
        public string Message;
    }
}
