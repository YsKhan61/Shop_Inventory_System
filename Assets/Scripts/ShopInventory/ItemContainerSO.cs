using SIS.Utilities;
using System.Collections.Generic;
using System.Threading;
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
                Debug.LogError("No items found!");
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

        public bool TryGetItemByTag(TagSO tag, out ItemDataSO item)
        {
            item = null;

            if (Items.Length <= 0)
            {
                Debug.LogError("No items found!");
                return false;
            }

            for (int i = 0, length = Items.Length; i < length; i++)
            {
                if (Items[i].NameTag == tag)
                {
                    item = Items[i];
                }
            }

            return item != null;
        }
    }
}
