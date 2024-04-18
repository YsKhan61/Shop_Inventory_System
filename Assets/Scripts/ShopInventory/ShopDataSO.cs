using SIS.Utilities;
using UnityEngine;

namespace SIS.ShopInventory
{
    [CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObjects/ShopDataSO", order = 1)]
    public class ShopDataSO : ScriptableObject
    {
        public TagSO[] itemTypes;
        public ItemDataSO[] items;

        public ItemTypeTabButtonView ItemTypeTabButtonPrefab;
    }
}
