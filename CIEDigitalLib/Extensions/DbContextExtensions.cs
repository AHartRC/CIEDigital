using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using CIEDigitalLib.Models.Context;

namespace CIEDigitalLib.Extensions
{
    public static class DbContextExtensions
    {
        public static ObjectContext ToObjectContext(this DbContext db)
        {
            return (db as IObjectContextAdapter).ObjectContext;
        }

        public static IEnumerable<T> Get<T>(this CIEDigitalEntities db) where T : class
        {
            return GetQuery<T>(db);
        }

        public static IQueryable<TEntity> GetQuery<TEntity>(this DbContext db, bool includeReferences = false) where TEntity : class
        {
            try
            {
                if (db == null)
                {
                    return null;
                }

                var key = typeof(TEntity).Name;
                var metaWorkspace = db.ToObjectContext().MetadataWorkspace;
                var workspaceItems = metaWorkspace.GetItems<EntityType>(DataSpace.OSpace);
                var workspaceItem = workspaceItems.First(f => f.FullName.Contains(key));
                var navProperties = workspaceItem.NavigationProperties;

                return !includeReferences
                        ? db.Set<TEntity>()
                        : navProperties.Aggregate((IQueryable<TEntity>)db.Set<TEntity>(), (current, navProperty) => current.Include(navProperty.Name));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid Entity Type supplied for Lookup", ex);
            }
        }
    }
}
