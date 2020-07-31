using Sorteos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services
{
    public class WinnerService
    {
        public void Insert(int userId,int raffleId) {
            using (var context = new SorteosDbEntities()) {
                var existigWinner = context.Ganador.FirstOrDefault(g => g.UsuarioId == userId && g.SorteoId == raffleId);
                if (existigWinner != null)
                    throw new Exception($"El participante {existigWinner.Usuario.Nombre + " " + existigWinner.Usuario.Apellido} ya se encuentra registrado como ganador para este sorteo");
                var newWinner = new Ganador {
                    SorteoId = raffleId,
                    UsuarioId = userId,
                    FechaCreacion = DateTime.UtcNow.AddHours(-5)
                };
                context.Ganador.Add(newWinner);
                context.SaveChanges();
            }

        }

        
    }
}
