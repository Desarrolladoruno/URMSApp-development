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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace URMSApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Discount : Page
    {
        public static DispatcherTimer isTimerD = new DispatcherTimer();
        public Discount()
        {
            this.InitializeComponent();

            ///Get ItemBarcode saved to show its discount properties
            ///by default.

            isTimerD.Tick += isTimerD_Tick;
            isTimerD.Interval = new TimeSpan(0, 0, 0, 0, 1);

            if (DiscountClass.GetMMSchema().ToString() == "" && DiscountClass.GetXYSchema().ToString() == "" && DiscountClass.GetQty1().ToString() == "" && DiscountClass.GetUnit1().ToString() == "" && DiscountClass.GetQty2().ToString() == "" && DiscountClass.GetUnit2().ToString() == "" && DiscountClass.GetQty3().ToString() == "" && DiscountClass.GetUnit3().ToString() == "")
            {
                ChkNoDiscount.IsChecked = true;

            }
            else
            {
                if (DiscountClass.GetMMSchema().ToString() != "")
                {
                    ChkMixMatch.IsChecked = true;
                    ComboMixMatch.IsEnabled = true;
                    ComboMixMatch.Items.Clear();
                    ComboMixMatch.Items.Add(DiscountClass.GetMMSchema().ToString());
                    ComboMixMatch.SelectedIndex = 0;
                }
                else
                {
                    if (DiscountClass.GetXYSchema().ToString() != "")
                    {
                        ChkBuyXYZ.IsChecked = true;
                        ComboBuyXYZ.IsEnabled = true;
                        ComboBuyXYZ.Items.Clear();
                        ComboBuyXYZ.Items.Add(DiscountClass.GetXYSchema().ToString());
                        ComboBuyXYZ.SelectedIndex = 0;
                    }
                    else
                    {
                        if (DiscountClass.GetQty1().ToString() != "" || DiscountClass.GetUnit1().ToString() != "" || DiscountClass.GetQty2().ToString() != "" || DiscountClass.GetUnit2().ToString() != "" || DiscountClass.GetQty3().ToString() != "" || DiscountClass.GetUnit3().ToString() != "")
                        {
                            ChkQuantity.IsChecked = true;
                            txtQuantity1.IsEnabled = true;
                            txtQuantity2.IsEnabled = true;
                            txtQuantity3.IsEnabled = true;
                            txtUnitP1.IsEnabled = true;
                            txtUnitP2.IsEnabled = true;
                            txtUnitP3.IsEnabled = true;
                            BtnEditQuantity1.IsEnabled = true;
                            BtnEditQuantity2.IsEnabled = true;
                            BtnEditQuantity3.IsEnabled = true;
                            BtnEditUnitP1.IsEnabled = true;
                            BtnEditUnitP2.IsEnabled = true;
                            BtnEditUnitP3.IsEnabled = true;

                            txtUnitP1.Text = DiscountClass.GetUnit1().ToString();
                            txtQuantity1.Text = DiscountClass.GetQty1().ToString();
                            txtUnitP2.Text = DiscountClass.GetUnit2().ToString();
                            txtQuantity2.Text = DiscountClass.GetQty2().ToString();
                            txtUnitP3.Text = DiscountClass.GetUnit3().ToString();
                            txtQuantity3.Text = DiscountClass.GetQty3().ToString();
                        }
                    }
                }
            }

            isTimerD.Start();

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            isTimerD.Stop();
            base.OnNavigatedFrom(e);

        }

        private void isTimerD_Tick(object sender, object e)
        {
            string mm;
            string xy;

            try
            {
                mm = ComboMixMatch.SelectedValue.ToString();
            }
            catch { mm = ""; }

            try
            {
                xy = ComboBuyXYZ.SelectedValue.ToString();
            }
            catch { xy = ""; }

            DiscountClass.AddData(mm, xy, txtQuantity1.Text, txtUnitP1.Text, txtQuantity2.Text, txtUnitP2.Text, txtQuantity3.Text, txtUnitP3.Text);
        }

        /// <summary>
        /// Manage discount or not discount properties for some item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkNoDiscount_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SqlConnection cnn = new SqlConnection();
            SqlConnection cnnD = new SqlConnection();
            SqlConnection cnne = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdD = new SqlCommand();
            SqlCommand cmde = new SqlCommand();

            String srv = Class1.GetServer();
            String srvu = Class1.GetUser();
            String srvp = Class1.GetPass();
            String srvdb = Class1.GetDb();

            string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();

                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [QuantityDiscountID] FROM Item Where [ItemLookupCode] = '" + ItemClass.GetItemBarcode().ToString() + "'";
                SqlDataReader dtr = cmd.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        if (dtr[0].ToString() != "0")
                        {
                            try
                            {
                                cnnD.ConnectionString = connectionString;
                                cnnD.Open();

                                cmdD.Connection = cnnD;
                                cmdD.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount Where [ID] = '" + dtr[0].ToString() + "'";
                                SqlDataReader dtrD = cmdD.ExecuteReader();

                                if (dtrD.HasRows)
                                {
                                    while (dtrD.Read())
                                    {
                                        if (dtrD[2].ToString() == "0")
                                        {
                                            cnne.ConnectionString = connectionString;
                                            cnne.Open();
                                            cmde.Connection = cnne;
                                            cmde.CommandText = "DELETE FROM QuantityDiscount WHERE ID = '" + dtr[0].ToString() + "'";
                                            cmde.ExecuteNonQuery();

                                            cnne.Close();
                                        }

                                    }
                                }
                                dtrD.Close();
                                cnnD.Close();
                            }
                            catch { }
                        }

                    }

                }
            }
            catch { }

            txtQuantity1.Text = "";
            txtUnitP1.Text = "";
            txtQuantity2.Text = "";
            txtUnitP2.Text = "";
            txtQuantity3.Text = "";
            txtUnitP3.Text = "";

            ComboBuyXYZ.Items.Clear();
            ComboMixMatch.Items.Clear();

            if (ChkNoDiscount.IsChecked == true)
            {

                ChkMixMatch.IsChecked = false;
                ChkBuyXYZ.IsChecked = false;
                ChkQuantity.IsChecked = false;
                ComboMixMatch.IsEnabled = false;
                ComboBuyXYZ.IsEnabled = false;
                BtnEditBuyXYZ.IsEnabled = false;
                BtnEditMixMatch.IsEnabled = false;
                txtQuantity1.IsEnabled = false;
                BtnEditQuantity1.IsEnabled = false;
                txtQuantity2.IsEnabled = false;
                BtnEditQuantity2.IsEnabled = false;
                txtQuantity3.IsEnabled = false;
                BtnEditQuantity3.IsEnabled = false;
                txtUnitP1.IsEnabled = false;
                BtnEditUnitP1.IsEnabled = false;
                txtUnitP2.IsEnabled = false;
                BtnEditUnitP2.IsEnabled = false;
                txtUnitP3.IsEnabled = false;
                BtnEditUnitP3.IsEnabled = false;
            }
            else
            {
                ChkNoDiscount.IsChecked = true;
                ChkMixMatch.IsChecked = false;
                ChkBuyXYZ.IsChecked = false;
                ChkQuantity.IsChecked = false;
                ComboMixMatch.IsEnabled = false;
                ComboBuyXYZ.IsEnabled = false;
                BtnEditBuyXYZ.IsEnabled = false;
                BtnEditMixMatch.IsEnabled = false;
                txtQuantity1.IsEnabled = false;
                BtnEditQuantity1.IsEnabled = false;
                txtQuantity2.IsEnabled = false;
                BtnEditQuantity2.IsEnabled = false;
                txtQuantity3.IsEnabled = false;
                BtnEditQuantity3.IsEnabled = false;
                txtUnitP1.IsEnabled = false;
                BtnEditUnitP1.IsEnabled = false;
                txtUnitP2.IsEnabled = false;
                BtnEditUnitP2.IsEnabled = false;
                txtUnitP3.IsEnabled = false;
                BtnEditUnitP3.IsEnabled = false;
            }
        }

        /// <summary>
        /// Asyncronic method to show string messages 
        /// </summary>
        /// <param name="msg">message to show in popup</param>
        public async void ShowDialog(String msg)
        {

            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }

        /// <summary>
        /// Controle Mix and Match discount options for some item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkMixMatch_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SqlConnection cnn = new SqlConnection();
            SqlConnection cnnD = new SqlConnection();
            SqlConnection cnne = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdD = new SqlCommand();
            SqlCommand cmde = new SqlCommand();

            String srv = Class1.GetServer();
            String srvu = Class1.GetUser();
            String srvp = Class1.GetPass();
            String srvdb = Class1.GetDb();

            string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();

                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [QuantityDiscountID] FROM Item Where [ItemLookupCode] = '" + ItemClass.GetItemBarcode().ToString() + "'";
                SqlDataReader dtr = cmd.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        if (dtr[0].ToString() != "0")
                        {
                            try
                            {
                                cnnD.ConnectionString = connectionString;
                                cnnD.Open();

                                cmdD.Connection = cnnD;
                                cmdD.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount Where [ID] = '" + dtr[0].ToString() + "'";
                                SqlDataReader dtrD = cmdD.ExecuteReader();

                                if (dtrD.HasRows)
                                {
                                    while (dtrD.Read())
                                    {
                                        if (dtrD[2].ToString() == "0")
                                        {
                                            cnne.ConnectionString = connectionString;
                                            cnne.Open();
                                            cmde.Connection = cnne;
                                            cmde.CommandText = "DELETE FROM QuantityDiscount WHERE ID = '" + dtr[0].ToString() + "'";
                                            cmde.ExecuteNonQuery();

                                            cnne.Close();
                                        }

                                    }
                                }
                                dtrD.Close();
                                cnnD.Close();
                            }
                            catch { }
                        }

                    }

                }
            }
            catch { }

            txtQuantity1.Text = "";
            txtUnitP1.Text = "";
            txtQuantity2.Text = "";
            txtUnitP2.Text = "";
            txtQuantity3.Text = "";
            txtUnitP3.Text = "";

            ComboBuyXYZ.Items.Clear();

            if (ChkMixMatch.IsChecked == true)
            {
                ChkNoDiscount.IsChecked = false;
                ChkBuyXYZ.IsChecked = false;
                ChkQuantity.IsChecked = false;
                ComboMixMatch.IsEnabled = true;
                ComboBuyXYZ.IsEnabled = false;
                BtnEditBuyXYZ.IsEnabled = false;
                BtnEditMixMatch.IsEnabled = true;
                txtQuantity1.IsEnabled = false;
                BtnEditQuantity1.IsEnabled = false;
                txtQuantity2.IsEnabled = false;
                BtnEditQuantity2.IsEnabled = false;
                txtQuantity3.IsEnabled = false;
                BtnEditQuantity3.IsEnabled = false;
                txtUnitP1.IsEnabled = false;
                BtnEditUnitP1.IsEnabled = false;
                txtUnitP2.IsEnabled = false;
                BtnEditUnitP2.IsEnabled = false;
                txtUnitP3.IsEnabled = false;
                BtnEditUnitP3.IsEnabled = false;
            }
            else
            {
                ChkNoDiscount.IsChecked = false;
                ChkMixMatch.IsChecked = true;
                ChkBuyXYZ.IsChecked = false;
                ChkQuantity.IsChecked = false;
                ComboMixMatch.IsEnabled = true;
                ComboBuyXYZ.IsEnabled = false;
                BtnEditBuyXYZ.IsEnabled = false;
                BtnEditMixMatch.IsEnabled = true;
                txtQuantity1.IsEnabled = false;
                BtnEditQuantity1.IsEnabled = false;
                txtQuantity2.IsEnabled = false;
                BtnEditQuantity2.IsEnabled = false;
                txtQuantity3.IsEnabled = false;
                BtnEditQuantity3.IsEnabled = false;
                txtUnitP1.IsEnabled = false;
                BtnEditUnitP1.IsEnabled = false;
                txtUnitP2.IsEnabled = false;
                BtnEditUnitP2.IsEnabled = false;
                txtUnitP3.IsEnabled = false;
                BtnEditUnitP3.IsEnabled = false;
            }
        }

        /// <summary>
        /// Controle Buy X and take Y for Z price discount options for some item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkBuyXYZ_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SqlConnection cnn = new SqlConnection();
            SqlConnection cnnD = new SqlConnection();
            SqlConnection cnne = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdD = new SqlCommand();
            SqlCommand cmde = new SqlCommand();

            String srv = Class1.GetServer();
            String srvu = Class1.GetUser();
            String srvp = Class1.GetPass();
            String srvdb = Class1.GetDb();

            string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();

                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [QuantityDiscountID] FROM Item Where [ItemLookupCode] = '" + ItemClass.GetItemBarcode().ToString() + "'";
                SqlDataReader dtr = cmd.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        if (dtr[0].ToString() != "0")
                        {
                            try
                            {
                                cnnD.ConnectionString = connectionString;
                                cnnD.Open();

                                cmdD.Connection = cnnD;
                                cmdD.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount Where [ID] = '" + dtr[0].ToString() + "'";
                                SqlDataReader dtrD = cmdD.ExecuteReader();

                                if (dtrD.HasRows)
                                {
                                    while (dtrD.Read())
                                    {
                                        if (dtrD[2].ToString() == "0")
                                        {
                                            cnne.ConnectionString = connectionString;
                                            cnne.Open();
                                            cmde.Connection = cnne;
                                            cmde.CommandText = "DELETE FROM QuantityDiscount WHERE ID = '" + dtr[0].ToString() + "'";
                                            cmde.ExecuteNonQuery();

                                            cnne.Close();
                                        }

                                    }
                                }
                                dtrD.Close();
                                cnnD.Close();
                            }
                            catch { }
                        }

                    }

                }
            }
            catch { }

            txtQuantity1.Text = "";
            txtUnitP1.Text = "";
            txtQuantity2.Text = "";
            txtUnitP2.Text = "";
            txtQuantity3.Text = "";
            txtUnitP3.Text = "";

            ComboMixMatch.Items.Clear();

            if (ChkBuyXYZ.IsChecked == true)
            {
                ChkMixMatch.IsChecked = false;
                ChkNoDiscount.IsChecked = false;
                ChkQuantity.IsChecked = false;
                ComboMixMatch.IsEnabled = false;
                ComboBuyXYZ.IsEnabled = true;
                BtnEditBuyXYZ.IsEnabled = true;
                BtnEditMixMatch.IsEnabled = false;
                txtQuantity1.IsEnabled = false;
                BtnEditQuantity1.IsEnabled = false;
                txtQuantity2.IsEnabled = false;
                BtnEditQuantity2.IsEnabled = false;
                txtQuantity3.IsEnabled = false;
                BtnEditQuantity3.IsEnabled = false;
                txtUnitP1.IsEnabled = false;
                BtnEditUnitP1.IsEnabled = false;
                txtUnitP2.IsEnabled = false;
                BtnEditUnitP2.IsEnabled = false;
                txtUnitP3.IsEnabled = false;
                BtnEditUnitP3.IsEnabled = false;
            }
            else
            {
                ChkNoDiscount.IsChecked = false;
                ChkMixMatch.IsChecked = false;
                ChkBuyXYZ.IsChecked = true;
                ChkQuantity.IsChecked = false;
                ComboMixMatch.IsEnabled = false;
                ComboBuyXYZ.IsEnabled = true;
                BtnEditBuyXYZ.IsEnabled = true;
                BtnEditMixMatch.IsEnabled = false;
                txtQuantity1.IsEnabled = false;
                BtnEditQuantity1.IsEnabled = false;
                txtQuantity2.IsEnabled = false;
                BtnEditQuantity2.IsEnabled = false;
                txtQuantity3.IsEnabled = false;
                BtnEditQuantity3.IsEnabled = false;
                txtUnitP1.IsEnabled = false;
                BtnEditUnitP1.IsEnabled = false;
                txtUnitP2.IsEnabled = false;
                BtnEditUnitP2.IsEnabled = false;
                txtUnitP3.IsEnabled = false;
                BtnEditUnitP3.IsEnabled = false;
            }
        }

        /// <summary>
        /// Use a quantity table for a discount for some item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkQuantity_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ComboBuyXYZ.Items.Clear();
            ComboMixMatch.Items.Clear();

            if (ChkQuantity.IsChecked == true)
            {
                ChkMixMatch.IsChecked = false;
                ChkBuyXYZ.IsChecked = false;
                ChkNoDiscount.IsChecked = false;
                ComboMixMatch.IsEnabled = false;
                ComboBuyXYZ.IsEnabled = false;
                BtnEditBuyXYZ.IsEnabled = false;
                BtnEditMixMatch.IsEnabled = false;
                txtQuantity1.IsEnabled = false;
                BtnEditQuantity1.IsEnabled = true;
                txtQuantity2.IsEnabled = false;
                BtnEditQuantity2.IsEnabled = true;
                txtQuantity3.IsEnabled = false;
                BtnEditQuantity3.IsEnabled = true;
                txtUnitP1.IsEnabled = false;
                BtnEditUnitP1.IsEnabled = true;
                txtUnitP2.IsEnabled = false;
                BtnEditUnitP2.IsEnabled = true;
                txtUnitP3.IsEnabled = false;
                BtnEditUnitP3.IsEnabled = true;

            }
            else
            {
                ChkNoDiscount.IsChecked = false;
                ChkMixMatch.IsChecked = false;
                ChkBuyXYZ.IsChecked = false;
                ChkQuantity.IsChecked = true;
                ComboMixMatch.IsEnabled = false;
                ComboBuyXYZ.IsEnabled = false;
                BtnEditBuyXYZ.IsEnabled = false;
                BtnEditMixMatch.IsEnabled = false;
                txtQuantity1.IsEnabled = false;
                BtnEditQuantity1.IsEnabled = true;
                txtQuantity2.IsEnabled = false;
                BtnEditQuantity2.IsEnabled = true;
                txtQuantity3.IsEnabled = false;
                BtnEditQuantity3.IsEnabled = true;
                txtUnitP1.IsEnabled = false;
                BtnEditUnitP1.IsEnabled = true;
                txtUnitP2.IsEnabled = false;
                BtnEditUnitP2.IsEnabled = true;
                txtUnitP3.IsEnabled = false;
                BtnEditUnitP3.IsEnabled = true;

            }
        }

        /// <summary>
        /// Allow user define quantities and properties for Mix and Match discount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditMixMatch_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Allow user define quantities and properties for Buy X and Y for Z price discount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditBuyXYZ_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Set quantities and price for a quantity table for some item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditQuantity1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            txtQuantity1.IsEnabled = true;

            if (!DiscountPopup.IsOpen)
            {
                Title.Text = "Quantity 1";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                DiscountPopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        private void BtnEditQuantity2_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            txtQuantity2.IsEnabled = true;

            if (!DiscountPopup.IsOpen)
            {
                Title.Text = "Quantity 2";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                DiscountPopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        private void BtnEditQuantity3_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            txtQuantity3.IsEnabled = true;


            if (!DiscountPopup.IsOpen)
            {
                Title.Text = "Quantity 3";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                DiscountPopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        private void BtnEditUnitP1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            txtUnitP1.IsEnabled = true;

            if (!DiscountPopup.IsOpen)
            {
                Title.Text = "Unit Price 1";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                DiscountPopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        private void BtnEditUnitP2_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            txtUnitP2.IsEnabled = true;

            if (!DiscountPopup.IsOpen)
            {
                Title.Text = "Unit Price 2";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                DiscountPopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        private void BtnEditUnitP3_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            txtUnitP3.IsEnabled = true;

            if (!DiscountPopup.IsOpen)
            {
                Title.Text = "Unit Price 3";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                DiscountPopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        /// <summary>
        /// Save permanently in configure data base the properties and
        /// values set by the user for some item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            String sc = ItemClass.GetItemBarcode();

            if (sc != "")
            {

                String srv = "";
                String srvu = "";
                String srvp = "";
                String srvdb = "";
                SqlConnection cnn = new SqlConnection();
                SqlConnection cnn1 = new SqlConnection();
                SqlConnection cnn2 = new SqlConnection();
                SqlConnection cnn3 = new SqlConnection();
                SqlConnection cnn4 = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd1 = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                SqlCommand cmd3 = new SqlCommand();
                SqlCommand cmd4 = new SqlCommand();
                CultureInfo provider = CultureInfo.InvariantCulture;

                srv = Class1.GetServer();
                srvu = Class1.GetUser();
                srvp = Class1.GetPass();
                srvdb = Class1.GetDb();
                String td = "0";

                String connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                try
                {

                    if (ChkNoDiscount.IsChecked == true)
                    {
                        td = "0";
                    }
                    else
                    {
                        if (ChkMixMatch.IsChecked == true)
                        {
                            try
                            {
                                cnn.ConnectionString = connectionString;
                                cnn.Open();

                                cmd.Connection = cnn;
                                cmd.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount Where [Description] = '" + ComboMixMatch.SelectedValue.ToString() + "'";
                                SqlDataReader dtr = cmd.ExecuteReader();


                                if (dtr.HasRows)
                                {
                                    while (dtr.Read())
                                    {
                                        td = dtr[0].ToString();
                                    }
                                }
                                dtr.Close();
                                cnn.Close();
                            }
                            catch { }
                        }
                        else
                        {
                            if (ChkBuyXYZ.IsChecked == true)
                            {
                                try
                                {
                                    cnn2.ConnectionString = connectionString;
                                    cnn2.Open();

                                    cmd2.Connection = cnn2;
                                    cmd2.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount Where [Description] = '" + ComboBuyXYZ.SelectedValue.ToString() + "'";
                                    SqlDataReader dtr2 = cmd2.ExecuteReader();


                                    if (dtr2.HasRows)
                                    {
                                        while (dtr2.Read())
                                        {
                                            td = dtr2[0].ToString();
                                        }
                                    }
                                    dtr2.Close();
                                    cnn2.Close();
                                }
                                catch { }
                            }
                        }
                    }

                    if (ChkQuantity.IsChecked == true)
                    {
                        if (txtQuantity1.Text == "")
                        {
                            txtQuantity1.Text = "0";
                        }
                        if (txtQuantity2.Text == "")
                        {
                            txtQuantity2.Text = "0";
                        }
                        if (txtQuantity3.Text == "")
                        {
                            txtQuantity3.Text = "0";
                        }
                        if (txtUnitP1.Text == "")
                        {
                            txtUnitP1.Text = "0";
                        }
                        if (txtUnitP2.Text == "")
                        {
                            txtUnitP2.Text = "0";
                        }
                        if (txtUnitP3.Text == "")
                        {
                            txtUnitP3.Text = "0";
                        }

                        cnn4.ConnectionString = connectionString;
                        cnn4.Open();
                        cmd4.Connection = cnn4;
                        cmd4.CommandText = "INSERT INTO [dbo].[QuantityDiscount]([Description],[HQID],[DiscountOddItems]" +
                       ",[Quantity1],[Price1],[Price1A],[Price1B],[Price1C],[Quantity2],[Price2],[Price2A],[Price2B],[Price2C],[Quantity3]" +
                       ",[Price3],[Price3A],[Price3B],[Price3C],[Quantity4],[Price4],[Price4A],[Price4B],[Price4C],[Type],[PercentOffPrice1]" +
                       ",[PercentOffPrice1A],[PercentOffPrice1B],[PercentOffPrice1C],[PercentOffPrice2],[PercentOffPrice2A],[PercentOffPrice2B]" +
                       ",[PercentOffPrice2C],[PercentOffPrice3],[PercentOffPrice3A],[PercentOffPrice3B],[PercentOffPrice3C],[PercentOffPrice4]" +
                       ",[PercentOffPrice4A],[PercentOffPrice4B],[PercentOffPrice4C]) VALUES ('',0,0," + txtQuantity1.Text + "," + txtUnitP1.Text + "," +
                       "0.00,0.00,0.00," + txtQuantity2.Text + " ," + txtUnitP2.Text + ",0.00,0.00,0.00," + txtQuantity3.Text + "," + txtUnitP3.Text + "," +
                       "0.00,0.00,0.00,0,0.00,0.00,0.00,0.00,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)";

                        cmd4.ExecuteNonQuery();
                        cnn4.Close();


                        try
                        {
                            cnn3.ConnectionString = connectionString;
                            cnn3.Open();

                            cmd3.Connection = cnn3;
                            cmd3.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount";
                            SqlDataReader dtr3 = cmd3.ExecuteReader();


                            if (dtr3.HasRows)
                            {
                                while (dtr3.Read())
                                {
                                    td = dtr3[0].ToString();
                                }
                            }
                            dtr3.Close();
                            cnn3.Close();

                        }
                        catch { }

                        SqlConnection cnndis = new SqlConnection();
                        SqlCommand cmddis = new SqlCommand();
                        cnndis.ConnectionString = connectionString;
                        cnndis.Open();
                        cmddis.Connection = cnndis;
                        cmddis.CommandText = "UPDATE Item SET [QuantityDiscountID] = " + td + " WHERE [ItemLookupCode] = '" + sc + "'";
                        cmddis.ExecuteNonQuery();

                        cnndis.Close();

                        String msg;
                        msg = "Changes have been applied";
                        ShowDialog(msg);
                    }
                    else
                    {
                        SqlConnection cnndisc = new SqlConnection();
                        SqlCommand cmddisc = new SqlCommand();

                        cnndisc.ConnectionString = connectionString;
                        cnndisc.Open();
                        cmddisc.Connection = cnndisc;
                        cmddisc.CommandText = "UPDATE Item SET [QuantityDiscountID] = " + td + " WHERE [ItemLookupCode] = '" + sc + "'";
                        cmddisc.ExecuteNonQuery();

                        cnndisc.Close();

                        String msg;
                        msg = "Changes have been applied";
                        ShowDialog(msg);
                    }
                }
                catch (SqlException e3)
                {
                    String msg3;
                    msg3 = "ERROR " + e3.ToString();
                    ShowDialog(msg3);
                }
            }
            else
            {
                String msg4;
                msg4 = "Fill Scan Barcode Field";
                ShowDialog(msg4);
            }

            txtQuantity1.Text = "";
            txtQuantity2.Text = "";
            txtQuantity3.Text = "";
            txtUnitP1.Text = "";
            txtUnitP2.Text = "";
            txtUnitP3.Text = "";

            ChkBuyXYZ.IsChecked = false;
            ChkMixMatch.IsChecked = false;
            ChkNoDiscount.IsChecked = false;
            ChkQuantity.IsChecked = false;

            ComboBuyXYZ.Items.Clear();
            ComboMixMatch.Items.Clear();
            //ItemClass.AddData("");

        }

        /// <summary>
        /// Send information set by de user on poup to the corresponding
        /// object of the principal page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSavePop_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Title.Text == "Quantity 1") { txtQuantity1.Text = popupPrice.Text; }
            else
            {
                if (Title.Text == "Quantity 2") { txtQuantity2.Text = popupPrice.Text; }
                else
                {
                    if (Title.Text == "Quantity 3") { txtQuantity3.Text = popupPrice.Text; }
                    else
                    {
                        if (Title.Text == "Unit Price 1") { txtUnitP1.Text = popupPrice.Text; }
                        else
                        {
                            if (Title.Text == "Unit Price 2") { txtUnitP2.Text = popupPrice.Text; }
                            else
                            {
                                if (Title.Text == "Unit Price 3") { txtUnitP3.Text = popupPrice.Text; }
                            }
                        }
                    }
                }
            }
            // if the Popup is open, then close it 
            popupPrice.Text = "";
            if (DiscountPopup.IsOpen) { DiscountPopup.IsOpen = false; }

        }

        private void BtnClosepopup_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // close the Popup
            popupPrice.Text = "";
            if (DiscountPopup != null) { DiscountPopup.IsOpen = false; }
        }

        private void ComboMixMatch_DropDownOpened(object sender, object e)
        {
            ComboMixMatch.IsEnabled = true;

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

            ComboMixMatch.Items.Clear();
            ComboMixMatch.Items.Add(" ");
            try
            {
                cnn1.ConnectionString = connectionString;
                cnn1.Open();

                cmd1.Connection = cnn1;
                cmd1.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount Where [Type] = 1 OR [Type] = 3";
                SqlDataReader dtr1 = cmd1.ExecuteReader();

                if (dtr1.HasRows)
                {
                    while (dtr1.Read())
                    {

                        ComboMixMatch.Items.Add(dtr1[1]);

                    }
                }
                dtr1.Close();
                cnn1.Close();
            }
            catch
            {

            }
        }

        private void ComboBuyXYZ_DropDownOpened(object sender, object e)
        {
            ComboBuyXYZ.IsEnabled = true;

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

            ComboBuyXYZ.Items.Clear();
            ComboBuyXYZ.Items.Add(" ");

            try
            {
                cnn1.ConnectionString = connectionString;
                cnn1.Open();

                cmd1.Connection = cnn1;
                cmd1.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount Where [Type] = 2 OR [Type] = 4";
                SqlDataReader dtr1 = cmd1.ExecuteReader();

                if (dtr1.HasRows)
                {
                    while (dtr1.Read())
                    {

                        ComboBuyXYZ.Items.Add(dtr1[1]);

                    }
                }
                dtr1.Close();
                cnn1.Close();
            }
            catch
            {

            }
        }

        private void DiscountPopup_Loaded(object sender, RoutedEventArgs e)
        {
            popupPrice.Focus(FocusState.Programmatic);
        }
    }
}
