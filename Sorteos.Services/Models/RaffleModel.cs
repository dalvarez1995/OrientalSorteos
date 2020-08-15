using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services.Models
{
    public class RaffleModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string HtmlCode { get; set; }
        public int ExtraPoints { get; set; }
        public string SiteUrl { get; set; }
        public int? SiteId { get; set; }
        public int WinnersNumber { get; set; }
        public bool Finished { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
