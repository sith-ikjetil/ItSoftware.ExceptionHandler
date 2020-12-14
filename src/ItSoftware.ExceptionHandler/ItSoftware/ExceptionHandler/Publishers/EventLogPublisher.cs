using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections.Specialized;
using ItSoftware.ExceptionHandler.Formatters;
using System.Reflection;
using System.Diagnostics;
namespace ItSoftware.ExceptionHandler.Publishers
{
    internal class EventLogPublisher : IExceptionPublisher
    {
        internal EventLogPublisher(XmlNode xnConfig)
        {
            if (xnConfig == null)
            {
                throw new ArgumentNullException("xnConfig");
            }

            NameValueCollection nvc = new NameValueCollection();
            for (int i = 0; i < xnConfig.Attributes.Count; i++)
            {
                nvc.Add(xnConfig.Attributes[i].Name, xnConfig.Attributes[i].InnerText);
            }
            this.Settings = nvc;

            string formatterType = xnConfig.SelectSingleNode("formatter").Attributes["type"].InnerText;
            System.Runtime.Remoting.ObjectHandle oh = AppDomain.CurrentDomain.CreateInstance("ItSoftware.ExceptionHandler", formatterType);
            this.FormatterObject = oh.Unwrap() as IExceptionFormatter;
        }

        internal NameValueCollection Settings { get; private set; }
        internal IExceptionFormatter FormatterObject { get; private set; }
        
        #region IExceptionPublisher Members

        public void PublishException(Exception exception)
        {
            string message = this.FormatterObject.FormatException(exception);
            EventLog.WriteEntry(this.Settings["source"], message, (EventLogEntryType)Enum.Parse(typeof(EventLogEntryType),this.Settings["eventLogEntryType"],true));
        }

        #endregion
    }
}
