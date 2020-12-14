using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ItSoftware.ExceptionHandler.Configuration;
namespace ItSoftware.ExceptionHandler
{
    public static class ExceptionManager
    {
        private static ExceptionHandlerConfiguration s_ehsh = null;
        private static object s_lockObject = new object();
        public static void PublishException(Exception exception, string policyName)
        {
            lock (s_lockObject)
            {
                if (s_ehsh == null)
                {
                    try
                    {
                        s_ehsh = ConfigurationManager.GetSection("exceptionHandler") as ExceptionHandlerConfiguration;
                    }
                    catch (ConfigurationException ce)
                    {
                        throw new Exception("Fault with exceptionHandler configuration", ce);
                    }
                }
            }

            if (s_ehsh == null)
            {
                throw new ArgumentNullException("s_ehsh","Missing exceptionHandler configuration section");
            }

            foreach (Policy p in ExceptionManager.s_ehsh.Policies)
            {
                if (p.Name == policyName)
                {
                    foreach (Publisher pub in p)
                    {
                        if (pub.Enabled)
                        {
                            try
                            {
                                pub.PublisherObject.PublishException(exception);
                            }
                            catch (OutOfMemoryException)
                            {
                                throw;
                            }
                            catch (StackOverflowException)
                            {
                                throw;
                            }
                            catch (Exception x)
                            {
                                string xy = x.Message;
                            }

                        }
                    }
                    break;
                }
            }
        }
    }
}
