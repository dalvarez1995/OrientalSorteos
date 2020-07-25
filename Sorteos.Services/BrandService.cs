using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorteos.Services.Models;
using Sorteos.Data;

namespace Sorteos.Services
{
    public class BrandService
    {
        public List<BrandModel> GetAllBrands()
        {
            using (var context = new SorteosDbEntities())
            {
                return context.Marca.Select(p => new BrandModel
                {
                    Id = p.Id,
                    Description = p.Descripcion
                }).ToList();
            }
        }
    }
}
