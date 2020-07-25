using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services.Models
{
    public class PermissionModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string PageUrl { get; set; }
        public string Group { get; set; }
    }
}
