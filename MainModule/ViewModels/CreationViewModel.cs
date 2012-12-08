using DataAccess;
using DataAccess.ServicePatient;
using DataAccess.ServiceUser;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace MainModule.ViewModels
{
    public class CreationViewModel : BaseViewModel
    {
        #region Constructor & Loaded

        public CreationViewModel()
        {
            FindUserPictureCommand = new RelayCommand(param => FindUserPicture());
            CreateUserCommand = new RelayCommand(param => CreateUser());

            CreatePatientCommand = new RelayCommand(param => CreatePatient());

            FindObsPictureCommand = new RelayCommand(param => FindObsPicture());
            CreateObsCommand = new RelayCommand(param => CreateObs());
        }

        public void UserControlLoaded()
        {
            RefreshPatientList();
            ResetDisplay();
            ResetNewUserFields();
            ResetNewPatientFields();
            ResetNewObservationFields();
        }

        #endregion Constructor & Loaded

        #region Fields - New user

        private string _newUserLogin;

        public string NewUserLogin
        {
            get { return _newUserLogin; }
            set { _newUserLogin = value; OnPropertyChanged("NewUserLogin"); }
        }

        private string _newUserPwd;

        public string NewUserPwd
        {
            get { return _newUserPwd; }
            set { _newUserPwd = value; OnPropertyChanged("NewUserPwd"); }
        }

        private string _newUserName;

        public string NewUserName
        {
            get { return _newUserName; }
            set { _newUserName = value; OnPropertyChanged("NewUserName"); }
        }

        private string _newUserFirstname;

        public string NewUserFirstname
        {
            get { return _newUserFirstname; }
            set { _newUserFirstname = value; OnPropertyChanged("NewUserFirstname"); }
        }

        private string _newUserRole;

        public string NewUserRole
        {
            get { return _newUserRole; }
            set { _newUserRole = value; OnPropertyChanged("NewUserRole"); }
        }

        private string _newUserPicturePath;

        public string NewUserPicturePath
        {
            get { return _newUserPicturePath; }
            set { _newUserPicturePath = value; OnPropertyChanged("NewUserPicturePath"); }
        }

        private string _resultMsgNewUser;

        public string ResultMsgNewUser
        {
            get { return _resultMsgNewUser; }
            set { _resultMsgNewUser = value; OnPropertyChanged("ResultMsgNewUser"); }
        }

        private string _resultColorNewUser;

        public string ResultColorNewUser
        {
            get { return _resultColorNewUser; }
            set { _resultColorNewUser = value; OnPropertyChanged("ResultColorNewUser"); }
        }

        #endregion Fields - New user

        #region Fields - New Patient

        private string _newPatientName;

        public string NewPatientName
        {
            get { return _newPatientName; }
            set { _newPatientName = value; OnPropertyChanged("NewPatientName"); }
        }

        private string _newPatientFirstname;

        public string NewPatientFirstname
        {
            get { return _newPatientFirstname; }
            set { _newPatientFirstname = value; OnPropertyChanged("NewPatientFirstname"); }
        }

        private DateTime _newPatientBirthday = DateTime.Now;

        public DateTime NewPatientBirthday
        {
            get { return _newPatientBirthday; }
            set { _newPatientBirthday = value; OnPropertyChanged("NewPatientBirthday"); }
        }

        private string _resultMsgNewPatient;

        public string ResultMsgNewPatient
        {
            get { return _resultMsgNewPatient; }
            set { _resultMsgNewPatient = value; OnPropertyChanged("ResultMsgNewPatient"); }
        }

        private string _resultColorNewPatient;

        public string ResultColorNewPatient
        {
            get { return _resultColorNewPatient; }
            set { _resultColorNewPatient = value; OnPropertyChanged("ResultColorNewPatient"); }
        }

        #endregion Fields - New Patient

        #region Fields - New Observation

        private ObservableCollection<Patient> _patients;

        public ObservableCollection<Patient> Patients
        {
            get { return _patients; }
            set { _patients = value; OnPropertyChanged("Patients"); }
        }

        private Patient _selectedPatient;

        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set { _selectedPatient = value; OnPropertyChanged("SelectedPatient"); }
        }

        private int _newObsWeight;

        public int NewObsWeight
        {
            get { return _newObsWeight; }
            set { _newObsWeight = value; OnPropertyChanged("NewObsWeight"); }
        }

        private int _newObsBloodPressure;

        public int NewObsBloodPressure
        {
            get { return _newObsBloodPressure; }
            set { _newObsBloodPressure = value; OnPropertyChanged("NewObsBloodPressure"); }
        }

        private string _newObsComment;

        public string NewObsComment
        {
            get { return _newObsComment; }
            set { _newObsComment = value; OnPropertyChanged("NewObsComment"); }
        }

        private string _newObsPrescription;

        public string NewObsPrescription
        {
            get { return _newObsPrescription; }
            set { _newObsPrescription = value; OnPropertyChanged("NewObsPrescription"); }
        }

        private string[] _newObsPicturePath;

        public string[] NewObsPicturePath
        {
            get { return _newObsPicturePath; }
            set { _newObsPicturePath = value; OnPropertyChanged("NewObsPicturePath"); }
        }

        private string _resultMsgNewObservation;

        public string ResultMsgNewObservation
        {
            get { return _resultMsgNewObservation; }
            set { _resultMsgNewObservation = value; OnPropertyChanged("ResultMsgNewObservation"); }
        }

        private string _resultColorNewObservation;

        public string ResultColorNewObservation
        {
            get { return _resultColorNewObservation; }
            set { _resultColorNewObservation = value; OnPropertyChanged("ResultColorNewObservation"); }
        }

        #endregion Fields - New Observation

        #region Commands

        /// <summary>
        /// Ouverture d'un exploreur pour récupérer une image lors de la création d'un utilisateur
        /// </summary>
        public RelayCommand FindUserPictureCommand { get; set; }

        private void FindUserPicture()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "JPG|*.jpg;*.jpeg|PNG|*.png";
            if (ofd.ShowDialog() == true)
                NewUserPicturePath = ofd.FileName;
        }

        /// <summary>
        /// Création d'un utilisateur
        /// </summary>
        public RelayCommand CreateUserCommand { get; set; }

        private void CreateUser()
        {
            if (string.IsNullOrEmpty(NewUserLogin) || string.IsNullOrEmpty(NewUserPwd) ||
                string.IsNullOrEmpty(NewUserName) || string.IsNullOrEmpty(NewUserFirstname) ||
                string.IsNullOrEmpty(NewUserRole) || string.IsNullOrEmpty(_newUserPicturePath))
            {
                DisplayErrorResultNewUser("Les champs pour la création d'un nouveau utilisateur sont imcomplets");
                return;
            }

            FileInfo fi = new FileInfo(_newUserPicturePath);
            if (!fi.Exists)
            {
                DisplayErrorResultNewUser("L'image de profil n'est pas valide");
                return;
            }

            if (!ServiceUserHelper.AddUser(new User()
            {
                Login = NewUserLogin,
                Pwd = NewUserPwd,
                Name = NewUserName,
                Firstname = NewUserFirstname,
                Role = NewUserRole,
                Picture = Tools.Useful.ImageToByte(_newUserPicturePath),
                Connected = false
            }))
            {
                DisplayErrorResultNewUser("La création du nouveau utilisateur a échoué");
                return;
            }

            DisplayValidResultNewUser("L'utilisateur " + NewUserName + " " + NewUserFirstname + " a bien été créé");
            ResetNewUserFields();
        }

        /// <summary>
        /// Création d'un patient
        /// </summary>
        public RelayCommand CreatePatientCommand { get; set; }

        private void CreatePatient()
        {
            if (string.IsNullOrEmpty(NewPatientName) || string.IsNullOrEmpty(NewPatientFirstname))
            {
                DisplayErrorResultNewPatient("Les champs pour la création d'un nouveau patient sont imcomplets");
                return;
            }

            if (NewPatientBirthday >= DateTime.Now)
            {
                DisplayErrorResultNewPatient("La date choisi est incorrecte");
                return;
            }

            if (!ServicePatientHelper.AddPatient(new Patient()
                {
                    Name = NewPatientName,
                    Firstname = NewPatientFirstname,
                    Birthday = NewPatientBirthday
                }))
            {
                DisplayErrorResultNewPatient("La création du nouveau patient a échoué");
                return;
            }

            DisplayValidResultNewPatient("Le patient " + NewPatientName + " " + NewPatientFirstname + " a bien été créé");
            ResetNewPatientFields();
            RefreshPatientList();
        }

        /// <summary>
        /// Ouverture d'un exploreur pour récupérer une image lors de la création d'une observation
        /// </summary>
        public RelayCommand FindObsPictureCommand { get; set; }

        private void FindObsPicture()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "JPG|*.jpg;*.jpeg|PNG|*.png";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == true)
                NewObsPicturePath = ofd.FileNames;
        }

        /// <summary>
        /// Création d'une observation
        /// </summary>
        public RelayCommand CreateObsCommand { get; set; }

        private void CreateObs()
        {
            if (SelectedPatient == null)
            {
                DisplayErrorResultNewObservation("Aucun patient n'est sélectionné");
                return;
            }

            if (string.IsNullOrEmpty(NewObsComment) || NewObsWeight == 0 || NewObsBloodPressure == 0)
            {
                DisplayErrorResultNewObservation("Les champs pour la création d'une nouvelle observation sont imcomplets");
                return;
            }

            if (!ArePicturesValid())
            {
                DisplayErrorResultNewObservation("Les radios ne sont pas valides");
                return;
            }

            string patientName = SelectedPatient.Name + " " + SelectedPatient.Firstname;

            if (!ServiceObservationHelper.AddObservation(SelectedPatient.Id, new DataAccess.ServiceObservation.Observation()
            {
                Date = DateTime.Now,
                Weight = NewObsWeight,
                BloodPressure = NewObsBloodPressure,
                Comment = NewObsComment,
                Prescription = GetPrescriptions(),
                Pictures = GetPictures()
            }))
            {
                DisplayErrorResultNewObservation("L'ajout de l'observation au patient " + patientName + " a échoué");
                return;
            }

            DisplayValidResultNewObservation("L'observation a bien été ajouté au patient " + patientName);
            ResetNewObservationFields();
            RefreshPatientList();
        }

        #endregion Commands

        #region Methods

        /// <summary>
        /// Reset la liste des patients
        /// </summary>
        private void RefreshPatientList()
        {
            Patients = new ObservableCollection<Patient>((from item in ServicePatientHelper.GetListPatient()
                                                          orderby item.Name
                                                          select item).ToList());
        }

        /// <summary>
        /// Reset les champs pour la création d'un nouveau utilisateur
        /// </summary>
        private void ResetNewUserFields()
        {
            NewUserLogin = string.Empty;
            NewUserPwd = string.Empty;
            NewUserName = string.Empty;
            NewUserFirstname = string.Empty;
            NewUserRole = string.Empty;
            NewUserPicturePath = string.Empty;
        }

        /// <summary>
        /// Reset les champs pour la création d'un nouveau patient
        /// </summary>
        private void ResetNewPatientFields()
        {
            NewPatientName = string.Empty;
            NewPatientFirstname = string.Empty;
            NewPatientBirthday = DateTime.Now;
        }

        /// <summary>
        /// Reset les champs pour la création d'une observation
        /// </summary>
        private void ResetNewObservationFields()
        {
            NewObsWeight = 0;
            NewObsBloodPressure = 0;
            NewObsComment = string.Empty;
            NewObsPrescription = string.Empty;
            NewObsPicturePath = null;
        }

        /// <summary>
        /// Reset l'affichage des resultats
        /// </summary>
        private void ResetDisplay()
        {
            ResultMsgNewUser = string.Empty;
            ResultMsgNewPatient = string.Empty;
            ResultMsgNewObservation = string.Empty;
        }

        /// <summary>
        /// Affiche un message d'erreur coté nouveau utilisateur
        /// </summary>
        private void DisplayErrorResultNewUser(string msg)
        {
            ResetDisplay();
            ResultMsgNewUser = msg;
            ResultColorNewUser = Tools.Useful.ErrorMsgColor;
        }

        /// <summary>
        /// Affiche un message d'action valide coté nouveau utilisateur
        /// </summary>
        private void DisplayValidResultNewUser(string msg)
        {
            ResetDisplay();
            ResultMsgNewUser = msg;
            ResultColorNewUser = Tools.Useful.ValidMsgColor;
        }

        /// <summary>
        /// Affiche un message d'erreur coté nouveau utilisateur
        /// </summary>
        private void DisplayErrorResultNewPatient(string msg)
        {
            ResetDisplay();
            ResultMsgNewPatient = msg;
            ResultColorNewPatient = Tools.Useful.ErrorMsgColor;
        }

        /// <summary>
        /// Affiche un message d'action valide coté nouveau utilisateur
        /// </summary>
        private void DisplayValidResultNewPatient(string msg)
        {
            ResetDisplay();
            ResultMsgNewPatient = msg;
            ResultColorNewPatient = Tools.Useful.ValidMsgColor;
        }

        /// <summary>
        /// Affiche un message d'erreur coté nouvelle observation
        /// </summary>
        private void DisplayErrorResultNewObservation(string msg)
        {
            ResetDisplay();
            ResultMsgNewObservation = msg;
            ResultColorNewObservation = Tools.Useful.ErrorMsgColor;
        }

        /// <summary>
        /// Affiche un message d'action valide coté nouvelle observation
        /// </summary>
        private void DisplayValidResultNewObservation(string msg)
        {
            ResetDisplay();
            ResultMsgNewObservation = msg;
            ResultColorNewObservation = Tools.Useful.ValidMsgColor;
        }

        /// <summary>
        /// Retourne les prescriptions de la nouvelle observation
        /// </summary>
        /// <returns></returns>
        private string[] GetPrescriptions()
        {
            if (NewObsPrescription == null || NewObsPrescription.Length == 0)
                return null;

            return NewObsPrescription.Split(';');
        }

        /// <summary>
        /// Retourne les images de la nouvelle observation
        /// </summary>
        /// <returns></returns>
        private Byte[][] GetPictures()
        {
            if (NewObsPicturePath == null)
                return null;

            Byte[][] pictures = new byte[NewObsPicturePath.Length][];
            for (int i = 0; i < NewObsPicturePath.Length; i++)
                pictures[i] = Tools.Useful.ImageToByte(NewObsPicturePath[i]);

            return pictures;
        }

        /// <summary>
        /// Return true si les radios sont correctes
        /// </summary>
        /// <returns></returns>
        private bool ArePicturesValid()
        {
            if (NewObsPicturePath == null)
                return true;

            foreach (string path in NewObsPicturePath)
                if (!(new FileInfo(path)).Exists)
                    return false;

            return true;
        }

        #endregion Methods
    }
}