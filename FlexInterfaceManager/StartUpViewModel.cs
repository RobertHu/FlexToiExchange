using GalaSoft.MvvmLight;
using FlexInterfaceManager.Model;


namespace FlexInterfaceManager
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class StartUpViewModel : ViewModelBase
    {
        private string _RunInfo;
        private double _ProgessValue;
        public StartUpViewModel()
        {
            this.MessengerInstance.Register<ProgressMessage>(this, item =>
            {
                RunInfo = item.Msg;
                ProgessValue = item.ProgressValue;
            });
        }

        public string RunInfo
        {
            get { return _RunInfo; }
            set
            {
                if (_RunInfo != value)
                {
                    _RunInfo = value;
                    RaisePropertyChanged(() => this.RunInfo);
                }
            }
        }

        public double ProgessValue
        {
            get
            {
                return _ProgessValue;
            }
            set
            {
                if (_ProgessValue != value)
                {
                    _ProgessValue = value;
                    RaisePropertyChanged(()=>this.ProgessValue);
                }
            }
        }


    }
}