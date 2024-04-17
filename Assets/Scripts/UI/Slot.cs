using SIS.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace SIS.UI
{
    public struct SerializableGuid
    { 
        public static SerializableGuid Empty => new SerializableGuid();
    }


    public class Slot : VisualElement
    {
        public Image Icon;
        public Label StackLabel;
        public int Index => parent.IndexOf(this);
        public SerializableGuid ItemId { get; private set; } = SerializableGuid.Empty;
        public Sprite BaseSprite;

        public Slot()
        {
            Icon = this.CreateChild<Image>("slotIcon");
            StackLabel = this.CreateChild("slotFrame").CreateChild<Label>("stackCount");
        }

        public void Set(SerializableGuid id, Sprite sprite, int qty = 0)
        {
            ItemId = id;
            BaseSprite = sprite;
            Icon.image = BaseSprite != null ? sprite.texture : null;
            StackLabel.text = qty > 1 ? qty.ToString() : string.Empty;
            StackLabel.visible = qty > 1;
        }

        public void Remove()
        {
            ItemId = SerializableGuid.Empty;
            Icon.image = null;
        }
    }
}