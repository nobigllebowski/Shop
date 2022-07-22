using System;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Core.Domain
{
    /// <summary>
    ///     Базовая сущность
    /// </summary>
    public abstract class BaseEntity : IBaseEntity, IEquatable<BaseEntity>, ICloneable
    {
        /// <summary>
        ///     Id сущности
        /// </summary>
        public int Id { get; private set; }

        #region Переопределение методов Object

        public override bool Equals(object? obj)
        {
            if (obj?.GetType() != GetType())
            {
                return false;
            }

            var other = (BaseEntity)obj;
            return Equals(other);
        }
        public bool Equals([AllowNull] BaseEntity other)
        {
            return Id.Equals(other?.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static implicit operator string(BaseEntity entity)
        {
            return entity?.ToString() ?? string.Empty;
        }

        #endregion

        #region Cloneable
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public virtual BaseEntity? GetCopy()
        {
            return Clone() as BaseEntity;
        }

        #endregion

        #region Проверка IsNull?

        /// <summary>
        ///     Проверка является ли объект пустым или null
        /// </summary>
        /// <param name="obj"> Входящий объект </param>
        /// <param name="parameterName"> Название объекта, передается в Exception </param>
        /// <exception cref="ArgumentNullException"> Исключение, если объект пуст </exception>
        public static void IsNullOrEmpty<T>(T obj, [AllowNull] string? parameterName)
        {
            if (IsNullOrEmpty(obj))
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        ///     Проверка является ли объект пустым или null
        /// </summary>
        public static bool IsNullOrEmpty<T>(T obj)
        {
            switch (obj)
            {
                case null:
                case string str when string.IsNullOrEmpty(str):
                case ValueObject value when value.IsEmpty():
                    return true;
            }

            return false;
        }

        #endregion

        #region Операторы сравнения

        protected static bool EqualOperator(BaseEntity? left, BaseEntity? right)
        {
            if (left is null ^ right is null) return false;

            return left?.Equals(right!) != false;
        }

        protected static bool EqualOperator(BaseEntity? left, object? right)
        {
            if (left is null ^ right is null) return false;

            return left?.Equals(right) != false;
        }

        public static bool operator ==(BaseEntity? left, BaseEntity? right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator ==(BaseEntity? left, object? right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator ==(object? left, BaseEntity? right)
        {
            return EqualOperator(right, left);
        }


        protected static bool NotEqualOperator(BaseEntity? left, BaseEntity? right)
        {
            return !EqualOperator(left, right);
        }

        protected static bool NotEqualOperator(BaseEntity? left, object? right)
        {
            return !EqualOperator(left, right);
        }

        public static bool operator !=(BaseEntity? left, BaseEntity? right)
        {
            return NotEqualOperator(left, right);
        }

        public static bool operator !=(BaseEntity? left, object? right)
        {
            return NotEqualOperator(left, right);
        }

        public static bool operator !=(object? left, BaseEntity? right)
        {
            return NotEqualOperator(right, left);
        }

        #endregion
    }
}