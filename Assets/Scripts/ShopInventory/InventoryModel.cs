using SIS.Utilities;
using System;
using UnityEngine;


namespace SIS.ShopInventory
{
    public class InventoryModel
    {
        public Action<int, int> OnCurrentWeightChanged = delegate { };
        public Action<int> OnCoinsCountUpdated = delegate { };

        private InventoryDataSO _data;

        public InventoryModel(InventoryDataSO data) => _data = data;

        public TagSO[] ItemTypes => _data.ItemTypes;

        public ItemTypeTabButtonView ItemTypeTabButtonPrefab
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

        public bool CanStoreWeight(int weight)
        {
            int newWeight = _currentWeight + weight;
            return newWeight <= MaxWeight;
        }

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
    }

}
