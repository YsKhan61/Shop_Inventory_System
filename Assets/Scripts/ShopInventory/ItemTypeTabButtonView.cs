using SIS.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SIS.ShopInventory
{
    public class ItemTypeTabButtonView : MonoBehaviour
    {
        public Button Button;
        public TextMeshProUGUI labelText;
        public Image underline;

        TagSO itemTypeTag;
        public TagSO ItemTypeTag => itemTypeTag;
        

        public void SetTag(TagSO tag)
        {
            itemTypeTag = tag;
        }

        public void SetText(string text)
        {
            labelText.text = text;
        }

        public void ShowSelectedMarker()
        {
            underline.enabled = true;
        }

        public void HideSelectedMarker()
        {
            underline.enabled = false;
        }
    }
}
