﻿namespace EVEStandard.Models
{
    using global::System;
    using global::System.Linq;
    using global::System.Text;
    using Newtonsoft.Json;

    public class ModelBase<T> : IEquatable<T>
    {
        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class " + this.GetType().Name + " {\n");
            foreach (var propertyInfo in this.GetType().GetProperties())
            {
                sb.Append("  " + propertyInfo.Name + ": ").Append(propertyInfo.GetValue(this)).Append("\n");
            }
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        ///     Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public bool Equals(T other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            var firstType = this.GetType();
            // Handle type mismatch anyway you please:
            if (other.GetType() != firstType)
            {
                return false;
            }

            return !(from propertyInfo in firstType.GetProperties()
                where propertyInfo.CanRead
                let thisValue = propertyInfo.GetValue(this, null)
                let otherValue = propertyInfo.GetValue(other, null)
                where !Equals(thisValue, otherValue)
                select thisValue).Any();
        }

        /// <summary>
        ///     Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() && this.Equals((T) obj);
        }

        /// <summary>
        ///     Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                return this.GetType().GetProperties().Where(propertyInfo => propertyInfo.CanRead).Aggregate(41, (current, propertyInfo) => current * 59 + propertyInfo.GetValue(this).GetHashCode());
            }
        }
    }
}