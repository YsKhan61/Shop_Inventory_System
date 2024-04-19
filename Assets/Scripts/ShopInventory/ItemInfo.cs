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
        public int Price;
        public int Weight;
        public string Rarity;
        public int QuantityToTrade;
        public string Message;
    }

    public struct InventoryItem
    {
        public int Quantity;
    }
}
