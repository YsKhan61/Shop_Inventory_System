using SIS.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.ShopInventory
{
    [CreateAssetMenu(fileName = "ItemContainer", menuName = "ScriptableObjects/ItemContainerSO", order = 1)]
    public class ItemContainerSO : ScriptableObject
    {
        public TagSO[] ItemTypes;
        public ItemDataSO[] Items;

        public bool TryGetItemByType(TagSO typeTag, out List<ItemDataSO> items)
        {
            if (Items.Length <= 0)
            {
                items = null;
                return false;
            }
                
            items = new List<ItemDataSO>();

            for (int i = 0, count = Items.Length; i < count; i++)
            {
                if (Items[i].TypeTag == typeTag)
                {
                    items.Add(Items[i]);
                }
            }

            return items.Count > 0;
        }
    }
}
