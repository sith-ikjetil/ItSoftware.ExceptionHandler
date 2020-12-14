using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItSoftware.ExceptionHandler.Publishers
{
    internal interface IExceptionPublisher
    {
        void PublishException(Exception exception);
    }
}
