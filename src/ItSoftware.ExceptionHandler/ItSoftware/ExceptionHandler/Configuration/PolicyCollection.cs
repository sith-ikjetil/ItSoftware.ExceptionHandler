using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace ItSoftware.ExceptionHandler.Configuration
{
    internal class PolicyCollection : List<Policy>
    {
        internal PolicyCollection(XmlNode xnConfig)
        {
            if (xnConfig == null)
            {
                throw new ArgumentNullException("xnConfig");
            }
            XmlNodeList xnl = xnConfig.SelectNodes("policy");
            foreach (XmlNode xn in xnl)
            {
                this.Add(new Policy(xn));
            }
        }
    }
}
