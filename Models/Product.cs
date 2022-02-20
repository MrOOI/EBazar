using System;

namespace EBazar.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private string ImagePath { get; set; }
        public ProductType productType { get; set; }
        public double Cost { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public string Quantity { get; set; }
    }
}
