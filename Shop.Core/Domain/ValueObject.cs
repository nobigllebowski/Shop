using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shop.Core.Domain
{
    /// <summary>
    /// Базовый класс, для объектов значений
    /// </summary>
    // Learn more: https://docs.microsoft.com/ru-ru/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
    public abstract class ValueObject : IEquatable<ValueObject>, ICloneable
    {
        /// <summary>
        /// Определение перебора полей, для сравнения
        /// </summary>
        protected abstract IEnumerable<object> GetAtomicValues();

        #region object

        public override bool Equals(object? obj)
        {
            if (obj?.GetType() != GetType())
            {
                return false;
            }

            var other = obj as ValueObject;
            return Equals(other);
        }

        public virtual bool Equals(ValueObject? obj)
        {
            if (obj == null)
            {
                return false;
            }

            using var thisValues = GetAtomicValues().GetEnumerator();
            using var otherValues = obj.GetAtomicValues().GetEnumerator();

            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (thisValues.Current is null ^ otherValues.Current is null) return false;

                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current)) return false;
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public override string ToString()
        {
            return string.Join("; ", GetAtomicValues());
        }

        public static implicit operator string(ValueObject fullName)
        {
            return fullName.ToString();
        }

        #endregion

        #region Cloneable

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public virtual ValueObject? GetCopy()
        {
            return Clone() as ValueObject;
        }

        #endregion

        /// <summary>
        /// Проверка объекта на пустоту его значений
        /// </summary>
        public bool IsEmpty()
        {
            var type = GetType();
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            return fields.Select(field => field.GetValue(this))
                .All(value => value == null);
        }

        #region Операторы сравнения

        protected static bool EqualOperator(ValueObject? left, ValueObject? right)
        {
            if (left is null ^ right is null) return false;

            return left?.Equals(right!) != false;
        }

        protected static bool EqualOperator(ValueObject? left, object? right)
        {
            if (left is null ^ right is null) return false;

            return left?.Equals(right) != false;
        }

        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator ==(ValueObject? left, object? right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator ==(object? left, ValueObject? right)
        {
            return EqualOperator(right, left);
        }


        protected static bool NotEqualOperator(ValueObject? left, ValueObject? right)
        {
            return !EqualOperator(left, right);
        }

        protected static bool NotEqualOperator(ValueObject? left, object? right)
        {
            return !EqualOperator(left, right);
        }

        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return NotEqualOperator(left, right);
        }

        public static bool operator !=(ValueObject? left, object? right)
        {
            return NotEqualOperator(left, right);
        }

        public static bool operator !=(object? left, ValueObject? right)
        {
            return NotEqualOperator(right, left);
        }

        #endregion
    }
}