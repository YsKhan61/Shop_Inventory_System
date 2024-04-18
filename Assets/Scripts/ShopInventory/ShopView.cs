using UnityEngine;
using UnityEngine.Serialization;


namespace SIS.ShopInventory
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("itemTypeTabButtonContainer")]
        GameObject _itemTypeTabButtonContainer;
        public GameObject ItemTypeTabButtonContainer => _itemTypeTabButtonContainer;

        [SerializeField, FormerlySerializedAs("tabContainer")]
        GameObject _tabContainer;
        public GameObject TabContainer => _tabContainer;

        [SerializeField]
        ItemInfoView _itemInfoView;
        public ItemInfoView ItemInfoView => _itemInfoView;
    }
}
