using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace LogHelper
{
    public class LogHelper
    {
        public static void WriteLog(Type t, string msg)
        {
            ILog logger = log4net.LogManager.GetLogger(t);
            logger.Error(msg);
        }
    }
}
