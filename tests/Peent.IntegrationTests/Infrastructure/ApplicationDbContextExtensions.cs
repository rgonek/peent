using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Peent.Application;

namespace Peent.IntegrationTests.Infrastructure
{
    public static class ApplicationDbContextExtensions
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> source, IEnumerable<string> navigationPropertyPaths)
            where T : class
        {
            return navigationPropertyPaths.Aggregate(source, (query, path) => query.Include(path));
        }

        public static IEnumerable<string> GetIncludePaths<T>(this IApplicationDbContext db)
            where T : class
        {
            var entityType = db.GetModel().FindEntityType(typeof(T));
            var includedNavigations = new HashSet<INavigation>();
            var stack = new Stack<IEnumerator<INavigation>>();
            while (true)
            {
                var entityNavigations = new List<INavigation>();
                foreach (var navigation in entityType.GetNavigations())
                {
                    if (includedNavigations.Add(navigation))
                        entityNavigations.Add(navigation);
                }

                if (entityNavigations.Count == 0)
                {
                    if (stack.Count > 0)
                        yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
                }
                else
                {
                    foreach (var navigation in entityNavigations)
                    {
                        var inverseNavigation = navigation.FindInverse();
                        if (inverseNavigation != null)
                            includedNavigations.Add(inverseNavigation);
                    }

                    stack.Push(entityNavigations.GetEnumerator());
                }

                while (stack.Count > 0 && !stack.Peek().MoveNext())
                    stack.Pop();
                if (stack.Count == 0) break;
                entityType = stack.Peek().Current.GetTargetType();
            }
        }

        private static IModel GetModel(this IApplicationDbContext db)
        {
            return db.GetType().GetProperty("Model").GetValue(db, null) as IModel;
        }
    }
}