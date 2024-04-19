using SIS.Utilities;
using UnityEngine;

namespace SIS.ShopInventory
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "ScriptableObjects/InventoryDataSO", order = 1)]
    public class  InventoryDataSO : ScriptableObject
    {
        public ItemContainerSO ItemContainer;

        public int MaxWeight;

        public TabButtonView ItemTypeTabButtonPrefab;

        public SlotView SlotPrefab;
    }
}
