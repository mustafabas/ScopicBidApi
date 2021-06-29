using System;

namespace BidApp.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public  decimal StartPrice { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }
    }
}
