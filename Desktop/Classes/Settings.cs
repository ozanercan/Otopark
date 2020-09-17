using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Desktop.Classes
{
    public static class Settings
    {
        public static ImageBrush GetParkingLotImage()
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Properties.Settings.Default
                .ParkingLootImageLocation)));
            }
            catch (Exception hata)
            {
                Debug.WriteLine(hata.Message);
                string workingDirectory = Environment.CurrentDirectory;
                string x = Directory.GetParent(workingDirectory).Parent.FullName;
                return new ImageBrush(new BitmapImage(new Uri(x + @"/Images/ParkingLot.png")));
            }
        }

        public static ImageBrush GetBackgroundImage()
        {
            try
            {
                return new ImageBrush(new BitmapImage(new Uri(Properties.Settings.Default
                .BackgroundImageLocation)));
            }
            catch (Exception hata)
            {
                Debug.WriteLine(hata.Message);
                string workingDirectory = Environment.CurrentDirectory;
                string x = Directory.GetParent(workingDirectory).Parent.FullName;
                return new ImageBrush(new BitmapImage(new Uri(x + @"/Images/BackgroundImage.jpg")));
            }
        }
    }
}