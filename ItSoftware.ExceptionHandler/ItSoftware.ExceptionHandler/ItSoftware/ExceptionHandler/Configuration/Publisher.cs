using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ItSoftware.ExceptionHandler.Publishers;
using ItSoftware.ExceptionHandler.Formatters;
namespace ItSoftware.ExceptionHandler.Configuration
{
    internal class Publisher
    {
        internal Publisher(XmlNode xnConfig)
        {
            if (xnConfig == null)
            {
                throw new ArgumentNullException("xnConfig");
            }

            this.Name = xnConfig.Attributes["name"].InnerText;
            this.Enabled = bool.Parse(xnConfig.Attributes["enabled"].InnerText);

            if (this.Name.ToLower() == "eventlog")
            {
                this.PublisherObject = new EventLogPublisher(xnConfig);
            }
            else if (this.Name.ToLower() == "file")
            {
                this.PublisherObject = new FilePublisher(xnConfig);
            }
            else if (this.Name.ToLower() == "email")
            {
                this.PublisherObject = new EMailPublisher(xnConfig);
            }
            else
            {
                throw new ArgumentException("Unknown publisher in exceptionHandler configuration");
            }
        }

        internal string Name { get; private set; }
        internal bool Enabled { get; private set; }        
        internal IExceptionPublisher PublisherObject { get; private set; }
    }
}
