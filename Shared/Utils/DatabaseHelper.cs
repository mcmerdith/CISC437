using DOOR.EF.Data;
using DOOR.EF.Models;
using DOOR.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DOOR.Shared.Utils
{
    public class DatabaseHelper
    {
        public static async Task<List<V>> GetAllObjects<T, V>(DbSet<T> dataTable, Expression<Func<T, V>> objectProvider) where T : class
        {
            return await dataTable.Select(objectProvider).ToListAsync();
        }

        public static async Task<V?> GetObject<T, V>(DbSet<T> dataTable, Expression<Func<T, bool>> whereClause, Expression<Func<T, V>> objectProvider) where T : class
        {
            return await dataTable
                .Where(whereClause)
                .Select(objectProvider).FirstOrDefaultAsync();
        }

        public static async Task PostObject<T>(DOOROracleContext dbContext, DbSet<T> dataTable, Expression<Func<T, bool>> whereClause, T newObject) where T : class
        {
            T? currentObject = await dataTable.Where(whereClause).FirstOrDefaultAsync();

            if (currentObject == null)
            {
                dataTable.Add(newObject);
                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task PutObject<T>(DOOROracleContext dbContext, DbSet<T> dataTable, Expression<Func<T, bool>> whereClause, Action<T> mutator) where T : class
        {
            T? currentObject = await dataTable.Where(whereClause).FirstOrDefaultAsync();

            if (currentObject != null)
            {
                mutator(currentObject);
                dataTable.Update(currentObject);
                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteObject<T>(DOOROracleContext dbContext, DbSet<T> dataTable, Expression<Func<T, bool>> whereClause) where T : class
        {
            T? currentObject = await dataTable.Where(whereClause).FirstOrDefaultAsync();

            if (currentObject != null)
            {
                dataTable.Remove(currentObject);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
