using MainModule.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Tools;

namespace MainModule.Models
{
    public class PictureModel
    {
        public PictureModel(byte[] picture, PatientsViewModel viewModel)
        {
            Picture = picture;
            Command = new RelayCommand(x => viewModel.ZoomPicture(Picture));
        }

        private byte[] _picture;

        public byte[] Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        private RelayCommand _command;

        public RelayCommand Command
        {
            get { return _command; }
            set { _command = value; }
        }
    }
}