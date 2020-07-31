using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services.Models
{
    public class ParticipantModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int ChancesNumber { get; set; }
    }
}
