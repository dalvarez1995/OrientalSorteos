using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorteos.Data;

namespace Sorteos.Services.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string FacebookAccessToken { get; set; }
        public RoleModel Role { get; set; }
        public List<PermissionModel> Permissions { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
