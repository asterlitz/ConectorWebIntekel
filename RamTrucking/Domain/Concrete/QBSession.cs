using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBFC13Lib;

namespace Domain.Concrete
{
    public class QBSession
    {
        private QBSessionManager sessionManager = null;
        private IMsgSetRequest requestMsgSet;
        private IMsgSetResponse responseMsgSet;
        private IResponse response;
        private bool isSessionBegun;
        private bool isConnectionOpen;
        private string pathQuickBooks;
        private string appName;
        private string msgError;

        private string COUNTRY;
        private short MAJOR_VERSION;
        private short MINOR_VERSION;
        private QBSession()
        {
            COUNTRY = "US";
            MAJOR_VERSION = 12;
            MINOR_VERSION = 0;
            initObjects();
        }
        public QBSession(string appName)
            : this()
        {
            this.appName = appName;
        }
        public QBSession(string path, string appName)
            : this()
        {
            this.pathQuickBooks = path;
            this.appName = appName;
        }
        public QBSession(string path, string country, short majorVersion, short minorVersion)
            : this()
        {
            this.pathQuickBooks = path;
            COUNTRY = country;
            MAJOR_VERSION = majorVersion;
            MINOR_VERSION = minorVersion;
        }
        private void openSession()
        {
            try
            {
                sessionManager.OpenConnection("", appName);
                isConnectionOpen = true;
                sessionManager.BeginSession(pathQuickBooks, ENOpenMode.omDontCare);
                isSessionBegun = true;
            }
            catch (Exception ex)
            {
                this.closeSession();
                this.msgError = ex.Message;
            }
        }
        private void closeSession()
        {
            if (isSessionBegun)
            {
                sessionManager.EndSession();
                isSessionBegun = false;
            }
            if (isConnectionOpen)
            {
                sessionManager.CloseConnection();
                isConnectionOpen = false;
            }
        }
        public bool executeQuery()
        {
            this.msgError = "";
            this.openSession();
            if (this.hasSession())
            {
                return doRequest();
            }
            return false;
        }

        private bool doRequest()
        {
            bool isExecute = false;
            try
            {
                response = null;
                responseMsgSet = sessionManager.DoRequests(requestMsgSet);
                response = responseMsgSet.ResponseList.GetAt(0);
                isExecute = (response.StatusCode == 0);
            }
            catch (Exception ex)
            {
                this.msgError = ex.Message;
            }
            finally
            {
                this.clearRequest();
                this.closeSession();
            }
            return isExecute;
        }

        private void clearRequest()
        {
            try
            {
                requestMsgSet.ClearRequests();                
            }
            catch (Exception ex)
            {
                this.msgError += "clearRequest: " + ex.Message;
            }
        }
        public string getMessageError()
        {
            string msg = string.Empty;
            if (response != null)
            {
                msg = "Response: " + response.StatusMessage;
            }
            msg = appendMsgError(msg);
            return msg;
        }

        private string appendMsgError(string msg)
        {
            if (this.msgError.Length > 0)
            {
                msg += "[Errors: " + this.msgError + "]";
            }
            return msg;
        }
        public bool hasSession()
        {
            return (isSessionBegun && isConnectionOpen);
        }
        public IQBBase getResponse()
        {
            return response.Detail;
        }
        public IMsgSetRequest getRequestMsgSet()
        {
            return requestMsgSet;
        }
        private void initObjects()
        {
            sessionManager = new QBSessionManager();
            requestMsgSet = sessionManager.CreateMsgSetRequest(COUNTRY, MAJOR_VERSION, MINOR_VERSION);
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
        }
    }
}