using SIS.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.ShopInventory
{
    public class ShopModel
    {
        private ShopDataSO _data;

        public ShopModel(ShopDataSO data) => _data = data;

        public ItemTypeTabButtonView ItemTypeTabButtonPrefab 
            => _data.ItemTypeTabButtonPrefab;

        public SlotView SlotPrefab => _data.SlotPrefab;

        public TagSO[] ItemTypes => _data.ItemContainer.ItemTypes;

        public bool TryGetItemsByType(TagSO typeTag, out List<ItemDataSO> items)
            => _data.ItemContainer.TryGetItemByType(typeTag, out items);

        public bool TryGetItemDataByTag(TagSO tag, out ItemDataSO data)
            => _data.ItemContainer.TryGetItemByTag(tag, out data);
    }
}
