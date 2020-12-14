using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItSoftware.ExceptionHandler;
namespace ItSoftware.ExceptionHandler.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationException ae = new ApplicationException("hello, world!", new Exception("This is exception 2"));
            try
            {
                throw ae;
            }
            catch (Exception y)
            {
                ExceptionManager.PublishException(y, "Error");
            }
        }
    }
}
