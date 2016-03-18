using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class QBFactorySession
    {
        private static QBSession qbSession = null;
        public static QBSession getInstance()
        {
            if (qbSession == null)
            {
                qbSession = new QBSession("App CI");
            }
            return qbSession;
        }
    }
}
