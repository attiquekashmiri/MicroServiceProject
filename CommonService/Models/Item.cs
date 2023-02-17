using System.ComponentModel.DataAnnotations;

namespace CommonService.Models
{
    public class Item
    {
        [Key]
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string UPC { get; set; }
        public string SKU { get; set; }
        public string Category { get; set; }
        public string Condition { get; set; }
        public bool isActive { get; set; } = true;
        public DateTime AddedDateTime { get; set; } = DateTime.Now;
        public DateTime? UpdateDateTime { get; set; } = DateTime.Now;
        public double? Price { get; set; }
        public double? SalePrice { get; set; }
        public double? Quantity { get; set; }
    }
}
