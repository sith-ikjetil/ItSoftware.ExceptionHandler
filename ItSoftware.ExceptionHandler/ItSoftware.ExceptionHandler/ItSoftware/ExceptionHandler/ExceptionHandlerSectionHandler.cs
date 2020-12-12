using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ItSoftware.ExceptionHandler.Configuration;
namespace ItSoftware.ExceptionHandler
{
    internal class ExceptionHandlerSectionHandler : IConfigurationSectionHandler 
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            return new ExceptionHandlerConfiguration(section);
        }

        #endregion
    }
}
