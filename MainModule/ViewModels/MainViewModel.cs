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
            EventAggregator eventAgg = (EventAggregator)ServiceLocator.Current.GetInstance<IEventAggregator>();
            eventAgg.GetEvent<CompositePresentationEvent<SharingData>>().Subscribe(OnCopyDataReceived, ThreadOption.UIThread);
        }

        public void OnCopyDataReceived(SharingData sharingData)
        {
            if (sharingData.DestinationModuleName != NameOfViews.MainView)
                return;

            UserName = sharingData.UserName;
        }

        #endregion Constructor & OnCopyDataReceived

        #region Fields

        public static string UserName;

        #endregion Fields
    }
}