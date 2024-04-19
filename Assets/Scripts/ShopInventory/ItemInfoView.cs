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
        TextMeshProUGUI _weightText;

        [SerializeField]
        TextMeshProUGUI _rarityText;

        [SerializeField]
        TextMeshProUGUI _totatPrice;

        [SerializeField]
        TextMeshProUGUI _totalWeight;

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
            _priceText.text = _itemInfo.Price.ToString();
            _weightText.text = _itemInfo.Weight.ToString();
            _rarityText.text = _itemInfo.Rarity;
            _qtyText.text = _itemInfo.QuantityToTrade.ToString();

            UpdateTotalPriceAndWeightText();

            ConfigureTradeButton();
            ConfigureAddButton();
            DeactivateSubtractButton();

            Show();
        }

        public void Show() => _container.SetActive(true);

        public void Hide() => _container.SetActive(false);

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
                    ActivateBuyButton();
                    HideNotBuyableReasonText();
                }
                else
                {
                    DeactivateBuyButton();
                    SetNotBuyableReasonText(_itemInfo.Message);
                    ShowNotBuyableReasonText();
                }

                HideSellButton();
            }
        }

        private void IncreaseItemQuantity()
        {
            if (_itemInfo.QuantityToTrade == 1)
                ActivateSubtractButton();

            _itemInfo.QuantityToTrade++;
            _qtyText.text = _itemInfo.QuantityToTrade.ToString();
            UpdateTotalPriceAndWeightText();

            ConfigureAddButton();
        }

        private void DecreaseItemQuantity()
        {
            _itemInfo.QuantityToTrade--;
            _qtyText.text = _itemInfo.QuantityToTrade.ToString();
            UpdateTotalPriceAndWeightText();

            ActivateAddButton();

            if (_itemInfo.QuantityToTrade == 1)
            {
                DeactivateSubtractButton();
            }
        }

        private void ConfigureAddButton()
        {
            if (_itemInfo.IsInInventory)
            {
                // Item is in inventory
                if (!CanSellMoreItems())
                {
                    DeactivateAddButton();
                }
                else
                {
                    ActivateAddButton();
                }

            }
            else
            {
                // Item is in shop
                if (!CanBuyMoreItems())
                {
                    DeactivateAddButton();
                }
                else
                {
                    ActivateAddButton();
                }
            }
        }

        private bool CanBuyMoreItems()
        {
            int quantity = _itemInfo.QuantityToTrade + 1;
            InventoryData inventoryData = EventService.Instance.GetInventoryData.InvokeEvent();
            return inventoryData.CoinsCount >= (_itemInfo.Price * quantity) 
                && inventoryData.AvailableWeight >= (_itemInfo.Weight * quantity);
        }

        private bool CanSellMoreItems()
        {
            int quantityInInventory = EventService.Instance.GetItemQuantityFromInventory.InvokeEvent(_itemInfo.Tag);
            return quantityInInventory >= _itemInfo.QuantityToTrade + 1;
        }

        private void UpdateTotalPriceAndWeightText()
        {
            _totatPrice.text = (_itemInfo.Price * _itemInfo.QuantityToTrade).ToString();
            _totalWeight.text = (_itemInfo.Weight * _itemInfo.QuantityToTrade).ToString();
        }

        private void ShowSellButton() => _sellButton.gameObject.SetActive(true);
        private void HideSellButton() => _sellButton.gameObject.SetActive(false);
        private void ShowBuyButton() => _buyButton.gameObject.SetActive(true);

        private void ActivateBuyButton() => _buyButton.interactable = true;
        private void DeactivateBuyButton() => _buyButton.interactable = false;
        private void ActivateAddButton() => _addButton.interactable = true;
        private void DeactivateAddButton() => _addButton.interactable = false;
        private void ActivateSubtractButton() => _subtractButton.interactable = true;
        private void DeactivateSubtractButton() => _subtractButton.interactable = false;

        private void HideBuyButton() => _buyButton.gameObject.SetActive(false);

        private void ShowNotBuyableReasonText() => _notBuyableReasonText.gameObject.SetActive(true);
        private void SetNotBuyableReasonText(string text) => _notBuyableReasonText.text = text;
        private void HideNotBuyableReasonText() => _notBuyableReasonText.gameObject.SetActive(false);
    
    }
}
