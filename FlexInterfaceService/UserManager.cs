using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using iExchange.Common;
using System.IO;
using System.Web.Hosting;
using System.Diagnostics;
using FlexInterface.Common;
using FlexInterfaceService.Util;

namespace FlexInterfaceService
{
    internal class UserManager
    {
        private UserManager() 
        {
            FileUtil.MakeFileWritable(this.configFile);
        }
        public static readonly UserManager Default = new UserManager();
        private object _SycBlock = new object();
        private readonly string configFile = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "UserStorage.xml");
        public bool AddUser(string sessionId, User user)
        {
            lock (this._SycBlock)
            {
                if (!ClientManager.Default.Contains(sessionId)) return false;

                if (IsUserExistWhenAdd(user)) return false;
                try
                {
                    user.LastModifedDate = DateTime.Now;
                    user.Id = Guid.NewGuid();
                    XElement root = XElement.Load(configFile);
                    XElement userElement = new XElement("User");
                    userElement.SetAttributeValue("Id", user.Id.ToString());
                    userElement.SetAttributeValue("Name", user.Name);
                    userElement.SetAttributeValue("Pwd", user.Pwd);
                    userElement.SetAttributeValue("Date", user.LastModifedDate.ToStandardDatetimeString());
                    root.Add(userElement);
                    root.Save(configFile);
                    user.Operation = UserOperateEnum.Added;
                    ClientManager.Default.Push(new UserMsg(user));
                    return true;
                }
                catch (Exception exception)
                {
                    AppDebug.LogEvent("ManagerService.QuotationManager.Service.AddUser", exception.ToString(), EventLogEntryType.Error);
                    return false;
                }
            }
        }

        public bool DelUser(string sessionId, User user)
        {
            lock (this._SycBlock)
            {
                if (!ClientManager.Default.Contains(sessionId)) return false;
                if (!IsUserExist(user)) return false;
                try
                {
                    XElement root = XElement.Load(configFile);
                    XElement userElement = root.Descendants().Single(x => x.Attribute("Id").Value == user.Id.ToString());
                    userElement.Remove();
                    root.Save(configFile);
                    user.Operation = UserOperateEnum.Deleted;
                    ClientManager.Default.Push(new UserMsg(user));
                    return true;
                }
                catch (Exception exception)
                {
                    AppDebug.LogEvent("ManagerService.QuotationManager.Service.KeepAlive", exception.ToString(), EventLogEntryType.Error);
                    return false;
                }

            }
        }


        public bool UpdateUser(string sessionId, User user)
        {
            lock (this._SycBlock)
            {
                if (!ClientManager.Default.Contains(sessionId)) return false;
                if (!IsUserExist(user) || IsUserExistWhenUpdate(user)) return false;
                try
                {
                    user.LastModifedDate = DateTime.Now;
                    XElement root = XElement.Load(configFile);
                    XElement userElement = root.Descendants().Single(x => x.Attribute("Id").Value == user.Id.ToString());
                    userElement.SetAttributeValue("Name", user.Name);
                    userElement.SetAttributeValue("Pwd", user.Pwd);
                    userElement.SetAttributeValue("Date", user.LastModifedDate.ToStandardDatetimeString());
                    root.Save(configFile);
                    user.Operation = UserOperateEnum.Updated;
                    ClientManager.Default.Push(new UserMsg(user));
                    return true;
                }
                catch (Exception exception)
                {
                    AppDebug.LogEvent("ManagerService.QuotationManager.Service.UpdateUser", exception.ToString(), EventLogEntryType.Error);
                    return false;
                }
            }
        }

        public User[] GetUserList(string sessionId)
        {
            lock (this._SycBlock)
            {
                if (!ClientManager.Default.Contains(sessionId)) return null;
                try
                {
                    XElement xelment = XElement.Load(configFile);
                    List<User> users = new List<User>();
                    foreach (var item in xelment.Descendants())
                    {
                        User user = new User();
                        user.Id = Guid.Parse(item.Attribute("Id").Value);
                        user.Name = item.Attribute("Name").Value;
                        user.Pwd = item.Attribute("Pwd").Value;
                        user.LastModifedDate = DateTime.Parse(item.Attribute("Date").Value);
                        users.Add(user);
                    }
                    return users.ToArray();
                }
                catch (Exception exception)
                {
                    AppDebug.LogEvent("ManagerService.QuotationManager.Service.GetUserList", exception.ToString(), EventLogEntryType.Error);
                    return null;
                }
            }
        }

        public bool CanLogin(string loginId, string pwd)
        {
            try
            {
                XElement root = XElement.Load(configFile);
                return root.Descendants().Any(x => x.Attribute("Name").Value == loginId && x.Attribute("Pwd").Value == pwd);
            }
            catch (Exception exception)
            {
                AppDebug.LogEvent("UserManager.CanLogin", exception.ToString(), EventLogEntryType.Error);
                return false;
            }
        }

        private bool IsUserExist(User user)
        {
            try
            {
                XElement root = XElement.Load(configFile);
                return root.Descendants().Any(x => x.Attribute("Id").Value == user.Id.ToString());
            }
            catch (Exception exception)
            {
                AppDebug.LogEvent("ManagerService.QuotationManager.Service.IsUserExist", exception.ToString(), EventLogEntryType.Error);
                return false;
            }
        }

        private bool IsUserExistWhenUpdate(User user)
        {
            try
            {
                XElement root = XElement.Load(configFile);
                return root.Descendants().Where(x => x.Attribute("Id").Value != user.Id.ToString() && x.Attribute("Name").Value == user.Name.ToString()).Count() > 0;
            }
            catch (Exception exception)
            {
                AppDebug.LogEvent("ManagerService.QuotationManager.Service.IsUserExistWhenAdd", exception.ToString(), EventLogEntryType.Error);
                return false;
            }
        }

        private bool IsUserExistWhenAdd(User user)
        {
            try
            {
                XElement root = XElement.Load(configFile);
                return root.Descendants().Any(x => x.Attribute("Name").Value == user.Name.ToString());
            }
            catch (Exception exception)
            {
                AppDebug.LogEvent("ManagerService.QuotationManager.Service.IsUserExistWhenAdd", exception.ToString(), EventLogEntryType.Error);
                return false;
            }
        }
    }
}