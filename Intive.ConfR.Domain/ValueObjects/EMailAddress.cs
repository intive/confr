using Intive.ConfR.Domain.Exceptions;
using Intive.ConfR.Domain.Infrastructure;
using System;
using System.Collections.Generic;

namespace Intive.ConfR.Domain.ValueObjects
{
    public class EMailAddress : ValueObject
    {
        private EMailAddress()
        { }

        public static EMailAddress For(string addressString)
        {
            var address = new EMailAddress();

            try
            {
                var index = addressString.IndexOf("@", StringComparison.Ordinal);
                address.Name = addressString.Substring(0, index);
                address.Domain = addressString.Substring(index + 1);
                address.Value = addressString;
            }

            catch (Exception exception)
            {
                throw new EMailAddressInvalidException(addressString, exception);
            }

            return address;
        }

        public string Name { get; private set; }

        public string Domain { get; private set; }
        public string Value { get; private set; }

        public static implicit operator string(EMailAddress address)
        {
            return address.ToString();
        }

        public static explicit operator EMailAddress(string addressString)
        {
            return For(addressString);
        }

        public override string ToString()
        {
            return $"{Name}@{Domain}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Domain;
            yield return Name;
        }

        public static bool operator == (EMailAddress lhs, EMailAddress rhs)
        {
            return lhs.Value == rhs.Value;
        }

        public static bool operator != (EMailAddress lhs, EMailAddress rhs)
        {
            return !(lhs == rhs);
        }
    }
}
