using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;
using FlexInterface.Common;
using log4net;

namespace FlexInterfaceManager
{
    public class AliveKeeper
    {
        public static AliveKeeper Default = new AliveKeeper();

        public event EventHandler ConnectionBroken;
        private ILog _Log = LogManager.GetLogger(typeof(AliveKeeper));
        private IFlexService _ManagerService;
        private string _SessionId;

        private Thread _AliveKeepThread = null;
        private object _Lock = new object();
        private int _ExceptionCount = 0;

        private AliveKeeper()
        {
        }

        public void Attach(IFlexService managerService, string sessionId)
        {
            lock (this._Lock)
            {
                this._ManagerService = managerService;
                this._SessionId = sessionId;

                if (this._AliveKeepThread == null)
                {
                    this._AliveKeepThread = new Thread(this.KeepAlive);
                    this._AliveKeepThread.IsBackground = true;
                    this._AliveKeepThread.Start();
                }
            }
        }

        private void KeepAlive(object state)
        {
            int sleepTime = 10000;
            while (true)
            {
                Thread.Sleep(sleepTime);
                try
                {
                    if (!this._ManagerService.KeepAlive(this._SessionId))
                    {
                        _Log.WarnFormat("KeepAlive Failed,Server not contains the sessionID:{0}", this._SessionId);
                        this.FireConnectionBroken();
                        break;
                    }
                    else
                    {
                        sleepTime = 30000;
                        this._ExceptionCount = 0;
                        _Log.Warn("Keep Alive Called!");
                    }
                }
                catch (Exception exception)
                {
                    _Log.ErrorFormat("Alive Keeper Failed:   {0}", exception.ToString());   
                    if (this._ExceptionCount++ >= 3)
                    {
                        this.FireConnectionBroken();
                        break;
                    }
                    sleepTime = 30000 / (2 * this._ExceptionCount);
                }
            }
            this._AliveKeepThread = null;
        }

        private void FireConnectionBroken()
        {
            if (this.ConnectionBroken != null)
            {
                _Log.WarnFormat("FireConnectionBroken");
                this.ConnectionBroken(this, null);
            }
        }
    }
}
