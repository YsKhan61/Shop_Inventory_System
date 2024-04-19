namespace SIS.Utilities
{
    public struct ReturnMessage
    {
        public bool IsSuccess;
        public string Message;
    }

    public class EventService
    {
        private static EventService instance;
        public static EventService Instance
            => instance ??= new EventService();

        public DoubleFuncEvent<int, int, ReturnMessage> OnItemSelectedInShop { get; private set; }

        public Event<TagSO> OnBuyItem { get; private set; }
        public Event<TagSO> OnSellItem { get; private set; }

        public EventService()
        {
            OnItemSelectedInShop = new();
            OnBuyItem = new();
            OnSellItem = new();
        }
    }
}

