using DataAccess;
using DataAccess.ServiceUser;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tools;

namespace MainModule.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        #region Constructor & OnCopyDataReceived

        public MainViewModel()
        {
            BackCommand = new RelayCommand(param => BackCommandAction());

            EventAggregator eventAgg = (EventAggregator)ServiceLocator.Current.GetInstance<IEventAggregator>();
            eventAgg.GetEvent<CompositePresentationEvent<SharingData>>().Subscribe(OnCopyDataReceived, ThreadOption.UIThread);
        }

        public void OnCopyDataReceived(SharingData sharingData)
        {
            if (sharingData.DestinationModuleName != NameOfViews.MainView)
                return;

            UserName = sharingData.UserName;

            User user = ServiceUserHelper.GetUser(sharingData.UserName);
            if (user.Role == "Infirmière")
            {
                LevelVisibility = Visibility.Collapsed;
                CreationVisibility = Visibility.Collapsed;
            }
            else
            {
                LevelVisibility = Visibility.Visible;
                CreationVisibility = Visibility.Visible;
            }
        }

        #endregion Constructor & OnCopyDataReceived

        #region Fields

        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public static string UserName;

        /// <summary>
        /// Visibilité des boutons de supression du patient. Hidden pour les infirmières
        /// </summary>
        public static Visibility LevelVisibility;

        /// <summary>
        /// Visibilité du TabItem "Ajout". Hidden pour les infirmières
        /// </summary>
        private Visibility _creationVisibility;

        public Visibility CreationVisibility
        {
            get { return _creationVisibility; }
            set { _creationVisibility = value; OnPropertyChanged("CreationVisibility"); }
        }

        #endregion Fields

        #region Commands

        /// <summary>
        /// Déconnexion de l'utilisateur
        /// </summary>
        public RelayCommand BackCommand { get; set; }

        private void BackCommandAction()
        {
            SharingData sharingData = new SharingData() { DestinationModuleName = NameOfViews.LoginView };
            LoadModule(sharingData, NameOfRegions.MainRegion);
        }

        #endregion Commands
    }
}