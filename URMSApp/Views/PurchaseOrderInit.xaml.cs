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
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using Microsoft.Data.Sqlite;
using Windows.UI.Popups;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using URMSApp.ViewModels;
using URMSApp.Views;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace URMSApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PurchaseOrderInit : Page
    {
        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
        }

        private void BtnRecPO_Click(object sender, RoutedEventArgs e)
        {
            //var tmp = (DefaultViewModel)this.DataContext;
            //tmp.PO = new PurchaseOrderClass()
            //{
            //    PONumber = "PruebaCambio",
            //    POTo = "ProveedorCambio",
            //    PODate = "25/6/2018",
            //    POStatus = "Close"
            //};
        }

        public PurchaseOrderInit()
        {
            this.InitializeComponent();
            POFrame.Navigate(typeof(POListPage));
        }

        public async void ShowDialog(String msg)
        {

            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }

        private void BtnNewPO_Click(object sender, RoutedEventArgs e)
        {
            PurchasePivot.SelectedItem = pivotHeader;
            Title.Text = "New Purchase Order";
            FilterPurchasePopup.IsOpen = true;

        }

        private void BtnEditPO_Click(object sender, RoutedEventArgs e)
        {
            PurchasePivot.SelectedItem = pivotHeader;
            PurchasePivot.Title = "Edit Purchase Order";
            PurchasePopup.IsOpen = true;
        }

        private void BtnDelPO_Click(object sender, RoutedEventArgs e)
        {
            //.Items.RemoveAt(1);
        }

        private void PurchasePopup_Loaded(object sender, RoutedEventArgs e)
        {
            PurchasePivot.SelectedItem = pivotHeader;
            string pid = PurchaseClass.GetPurchaseId();

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
                cmd.CommandText = "SELECT [Status],[ID],[PONumber],[DateCreated]" +
                    @",[SupplierId],[LastUpdated] FROM [RMSPRUEBA].[dbo].[PurchaseOrder] WHERE [ID] = '"+ pid +"'";

                SqlDataReader dtr = cmd.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {

                    }

                }
                else
                {
                    //String msg = "";
                    //msg = "Purchase Order doesn't exist";
                    //ShowDialog(msg);
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
        }

        private void BtnOkFilter_Click_1(object sender, RoutedEventArgs e)
        {
            if (RbtnBlank.IsChecked == true) {

                //PurchasePivot.Title = "New Purchase Order";
                //PurchasePivot.SelectedItem = pivotContent;
                //pivotHeader.IsEnabled = false;
                //PurchasePopup.IsOpen = true;
                POFrame.Navigate(typeof(PurchaseOrderPage));
            }

            if (FilterPurchasePopup != null) { FilterPurchasePopup.IsOpen = false; }
        }

        private void SimulateSaveClicked(object sender, RoutedEventArgs e)
        {
            PurchasePivot.SelectedItem = pivotHeader;
            if (FilterPurchasePopup != null) { FilterPurchasePopup.IsOpen = false; }
            if (PurchasePopup != null) { PurchasePopup.IsOpen = false; }
        }

        private void BtnOkPO_Click(object sender, RoutedEventArgs e)
        {

        }

       

        private void FilterPurchasePopup_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void RbtnBlank_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void RbtnPODep_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void RbtnPOCat_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void RbtnPOSup_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnNone_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
