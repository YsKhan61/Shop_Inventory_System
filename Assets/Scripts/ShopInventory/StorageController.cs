using SIS.Utilities;
using System.Collections.Generic;
using UnityEngine;


namespace SIS.ShopInventory
{
    public abstract class StorageController : IStorageController
    {
        protected List<ItemTab> _tabs = new();
        protected ItemTab _selectedTab;

        public abstract void ShowItemInfo(TagSO itemTag);

        public virtual void Initialize()
        {
            CreateItemTabs();

            if (_tabs.Count > 0)
            {
                _selectedTab = _tabs[0];
                _selectedTab.Show();
            }
        }

        protected abstract void CreateItemTabs();

        protected TabButtonView CreateItemTabButton(TabButtonView prefab, Transform parent, TagSO tag)
        {
            TabButtonView button = Object.Instantiate(prefab, parent);
            button.gameObject.SetActive(true);
            button.SetText(tag.name);
            button.HideSelectedMarker();
            return button;
        }

        protected ItemTab CreateItemTab(TagSO typeTag, TabButtonView buttonView)
        {
            return new ItemTab()
            {
                ItemType = typeTag,
                ButtonView = buttonView,
                Slots = new List<SlotView>()
            };
        }

        protected void CreateSlotsInTab(ItemTab tab, List<ItemDataSO> items, SlotView prefab, Transform parent)
        {
            foreach (ItemDataSO data in items)
            {
                CreateSlotAndAddInTab(tab, data, prefab, parent);
            }
        }

        protected void CreateSlotAndAddInTab(ItemTab tab, ItemDataSO data, SlotView prefab, Transform parent)
        {
            SlotView slot = CreateItemSlot(prefab, parent, data);
            tab.Slots.Add(slot);
        }

        protected SlotView CreateItemSlot(SlotView prefab, Transform parent,  ItemDataSO data)
        {
            SlotView slot = Object.Instantiate(prefab, parent);
            slot.gameObject.SetActive(true);
            slot.SetStorageController(this);
            slot.SetTag(data.NameTag);
            slot.SetIcon(data.IconSprite);
            slot.HideStackCount();
            slot.Hide();
            return slot;
        }

        protected void OnTabButtonClicked(ItemTab tab)
        {
            _selectedTab?.Hide();
            _selectedTab = tab;
            _selectedTab.Show();
        }
    }
}
