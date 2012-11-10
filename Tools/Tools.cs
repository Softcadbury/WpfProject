﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Tools
{
    /// <summary>
    /// Permet de déclarer des méthodes utiles au projet
    /// </summary>
    public static class UsefulMethods
    {
        /// <summary>
        /// Donne le byte[] d'une image grâce à son chemin
        /// </summary>
        public static byte[] ImageToByte(string path)
        {
            Image oldImage = Image.FromFile(path);

            // Définie les nouvelles height et with
            int newWidth;
            int newHeight;
            if (oldImage.Width > oldImage.Height)
            {
                newWidth = 120;
                newHeight = 120 * oldImage.Height / oldImage.Width;
            }
            else
            {
                newHeight = 120;
                newWidth = 120 * oldImage.Width / oldImage.Height;
            }

            // Redimensionne l'image
            Bitmap newImage = new Bitmap(newWidth, newHeight);
            Graphics resizer = Graphics.FromImage(newImage);
            resizer.InterpolationMode = InterpolationMode.HighQualityBicubic;
            resizer.DrawImage(oldImage, 0, 0, newWidth, newHeight);

            // Transforme l'image en byte[]
            ImageConverter converter = new ImageConverter();
            byte[] newImageByteArray = (byte[])converter.ConvertTo(newImage, typeof(byte[]));

            // Libération mémoire
            oldImage.Dispose();
            newImage.Dispose();
            resizer.Dispose();

            return newImageByteArray;
        }
    }
}