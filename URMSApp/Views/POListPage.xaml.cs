using SqliteData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using URMSApp.Models;
using System.ComponentModel;
using URMSApp.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace URMSApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class POListPage : Page
    {

        public POListPage()
        {
            this.InitializeComponent();

            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }


        private void BtnRecPO_Click(object sender, RoutedEventArgs e)
        {
            BtnNewPO.IsChecked = false;
            BtnEditPO.IsChecked = false;
            BtnRecPO.IsChecked = false;
            BtnDelPO.IsChecked = false;

            string pon = PurchaseClass.GetPurchasePONumber();

            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            string connectionString;
            string srv = Class1.GetServer();
            string srvu = Class1.GetUser();
            string srvp = Class1.GetPass();
            string srvdb = Class1.GetDb();


            cnn.ConnectionString = connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            cnn.Open();

            cmd.Connection = cnn;
            cmd.CommandText = "SELECT [Status],[ID] FROM [PurchaseOrder]  WHERE [PONumber] = '" + pon + "'";

            SqlDataReader dtr = cmd.ExecuteReader();

            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    if (dtr[0].ToString() == "0")
                    {

                        PurchaseClass.AddPurchase(dtr[1].ToString(), pon);

                        PurchaseOrderFilterType.AddPurchaseType("Receive");

                        Frame.Navigate(typeof(PurchaseOrderPage));
                    }
                    else
                    {
                        string msg = "This Purchase has been received";
                        ShowDialog(msg);
                    }
                }
            }

        }

        private void BtnEditPO_Click(object sender, RoutedEventArgs e)
        {
            string pon = PurchaseClass.GetPurchasePONumber();

            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            string connectionString;
            string srv = Class1.GetServer();
            string srvu = Class1.GetUser();
            string srvp = Class1.GetPass();
            string srvdb = Class1.GetDb();
            string pui = "";

            cnn.ConnectionString = connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            cnn.Open();

            cmd.Connection = cnn;
            cmd.CommandText = "SELECT [ID],[Status] FROM [PurchaseOrder]  WHERE [PONumber] = '" + pon + "'";

            SqlDataReader dtr = cmd.ExecuteReader();

            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    if (dtr[1].ToString() == "2")
                    {
                        ShowDialog("Disable to edit this Purchase Order, it has been received");
                    }
                    pui = dtr[0].ToString();
                }
            }
            PurchaseClass.AddPurchase(pui, pon);
            PurchaseOrderFilterType.AddPurchaseType("Edit");

            
            BtnNewPO.IsChecked = false;
            BtnEditPO.IsChecked = false;
            BtnRecPO.IsChecked = false;
            BtnDelPO.IsChecked = false;

            Frame.Navigate(typeof(PurchaseOrderPage));
        }

        private void BtnNewPO_Click(object sender, RoutedEventArgs e)
        {
            BtnNewPO.IsChecked = true;
            BtnEditPO.IsChecked = false;
            BtnRecPO.IsChecked = false;
            BtnDelPO.IsChecked = false;

            BtnEditPO.IsEnabled = false;
            BtnRecPO.IsEnabled = false;
            BtnDelPO.IsEnabled = false;

            PurchaseClass.AddPurchase("", "");
            FilterPurchasePopup.IsOpen = true;
        }


        private void RbtnPOSup_Click(object sender, RoutedEventArgs e)
        {
            ListG.Items.Clear();

            ListG.IsEnabled = true;
            //BtnAll.IsEnabled = true;
            //BtnNone.IsEnabled = true;

            string srv = "";
            string srvu = "";
            string srvp = "";
            string srvdb = "";

            srv = Class1.GetServer();
            srvu = Class1.GetUser();
            srvp = Class1.GetPass();
            srvdb = Class1.GetDb();

            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            String connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();

                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [SupplierName] FROM Supplier";
                SqlDataReader dtr = cmd.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        ListG.Items.Add(dtr[0].ToString());
                    }
                }
            }
            catch
            {

            }
        }

        private void RbtnBlank_Click(object sender, RoutedEventArgs e)
        {
            ListG.IsEnabled = false;
            //BtnAll.IsEnabled = false;
            //BtnNone.IsEnabled = false;

            ListG.Items.Clear();
        }

        private void FilterPurchasePopup_Loaded(object sender, RoutedEventArgs e)
        {
            ListG.IsEnabled = false;
            //BtnAll.IsEnabled = false;
            //BtnNone.IsEnabled = false;
        }

        private void SimulateSaveClicked(object sender, RoutedEventArgs e)
        {
            BtnNewPO.IsChecked = false;
            BtnEditPO.IsChecked = false;
            BtnRecPO.IsChecked = false;
            BtnDelPO.IsChecked = false;

            BtnEditPO.IsEnabled = true;
            BtnRecPO.IsEnabled = true;
            BtnDelPO.IsEnabled = true;

            if (FilterPurchasePopup != null) { FilterPurchasePopup.IsOpen = false; }
            if (DeletePOPopup != null) { DeletePOPopup.IsOpen = false; }

        }

        private void BtnOkFilter_Click_1(object sender, RoutedEventArgs e)
        {
            BtnNewPO.IsChecked = false;
            BtnEditPO.IsChecked = false;
            BtnRecPO.IsChecked = false;
            BtnDelPO.IsChecked = false;

            BtnEditPO.IsEnabled = true;
            BtnRecPO.IsEnabled = true;
            BtnDelPO.IsEnabled = true;

            string sLine = "";
            int nc = 0;
            string tcn = "";
            string subcadena = "";
            string sname = "";
            
            PurchaseOrderFilterType.AddPurchaseType("New");

            if (RbtnBlank.IsChecked == true)
            {
                Frame.Navigate(typeof(PurchaseOrderPage));
            }
            else if (RbtnManu.IsChecked == true)
            {
                Frame.Navigate(typeof(PurchaseOrderPageManu));
            }
            else if (RbtnPOSup.IsChecked == true)
            {
                int pocont = 0;

                string srv = "";
                string srvu = "";
                string srvp = "";
                string srvdb = "";


                srv = Class1.GetServer();
                srvu = Class1.GetUser();
                srvp = Class1.GetPass();
                srvdb = Class1.GetDb();

                SqlConnection cnninic = new SqlConnection();
                SqlConnection cnn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                SqlConnection cnn1 = new SqlConnection();
                SqlCommand cmdinic = new SqlCommand();
                SqlCommand cmd1 = new SqlCommand();
                SqlConnection cnn2 = new SqlConnection();
                SqlCommand cmd2 = new SqlCommand();
                SqlConnection cnn3 = new SqlConnection();
                SqlCommand cmd3 = new SqlCommand();
                string inicio = "";

                String connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;

                int poinic = 1;

                try
                {
                    cnninic.ConnectionString = connectionString;
                    cnninic.Open();

                    cmdinic.Connection = cnninic;
                    cmdinic.CommandText = "SELECT [ID] FROM PurchaseOrder order by id asc";
                    SqlDataReader dtrinic = cmdinic.ExecuteReader();

                    if (dtrinic.HasRows)
                    {
                        while (dtrinic.Read())
                        {
                            poinic = Int32.Parse(dtrinic[0].ToString()) + 1;
                        }
                    }

                    if (poinic < 10)
                    {
                        inicio = "00000" + poinic.ToString();
                    }
                    else if (poinic < 100)
                    {
                        inicio = "0000" + poinic.ToString();
                    }
                    else if (poinic < 1000)
                    {
                        inicio = "000" + poinic.ToString();
                    }
                    else if (poinic < 10000)
                    {
                        inicio = "00" + poinic.ToString();
                    }


                    dtrinic.Close();
                    cnninic.Close();
                }
                catch
                {

                }

                string shipto = "";
                string sti = "";
                //Lastupdated / DateCreated / RequiredDate FROM NOWDATE
                //StoreID / shipto: Name / Address / City, State/ Zip / "Phone:" Phone / "Fax:" Fax FROM Configuration
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
                catch
                {

                }

                string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
                string actualponumber = "";

                

                for (int i = 0; i < ListG.SelectedItems.Count; i++)
                {
                    string supidList = "";
                    string lastid = "";
                    var supitem = ListG.SelectedItems[i].ToString();

                    pocont = pocont + 1;

                    double taxrate = 0;

                    //taxrate se obtiene de la suma del total del tax sobre el total del costo de la orden// supplier list o taxrate en orden

                    string to = "";

                    SqlConnection tocnn = new SqlConnection();
                    SqlCommand tocmd = new SqlCommand();

                    try
                    {
                        
                        tocnn.ConnectionString = connectionString;
                        tocnn.Open();

                        tocmd.Connection = tocnn;
                        tocmd.CommandText = "SELECT ID,SupplierName,Address1,City,State,Zip,PhoneNumber,FaxNumber, ContactName from Supplier where SupplierName = '" + supitem + "'";

                        SqlDataReader todtr = tocmd.ExecuteReader();

                       
                        if (todtr.HasRows)
                        {
                            while (todtr.Read())
                            {
                                sLine = todtr[1].ToString();
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


                                supidList = todtr[0].ToString();
                                to = subcadena + " " + todtr[2].ToString() + " " + todtr[3].ToString() + "," +
                                    @" " + todtr[4].ToString() + " " + todtr[5].ToString() + " Phone: " + todtr[6].ToString() + " Fax: " + todtr[7].ToString() + " Contact:" + todtr[8].ToString();

                                sname = subcadena;
                            }
                        }

                        todtr.Close();
                        tocnn.Close();
                    }
                    catch
                    {

                    }

                    SqlConnection cnninsert = new SqlConnection();
                    SqlCommand cmdinsert = new SqlCommand();

                    try
                    {

                        cnninsert.ConnectionString = connectionString;
                        cnninsert.Open();

                        cmdinsert.Connection = cnninsert;
                        cmdinsert.CommandText = "INSERT INTO [PurchaseOrder] ([LastUpdated],[POTitle],[POType],[StoreID]" +
                        @",[WorksheetID],[PONumber],[Status],[DateCreated],[To],[ShipTo],[Requisitioner],[ShipVia],[FOBPoint],[Terms]" +
                        @",[TaxRate],[Shipping],[Freight],[RequiredDate],[ConfirmingTo],[Remarks],[SupplierID],[OtherStoreID],[CurrencyID]" +
                        @",[ExchangeRate],[OtherPOID],[InventoryLocation],[IsPlaced],[DatePlaced],[BatchNumber]) VALUES ('" + now + "','',0,'" + sti + "',0,'',0,'" + now + "'" +
                        @",'" + to + "','" + shipto + "','','','',''," + taxrate + ",0.00,'','" + now + "','','','" + supidList.ToString() + "',0,0,1,0,0,0,NULL,0)";

                        cmdinsert.ExecuteNonQuery();
                        cnninsert.Close();
                    }
                    catch
                    {
                        try
                        {

                            cnninsert.ConnectionString = connectionString;
                            cnninsert.Open();

                            cmdinsert.Connection = cnninsert;
                            cmdinsert.CommandText = "INSERT INTO [PurchaseOrder] ([LastUpdated],[POTitle],[POType],[StoreID]" +
                            @",[WorksheetID],[PONumber],[Status],[DateCreated],[To],[ShipTo],[Requisitioner],[ShipVia],[FOBPoint],[Terms]" +
                            @",[TaxRate],[Shipping],[Freight],[RequiredDate],[ConfirmingTo],[Remarks],[SupplierID],[OtherStoreID],[CurrencyID]" +
                            @",[ExchangeRate],[OtherPOID],[InventoryLocation],[IsPlaced],[DatePlaced],[BatchNumber],[EstShipping],[CurrentShipping]" +
                            @",[EstOtherFees],[CurrentOtherFees],[OtherFees],[CostDistributionMethod],[ParentPOId],[RootPOId],[OriginPOId]" +
                            @",[MasterPO],[DiscrepancyStatus]) VALUES ('" + now + "','',0,'" + sti + "',0,'',0,'" + now + "'" +
                            @",'" + to + "','" + shipto + "','','','',''," + taxrate + ",0.00,'','" + now + "','','','" + supidList.ToString() + "',0,0,1,0,0,0,NULL,0" +
                            @",0.00,0.00,0.00,0.00,0.00,0,0,0,0,"",0)";

                            cmdinsert.ExecuteNonQuery();
                            cnninsert.Close();
                        }
                        catch (SqlException ex1)
                        {
                            ShowDialog(ex1.ToString());
                        }
                    }

                    try
                    {
                        cnn.ConnectionString = connectionString;
                        cnn.Open();

                        cmd.Connection = cnn;
                        cmd.CommandText = "SELECT [ID] FROM PurchaseOrder order by id asc";
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
                    catch
                    {

                    }

                    
                    if (Int32.Parse(lastid) < 10)
                    {
                        actualponumber = "00000" + lastid.ToString();
                    }
                    else if (Int32.Parse(lastid) < 100)
                    {
                        actualponumber = "0000" + lastid.ToString();
                    }
                    else if (Int32.Parse(lastid) < 1000)
                    {
                        actualponumber = "000" + lastid.ToString();
                    }
                    else if (Int32.Parse(lastid) < 10000)
                    {
                        actualponumber = "00" + lastid.ToString();
                    }

                    

                    //Update PurchaseOrder
                    try
                    {
                        SqlConnection cnn5 = new SqlConnection();
                        SqlCommand cmd5 = new SqlCommand();
                        cnn5.ConnectionString = connectionString;
                        cnn5.Open();

                        cmd5.Connection = cnn5;
                        cmd5.CommandText = "UPDATE [PurchaseOrder] SET [PONumber] = '" + actualponumber + "'  WHERE ID = '" + lastid + "'";

                        cmd5.ExecuteNonQuery();

                       
                        cnn5.Close();
                    }
                    catch
                    {
                        try
                        {
                            SqlConnection cnn5 = new SqlConnection();
                            SqlCommand cmd5 = new SqlCommand();
                            cnn5.ConnectionString = connectionString;
                            cnn5.Open();

                            cmd5.Connection = cnn5;
                            cmd5.CommandText = "UPDATE [PurchaseOrder] SET [PONumber] = '" + actualponumber + "',[OriginPOId] = '" + lastid + "'  WHERE ID = '" + lastid + "'";

                            cmd5.ExecuteNonQuery();


                            cnn5.Close();
                        }
                        catch (SqlException ex2)
                        {
                            ShowDialog(ex2.ToString());
                        }

                    }


                    /// Insert en PurchaseEntry
                    /// 
                    try
                    {
                        float POtaxrate = 0;
                        float taxtotal = 0;
                        float costototal = 0;

                        cnn2.ConnectionString = connectionString;
                        cnn2.Open();

                        cmd2.Connection = cnn2;
                        cmd2.CommandText = "SELECT [MinimumOrder],Item.[ID],SupplierList.[SupplierID],Item.[Cost],[TaxRate],Item.[Description] FROM " +
                            @"Item left join SupplierList on Item.ID = SupplierList.ItemID where SupplierList.[SupplierID] = '" + supidList.ToString() + "'";
                        SqlDataReader dtr2 = cmd2.ExecuteReader();

                        if (dtr2.HasRows)
                        {
                            cnn3.ConnectionString = connectionString;
                            cnn3.Open();

                            while (dtr2.Read())
                            {
                                sLine = dtr2[5].ToString();
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
                                    string cant = dtr2[0].ToString();
                                    if (cant == "0") { cant = "1"; }

                                    cmd3.Connection = cnn3;
                                    cmd3.CommandText = "INSERT INTO [PurchaseOrderEntry] ([ItemDescription],[LastUpdated]" +
                                    @",[QuantityReceivedToDate],[StoreID],[PurchaseOrderID],[QuantityOrdered],[QuantityReceived],[ItemID]" +
                                    @",[OrderNumber],[Price],[TaxRate],[InventoryOfflineID]) VALUES ('"+subcadena+"','"+now+"',0,'"+sti+"','"+lastid+"',"+cant+ ",0,'"+dtr2[1].ToString()+ "',''," + dtr2[3].ToString() + "," + dtr2[4].ToString() + ",0)";


                                    
                                    cmd3.ExecuteNonQuery();
                                    
                                    costototal = costototal + float.Parse(dtr2[3].ToString())* float.Parse(cant);
                                    taxtotal = taxtotal + float.Parse(dtr2[3].ToString()) * float.Parse(cant) * float.Parse(dtr2[4].ToString())/100;

                                }
                                catch
                                {
                                    try
                                    {
                                        string cant = dtr2[0].ToString();
                                        if (cant == "0") { cant = "1"; }

                                        cmd3.Connection = cnn3;
                                        cmd3.CommandText = "INSERT INTO [PurchaseOrderEntry] ([ItemDescription],[LastUpdated]" +
                                        @",[QuantityReceivedToDate],[StoreID],[PurchaseOrderID],[QuantityOrdered],[QuantityReceived],[ItemID]" +
                                        @",[OrderNumber],[Price],[TaxRate],[InventoryOfflineID],[ShippingPerItem],[OtherFeesPerItem]" +
                                        @",[LastQuantityReceived],[LastReceivedDate]) VALUES ('" + subcadena + "','" + now + "',0" +
                                        @",'" + sti + "','" + lastid + "'," + cant + ",0,'" + dtr2[1].ToString() + "',''," + dtr2[3].ToString() + "," + 
                                        dtr2[4].ToString() + ",0,0.00,0.00,0,NULL)";

                                        cmd3.ExecuteNonQuery();

                                        costototal = costototal + float.Parse(dtr2[3].ToString()) * float.Parse(cant);
                                        taxtotal = taxtotal + float.Parse(dtr2[3].ToString()) * float.Parse(cant) * float.Parse(dtr2[4].ToString()) / 100;
                                    }
                                    catch (SqlException ex)
                                    {
                                        ShowDialog("Error en insert" + ex.ToString());
                                    }
                                }
                            }

                            if (costototal == 0) { POtaxrate = 0; } else { POtaxrate = taxtotal / costototal * 100; };
                            

                            try
                            {
                                SqlConnection cnntr = new SqlConnection();
                                SqlCommand cmdtr = new SqlCommand();
                                cnntr.ConnectionString = connectionString;
                                cnntr.Open();
                                ShowDialog(POtaxrate.ToString());
                                cmdtr.Connection = cnntr;
                                cmdtr.CommandText = "UPDATE [PurchaseOrder] SET [TaxRate] = " + POtaxrate.ToString() + "  WHERE ID = '" + lastid + "'";

                                cmdtr.ExecuteNonQuery();
                                

                                cnntr.Close();
                            }
                            catch (SqlException ex2)
                            {
                                ShowDialog(ex2.ToString());
                            }
                        }
                        dtr2.Close();
                    }
                    catch (SqlException ex2)
                    {

                        ShowDialog("Error en ingreso de detalle" + ex2.ToString());

                    }

                    DateTime fa;
                    fa = DateTime.Parse(now);
                    int faM = fa.Month;
                    int fad = fa.Day;
                    int fay = fa.Year;

                    string fech = fay + "/" + faM + "/" + fad;

                    CollectionVM.PurchaseList.Add(new PurchaseOrderClass()
                    {
                        PONumber = actualponumber,
                        POTo = sname,
                        PODate = fech,
                        POStatus = "Open"
                    });

                    cnn2.Close();
                    cnn3.Close();
                }
                 
                string msg = pocont + " Purchase Orders beginning in #"+ inicio +" have been created";
                ShowDialog(msg);

                cnn1.Close();

            }

            RbtnManu.IsChecked = true;
            ListG.IsEnabled = false;
            ListG.Items.Clear();
            if (FilterPurchasePopup != null) { FilterPurchasePopup.IsOpen = false; }
        }

        public async void ShowDialog(string msg)
        {
            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }

        private void BtnNone_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAll_Click(object sender, RoutedEventArgs e)
        {

        }


        private void BtnDelPO_Click(object sender, RoutedEventArgs e)
        {
            BtnNewPO.IsChecked = false;
            BtnEditPO.IsChecked = false;
            BtnRecPO.IsChecked = false;
            BtnDelPO.IsChecked = false;

            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlConnection cnn1 = new SqlConnection();
            SqlCommand cmd1 = new SqlCommand();
            SqlConnection cnn2 = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand();

            string connectionString;
            string srv = Class1.GetServer();
            string srvu = Class1.GetUser();
            string srvp = Class1.GetPass();
            string srvdb = Class1.GetDb();
            string puon = PurchaseClass.GetPurchasePONumber();

            connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;

            cnn.ConnectionString = connectionString;
            cnn.Open();

            try
            {
                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [Status],[ID] FROM [PurchaseOrder]  WHERE [PONumber] = '" + puon + "'";

                SqlDataReader dtr = cmd.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        if (dtr[0].ToString() == "0")
                        {
                            DeletePOPopup.IsOpen = true;

                        }
                        else
                        {
                            string msg = "This Purchase has been received";
                            ShowDialog(msg);
                        }
                    }
                }


            }
            catch { }
            
        }

        private void POListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var POclass = (PurchaseOrderClass)e.ClickedItem;

            string srv = Class1.GetServer();
            string srvu = Class1.GetUser();
            string srvp = Class1.GetPass();
            string srvdb = Class1.GetDb();

            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            
            string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;

            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();

                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [ID],[PONumber] FROM [PurchaseOrder] WHERE [PONumber] = '" + POclass.PONumber + "'";

                SqlDataReader dtr = cmd.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        PurchaseClass.AddPurchase(dtr[0].ToString(), POclass.PONumber);
                    }
                }

                dtr.Close();
            }
            catch { }
            cnn.Close();

        }

        private void ListG_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void BtnOkDelete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlConnection cnn1 = new SqlConnection();
            SqlCommand cmd1 = new SqlCommand();
            SqlConnection cnn2 = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand();

            string connectionString;
            string srv = Class1.GetServer();
            string srvu = Class1.GetUser();
            string srvp = Class1.GetPass();
            string srvdb = Class1.GetDb();
            string puon = PurchaseClass.GetPurchasePONumber();

            connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;

            cnn.ConnectionString = connectionString;
            cnn.Open();

            try
            {
                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [Status],[ID] FROM [PurchaseOrder]  WHERE [PONumber] = '" + puon + "'";

                SqlDataReader dtr = cmd.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        if (dtr[0].ToString() == "0")
                        {

                            try
                            {
                                cnn2.ConnectionString = connectionString;
                                cnn2.Open();

                                cmd2.Connection = cnn2;
                                cmd2.CommandText = "DELETE FROM [PurchaseOrderEntry]  WHERE [PurchaseOrderId] = '" + dtr[1].ToString() + "'";
                                cmd2.ExecuteNonQuery();

                                cnn1.ConnectionString = connectionString;
                                cnn1.Open();

                                cmd1.Connection = cnn1;
                                cmd1.CommandText = "DELETE FROM [PurchaseOrder]  WHERE [PONumber] = '" + puon + "'";
                                cmd1.ExecuteNonQuery();

                                PurchaseClass.AddPurchase("", "");

                                string msg = "Purchase Order has been deleted";
                                ShowDialog(msg);

                                while (POListView.SelectedItems.Count > 0)
                                {
                                    CollectionVM.PurchaseList.Remove((PurchaseOrderClass)POListView.SelectedItem);
                                }

                                if (DeletePOPopup != null) { DeletePOPopup.IsOpen = false; }
                            }
                            catch (SqlException exc)
                            {
                                ShowDialog(exc.ToString());
                            }
                        }
                        else
                        {
                            string msg = "This Purchase has been received";
                            ShowDialog(msg);
                        }
                    }
                }


            }
            catch { }

        }

        private void RbtnManu_Click(object sender, RoutedEventArgs e)
        {
            ListG.IsEnabled = false;
            
            ListG.Items.Clear();
        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            CollectionVM.PurchaseList.Clear();
            //{
            //string poi = PurchaseClass.GetPurchaseId();

            string stat = ComboStatus.SelectedValue.ToString();
            string fes;

            if (DPPO.Date != null)
            { fes = DPPO.Date.Value.ToString("yyyy-MM-dd"); } else { fes = ""; }

            if (stat == "Open")
            { stat = "0"; }
            else if (stat == "Close")
            { stat = "2"; }
            else if (stat == "Partial")
            {
                stat = "1";
            }
            else if (stat == "All")
            {
                stat = "";
            }


            string srv = Class1.GetServer();
            string srvu = Class1.GetUser();
            string srvp = Class1.GetPass();
            string srvdb = Class1.GetDb();

            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlConnection cnn1 = new SqlConnection();
            SqlCommand cmd1 = new SqlCommand();

            string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();

                cmd.Connection = cnn;


                if (fes == "") { 
                    if (stat == "") {
                        cmd.CommandText = "SELECT [Status],PurchaseOrder.[ID],[PONumber],[SupplierName],PurchaseOrder.[DateCreated] FROM [PurchaseOrder] left join Supplier on Supplier.ID = PurchaseOrder.SupplierID ORDER BY PurchaseOrder.[ID]";
                    } else { 
                    cmd.CommandText = "SELECT [Status],PurchaseOrder.[ID],[PONumber],[SupplierName],PurchaseOrder.[DateCreated] FROM [PurchaseOrder] left join Supplier on Supplier.ID = PurchaseOrder.SupplierID where [status] = "+stat+" ORDER BY PurchaseOrder.[ID]";
                    }
                } else {
                    if (stat == "")
                    {
                        cmd.CommandText = "SELECT [Status],PurchaseOrder.[ID],[PONumber],[SupplierName],PurchaseOrder.[DateCreated] FROM [PurchaseOrder] left join Supplier on Supplier.ID = PurchaseOrder.SupplierID WHERE PurchaseOrder.[DateCreated] BETWEEN '" + fes + " 00:00:00.000' AND '" + fes + " 23:59:59.000' ORDER BY PurchaseOrder.[ID]";
                    }
                    else
                    {
                        cmd.CommandText = "SELECT [Status],PurchaseOrder.[ID],[PONumber],[SupplierName],PurchaseOrder.[DateCreated] FROM [PurchaseOrder] left join Supplier on Supplier.ID = PurchaseOrder.SupplierID where [status] = " + stat + " and PurchaseOrder.[DateCreated] BETWEEN '" + fes + " 00:00:00.000' AND '" + fes + " 23:59:59.000' ORDER BY PurchaseOrder.[ID]";
                    }
                }

                SqlDataReader dtr = cmd.ExecuteReader();

                DateTime fa;
                string sta = "";

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        
                        if (dtr[0].ToString() == "0") { sta = "Open"; }
                        else
                        {
                            if (dtr[0].ToString() == "1") { sta = "Partial"; }
                            else { if (dtr[0].ToString() == "2") { sta = "Close"; } }
                        }

                        fa = DateTime.Parse(dtr[4].ToString());
                        int faM = fa.Month;
                        int fad = fa.Day;
                        int fay = fa.Year;

                        string fech = fay + "/" + faM + "/" + fad;
                       
                        
                        CollectionVM.PurchaseList.Add(new PurchaseOrderClass()
                        {
                            PONumber = dtr[2].ToString(),
                            POTo = dtr[3].ToString(),
                            PODate = fech,
                            POStatus = sta
                        });
                    }

                }
                else
                {
                    String msg = "";
                    msg = "There are no " + sta + " Purchase Orders";
                    ShowDialog(msg);
                }

                dtr.Close();
                cnn.Close();
            }
            catch
            {
                //String msg = "";
                //msg = "Verify Connection";
                //ShowDialog(msg);
            }

            //ChkFPO.IsChecked = false;
            //BtnFilter.IsEnabled = false;
            //DPPO.Date = null;
            //DPPO.IsEnabled = false;
            //ComboStatus.SelectedValue = null;
        }

      

        private void ChkFPO_Click(object sender, RoutedEventArgs e)
        {
            if (ChkFPO.IsChecked == true)
            {
                DPPO.IsEnabled = true;
            }
            else
            {
                DPPO.Date = null;
                DPPO.IsEnabled = false;
            }
        }

        private void ComboStatus_DropDownOpened(object sender, object e)
        {
            
            BtnFilter.IsEnabled = true;
            
            
        }
    }
}
