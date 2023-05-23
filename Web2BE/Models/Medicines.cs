using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web2BE.Models
{
    public class Medicines
    {
        private int id;
        private string name;
        private string manufacturer;
        private decimal unitPrice;
        private decimal discount;
        private int quantity;
        private DateTime expDate;
        private string imgUrl;
        private int status;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        public decimal UnitPrice { get => unitPrice; set => unitPrice = value; }
        public decimal Discount { get => discount; set => discount = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public DateTime ExpDate { get => expDate; set => expDate = value; }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
        public int Status { get => status; set => status = value; }
    }
}
