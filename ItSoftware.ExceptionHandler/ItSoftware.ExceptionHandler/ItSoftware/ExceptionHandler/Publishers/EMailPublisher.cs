using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItSoftware.ExceptionHandler.Configuration;
using System.Xml;
using System.Collections.Specialized;
using ItSoftware.ExceptionHandler.Formatters;
using System.Reflection;
using System.Net.Mail;
namespace ItSoftware.ExceptionHandler.Publishers
{
    internal class EMailPublisher : IExceptionPublisher
    {
        internal EMailPublisher(XmlNode xnConfig)
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
            MailMessage mm = new MailMessage();

            mm.From = new MailAddress(this.Settings["fromAddress"]);

            string[] to = this.Settings["toAddress"].Split(new char[] { ' ', ';', ',', '|' });
            foreach (string adr in to)
            {
                mm.To.Add(new MailAddress(adr));
            }

            string[] cc = this.Settings["ccAddress"].Split(new char[] { ' ', ';', ',', '|' });
            foreach (string adr in cc)
            {
                mm.CC.Add(new MailAddress(adr));
            }

            string[] bcc = this.Settings["bccAddress"].Split(new char[] { ' ', ';', ',', '|' });
            foreach (string adr in bcc)
            {
                mm.Bcc.Add(new MailAddress(adr));
            }

            mm.Subject = this.Settings["subject"];
            mm.Body = this.FormatterObject.FormatException(exception);

            SmtpClient sc = new SmtpClient(this.Settings["host"]);
            sc.Port = int.Parse(this.Settings["port"]);
            sc.Send(mm);
        }

        #endregion
    }
}
