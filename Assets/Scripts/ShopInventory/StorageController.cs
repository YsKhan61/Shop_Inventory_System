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

        protected ItemTypeTabButtonView CreateItemTabButton(ItemTypeTabButtonView prefab, Transform parent, TagSO tag)
        {
            ItemTypeTabButtonView button = Object.Instantiate(prefab, parent);
            button.gameObject.SetActive(true);
            button.SetText(tag.name);
            button.HideSelectedMarker();
            return button;
        }

        protected ItemTab CreateItemTab(TagSO tag, ItemTypeTabButtonView buttonView)
        {
            return new ItemTab()
            {
                ItemType = tag,
                ButtonView = buttonView,
                Slots = new List<SlotView>()
            };
        }

        protected void CreateSlotsInTab(ItemTab tab, List<ItemDataSO> items, SlotView prefab, Transform parent)
        {
            foreach (ItemDataSO data in items)
            {
                SlotView slot = CreateItemSlot(prefab, parent, data);
                tab.Slots.Add(slot);
            }
        }

        protected SlotView CreateItemSlot(SlotView prefab, Transform parent,  ItemDataSO data)
        {
            SlotView slot = Object.Instantiate(prefab, parent);
            slot.gameObject.SetActive(true);
            slot.SetStorageController(this);
            slot.SetTag(data.NameTag);
            slot.SetIcon(data.IconSprite);
            slot.Hide();
            return slot;
        }

        protected void OnItemTypeTabButtonClicked(ItemTab tab)
        {
            _selectedTab?.Hide();
            _selectedTab = tab;
            _selectedTab.Show();
        }
    }
}
