using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace SIS.ShopInventory
{
    public class InventoryView : MonoBehaviour
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

        [SerializeField]
        Slider _weightSlider;

        [SerializeField]
        TextMeshProUGUI _weightText;

        [SerializeField]
        TextMeshProUGUI _coinsCountText;

        public void SetWeight(int currentWeight, int maxWeight)
        {
            _weightSlider.value = (float)currentWeight / maxWeight;
            _weightText.text = currentWeight.ToString() + "/" + maxWeight.ToString();
        }

        public void SetCoinsCount(int count)
        {
            _coinsCountText.text = count.ToString();
        }
    }
}
