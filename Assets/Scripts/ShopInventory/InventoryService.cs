using UnityEngine;

namespace SIS.ShopInventory
{
    public class InventoryService : MonoBehaviour
    {
        [SerializeField]
        InventoryDataSO _inventoryData;

        [SerializeField]
        InventoryView _view;

        private void Awake()
        {
            InventoryModel model = new(_inventoryData);
            InventoryController controller = new(model, _view);
            controller.Initialize();
        }
    }
}
