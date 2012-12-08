using DataAccess;
using DataAccess.ServiceUser;
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
        #region Constructor & Loaded

        public UsersViewModel()
        {
            DeleteUserCommand = new RelayCommand(param => DeleteUser());
        }

        public void UserControlLoaded()
        {
            DeleteVisibility = MainViewModel.LevelVisibility;
            RefreshUserList();
            ResultMsgUserList = String.Empty;
        }

        #endregion Constructor & Loaded

        #region Fields - Users list

        /// <summary>
        /// Liste des utilisateurs
        /// </summary>
        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged("Users"); }
        }

        /// <summary>
        /// Utilisateur sélectionné
        /// </summary>
        private User _selectedUser;

        public User SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; OnPropertyChanged("SelectedUser"); }
        }

        /// <summary>
        /// Text du message de résultat
        /// </summary>
        private string _resultMsgUserList;

        public string ResultMsgUserList
        {
            get { return _resultMsgUserList; }
            set { _resultMsgUserList = value; OnPropertyChanged("ResultMsgUserList"); }
        }

        /// <summary>
        /// Couleur du message de résultat
        /// </summary>
        private string _resultColorUserList;

        public string ResultColorUserList
        {
            get { return _resultColorUserList; }
            set { _resultColorUserList = value; OnPropertyChanged("ResultColorUserList"); }
        }

        #endregion Fields - Users list

        #region Field - Visibility

        /// <summary>
        /// Visibilité du bouton de supression de l'utilisateur. Hidden pour les infirmières
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

            string userName = SelectedUser.Name + " " + SelectedUser.Firstname;

            if (MessageBoxResult.No == MessageBox.Show("Etes-vous sûre de vouloir supprimer l'utilisateur " + userName + " ?",
                                                       "Confirmation de suppression", MessageBoxButton.YesNo))
            {
                DisplayValidResultUserList("La suppression de l'utilisateur " + userName + " a été annulée");
                return;
            }

            if (!ServiceUserHelper.DeleteUser(SelectedUser.Login))
            {
                DisplayErrorResultUserList("La suppresion de l'utilisateur " + userName + " a échoué");
                return;
            }

            DisplayValidResultUserList("L'utilisateur " + userName + " a bien été supprimé");
            RefreshUserList();
        }

        #endregion Commands

        #region Methods

        /// <summary>
        /// Reset la liste des utilisateurs
        /// </summary>
        private void RefreshUserList()
        {
            Users = new ObservableCollection<User>((from item in ServiceUserHelper.GetListUser()
                                                    orderby item.Name
                                                    select item).ToList());
        }

        /// <summary>
        /// Affiche un message d'erreur coté liste utilisateurs
        /// </summary>
        private void DisplayErrorResultUserList(string msg)
        {
            ResultMsgUserList = msg;
            ResultColorUserList = Tools.Useful.ErrorMsgColor;
        }

        /// <summary>
        /// Affiche un message d'action valide coté liste utilisateurs
        /// </summary>
        private void DisplayValidResultUserList(string msg)
        {
            ResultMsgUserList = msg;
            ResultColorUserList = Tools.Useful.ValidMsgColor;
        }

        #endregion Methods
    }
}