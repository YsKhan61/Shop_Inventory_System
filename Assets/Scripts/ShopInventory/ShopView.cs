using UnityEngine;


namespace SIS.ShopInventory
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField]
        GameObject _itemTypeTabButtonContainer;
        public GameObject TabButtonContainer => _itemTypeTabButtonContainer;

        [SerializeField]
        GameObject _tabContainer;
        public GameObject TabContainer => _tabContainer;

        [SerializeField]
        ItemInfoView _itemInfoView;
        public ItemInfoView ItemInfoView => _itemInfoView;
    }
}
