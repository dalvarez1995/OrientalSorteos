using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services.Models
{
    public class BatchModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public RaffleModel Ruffle { get; set; }
        public BrandModel Brand { get; set; }
    }
}
