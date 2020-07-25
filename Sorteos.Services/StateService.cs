using Sorteos.Data;
using Sorteos.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sorteos.Services
{
    public class StateService
    {
        public List<StateModel> GetAllStates() {
            using (var context = new SorteosDbEntities()) {
                return context.Provincia.Select(p => new StateModel
                {
                    Id = p.Id,
                    Name = p.Nombre
                }).ToList();
            }
        }
    }
}
