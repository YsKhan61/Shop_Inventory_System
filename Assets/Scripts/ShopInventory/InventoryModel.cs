using SIS.Utilities;

namespace SIS.ShopInventory
{
    public class InventoryModel
    {
        private InventoryDataSO _data;

        public InventoryModel(InventoryDataSO data) => _data = data;

        public TagSO[] ItemTypes => _data.ItemTypes;

        public ItemTypeTabButtonView ItemTypeTabButtonPrefab
            => _data.ItemTypeTabButtonPrefab;
    }

}
