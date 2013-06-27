using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexInterface.Common;
using System.Threading;

namespace FlexInterfaceService
{
    public class ClientManager
    {
        public static readonly ClientManager Default = new ClientManager();

        private Dictionary<string, Client> _ClientDict = new Dictionary<string, Client>();

        private object _ClientBlock = new object();

        private object _SentBlock = new object();
        private AutoResetEvent _SentEvent = new AutoResetEvent(false);
        private Queue<object> _SentQueue = new Queue<object>();

        private ClientManager() { }


        public void Start()
        {
            Thread sendThread = new Thread(Send);
            sendThread.IsBackground = true;
            sendThread.Start();
        }

        public void Push(object request)
        {
            lock (this._SentBlock)
            {
                this._SentQueue.Enqueue(request);
                this._SentEvent.Set();
            }
        }

        public void Send()
        {
            while (true)
            {
                this._SentEvent.WaitOne();
                while (this._SentQueue.Count != 0)
                {
                    List<string> toBeRemovedClients = new List<string>();
                    object request = null;
                    lock (this._SentBlock)
                    {
                        if (this._SentQueue.Count == 0) break;
                        request = this._SentQueue.Dequeue();
                    }

                    UserMsg userMsg = request as UserMsg;
                    foreach (KeyValuePair<string, Client> pair in this._ClientDict)
                    {
                        try
                        {


                            if (userMsg != null)
                            {
                                pair.Value.CallbackChannel.PushUpdateUser(userMsg.Msg);
                            }
                        }
                        catch (Exception e)
                        {
                            toBeRemovedClients.Add(pair.Key);
                        }
                    }

                    if (toBeRemovedClients.Count != 0)
                    {
                        foreach (var item in toBeRemovedClients)
                        {
                            _ClientDict.Remove(item);
                        }
                    }

                }
            }
        }

        public Client Add(string sessionId, IFlexServiceCallback callback)
        {
            lock (this._ClientBlock)
            {
                Client client = new Client(sessionId, callback);
                this._ClientDict[sessionId] = client;
                return client;
            }
        }

        public bool Remove(string sessionId)
        {
            lock (this._ClientBlock)
            {
                return this._ClientDict.Remove(sessionId);
            }
        }

        public bool Contains(string sessionId)
        {
            lock (this._ClientBlock)
            {
                return this._ClientDict.ContainsKey(sessionId);
            }
        }
    }

    public class Client
    {
        public Client(string sessionId, IFlexServiceCallback callback)
        {
            this.SessionID = sessionId;
            this.CallbackChannel = callback;
        }
        public string SessionID { get; private set; }
        public IFlexServiceCallback CallbackChannel { get; private set; }
    }

    public class UserMsg
    {
        public UserMsg(User msg)
        {
            this.Msg = msg;
        }
        public User Msg { get; private set; }
    }
}