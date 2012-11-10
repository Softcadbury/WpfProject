using DataAccess;
using DataAccess.ServiceUser;
using DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace MainModule.ViewModels
{
    public class PatientsViewModel : BaseViewModel
    {
        #region Constructor

        public PatientsViewModel()
        {
            //Patients = new ObservableCollection<PatientModel>(DataAccess.PatientService.GetListPatient());
        }

        #endregion Constructor

        #region Fields

        private ObservableCollection<PatientModel> _patients;

        public ObservableCollection<PatientModel> Patients
        {
            get { return _patients; }
            set { _patients = value; OnPropertyChanged("Patients"); }
        }

        private PatientModel _selectedPatient;

        public PatientModel SelectedPatient
        {
            get { return _selectedPatient; }
            set { _selectedPatient = value; OnPropertyChanged("SelectedPatient"); }
        }

        #endregion Fields

        #region Commands

        #endregion Commands

        #region Methods

        #endregion Methods
    }
}