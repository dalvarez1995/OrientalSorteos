using Sorteos.Data;
using Sorteos.Services.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services
{
   public class PurchaseService
   {
        public void Insert(string lote,string tipo,int cantidad,int cityId, int brandId, int stateId, int userId, string invoicePath) {

            using (var context = new SorteosDbEntities())
            {
                RaffleService raffleService = new RaffleService();
                var currentRaffle = raffleService.findCurrentRaffle();
                if (currentRaffle == null)
                    throw new Exception("No exiten sorteos activos. Inténtelo más tarde");

                // validations
                if (cantidad <= 0)
                    throw new Exception("Cantidad no válida");
                if(string.IsNullOrEmpty(lote))
                    throw new Exception("Lote del producto no válido.");
                var city = context.Ciudad.Where(m => m.Id == cityId).FirstOrDefault();
                if (city == null)
                    throw new Exception("La ciudad seleccionada no existe.");
                var brand = context.Marca.Where(m => m.Id == brandId).FirstOrDefault();
                if (brand == null)
                    throw new Exception("La marca seleccionada no existe.");
                var state = context.Provincia.Where(p => p.Id == stateId).FirstOrDefault();
                if (state == null)
                    throw new Exception("La provincia seleccionada no existe");

                context.Compra.Add(new Compra
                {
                    Lote = lote,
                    CiudadId = cityId,
                    Cantidad = cantidad,
                    Tipo = tipo,
                    MarcaId = brand.Id,
                    ProvinciaId = state.Id,
                    SorteoId = currentRaffle.Id,
                    UsuarioId = userId,
                    FacturaPath = invoicePath,
                    Estado = (int)PurchaseStatus.Pendiente,
                    FechaCreacion = DateTime.UtcNow.AddHours(-5)
                });

                context.SaveChanges();
            }
        }

        public int GetCustomerPurchasesCount(string customerId, int raffleId)
        {
            using (var context = new SorteosDbEntities())
            {
                return (
                    from c in context.Compra  
                    join u in context.Usuario
                    on
                        c.UsuarioId equals u.Id
                    where
                        u.Email == customerId &&
                        c.SorteoId == raffleId
                    select c
                ).Count();
            }
        }

        public int GetPendingPurchasesCountByRaffle(int raffleId) {
            using (var context = new SorteosDbEntities())
            {
                return (
                    from c in context.Compra
                    where
                        c.SorteoId == raffleId &&
                        c.Estado == (int)PurchaseStatus.Pendiente

                    select c
                ).Count();
            }
        }

        public PurchaseModel GetNextPendingByRaffleId(int raffleId)
        {
            using (var context = new SorteosDbEntities())
            {
                return (
                    from c in context.Compra
                    where
                        c.SorteoId == raffleId &&
                        c.Estado == (int)PurchaseStatus.Pendiente
                    orderby c.FechaCreacion descending
                    select c
                ).Select(c => new PurchaseModel
                {
                    Id = c.Id,
                    Brand = new BrandModel
                    {
                        Id = c.Marca.Id,
                        Description = c.Marca.Descripcion
                    },
                    User = new UserModel { 
                        Id = c.Usuario.Id,
                        FullName = c.Usuario.Nombre + " " + c.Usuario.Apellido,
                        Email = c.Usuario.Email
                    },
                    City = c.Ciudad.Nombre,
                    State = c.Provincia.Nombre,
                    Qty = c.Cantidad.Value,
                    InvoicePath = c.FacturaPath,
                    Raffle = new RaffleModel
                    {
                        Id = c.Sorteo.Id,
                        Description = c.Sorteo.Descripcion
                    },
                    Lote = c.Lote,
                    Type = c.Tipo,
                    CreatedAt = c.FechaCreacion
                }).FirstOrDefault();

            }
        }

        public void UpdateStatus(int purchaseId, PurchaseStatus purchaseStatus) {
            using (var context = new SorteosDbEntities())
            {
                var purchase = context.Compra.FirstOrDefault(c => c.Id == purchaseId);
                if (purchase == null)
                    throw new Exception("La compra ya no existe o no es válida.");

                purchase.Estado = (int)purchaseStatus;
                context.SaveChanges();

            }
        }



   }
}
