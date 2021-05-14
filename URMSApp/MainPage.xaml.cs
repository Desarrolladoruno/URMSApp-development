using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SqliteData;
using System.Data.SqlClient;
using System.Globalization;
using Windows.UI.Popups;
using URMSApp.Views;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace URMSApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// Show initial page by default.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static DispatcherTimer isTimerb = new DispatcherTimer();
        string connst = "";
        string stact = "";
        public MainPage()
        {
            this.InitializeComponent();
            BackButton.Visibility = Visibility.Collapsed;

            SqlConnection cnn = new SqlConnection();

            SqlCommand cmd = new SqlCommand();




            CultureInfo provider = CultureInfo.InvariantCulture;

            String srv = Class1.GetServer();
            String srvu = Class1.GetUser();
            String srvp = Class1.GetPass();
            String srvdb = Class1.GetDb();


            string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            try
            {

                cnn.ConnectionString = connectionString;
                cnn.Open();

                stact = "1";
                BitmapImage image = new BitmapImage(new Uri("ms-appx:///Assets/Connected.png"));
                Connection.Source = image;

            }
            catch
            {
                stact = "0";
                BitmapImage image = new BitmapImage(new Uri("ms-appx:///Assets/Disconnected.png"));
                Connection.Source = image;

            }
            cnn.Close();

            isTimerb.Tick += isTimer_Tick;
            isTimerb.Interval = new TimeSpan(0, 0, 0, 1);

                      
            // Falto agregar el Start
            isTimerb.Start();

            CentralFrame.Navigate(typeof(ItemGeneral));
        }

        private void isTimer_Tick(object sender, object e)
        {
            SqlConnection cnn = new SqlConnection();
            
            SqlCommand cmd = new SqlCommand();
            



            CultureInfo provider = CultureInfo.InvariantCulture;

            String srv = Class1.GetServer();
            String srvu = Class1.GetUser();
            String srvp = Class1.GetPass();
            String srvdb = Class1.GetDb();


            string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            try
            {
                
                cnn.ConnectionString = connectionString;
                cnn.Open();
                                
                if (stact != "1")
                {
                    BitmapImage image = new BitmapImage(new Uri("ms-appx:///Assets/Connected.png"));
                    Connection.Source = image;
                    stact = "1";
                }
            }
            catch {
                if (stact != "0")
                {
                    BitmapImage image = new BitmapImage(new Uri("ms-appx:///Assets/Disconnected.png"));
                    Connection.Source = image;
                    stact = "0";
                }

            }
            cnn.Close();

        }

        public class Log
        {
            private string Path = "";


            public Log(string Path)
            {
                this.Path = Path;
            }

            public void Add(string sLog)
            {
                CreateDirectory();
                string nombre = GetNameFile();
                string cadena = "";

                cadena += DateTime.Now + " - " + sLog + Environment.NewLine;

                StreamWriter sw = new StreamWriter(Path + "/" + nombre, true);
                sw.Write(cadena);
                sw.Close();

            }

            #region HELPER
            private string GetNameFile()
            {
                string nombre = "";

                nombre = "log_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt";

                return nombre;
            }

            private void CreateDirectory()
            {
                try
                {
                    if (!Directory.Exists(Path))
                        Directory.CreateDirectory(Path);


                }
                catch (DirectoryNotFoundException ex)
                {
                    throw new Exception(ex.Message);

                }
            }
        }


        public async void ShowDialog(String msg)
        {

            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();

        }
            /// <summary>
            /// Hamburguer Button funtionality, show interactivity.
            /// </summary>
            private void HamburguerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (CentralFrame.CanGoBack) {
                CentralFrame.GoBack();    
            }
        }

        /// <summary>
        /// Listbox options navigation.
        /// </summary>
        private void IconsListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemManListBoxItem.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;

                CentralFrame.Navigate(typeof(ItemGeneral));

            }
            else
            {
                if (ConnectListBoxItem.IsSelected)
                {
                    ItemClass.AddData("", "", "", "", "", "", "", "", "", "", "");
                    BackButton.Visibility = Visibility.Visible;
                    CentralFrame.Navigate(typeof(Connection));
                } else
                { 
                    if (PurchOrderListBoxItem.IsSelected)
                    {
                        BackButton.Visibility = Visibility.Visible;
                        CentralFrame.Navigate(typeof(PurchaseOrders)); 
                    } else {
                        if (InventoryListBoxItem.IsSelected)
                        {
                            BackButton.Visibility = Visibility.Visible;
                            CentralFrame.Navigate(typeof(ManualInventory));
                        }
                        else
                        {
                            if (CloseListBoxItem.IsSelected)
                            {
                                Environment.Exit(0);
                            }
                        }
                    }
                    
                }
            }
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
