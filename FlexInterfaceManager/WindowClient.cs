using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexInterface.Common;

namespace FlexInterfaceManager
{
    public class WindowClient : IFlexServiceCallback
    {
        void IFlexServiceCallback.PushUpdateUser(User user)
        {
            switch (user.Operation)
            {
                case UserOperateEnum.Added:
                    UserManager.Default.AddUser(user);
                    break;
                case UserOperateEnum.Deleted:
                    UserManager.Default.DelUser(user);
                    break;
                case UserOperateEnum.Updated:
                    UserManager.Default.UpdateUser(user);
                    break;
            }
        }
    }
}
