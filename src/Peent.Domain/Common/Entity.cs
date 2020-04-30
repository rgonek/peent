using System;

namespace Peent.Domain.Common
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; }

        protected Entity()
        {
        }

        protected Entity(T id)
            : this()
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<T> other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetRealType() != other.GetRealType())
                return false;

            if (Equals(Id, default(T)) || Equals(other.Id, default(T)))
                return false;

            return Equals(Id, other.Id);
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetRealType().ToString() + Id).GetHashCode();
        }

        private Type GetRealType()
        {
            var type = GetType();

            return type.ToString().Contains("Castle.Proxies.") ? type.BaseType : type;
        }
    }
}