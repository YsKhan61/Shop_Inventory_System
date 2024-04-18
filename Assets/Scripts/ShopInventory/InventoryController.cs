using SIS.Utilities;

namespace SIS.ShopInventory
{
    public class InventoryController : StorageController
    {
        private InventoryModel _model;
        private InventoryView _view;

        public InventoryController(InventoryModel model, InventoryView view)
        {
            _model = model;
            _view = view;
        }

        public override void ShowItemInfo(TagSO itemTag)
        {
            
        }

        protected override void CreateItemTabs()
        {
            foreach (TagSO tag in _model.ItemTypes)
            {
                ItemTypeTabButtonView buttonView =
                    CreateItemTabButton(
                        _model.ItemTypeTabButtonPrefab,
                        _view.ItemTypeTabButtonContainer.transform,
                        tag);

                ItemTab tab = CreateItemTab(tag, buttonView);

                buttonView.Button.onClick.AddListener(() => OnItemTypeTabButtonClicked(tab));
                tab.Hide();
                _tabs.Add(tab);
            }
        }
    }
}
