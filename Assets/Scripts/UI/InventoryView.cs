using SIS.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace SIS.UI
{
    public class InventoryView : StorageView
    {
        [SerializeField]
        string _panelName = "Inventory";

        public override IEnumerator InitializeView(int size = 20)
        {
            Slots = new Slot[size];
            _root = _document.rootVisualElement;
            _root.Clear();

            _root.styleSheets.Add(_styleSheet);

            _container = _root.CreateChild("container");

            var inventory = _container.CreateChild("inventory");
            inventory.CreateChild("inventoryFrame");
            inventory.CreateChild("inventoryHeader").Add(new Label(_panelName));

            var slotContainer = inventory.CreateChild("slotsContainer");
            for (int i = 0; i < size; i++)
            {
                var slot = slotContainer.CreateChild<Slot>("slot");
                Slots[i] = slot;
            }
            _ghostIcon = _container.CreateChild("ghostIcon");
            _ghostIcon.BringToFront();

            yield return null;
        }
    }
}
