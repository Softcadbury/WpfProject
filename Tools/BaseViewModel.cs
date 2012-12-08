using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    /// <summary>
    /// Classe à utliser en classe mère pour les ViewModels
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Indique à la View que le propriété a changé
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Charge le module spécifié dans la region désirée
        /// </summary>
        protected virtual void LoadModule(SharingData sharingData, string regionName)
        {
            EventAggregator eventAgg = (EventAggregator)ServiceLocator.Current.GetInstance<IEventAggregator>();
            CompositePresentationEvent<SharingData> fileSharingEvent = eventAgg.GetEvent<CompositePresentationEvent<SharingData>>();
            fileSharingEvent.Publish(sharingData);

            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.RequestNavigate(regionName, new Uri(sharingData.DestinationModuleName, UriKind.Relative));
        }
    }
}