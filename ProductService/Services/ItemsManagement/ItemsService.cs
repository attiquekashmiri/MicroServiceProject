using Common.Helpers;
using CommonService.Common.ViewModels;
using CommonService.Data;
using CommonService.Models;

namespace ProductService.Services.ItemsManagement
{
    public class ItemsService : IItemsService
    {
        private readonly AppDbContext _context;
        public ItemsService(AppDbContext context)
        {
            _context = context;
        }
        ConstantHelpers ConstantHelpers = new ConstantHelpers();
        #region Item CRUD Operation
        public bool AddItem(ItemVM item)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.SKU) && !_context.Items.Any(x=>x.SKU==item.SKU))
                {
                    _context.Items.Add(new Item()
                    {
                        ProductName = item.ProductName,
                        UPC = item.UPC,
                        SKU = item.SKU,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Category = item.Category,
                        Condition = item.Condition,
                        isActive = item.isActive,
                        SalePrice= item.SalePrice,
                        AddedDateTime = DateTime.Now,
                    });
                    Save();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                ConstantHelpers.ApiLogs("AddItem", e.ToString(), false);
                return false; 
            }
        }
        public List<ItemVM> GetAllItems()
        {
            try
            {
                var result= _context.Items.Select(item=> new ItemVM()
                {
                    Id= item.Id,
                    ProductName = item.ProductName,
                    UPC = item.UPC,
                    SKU = item.SKU,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Category = item.Category,
                    Condition = item.Condition,
                    isActive = item.isActive,
                    SalePrice = item.SalePrice
                }).ToList();
                return result != null && result.Count>0 ? result.OrderBy(x=> ConstantHelpers.PadNumbers(x.ProductName)).ToList()
                    : new List<ItemVM>();

            }
            catch (Exception e)
            {
                ConstantHelpers.ApiLogs("GetAllItems", e.ToString(), false);
                return new List<ItemVM>();
            }
        }
        public bool UpdateItem(ItemVM item)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.SKU) && item.Id>0)
                {
                    var resultitem = _context.Items.Where(x => x.Id == item.Id && x.isActive== true).FirstOrDefault();
                    if (resultitem != null)
                    {
                        resultitem.ProductName = item.ProductName;
                        resultitem.UPC = item.UPC;
                        resultitem.SKU = item.SKU;
                        resultitem.Price = item.Price;
                        resultitem.Quantity = item.Quantity;
                        resultitem.Category = item.Category;
                        resultitem.Condition = item.Condition;
                        resultitem.isActive = item.isActive;
                        resultitem.SalePrice = item.SalePrice;
                        resultitem.UpdateDateTime = DateTime.Now;
                        Save();
                        return true;
                    }
                }
                
                
                return false;
            }
            catch (Exception e)
            {
                ConstantHelpers.ApiLogs("UpdateItem", e.ToString(), false);
                return false;
            }
        }
        public bool DisableItem(long id)
        {
            try
            {
                if (id > 0)
                {
                    var item = _context.Items.Where(x => x.Id == id && x.isActive == true).FirstOrDefault();
                    if (item != null)
                    {
                        item.isActive = false;
                        Save();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                ConstantHelpers.ApiLogs("DeleteItem", e.ToString(), false);
                return false;
            }
        }
        public bool EnableItem(long id)
        {
            try
            {
                if (id > 0)
                {
                    var item = _context.Items.Where(x => x.Id == id && x.isActive == false).FirstOrDefault();
                    if (item != null)
                    {
                        item.isActive = true;
                        Save();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                ConstantHelpers.ApiLogs("DeleteItem", e.ToString(), false);
                return false;
            }
        }
        public bool DeleteItem(long id)
        {
            try
            {
                if (id>0)
                {
                    var item = _context.Items.Where(x => x.Id == id).FirstOrDefault();
                    if (item != null)
                    {
                        _context.Items.Remove(item);
                        Save();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                ConstantHelpers.ApiLogs("DeleteItem", e.ToString(), false);
                return false;
            }
        }
        public List<ItemVM> SearchItems(string query)
        {
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    var result = _context.Items.Where(x=>x.SKU==query || x.UPC ==query || x.ProductName.Contains(query)).Select(item => new ItemVM()
                    {
                        Id = item.Id,
                        ProductName = item.ProductName,
                        UPC = item.UPC,
                        SKU = item.SKU,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Category = item.Category,
                        Condition = item.Condition,
                        isActive = item.isActive,
                        SalePrice = item.SalePrice
                    }).ToList();
                    return result != null && result.Count > 0 ? result.OrderBy(x => ConstantHelpers.PadNumbers(x.ProductName)).ToList()
                    : new List<ItemVM>();
                }
                return new List<ItemVM>();

            }
            catch (Exception e)
            {
                ConstantHelpers.ApiLogs("SearchItems", e.ToString(), false);
                return new List<ItemVM>();
            }
        }
        #endregion
        #region private methods
        private void Save()
        {
            _context.SaveChanges();
        }
        #endregion

    }
}
