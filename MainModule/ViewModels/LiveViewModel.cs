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

        private bool _isLiveStarted;
        private string _startLiveText = "Démarrer le live";
        private string _stopLiveText = "Stopper le live";

        public Chart HeartChart;
        private ObservableCollection<KeyValuePair<DateTime, double>> _heartQueue;

        public Chart TempChart;
        private ObservableCollection<KeyValuePair<DateTime, double>> _tempQueue;

        private string _buttonContent;

        public string ButtonContent
        {
            get { return _buttonContent; }
            set { _buttonContent = value; OnPropertyChanged("ButtonContent"); }
        }

        #endregion Fields

        #region Commands

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