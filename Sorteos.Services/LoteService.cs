using Sorteos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services
{
    public class LoteService
    {

        public bool ValidateLote(string lote) {
            using (var context = new SorteosDbEntities()) {
                RaffleService raffleService = new RaffleService();
                var currentRaffle = raffleService.findCurrentRaffle();
                if (currentRaffle == null)
                    throw new Exception("Lo sentimos, pero al momento no existe ningún sorteo activo.");
                return (from lot in context.Lote
                        where
                            lot.Activo == true &&
                            lot.SorteoId == currentRaffle.Id &&
                            lot.Codigo == lote
                        select lot).ToList().Any();
            }
        }
    }
}
