using SIS.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.ShopInventory
{
    public class ShopModel
    {
        private ShopDataSO _data;

        public ShopModel(ShopDataSO data)
        {
            this._data = data;
        }

        public ItemTypeTabButtonView ItemTypeTabButtonPrefab 
            => _data.ItemTypeTabButtonPrefab;

        public SlotView SlotPrefab => _data.SlotPrefab;

        public bool TryGetItemTypeList(out TagSO[] list)
        {
            int itemTypeCount = _data.ItemContainer.ItemTypes.Length;

            list = new TagSO[itemTypeCount];

            if (list.Length == 0)
            {
                Debug.LogError("Shop data's item type list is empty!");
                list = null;
                return false;
            }

            for (int i = 0; i < itemTypeCount; i++)
            {
                list[i] = _data.ItemContainer.ItemTypes[i];
            }

            return true;
        }

        public bool TryGetItemsByType(TagSO typeTag, out List<ItemDataSO> items)
        {
            bool found = _data.ItemContainer.TryGetItemByType(typeTag, out items);

            if (!found)
            {
                Debug.LogError($"No items found for type tag {typeTag.name}!");
                items = null;
                return false;
            }

            return true;
        }
    }
}
