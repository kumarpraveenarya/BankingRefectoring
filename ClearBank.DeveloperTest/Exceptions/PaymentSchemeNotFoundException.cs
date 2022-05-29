using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Exceptions
{
    public class PaymentSchemeNotFoundException :Exception
    {
        public PaymentSchemeNotFoundException(string message) : base(message)
        {
        }
    }
}
