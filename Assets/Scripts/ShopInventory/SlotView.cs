using SIS.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SIS.ShopInventory
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField]
        Image icon;

        [SerializeField]
        TextMeshProUGUI stackCount;

        TagSO itemTag;

        public void SetTag(TagSO tag)
        {
            itemTag = tag;
        }

        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
