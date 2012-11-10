using DataAccess;
using DataAccess.ServiceUser;
using DataModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tools;

namespace MainModule.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        #region Constructor

        public UsersViewModel()
        {
            Users = new ObservableCollection<UserModel>(UserService.GetListUser());

            DeleteUserCommand = new RelayCommand(param => DeleteUser());
            FindPictureCommand = new RelayCommand(param => FindPicture());
            CreateUserCommand = new RelayCommand(param => CreateUser());
        }

        #endregion Constructor

        #region Fields - Users list

        private ObservableCollection<UserModel> _users;

        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged("Users"); }
        }

        private UserModel _selectedUser;

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; OnPropertyChanged("SelectedUser"); }
        }

        private string _resultMsgUserList;

        public string ResultMsgUserList
        {
            get { return _resultMsgUserList; }
            set { _resultMsgUserList = value; OnPropertyChanged("ResultMsgUserList"); }
        }

        private string _resultColorUserList;

        public string ResultColorUserList
        {
            get { return _resultColorUserList; }
            set { _resultColorUserList = value; OnPropertyChanged("ResultColorUserList"); }
        }

        #endregion Fields - Users list

        #region Fields - New user

        private string _newLogin;

        public string NewLogin
        {
            get { return _newLogin; }
            set { _newLogin = value; OnPropertyChanged("NewLogin"); }
        }

        private string _newPwd;

        public string NewPwd
        {
            get { return _newPwd; }
            set { _newPwd = value; OnPropertyChanged("NewPwd"); }
        }

        private string _newName;

        public string NewName
        {
            get { return _newName; }
            set { _newName = value; OnPropertyChanged("NewName"); }
        }

        private string _newFirstname;

        public string NewFirstname
        {
            get { return _newFirstname; }
            set { _newFirstname = value; OnPropertyChanged("NewFirstname"); }
        }

        private string _newRole;

        public string NewRole
        {
            get { return _newRole; }
            set { _newRole = value; OnPropertyChanged("NewRole"); }
        }

        private string _newPicturePath;

        public string NewPicturePath
        {
            get { return _newPicturePath; }
            set { _newPicturePath = value; OnPropertyChanged("NewPicturePath"); }
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

        #region Commands

        /// <summary>
        /// Suppresion d'un utilisateur
        /// </summary>
        public RelayCommand DeleteUserCommand { get; set; }

        private void DeleteUser()
        {
            if (SelectedUser == null)
            {
                DisplayErrorResultUserList("Aucun utilisateur n'est sélectionné");
                return;
            }

            if (MessageBoxResult.No == MessageBox.Show("Etes-vous sûre de vouloir supprimer l'utilisateur " +
                                                       SelectedUser.Name + " " + SelectedUser.Firstname + " ?",
                                                       "Confirmation de suppression", MessageBoxButton.YesNo))
            {
                DisplayValidResultUserList("La suppression de l'utilisateur a été annulée");
                return;
            }

            if (!UserService.DeleteUser(SelectedUser.Login))
            {
                DisplayErrorResultUserList("La suppresion de l'utilisateur a échoué");
                return;
            }

            DisplayValidResultUserList("L'utilisateur " + SelectedUser.Name + " " + SelectedUser.Firstname + " a bien été supprimé");
            RefreshUserList();
        }

        /// <summary>
        /// Ouverture d'un exploreur pour récupérer une image lors de la création d'un utilisateur
        /// </summary>
        public RelayCommand FindPictureCommand { get; set; }

        private void FindPicture()
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
                NewPicturePath = ofd.FileName;
        }

        /// <summary>
        /// Création d'un utilisateur
        /// </summary>
        public RelayCommand CreateUserCommand { get; set; }

        private void CreateUser()
        {
            if (string.IsNullOrEmpty(NewLogin) || string.IsNullOrEmpty(NewPwd) ||
                string.IsNullOrEmpty(NewName) || string.IsNullOrEmpty(NewFirstname) ||
                string.IsNullOrEmpty(NewRole) || string.IsNullOrEmpty(_newPicturePath))
            {
                DisplayErrorResultNewUser("Les champs pour la création d'un nouvel utilisateur sont imcomplets");
                return;
            }

            FileInfo fi = new FileInfo(_newPicturePath);
            if (!fi.Exists || (fi.Extension != ".jpg" && fi.Extension != ".png"))
            {
                DisplayErrorResultNewUser("L'image de profil n'est pas correcte, fomats acceptés : jpg et png");
                return;
            }

            if (!UserService.AddUser(new UserModel()
                {
                    Login = NewLogin,
                    Pwd = NewPwd,
                    Name = NewName,
                    Firstname = NewFirstname,
                    Role = NewRole,
                    Picture = Tools.UsefulMethods.ImageToByte(_newPicturePath),
                    Connected = false
                }))
            {
                DisplayErrorResultNewUser("La création du nouvel utilisateur a échoué");
                return;
            }

            DisplayValidResultNewUser("L'utilisateur a bien été créé");
            RefreshUserList();
            ResetNewUserFields();
        }

        #endregion Commands

        #region Methods

        /// <summary>
        /// Reset la liste des utilisateurs
        /// </summary>
        private void RefreshUserList()
        {
            Users = new ObservableCollection<UserModel>(UserService.GetListUser());
        }

        /// <summary>
        /// Reset les champs pour la création d'un nouvel utilisateur
        /// </summary>
        private void ResetNewUserFields()
        {
            NewLogin = string.Empty;
            NewPwd = string.Empty;
            NewName = string.Empty;
            NewFirstname = string.Empty;
            NewRole = string.Empty;
            NewPicturePath = string.Empty;
        }

        /// <summary>
        /// Affiche un message d'erreur coté liste utilisateurs
        /// </summary>
        private void DisplayErrorResultUserList(string msg)
        {
            ResultMsgNewUser = string.Empty;
            ResultMsgUserList = msg;
            ResultColorUserList = "Red";
        }

        /// <summary>
        /// Affiche un message d'action valide coté liste utilisateurs
        /// </summary>
        private void DisplayValidResultUserList(string msg)
        {
            ResultMsgNewUser = string.Empty;
            ResultMsgUserList = msg;
            ResultColorUserList = "Green";
        }

        /// <summary>
        /// Affiche un message d'erreur coté nouvel utilisateur
        /// </summary>
        private void DisplayErrorResultNewUser(string msg)
        {
            ResultMsgUserList = string.Empty;
            ResultMsgNewUser = msg;
            ResultColorNewUser = "Red";
        }

        /// <summary>
        /// Affiche un message d'action valide coté nouvel utilisateur
        /// </summary>
        private void DisplayValidResultNewUser(string msg)
        {
            ResultMsgUserList = string.Empty;
            ResultMsgNewUser = msg;
            ResultColorNewUser = "Green";
        }

        #endregion Methods
    }
}