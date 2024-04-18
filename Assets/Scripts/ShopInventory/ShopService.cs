using UnityEngine;

namespace SIS.ShopInventory
{
    public class ShopService : MonoBehaviour
    {
        [SerializeField]
        ShopDataSO _shopData;

        [SerializeField]
        ShopView _view;

        private void Awake()
        {
            ShopModel model = new(_shopData);
            ShopController controller = new (model, _view);
            controller.Initialize();
        }
    }
}
