using Sorteos.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorteos.Data;

namespace Sorteos.Services
{
    public class RaffleService
    {

        public void Insert(RaffleModel newRaffle)
        {
            using (var context = new SorteosDbEntities())
            {
                //if (newRaffle.BeginDate < DateTime.Parse(DateTime.UtcNow.AddHours(-5).ToShortDateString().Trim() + " 00:00:00"))
                //    throw new Exception("Fecha de Inicio del sorteo no debe ser menor a la Fecha Actual");
                if (newRaffle.EndDate <= newRaffle.BeginDate)
                    throw new Exception("Fecha de Finalización del sorteo no debe ser menor o igual a la Fecha de Inicio");

                var dbRaffleList = context.Sorteo.ToList();

                if (newRaffle.Active)
                {
                    dbRaffleList.ForEach(raffle =>
                    {
                        raffle.Activo = false;
                    });
                };



                context.Sorteo.Add(new Sorteo {
                    Descripcion = newRaffle.Description,
                    HtmlCode = newRaffle.HtmlCode,
                    FechaInicio = newRaffle.BeginDate,
                    FechaFin = newRaffle.EndDate,
                    Activo = newRaffle.Active,
                    FechaCreacion = DateTime.UtcNow.AddHours(-5)
                });

                context.SaveChanges();
            }
        }

        public void Update(RaffleModel modifiedRaffle)
        {
            using (var context = new SorteosDbEntities())
            {

                //if (modifiedRaffle.BeginDate < DateTime.Parse(DateTime.UtcNow.AddHours(-5).ToShortDateString().Trim() + " 00:00:00"))
                //    throw new Exception("Fecha de Inicio del sorteo no debe ser menor a la Fecha Actual");
                if (modifiedRaffle.EndDate <= modifiedRaffle.BeginDate)
                    throw new Exception("Fecha de Finalización del sorteo no debe ser menor o igual a la Fecha de Inicio");

                var dbRaffleList = context.Sorteo.ToList();

                if (modifiedRaffle.Active)
                {
                    dbRaffleList.ForEach(raffle =>
                    {
                        raffle.Activo = false;
                    });
                }

                var dbRaffle = dbRaffleList.Where(s => s.Id == modifiedRaffle.Id).FirstOrDefault();

                if (dbRaffle == null)
                    throw new Exception("El sorteo a modificar no existe.");

                dbRaffle.Descripcion = modifiedRaffle.Description;
                dbRaffle.FechaInicio = modifiedRaffle.BeginDate;
                dbRaffle.FechaFin = modifiedRaffle.EndDate;
                dbRaffle.HtmlCode = modifiedRaffle.HtmlCode;
                dbRaffle.Activo = modifiedRaffle.Active;
                dbRaffle.FechaModificacion = DateTime.UtcNow.AddHours(-5);

                context.SaveChanges();
            }
        }

        public RaffleModel GetRaffleById(int id) {
            using (var context = new SorteosDbEntities()) {
                return context.Sorteo.Where(s => s.Id == id).Select(s => new RaffleModel {
                    Description = s.Descripcion,
                    BeginDate = s.FechaInicio,
                    EndDate = s.FechaFin,
                    Active = s.Activo,
                    HtmlCode = s.HtmlCode,
                    CreatedAt = s.FechaCreacion
                }).FirstOrDefault();
            }
        }

        public RaffleModel findCurrentRaffle() {
            using (var context = new SorteosDbEntities())
            {
                var currentDateTime = DateTime.UtcNow.AddHours(-5);
                return (from sort in context.Sorteo
                        where
                            sort.Activo == true &&
                            DateTime.Compare(sort.FechaInicio, currentDateTime) <= 0 &&
                            DateTime.Compare(sort.FechaFin, currentDateTime) >= 0
                        select sort
                        ).Select( s=> new RaffleModel { 
                            Id = s.Id,
                            Description = s.Descripcion,
                            BeginDate = s.FechaInicio,
                            EndDate = s.FechaFin,
                            HtmlCode =s.HtmlCode,
                            CreatedAt = s.FechaCreacion,
                            Active = s.Activo
                        }).FirstOrDefault(); 
            }
        }


        public List<RaffleModel> GetAllRaffles() {
            using (var context = new SorteosDbEntities())
            {
                return context.Sorteo.Select(s => new RaffleModel
                {
                    Id = s.Id,
                    Description = s.Descripcion,
                    BeginDate = s.FechaInicio,
                    EndDate = s.FechaFin,
                    CreatedAt = s.FechaCreacion,
                    Active = s.Activo
                }).ToList();
            }
        }


    }
}
