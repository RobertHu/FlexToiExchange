using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FlexInterfaceManager.Util
{
    public static class BackgroundWorkerUtil
    {
        public static void Start<T>(Func<object, T> workerAction, Action<T> completeAction, object state)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, e) =>
            {
                e.Result = workerAction(state);
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                T pameter = (T)e.Result;
                completeAction(pameter);
            };
            worker.RunWorkerAsync();
        }


        public static void Start<T>(Func<T> workerAction, Action<T> completeAction)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, e) =>
            {
                e.Result = workerAction();
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                T pameter = (T)e.Result;
                completeAction(pameter);
            };
            worker.RunWorkerAsync();
        }
    }
}
