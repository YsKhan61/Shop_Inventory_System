using SIS.Utilities;
using System.Collections.Generic;


namespace SIS.ShopInventory
{
    public class ItemTab
    {
        public TagSO ItemType;
        public ItemTypeTabButtonView ButtonView;
        public List<SlotView> Slots;

        public void Show()
        {
            ButtonView.ShowSelectedMarker();
            ShowSlots();
        }

        public void Hide()
        {
            ButtonView.HideSelectedMarker();
            HideSlots();
        }

        void ShowSlots()
        {
            foreach (SlotView slot in Slots)
            {
                slot.Show();
            }
        }

        void HideSlots()
        {
            foreach (SlotView slot in Slots)
            {
                slot.Hide();
            }
        }
    }
}
