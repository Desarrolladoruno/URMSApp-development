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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZXing.Mobile;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace URMSApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class General : Page
    {
        //UIElement customScanElement = null;
        MobileBarcodeScanner scanner;
        private UIElement _overlay = null;
        public static DispatcherTimer isTimer = new DispatcherTimer();
        string inicio = "";
        string idinicio = "";
        public General()
        {
            this.InitializeComponent();
            this.Loaded += this.general_loaded;

            isTimer.Tick += isTimer_Tick;
            isTimer.Interval = new TimeSpan(0, 0, 0, 0,1);

            //Create a new instance of our scanner
            scanner = new MobileBarcodeScanner(this.Dispatcher);
            scanner.Dispatcher = this.Dispatcher;
            scanner.RootFrame = this.Frame;
            scanner.AutoFocus();

            /// Consulting at database item properties and setting them
            // / at general page.

            if (ItemClass.GetFoodstamp().ToString() == "1")
            {
                ChkFS.IsChecked = true;

            }
            else
            {
                ChkFS.IsChecked = false;
            }

            ComboTagAlone.Items.Clear();
            ComboTagAlone.Items.Add(ItemClass.GetTag().ToString());
            ComboTagAlone.SelectedIndex = 0;
            txtTagaloneqty.Text = ItemClass.GetTagq().ToString();

            txtScanBarcode.Text = ItemClass.GetItemBarcode();
            txtDescription.Text = ItemClass.GetDescription();
            txtItemNumber.Text = ItemClass.GetItemNumber();
            ComboDepartment.Items.Clear();
            ComboDepartment.Items.Add(ItemClass.GetDepartment());
            ComboDepartment.SelectedIndex = 0;

            ComboCategory.Items.Clear();
            ComboCategory.Items.Add(ItemClass.GetCategory());
            ComboCategory.SelectedIndex = 0;

            ComboTax.Items.Clear();
            ComboTax.Items.Add(ItemClass.GetTax());
            ComboTax.SelectedIndex = 0;

            txtCost.Text = ItemClass.GetCost();
            txtPrice.Text = ItemClass.GetPrice();



            isTimer.Start();


        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            isTimer.Stop();
            base.OnNavigatedFrom(e);
        }

        private void isTimer_Tick(object sender, object e)
        {
            string dept = "";
            string cat = "";
            string ta = "";

            string tag;
            string pt;
            string fs = "";
            string mi;

            if (ChkFS.IsChecked == true) { fs = "1"; }

            try
            {
                tag = ComboTagAlone.SelectedValue.ToString();
            }
            catch { tag = ""; }

          
            try
            {
                dept = ComboDepartment.SelectedValue.ToString();
            }
            catch { dept = ""; }

            try
            {
                cat = ComboCategory.SelectedValue.ToString();
            }
            catch { cat = ""; }

            try
            {
                ta = ComboTax.SelectedValue.ToString();
            }
            catch { ta = ""; }

            ItemClass.AddData(txtScanBarcode.Text, txtDescription.Text, txtItemNumber.Text, dept, cat, ta, txtCost.Text, txtPrice.Text,tag, txtTagaloneqty.Text,fs);
        }



        private void general_loaded(object sender, RoutedEventArgs e) {

            txtScanBarcode.InputScope = new InputScope()
            {
                Names = { new InputScopeName(InputScopeNameValue.Number) }
            };


            txtScanBarcode.Focus(FocusState.Programmatic);

        }


        /// <summary>
        /// Button Scan's click method to open a new page for
        /// read barcodes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnScan_Click(object sender, RoutedEventArgs e)
        {

            ItemClass.AddData(txtScanBarcode.Text, "", "", "", "", "", "", "", "", "", "");



            if (_overlay == null)
            {
                _overlay = this.Overlay.Children[0];
                this.Overlay.Children.RemoveAt(0);
            }

            this.ButtonCancel.Click += (s, e2) =>
            {
                scanner.Cancel();
            };


            scanner.CustomOverlay = _overlay;
            scanner.UseCustomOverlay = true;

            //We can customize the top and bottom text of our default overlay
            scanner.TopText = "Hold camera up to barcode";
            scanner.BottomText = "Camera will automatically scan barcode\r\n\r\nPress the 'Back' button to Cancel";


            var result = await scanner.Scan();

            if (result != null)
            {
                txtScanBarcode.Text = result.Text;

                ItemClass.AddData(txtScanBarcode.Text, "", "", "", "", "", "", "", "", "", "");

            }
        }



        public async void ShowDialog(String msg)
        {

            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }

        /// <summary>
        /// Prepare the page to recive information for create a new item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNewItem_Click(object sender, RoutedEventArgs e)
        {


            //txtDescription.IsReadOnly = true;
            //txtItemNumber.IsReadOnly = true;
            //txtCost.IsReadOnly = true;
            //txtPrice.IsReadOnly = true;

            txtScanBarcode.Text = "";
            txtDescription.Text = "";
            txtItemNumber.Text = "";

            ComboDepartment.Items.Clear();
            ComboCategory.Items.Clear();

            ComboTax.Items.Clear();


            txtCost.Text = "";
            txtPrice.Text = "";


            ItemClass.AddData("", "", "", "", "", "", "", "", "", "", "");
            PricingClass.AddData("", "", "", "", "", "", "", "");
            DiscountClass.AddData("", "", "", "", "", "", "", "");
            MoreClass.AddData("", "", "");

            txtScanBarcode.Focus(FocusState.Programmatic);

        }


        /// <summary>
        /// Consult some barcode in database a retrive properties
        /// of the item to show them at this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLookItem_Click(object sender, RoutedEventArgs e)
        {
            string swic = "";

            if (txtScanBarcode.Text != "")
            {
                SqlConnection cnn = new SqlConnection();
                SqlConnection cnn1 = new SqlConnection();
                SqlConnection cnn2 = new SqlConnection();
                SqlConnection cnn3 = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd1 = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                SqlCommand cmd3 = new SqlCommand();

                
                
                CultureInfo provider = CultureInfo.InvariantCulture;

                String srv = Class1.GetServer();
                String srvu = Class1.GetUser();
                String srvp = Class1.GetPass();
                String srvdb = Class1.GetDb();

                string ic = txtScanBarcode.Text;

                ItemClass.AddData(ic, "", "", "", "", "", "", "", "", "", "");

                string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                try
                {

                    cnn.ConnectionString = connectionString;
                    cnn.Open();

                    cmd.Connection = cnn;
                    swic = "1";
                }
                catch
                {
                    swic = "";
                    String msg;
                    msg = "Verify Connection";
                    ShowDialog(msg);
                }

                if (swic == "1") { 
                cmd.CommandText = "SELECT [ItemLookupCode],[Description],[ID],[DepartmentID],[CategoryID],[TaxID],[Cost],[Price]," +
                    " [PriceA],[PriceB],[PriceC],[SalePrice],[SaleStartDate],[SaleEndDate],[SaleScheduleID],[SaleType], [QuantityDiscountID]," +
                    " [Foodstampable],[TagAlongItem],[TagAlongQuantity],[ParentItem],[ParentQuantity],[MessageID] FROM Item Where [ItemLookupCode] = '" + ic + "'";
                    SqlDataReader dtr = cmd.ExecuteReader();

                    if (dtr.HasRows)
                    {
                        //txtbTypeSave.Text = "";
                        while (dtr.Read())
                        {

                            txtDescription.Text = dtr[1].ToString();
                            txtItemNumber.Text = dtr[2].ToString();
                            try
                            {
                                cnn1.ConnectionString = connectionString;
                                cnn1.Open();

                                cmd1.Connection = cnn1;
                                cmd1.CommandText = "SELECT [ID],[Name] FROM Department Where [ID] = '" + dtr[3].ToString() + "'";
                                SqlDataReader dtr1 = cmd1.ExecuteReader();


                                if (dtr1.HasRows)
                                {
                                    while (dtr1.Read())
                                    {
                                        ComboDepartment.Items.Clear();
                                        ComboDepartment.Items.Add(dtr1[1]);
                                        ComboDepartment.SelectedIndex = 0;

                                    }
                                }
                                dtr1.Close();
                                cnn1.Close();
                            }
                            catch { }

                            try
                            {
                                cnn2.ConnectionString = connectionString;
                                cnn2.Open();

                                cmd2.Connection = cnn2;
                                cmd2.CommandText = "SELECT [ID],[Name] FROM Category Where [ID] = '" + dtr[4].ToString() + "'";
                                SqlDataReader dtr2 = cmd2.ExecuteReader();

                                if (dtr2.HasRows)
                                {
                                    while (dtr2.Read())
                                    {
                                        ComboCategory.Items.Clear();
                                        ComboCategory.Items.Add(dtr2[1]);
                                        ComboCategory.SelectedIndex = 0;
                                    }
                                }
                                dtr2.Close();
                                cnn2.Close();
                            }
                            catch
                            {
                            }

                            try
                            {
                                cnn3.ConnectionString = connectionString;
                                cnn3.Open();

                                cmd3.Connection = cnn3;
                                cmd3.CommandText = "SELECT [ID],[Description] FROM ItemTax Where [ID] = '" + dtr[5].ToString() + "'";
                                SqlDataReader dtr3 = cmd3.ExecuteReader();

                                if (dtr3.HasRows)
                                {
                                    while (dtr3.Read())
                                    {
                                        ComboTax.Items.Clear();
                                        ComboTax.Items.Add(dtr3[1]);
                                        ComboTax.SelectedIndex = 0;
                                    }
                                }
                                dtr3.Close();
                                cnn3.Close();
                            }
                            catch
                            {
                            }

                            txtCost.Text = dtr.GetDecimal(6).ToString("0.00");
                            txtPrice.Text = dtr.GetDecimal(7).ToString("0.00");

                            SqlConnection cnnsch = new SqlConnection();
                            SqlCommand cmdsch = new SqlCommand();
                            String sch = "";

                            try
                            {
                                cnnsch.ConnectionString = connectionString;
                                cnnsch.Open();

                                cmdsch.Connection = cnnsch;
                                cmdsch.CommandText = "SELECT [ID],[Description] FROM Schedule Where [ID] = '" + dtr[14].ToString() + "'";
                                SqlDataReader dtrsch = cmdsch.ExecuteReader();

                                if (dtrsch.HasRows)
                                {
                                    while (dtrsch.Read())
                                    {
                                        sch = dtrsch[1].ToString();
                                    }
                                }
                                dtrsch.Close();
                                cnnsch.Close();
                            }
                            catch { }

                            SqlConnection cnnD = new SqlConnection();
                            SqlCommand cmdD = new SqlCommand();
                            String MM = "";
                            String BXY = "";
                            String Quantity1 = "";
                            String UnitP1 = "";
                            String Quantity2 = "";
                            String UnitP2 = "";
                            String Quantity3 = "";
                            String UnitP3 = "";

                            if (dtr[16].ToString() != "0")
                            {
                                try
                                {
                                    cnnD.ConnectionString = connectionString;
                                    cnnD.Open();

                                    cmdD.Connection = cnnD;
                                    cmdD.CommandText = "SELECT [ID],[Description],[Type],[Quantity1]," +
                                        " ROUND([Price1], 2) AS [Price1],[Quantity2],ROUND([Price2], 2) AS [Price2],[Quantity3],ROUND([Price3], 2) AS [Price3]" +
                                        " FROM QuantityDiscount Where [ID] = '" + dtr[16].ToString() + "'";
                                    SqlDataReader dtrD = cmdD.ExecuteReader();

                                    if (dtrD.HasRows)
                                    {
                                        while (dtrD.Read())
                                        {

                                            if (dtrD[2].ToString() == "1" || dtrD[2].ToString() == "3")
                                            {
                                                MM = (dtrD[1].ToString());
                                            }
                                            else
                                            {
                                                if (dtrD[2].ToString() == "2" || dtrD[2].ToString() == "4")
                                                {
                                                    BXY = (dtrD[1]).ToString();
                                                }
                                                else
                                                {
                                                    if (dtrD[2].ToString() == "0")
                                                    {
                                                        if (dtrD[3].ToString() == "0") { Quantity1 = ""; } else { Quantity1 = dtrD[3].ToString(); }
                                                        if (dtrD.GetDecimal(4).ToString("0.00") == "0.00") { UnitP1 = ""; } else { UnitP1 = dtrD.GetDecimal(4).ToString("0.00"); }
                                                        if (dtrD[5].ToString() == "0") { Quantity2 = ""; } else { Quantity2 = dtrD[5].ToString(); }
                                                        if (dtrD.GetDecimal(6).ToString("0.00") == "0.00") { UnitP2 = ""; } else { UnitP2 = dtrD.GetDecimal(6).ToString("0.00"); }
                                                        if (dtrD[7].ToString() == "0") { Quantity3 = ""; } else { Quantity3 = dtrD[7].ToString(); }
                                                        if (dtrD.GetDecimal(8).ToString("0.00") == "0.00") { UnitP3 = ""; } else { UnitP3 = dtrD.GetDecimal(8).ToString("0.00"); }

                                                    }
                                                }
                                            }

                                        }
                                    }
                                    dtrD.Close();
                                    cnnD.Close();
                                }
                                catch { }
                            }

                            String taq = "";
                            String chq = "";
                            String fs = "";

                            taq = dtr[19].ToString();
                            txtTagaloneqty.Text = taq;
                            chq = dtr[21].ToString();

                            if (dtr[17].ToString() == "True")
                            {
                                fs = "1";
                                ChkFS.IsChecked = true;
                            }
                            else { 
                                fs = "0";
                                ChkFS.IsChecked = false;
                            }


                            SqlConnection cnnM = new SqlConnection();
                            SqlCommand cmdM = new SqlCommand();
                            SqlConnection cnnM2 = new SqlConnection();
                            SqlCommand cmdM2 = new SqlCommand();
                            SqlConnection cnnMI = new SqlConnection();
                            SqlCommand cmdMI = new SqlCommand();
                            String mi = "";
                            String chi = "";
                            String tai = "";

                            try
                            {
                                cnnM.ConnectionString = connectionString;
                                cnnM.Open();

                                cmdM.Connection = cnnM;
                                cmdM.CommandText = "SELECT [ID],[Description] FROM Item Where [ID] = '" + dtr[18].ToString() + "'";
                                SqlDataReader dtrM = cmdM.ExecuteReader();

                                if (dtrM.HasRows)
                                {
                                    while (dtrM.Read())
                                    {
                                        tai = dtrM[1].ToString();
                                        ComboTagAlone.Items.Clear();
                                        ComboTagAlone.Items.Add(dtrM[1]);
                                        ComboTagAlone.SelectedIndex = 0;

                                    }
                                }
                                dtrM.Close();
                                cnnM.Close();
                            }
                            catch { }

                            try
                            {
                                cnnM2.ConnectionString = connectionString;
                                cnnM2.Open();

                                cmdM2.Connection = cnnM2;
                                cmdM2.CommandText = "SELECT [ID],[Description] FROM Item Where [ID] = '" + dtr[20].ToString() + "'";
                                SqlDataReader dtrM2 = cmdM2.ExecuteReader();

                                if (dtrM2.HasRows)
                                {
                                    while (dtrM2.Read())
                                    {
                                        chi = dtrM2[1].ToString();

                                    }
                                }
                                dtrM2.Close();
                                cnnM2.Close();
                            }
                            catch { }

                            
                            try
                            {
                                cnnMI.ConnectionString = connectionString;
                                cnnMI.Open();
                                
                                cmdMI.Connection = cnnMI;
                                cmdMI.CommandText = "SELECT [ID],[Title] FROM ItemMessage Where [ID] = '" + dtr[22].ToString() + "'";
                                SqlDataReader dtrMI = cmdMI.ExecuteReader();
                                
                                if (dtrMI.HasRows)
                                {
                                    while (dtrMI.Read())
                                    {
                                        mi = dtrMI[1].ToString();
                                        
                                    }
                                }
                                dtrMI.Close();
                                cnnMI.Close();
                            }
                            catch {  }

                            string dept = "";
                            string cat = "";
                            string ta = "";


                            try
                            {
                                dept = ComboDepartment.SelectedValue.ToString();
                            }
                            catch { dept = ""; }

                            try
                            {
                                cat = ComboCategory.SelectedValue.ToString();
                            }
                            catch { cat = ""; }

                            try
                            {
                                ta = ComboTax.SelectedValue.ToString();
                            }
                            catch { ta = ""; }

                            ItemClass.AddData(ic, txtDescription.Text, txtItemNumber.Text, dept, cat, ta, txtCost.Text, txtPrice.Text,tai,taq,fs);
                            PricingClass.AddData(dtr.GetDecimal(8).ToString("0.00"), dtr.GetDecimal(9).ToString("0.00"), dtr.GetDecimal(10).ToString("0.00"), dtr.GetDecimal(11).ToString("0.00"), dtr[12].ToString(), dtr[13].ToString(), sch, dtr[15].ToString());
                            DiscountClass.AddData(MM, BXY, Quantity1, UnitP1, Quantity2, UnitP2, Quantity3, UnitP3);
                            MoreClass.AddData( chi, chq, mi);

                            
                        }

                        isTimer.Start();
                    }
                    else
                    {
                        //txtbTypeSave.Text = "1";
                        String msg;
                        msg = "Scan Barcode does not exist";
                        ShowDialog(msg);

                        txtDescription.IsReadOnly = true;
                        txtItemNumber.IsReadOnly = true;
                        txtCost.IsReadOnly = true;
                        txtPrice.IsReadOnly = true;

                        txtScanBarcode.Text = "";
                        txtDescription.Text = "";
                        txtItemNumber.Text = "";

                        ComboDepartment.Items.Clear();
                        ComboCategory.Items.Clear();

                        ComboTax.Items.Clear();


                        txtCost.Text = "";
                        txtPrice.Text = "";


                        ItemClass.AddData("", "", "", "", "", "", "", "", "", "", "");
                        PricingClass.AddData("", "", "", "", "", "", "", "");
                        DiscountClass.AddData("", "", "", "", "", "", "", "");
                        MoreClass.AddData("", "", "");


                    }

                    dtr.Close();
                    cnn.Close();
                }

            }
            else {
                String msg;
                msg = "Fill Barcode Field";
                ShowDialog(msg);
            }
        }

        /// <summary>
        /// Send information set by de user on poup to the corresponding
        /// object of the principal page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditDescription_Click(object sender, RoutedEventArgs e)
        {

            if (!StandardPopup.IsOpen)
            {
                Title.Text = "Item Description";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Default) }
                };


                StandardPopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }
        }

        /// <summary>
        /// Allow to choose a department name which 
        /// exist in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditDepartment_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Allow to choose a category name which 
        /// exist in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditCategory_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Allow to choose a tax definition  which 
        /// exist in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditTaxes_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Allow to modify the previus value or blank value in textbox
        /// from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditCost_Click(object sender, RoutedEventArgs e)
        {
            
            if (!StandardPopup.IsOpen)
            {
                Title.Text = "Item Cost";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                StandardPopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }

        }

        /// <summary>
        /// Allow to modify the previus value or blank value in textbox
        /// from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditPrice_Click(object sender, RoutedEventArgs e)
        {

            if (!StandardPopup.IsOpen) {
                Title.Text = "Item Price";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                StandardPopup.IsOpen = true;
                popupPrice.PreventKeyboardDisplayOnProgrammaticFocus = false;
                popupPrice.Focus(FocusState.Programmatic);


            }
        }

        /// <summary>
        /// Save on data base item properties information
        /// set by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            txtPrice.Text = popupPrice.Text;
            // if the Popup is open, then close it 
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
        }

        /// <summary>
        /// Send information set at popup to the objects in page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Text == "Item Cost") { txtCost.Text = popupPrice.Text;
                //ItemClass.AddData(txtScanBarcode.Text, txtDescription.Text, txtItemNumber.Text, ComboDepartment.SelectedValue.ToString(), ComboCategory.SelectedValue.ToString(), ComboTax.SelectedValue.ToString(), popupPrice.Text, txtPrice.Text); 
            }
            else { if (Title.Text == "Item Price")
                {
                    txtPrice.Text = popupPrice.Text;
                    //ItemClass.AddData(txtScanBarcode.Text, txtDescription.Text, txtItemNumber.Text, ComboDepartment.SelectedValue.ToString(), ComboCategory.SelectedValue.ToString(), ComboTax.SelectedValue.ToString(), txtCost.Text, popupPrice.Text);
                }
                else
                {
                    if (Title.Text == "Item Description")
                    {
                        txtDescription.Text = popupPrice.Text;
                        //ItemClass.AddData(txtScanBarcode.Text, popupPrice.Text, txtItemNumber.Text, ComboDepartment.SelectedValue.ToString(), ComboCategory.SelectedValue.ToString(), ComboTax.SelectedValue.ToString(), txtCost.Text, txtPrice.Text);
                    }
                    else
                    {
                        if (Title.Text == "New Itemlookupcode")
                        {
                            String swni = "";

                            String srv = Class1.GetServer();
                            String srvu = Class1.GetUser();
                            String srvp = Class1.GetPass();
                            String srvdb = Class1.GetDb();

                            SqlConnection cnnT = new SqlConnection();
                            SqlCommand cmdT = new SqlCommand();

                            string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;

                            try
                            {
                                cnnT.ConnectionString = connectionString;
                                cnnT.Open();

                                cmdT.Connection = cnnT;
                                cmdT.CommandText = "SELECT [Itemlookupcode] FROM item Where [Itemlookupcode] = '" + popupPrice.Text + "'";
                                SqlDataReader dtrT = cmdT.ExecuteReader();


                                if (dtrT.HasRows)
                                {
                                    swni = "0";
                                    ShowDialog("Itemlookupcode already exist");

                                }
                                else { 
                                    swni = "1";
                                    txtScanBarcode.Text = popupPrice.Text;
                                    ItemClass.AddData(popupPrice.Text,"","","","","","","", "", "", "");
                                    isTimer.Start();
                                    //ItemClass.AddData(txtScanBarcode.Text, popupPrice.Text, txtItemNumber.Text, ComboDepartment.SelectedValue.ToString(), ComboCategory.SelectedValue.ToString(), ComboTax.SelectedValue.ToString(), txtCost.Text, txtPrice.Text);
                                }

                                dtrT.Close();
                                cnnT.Close();

                            }
                            catch { }
                        }else 
                        {
                            if (Title.Text == "Tag Alone Qty") {
                                txtTagaloneqty.Text = popupPrice.Text;
                                //ItemClass.AddData(txtScanBarcode.Text, txtDescription.Text, txtItemNumber.Text, ComboDepartment.SelectedValue.ToString(), ComboCategory.SelectedValue.ToString(), ComboTax.SelectedValue.ToString(), txtCost.Text, popupPrice.Text);
                            }
                        }
                    }
            }   }

            // if the Popup is open, then close it 
            popupPrice.Text = "";
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
        }

        private void SimulateSaveClicked(object sender, RoutedEventArgs e)
        {
            // close the Popup
            popupPrice.Text = "";
            if (StandardPopup != null) { StandardPopup.IsOpen = false; }
        }

        private void StandardPopup_Loaded(object sender, RoutedEventArgs e)
        {
            popupPrice.Focus(FocusState.Programmatic);
        }

        /// <summary>
        /// Allow App to consult data base when press enter key in
        /// order to show item properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtScanBarcode_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter) {
                if (txtScanBarcode.Text != "")
                {
                    string swic = "";

                    SqlConnection cnn = new SqlConnection();
                    SqlConnection cnn1 = new SqlConnection();
                    SqlConnection cnn2 = new SqlConnection();
                    SqlConnection cnn3 = new SqlConnection();
                    SqlCommand cmd = new SqlCommand();
                    SqlCommand cmd1 = new SqlCommand();
                    SqlCommand cmd2 = new SqlCommand();
                    SqlCommand cmd3 = new SqlCommand();


                    CultureInfo provider = CultureInfo.InvariantCulture;

                    String srv = Class1.GetServer();
                    String srvu = Class1.GetUser();
                    String srvp = Class1.GetPass();
                    String srvdb = Class1.GetDb();

                    string ic = txtScanBarcode.Text;

                    string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                    try
                    {

                        cnn.ConnectionString = connectionString;
                        cnn.Open();

                        swic = "1";
                    }
                    catch
                    {
                        String msg;
                        msg = "Verify Connection";
                        ShowDialog(msg);
                    }

                    if (swic == "1")
                    {

                        cmd.Connection = cnn;
                        cmd.CommandText = "SELECT [ItemLookupCode],[Description],[ID],[DepartmentID],[CategoryID],[TaxID],[Cost],[Price]," +
                        " [PriceA],[PriceB],[PriceC],[SalePrice],[SaleStartDate],[SaleEndDate],[SaleScheduleID],[SaleType], [QuantityDiscountID]," +
                        " [Foodstampable],[TagAlongItem],[TagAlongQuantity],[ParentItem],[ParentQuantity],[MessageID] FROM Item Where [ItemLookupCode] = '" + ic + "'";
                        SqlDataReader dtr = cmd.ExecuteReader();

                        if (dtr.HasRows)
                        {
                            //txtbTypeSave.Text = "";
                            while (dtr.Read())
                            {

                                txtDescription.Text = dtr[1].ToString();
                                txtItemNumber.Text = dtr[2].ToString();
                                try
                                {
                                    cnn1.ConnectionString = connectionString;
                                    cnn1.Open();

                                    cmd1.Connection = cnn1;
                                    cmd1.CommandText = "SELECT [ID],[Name] FROM Department Where [ID] = '" + dtr[3].ToString() + "'";
                                    SqlDataReader dtr1 = cmd1.ExecuteReader();


                                    if (dtr1.HasRows)
                                    {
                                        while (dtr1.Read())
                                        {
                                            ComboDepartment.Items.Clear();
                                            ComboDepartment.Items.Add(dtr1[1]);
                                            ComboDepartment.SelectedIndex = 0;

                                        }
                                    }
                                    dtr1.Close();
                                    cnn1.Close();
                                }
                                catch { }

                                try
                                {
                                    cnn2.ConnectionString = connectionString;
                                    cnn2.Open();

                                    cmd2.Connection = cnn2;
                                    cmd2.CommandText = "SELECT [ID],[Name] FROM Category Where [ID] = '" + dtr[4].ToString() + "'";
                                    SqlDataReader dtr2 = cmd2.ExecuteReader();

                                    if (dtr2.HasRows)
                                    {
                                        while (dtr2.Read())
                                        {
                                            ComboCategory.Items.Clear();
                                            ComboCategory.Items.Add(dtr2[1]);
                                            ComboCategory.SelectedIndex = 0;
                                        }
                                    }
                                    dtr2.Close();
                                    cnn2.Close();
                                }
                                catch
                                {
                                }

                                try
                                {
                                    cnn3.ConnectionString = connectionString;
                                    cnn3.Open();

                                    cmd3.Connection = cnn3;
                                    cmd3.CommandText = "SELECT [ID],[Description] FROM ItemTax Where [ID] = '" + dtr[5].ToString() + "'";
                                    SqlDataReader dtr3 = cmd3.ExecuteReader();

                                    if (dtr3.HasRows)
                                    {
                                        while (dtr3.Read())
                                        {
                                            ComboTax.Items.Clear();
                                            ComboTax.Items.Add(dtr3[1]);
                                            ComboTax.SelectedIndex = 0;
                                        }
                                    }
                                    dtr3.Close();
                                    cnn3.Close();
                                }
                                catch
                                {
                                }

                                txtCost.Text = dtr.GetDecimal(6).ToString("0.00");
                                txtPrice.Text = dtr.GetDecimal(7).ToString("0.00");

                                SqlConnection cnnsch = new SqlConnection();
                                SqlCommand cmdsch = new SqlCommand();
                                String sch = "";

                                try
                                {
                                    cnnsch.ConnectionString = connectionString;
                                    cnnsch.Open();

                                    cmdsch.Connection = cnnsch;
                                    cmdsch.CommandText = "SELECT [ID],[Description] FROM Schedule Where [ID] = '" + dtr[14].ToString() + "'";
                                    SqlDataReader dtrsch = cmdsch.ExecuteReader();

                                    if (dtrsch.HasRows)
                                    {
                                        while (dtrsch.Read())
                                        {
                                            sch = dtrsch[1].ToString();
                                        }
                                    }
                                    dtrsch.Close();
                                    cnnsch.Close();
                                }
                                catch { }

                                SqlConnection cnnD = new SqlConnection();
                                SqlCommand cmdD = new SqlCommand();
                                String MM = "";
                                String BXY = "";
                                String Quantity1 = "";
                                String UnitP1 = "";
                                String Quantity2 = "";
                                String UnitP2 = "";
                                String Quantity3 = "";
                                String UnitP3 = "";

                                if (dtr[16].ToString() != "0")
                                {
                                    try
                                    {
                                        cnnD.ConnectionString = connectionString;
                                        cnnD.Open();

                                        cmdD.Connection = cnnD;
                                        cmdD.CommandText = "SELECT [ID],[Description],[Type],[Quantity1]," +
                                            " ROUND([Price1], 2) AS [Price1],[Quantity2],ROUND([Price2], 2) AS [Price2],[Quantity3],ROUND([Price3], 2) AS [Price3]" +
                                            " FROM QuantityDiscount Where [ID] = '" + dtr[16].ToString() + "'";
                                        SqlDataReader dtrD = cmdD.ExecuteReader();

                                        if (dtrD.HasRows)
                                        {
                                            while (dtrD.Read())
                                            {

                                                if (dtrD[2].ToString() == "1" || dtrD[2].ToString() == "3")
                                                {
                                                    MM = (dtrD[1].ToString());
                                                }
                                                else
                                                {
                                                    if (dtrD[2].ToString() == "2" || dtrD[2].ToString() == "4")
                                                    {
                                                        BXY = (dtrD[1]).ToString();
                                                    }
                                                    else
                                                    {
                                                        if (dtrD[2].ToString() == "0")
                                                        {
                                                            if (dtrD[3].ToString() == "0") { Quantity1 = ""; } else { Quantity1 = dtrD[3].ToString(); }
                                                            if (dtrD.GetDecimal(4).ToString("0.00") == "0.00") { UnitP1 = ""; } else { UnitP1 = dtrD.GetDecimal(4).ToString("0.00"); }
                                                            if (dtrD[5].ToString() == "0") { Quantity2 = ""; } else { Quantity2 = dtrD[5].ToString(); }
                                                            if (dtrD.GetDecimal(6).ToString("0.00") == "0.00") { UnitP2 = ""; } else { UnitP2 = dtrD.GetDecimal(6).ToString("0.00"); }
                                                            if (dtrD[7].ToString() == "0") { Quantity3 = ""; } else { Quantity3 = dtrD[7].ToString(); }
                                                            if (dtrD.GetDecimal(8).ToString("0.00") == "0.00") { UnitP3 = ""; } else { UnitP3 = dtrD.GetDecimal(8).ToString("0.00"); }

                                                        }
                                                    }
                                                }

                                            }
                                        }
                                        dtrD.Close();
                                        cnnD.Close();
                                    }
                                    catch { }
                                }

                                String taq = "";
                                String chq = "";
                                String fs = "";

                                taq = dtr[19].ToString();
                                txtTagaloneqty.Text = taq;
                                chq = dtr[21].ToString();

                                if (dtr[17].ToString() == "True")
                                {
                                    fs = "1";
                                    ChkFS.IsChecked = true;
                                }
                                else
                                {
                                    fs = "0";
                                    ChkFS.IsChecked = false;
                                }


                                SqlConnection cnnM = new SqlConnection();
                                SqlCommand cmdM = new SqlCommand();
                                SqlConnection cnnM2 = new SqlConnection();
                                SqlCommand cmdM2 = new SqlCommand();
                                SqlConnection cnnMI = new SqlConnection();
                                SqlCommand cmdMI = new SqlCommand();
                                String mi = "";
                                String chi = "";
                                String tai = "";

                                try
                                {
                                    cnnM.ConnectionString = connectionString;
                                    cnnM.Open();

                                    cmdM.Connection = cnnM;
                                    cmdM.CommandText = "SELECT [ID],[Description] FROM Item Where [ID] = '" + dtr[18].ToString() + "'";
                                    SqlDataReader dtrM = cmdM.ExecuteReader();

                                    if (dtrM.HasRows)
                                    {
                                        while (dtrM.Read())
                                        {
                                            tai = dtrM[1].ToString();
                                            ComboTagAlone.Items.Clear();
                                            ComboTagAlone.Items.Add(dtrM[1]);
                                            ComboTagAlone.SelectedIndex = 0;
                                        }
                                    }
                                    dtrM.Close();
                                    cnnM.Close();
                                }
                                catch { }

                                try
                                {
                                    cnnM2.ConnectionString = connectionString;
                                    cnnM2.Open();

                                    cmdM2.Connection = cnnM2;
                                    cmdM2.CommandText = "SELECT [ID],[Description] FROM Item Where [ID] = '" + dtr[20].ToString() + "'";
                                    SqlDataReader dtrM2 = cmdM2.ExecuteReader();

                                    if (dtrM2.HasRows)
                                    {
                                        while (dtrM2.Read())
                                        {
                                            chi = dtrM2[1].ToString();

                                        }
                                    }
                                    dtrM2.Close();
                                    cnnM2.Close();
                                }
                                catch { }

                                try
                                {
                                    cnnMI.ConnectionString = connectionString;
                                    cnnMI.Open();

                                    cmdMI.Connection = cnnMI;
                                    cmdMI.CommandText = "SELECT [ID],[Title] FROM ItemMessage Where [ID] = '" + dtr[22].ToString() + "'";
                                    SqlDataReader dtrMI = cmdMI.ExecuteReader();

                                    if (dtrMI.HasRows)
                                    {
                                        while (dtrMI.Read())
                                        {
                                            mi = dtrMI[1].ToString();

                                        }
                                    }
                                    dtrMI.Close();
                                    cnnMI.Close();
                                }
                                catch { }

                                string dept = "";
                                string cat = "";
                                string ta = "";


                                try
                                {
                                    dept = ComboDepartment.SelectedValue.ToString();
                                }
                                catch { dept = ""; }

                                try
                                {
                                    cat = ComboCategory.SelectedValue.ToString();
                                }
                                catch { cat = ""; }

                                try
                                {
                                    ta = ComboTax.SelectedValue.ToString();
                                }
                                catch { ta = ""; }

                                ItemClass.AddData(txtScanBarcode.Text, txtDescription.Text, txtItemNumber.Text, dept, cat, ta, txtCost.Text, txtPrice.Text, tai, taq,fs);
                                //txtbTypeSave.Text = "";
                                PricingClass.AddData(dtr.GetDecimal(8).ToString("0.00"), dtr.GetDecimal(9).ToString("0.00"), dtr.GetDecimal(10).ToString("0.00"), dtr.GetDecimal(11).ToString("0.00"), dtr[12].ToString(), dtr[13].ToString(), sch, dtr[15].ToString());
                                DiscountClass.AddData(MM, BXY, Quantity1, UnitP1, Quantity2, UnitP2, Quantity3, UnitP3);
                                MoreClass.AddData( chi, chq, mi);
                            }

                            isTimer.Start();
                        }
                        else
                        {
                            //txtbTypeSave.Text = "1";
                            String msg;
                            msg = "Scan Barcode does not exist";
                            ShowDialog(msg);

                            txtDescription.IsReadOnly = true;
                            txtItemNumber.IsReadOnly = true;
                            txtCost.IsReadOnly = true;
                            txtPrice.IsReadOnly = true;

                            txtScanBarcode.Text = "";
                            txtDescription.Text = "";
                            txtItemNumber.Text = "";

                            ComboDepartment.Items.Clear();
                            ComboCategory.Items.Clear();

                            ComboTax.Items.Clear();


                            txtCost.Text = "";
                            txtPrice.Text = "";


                            ItemClass.AddData("", "", "", "", "", "", "", "", "", "", "");
                            PricingClass.AddData("", "", "", "", "", "", "", "");
                            DiscountClass.AddData("", "", "", "", "", "", "", "");
                            MoreClass.AddData("", "", "");


                        }

                        dtr.Close();
                        cnn.Close();
                    }
                    
                }
                else
                {
                    String msg;
                    msg = "Fill Barcode Field";
                    ShowDialog(msg);
                }
            }
        }

        private void ComboDepartment_DropDownOpened(object sender, object e)
        {
            SqlCommand cmdd = new SqlCommand();
            SqlConnection cnnd = new SqlConnection();

            String srvd = Class1.GetServer();
            String srvud = Class1.GetUser();
            String srvpd = Class1.GetPass();
            String srvdbd = Class1.GetDb();

            String connectionStringd = "Data Source=" + srvd + ";Initial Catalog=" + srvdbd + ";User Id=" + srvud + ";Password=" + srvpd;

            ComboDepartment.Items.Clear();
            ComboDepartment.Items.Add(" ");

            try
            {
                cnnd.ConnectionString = connectionStringd;
                cnnd.Open();

                cmdd.Connection = cnnd;
                cmdd.CommandText = "SELECT [ID],[Name] FROM Department";
                SqlDataReader dtrd = cmdd.ExecuteReader();

                if (dtrd.HasRows)
                {
                    while (dtrd.Read())
                    {
                        ComboDepartment.Items.Add(dtrd[1]);
                    }
                }
                dtrd.Close();
                cnnd.Close();
            }
            catch
            {
                String msg;
                msg = "Connection Error";
                ShowDialog(msg);
            }
        }

        private void ComboCategory_DropDownOpened(object sender, object e)
        {
            SqlCommand cmdd = new SqlCommand();
            SqlConnection cnnd = new SqlConnection();

            String srvd = Class1.GetServer();
            String srvud = Class1.GetUser();
            String srvpd = Class1.GetPass();
            String srvdbd = Class1.GetDb();

            String connectionStringd = "Data Source=" + srvd + ";Initial Catalog=" + srvdbd + ";User Id=" + srvud + ";Password=" + srvpd;

            ComboCategory.Items.Clear();
            ComboCategory.Items.Add(" ");

            try
            {
                cnnd.ConnectionString = connectionStringd;
                cnnd.Open();

                cmdd.Connection = cnnd;
                cmdd.CommandText = "SELECT [ID],[Name] FROM Category";
                SqlDataReader dtrd = cmdd.ExecuteReader();

                if (dtrd.HasRows)
                {
                    while (dtrd.Read())
                    {
                        ComboCategory.Items.Add(dtrd[1]);
                    }
                }
                dtrd.Close();
                cnnd.Close();
            }
            catch
            {
                String msg;
                msg = "Connection Error";
                ShowDialog(msg);
            }
        }

        private void ComboTax_DropDownOpened(object sender, object e)
        {
            SqlCommand cmdd = new SqlCommand();
            SqlConnection cnnd = new SqlConnection();

            String srvd = Class1.GetServer();
            String srvud = Class1.GetUser();
            String srvpd = Class1.GetPass();
            String srvdbd = Class1.GetDb();

            String connectionStringd = "Data Source=" + srvd + ";Initial Catalog=" + srvdbd + ";User Id=" + srvud + ";Password=" + srvpd;

            ComboTax.Items.Clear();
            ComboTax.Items.Add(" ");

            try
            {
                cnnd.ConnectionString = connectionStringd;
                cnnd.Open();

                cmdd.Connection = cnnd;
                cmdd.CommandText = "SELECT [ID],[Description] FROM ItemTax";
                SqlDataReader dtrd = cmdd.ExecuteReader();

                if (dtrd.HasRows)
                {
                    while (dtrd.Read())
                    {
                        ComboTax.Items.Add(dtrd[1]);
                    }
                }
                dtrd.Close();
                cnnd.Close();
            }
            catch
            {
                String msg;
                msg = "Connection Error";
                ShowDialog(msg);
            }
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            String srv;
            String srvu;
            String srvp;
            String srvdb;
           
            srv = Class1.GetServer();
            srvu = Class1.GetUser();
            srvp = Class1.GetPass();
            srvdb = Class1.GetDb();

            String connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;


            SqlConnection cnninic = new SqlConnection();
            SqlCommand cmdinic = new SqlCommand();
            
            try
            {
                cnninic.ConnectionString = connectionString;
                cnninic.Open();

                cmdinic.Connection = cnninic;
                cmdinic.CommandText = "SELECT [ID],[PONumber] FROM PurchaseOrder Where PONumber LIKE 'LBL%' order by id asc";
                SqlDataReader dtrinic = cmdinic.ExecuteReader();
                
                int found = 0;
                
                if (dtrinic.HasRows)
                {
                    while (dtrinic.Read())
                    {
                        found = dtrinic[1].ToString().IndexOf("-");
                        inicio = dtrinic[1].ToString().Substring(found + 1);

                        idinicio = dtrinic[0].ToString();

                        Msg.Text = "Do you want add this item";
                        Msg2.Text = "to Purchase Order # LBL-" + inicio + "?";

                        YESNOPOPopup.IsOpen = true;
                    }
                }
                else
                {
                    
                    inicio = "1";

                    string sLine = "";
                    int nc = 0;
                    string tcn = "";
                    string subcadena = "";
                                       
                    srv = Class1.GetServer();
                    srvu = Class1.GetUser();
                    srvp = Class1.GetPass();
                    srvdb = Class1.GetDb();

                    SqlConnection cnn = new SqlConnection();
                    SqlCommand cmd = new SqlCommand();
                    SqlConnection cnn1 = new SqlConnection();
                    SqlCommand cmd1 = new SqlCommand();
                    SqlConnection cnn2 = new SqlConnection();
                    SqlCommand cmd2 = new SqlCommand();
                    SqlConnection cnn3 = new SqlConnection();
                    SqlCommand cmd3 = new SqlCommand();
                    SqlConnection cnnsupid = new SqlConnection();
                    SqlCommand cmdsupid = new SqlCommand();
                    
                    try
                    {
                        cnnsupid.ConnectionString = connectionString;
                        cnnsupid.Open();

                        cmdsupid.Connection = cnnsupid;
                        cmdsupid.CommandText = "SELECT [MinimumOrder],Item.[ID],SupplierList.[SupplierID],Item.[Cost],[TaxRate],Item.[Description] FROM " +
                                    @"Item left join SupplierList on Item.ID = SupplierList.ItemID where Item.[Itemlookupcode] = '" + ItemClass.GetItemBarcode().ToString() + "'";
                        SqlDataReader dtrsupid = cmdsupid.ExecuteReader();
                        
                        if (dtrsupid.HasRows)
                        {
                            while (dtrsupid.Read())
                            {
                                string shipto = "";
                                string sti = "";
                                SqlConnection storecnn = new SqlConnection();
                                SqlCommand storecmd = new SqlCommand();

                                try
                                {

                                    storecnn.ConnectionString = connectionString;
                                    storecnn.Open();

                                    storecmd.Connection = storecnn;
                                    storecmd.CommandText = "SELECT StoreID,StoreName,StoreAddress1,StoreCity,StoreState,StoreZip,StorePhone,StoreFax from Configuration";
                                    
                                    SqlDataReader storedtr = storecmd.ExecuteReader();

                                    if (storedtr.HasRows)
                                    {
                                        while (storedtr.Read())
                                        {
                                            sLine = storedtr[1].ToString();
                                            nc = sLine.Length;
                                            tcn = "";
                                            subcadena = "";

                                            for (int iline = 0; iline < nc; iline++)
                                            {
                                                tcn = sLine.Substring(iline, 1);
                                                if (tcn == "'")
                                                {
                                                    tcn = "''";
                                                    subcadena = subcadena + tcn;
                                                }
                                                else
                                                {
                                                    subcadena = subcadena + tcn;
                                                }
                                            }
                                            subcadena = subcadena.TrimEnd();
                                            subcadena = subcadena.TrimStart();

                                            sti = storedtr[0].ToString();
                                            shipto = subcadena + " " + storedtr[2].ToString() + " " + storedtr[3].ToString() + "," +
                                                @" " + storedtr[4].ToString() + " " + storedtr[5].ToString() + " Phone: " + storedtr[6].ToString() + " Fax: " + storedtr[7].ToString();
                                        }
                                    }

                                    storedtr.Close();
                                    storecnn.Close();
                                }
                                catch (Exception ex)
                                {
                                    ShowDialog("Error consulting store: " + ex.ToString());
                                }

                                string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");

                                string to = shipto;

                                SqlConnection cnninsert = new SqlConnection();
                                SqlCommand cmdinsert = new SqlCommand();
                                cnninsert.ConnectionString = connectionString;
                                cnninsert.Open();
                                cmdinsert.Connection = cnninsert;
                                string idsup = "";

                                if (dtrsupid[4].ToString() == null)
                                {
                                    idsup = "0";
                                }

                                try
                                {
                                    cmdinsert.CommandText = "INSERT INTO [PurchaseOrder] ([LastUpdated],[POTitle],[POType],[StoreID]" +
                                    @",[WorksheetID],[PONumber],[Status],[DateCreated],[To],[ShipTo],[Requisitioner],[ShipVia],[FOBPoint],[Terms]" +
                                    @",[TaxRate],[Shipping],[Freight],[RequiredDate],[ConfirmingTo],[Remarks],[SupplierID],[OtherStoreID],[CurrencyID]" +
                                    @",[ExchangeRate],[OtherPOID],[InventoryLocation],[IsPlaced],[DatePlaced],[BatchNumber]) VALUES ('" + now + "','',0" +
                                    @",'" + sti + "',0,'LBL-" + inicio + "',0,'" + now + "'" +
                                    @",'xxxx','xxxx','','','','',0,0.00,'','" + now + "','','','" + idsup + "',0,0,1,0,0,0,NULL,0)";

                                    cmdinsert.ExecuteNonQuery();
                                    cnninsert.Close();
                                }
                                catch
                                {
                                    try
                                    {
                                        cmdinsert.CommandText = "INSERT INTO [PurchaseOrder] ([LastUpdated],[POTitle],[POType],[StoreID]" +
                                        @",[WorksheetID],[PONumber],[Status],[DateCreated],[To],[ShipTo],[Requisitioner],[ShipVia],[FOBPoint],[Terms]" +
                                        @",[TaxRate],[Shipping],[Freight],[RequiredDate],[ConfirmingTo],[Remarks],[SupplierID],[OtherStoreID],[CurrencyID]" +
                                        @",[ExchangeRate],[OtherPOID],[InventoryLocation],[IsPlaced],[DatePlaced],[BatchNumber],[EstShipping],[CurrentShipping]" +
                                        @",[EstOtherFees],[CurrentOtherFees],[OtherFees],[CostDistributionMethod],[ParentPOId],[RootPOId],[OriginPOId]" +
                                        @",[MasterPO],[DiscrepancyStatus]) VALUES ('" + now + "','',0,'" + sti + "',0,'LBL-" + inicio + "',0,'" + now + "'" +
                                        @",'xxxx','xxxx','','','','',0,0.00,'','" + now + "','','','" + idsup + "',0,0,1,0,0,0,NULL,0" +
                                        @",0.00,0.00,0.00,0.00,0.00,0,0,0,0,'',0)";

                                        cmdinsert.ExecuteNonQuery();
                                        cnninsert.Close();
                                    }
                                    catch (SqlException ex1)
                                    {
                                        ShowDialog("Error inserting purchase order: " + ex1.ToString());
                                    }
                                }

                                string lastid = "";

                                try
                                {
                                    
                                    cnn.ConnectionString = connectionString;
                                    cnn.Open();
                                    
                                    cmd.Connection = cnn;
                                    cmd.CommandText = "SELECT [ID] FROM PurchaseOrder Where PONumber = 'LBL-1' order by id asc";
                                    SqlDataReader dtr = cmd.ExecuteReader();

                                    if (dtr.HasRows)
                                    {
                                        while (dtr.Read())
                                        {
                                            lastid = dtr[0].ToString();
                                        }
                                    }

                                    dtr.Close();
                                    cnn.Close();
                                }
                                catch (Exception ex)
                                {
                                    ShowDialog("Error finding purchase order: " + ex.ToString());
                                }


                                /// Insert in PurchaseEntry
                                /// 
                                cnn3.ConnectionString = connectionString;
                                cnn3.Open();


                                try
                                {
                                    sLine = dtrsupid[5].ToString();
                                    nc = sLine.Length;
                                    tcn = "";
                                    subcadena = "";

                                    for (int iline = 0; iline < nc; iline++)
                                    {
                                        tcn = sLine.Substring(iline, 1);
                                        if (tcn == "'")
                                        {
                                            tcn = "''";
                                            subcadena = subcadena + tcn;
                                        }
                                        else
                                        {
                                            subcadena = subcadena + tcn;
                                        }
                                    }
                                    subcadena = subcadena.TrimEnd();
                                    subcadena = subcadena.TrimStart();

                                    try
                                    {
                                        string cant = "1";

                                        cmd3.Connection = cnn3;
                                        cmd3.CommandText = "INSERT INTO [PurchaseOrderEntry] ([ItemDescription],[LastUpdated]" +
                                        @",[QuantityReceivedToDate],[StoreID],[PurchaseOrderID],[QuantityOrdered],[QuantityReceived],[ItemID]" +
                                        @",[OrderNumber],[Price],[TaxRate],[InventoryOfflineID]) VALUES ('" + subcadena + "','" + now + "',0,'" + sti +
                                        @"','" + lastid + "'," + cant + ",0,'" + dtrsupid[1].ToString() + "','LBL-1'," + dtrsupid[3].ToString() + ",0,0)";

                                        cmd3.ExecuteNonQuery();


                                    }
                                    catch
                                    {

                                        try
                                        {

                                            string cant = dtrsupid[0].ToString();
                                            if (cant == "0") { cant = "1"; }

                                            cmd3.Connection = cnn3;
                                            cmd3.CommandText = "INSERT INTO [PurchaseOrderEntry] ([ItemDescription],[LastUpdated]" +
                                            @",[QuantityReceivedToDate],[StoreID],[PurchaseOrderID],[QuantityOrdered],[QuantityReceived],[ItemID]" +
                                            @",[OrderNumber],[Price],[TaxRate],[InventoryOfflineID],[ShippingPerItem],[OtherFeesPerItem]" +
                                            @",[LastQuantityReceived],[LastReceivedDate]) VALUES ('" + subcadena + "','" + now + "',0" +
                                            @",'" + sti + "','" + lastid + "'," + cant + ",0,'" + dtrsupid[1].ToString() + "','LBL-1'," + dtrsupid[3].ToString() + ",0" +
                                            @",0,0.00,0.00,0,NULL)";

                                            cmd3.ExecuteNonQuery();

                                        }
                                        catch (SqlException ex)
                                        {
                                            ShowDialog("Error inserting: " + ex.ToString());
                                        }
                                    }
                                                                        
                                }
                                catch (SqlException ex2)
                                {

                                    ShowDialog("Error inserting detail: " + ex2.ToString());

                                }



                                string msgpo = " Purchase Order # LBL-" + inicio + " have been created";
                                ShowDialog(msgpo);

                                txtDescription.IsReadOnly = true;
                                txtItemNumber.IsReadOnly = true;
                                txtCost.IsReadOnly = true;
                                txtPrice.IsReadOnly = true;

                                txtScanBarcode.Text = "";
                                txtDescription.Text = "";
                                txtItemNumber.Text = "";

                                ComboDepartment.Items.Clear();
                                ComboCategory.Items.Clear();

                                ComboTax.Items.Clear();


                                txtCost.Text = "";
                                txtPrice.Text = "";


                                ItemClass.AddData("", "", "", "", "", "", "", "", "", "", "");
                                PricingClass.AddData("", "", "", "", "", "", "", "");
                                DiscountClass.AddData("", "", "", "", "", "", "", "");
                                MoreClass.AddData("", "", "");

                                cnn3.Close();
                                
                            }
                            cnnsupid.Close();
                        }
                    }
                    catch (Exception ex) 
                    { 
                        ShowDialog("Supplier Error: " + ex.ToString()); 
                    }
                                        
                }
                                
                dtrinic.Close();
                cnninic.Close();
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString());
            }

                                 

        }

        private void BtnChart_Click(object sender, RoutedEventArgs e)
        {
            if (!ChartPopup.IsOpen)
            {
                Title.Text = "Item Sales";
                LoadChartContents();
                ChartPopup.IsOpen = true;

            }
        }

        private void LoadChartContents()
        {

            string srv = "";
            string srvu = "";
            string srvp = "";
            string srvdb = "";
            List<Sales> lstSales = new List<Sales>();
            List<Sales> lstSalesF = new List<Sales>();

            srv = Class1.GetServer();
            srvu = Class1.GetUser();
            srvp = Class1.GetPass();
            srvdb = Class1.GetDb();

            SqlConnection cnnch = new SqlConnection();
            SqlCommand cmdch = new SqlCommand();

            String connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;


            try
            {
                cnnch.ConnectionString = connectionString;
                cnnch.Open();

                cmdch.Connection = cnnch;
                cmdch.CommandText = "SELECT sum([Quantity]) as Amount,convert(date,[Transaction].[Time], 1) as Date " +
                @" FROM [TransactionEntry] inner join[Transaction] on TransactionEntry.TransactionNumber = [Transaction].TransactionNumber" +
                @" where itemID = '"+ ItemClass.GetItemNumber() +"' group by convert(date,[Transaction].[Time], 1) order by convert(date,[Transaction].[Time], 1) desc";
                SqlDataReader dtrch = cmdch.ExecuteReader();

                
                if (dtrch.HasRows)
                {
                    int count = 1;

                    while (dtrch.Read())
                    {
                        if (count < 6)
                        {
                            lstSales.Add(new Sales() { Date = Convert.ToDateTime(dtrch["Date"]).ToString("MM/dd/yyyy"), Amount = Int32.Parse(dtrch[0].ToString()) });
                            
                        }                       
                        count = count + 1;
                    }
                    
                    
                }

                for (int i = 4; i >= 0; i--)
                {
                    lstSalesF.Add(lstSales[i]);
                }

            }
            catch {  }
              
            
            (ColumnChart.Series[0] as ColumnSeries).ItemsSource = lstSalesF;
        }

        private void ChkFS_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private async void BtnEditTagaloneqty_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!StandardPopup.IsOpen)
            {
                Title.Text = "Tag Alone Qty";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };

                StandardPopup.IsOpen = true;
                popupPrice.PreventKeyboardDisplayOnProgrammaticFocus = false;
                popupPrice.Focus(FocusState.Programmatic);


            }

            //if (!MorePopup.IsOpen)
            //{
            //    Titlemore.Text = "Tag Alone Qty";

            //    MorePopup.IsOpen = true;

            //    //popupPricemore.InputScope = new InputScope()
            //    //{
            //    //    Names = { new InputScopeName(InputScopeNameValue.Number) }
            //    //};

            //    await Task.Delay(100);
            //    //popupPricemore.Focus(FocusState.Programmatic);
            //}
        }

        //private void BtnSavePop_Click(object sender, RoutedEventArgs e)
        //{
        //    //if (Titlemore.Text == "Tag Alone Qty") { txtTagaloneqty.Text = popupPricemore.Text; }
           
           
        //    //// if the Popup is open, then close it 
        //    //popupPricemore.Text = "";
        //    //if (MorePopup.IsOpen) { MorePopup.IsOpen = false; }
        //}

        //private void BtnClosepopup_Click(object sender, RoutedEventArgs e)
        //{
        //    //// close the Popup
        //    //popupPricemore.Text = "";
        //    //if (MorePopup != null) { MorePopup.IsOpen = false; }
        //}


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

        private void BtnChart_Click_1(object sender, RoutedEventArgs e)
        {
            // close the Popup
            if (ChartPopup != null) { ChartPopup.IsOpen = false; }

        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            txtDescription.IsReadOnly = true;
            txtItemNumber.IsReadOnly = true;
            txtCost.IsReadOnly = true;
            txtPrice.IsReadOnly = true;

            txtScanBarcode.Text = "";
            txtDescription.Text = "";
            txtItemNumber.Text = "";

            //ComboDepartment.Items.Clear();
            //ComboCategory.Items.Clear();

            //ComboTax.Items.Clear();


            txtCost.Text = "";
            txtPrice.Text = "";


            ItemClass.AddData("", "", "", "", "", "", "", "", "", "", "");
            PricingClass.AddData("", "", "", "", "", "", "", "");
            DiscountClass.AddData("", "", "", "", "", "", "", "");
            MoreClass.AddData("", "", "");

            if (!StandardPopup.IsOpen)
            {
                Title.Text = "New Itemlookupcode";
                popupPrice.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Default) }
                };


                StandardPopup.IsOpen = true;
                popupPrice.Focus(FocusState.Programmatic);
            }

        }

        private void YESNOPOPopup_Loaded(object sender, RoutedEventArgs e)
        {
            BtnN.Focus(FocusState.Programmatic);
        }

        private void BtnN_Click(object sender, RoutedEventArgs e)
        {
            if (YESNOPOPopup != null) { YESNOPOPopup.IsOpen = false; }
            ShowDialog("You should cancel Purchase Order # LBL-" + inicio + " before adding this item");
            
        }

        private void BtnY_Click(object sender, RoutedEventArgs e)
        {            
            string sLine;
            int nc;
            string tcn;
            string subcadena;
            
            string srv ;
            string srvu;
            string srvp;
            string srvdb;

            srv = Class1.GetServer();
            srvu = Class1.GetUser();
            srvp = Class1.GetPass();
            srvdb = Class1.GetDb();

            SqlConnection cnn3 = new SqlConnection();
            SqlCommand cmd3 = new SqlCommand();
            SqlConnection cnnsupid = new SqlConnection();
            SqlCommand cmdsupid = new SqlCommand();

            String connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                        
            try
            {
                cnnsupid.ConnectionString = connectionString;
                cnnsupid.Open();

                cmdsupid.Connection = cnnsupid;
                cmdsupid.CommandText = "SELECT [MinimumOrder],Item.[ID],SupplierList.[SupplierID],Item.[Cost],[TaxRate],Item.[Description] FROM " +
                            @"Item left join SupplierList on Item.ID = SupplierList.ItemID where Item.[Itemlookupcode] = '" + ItemClass.GetItemBarcode().ToString() + "'";
                SqlDataReader dtrsupid = cmdsupid.ExecuteReader();

                if (dtrsupid.HasRows)
                {                    
                    while (dtrsupid.Read())
                    {
                        string sti = "";
                        SqlConnection storecnn = new SqlConnection();
                        SqlCommand storecmd = new SqlCommand();

                        try
                        {
                            storecnn.ConnectionString = connectionString;
                            storecnn.Open();

                            storecmd.Connection = storecnn;
                            storecmd.CommandText = "SELECT StoreID,StoreName,StoreAddress1,StoreCity,StoreState,StoreZip,StorePhone,StoreFax from Configuration";

                            SqlDataReader storedtr = storecmd.ExecuteReader();

                            if (storedtr.HasRows)
                            {
                                while (storedtr.Read())
                                {
                                    sLine = storedtr[1].ToString();
                                    
                                    sti = storedtr[0].ToString();
                                }
                            }

                            storedtr.Close();
                            storecnn.Close();
                        }
                        catch
                        {

                        }

                        string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
                                               
                        /// Insert in PurchaseEntry
                        /// 
                        cnn3.ConnectionString = connectionString;
                        cnn3.Open();


                        try
                        {
                            sLine = dtrsupid[5].ToString();
                            nc = sLine.Length;
                            tcn = "";
                            subcadena = "";

                            for (int iline = 0; iline < nc; iline++)
                            {
                                tcn = sLine.Substring(iline, 1);
                                if (tcn == "'")
                                {
                                    tcn = "''";
                                    subcadena = subcadena + tcn;
                                }
                                else
                                {
                                    subcadena = subcadena + tcn;
                                }
                            }
                            subcadena = subcadena.TrimEnd();
                            subcadena = subcadena.TrimStart();

                                                       
                            try
                            {
                                string cant = "1"; 

                                cmd3.Connection = cnn3;
                                cmd3.CommandText = "INSERT INTO [PurchaseOrderEntry] ([ItemDescription],[LastUpdated]" +
                                @",[QuantityReceivedToDate],[StoreID],[PurchaseOrderID],[QuantityOrdered],[QuantityReceived],[ItemID]" +
                                @",[OrderNumber],[Price],[TaxRate],[InventoryOfflineID]) VALUES ('" + subcadena + "','" + now + "',0,'" + sti +
                                @"','" + idinicio + "'," + cant + ",0,'" + dtrsupid[1].ToString() + "','LBL-" + inicio + "'," + dtrsupid[3].ToString() + ",0,0)";

                                cmd3.ExecuteNonQuery();
                            }
                            catch
                            {
                                try
                                {

                                    string cant = "1";

                                    cmd3.Connection = cnn3;
                                    cmd3.CommandText = "INSERT INTO [PurchaseOrderEntry] ([ItemDescription],[LastUpdated]" +
                                    @",[QuantityReceivedToDate],[StoreID],[PurchaseOrderID],[QuantityOrdered],[QuantityReceived],[ItemID]" +
                                    @",[OrderNumber],[Price],[TaxRate],[InventoryOfflineID],[ShippingPerItem],[OtherFeesPerItem]" +
                                    @",[LastQuantityReceived],[LastReceivedDate]) VALUES ('" + subcadena + "','" + now + "',0" +
                                    @",'" + sti + "','" + idinicio + "'," + cant + ",0,'" + dtrsupid[1].ToString() + "','LBL-" + inicio + "'," + dtrsupid[3].ToString() + ",0" +
                                    @",0,0.00,0.00,0,NULL)";

                                    cmd3.ExecuteNonQuery();

                                }
                                catch (SqlException ex)
                                {
                                    ShowDialog("Error en insert" + ex.ToString());
                                }
                            }
                                                                                   
                        }
                        catch (SqlException ex2)
                        {

                            ShowDialog("Error en ingreso de detalle" + ex2.ToString());

                        }



                        string msg = "Item has been added to Purchase Order # LBL-" + inicio;
                        ShowDialog(msg);

                        if (YESNOPOPopup != null) { YESNOPOPopup.IsOpen = false; }

                        txtDescription.IsReadOnly = true;
                        txtItemNumber.IsReadOnly = true;
                        txtCost.IsReadOnly = true;
                        txtPrice.IsReadOnly = true;

                        txtScanBarcode.Text = "";
                        txtDescription.Text = "";
                        txtItemNumber.Text = "";

                        ComboDepartment.Items.Clear();
                        ComboCategory.Items.Clear();

                        ComboTax.Items.Clear();


                        txtCost.Text = "";
                        txtPrice.Text = "";


                        ItemClass.AddData("", "", "", "", "", "", "", "", "", "", "");
                        PricingClass.AddData("", "", "", "", "", "", "", "");
                        DiscountClass.AddData("", "", "", "", "", "", "", "");
                        MoreClass.AddData("", "", "");

                        cnn3.Close();
                        cnnsupid.Close();
                    }
                }
            }
            catch { }

        }
    }
    
}
