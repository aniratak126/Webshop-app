using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web2BE.Models
{
    public class Cart
    {
        private int id;
        private int userId;
        private decimal unitPrice;
        private decimal discount;
        private int quantity;
        private decimal totalPrice;
        private int medicineId;

        public int Id { get => id; set => id = value; }
        public int UserId { get => userId; set => userId = value; }
        public decimal UnitPrice { get => unitPrice; set => unitPrice = value; }
        public decimal Discount { get => discount; set => discount = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public decimal TotalPrice { get => totalPrice; set => totalPrice = value; }
        public int MedicineId { get => medicineId; set => medicineId = value; }
    }
}
