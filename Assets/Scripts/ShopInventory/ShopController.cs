﻿using SIS.Utilities;
using System.Collections.Generic;


namespace SIS.ShopInventory
{
    public class ShopController : StorageController
    {
        ShopModel _model;
        ShopView _view;

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

        public override void ShowItemInfo(TagSO itemTag)
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

        protected override void CreateItemTabs()
        {
            foreach (TagSO tag in _model.ItemTypes)
            {
                ItemTypeTabButtonView buttonView = 
                    CreateItemTabButton(
                        _model.ItemTypeTabButtonPrefab, 
                        _view.ItemTypeTabButtonContainer.transform, 
                        tag);

                ItemTab tab = CreateItemTab(tag, buttonView);

                bool foundItems = _model.TryGetItemsByType(tag, out List<ItemDataSO> items);
                if (!foundItems)
                {
                    continue;
                }

                CreateSlotsInTab(tab, items, _model.SlotPrefab, _view.TabContainer.transform);

                /*foreach (ItemDataSO data in items)
                {
                    SlotView slot = CreateItemSlot(_model.SlotPrefab, _view.TabContainer.transform, data);
                    tab.Slots.Add(slot);
                }*/

                buttonView.Button.onClick.AddListener(() => OnItemTypeTabButtonClicked(tab));
                tab.Hide();
                _tabs.Add(tab);
            }
        }

        /*private ItemTypeTabButtonView CreateItemTabButton(TagSO tag)
        {
            ItemTypeTabButtonView button = Object.Instantiate(_model.ItemTypeTabButtonPrefab, _view.ItemTypeTabButtonContainer.transform);
            button.gameObject.SetActive(true);
            button.SetText(tag.name);
            button.HideSelectedMarker();
            return button;
        }*/

        /*private SlotView CreateItemSlot(ItemDataSO data)
        {
            SlotView slot = Object.Instantiate(_model.SlotPrefab, _view.TabContainer.transform);
            slot.gameObject.SetActive(true);
            slot.SetStorageController(this);
            slot.SetTag(data.NameTag);
            slot.SetIcon(data.IconSprite);
            slot.Hide();
            return slot;
        }*/

        /*private void OnItemTypeTabButtonClicked(ItemTab tab)
        {
            _selectedTab?.Hide();
            _selectedTab = tab;
            _selectedTab.Show();
        }*/
    }
}
