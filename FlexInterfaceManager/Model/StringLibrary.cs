using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexInterface.Common;

namespace FlexInterfaceManager.Model
{
    public static class StringLibrary
    {
        public static readonly string PLStartTxtMsg = "Generating PL Interface File!";
        public static readonly string PLEndTxtMsg = "Generate PL Interface File Completed!";
        public static readonly string PLFailedTxtMsg = "Generate PL Interface File Failed!";
        public static readonly string PLStartSendMsg = "Start Generating PL Interface File";
        public static readonly string PLGeneratingSendMsg = "Generating {0} PL Interface File!";

        public static readonly string DepositStartTxtMsg = "Generating Deposit Interface File!";
        public static readonly string DepositEndTxtMsg = "Generate Deposit Interface File Completed!";
        public static readonly string DepositFailedTxtMsg = "Generate Deposit Interface File Failed!";
        public static readonly string DepositStartSendMsg = "Start Generating Deposit Interface File";
        public static readonly string DepositGeneratingSendMsg = "Generating {0} Deposit Interface File!";


        public static readonly string ExceedMaxRecordCountWarnInfo = "The total records have exceeded {0},if continue,the process will be very slow.Do you want to continue ?";


        public static readonly string NoDataMsg = "No data was found!";
        
    }

    public class CustomerHandCheckedMsg { }
    public class CustomerCheckedMsg { }


    public class ShowProgressMsg
    {
        public ShowProgressMsg(bool isShow)
        {
            this.IsShow = isShow;
        }
        public bool IsShow { get; private set; }
    }

    public class ProgressMessage
    {
        public ProgressMessage(string msg, double value)
        {
            this.Msg = msg;
            this.ProgressValue = value;
        }
        public string Msg { get; set; }
        public double ProgressValue { get; set; }
    }
    public class ApplicationCloseMsg { }

    public class CloseProgressMsg { }

    public class HideUserManageComponentMsg
    {
        public HideUserManageComponentMsg(bool isAdmin)
        {
            this.IsAdmin = isAdmin;
        }
        public bool IsAdmin { get; private set; }
    }
    public class LoginSuccessMsg
    {
        public LoginSuccessMsg(LoginProgressEnum loginProgress)
        {
            this.LoginProgress = loginProgress;
        }
        public LoginProgressEnum LoginProgress { get; private set; }
    }

    public class LoginedMsg
    {
        public LoginedMsg(Tuple<bool,IFlexService, string, string, string> msg)
        {
            this.Msg = msg;
        }
        public Tuple<bool,IFlexService, string, string, string> Msg { get; private set; }
    }

    public enum LoginProgressEnum
    {
        LoginOpen,
        LoginClose,
        ProgressOpen,
        ProgressClose,
        LoginCloseProgressOpen,
        ProgressCloseLoginOpen
    }

    public class LoginMsg
    {
        public LoginMsg(string loginId, string pwd)
        {
            this.LoginId = loginId;
            this.Pwd = pwd;
        }
        public string LoginId { get; private set; }
        public string Pwd { get; private set; }
    }

    public class LoadFundNoMsg { }


    public class UpdateWarnInfoMsg
    {
        public UpdateWarnInfoMsg(string info)
        {
            this.Info = info;
        }
        public string Info { get; private set; }
    }


    public class LoadingMsg { }


    public class UpdateFundStatusMsg
    {
        public UpdateFundStatusMsg(string msg)
        {
            this.Msg = msg;
        }
        public string Msg { get; private set; }
    }

    public class FundDataCheckedMsg
    {
    }

    public class UncheckedFundCheckAllButton { }


    public class BeginFetchUserInputFundNoRangeMsg { }


    public class UserInputFundNoRangeMsg
    {
        public UserInputFundNoRangeMsg(string fromFundNo,string toFundNo)
        {
            this.FromFundNo = fromFundNo;
            this.ToFundNo = toFundNo;
        }
        public string FromFundNo { get; private set; }
        public string ToFundNo { get; private set; }
    }
   

}
