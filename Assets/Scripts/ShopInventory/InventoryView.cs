using UnityEngine;


namespace SIS.ShopInventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField]
        GameObject _itemTypeTabButtonContainer;
        public GameObject ItemTypeTabButtonContainer => _itemTypeTabButtonContainer;

        [SerializeField]
        GameObject _tabContainer;
        public GameObject TabContainer => _tabContainer;

        [SerializeField]
        ItemInfoView _itemInfoView;
        public ItemInfoView ItemInfoView => _itemInfoView;
    }
}
