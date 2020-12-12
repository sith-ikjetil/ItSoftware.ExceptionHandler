using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;
using System.Collections.Specialized;
using ItSoftware.ExceptionHandler.Formatters;
namespace ItSoftware.ExceptionHandler.Publishers
{
    internal class FilePublisher : IExceptionPublisher
    {
        internal FilePublisher(XmlNode xnConfig)
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

        void IExceptionPublisher.PublishException(Exception exception)
        {
            string message = this.FormatterObject.FormatException(exception);


            if ( File.Exists(this.Settings["filename"]) ) {
                if (bool.Parse(this.Settings["overwrite"]))
                {
                    File.Delete(this.Settings["filename"]);
                    using (StreamWriter sw = File.AppendText(this.Settings["filename"]))
                    {
                        sw.Write(message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(this.Settings["filename"]))
                    {
                        sw.WriteLine(Environment.NewLine);
                        sw.WriteLine(Environment.NewLine);
                        sw.Write(message);
                    }
                }
            }
            else {
                using( StreamWriter sw = File.CreateText(this.Settings["filename"]) ) {
                    sw.Write(message);
                }
            }
        }

        #endregion
    }
}
