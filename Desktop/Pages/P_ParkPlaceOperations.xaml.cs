﻿using Business.Abstract;
using Desktop.Classes;
using Desktop.UserControls;
using Entities.Concrete;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_ParkPlaceOperations.xaml
    /// </summary>
    public partial class P_ParkPlaceOperations : Page
    {
        private IParkPlaceService _parkPlaceService;
        private Employee _employee;

        public P_ParkPlaceOperations(IParkPlaceService parkPlaceService, Employee employee)
        {
            _employee = employee;
            _parkPlaceService = parkPlaceService;

            InitializeComponent();

            GetParkingLootImage();
            CreateButtonMenuItems();
            LoadParkPlaces();
        }

        private bool IsResizeMode { get; set; }
        private bool IsPositionMode { get; set; }
        public List<ParkPlace> ParkPlaces { get; set; }
        public UC_ParkPlace SelectedItem { get; set; }

        private ContextMenu ucparkPlace_contextMenu = new ContextMenu();


        // Park alanlarını sayfaya yüklemek ve konumlandırmak için.
        private void LoadParkPlaces()
        {
            try
            {
                ParkPlaces = _parkPlaceService.ListByNonDeleted();

                foreach (var parkPlace in ParkPlaces)
                {
                    UC_ParkPlace uc_Place = new UC_ParkPlace(parkPlace, _employee, true)
                    {
                        Width = parkPlace.Width,
                        Height = parkPlace.Height,
                        Name = "button_Place" + parkPlace.Id,
                        Content = parkPlace.Name,
                        ContextMenu = ucparkPlace_contextMenu
                    };
                    uc_Place.PreviewMouseRightButtonUp += Uc_Place_PreviewMouseRightButtonUp;
                    canvas.Children.Add(uc_Place);

                    Canvas.SetLeft(uc_Place, parkPlace.X);
                    Canvas.SetTop(uc_Place, parkPlace.Y);
                }
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
            }

        }

        // Sağ tıklama menüsünü oluşturur
        private void CreateButtonMenuItems()
        {
            ucparkPlace_contextMenu.Items.Clear();
            MenuItem menuItem = new MenuItem()
            {
                Name = "menuItem_Resize",
                Header = "Boyutlandır"
            };
            MenuItem menuItem2 = new MenuItem()
            {
                Name = "menuItem_Delete",
                Header = "Sil"
            };
            MenuItem menuItem3 = new MenuItem()
            {
                Name = "menuItem_Rename",
                Header = "Yeniden Adlandır"
            };
            MenuItem menuItem4 = new MenuItem()
            {
                Name = "menuItem_Position",
                Header = "Konumlandır"
            };
            menuItem.Click += MenuItem_Resize_Click;
            menuItem2.Click += MenuItem_Delete_Click;
            menuItem3.Click += MenuItem_Rename_Click;
            menuItem4.Click += MenuItem_Position_Click;
            ucparkPlace_contextMenu.Items.Add(menuItem);
            ucparkPlace_contextMenu.Items.Add(menuItem2);
            ucparkPlace_contextMenu.Items.Add(menuItem3);
            ucparkPlace_contextMenu.Items.Add(menuItem4);
        }

        // Settings.settings dosyasında bulunan park alanı görselini getirmek veya yeni görsel kayıt etmek için.
        private void GetParkingLootImage()
        {
            //string targetImageLocation = Properties.Settings.Default.ParkingLootImageLocation;
            //if (File.Exists(targetImageLocation) == true)
            //    this.Background = new ImageBrush(new BitmapImage(new Uri(targetImageLocation)));

            try
            {
                this.Background = Settings.GetParkingLotImage();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Dinamik MenuItem Metodları
        private void MenuItem_Position_Click(object sender, RoutedEventArgs e)
        {
            IsPositionMode = true;
            IsResizeMode = false;
        }
        private void MenuItem_Rename_Click(object sender, RoutedEventArgs e)
        {
            IsResizeMode = false;
            IsPositionMode = false;

            UC_ParkPlace selectedParkPlace = SelectedItem;
            selectedParkPlace.Content = Interaction.InputBox("Park Alanı Adını Girin", "BİLGİ GİRİŞİ", selectedParkPlace.Content.ToString());
        }
        private void MenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            IsPositionMode = false;
            IsResizeMode = false;

            SelectedItem.Visibility = Visibility.Hidden;
        }
        private void MenuItem_Resize_Click(object sender, RoutedEventArgs e)
        {
            IsResizeMode = true;
            IsPositionMode = false;
        }
        #endregion

        #region Statik MenuItem Metodları
        // Menüden Yeni Obje Oluşturmak İçin.
        private void menuItem_CreateObject_Click(object sender, RoutedEventArgs e)
        {
            UC_ParkPlace btn = new UC_ParkPlace(isEditable: true)
            {
                Name = "btn_Object",
                Width = (SelectedItem != null) ? SelectedItem.Width : 150,
                Height = (SelectedItem != null) ? SelectedItem.Height : 150,
                Content = "Yeni Park Alanı",
                AllowDrop = true,
                ContextMenu = ucparkPlace_contextMenu
            };

            btn.PreviewMouseRightButtonUp += Uc_Place_PreviewMouseRightButtonUp;

            canvas.Children.Add(btn);
            Canvas.SetTop(btn, this.ActualHeight / 2);
            Canvas.SetLeft(btn, this.ActualWidth / 2);
        }

        // Menüden Arka Plan Fotoğrafı Seçmek İçin
        private void menuItem_BackgroundImageChangeObject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Title = "Fotoğraf Seç",
                    Filter = "Fotoğraf Dosyası(*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    string targetLocation = Environment.CurrentDirectory + "/img/" + openFileDialog.SafeFileName;
                    File.Copy(openFileDialog.FileName, targetLocation, true);
                    Properties.Settings.Default.ParkingLootImageLocation = targetLocation;
                    Properties.Settings.Default.Save();

                    GetParkingLootImage();
                }
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
            }

        }

        // Yapılan Değişiklikleri Kayıt Etmek İçin
        private void menuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ParkPlace> createparkPlaces = new List<ParkPlace>();
                List<ParkPlace> updateparkPlaces = new List<ParkPlace>();
                List<int> deleteparkPlaceIds = new List<int>();
                foreach (var item in canvas.Children)
                {
                    if (item.GetType() == typeof(UC_ParkPlace))
                    {
                        UC_ParkPlace uc_parkplace = item as UC_ParkPlace;
                        if (uc_parkplace.Visibility == Visibility.Visible) // Item Görünüyorsa
                        {
                            if (uc_parkplace.ParkPlace == null) // ParkPlace nullsa yeni park alanı olarak ekle
                            {
                                createparkPlaces.Add(new ParkPlace()
                                {
                                    X = Convert.ToInt32(Canvas.GetLeft(uc_parkplace)),
                                    Y = Convert.ToInt32(Canvas.GetTop(uc_parkplace)),
                                    Width = Convert.ToInt32(uc_parkplace.Width),
                                    Height = Convert.ToInt32(uc_parkplace.Height),
                                    Name = uc_parkplace.Content.ToString(),
                                    CreationDate = DateTime.Now,
                                    IsDeleted = true
                                });
                            }
                            else // ParkPlace null değilse objeyi update yap.
                            {
                                ParkPlace p = uc_parkplace.ParkPlace;
                                p.X = Convert.ToInt32(Canvas.GetLeft(uc_parkplace));
                                p.Y = Convert.ToInt32(Canvas.GetTop(uc_parkplace));
                                p.Width = Convert.ToInt32(uc_parkplace.Width);
                                p.Height = Convert.ToInt32(uc_parkplace.Height);
                                p.Name = uc_parkplace.Content.ToString();
                                p.IsDeleted = true;
                                updateparkPlaces.Add(p);
                            }
                        }
                        else // Item Görünmüyorsa
                        {
                            if (uc_parkplace.ParkPlace != null)
                            {
                                deleteparkPlaceIds.Add(uc_parkplace.ParkPlace.Id); // Sil
                            }
                        }
                    }
                }

                if (createparkPlaces.Count > 0)
                {
                    _parkPlaceService.MultipleCreate(createparkPlaces);
                }
                if (updateparkPlaces.Count > 0)
                {
                    _parkPlaceService.MultipleUpdate(updateparkPlaces);
                }
                if (deleteparkPlaceIds.Count > 0)
                {
                    // Todo: Üşendiğim için multiplechangeisdeletefield metodunu yazmadım.
                    foreach (var item in deleteparkPlaceIds)
                    {
                        _parkPlaceService.ChangeIsDeleteField(item, true);
                    }

                    //_parkPlaceService.MultipleDelete(deleteparkPlaceIds);
                }
                MessageBox.Show("Park Alanları Kayıt Edildi!", "UYARI", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        #endregion


        // Son tıklanan itemi görmek için.
        private void Uc_Place_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectedItem = sender as UC_ParkPlace;
        }

        // Boyutlandırma - Konumlandırma
        private void canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (IsResizeMode == true) // Yeniden Boyutlandırma
                {
                    // işlem Aktifken Kullanıcıya Gösterilen Şekiller İçin
                    SelectedItem.BorderBrush = Brushes.Green;
                    SelectedItem.BorderThickness = new Thickness(3);
                    SelectedItem.Cursor = Cursors.SizeNWSE;


                    Point mPoint = Mouse.GetPosition(Application.Current.MainWindow);
                    double left = Canvas.GetLeft(SelectedItem);
                    double top = Canvas.GetTop(SelectedItem);

                    SelectedItem.Width = mPoint.X - left;
                    SelectedItem.Height = mPoint.Y - top - 70;
                }

                if (IsPositionMode == true) // Yeniden Konumlandırma
                {
                    // işlem Aktifken Kullanıcıya Gösterilen Şekiller İçin
                    SelectedItem.BorderBrush = Brushes.Green;
                    SelectedItem.BorderThickness = new Thickness(3);
                    SelectedItem.Cursor = Cursors.Cross;


                    Point mPoint = Mouse.GetPosition(Application.Current.MainWindow);
                    Canvas.SetLeft(SelectedItem, mPoint.X);
                    Canvas.SetTop(SelectedItem, mPoint.Y - 70);
                }

                if (IsPositionMode == false && IsResizeMode == false && SelectedItem != null) // Herhangi bir işlem aktif değilse
                {
                    SelectedItem.BorderBrush = new SolidColorBrush(Color.FromArgb(51, 0, 0, 0));
                    SelectedItem.BorderThickness = new Thickness(3);
                    SelectedItem.Cursor = Cursors.Arrow;
                }
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ContextMenu işlevleri aktifken sol tıklandığında işlevleri sonlandırır.
        private void canvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsResizeMode = false;
            IsPositionMode = false;
        }
    }
}