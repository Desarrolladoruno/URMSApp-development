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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace URMSApp.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ManualInventory : Page
    {
        public ManualInventory()
        {
            this.InitializeComponent();
        }

        private void BtnNewInv_Click(object sender, RoutedEventArgs e)
        {
            BtnNewInv.IsChecked = true;
            BtnEditInv.IsChecked = false;
            BtnDelInv.IsChecked = false;

            BtnEditInv.IsEnabled = false;
            BtnDelInv.IsEnabled = false;

            InventoryClass.AddInventory("", "","","", "", "");


            BtnEditInv.IsEnabled = true;
            BtnDelInv.IsEnabled = true;

           
            ////////////////////////////////////////////////////
            InventoryType.AddInventoryType("New");
                        
            Frame.Navigate(typeof(InventoryList));
            
        }

        private void BtnEditInv_Click(object sender, RoutedEventArgs e)
        {
            string invid = InventoryClass.GetInventoryId();
            
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
            cmd.CommandText = "SELECT [ID],[Code],[StoreID],[OpenTime],[Description],[Status] FROM [PhysicalInventory]  WHERE [ID] = '" + invid + "'";

            SqlDataReader dtr = cmd.ExecuteReader();

            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    if (dtr[5].ToString() == "2")
                    {
                        ShowDialog("Disable to edit this Physical Inventory, it has been closed");
                    } else if (dtr[5].ToString() == "1")
                    {
                        ShowDialog("Disable to edit this Physical Inventory, it has been calculated");
                    } 
                    else if (dtr[5].ToString() == "0")
                    {
                        InventoryClass.AddInventory(dtr[0].ToString(), dtr[1].ToString(), dtr[2].ToString(), dtr[4].ToString(), dtr[3].ToString(), dtr[5].ToString());
                        InventoryType.AddInventoryType("Edit");

                        Frame.Navigate(typeof(InventoryList));
                    }
                    
                }
            }
            

        }

        public async void ShowDialog(string msg)
        {
            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            CollectionVM.InventoryList.Clear();
            
            string stat = ComboStatus.SelectedValue.ToString();
            string fes;

            if (DPInv.Date != null)
            { fes = DPInv.Date.Value.ToString("yyyy-MM-dd"); }
            else { fes = ""; }

            if (stat == "Opened")
            { stat = "0"; }
            else if (stat == "Closed")
            { stat = "2"; }
            else if (stat == "Calculated")
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


                if (fes == "")
                {
                    if (stat == "")
                    {
                        cmd.CommandText = "SELECT ID,[Status],StoreId,OpenTime,Description,Code FROM [PhysicalInventory] ORDER BY PhysicalInventory.[ID]";
                    }
                    else
                    {
                        cmd.CommandText = "SELECT ID,[Status],StoreId,OpenTime,Description,Code FROM [PhysicalInventory] where [status] = " + stat + " ORDER BY PhysicalInventory.[ID]";
                    }
                }
                else
                {
                    if (stat == "")
                    {
                        cmd.CommandText = "SELECT ID,[Status],StoreId,OpenTime,Description,Code FROM [PhysicalInventory] WHERE PhysicalInventory.[OpenTime] BETWEEN '" + fes + " 00:00:00.000' AND '" + fes + " 23:59:59.000' ORDER BY PhysicalInventory.[ID]";
                    }
                    else
                    {
                        cmd.CommandText = "SELECT ID,[Status],StoreId,OpenTime,Description,Code FROM [PhysicalInventory] where [status] = " + stat + " and PhysicalInventory.[OpenTime] BETWEEN '" + fes + " 00:00:00.000' AND '" + fes + " 23:59:59.000' ORDER BY PhysicalInventory.[ID]";
                    }
                }

                SqlDataReader dtr = cmd.ExecuteReader();

                DateTime fa;

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        string sta = "";
                        if (dtr[1].ToString() == "0") { sta = "Opened"; }
                        else
                        {
                            if (dtr[1].ToString() == "1") { sta = "Calculated"; }
                            else { if (dtr[1].ToString() == "2") { sta = "Closed"; } }
                        }

                        fa = DateTime.Parse(dtr[3].ToString());
                        int faM = fa.Month;
                        int fad = fa.Day;
                        int fay = fa.Year;
                        int fah = fa.Hour;
                        int fam = fa.Minute;
                        int fas = fa.Second;

                        string fech = fay + "-" + faM + "-" + fad + " " + fah + ":" + fam + ":" + fas + ".000";


                        CollectionVM.InventoryList.Add(new ManualInventoryClass()
                        {
                            INVId = dtr[0].ToString(),
                            INVCode = dtr[5].ToString(),
                            INVStoreId = dtr[2].ToString(),
                            INVDate = fech,
                            INVStatus = sta,
                            INVDescription = dtr[4].ToString()
                        });
                    }

                }
                else
                {
                    String msg = "";
                    msg = "There are no Physical Inventories.";
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

        private void BtnDelInv_Click(object sender, RoutedEventArgs e)
        {
            BtnNewInv.IsChecked = false;
            BtnEditInv.IsChecked = false;
            BtnDelInv.IsChecked = false;

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
            string invn = InventoryClass.GetInventoryId();

            connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;

            cnn.ConnectionString = connectionString;
            cnn.Open();

            try
            {
                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [Status],[ID] FROM [PhysicalInventory]  WHERE [ID] = '" + invn + "'";

                SqlDataReader dtr = cmd.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        if (dtr[0].ToString() == "0")
                        {
                            DeleteINVPopup.IsOpen = true;

                        }
                        else
                        {
                            string msg = "This Physical Inventory can't be deleted";
                            ShowDialog(msg);
                        }
                    }
                }


            }
            catch { }
        }

        private void ChkFInv_Click(object sender, RoutedEventArgs e)
        {
            if (ChkFInv.IsChecked == true)
            {
                DPInv.IsEnabled = true;
            }
            else
            {
                DPInv.Date = null;
                DPInv.IsEnabled = false;
            }
        }

        private void ComboStatus_DropDownOpened(object sender, object e)
        {
            BtnFilter.IsEnabled = true;
        }

        private void InvListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var INVclass = (ManualInventoryClass)e.ClickedItem;

            InventoryClass.AddInventory(INVclass.INVId,"", "", "", "", "");
                    
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
            string invid = InventoryClass.GetInventoryId();

            connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;

            cnn.ConnectionString = connectionString;
            cnn.Open();

            try
            {
                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [Status] FROM [PhysicalInventory]  WHERE [ID] = '" + invid + "'";

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
                                cmd2.CommandText = "DELETE FROM [PhysicalInventoryEntry]  WHERE [PhysicalInventoryID] = '" + invid + "'";
                                cmd2.ExecuteNonQuery();

                                cnn1.ConnectionString = connectionString;
                                cnn1.Open();

                                cmd1.Connection = cnn1;
                                cmd1.CommandText = "DELETE FROM [PhysicalInventory]  WHERE [ID] = '" + invid + "'";
                                cmd1.ExecuteNonQuery();

                                PurchaseClass.AddPurchase("", "");

                                string msg = "Physical Inventory has been deleted";
                                ShowDialog(msg);

                                while (InvListView.SelectedItems.Count > 0)
                                {
                                    CollectionVM.InventoryList.Remove((ManualInventoryClass)InvListView.SelectedItem);
                                }

                                if (DeleteINVPopup != null) { DeleteINVPopup.IsOpen = false; }
                            }
                            catch (SqlException exc)
                            {
                                ShowDialog(exc.ToString());
                            }
                        }
                        else
                        {
                            string msg = "This Physical Inventory can't be deleted";
                            ShowDialog(msg);
                        }
                    }
                }


            }
            catch { }

        }

        private void SimulateSaveClicked(object sender, RoutedEventArgs e)
        {
            BtnNewInv.IsChecked = false;
            BtnEditInv.IsChecked = false;
            BtnDelInv.IsChecked = false;

            BtnEditInv.IsEnabled = true;
            BtnDelInv.IsEnabled = true;

            if (DeleteINVPopup != null) { DeleteINVPopup.IsOpen = false; }
        }
    }
}
