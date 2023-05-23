using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web2BE.Models
{
    public class Orders
    {
        private int id;
        private int userId;
        private string orderNumber;
        private decimal orderTotal;
        private string orderStatus;

        public int Id { get => id; set => id = value; }
        public int UserId { get => userId; set => userId = value; }
        public string OrderNumber { get => orderNumber; set => orderNumber = value; }
        public decimal OrderTotal { get => orderTotal; set => orderTotal = value; }
        public string OrderStatus { get => orderStatus; set => orderStatus = value; }
    }
}
