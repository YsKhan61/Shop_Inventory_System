namespace SIS.Utilities
{
    public struct ReturnMessage
    {
        public bool IsSuccess;
        public string Message;
    }

    public struct TradeInfo
    {
        public int Price;
        public int Weight;
    }

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

        // public FuncEvent<TradeInfo, ReturnMessage> IsSelectedItemBuyable { get; private set; }
        public FuncEvent<InventoryData> GetInventoryData { get; private set; }
        public DoubleFuncEvent<TagSO, int> GetItemQuantityFromInventory { get; private set; }

        public DoubleEvent<TagSO, int> OnBuyItem { get; private set; }
        public DoubleEvent<TagSO, int> OnSellItem { get; private set; }

        public EventService()
        {
            // IsSelectedItemBuyable = new();
            GetInventoryData = new();
            GetItemQuantityFromInventory = new();
            OnBuyItem = new();
            OnSellItem = new();
        }
    }
}

