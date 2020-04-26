using System;
using System.Linq;
using EnsureThat;
using Peent.Application.Common;
using Peent.Domain.Common;
using Peent.Domain.Entities;

namespace Peent.Application.DynamicQuery.Sorts
{
    public class Sort
    {
        private readonly string _field;
        private readonly SortDirection _direction;

        public Sort(string field, SortDirection direction = SortDirection.Asc)
        {
            Ensure.That(field, nameof(field)).IsNotNullOrWhiteSpace();

            _field = field;
            _direction = direction;
        }

        public string GetPredicate()
        {
            return $"{_field} {_direction}";
        }

        public static Sort Default<T>()
        {
            var requestedType = typeof(T);

            if (typeof(IHaveAuditInfo).IsAssignableFrom(requestedType))
            {
                return new Sort(nameof(IHaveAuditInfo.Created) + "." + nameof(IHaveAuditInfo.Created.On));
            }
            if (typeof(IEntity<>).IsAssignableFromRawGeneric(requestedType))
            {
                return new Sort(nameof(IEntity<int>.Id));
            }

            var propertyName = requestedType.GetProperties().FirstOrDefault()?.Name;
            if (propertyName == null)
            {
                throw new InvalidOperationException($"Cannot determine default sort for {requestedType}.");
            }

            return new Sort(propertyName);
        }
    }
}
