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
using Microsoft.Data.Sqlite;
using System.ComponentModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Globalization;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace URMSApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pricing : Page
    {
        public static DispatcherTimer isTimerP = new DispatcherTimer();
        public Pricing()
        {
            this.InitializeComponent();

            isTimerP.Tick += isTimerP_Tick;
            isTimerP.Interval = new TimeSpan(0, 0, 0, 0, 1);
            

            txtPriceA.Text = PricingClass.GetPriceA().ToString();
            txtPriceB.Text = PricingClass.GetPriceB();
            txtPriceC.Text = PricingClass.GetPriceC();
            txtSalePrice2.Text = PricingClass.GetSalePrice();

            if (PricingClass.GetSaleType() == "0" || PricingClass.GetSaleType() == "")
            {
                ChkISch.IsChecked = false;

            }
            else
            {
                
                txtSalePrice2.IsEnabled = true;
                BtnEditSalePrice2.IsEnabled = true;
                ChkSEDate.IsEnabled = true;
                ChkSCH.IsEnabled = true;

                if (PricingClass.GetSaleType() == "1")
                {
                    ChkISch.IsChecked = true;
                    ChkSEDate.IsChecked = true;
                    DPStart.IsEnabled = true;
                    DPEnd.IsEnabled = true;
                    //BtnEditStart.IsEnabled = true;
                    //BtnEditEnd.IsEnabled = true;
                }
                if (PricingClass.GetSaleType() == "2")
                {
                    ChkISch.IsChecked = true;
                    ChkSCH.IsChecked = true;

                    //BtnEditShedule.IsEnabled = true;
                }
            }

            if (PricingClass.GetStar() == "")
            {
                DPStart.Date = null;
            }
            else
            {
                DateTime fa;
                fa = DateTime.Parse(PricingClass.GetStar());
                int faM = fa.Month;
                int fad = fa.Day;
                int fay = fa.Year;

                DPStart.Date = new DateTime(fay, faM, fad);
            }

            if (PricingClass.GetEnd() == "")
            {
                DPEnd.Date = null;
            }
            else
            {
                DateTime fa;
                fa = DateTime.Parse(PricingClass.GetEnd());
                int faM = fa.Month;
                int fad = fa.Day;
                int fay = fa.Year;

                DPEnd.Date = new DateTime(fay, faM, fad);
            }

            ComboSchedule.Items.Clear();
            ComboSchedule.Items.Add(PricingClass.GetSchedule());
            ComboSchedule.SelectedIndex = 0;

            isTimerP.Start();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            isTimerP.Stop();
            base.OnNavigatedFrom(e);
        }

        private void isTimerP_Tick(object sender, object e)
        {
            string sch = "";
            string fs = "";

            if (ChkISch.IsChecked == true && ChkSCH.IsChecked == true)
            {
                fs = "2";
            }
            else if (ChkISch.IsChecked == true)
            {
                fs = "1";
            }
            else if (ChkISch.IsChecked == false)
            {
                fs = "0";
            }

            String fas;
            String fes;

            if (DPStart.Date != null)
            {
                fas = DPStart.Date.Value.ToString("yyyy-MM-dd");

            }
            else
            {

                fas = "";
            }

            if (DPEnd.Date != null)
            {
                fes = DPEnd.Date.Value.ToString("yyyy-MM-dd");
            }
            else
            {

                fes = "";
            }

            try
            {
                sch = ComboSchedule.SelectedValue.ToString();
            }
            catch { sch = ""; }

            PricingClass.AddData(txtPriceA.Text, txtPriceB.Text, txtPriceC.Text, txtSalePrice2.Text, fas, fes, sch, fs);

        }

        private void ChkIS_Click(object sender, RoutedEventArgs e)
        {
            if (ChkISch.IsChecked == true)
            {
                txtSalePrice2.IsEnabled = true;
                txtSalePrice2.IsReadOnly = true;
                ChkSEDate.IsEnabled = true;
                ChkSCH.IsEnabled = true;
                BtnEditSalePrice2.IsEnabled = true;

            }
            else
            {
                txtSalePrice2.IsEnabled = false;
                ChkSEDate.IsEnabled = false;
                ChkSCH.IsChecked = false;
                ChkSEDate.IsChecked = false;
                ChkSCH.IsEnabled = false;
                DPStart.IsEnabled = false;
                DPEnd.IsEnabled = false;
                //BtnEditStart.IsEnabled = false;
                //BtnEditEnd.IsEnabled = false;
                BtnEditSalePrice2.IsEnabled = false;
                ComboSchedule.IsEnabled = false;
                //BtnEditShedule.IsEnabled = false;
                txtSalePrice2.IsEnabled = false;
                txtSalePrice2.Text = "";
                DPStart.Date = null;
                DPEnd.Date = null;
                ComboSchedule.Items.Clear();
            }
        }

        private void ChkSEDate_Click(object sender, RoutedEventArgs e)
        {
            if (ChkSEDate.IsChecked == true)
            {
                //BtnEditStart.IsEnabled = true;
                //BtnEditEnd.IsEnabled = true;
                DPStart.IsEnabled = true;
                DPEnd.IsEnabled = true;
                ChkSCH.IsChecked = false;
                ComboSchedule.IsEnabled = false;
                ComboSchedule.Items.Clear();
            }
            else
            {
                //BtnEditStart.IsEnabled = false;
                //BtnEditEnd.IsEnabled = false;
                DPStart.IsEnabled = false;
                DPEnd.IsEnabled = false;
                DPStart.Date = null;
                DPEnd.Date = null;
            }
        }

        private void ChkSCH_Click(object sender, RoutedEventArgs e)
        {
            if (ChkSCH.IsChecked == true)
            {
                //BtnEditShedule.IsEnabled = true;
                ComboSchedule.IsEnabled = true;
                ChkSEDate.IsChecked = false;
                DPStart.IsEnabled = false;
                DPEnd.IsEnabled = false;
                DPStart.Date = null;
                DPEnd.Date = null;

            }
            else
            {
                //BtnEditShedule.IsEnabled = false;
                ComboSchedule.IsEnabled = false;
                ComboSchedule.Items.Clear();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void BtnEditPriceA_Click(object sender, RoutedEventArgs e)
        {
            //txtPriceA.IsReadOnly = false;

            if (!PricingPopup.IsOpen)
            {
                Title.Text = "Price A";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                PricingPopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        private void BtnEditPriceB_Click(object sender, RoutedEventArgs e)
        {
            //txtPriceB.IsReadOnly = false;
            if (!PricingPopup.IsOpen)
            {
                Title.Text = "Price B";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                PricingPopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        private void BtnEditPriceC_Click(object sender, RoutedEventArgs e)
        {
            //txtPriceC.IsReadOnly = false;

            if (!PricingPopup.IsOpen)
            {
                Title.Text = "Price C";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                PricingPopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        private void BtnEditSalePrice_Click(object sender, RoutedEventArgs e)
        {
            txtSalePrice2.IsEnabled = true;
            //txtSalePrice.IsReadOnly = false;
            if (!PricingPopup.IsOpen)
            {
                Title.Text = "Sales Price";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                PricingPopup.IsOpen = true;
                
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        private void BtnEditStart_Click(object sender, RoutedEventArgs e)
        {
            DPStart.IsEnabled = true;

        }

        private void BtnEditEnd_Click(object sender, RoutedEventArgs e)
        {
            DPEnd.IsEnabled = true;

        }

        private void BtnEditShedule_Click(object sender, RoutedEventArgs e)
        {
            

        }

        public async void ShowDialog(String msg)
        {

            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }

        private void BtnClosepopup_Click(object sender, RoutedEventArgs e)
        {
            // close the Popup
            popupPrice.Text = "";
            if (PricingPopup != null) { PricingPopup.IsOpen = false; }
        }

        private void BtnSavePop_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Text == "Price A") { txtPriceA.Text = popupPrice.Text; }
            else
            {
                if (Title.Text == "Price B") { txtPriceB.Text = popupPrice.Text; }
                else
                {
                    if (Title.Text == "Price C") { txtPriceC.Text = popupPrice.Text; }
                    else { if (Title.Text == "Sales Price") { txtSalePrice2.Text = popupPrice.Text; } }
                }
            }
                // if the Popup is open, then close it 
                popupPrice.Text = "";
                if (PricingPopup.IsOpen) { PricingPopup.IsOpen = false; }
            
        }

        private void ComboSchedule_DropDownOpened(object sender, object e)
        {
            ComboSchedule.IsEnabled = true;

            String srv;
            String srvu;
            String srvp;
            String srvdb;
            SqlConnection cnn = new SqlConnection();
            SqlConnection cnn1 = new SqlConnection();
            SqlConnection cnn2 = new SqlConnection();
            SqlConnection cnn3 = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd1 = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();
            SqlCommand cmd3 = new SqlCommand();


            CultureInfo provider = CultureInfo.InvariantCulture;

            srv = Class1.GetServer();
            srvu = Class1.GetUser();
            srvp = Class1.GetPass();
            srvdb = Class1.GetDb();

            String connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;

            ComboSchedule.Items.Clear();
            ComboSchedule.Items.Add(" ");

            try
            {
                cnn1.ConnectionString = connectionString;
                cnn1.Open();

                cmd1.Connection = cnn1;
                cmd1.CommandText = "SELECT [ID],[Description] FROM Schedule";
                SqlDataReader dtr1 = cmd1.ExecuteReader();



                if (dtr1.HasRows)
                {
                    while (dtr1.Read())
                    {

                        ComboSchedule.Items.Add(dtr1[1]);

                    }
                }
                dtr1.Close();
                cnn1.Close();
            }
            catch { }
        }

        private void StandardPopup_Loaded(object sender, RoutedEventArgs e)
        {
            popupPrice.Focus(FocusState.Programmatic);
        }
    }
}
