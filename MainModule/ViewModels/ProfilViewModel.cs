using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace MainModule.ViewModels
{
    public class ProfilViewModel : BaseViewModel
    {
        #region Constructor & Loaded

        public ProfilViewModel()
        {
            BackCommand = new RelayCommand(param => BackCommandAction());
        }

        public void UserControlLoaded()
        {
            Welcome = "Bienvenue " + MainViewModel.UserName;
        }

        #endregion Constructor & Loaded

        #region Commands

        private string _welcome;

        public string Welcome
        {
            get { return _welcome; }
            set { _welcome = value; OnPropertyChanged("Welcome"); }
        }

        public RelayCommand BackCommand { get; set; }

        private void BackCommandAction()
        {
            SharingData sharingData = new SharingData() { DestinationModuleName = NameOfViews.LoginView };
            LoadModule(sharingData, NameOfRegions.MainRegion);
        }

        #endregion Commands
    }
}