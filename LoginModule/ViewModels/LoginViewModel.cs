using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Tools;

namespace LoginModule.ViewModels
{
    internal class LoginViewModel : BaseViewModel
    {
        #region Constructor & OnCopyDataReceived

        public LoginViewModel()
        {
            LeaveCommand = new RelayCommand(param => LeaveCommandAction());
            LoginCommand = new RelayCommand(param => LoginCommandAction());
        }

        #endregion Constructor & OnCopyDataReceived

        #region Fields

        private string _login;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        private PasswordBox _pwd;

        public PasswordBox Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        private string _errorMsg;

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { _errorMsg = value; OnPropertyChanged("ErrorMsg"); }
        }

        #endregion Fields

        #region Commands

        public RelayCommand LeaveCommand { get; set; }

        private void LeaveCommandAction()
        {
            Application.Current.Shutdown();
        }

        public RelayCommand LoginCommand { get; set; }

        private void LoginCommandAction()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Pwd.Password))
            {
                ErrorMsg = "Les champs de connexion sont imcomplets";
                return;
            }

            if (!DataAccess.ServiceUserHelper.Connect(Login, Pwd.Password))
            {
                ErrorMsg = "Les champs de connexion sont incorrects";
                Pwd.Password = string.Empty;
                return;
            }

            SharingData sharingData = new SharingData()
            {
                DestinationModuleName = NameOfViews.MainView,
                UserName = Login
            };

            LoadModule(sharingData, NameOfRegions.MainRegion);
            Pwd.Password = string.Empty;
        }

        #endregion Commands
    }
}