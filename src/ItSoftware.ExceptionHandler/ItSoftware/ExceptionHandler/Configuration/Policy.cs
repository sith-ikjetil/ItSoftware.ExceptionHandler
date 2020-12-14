using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace ItSoftware.ExceptionHandler.Configuration
{
    internal class Policy : List<Publisher>
    {
        internal Policy(XmlNode xnConfig)
        {
            if (xnConfig == null)
            {
                throw new ArgumentNullException("xnConfig");
            }

            this.Name = xnConfig.Attributes["name"].InnerText;

            XmlNodeList xnl = xnConfig.SelectNodes("publisher");
            foreach (XmlNode xn in xnl)
            {
                this.Add(new Publisher(xn));
            }
        }

        internal string Name { get; private set; }
    }
}
