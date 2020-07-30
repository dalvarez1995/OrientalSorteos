using Sorteos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services.Models
{
    public class PurchaseModel
    {
        public int Id { get; set; }
        public string Lote { get; set; }
        public int Qty { get; set; }
        public string Type { get; set; }
        public UserModel User { get; set; }
        public string InvoicePath { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public BrandModel Brand { get; set; }
        public RaffleModel Raffle { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
