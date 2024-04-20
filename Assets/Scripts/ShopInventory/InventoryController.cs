using SIS.Utilities;
using UnityEngine;

namespace SIS.ShopInventory
{
    public class InventoryController : StorageController
    {
        private InventoryModel _model;
        private InventoryView _view;

        public InventoryController(InventoryModel model, InventoryView view)
        {
            _model = model;
            _view = view;

            EventService.Instance.GetInventoryData.AddListener(SendData);
            EventService.Instance.GetItemQuantityFromInventory.AddListener(SendItemQuantity);
            EventService.Instance.OnBuyItem.AddListener(OnBuyItem);
            EventService.Instance.OnSellItem.AddListener(OnSellItem);
        }

        ~InventoryController()
        {
            foreach (ItemTab tab in _tabs)
            {
                tab.ButtonView.Button.onClick.RemoveAllListeners();
            }

            EventService.Instance.GetInventoryData.RemoveListener(SendData);
            EventService.Instance.GetItemQuantityFromInventory.RemoveListener(SendItemQuantity);
            EventService.Instance.OnBuyItem.RemoveListener(OnBuyItem);
            EventService.Instance.OnSellItem.RemoveListener(OnSellItem);
        }

        public override void Initialize()
        {
            base.Initialize();

            _model.OnCoinsCountUpdated += _view.SetCoinsCount;
            _model.OnCurrentWeightChanged += _view.SetWeight;

            _model.CoinsCount = 100;
            _model.CurrentWeight = 0;
        }

        public override void ShowItemInfo(TagSO itemTag)
        {
            bool found = _model.TryGetItemData(itemTag, out ItemDataSO data);
            if (!found)
                return;

            ItemInfo info = new()
            {
                Tag = itemTag,
                IsInInventory = true,
                Name = data.name,
                IconSprite = data.IconSprite,
                Description = data.Description,
                Price = data.SellPrice,
                Weight = data.Weight,
                Rarity = data.RarityTag.name,
                QuantityToTrade = 1,
            };

            _view.ItemInfoView.ShowItemInfo(info);
        }

        protected override void CreateItemTabs()
        {
            foreach (TagSO typeTag in _model.ItemTypes)
            {
                TabButtonView buttonView =
                    CreateItemTabButton(
                        _model.TabButtonView,
                        _view.TabButtonContainer.transform,
                        typeTag);

                ItemTab tab = CreateItemTab(typeTag, buttonView);

                buttonView.Button.onClick.AddListener(() => OnTabButtonClicked(tab));
                tab.Hide();
                _tabs.Add(tab);
            }
        }

        private InventoryData SendData()
        {
            return new InventoryData
            {
                CoinsCount = _model.CoinsCount,
                AvailableWeight = _model.MaxWeight - _model.CurrentWeight
            };
        }

        private int SendItemQuantity(TagSO itemTag)
        {
            bool found = _model.Items.TryGetValue(itemTag, out InventoryItem item);
            if (!found)
                return 0;

            return item.Quantity;
        }

        private void OnBuyItem(TagSO itemTag, int qty)
        {
            bool found = _model.TryGetItemData(itemTag, out ItemDataSO data);
            if (!found)
                return;

            _model.CoinsCount -= data.BuyPrice * qty;
            _model.CurrentWeight += data.Weight * qty;

            found = TryGetItemTab(data.TypeTag, out ItemTab tab);
            if (!found)
                return;

            found = _model.Items.TryGetValue(itemTag, out InventoryItem item);
            if (!found)
            {
                item = new InventoryItem { Quantity = qty };
                _model.Items.Add(itemTag, item);
                CreateSlotAndAddInTab(tab, data, _model.SlotPrefab, _view.TabContainer.transform);   
            }
            else
            {
                item.Quantity += qty;
                _model.Items[itemTag] = item;
            }

            UpdateStackCountInTab(tab, itemTag, item);
            _selectedTab?.Hide();
            _selectedTab = tab;
            _selectedTab.Show();

            EventService.Instance.OnItemBought.InvokeEvent(itemTag, qty);
        }

        private void OnSellItem(TagSO itemTag, int qty)
        {
            bool found = _model.TryGetItemData(itemTag, out ItemDataSO data);
            if (!found)
                return;

            _model.CoinsCount += data.SellPrice * qty;
            _model.CurrentWeight -= data.Weight * qty;

            found = TryGetItemTab(data.TypeTag, out ItemTab tab);
            if (!found)
                return;

            found = _model.Items.TryGetValue(itemTag, out InventoryItem item);
            if (!found)
            {
                Debug.LogError("Item not found!");
                return;
            }

            item.Quantity -= qty;
            _model.Items[itemTag] = item;

            switch (item.Quantity)
            {
                case 0:
                    RemoveSlotFromTab(tab, itemTag);
                    _model.Items.Remove(itemTag);
                    break;
                default:
                    UpdateStackCountInTab(tab, itemTag, item);
                    break;
            }

            EventService.Instance.OnItemSold.InvokeEvent(itemTag, qty);
        }

        private void UpdateStackCountInTab(ItemTab tab, TagSO itemTag, InventoryItem item)
        {
            SlotView slotView = tab.Slots.Find(s => s.ItemTag == itemTag);
            if (slotView == null)
            {
                Debug.LogError("Slot not found!");
                return;
            }

            UpdateStackCountInSlot(slotView, item);
        }

        private void UpdateStackCountInSlot(SlotView slot, InventoryItem item)
        {
            if (item.Quantity == 1)
            {
                slot.HideStackCount();
            }
            else
            {
                slot.ShowStackCount();
                slot.SetStackCount(item.Quantity);
            }
        }

        

        private void RemoveSlotFromTab(ItemTab tab, TagSO itemTag)
        {
            SlotView slot = tab.Slots.Find(s => s.ItemTag == itemTag);
            if (slot == null)
            {
                Debug.LogError("Slot not found!");
                return;
            }

            tab.Slots.Remove(slot);
            Object.Destroy(slot.gameObject);
        }

        private bool TryGetItemTab(TagSO tag, out ItemTab tab)
        {
            tab = _tabs.Find(t => t.ItemType == tag);
            if (tab == null)
            {
                Debug.LogError("Item tab not found!");
                return false;
            }

            return true;
        }
    }
}
