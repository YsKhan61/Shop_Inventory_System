using SIS.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace SIS.ShopInventory
{
    public class InventoryModel
    {
        public Action<int, int> OnCurrentWeightChanged = delegate { };
        public Action<int> OnCoinsCountUpdated = delegate { };

        private InventoryDataSO _data;

        public Dictionary<TagSO, InventoryItem> Items;

        public InventoryModel(InventoryDataSO data)
        {
            Items = new Dictionary<TagSO, InventoryItem>();
            _data = data;
        }

        public TagSO[] ItemTypes => _data.ItemContainer.ItemTypes;

        public TabButtonView ItemTypeTabButtonPrefab
            => _data.ItemTypeTabButtonPrefab;

        private int _currentWeight;
        public int CurrentWeight
        {
            get => _currentWeight;
            set
            {
                if (value > MaxWeight)
                {
                    Debug.LogError("Current weight can't be more than max weight!");
                    return;
                }
                _currentWeight = value;
                OnCurrentWeightChanged.Invoke(_currentWeight, MaxWeight);
            }
        }
        public int MaxWeight => _data.MaxWeight;

        private int _coinsCount;
        public int CoinsCount
        {
            get => _coinsCount;
            set
            {
                if (value < 0)
                {
                    Debug.LogError("Coins count can never be negative!");
                    return;
                }
                _coinsCount = value;
                OnCoinsCountUpdated.Invoke(_coinsCount);
            }
        }

        public bool TryGetItemData(TagSO tag, out ItemDataSO data) 
            => _data.ItemContainer.TryGetItemByTag(tag, out data);

        public SlotView SlotPrefab => _data.SlotPrefab;
    }

}
