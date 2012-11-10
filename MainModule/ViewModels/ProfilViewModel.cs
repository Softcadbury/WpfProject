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
        #region Constructor

        #endregion Constructor

        #region Fields

        public ProfilViewModel()
        {
            BackCommand = new RelayCommand(param => BackCommandAction());
        }

        #endregion Fields

        #region Commands

        public RelayCommand BackCommand { get; set; }

        private void BackCommandAction()
        {
            SharingData sharingData = new SharingData() { DestinationModuleName = NameOfViews.LoginView };
            LoadModule(sharingData, NameOfRegions.MainRegion);
        }

        #endregion Commands

        #region Methods

        #endregion Methods
    }
}