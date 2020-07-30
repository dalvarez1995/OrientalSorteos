using Sorteos.Data;
using System.Collections.Generic;
using Sorteos.Services.Models;
using System.Linq;

namespace Sorteos.Services
{
    public class CityService
    {
        public List<CityModel> GetCitiesByState(int idState) {
            using (var context = new SorteosDbEntities()) {
                return context.Ciudad.Where( c => c.ProvinciaId == idState).Select( c => new CityModel{ 
                    Id = c.Id,
                    Name = c.Nombre
                }).OrderBy( c => c.Name).ToList();
            }
        }
    }
}
