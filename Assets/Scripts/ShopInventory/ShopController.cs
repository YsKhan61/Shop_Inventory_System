using SIS.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.ShopInventory
{
    public class ShopController
    {
        ShopModel _model;
        ShopView _view;

        private List<ItemTypeTabButtonView> _itemTypeButtons = new();
        private ItemTypeTabButtonView _selectedItemTypeButton;

        public ShopController(ShopModel model, ShopView view)
        {
            _model = model;
            _view = view;
        }

        ~ShopController()
        {
            foreach (ItemTypeTabButtonView button in _itemTypeButtons)
            {
                button.Button.onClick.RemoveAllListeners();
            }
        }

        public void Initialize()
        {
            CreateItemTypeButtons();
        }

        private void CreateItemTypeButtons()
        {
            bool found = _model.TryGetItemTypeList(out TagSO[] list);
            if (!found)
            {
                return;
            }

            foreach (TagSO tag in list)
            {
                ItemTypeTabButtonView button = Object.Instantiate(_model.ItemTypeTabButtonPrefab, _view.ItemTypeTabButtonContainer.transform);
                button.gameObject.SetActive(true);
                button.SetTag(tag);
                button.SetText(tag.name);
                button.Button.onClick.AddListener(() => OnItemTypeTabButtonClicked(button));
                button.HideSelectedMarker();
                _itemTypeButtons.Add(button);
            }
        }

        private void OnItemTypeTabButtonClicked(ItemTypeTabButtonView button)
        {
            _selectedItemTypeButton?.HideSelectedMarker();
            _selectedItemTypeButton = button;
            _selectedItemTypeButton.ShowSelectedMarker();
        }
    }
}
