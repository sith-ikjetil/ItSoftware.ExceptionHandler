using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItSoftware.ExceptionHandler.Formatters
{
    internal interface IExceptionFormatter
    {
        string FormatException(Exception exception);
    }
}
