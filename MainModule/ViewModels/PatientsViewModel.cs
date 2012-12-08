using DataAccess;
using DataAccess.ServicePatient;
using MainModule.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using Tools;

namespace MainModule.ViewModels
{
    public class PatientsViewModel : BaseViewModel
    {
        #region Constructor & Loaded

        public PatientsViewModel()
        {
            PatientInfoVisibility = Visibility.Hidden;
            PatientObservationVisibility = Visibility.Hidden;

            _weightQueue = new ObservableCollection<KeyValuePair<DateTime, double>>();
            _bloodPressureQueue = new ObservableCollection<KeyValuePair<DateTime, double>>();
            Pictures = new ObservableCollection<PictureModel>();

            DeletePatientCommand = new RelayCommand(param => DeletePatient());
        }

        public void UserControlLoaded()
        {
            DeleteVisibility = MainViewModel.LevelVisibility;
            ((LineSeries)ChartObs.Series[0]).ItemsSource = _weightQueue;
            ((LineSeries)ChartObs.Series[1]).ItemsSource = _bloodPressureQueue;
            RefreshPatientList();
            ResultMsgPatientList = String.Empty;
        }

        #endregion Constructor & Loaded

        #region Fields - Patients list & info

        /// <summary>
        /// Liste des patients
        /// </summary>
        private ObservableCollection<Patient> _patients;

        public ObservableCollection<Patient> Patients
        {
            get { return _patients; }
            set { _patients = value; OnPropertyChanged("Patients"); }
        }

        /// <summary>
        /// Patient sélectionné
        /// </summary>
        private Patient _selectedPatient;

        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
                PatientInfoVisibility = Visibility.Visible;
                PatientObservationVisibility = Visibility.Hidden;
                RefreshObservationsChart();
            }
        }

        /// <summary>
        /// Text du message de résultat
        /// </summary>
        private string _resultMsgPatientList;

        public string ResultMsgPatientList
        {
            get { return _resultMsgPatientList; }
            set { _resultMsgPatientList = value; OnPropertyChanged("ResultMsgPatientList"); }
        }

        /// <summary>
        /// Couleur du message de résultat
        /// </summary>
        private string _resultColorPatientList;

        public string ResultColorPatientList
        {
            get { return _resultColorPatientList; }
            set { _resultColorPatientList = value; OnPropertyChanged("ResultColorPatientList"); }
        }

        #endregion Fields - Patients list & info

        #region Fields - Observation info & images

        /// <summary>
        /// Observation sélectionnée
        /// </summary>
        private Observation _selectedObservation;

        public Observation SelectedObservation
        {
            get { return _selectedObservation; }
            set
            {
                _selectedObservation = value;
                OnPropertyChanged("SelectedObservation");
                PatientObservationVisibility = Visibility.Visible;
                RefreshObservationPictures();
            }
        }

        /// <summary>
        /// Chart et listes utilisées pour afficher les poids et les tensions artérielles enregistrés dans les observations
        /// </summary>
        public Chart ChartObs;

        private ObservableCollection<KeyValuePair<DateTime, double>> _weightQueue;
        private ObservableCollection<KeyValuePair<DateTime, double>> _bloodPressureQueue;

        /// <summary>
        /// Liste des images contenues dans l'observation
        /// </summary>
        private ObservableCollection<PictureModel> _pictures;

        public ObservableCollection<PictureModel> Pictures
        {
            get { return _pictures; }
            set { _pictures = value; OnPropertyChanged("Pictures"); }
        }

        #endregion Fields - Observation info & images

        #region Field - Visibility

        /// <summary>
        /// Visibilité des infos du patient. Hidden quand aucun patient n'est sélectionné
        /// </summary>
        private Visibility _patientInfoVisibility;

        public Visibility PatientInfoVisibility
        {
            get { return _patientInfoVisibility; }
            set { _patientInfoVisibility = value; OnPropertyChanged("PatientInfoVisibility"); }
        }

        /// <summary>
        /// Visibilité des informations sur l'observation du patient. Hidden quand aucune observation n'est sélectionnée
        /// </summary>
        private Visibility _patientObservationVisibility;

        public Visibility PatientObservationVisibility
        {
            get { return _patientObservationVisibility; }
            set { _patientObservationVisibility = value; OnPropertyChanged("PatientObservationVisibility"); }
        }

        /// <summary>
        /// Visibilité du bouton de supression du patient. Hidden pour les infirmières
        /// </summary>
        private Visibility _deleteVisibility;

        public Visibility DeleteVisibility
        {
            get { return _deleteVisibility; }
            set { _deleteVisibility = value; OnPropertyChanged("DeleteVisibility"); }
        }

        #endregion Field - Visibility

        #region Commands

        /// <summary>
        /// Suppresion d'un patient
        /// </summary>
        public RelayCommand DeletePatientCommand { get; set; }

        private void DeletePatient()
        {
            if (SelectedPatient == null)
            {
                DisplayErrorResultPatientList("Aucun patient n'est sélectionné");
                return;
            }

            string patientName = SelectedPatient.Name + " " + SelectedPatient.Firstname;

            if (MessageBoxResult.No == MessageBox.Show("Etes-vous sûre de vouloir supprimer le patient " + patientName + " ?",
                                                       "Confirmation de suppression", MessageBoxButton.YesNo))
            {
                DisplayValidResultPatientList("La suppression du patient " + patientName + " a été annulée");
                return;
            }

            if (!ServicePatientHelper.DeletePatient(SelectedPatient.Id))
            {
                DisplayErrorResultPatientList("La suppresion du patient " + patientName + " a échoué");
                return;
            }

            DisplayValidResultPatientList("Le patient " + patientName + " a bien été supprimé");
            RefreshPatientList();
        }

        #endregion Commands

        #region Methods - Refresh

        /// <summary>
        /// Reset la liste des patients
        /// </summary>
        private void RefreshPatientList()
        {
            Patients = new ObservableCollection<Patient>((from item in ServicePatientHelper.GetListPatient()
                                                          orderby item.Name
                                                          select item).ToList());

            PatientInfoVisibility = Visibility.Hidden;
            PatientObservationVisibility = Visibility.Hidden;
        }

        /// <summary>
        /// Reset le chart des observations
        /// </summary>
        private void RefreshObservationsChart()
        {
            _weightQueue.Clear();
            _bloodPressureQueue.Clear();

            if (SelectedPatient == null || SelectedPatient.Observations == null)
                return;

            foreach (Observation obs in SelectedPatient.Observations)
            {
                _weightQueue.Add(new KeyValuePair<DateTime, double>(obs.Date, obs.Weight));
                _bloodPressureQueue.Add(new KeyValuePair<DateTime, double>(obs.Date, obs.BloodPressure));
            }
        }

        /// <summary>
        /// Reset les images de l'observation
        /// </summary>
        private void RefreshObservationPictures()
        {
            if (SelectedObservation == null)
                return;

            Pictures.Clear();
            foreach (byte[] picture in SelectedObservation.Pictures)
                Pictures.Add(new PictureModel(picture, this));
        }

        #endregion Methods - Refresh

        #region Methods - Result Msg

        /// <summary>
        /// Affiche un message d'erreur coté liste patient
        /// </summary>
        private void DisplayErrorResultPatientList(string msg)
        {
            ResultMsgPatientList = msg;
            ResultColorPatientList = Tools.Useful.ErrorMsgColor;
        }

        /// <summary>
        /// Affiche un message d'action valide coté liste utilisateurs
        /// </summary>
        private void DisplayValidResultPatientList(string msg)
        {
            ResultMsgPatientList = msg;
            ResultColorPatientList = Tools.Useful.ValidMsgColor;
        }

        #endregion Methods - Result Msg
    }
}