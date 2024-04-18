using SIS.Utilities;
using UnityEngine;

namespace SIS.ShopInventory
{
    [CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObjects/ShopDataSO", order = 1)]
    public class ShopDataSO : ScriptableObject
    {
        public ItemContainerSO ItemContainer;

        public ItemTypeTabButtonView ItemTypeTabButtonPrefab;

        public SlotView SlotPrefab;
    }
}
