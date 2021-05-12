﻿using System;
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
using Windows.UI.Popups;
using URMSApp.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace URMSApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PurchaseOrderPageManu : Page
    {
        string ilstchose = "";
        string ipoidchose = "";
        public PurchaseOrderPageManu()
        {
            this.InitializeComponent();
            
                PurchasePivot.SelectedItem = pivotHeader;
                BtnReceiveI.Visibility = Visibility.Collapsed;
                ItemListView.IsEnabled = true;
                //BtnLookTo.IsEnabled = true;
                //BtnLookShip.IsEnabled = true;
                TextTo.IsEnabled = true;
                TextShipTo.IsEnabled = false;
                BtnAddI.IsEnabled = true;
                BtnDeleteI.IsEnabled = true;
            
        }

        public async void ShowDialog(string msg)
        {
            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }

        private void SimulateSaveClicked(object sender, RoutedEventArgs e)
        {
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
                cmd.CommandText = "DELETE from PurchaseOrderEntry where PurchaseOrderId = '0'";

                SqlDataReader dtr = cmd.ExecuteReader();
            }
            catch { }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void BtnOkPO_Click(object sender, RoutedEventArgs e)
        {

            string ft = PurchaseOrderFilterType.GetPurchaseType();

            string sLine = "";
            int nc = 0;
            string tcn = "";
            string subcadena = "";

            if (ft == "New")
            {

                //int pocont = 0;
                string srv = Class1.GetServer();
                string srvu = Class1.GetUser();
                string srvp = Class1.GetPass();
                string srvdb = Class1.GetDb();
                string inicio = "";
                string sname = "";

                SqlConnection cnn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                SqlConnection cnninic = new SqlConnection();
                SqlCommand cmdinic = new SqlCommand();

                string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                //cnn.ConnectionString = connectionString;
                //cnn.Open();

                //cmd.Connection = cnn;
                //cmd.CommandText = "SELECT PurchaseOrderEntry.PurchaseOrderID,Item.SupplierID from " +
                //    @" PurchaseOrderEntry inner join Item on Item.ID =  PurchaseOrderEntry.ItemID where PurchaseOrderEntry.PurchaseOrderID = '0' GROUP BY Item.SupplierID, PurchaseOrderEntry.PurchaseOrderID";

                //SqlDataReader dtr = cmd.ExecuteReader();

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
                }
                catch { }

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

                sLine = TextShipTo.Text;
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

                sti = "";
                shipto = subcadena;
                string t = DateTime.Now.ToString("yyyy-MM-dd");
                string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
                string actualponumber = "";




                string lastid = "";


                CollectionVM.ItemList.Count();

                float iexost = 0;
                float itax = 0;
                float realtax = 0;

                for (int k = 0; k < CollectionVM.ItemList.Count(); k++)
                {

                    var itemobj = (Item)CollectionVM.ItemList[k];
                    iexost = iexost + float.Parse(itemobj.Cost) * float.Parse(itemobj.Qtyorder);
                    realtax = float.Parse(itemobj.Cost) * float.Parse(itemobj.Qtyorder) * float.Parse(itemobj.Tax) / 100;
                    itax = itax + realtax;
                }

                float taxrate = itax / iexost * 100;
                string textestsh = TextEstship.Text;

                //taxrate se obtiene de la suma del total del tax sobre el total del costo de la orden// supplier list o taxrate en orden

                //string to = "";


                sLine = TextTo.Text;
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


                sname = subcadena;



                SqlConnection cnn1 = new SqlConnection();
                SqlCommand cmd1 = new SqlCommand();

                try
                {

                    cnn1.ConnectionString = connectionString;
                    cnn1.Open();

                    cmd1.Connection = cnn1;
                    cmd1.CommandText = "INSERT INTO [PurchaseOrder] ([LastUpdated],[POTitle],[POType],[StoreID]" +
                    @",[WorksheetID],[PONumber],[Status],[DateCreated],[To],[ShipTo],[Requisitioner],[ShipVia],[FOBPoint],[Terms]" +
                    @",[TaxRate],[Shipping],[Freight],[RequiredDate],[ConfirmingTo],[Remarks],[SupplierID],[OtherStoreID],[CurrencyID]" +
                    @",[ExchangeRate],[OtherPOID],[InventoryLocation],[IsPlaced],[DatePlaced],[BatchNumber]) VALUES ('" + now + "','',0,'" + sti + "',0,'',0,'" + now + "'" +
                    @",'" + sname + "','" + shipto + "','','','','',0,0.00,'','" + now + "','','','',0,0,1,0,0,0,NULL,0)";

                    cmd1.ExecuteNonQuery();
                    cnn1.Close();
                }
                catch
                {
                    try
                    {

                        cnn1.ConnectionString = connectionString;
                        cnn1.Open();

                        cmd1.Connection = cnn1;
                        cmd1.CommandText = "INSERT INTO [PurchaseOrder] ([LastUpdated],[POTitle],[POType],[StoreID]" +
                        @",[WorksheetID],[PONumber],[Status],[DateCreated],[To],[ShipTo],[Requisitioner],[ShipVia],[FOBPoint],[Terms]" +
                        @",[TaxRate],[Shipping],[Freight],[RequiredDate],[ConfirmingTo],[Remarks],[SupplierID],[OtherStoreID],[CurrencyID]" +
                        @",[ExchangeRate],[OtherPOID],[InventoryLocation],[IsPlaced],[DatePlaced],[BatchNumber],[EstShipping],[CurrentShipping]" +
                        @",[EstOtherFees],[CurrentOtherFees],[OtherFees],[CostDistributionMethod],[ParentPOId],[RootPOId],[OriginPOId]" +
                        @",[MasterPO],[DiscrepancyStatus]) VALUES ('" + now + "','',0,'" + sti + "',0,'',0,'" + now + "'" +
                        @",'" + sname + "','" + shipto + "','','','','',0,0.00,'','" + now + "','','','',0,0,1,0,0,0,NULL,0," + textestsh + "," +
                        @"0.00,0.00,0.00,0.00,0,0,0,0,"",0)";

                        cmd1.ExecuteNonQuery();
                        cnn1.Close();
                    }
                    catch (SqlException ex1)
                    {
                        ShowDialog(ex1.ToString());
                    }
                }

                SqlConnection pocnn = new SqlConnection();
                SqlCommand pocmd = new SqlCommand();
                try
                {
                    pocnn.ConnectionString = connectionString;
                    pocnn.Open();
                    pocmd.Connection = pocnn;
                    pocmd.CommandText = "SELECT [ID] FROM PurchaseOrder order by id asc";
                    SqlDataReader pudtr = pocmd.ExecuteReader();

                    if (pudtr.HasRows)
                    {
                        while (pudtr.Read())
                        {
                            lastid = pudtr[0].ToString();

                        }
                    }

                    pudtr.Close();
                    pocnn.Close();
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
                    cmd5.CommandText = "UPDATE [PurchaseOrder] SET [PONumber] = '" + actualponumber + "' WHERE ID = '" + lastid + "'";

                    cmd5.ExecuteNonQuery();
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
                    SqlConnection cnn2 = new SqlConnection();
                    SqlCommand cmd2 = new SqlCommand();
                    cnn2.ConnectionString = connectionString;
                    cnn2.Open();

                    cmd2.Connection = cnn2;
                    cmd2.CommandText = "SELECT PurchaseOrderEntry.[ID], PurchaseOrderEntry.ItemID FROM PurchaseOrderEntry inner join Item on" +
                        @" Item.ID = PurchaseOrderEntry.ItemID where PurchaseOrderEntry.[PurchaseOrderID] = '0'";
                    SqlDataReader dtr2 = cmd2.ExecuteReader();

                    if (dtr2.HasRows)
                    {
                        SqlConnection cnn3 = new SqlConnection();
                        SqlCommand cmd3 = new SqlCommand();

                        cnn3.ConnectionString = connectionString;
                        cnn3.Open();

                        while (dtr2.Read())
                        {
                            try
                            {
                                cmd3.Connection = cnn3;
                                cmd3.CommandText = "UPDATE [PurchaseOrderEntry] SET [PurchaseOrderID] = '" + lastid + "'  Where ID = '" + dtr2[0].ToString() + "'";

                                cmd3.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                ShowDialog("Error en insert" + ex.ToString());
                            }

                        }
                        cnn3.Close();
                        dtr2.Close();
                        cnn2.Close();
                    }


                }
                catch (SqlException ex2)
                {

                    ShowDialog("Error en ingreso de detalle" + ex2.ToString());

                }

                

                

            CollectionVM.PurchaseList.Add(new PurchaseOrderClass()
            {
                PONumber = actualponumber,
                POTo = sname,
                PODate = t,
                POStatus = "Open"
            });

            string msg = "Purchase Order has been created";
                ShowDialog(msg);

            }         

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }

            PurchaseClass.AddPurchase("", "");
        }

        private void ToPopup_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (ToPopup != null) { ToPopup.IsOpen = false; }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            TextTo.Text = "";
            if (ToPopup != null) { ToPopup.IsOpen = false; }
        }

        private void BtnLookTo_Click(object sender, RoutedEventArgs e)
        {
            TextTo.IsEnabled = true;
        }

        private void ShiptoPopup_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOkCus_Click(object sender, RoutedEventArgs e)
        {
            //string shiptoname = "";
            ////shiptoname = ListCus.SelectedItems[0].ToString();

            //TextTo.Text = shiptoname;
        }

        private void BtnCancelCus_Click(object sender, RoutedEventArgs e)
        {
            if (ShiptoPopup != null) { ShiptoPopup.IsOpen = false; }
        }

        private void BtnReceiveI_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnAddI_Click(object sender, RoutedEventArgs e)
        {
            AddIPopup.IsOpen = true;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string ft = PurchaseOrderFilterType.GetPurchaseType();
            string sLine = "";
            int nc = 0;
            string tcn = "";
            string subcadena = "";

            if (TextBAddItem.Text != "")
            {
                if (ft == "New")
                {
                    SqlConnection cnn = new SqlConnection();
                    SqlConnection cnn1 = new SqlConnection();
                    SqlConnection cnn2 = new SqlConnection();
                    SqlConnection cnn3 = new SqlConnection();
                    SqlCommand cmd = new SqlCommand();
                    SqlCommand cmd1 = new SqlCommand();
                    SqlCommand cmd2 = new SqlCommand();
                    SqlCommand cmd3 = new SqlCommand();
                    string sti = "";

                    String srv = Class1.GetServer();
                    String srvu = Class1.GetUser();
                    String srvp = Class1.GetPass();
                    String srvdb = Class1.GetDb();
                    string poi = PurchaseClass.GetPurchaseId();
                    string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
                    string ic = TextBAddItem.Text;

                    string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                    try
                    {
                        cnn.ConnectionString = connectionString;
                        cnn.Open();

                        cmd.Connection = cnn;
                        cmd.CommandText = "SELECT [ItemLookupCode],[Description],[ID],[Cost] FROM Item Where [ItemLookupCode] = '" + ic + "'";
                        SqlDataReader dtr = cmd.ExecuteReader();

                        cnn1.ConnectionString = connectionString;
                        cnn1.Open();

                        cmd1.Connection = cnn1;
                        cmd1.CommandText = "SELECT [StoreID] FROM Configuration";
                        SqlDataReader dtr1 = cmd1.ExecuteReader();

                        if (dtr1.HasRows)
                        {
                            while (dtr1.Read())
                            {
                                sti = dtr1[0].ToString();
                            }
                        }

                        if (dtr.HasRows)
                        {
                            while (dtr.Read())
                            {
                                string cant = TextBCantAddItem.Text;

                                sLine = dtr[1].ToString();
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
                                    cnn2.ConnectionString = connectionString;
                                    cnn2.Open();

                                    cmd2.Connection = cnn2;
                                    cmd2.CommandText = "INSERT INTO [PurchaseOrderEntry] ([ItemDescription],[LastUpdated]" +
                                    @",[QuantityReceivedToDate],[StoreID],[PurchaseOrderID],[QuantityOrdered],[QuantityReceived],[ItemID]" +
                                    @",[OrderNumber],[Price],[TaxRate],[InventoryOfflineID]) VALUES ('" + subcadena + "','" + now + "',0,'" + sti + "','" + poi + "'," + cant + "," +
                                    @"0," + dtr[2].ToString() + ",''," + dtr[3].ToString() + ",0,0)";

                                    cmd2.ExecuteNonQuery();

                                    SqlConnection cnnitem = new SqlConnection();
                                    SqlCommand cmditem = new SqlCommand();
                                    string lastitem = "";

                                    cnnitem.ConnectionString = connectionString;
                                    cnnitem.Open();

                                    cmditem.Connection = cnnitem;
                                    cmditem.CommandText = "SELECT [ID] FROM PurchaseOrderEntry order by ID asc";
                                    SqlDataReader dtritem = cmditem.ExecuteReader();

                                    if (dtritem.HasRows)
                                    {
                                        while (dtritem.Read())
                                        {
                                            lastitem = dtritem[0].ToString();
                                        }
                                    }

                                    CollectionVM.ItemList.Add(new Models.Item()
                                    {
                                        ItemLookupCode = dtr[0].ToString(),
                                        Description = dtr[1].ToString(),
                                        Qtyorder = cant,
                                        Cost = dtr.GetDecimal(3).ToString("0.00"),
                                        Extcost = (float.Parse(cant) * float.Parse(dtr.GetDecimal(3).ToString("0.00"))).ToString(),
                                        Tax = "0",
                                        IPOid = lastitem
                                    });
                                }
                                catch
                                {
                                    try
                                    {
                                        cnn3.ConnectionString = connectionString;
                                        cnn3.Open();

                                        cmd3.Connection = cnn3;
                                        cmd3.CommandText = "INSERT INTO [PurchaseOrderEntry] ([ItemDescription],[LastUpdated]" +
                                        @",[QuantityReceivedToDate],[StoreID],[PurchaseOrderID],[QuantityOrdered],[QuantityReceived],[ItemID]" +
                                        @",[OrderNumber],[Price],[TaxRate],[InventoryOfflineID],[ShippingPerItem],[OtherFeesPerItem]" +
                                        @",[LastQuantityReceived],[LastReceivedDate]) VALUES ('" + subcadena + "','" + now + "',0" +
                                        @",'" + sti + "','" + poi + "'," + cant + ",0,'" + dtr[2].ToString() + "',''," + dtr[3].ToString() + "," +
                                        @"0,0,0.00,0.00,0,NULL)";

                                        cmd3.ExecuteNonQuery();

                                        SqlConnection cnnitem = new SqlConnection();
                                        SqlCommand cmditem = new SqlCommand();
                                        string lastitem = "";

                                        cnnitem.ConnectionString = connectionString;
                                        cnnitem.Open();

                                        cmditem.Connection = cnnitem;
                                        cmditem.CommandText = "SELECT [ID] FROM PurchaseOrderEntry order by ID asc";
                                        SqlDataReader dtritem = cmditem.ExecuteReader();

                                        if (dtritem.HasRows)
                                        {
                                            while (dtritem.Read())
                                            {
                                                lastitem = dtritem[0].ToString();
                                            }
                                        }

                                        CollectionVM.ItemList.Add(new Models.Item()
                                        {
                                            ItemLookupCode = dtr[0].ToString(),
                                            Description = dtr[1].ToString(),
                                            Qtyorder = cant,
                                            Cost = dtr.GetDecimal(3).ToString("0.00"),
                                            Extcost = (float.Parse(cant) * float.Parse(dtr.GetDecimal(3).ToString("0.00"))).ToString(),
                                            Tax = "0",
                                            IPOid = lastitem
                                        });
                                    }
                                    catch (SqlException ex)
                                    {
                                        ShowDialog("Error en insert" + ex.ToString());
                                    }


                                    cnn3.Close();
                                }
                                cnn2.Close();
                            }
                        }
                        else
                        {
                            String msg;
                            msg = "Scan Barcode does not exist";
                            ShowDialog(msg);
                        }

                        dtr.Close();
                        cnn.Close();
                    }
                    catch (SqlException sqex)
                    {
                        String msg;
                        msg = "Verify Connection " + sqex.ToString();
                        ShowDialog(msg);
                    }

                    CollectionVM.ItemList.Count();

                    float iexost = 0;
                    float itax = 0;
                    float realtax = 0;

                    for (int k = 0; k < CollectionVM.ItemList.Count(); k++)
                    {

                        var itemobj = (Item)CollectionVM.ItemList[k];
                        iexost = iexost + float.Parse(itemobj.Cost) * float.Parse(itemobj.Qtyorder);
                        realtax = float.Parse(itemobj.Cost) * float.Parse(itemobj.Qtyorder) * float.Parse(itemobj.Tax) / 100;
                        itax = itax + realtax;
                    }
                    
                    float total = iexost + itax;
                    
                    TextSubtotal.Text = Convert.ToDecimal(iexost).ToString("0.00");
                    TextSalestax.Text = Convert.ToDecimal(itax).ToString("0.00");
                    TextTotal.Text = Convert.ToDecimal(total).ToString("0.00");
                }

            }
            else
            {
                String msg;
                msg = "Fill Barcode Field";
                ShowDialog(msg);
            }
            TextBAddItem.Text = "";
            TextBCantAddItem.Text = "1";
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            if (AddIPopup != null) { AddIPopup.IsOpen = false; }
        }

        private void TextBAddItem_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            string sLine = "";
            int nc = 0;
            string tcn = "";
            string subcadena = "";

            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (TextBAddItem.Text != "")
                {
                    SqlConnection cnn = new SqlConnection();
                    SqlConnection cnn1 = new SqlConnection();
                    SqlConnection cnn2 = new SqlConnection();
                    SqlConnection cnn3 = new SqlConnection();
                    SqlCommand cmd = new SqlCommand();
                    SqlCommand cmd1 = new SqlCommand();
                    SqlCommand cmd2 = new SqlCommand();
                    SqlCommand cmd3 = new SqlCommand();
                    string sti = "";
                    string cant = TextBCantAddItem.Text;

                    String srv = Class1.GetServer();
                    String srvu = Class1.GetUser();
                    String srvp = Class1.GetPass();
                    String srvdb = Class1.GetDb();
                    string poi = PurchaseClass.GetPurchaseId();
                    string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
                    string ic = TextBAddItem.Text;

                    string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                    try
                    {
                        cnn.ConnectionString = connectionString;
                        cnn.Open();

                        cmd.Connection = cnn;
                        cmd.CommandText = "SELECT [ItemLookupCode],[Description],[ID],[Cost] FROM Item Where [ItemLookupCode] = '" + ic + "'";
                        SqlDataReader dtr = cmd.ExecuteReader();

                        cnn1.ConnectionString = connectionString;
                        cnn1.Open();
                         
                        cmd1.Connection = cnn1;
                        cmd1.CommandText = "SELECT [StoreID] FROM Configuration";
                        SqlDataReader dtr1 = cmd1.ExecuteReader();

                        if (dtr1.HasRows)
                        {
                            while (dtr1.Read())
                            {
                                sti = dtr1[0].ToString();
                            }
                        }

                        if (dtr.HasRows)
                        {
                            while (dtr.Read())
                            {
                                sLine = dtr[1].ToString();
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
                                    cnn2.ConnectionString = connectionString;
                                    cnn2.Open();

                                    cmd2.Connection = cnn2;
                                    cmd2.CommandText = "INSERT INTO [PurchaseOrderEntry] ([ItemDescription],[LastUpdated]" +
                                    @",[QuantityReceivedToDate],[StoreID],[PurchaseOrderID],[QuantityOrdered],[QuantityReceived],[ItemID]" +
                                    @",[OrderNumber],[Price],[TaxRate],[InventoryOfflineID]) VALUES ('" + subcadena + "','" + now + "',0,'" + sti + "','" + poi + "'," + cant + "," +
                                    @"0," + dtr[2].ToString() + ",''," + dtr[3].ToString() + ",0,0)";

                                    cmd2.ExecuteNonQuery();

                                    SqlConnection cnnitem = new SqlConnection();
                                    SqlCommand cmditem = new SqlCommand();
                                    string lastitem = "";

                                    cnnitem.ConnectionString = connectionString;
                                    cnnitem.Open();

                                    cmditem.Connection = cnnitem;
                                    cmditem.CommandText = "SELECT [ID] FROM PurchaseOrderEntry order by ID asc";
                                    SqlDataReader dtritem = cmditem.ExecuteReader();

                                    if (dtritem.HasRows)
                                    {
                                        while (dtritem.Read())
                                        {
                                            lastitem = dtritem[0].ToString();
                                        }
                                    }

                                    CollectionVM.ItemList.Add(new Models.Item()
                                    {
                                        ItemLookupCode = dtr[0].ToString(),
                                        Description = dtr[1].ToString(),
                                        Qtyorder = cant,
                                        Cost = dtr.GetDecimal(3).ToString("0.00"),
                                        Extcost = (float.Parse(cant) * float.Parse(dtr.GetDecimal(3).ToString("0.00"))).ToString(),
                                        Tax = "0",
                                        IPOid = lastitem

                                    });
                                }
                                catch
                                {
                                    try
                                    {
                                        cnn3.ConnectionString = connectionString;
                                        cnn3.Open();
                                        cmd3.Connection = cnn3;
                                        cmd3.CommandText = "INSERT INTO [PurchaseOrderEntry] ([ItemDescription],[LastUpdated]" +
                                        @",[QuantityReceivedToDate],[StoreID],[PurchaseOrderID],[QuantityOrdered],[QuantityReceived],[ItemID]" +
                                        @",[OrderNumber],[Price],[TaxRate],[InventoryOfflineID],[ShippingPerItem],[OtherFeesPerItem]" +
                                        @",[LastQuantityReceived],[LastReceivedDate]) VALUES ('" + subcadena + "','" + now + "',0" +
                                        @",'" + sti + "','" + poi + "'," + cant + ",0,'" + dtr[2].ToString() + "',''," + dtr[3].ToString() + "," +
                                        @"0,0,0.00,0.00,0,NULL)";

                                        cmd3.ExecuteNonQuery();

                                        SqlConnection cnnitem = new SqlConnection();
                                        SqlCommand cmditem = new SqlCommand();
                                        string lastitem = "";

                                        cnnitem.ConnectionString = connectionString;
                                        cnnitem.Open();

                                        cmditem.Connection = cnnitem;
                                        cmditem.CommandText = "SELECT [ID] FROM PurchaseOrderEntry order by ID asc";
                                        SqlDataReader dtritem = cmditem.ExecuteReader();

                                        if (dtritem.HasRows)
                                        {
                                            while (dtritem.Read())
                                            {
                                                lastitem = dtritem[0].ToString();
                                            }
                                        }

                                        CollectionVM.ItemList.Add(new Models.Item()
                                        {
                                            ItemLookupCode = dtr[0].ToString(),
                                            Description = dtr[1].ToString(),
                                            Qtyorder = cant,
                                            Cost = dtr.GetDecimal(3).ToString("0.00"),
                                            Extcost = (float.Parse(cant) * float.Parse(dtr.GetDecimal(3).ToString("0.00"))).ToString(),
                                            Tax = "0",
                                            IPOid = lastitem
                                        });

                                        cnnitem.Close();
                                        dtritem.Close();
                                    }
                                    catch (SqlException ex)
                                    {
                                        ShowDialog("Error en insert" + ex.ToString());
                                    }

                                    cnn3.Close();
                                }

                                cnn2.Close();

                            }
                        }
                        else
                        {
                            String msg;
                            msg = "Scan Barcode does not exist";
                            ShowDialog(msg);
                        }

                        CollectionVM.ItemList.Count();

                        float iexost = 0;
                        float itax = 0;
                        float realtax = 0;

                        for (int k = 0; k < CollectionVM.ItemList.Count(); k++)
                        {

                            var itemobj = (Item)CollectionVM.ItemList[k];
                            iexost = iexost + float.Parse(itemobj.Cost) * float.Parse(itemobj.Qtyorder);
                            realtax = float.Parse(itemobj.Cost) * float.Parse(itemobj.Qtyorder) * float.Parse(itemobj.Tax) / 100;
                            itax = itax + realtax;
                        }

                        float total = iexost + itax;
                        
                        TextSubtotal.Text = Convert.ToDecimal(iexost).ToString("0.00");
                        TextSalestax.Text = Convert.ToDecimal(itax).ToString("0.00");
                        TextTotal.Text = Convert.ToDecimal(total).ToString("0.00");

                        dtr.Close();
                        cnn.Close();
                    }
                    catch (SqlException sqex)
                    {
                        String msg;
                        msg = "Verify Connection " + sqex.ToString();
                        ShowDialog(msg);
                    }

                }
                else
                {
                    String msg;
                    msg = "Fill Barcode Field";
                    ShowDialog(msg);
                }
                TextBAddItem.Text = "";
                TextBCantAddItem.Text = "1";
            }

        }

        private void BtnDeleteI_Click(object sender, RoutedEventArgs e)
        {
            string ft = PurchaseOrderFilterType.GetPurchaseType();
            if (ft == "New")
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

                string poi = PurchaseClass.GetPurchaseId();
                string ipoid = IPOid.GetIpoid();
                string iid = "";

                connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;

                cnn.ConnectionString = connectionString;
                cnn.Open();

                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [ID] FROM [Item]  WHERE [ItemLookupCode] = '" + ilstchose + "'";

                SqlDataReader dtr = cmd.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        iid = dtr[0].ToString();
                    }


                    cnn2.ConnectionString = connectionString;
                    cnn2.Open();

                    cmd2.Connection = cnn2;
                    cmd2.CommandText = "DELETE FROM [PurchaseOrderEntry]  WHERE [PurchaseOrderId] = '" + poi + "' and [ItemID] = '" + iid + "' and [ID]='" + ipoidchose + "'";
                    cmd2.ExecuteNonQuery();

                    while (ItemListView.SelectedItems.Count > 0)
                    {
                        CollectionVM.ItemList.Remove((Models.Item)ItemListView.SelectedItem);
                    }

                }
                else
                {
                    string msg = "ItemLookupCode doesn't exist";
                    ShowDialog(msg);
                }

                CollectionVM.ItemList.Count();

                float iexost = 0;
                float itax = 0;
                float realtax = 0;

                for (int k = 0; k < CollectionVM.ItemList.Count(); k++)
                {

                    var itemobj = (Item)CollectionVM.ItemList[k];
                    iexost = iexost + float.Parse(itemobj.Cost) * float.Parse(itemobj.Qtyorder);
                    realtax = float.Parse(itemobj.Cost) * float.Parse(itemobj.Qtyorder) * float.Parse(itemobj.Tax) / 100;
                    itax = itax + realtax;
                }

                float total = iexost + itax;
                TextSubtotal.Text = iexost.ToString("0.00");
                TextSalestax.Text = itax.ToString("0.00");
                TextTotal.Text = total.ToString("0.00");
            }

        }

        private void ItemListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = (Item)e.ClickedItem;

            string srv = Class1.GetServer();
            string srvu = Class1.GetUser();
            string srvp = Class1.GetPass();
            string srvdb = Class1.GetDb();

            string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;

            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();


            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();

                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [ID],[Itemlookupcode] FROM [item] WHERE [Itemlookupcode] = '" + item.ItemLookupCode + "'";

                SqlDataReader dtr = cmd.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        ilstchose = dtr[1].ToString();
                    }
                }

                dtr.Close();
            }
            catch { }
            cnn.Close();

            SqlConnection cnn1 = new SqlConnection();
            SqlCommand cmd1 = new SqlCommand();


            try
            {
                cnn1.ConnectionString = connectionString;
                cnn1.Open();

                cmd1.Connection = cnn1;
                cmd1.CommandText = "SELECT [ID] FROM [purchaseorderentry] WHERE [id] = '" + item.IPOid + "'";

                SqlDataReader dtr1 = cmd1.ExecuteReader();

                if (dtr1.HasRows)
                {
                    while (dtr1.Read())
                    {
                        ipoidchose = dtr1[0].ToString();
                    }
                }

                dtr1.Close();
            }
            catch { }
            cnn1.Close();
        }

        private void ListSup_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var sup = (string)e.ClickedItem;
            //TextTo.Text = sup[0].ToString();
        }

        private void BtnLookShip_Click(object sender, RoutedEventArgs e)
        {
            
            TextShipTo.IsEnabled = true;
        }

        private void AddIPopup_Loaded(object sender, RoutedEventArgs e)
        {
            TextBAddItem.InputScope = new InputScope()
            {
                Names = { new InputScopeName(InputScopeNameValue.Number) }
            };
                        
            TextBAddItem.Focus(FocusState.Programmatic);
        }

        private void TextQO_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CollectionIVM.ItemList.Count();

            //float icost = 0;
            //float iexost = 0;
            //float itax = 0;
            //float realtax = 0;

            //for (int k = 0; k < CollectionIVM.ItemList.Count(); k++)
            //{
            //    var itemobj = (Item)CollectionIVM.ItemList[k];
            //    icost = icost + float.Parse(itemobj.Cost);
            //    realtax = float.Parse(itemobj.Cost) * float.Parse(itemobj.Qtyorder) * float.Parse(itemobj.Tax);
            //    itax = itax + realtax;
            //}

            //float total = iexost + itax;
            //TextSubtotal.Text = iexost.ToString();
            //TextSalestax.Text = itax.ToString();
            //TextTotal.Text = total.ToString();

        }
    }
}
