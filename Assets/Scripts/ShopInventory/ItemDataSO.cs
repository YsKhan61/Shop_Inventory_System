using SIS.Utilities;
using UnityEngine;

namespace SIS.ShopInventory
{
    [CreateAssetMenu(fileName = "ItemDataSO", menuName = "ScriptableObjects/ItemDataSO", order = 2)]
    public class ItemDataSO : ScriptableObject
    {
        [Tooltip("The tag is the name of the item")]
        public TagSO tagName;

        public TagSO type;

        public Sprite icon;

        public TagSO rarity;

        [TextArea(20, 20)]
        public string description;

        public int buyPrice;

        public int sellPrice;

        public int weight;
    }
}
