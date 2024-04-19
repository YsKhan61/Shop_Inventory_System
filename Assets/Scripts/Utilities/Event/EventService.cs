using UnityEngine;

namespace SIS.Utilities
{
    public struct InventoryData
    {
        public int CoinsCount;
        public int AvailableWeight;
    }

    public class EventService
    {
        private static EventService instance;
        public static EventService Instance
            => instance ??= new EventService();

        public FuncEvent<InventoryData> GetInventoryData { get; private set; }
        public DoubleFuncEvent<TagSO, int> GetItemQuantityFromInventory { get; private set; }

        public DoubleEvent<TagSO, int> OnBuyItem { get; private set; }
        public DoubleEvent<TagSO, int> OnSellItem { get; private set; }
        public DoubleEvent<TagSO, int> OnItemBought { get; private set; }
        public DoubleEvent<TagSO, int> OnItemSold { get; private set; }
        public Event<AudioClip> OnPlayAudio { get; private set; }


        public EventService()
        {
            GetInventoryData = new();
            GetItemQuantityFromInventory = new();
            OnBuyItem = new();
            OnSellItem = new();
            OnItemBought = new();
            OnItemSold = new();
            OnPlayAudio = new();
        }
    }
}