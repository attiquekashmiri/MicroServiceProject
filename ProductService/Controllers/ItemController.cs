using Microsoft.AspNetCore.Mvc;
using CommonService.Common.ViewModels;
using ProductService.Services.ItemsManagement;
using Common.Helpers;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

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
        [Route("GetReport")]
        [HttpGet]
        public IActionResult GetReport()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var workbook = new XLWorkbook();
                var items = _itemsService.GetAllItems();

                var instockItems = items.Where(x => x.Quantity > 0 && x.isActive==true).ToList();
                var laststockItems = items.Where(x => x.Quantity == 1 && x.isActive == true).ToList();
                var outOfstockItems = items.Where(x => x.Quantity == 0 && x.isActive == true).ToList();
                var disabledOfstockItems = items.Where(x =>  x.isActive == false).ToList();

                //for Available Stock
                #region WorkSheetForAvailable Stock
                var Instocksheet = workbook.Worksheets.Add("Stock Available");
                Instocksheet.Cell("A1").Value = "SKU";
                Instocksheet.Cell("B1").Value = "UPC";
                Instocksheet.Cell("C1").Value = "Product Name";
                Instocksheet.Cell("D1").Value = "Price";
                Instocksheet.Cell("E1").Value = "Sale Price";
                Instocksheet.Cell("F1").Value = "Quantity";
                Instocksheet.Cell("G1").Value = "Category";
                Instocksheet.Cell("H1").Value = "Condition";
                int row = 2;
                foreach (var item in instockItems)
                {

                    Instocksheet.Cell("A"+row.ToString()).Value ="'"+ item.SKU;
                    Instocksheet.Cell("B"+row.ToString()).Value = "'" + item.UPC;
                    Instocksheet.Cell("C"+row.ToString()).Value = item.ProductName;
                    Instocksheet.Cell("D"+row.ToString()).Value = item.Price;
                    Instocksheet.Cell("E" + row.ToString()).Value = item.SalePrice;
                    Instocksheet.Cell("F" + row.ToString()).Value = item.Quantity;
                    Instocksheet.Cell("G" + row.ToString()).Value = item.Category;
                    Instocksheet.Cell("H" + row.ToString()).Value = item.Condition;
                    row++;
                }
                #endregion

                //for Last Stock
                #region WorkSheetForAvailable Stock
                var laststocksheet = workbook.Worksheets.Add("Last Available Stock");
                laststocksheet.Cell("A1").Value = "SKU";
                laststocksheet.Cell("B1").Value = "UPC";
                laststocksheet.Cell("C1").Value = "Product Name";
                laststocksheet.Cell("D1").Value = "Price";
                laststocksheet.Cell("E1").Value = "Sale Price";
                laststocksheet.Cell("F1").Value = "Quantity";
                laststocksheet.Cell("G1").Value = "Category";
                laststocksheet.Cell("H1").Value = "Condition";
                 row = 2;
                foreach (var item in laststockItems)
                {

                    laststocksheet.Cell("A" + row.ToString()).Value = "'" + item.SKU;
                    laststocksheet.Cell("B" + row.ToString()).Value = "'" + item.UPC;
                    laststocksheet.Cell("C" + row.ToString()).Value = item.ProductName;
                    laststocksheet.Cell("D" + row.ToString()).Value = item.Price;
                    laststocksheet.Cell("E" + row.ToString()).Value = item.SalePrice;
                    laststocksheet.Cell("F" + row.ToString()).Value = item.Quantity;
                    laststocksheet.Cell("G" + row.ToString()).Value = item.Category;
                    laststocksheet.Cell("H" + row.ToString()).Value = item.Condition;
                    row++;
                }
                #endregion


                //for Out Of Stock
                #region WorkSheetFor Out Of Stock Items
                var outOfstocksheet = workbook.Worksheets.Add("Out Of Stock");
                outOfstocksheet.Cell("A1").Value = "SKU";
                outOfstocksheet.Cell("B1").Value = "UPC";
                outOfstocksheet.Cell("C1").Value = "Product Name";
                outOfstocksheet.Cell("D1").Value = "Price";
                outOfstocksheet.Cell("E1").Value = "Sale Price";
                outOfstocksheet.Cell("F1").Value = "Quantity";
                outOfstocksheet.Cell("G1").Value = "Category";
                outOfstocksheet.Cell("H1").Value = "Condition";
                row = 2;
                foreach (var item in outOfstockItems)
                {

                    outOfstocksheet.Cell("A" + row.ToString()).Value = "'" + item.SKU;
                    outOfstocksheet.Cell("B" + row.ToString()).Value = "'" + item.UPC;
                    outOfstocksheet.Cell("C" + row.ToString()).Value = item.ProductName;
                    outOfstocksheet.Cell("D" + row.ToString()).Value = item.Price;
                    outOfstocksheet.Cell("E" + row.ToString()).Value = item.SalePrice;
                    outOfstocksheet.Cell("F" + row.ToString()).Value = item.Quantity;
                    outOfstocksheet.Cell("G" + row.ToString()).Value = item.Category;
                    outOfstocksheet.Cell("H" + row.ToString()).Value = item.Condition;
                    row++;
                }
                #endregion

                //for Not Active Stock
                #region WorkSheetFor Disabled Stock Items
                var disavkestocksheet = workbook.Worksheets.Add("Inventory Disabled");
                disavkestocksheet.Cell("A1").Value = "SKU";
                disavkestocksheet.Cell("B1").Value = "UPC";
                disavkestocksheet.Cell("C1").Value = "Product Name";
                disavkestocksheet.Cell("D1").Value = "Price";
                disavkestocksheet.Cell("E1").Value = "Sale Price";
                disavkestocksheet.Cell("F1").Value = "Quantity";
                disavkestocksheet.Cell("G1").Value = "Category";
                disavkestocksheet.Cell("H1").Value = "Condition";
                row = 2;
                foreach (var item in disabledOfstockItems)
                {

                    disavkestocksheet.Cell("A" + row.ToString()).Value = "'" + item.SKU;
                    disavkestocksheet.Cell("B" + row.ToString()).Value = "'" + item.UPC;
                    disavkestocksheet.Cell("C" + row.ToString()).Value = item.ProductName;
                    disavkestocksheet.Cell("D" + row.ToString()).Value = item.Price;
                    disavkestocksheet.Cell("E" + row.ToString()).Value = item.SalePrice;
                    disavkestocksheet.Cell("F" + row.ToString()).Value = item.Quantity;
                    disavkestocksheet.Cell("G" + row.ToString()).Value = item.Category;
                    disavkestocksheet.Cell("H" + row.ToString()).Value = item.Condition;
                    row++;
                }
                #endregion

                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                return this.File(
                    fileContents: stream.ToArray(),
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",

                    // By setting a file download name the framework will
                    // automatically add the attachment Content-Disposition header
                    fileDownloadName: "StockSheet.xlsx"
                );
            }
        }
    }
}
