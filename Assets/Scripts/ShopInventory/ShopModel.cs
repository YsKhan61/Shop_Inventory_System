using SIS.Utilities;
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

        public bool TryGetItemTypeList(out TagSO[] list)
        {
            list = new TagSO[_data.itemTypes.Length];

            if (list.Length == 0)
            {
                Debug.LogError("Shop data's item type list is empty!");
                list = null;
                return false;
            }

            for (int i = 0; i < _data.itemTypes.Length; i++)
            {
                list[i] = _data.itemTypes[i];
            }

            return true;
        }
    }
}
