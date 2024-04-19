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

            EventService.Instance.OnItemSelectedInShop.AddListener(OnItemSelectedInShop);
            EventService.Instance.OnBuyItem.AddListener(OnBuyItem);
            EventService.Instance.OnSellItem.AddListener(OnSellItem);
        }

        ~InventoryController()
        {
            foreach (ItemTab tab in _tabs)
            {
                tab.ButtonView.Button.onClick.RemoveAllListeners();
            }

            EventService.Instance.OnItemSelectedInShop.RemoveListener(OnItemSelectedInShop);
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
            bool found = _model.TryGetItemDataByTag(itemTag, out ItemDataSO data);
            if (!found)
                return;

            ItemInfo info = new()
            {
                Tag = itemTag,
                IsInInventory = true,
                Name = data.name,
                IconSprite = data.IconSprite,
                Description = data.Description,
                Price = data.SellPrice.ToString(),
                Rarity = data.RarityTag.name,
                QuantityToTrade = 1,
                MaxQuantityToTrade = 5
            };

            _view.ItemInfoView.ShowItemInfo(info);
        }

        protected override void CreateItemTabs()
        {
            foreach (TagSO typeTag in _model.ItemTypes)
            {
                ItemTypeTabButtonView buttonView =
                    CreateItemTabButton(
                        _model.ItemTypeTabButtonPrefab,
                        _view.ItemTypeTabButtonContainer.transform,
                        typeTag);

                ItemTab tab = CreateItemTab(typeTag, buttonView);

                buttonView.Button.onClick.AddListener(() => OnItemTypeTabButtonClicked(tab));
                tab.Hide();
                _tabs.Add(tab);
            }
        }

        private ReturnMessage OnItemSelectedInShop(int price, int weight)
        {
            ReturnMessage message = new();

            if (price > _model.CoinsCount)
            {
                message.Message = "Not enough coins!";
            }
            else if (weight > _model.MaxWeight - _model.CurrentWeight)
            {
                message.Message = "Not enough space!";
            }

            message.IsSuccess = price <= _model.CoinsCount
                && weight <= _model.MaxWeight - _model.CurrentWeight;

            return message;
        }

        private void OnBuyItem(TagSO itemTag)
        {
            _view.ItemInfoView.Hide();

            bool found = _model.TryGetItemDataByTag(itemTag, out ItemDataSO data);
            if (!found)
                return;

            _model.CoinsCount -= data.BuyPrice;
            _model.CurrentWeight += data.Weight;

            found = TryGetItemTabByTag(data.TypeTag, out ItemTab tab);
            if (!found)
                return;

            CreateSlotAndAddInTab(tab, data, _model.SlotPrefab, _view.TabContainer.transform);
            _selectedTab?.Hide();
            _selectedTab = tab;
            _selectedTab.Show();
        }

        private void OnSellItem(TagSO itemTag)
        {
            _view.ItemInfoView.Hide();

            bool found = _model.TryGetItemDataByTag(itemTag, out ItemDataSO data);
            if (!found)
                return;

            _model.CoinsCount += data.SellPrice;
            _model.CurrentWeight -= data.Weight;

            found = TryGetItemTabByTag(data.TypeTag, out ItemTab tab);
            if (!found)
                return;

            RemoveSlotFromTab(tab, itemTag);
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

        private bool TryGetItemTabByTag(TagSO tag, out ItemTab tab)
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
