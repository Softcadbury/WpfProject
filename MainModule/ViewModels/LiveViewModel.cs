using DataAccess;
using DataAccess.ServiceLive;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Forms;
using Tools;

namespace MainModule.ViewModels
{
    public class LiveViewModel : BaseViewModel
    {
        #region Constructor & Loaded

        public LiveViewModel()
        {
            _isLiveStarted = false;
            ButtonContent = _startLiveText;

            _heartQueue = new ObservableCollection<KeyValuePair<DateTime, double>>();
            _tempQueue = new ObservableCollection<KeyValuePair<DateTime, double>>();

            LiveCommand = new RelayCommand(param => LiveCommandAction());
        }

        public void UserControlLoaded()
        {
            ((LineSeries)HeartChart.Series[0]).ItemsSource = _heartQueue;
            ((LineSeries)TempChart.Series[0]).ItemsSource = _tempQueue;
        }

        #endregion Constructor & Loaded

        #region Fields

        /// <summary>
        /// Boolean pour définir si le live est actif
        /// </summary>
        private bool _isLiveStarted;

        /// <summary>
        /// Propriétés pour la pression cardiaque
        /// </summary>
        public Chart HeartChart;

        private ObservableCollection<KeyValuePair<DateTime, double>> _heartQueue;

        /// <summary>
        /// Propriétés pour la température
        /// </summary>
        public Chart TempChart;

        private ObservableCollection<KeyValuePair<DateTime, double>> _tempQueue;

        /// <summary>
        /// Propriétés pour le contenu du bouton
        /// </summary>
        private string _buttonContent;

        public string ButtonContent
        {
            get { return _buttonContent; }
            set { _buttonContent = value; OnPropertyChanged("ButtonContent"); }
        }

        /// <summary>
        /// Valeurs possibles pour le bouton
        /// </summary>
        private string _startLiveText = "Démarrer le live";

        private string _stopLiveText = "Stopper le live";

        #endregion Fields

        #region Commands

        /// <summary>
        /// Commande pour lancer ou arrêter le live
        /// </summary>
        public RelayCommand LiveCommand { get; set; }

        private void LiveCommandAction()
        {
            if (_isLiveStarted)
            {
                _isLiveStarted = false;
                ButtonContent = _startLiveText;
                ServiceLiveHelper.UnSubscribe();
            }
            else
            {
                _isLiveStarted = true;
                ButtonContent = _stopLiveText;
                ServiceLiveHelper.Subscribe(_heartQueue, _tempQueue);
            }
        }

        #endregion Commands
    }
}