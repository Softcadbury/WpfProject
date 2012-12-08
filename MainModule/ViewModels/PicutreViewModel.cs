using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Tools;

namespace MainModule.ViewModels
{
    public class PicutreViewModel : BaseViewModel
    {
        public PicutreViewModel(byte[] p)
        {
            Picture = p;
            PrintCommand = new RelayCommand(param => PrintAction());
        }

        private byte[] _picture;

        public byte[] Picture
        {
            get { return _picture; }
            set { _picture = value; OnPropertyChanged("Picture"); }
        }

        public RelayCommand PrintCommand { get; set; }

        private void PrintAction()
        {
            PrintDocument print = new PrintDocument();
            print.PrintPage += PrintPage;

            PrintDialog printPreviewDialog = new PrintDialog();
            printPreviewDialog.Document = print;

            if (printPreviewDialog.ShowDialog() == DialogResult.OK)
                print.Print();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(System.Drawing.Image.FromStream(new MemoryStream(Picture)), 0, 0);
        }
    }
}