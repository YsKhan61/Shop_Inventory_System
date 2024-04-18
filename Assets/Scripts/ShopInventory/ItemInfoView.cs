using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace SIS.ShopInventory
{
    public class ItemInfoView : MonoBehaviour
    {
        [SerializeField]
        GameObject _container;

        [SerializeField]
        Image _iconImage;

        [SerializeField]
        TextMeshProUGUI _nameText;

        [SerializeField]
        TextMeshProUGUI _descriptionText;

        [SerializeField]
        TextMeshProUGUI _priceText;

        [SerializeField]
        TextMeshProUGUI _rarityText;

        [SerializeField]
        Button _buyButton;

        [SerializeField]
        Button _sellButton;

        [SerializeField]
        Button _cancelButton;

        [SerializeField]
        TextMeshProUGUI _qtyText;

        [SerializeField]
        Button _addButton;

        [SerializeField]
        Button _subtractButton;

        private ItemInfo _itemInfo;

        private void OnEnable()
        {
            _cancelButton.onClick.AddListener(Hide);
            _addButton.onClick.AddListener(IncreaseItemQuantity);
            _subtractButton.onClick.AddListener(DecreaseItemQuantity);
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
            _sellButton.onClick.AddListener(OnSellButtonClicked);
        }

        private void Start()
        {
            Hide();
        }

        private void OnDisable()
        {
            _cancelButton.onClick.RemoveAllListeners();
            _addButton.onClick.RemoveAllListeners();
            _subtractButton.onClick.RemoveAllListeners();
            _buyButton.onClick.RemoveAllListeners();
        }

        public void ShowItemInfo(ItemInfo itemInfo)
        {
            _itemInfo = itemInfo;

            _nameText.text = _itemInfo.Name;
            _iconImage.sprite = _itemInfo.IconSprite;
            _descriptionText.text = _itemInfo.Description;
            _priceText.text = _itemInfo.Price;
            _rarityText.text = _itemInfo.Rarity;
            _qtyText.text = _itemInfo.QuantityToTrade.ToString();

            ConfigureTradeButton();

            Show();
        }

        public void Show() => _container.SetActive(true);

        public void Hide() => _container.SetActive(false);

        private void ConfigureTradeButton()
        {
            if (_itemInfo.IsBought)
            {
                ShowSellButton();
                HideBuyButton();
            }
            else
            {
                ShowBuyButton();
                HideSellButton();
            }
        }

        private void OnBuyButtonClicked()
        {
            Debug.Log("Bought item " + _itemInfo.Name);
        }

        private void OnSellButtonClicked()
        {
            Debug.Log("Sold item " + _itemInfo.Name);
        }

        private void IncreaseItemQuantity()
        {
            _itemInfo.QuantityToTrade++;
            _itemInfo.QuantityToTrade = Mathf.Clamp(_itemInfo.QuantityToTrade, 0, _itemInfo.MaxQuantityToTrade);
            _qtyText.text = _itemInfo.QuantityToTrade.ToString();
        }

        private void DecreaseItemQuantity()
        {
            _itemInfo.QuantityToTrade--;
            _itemInfo.QuantityToTrade = Mathf.Clamp(_itemInfo.QuantityToTrade, 0, _itemInfo.MaxQuantityToTrade);
            _qtyText.text = _itemInfo.QuantityToTrade.ToString();
        }

        private void ShowSellButton() => _sellButton.gameObject.SetActive(true);

        private void HideSellButton() => _sellButton.gameObject.SetActive(false);

        private void ShowBuyButton() => _buyButton.gameObject.SetActive(true);

        private void HideBuyButton() => _buyButton.gameObject.SetActive(false);

    }
}
