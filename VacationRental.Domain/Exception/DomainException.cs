using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRental.Domain.Exception
{
    public abstract class DomainException : System.Exception
    {
        public DomainException(string message) : base(message)
        {

        }
    }
    public class ValidationException : DomainException
    {
        public ValidationException(string message) : base(message)
        {

        }
    }

    public class NotFoundException : DomainException
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
