// using Yara.FleetManagement.Shared.Exceptions;
using System;
using System.Collections.Generic;
using TypeAheadApi.Utils.Exceptions;

namespace TypeAheadApi.Utils.Validations
{
    public static class Validate
    {
        public static StringValidations That(string value)
        {
            return new StringValidations(value);
        }

        public static void ThrowError()
        {
            throw new InvalidParameterException();
        }

        public static void ThrowError(string message)
        {
            throw new InvalidParameterException(message);
        }
    }
}
