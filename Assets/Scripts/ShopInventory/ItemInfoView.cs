using SIS.Utilities;
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

        [SerializeField]
        TextMeshProUGUI _notBuyableReasonText;

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
            if (_itemInfo.IsInInventory)
            {
                ShowSellButton();
                HideBuyButton();
                HideNotBuyableReasonText();
            }
            else
            {
                ShowBuyButton();

                if (_itemInfo.IsBuyable)
                {
                    EnableBuyButton();
                    HideNotBuyableReasonText();
                }
                else
                {
                    DisableBuyButton();
                    SetNotBuyableReasonText(_itemInfo.Message);
                    ShowNotBuyableReasonText();
                }

                HideSellButton();
            }
        }

        private void OnBuyButtonClicked()
        {
            EventService.Instance.OnBuyItem.InvokeEvent(_itemInfo.Tag, _itemInfo.QuantityToTrade);
            Hide();
        }

        private void OnSellButtonClicked()
        {
            EventService.Instance.OnSellItem.InvokeEvent(_itemInfo.Tag, _itemInfo.QuantityToTrade);
            Hide();
        }

        private void IncreaseItemQuantity()
        {
            if (_itemInfo.QuantityToTrade >= _itemInfo.MaxQuantityToTrade)
                return;

            _itemInfo.QuantityToTrade++;
            _qtyText.text = _itemInfo.QuantityToTrade.ToString();
        }

        private void DecreaseItemQuantity()
        {
            if (_itemInfo.QuantityToTrade <= 1)
                return;

            _itemInfo.QuantityToTrade--;
            _qtyText.text = _itemInfo.QuantityToTrade.ToString();
        }

        private void ShowSellButton() => _sellButton.gameObject.SetActive(true);

        private void HideSellButton() => _sellButton.gameObject.SetActive(false);

        private void ShowBuyButton() => _buyButton.gameObject.SetActive(true);

        private void EnableBuyButton() => _buyButton.interactable = true;
        private void DisableBuyButton() => _buyButton.interactable = false;

        private void HideBuyButton() => _buyButton.gameObject.SetActive(false);

        private void ShowNotBuyableReasonText() => _notBuyableReasonText.gameObject.SetActive(true);
        private void SetNotBuyableReasonText(string text) => _notBuyableReasonText.text = text;
        private void HideNotBuyableReasonText() => _notBuyableReasonText.gameObject.SetActive(false);
    
    }
}
