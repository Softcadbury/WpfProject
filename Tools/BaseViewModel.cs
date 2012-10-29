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
    /// Classe dont doivent hériter les ViewModels pour implémenter OnPropertyChanged()
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            ThrowOnInvalidPropertyName = false;
        }

        private bool ThrowOnInvalidPropertyName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Indique à la View que le propriété a changé
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        /// <summary>
        /// Vérifie le nom de la propriété
        /// Ne s'active qu'à la compilation par debug et avec ThrowOnInvalidPropertyName = true
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
    }
}