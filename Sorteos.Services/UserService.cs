using System;
using System.Linq;
using Sorteos.Data;
using Sorteos.Services.Models;

namespace Sorteos.Services
{
    public class UserService
    {

        public UserModel GetUserById(int userId) {
            using (var context = new SorteosDbEntities())
            {
                var userModel = context.Usuario.Where(user => user.Id == userId).Select(user => new UserModel
                {
                    FullName = user.Nombre + " " + user.Apellido,
                    Email = user.Email,
                    Role = new RoleModel {
                        Id = user.Id,
                        Description = user.Perfil.Descripcion,
                        Special = user.Perfil.PermisosEspeciales
                    },
                    Permissions = context.Permiso.Where(perm => user.Perfil.PerfilPermiso
                                    .Select(pp => pp.PermisoId).Contains(perm.Id)).ToList()
                                        .Select(per => new PermissionModel {
                                            Id = per.Id,
                                            Description = per.Descripcion,
                                            Code = per.Codigo,
                                            PageUrl = per.PageUrl,
                                            Group = per.Categoria
                                        }).ToList()
                }).FirstOrDefault();

                return userModel;
            }
        }

        public void Update(UserModel user)
        {
            using (var context = new SorteosDbEntities())
            {
                var userFound = context.Usuario.Where(u => u.Email == user.Email).FirstOrDefault();
                if (userFound == null)
                    throw new Exception("Usuario a actualizar no existe");
                userFound.FacebookAccessToken = user.FacebookAccessToken;
                context.SaveChanges();
            }
        }
        public UserModel GetUserByEmail(string email)
        {
            using (var context = new SorteosDbEntities())
            {
                var userModel = context.Usuario.Where(user => user.Email == email).Select(user => new UserModel
                {
                    Id = user.Id,
                    FullName = user.Nombre + " " + user.Apellido,
                    Email = user.Email,
                    Role = new RoleModel
                    {
                        Id = user.Id,
                        Description = user.Perfil.Descripcion,
                        Special = user.Perfil.PermisosEspeciales
                    },
                    Permissions = context.Permiso.Where(perm => user.Perfil.PerfilPermiso
                                    .Select(pp => pp.PermisoId).Contains(perm.Id)).ToList()
                                        .Select(per => new PermissionModel
                                        {
                                            Id = per.Id,
                                            Description = per.Descripcion,
                                            Code = per.Codigo,
                                            PageUrl = per.PageUrl,
                                            Group = per.Categoria
                                        }).ToList()
                }).FirstOrDefault();

                return userModel;
            }
        }

    }
}
