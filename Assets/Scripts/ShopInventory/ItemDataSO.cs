using SIS.Utilities;
using UnityEngine;

namespace SIS.ShopInventory
{
    [CreateAssetMenu(fileName = "ItemDataSO", menuName = "ScriptableObjects/ItemDataSO", order = 2)]
    public class ItemDataSO : ScriptableObject
    {
        [Tooltip("The tag is the name of the item")]
        public TagSO NameTag;

        public TagSO TypeTag;

        public Sprite IconSprite;

        public TagSO RarityTag;

        [TextArea(20, 20)]
        public string Description;

        public int BuyPrice;

        public int SellPrice;

        public int Weight;
    }
}
