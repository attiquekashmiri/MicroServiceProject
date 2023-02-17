using CommonService.Common.ViewModels;

namespace ProductService.Services.ItemsManagement
{
    public interface IItemsService
    {
        public bool AddItem(ItemVM item);
        public List<ItemVM> GetAllItems();
        public List<ItemVM> SearchItems(string query);
        public bool DeleteItem(long id);
        public bool UpdateItem(ItemVM item);
        public bool EnableItem(long id);
        public bool DisableItem(long id);
    }
}
