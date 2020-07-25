
using Sorteos.Services.Datatables.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Sorteos.Services.Datatables.Util
{
    public static class ExtensionMethods
    {

        /// <summary>
        /// Devuelve la coleccion de datos ordenada basada en los rangos de ordenamiento.
        /// </summary>
        /// <param name="source"> Coleccion de datos consultable a ordenar </param>
        /// <param name="orderRange">Rangos de ordenamiento</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderData<T>(this IQueryable<T> source,List<DTOrder> orderRange, List<DTColumn> columns)
        {

            var range = orderRange[0];
            var propertyName = columns[(range.column)].data;
            var methodName = range.dir == "asc" ? "OrderBy" : "OrderByDescending";
            return source.OrderBy(propertyName, methodName);
        }


        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName, string methodName)
        {
            Type type = typeof(T);
            ParameterExpression prm = Expression.Parameter(type, "tbl");
            PropertyInfo pi = type.GetProperty(propertyName);
            Expression property = Expression.Property(prm, pi);
            Type propertyType = pi.PropertyType;
            Type delegateType = typeof(Func<,>).MakeGenericType(type, propertyType);
            LambdaExpression lambda = Expression.Lambda(delegateType, property, prm);


            return (IOrderedQueryable<T>)typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2)
                .MakeGenericMethod(type, propertyType)
                .Invoke(null, new object[] { query, lambda });

        }

        public static bool PropertyExists(dynamic obj, string name)
        {
            if (obj == null) return false;
            if (obj is IDictionary<string, object> dict)
            {
                return dict.ContainsKey(name);
            }
            return obj.GetType().GetProperty(name) != null;
        }

    }
}
