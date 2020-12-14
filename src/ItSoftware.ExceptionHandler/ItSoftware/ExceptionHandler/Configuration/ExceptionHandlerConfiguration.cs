using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace ItSoftware.ExceptionHandler.Configuration
{
    internal class ExceptionHandlerConfiguration
    {
        public ExceptionHandlerConfiguration(XmlNode xnConfig)
        {
            if (xnConfig == null)
            {
                throw new ArgumentNullException("xnConfig"); 
            }

            this.Policies = new PolicyCollection(xnConfig);
        }

        public PolicyCollection Policies { get; private set; }
    }
}
