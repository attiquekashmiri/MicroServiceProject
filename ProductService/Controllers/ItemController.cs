using Microsoft.AspNetCore.Mvc;
using CommonService.Common.ViewModels;
using ProductService.Services.ItemsManagement;
using Common.Helpers;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemsService _itemsService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public ItemController(IItemsService itemsService, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _itemsService = itemsService;
            ConstantHelpers.BaseUrl = environment.ContentRootPath;
        }

        // GET: api/<ItemController>
        [Route("GetAllItems")]
        [HttpGet]
        public IEnumerable<ItemVM> GetAllItems()
        {
            return _itemsService.GetAllItems();
        }

        // GET api/<ItemController>/5
        [Route("SearchItems")]
        [HttpGet]
        public IEnumerable<ItemVM> SearchItems(string text)
        {
            return _itemsService.SearchItems(text);
        }

        // POST api/<ItemController>
        [Route("AddItem")]
        [HttpPost]
        public bool AddItem(ItemVM item)
        {
          return  _itemsService.AddItem(item);
        }

        // PUT api/<ItemController>/5
        [Route("UpdateItem")]
        [HttpPut]
        public bool UpdateItem(ItemVM item)
        {
            return _itemsService.UpdateItem(item);
        }
        
        [Route("DisableItem")]
        [HttpPut]
        public bool DisableItem(long id)
        {
            return _itemsService.DisableItem(id);
        }
        [Route("EnableItem")]
        [HttpPut]
        public bool EnableItem(long id)
        {
            return _itemsService.DisableItem(id);
        }

        //// DELETE api/<ItemController>/5
        [Route("DeleteItem")]
        [HttpDelete]
        public bool DeleteItem(long id)
        {
            return _itemsService.DeleteItem(id);
        }
    }
}
