using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexInterfaceManager
{
    public class ViewModelLocator
    {


        private StartUpViewModel _StartUpViewModel;

        private LoginViewModel _LoginViewModel;

        public LoginViewModel LoginViewModel
        {
            get
            {
                if (_LoginViewModel == null)
                {
                    _LoginViewModel = new LoginViewModel();
                }
                return _LoginViewModel;
            }
        }


        public StartUpViewModel StartUpViewModel
        {
            get
            {
                if (_StartUpViewModel == null)
                {
                    _StartUpViewModel = new StartUpViewModel();
                }
                return _StartUpViewModel;
            }
        }

     
    }
}
