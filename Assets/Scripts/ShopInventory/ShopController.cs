using SIS.Utilities;
using System.Collections.Generic;
using UnityEngine;


namespace SIS.ShopInventory
{

    public class ShopController : IStorageController
    {
        ShopModel _model;
        ShopView _view;

        private List<ItemTab> _tabs = new();

        private ItemTab _selectedTab;

        public ShopController(ShopModel model, ShopView view)
        {
            _model = model;
            _view = view;
        }

        ~ShopController()
        {
            foreach (ItemTab tab in _tabs)
            {
                tab.ButtonView.Button.onClick.RemoveAllListeners();
            }
        }

        public void Initialize()
        {
            CreateItemTabs();

            if (_tabs.Count > 0)
            {
                _selectedTab = _tabs[0];
                _selectedTab.Show();
            }
        }

        public void ShowItemInfo(TagSO itemTag)
        {
            bool found = _model.TryGetItemDataByTag(itemTag, out ItemDataSO data);
            if (!found)
                return;

            ItemInfo info = new()
            {
                IsBought = false,
                Name = data.name,
                IconSprite = data.IconSprite,
                Description = data.Description,
                Price = data.BuyPrice.ToString(),
                Rarity = data.RarityTag.name,
                QuantityToTrade = 1,
                MaxQuantityToTrade = 5
            };

            _view.ItemInfoView.ShowItemInfo(info);
        }

        private void CreateItemTabs()
        {
            bool found = _model.TryGetItemTypeList(out TagSO[] list);
            if (!found)
            {
                return;
            }

            foreach (TagSO tag in list)
            {
                ItemTypeTabButtonView buttonView = CreateItemTabButton(tag);

                bool foundItems = _model.TryGetItemsByType(tag, out List<ItemDataSO> items);
                if (!foundItems)
                {
                    continue;
                }

                ItemTab tab = new()
                {
                    ItemType = tag,
                    ButtonView = buttonView,
                    Slots = new List<SlotView>(),
                };

                foreach (ItemDataSO data in items)
                {
                    tab.Slots.Add(CreateItemSlot(data));
                }

                buttonView.Button.onClick.AddListener(() => OnItemTypeTabButtonClicked(tab));
                tab.Hide();
                _tabs.Add(tab);
            }
        }

        private ItemTypeTabButtonView CreateItemTabButton(TagSO tag)
        {
            ItemTypeTabButtonView button = Object.Instantiate(_model.ItemTypeTabButtonPrefab, _view.ItemTypeTabButtonContainer.transform);
            button.gameObject.SetActive(true);
            button.SetText(tag.name);
            button.HideSelectedMarker();
            return button;
        }

        private SlotView CreateItemSlot(ItemDataSO data)
        {
            SlotView slot = Object.Instantiate(_model.SlotPrefab, _view.TabContainer.transform);
            slot.gameObject.SetActive(true);
            slot.SetStorageController(this);
            slot.SetTag(data.NameTag);
            slot.SetIcon(data.IconSprite);
            slot.Hide();
            return slot;
        }

        private void OnItemTypeTabButtonClicked(ItemTab tab)
        {
            _selectedTab?.Hide();
            _selectedTab = tab;
            _selectedTab.Show();
        }
    }
}
