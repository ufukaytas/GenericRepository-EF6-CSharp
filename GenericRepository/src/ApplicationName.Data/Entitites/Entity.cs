using ApplicationName.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationName.Data.Entitites
{
    public abstract class Entity : Entity<int>
    {

    }

    public abstract class Entity<TId> : IEntity<TId>
    {
        public virtual TId Id { get; protected set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TId>);
        }

        private static bool IsTransient(Entity<TId> obj)
        {
            return obj != null && Equals(obj.Id, default(TId));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(Entity<TId> other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        // TODO Burası editlenecek! Hatalı olmalı
        public override int GetHashCode()
        {
            if (Equals(Id, default(TId)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }

        /// <summary>
        /// Equality operator so we can have == semantics
        /// </summary>
        public static bool operator ==(Entity<TId> x, Entity<TId> y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// Inequality operator so we can have != semantics
        /// </summary>
        public static bool operator !=(Entity<TId> x, Entity<TId> y)
        {
            return !(x == y);
        }
    }
}
