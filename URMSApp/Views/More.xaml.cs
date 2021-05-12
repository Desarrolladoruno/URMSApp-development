using SqliteData;
using Microsoft.Data.Sqlite;
using System.ComponentModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Globalization;
using Windows.UI.Popups;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace URMSApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// show item properties in page depending on barcode set by user
    /// </summary>
    public sealed partial class More : Page
    {
        public static DispatcherTimer isTimerM = new DispatcherTimer();
        public More()
        {
            this.InitializeComponent();

            isTimerM.Tick += isTimerM_Tick;
            isTimerM.Interval = new TimeSpan(0, 0, 0, 0, 1);

            //if (MoreClass.GetFs().ToString() == "1")
            //{
            //    ChkFS.IsChecked = true;

            //}
            //else
            //{
            //    ChkFS.IsChecked = false;
            //}

            //ComboTagAlone.Items.Clear();
            //ComboTagAlone.Items.Add(MoreClass.GetTAItem().ToString());
            //ComboTagAlone.SelectedIndex = 0;

            ComboParent.Items.Clear();
            ComboParent.Items.Add(MoreClass.GetPItem().ToString());
            ComboParent.SelectedIndex = 0;


            ComboMessage.Items.Clear();
            ComboMessage.Items.Add(MoreClass.GetMi().ToString());
            ComboMessage.SelectedIndex = 0;

            txtChildqty.Text = MoreClass.GetCQty().ToString();
            //txtTagaloneqty.Text = MoreClass.GetTAQty().ToString();

            isTimerM.Start();

           
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            isTimerM.Stop();
            base.OnNavigatedFrom(e);
        }
        private void isTimerM_Tick(object sender, object e)
        {
            string ta;
            string pt;
            string fs = "";
            string mi;

            if (ChkFS.IsChecked == true) { fs = "1"; }

            try
            {
                ta = ComboTagAlone.SelectedValue.ToString();
            }
            catch { ta = ""; }

            try
            {
                pt = ComboParent.SelectedValue.ToString();
            }
            catch { pt = ""; }

            try
            {
                mi = ComboMessage.SelectedValue.ToString();
            }
            catch { mi = ""; }

            MoreClass.AddData(pt, txtChildqty.Text, mi);
        }

        public async void ShowDialog(String msg)
        {

            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }


        private async void BtnEditTagaloneitem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {


        }

        private async void BtnEditTagaloneqty_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            
            if (!MorePopup.IsOpen)
            {
                Title.Text = "Tag Alone Qty";
                                
                MorePopup.IsOpen = true;

                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                await Task.Delay(100);
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        private void BtnEditParentItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            
        }

        private void BtnEditChildqty_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
           
            if (!MorePopup.IsOpen)
            {
                Title.Text = "Child Qty";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                MorePopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        /// <summary>
        /// Save item properties set by the user at data base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
           
        }

        private void ChkFS_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void BtnClosepopup_Click(object sender, RoutedEventArgs e)
        {
            // close the Popup
            popupPrice.Text = "";
            if (MorePopup != null) { MorePopup.IsOpen = false; }
        }

        private void BtnSavePop_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Text == "Tag Alone Qty") { txtTagaloneqty.Text = popupPrice.Text; }
            else
            {
                if (Title.Text == "Child Qty") { txtChildqty.Text = popupPrice.Text; }
                
            }
            // if the Popup is open, then close it 
            popupPrice.Text = "";
            if (MorePopup.IsOpen) { MorePopup.IsOpen = false; }
        }

        private void ComboTagAlone_DropDownOpened(object sender, object e)
        {
            ComboTagAlone.IsEnabled = true;

            String srv;
            String srvu;
            String srvp;
            String srvdb;
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();



            CultureInfo provider = CultureInfo.InvariantCulture;

            srv = Class1.GetServer();
            srvu = Class1.GetUser();
            srvp = Class1.GetPass();
            srvdb = Class1.GetDb();

            String connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();

                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [ID],[Description] FROM Item WHERE SubDescription3 like '%CRV%'";
                SqlDataReader dtr = cmd.ExecuteReader();

                ComboTagAlone.Items.Clear();
                ComboTagAlone.Items.Add(" ");

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {

                        ComboTagAlone.Items.Add(dtr[1]);

                    }
                }
                dtr.Close();
                cnn.Close();
            }
            catch { }

        }

        private void ComboParent_DropDownOpened(object sender, object e)
        {
            ComboParent.IsEnabled = true;

            String srv;
            String srvu;
            String srvp;
            String srvdb;
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();



            CultureInfo provider = CultureInfo.InvariantCulture;

            srv = Class1.GetServer();
            srvu = Class1.GetUser();
            srvp = Class1.GetPass();
            srvdb = Class1.GetDb();

            String connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();

                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [ID],[Description] FROM Item WHERE SubDescription3 like '%PARENT%'";
                SqlDataReader dtr = cmd.ExecuteReader();

                ComboParent.Items.Clear();
                ComboParent.Items.Add(" ");

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {

                        ComboParent.Items.Add(dtr[1]);

                    }
                }
                dtr.Close();
                cnn.Close();
            }
            catch { }
        }

        private void ComboMessage_DropDownOpened(object sender, object e)
        {
            ComboMessage.IsEnabled = true;

            String srv;
            String srvu;
            String srvp;
            String srvdb;
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();



            CultureInfo provider = CultureInfo.InvariantCulture;

            srv = Class1.GetServer();
            srvu = Class1.GetUser();
            srvp = Class1.GetPass();
            srvdb = Class1.GetDb();

            String connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();

                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [ID],[Title] FROM ItemMessage";
                SqlDataReader dtr = cmd.ExecuteReader();

                ComboMessage.Items.Clear();
                ComboMessage.Items.Add(" ");

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {

                        ComboMessage.Items.Add(dtr[1]);

                    }
                }
                dtr.Close();
                cnn.Close();
            }
            catch { }
        }
    }
}
