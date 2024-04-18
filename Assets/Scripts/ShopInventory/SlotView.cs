using SIS.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SIS.ShopInventory
{
    public class SlotView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField, FormerlySerializedAs("icon")]
        Image _icon;

        [SerializeField, FormerlySerializedAs("stackCount")]
        TextMeshProUGUI stackCountText;

        private IStorageController _storageController;
        TagSO _itemTag;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _storageController.ShowItemInfo(_itemTag);
            }
        }

        public void SetStorageController(IStorageController controller) => _storageController = controller;
        public void SetTag(TagSO tag) => _itemTag = tag;
        public void SetIcon(Sprite sprite) => _icon.sprite = sprite;
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
        public void SetStackCount(int count) => stackCountText.text = count.ToString();
        public void ShowStackCount() => stackCountText.gameObject.SetActive(true);
        public void HideStackCount() => stackCountText.gameObject.SetActive(false);

    }
}
