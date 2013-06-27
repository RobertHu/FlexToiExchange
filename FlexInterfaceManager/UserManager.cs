using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using FlexInterfaceManager.Model;
using System.Threading;
using FlexInterface.Common;
using FlexInterfaceManager.Manager;

namespace FlexInterfaceManager
{
    public class UserManager
    {
        public static readonly UserManager Default = new UserManager();
        private UserManager()
        {
            this.UserCol = new ObservableCollection<UserModel>();
        }
        public ObservableCollection<UserModel> UserCol { get; private set; }


        public void AddUser(User user)
        {
            if (UserCol.Any(m => m.ID == user.Id)) return;
            Common.SynchronizationContext.Post(m =>
            {
                UserCol.Add(new UserModel { ID = user.Id, LoginID = user.Name, Pwd = user.Pwd, LastModifiedDate = user.LastModifedDate });
            }, null);
        }

        public void DelUser(User user)
        {
            if (!UserCol.Any(m => m.ID == user.Id)) return;
            Common.SynchronizationContext.Post(m =>
            {
                var item = UserCol.Single(c => c.ID == user.Id);
                UserCol.Remove(item);
            }, null);
        }


        public void UpdateUser(User user)
        {
            if (!UserCol.Any(m => m.ID == user.Id)) return;
            Common.SynchronizationContext.Post(m =>
            {
                var item = UserCol.Single(c => c.ID == user.Id);
                item.LoginID = user.Name;
                item.Pwd = user.Pwd;
                item.LastModifiedDate = user.LastModifedDate;
            }, null);
        }

    }
}
