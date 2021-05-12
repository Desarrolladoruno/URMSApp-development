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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace URMSApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuantityDiscount:Page
    {
        String swi = "";

        String conswi = "";
        public QuantityDiscount()
        {
            this.InitializeComponent();
            pivotProperty.IsEnabled = false;
           
        }

        private void BtnNewQD_Click(object sender, RoutedEventArgs e)
        {
            swi = "1";

            pivotHeader.IsEnabled = false;
            pivotProperty.IsEnabled = true;
            QDPivot.SelectedItem = pivotProperty;

            LblDiscount.Text = "New Quantity Discount";
            txtDescription.Text = "";

            ChkMixMatchU.IsChecked = false;
            ChkMixMatchP.IsChecked = false;
            ChkBuyXYZU.IsChecked = false;
            ChkBuyXYZP.IsChecked = false;

            txtQuantity1.Text = "";
            txtQuantity2.Text = "";
            txtQuantity3.Text = "";
            txtQuantity4.Text = "";

            txtRegPrice1.Text = "";
            txtRegPrice2.Text = "";
            txtRegPrice3.Text = "";
            txtRegPrice4.Text = "";

            txtLevelA1.Text = "";
            txtLevelA2.Text = "";
            txtLevelA3.Text = "";
            txtLevelA4.Text = "";

            txtLevelB1.Text = "";
            txtLevelB2.Text = "";
            txtLevelB3.Text = "";
            txtLevelB4.Text = "";

            txtLevelC1.Text = "";
            txtLevelC2.Text = "";
            txtLevelC3.Text = "";
            txtLevelC4.Text = "";

            txtQBF.Text = "";
            txtQGD.Text = "";
            txtDP.Text = "";

            QuantityDiscountClass.AddData("","");
           
        }

        private void BtnProperties_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteQD_Click(object sender, RoutedEventArgs e)
        {
                       
              DeleteQDPopup.IsOpen = true;

        }

        private void QDListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var QDClass = (QuantityDiscounts)e.ClickedItem;
            
            QuantityDiscountClass.AddData(QDClass.Description,QDClass.ID);

            string qdt = "";
            qdt = QDClass.Type;

            if (qdt == "1")
            {
                ChkMixMatchU.IsChecked = true;
                ChkMixMatchP.IsChecked = false;
                ChkBuyXYZU.IsChecked = false;
                ChkBuyXYZP.IsChecked = false;

                txtDP.Text = "";
                txtQBF.Text = "";
                txtQGD.Text = "";
                txtDP.IsEnabled = false;
                txtQBF.IsEnabled = false;
                txtQGD.IsEnabled = false;

                txtQuantity1.IsEnabled = true;
                txtQuantity2.IsEnabled = true;
                txtQuantity3.IsEnabled = true;
                txtQuantity4.IsEnabled = true;

                txtRegPrice1.IsEnabled = true;
                txtRegPrice2.IsEnabled = true;
                txtRegPrice3.IsEnabled = true;
                txtRegPrice4.IsEnabled = true;

                txtLevelA1.IsEnabled = true;
                txtLevelA2.IsEnabled = true;
                txtLevelA3.IsEnabled = true;
                txtLevelA4.IsEnabled = true;

                txtLevelB1.IsEnabled = true;
                txtLevelB2.IsEnabled = true;
                txtLevelB3.IsEnabled = true;
                txtLevelB4.IsEnabled = true;

                txtLevelC1.IsEnabled = true;
                txtLevelC2.IsEnabled = true;
                txtLevelC3.IsEnabled = true;
                txtLevelC4.IsEnabled = true;
                
                txtQuantity1.Text = QDClass.Quantity1;
                txtRegPrice1.Text = Convert.ToDecimal(QDClass.Price1).ToString("0.00");
                txtLevelA1.Text =   Convert.ToDecimal(QDClass.Price1A).ToString("0.00");
                txtLevelB1.Text =   Convert.ToDecimal(QDClass.Price1B).ToString("0.00");
                txtLevelC1.Text =   Convert.ToDecimal(QDClass.Price1C).ToString("0.00");

                txtQuantity2.Text = QDClass.Quantity2;
                txtRegPrice2.Text = Convert.ToDecimal( QDClass.Price2).ToString("0.00");
                txtLevelA2.Text =   Convert.ToDecimal(QDClass.Price2A).ToString("0.00");
                txtLevelB2.Text =   Convert.ToDecimal(QDClass.Price2B).ToString("0.00");
                txtLevelC2.Text =   Convert.ToDecimal(QDClass.Price2C).ToString("0.00");
                                   
                txtQuantity3.Text = QDClass.Quantity3;
                txtRegPrice3.Text = Convert.ToDecimal( QDClass.Price3).ToString("0.00");
                txtLevelA3.Text =   Convert.ToDecimal(QDClass.Price3A).ToString("0.00");
                txtLevelB3.Text =   Convert.ToDecimal(QDClass.Price3B).ToString("0.00");
                txtLevelC3.Text =   Convert.ToDecimal(QDClass.Price3C).ToString("0.00");
                                   
                txtQuantity4.Text = QDClass.Quantity4;
                txtRegPrice4.Text = Convert.ToDecimal( QDClass.Price4).ToString("0.00");
                txtLevelA4.Text =   Convert.ToDecimal(QDClass.Price4A).ToString("0.00");
                txtLevelB4.Text =   Convert.ToDecimal(QDClass.Price4B).ToString("0.00");
                txtLevelC4.Text =   Convert.ToDecimal(QDClass.Price4C).ToString("0.00");

                TBRegPrice.Text = "R. Price";
                TBLevelA.Text = "Level A";
                TBLevelB.Text = "Level B";
                TBLevelC.Text = "Level C";
            }
            else if (qdt == "3")
            {
                ChkMixMatchU.IsChecked = false;
                ChkMixMatchP.IsChecked = true;
                ChkBuyXYZU.IsChecked = false;
                ChkBuyXYZP.IsChecked = false;

                txtDP.Text = "";
                txtQBF.Text = "";
                txtQGD.Text = "";
                txtDP.IsEnabled = false;
                txtQBF.IsEnabled = false;
                txtQGD.IsEnabled = false;

                txtQuantity1.IsEnabled = true;
                txtQuantity2.IsEnabled = true;
                txtQuantity3.IsEnabled = true;
                txtQuantity4.IsEnabled = true;

                txtRegPrice1.IsEnabled = true;
                txtRegPrice2.IsEnabled = true;
                txtRegPrice3.IsEnabled = true;
                txtRegPrice4.IsEnabled = true;

                txtLevelA1.IsEnabled = true;
                txtLevelA2.IsEnabled = true;
                txtLevelA3.IsEnabled = true;
                txtLevelA4.IsEnabled = true;

                txtLevelB1.IsEnabled = true;
                txtLevelB2.IsEnabled = true;
                txtLevelB3.IsEnabled = true;
                txtLevelB4.IsEnabled = true;

                txtLevelC1.IsEnabled = true;
                txtLevelC2.IsEnabled = true;
                txtLevelC3.IsEnabled = true;
                txtLevelC4.IsEnabled = true;

                txtQuantity1.Text = QDClass.Quantity1;
                txtRegPrice1.Text = QDClass.PercentOffPrice1;
                txtLevelA1.Text =   QDClass.PercentOffPrice1A;
                txtLevelB1.Text =   QDClass.PercentOffPrice1B;
                txtLevelC1.Text =   QDClass.PercentOffPrice1C;

                txtQuantity2.Text = QDClass.Quantity2;
                txtRegPrice2.Text = QDClass.PercentOffPrice2;
                txtLevelA2.Text =   QDClass.PercentOffPrice2A;
                txtLevelB2.Text =   QDClass.PercentOffPrice2B;
                txtLevelC2.Text =   QDClass.PercentOffPrice2C;

                txtQuantity3.Text = QDClass.Quantity3;
                txtRegPrice3.Text = QDClass.PercentOffPrice3;
                txtLevelA3.Text =   QDClass.PercentOffPrice3A;
                txtLevelB3.Text =   QDClass.PercentOffPrice3B;
                txtLevelC3.Text =   QDClass.PercentOffPrice3C;

                txtQuantity4.Text = QDClass.Quantity4;
                txtRegPrice4.Text = QDClass.PercentOffPrice4;
                txtLevelA4.Text =   QDClass.PercentOffPrice4A;
                txtLevelB4.Text =   QDClass.PercentOffPrice4B;
                txtLevelC4.Text =   QDClass.PercentOffPrice4C;

                TBRegPrice.Text = "R. Price%";
                TBLevelA.Text = "Level A%";
                TBLevelB.Text = "Level B%";
                TBLevelC.Text = "Level C%";
            }
            else if (qdt == "2")
            {
                ChkMixMatchU.IsChecked = false;
                ChkMixMatchP.IsChecked = false;
                ChkBuyXYZU.IsChecked = true;
                ChkBuyXYZP.IsChecked = false;

                txtDP.Text = QDClass.Price2;
                txtQBF.Text = QDClass.Quantity1;
                txtQGD.Text = QDClass.Quantity2;

                txtDP.IsEnabled = true;
                txtQBF.IsEnabled = true;
                txtQGD.IsEnabled = true;

                txtQuantity1.Text = "";
                txtQuantity2.Text = "";
                txtQuantity3.Text = "";
                txtQuantity4.Text = "";

                txtRegPrice1.Text = "";
                txtRegPrice2.Text = "";
                txtRegPrice3.Text = "";
                txtRegPrice4.Text = "";

                txtLevelA1.Text = "";
                txtLevelA2.Text = "";
                txtLevelA3.Text = "";
                txtLevelA4.Text = "";

                txtLevelB1.Text = "";
                txtLevelB2.Text = "";
                txtLevelB3.Text = "";
                txtLevelB4.Text = "";

                txtLevelC1.Text = "";
                txtLevelC2.Text = "";
                txtLevelC3.Text = "";
                txtLevelC4.Text = "";

                txtQuantity1.IsEnabled = false;
                txtQuantity2.IsEnabled = false;
                txtQuantity3.IsEnabled = false;
                txtQuantity4.IsEnabled = false;

                txtRegPrice1.IsEnabled = false;
                txtRegPrice2.IsEnabled = false;
                txtRegPrice3.IsEnabled = false;
                txtRegPrice4.IsEnabled = false;

                txtLevelA1.IsEnabled = false;
                txtLevelA2.IsEnabled = false;
                txtLevelA3.IsEnabled = false;
                txtLevelA4.IsEnabled = false;

                txtLevelB1.IsEnabled = false;
                txtLevelB2.IsEnabled = false;
                txtLevelB3.IsEnabled = false;
                txtLevelB4.IsEnabled = false;

                txtLevelC1.IsEnabled = false;
                txtLevelC2.IsEnabled = false;
                txtLevelC3.IsEnabled = false;
                txtLevelC4.IsEnabled = false;

                tbDP.Text = "Discount Price: ";
            }
            else if (qdt == "4")
            {
                txtDP.IsEnabled = true;
                txtQBF.IsEnabled = true;
                txtQGD.IsEnabled = true;

                ChkMixMatchU.IsChecked = false;
                ChkMixMatchP.IsChecked = false;
                ChkBuyXYZU.IsChecked = false;
                ChkBuyXYZP.IsChecked = true;

                txtDP.Text = QDClass.PercentOffPrice2;
                txtQBF.Text = QDClass.Quantity1;
                txtQGD.Text = QDClass.Quantity2;

                txtQuantity1.Text = "";
                txtQuantity2.Text = "";
                txtQuantity3.Text = "";
                txtQuantity4.Text = "";

                txtRegPrice1.Text = "";
                txtRegPrice2.Text = "";
                txtRegPrice3.Text = "";
                txtRegPrice4.Text = "";

                txtLevelA1.Text = "";
                txtLevelA2.Text = "";
                txtLevelA3.Text = "";
                txtLevelA4.Text = "";

                txtLevelB1.Text = "";
                txtLevelB2.Text = "";
                txtLevelB3.Text = "";
                txtLevelB4.Text = "";

                txtLevelC1.Text = "";
                txtLevelC2.Text = "";
                txtLevelC3.Text = "";
                txtLevelC4.Text = "";

                txtQuantity1.IsEnabled = false;
                txtQuantity2.IsEnabled = false;
                txtQuantity3.IsEnabled = false;
                txtQuantity4.IsEnabled = false;

                txtRegPrice1.IsEnabled = false;
                txtRegPrice2.IsEnabled = false;
                txtRegPrice3.IsEnabled = false;
                txtRegPrice4.IsEnabled = false;

                txtLevelA1.IsEnabled = false;
                txtLevelA2.IsEnabled = false;
                txtLevelA3.IsEnabled = false;
                txtLevelA4.IsEnabled = false;

                txtLevelB1.IsEnabled = false;
                txtLevelB2.IsEnabled = false;
                txtLevelB3.IsEnabled = false;
                txtLevelB4.IsEnabled = false;

                txtLevelC1.IsEnabled = false;
                txtLevelC2.IsEnabled = false;
                txtLevelC3.IsEnabled = false;
                txtLevelC4.IsEnabled = false;

                tbDP.Text = "Discount Percent: ";

            }

            LblDiscount.Text = QDClass.Description;

            txtDescription.Text = QDClass.Description;


        }

        private void BtnEditQD_Click(object sender, RoutedEventArgs e)
        {
            
            if (QuantityDiscountClass.GetQuantityId() == ""){
                ShowDialog("You have to choose discount to edit");
            } else
            {
                swi = "2";

                pivotHeader.IsEnabled = false;
                pivotProperty.IsEnabled = true;
                QDPivot.SelectedItem = pivotProperty;
            }

            
        }

        public async void ShowDialog(string msg)
        {
            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }

        private void BtnOkQD_Click(object sender, RoutedEventArgs e)
        {
            string qdt = "";
            string qbf, qgd, dp,q1,p1,p1a,p1b,p1c, q2, p2, p2a, p2b, p2c, q3, p3, p3a, p3b, p3c, q4, p4, p4a, p4b, p4c;

            string qdid = QuantityDiscountClass.GetQuantityId();

            String srv = Class1.GetServer();
            String srvu = Class1.GetUser();
            String srvp = Class1.GetPass();
            String srvdb = Class1.GetDb();

            SqlConnection cnnD = new SqlConnection();
            SqlCommand cmdD = new SqlCommand();

            if (txtQBF.Text == "") { qbf = "0"; } else { qbf = txtQBF.Text; }
            if (txtQGD.Text == "") { qgd = "0"; } else { qgd = txtQGD.Text; }
            if (txtDP.Text == "") { dp = "0"; } else { dp = txtDP.Text; }

            if (txtQuantity1.Text == "") { q1 = "0"; } else { q1 = txtQuantity1.Text; }
            if (txtRegPrice1.Text == "") { p1 = "0"; } else { p1 = txtRegPrice1.Text; }
            if (txtLevelA1.Text == "") { p1a = "0"; } else { p1a = txtLevelA1.Text; }
            if (txtLevelB1.Text == "") { p1b = "0"; } else { p1b = txtLevelB1.Text; }
            if (txtLevelC1.Text == "") { p1c = "0"; } else { p1c = txtLevelC1.Text; }

            if (txtQuantity2.Text == "") { q2 = "0"; } else { q2 = txtQuantity2.Text; }
            if (txtRegPrice2.Text == "") { p2 = "0"; } else { p2 = txtRegPrice2.Text; }
            if (txtLevelA2.Text == "") { p2a = "0"; } else { p2a = txtLevelA2.Text; }
            if (txtLevelB2.Text == "") { p2b = "0"; } else { p2b = txtLevelB2.Text; }
            if (txtLevelC2.Text == "") { p2c = "0"; } else { p2c = txtLevelC2.Text; }

            if (txtQuantity3.Text == "") { q3 = "0"; } else { q3 = txtQuantity3.Text; }
            if (txtRegPrice3.Text == "") { p3 = "0"; } else { p3 = txtRegPrice3.Text; }
            if (txtLevelA3.Text == "") { p3a = "0"; } else { p3a = txtLevelA3.Text; }
            if (txtLevelB3.Text == "") { p3b = "0"; } else { p3b = txtLevelB3.Text; }
            if (txtLevelC3.Text == "") { p3c = "0"; } else { p3c = txtLevelC3.Text; }

            if (txtQuantity4.Text == "") { q4 = "0"; } else { q4 = txtQuantity4.Text; }
            if (txtRegPrice4.Text == "") { p4 = "0"; } else { p4 = txtRegPrice4.Text; }
            if (txtLevelA4.Text == "") { p4a = "0"; } else { p4a = txtLevelA4.Text; }
            if (txtLevelB4.Text == "") { p4b = "0"; } else { p4b = txtLevelB4.Text; }
            if (txtLevelC4.Text == "") { p4c = "0"; } else { p4c = txtLevelC4.Text; }

            string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            if (ChkMixMatchU.IsChecked == true) {
                qdt = "1";
            }else if (ChkMixMatchP.IsChecked == true)
            {
                qdt = "3";
            }
            else if (ChkBuyXYZU.IsChecked == true)
            {
                qdt = "2";
            }
            else if (ChkBuyXYZP.IsChecked == true)
            {
                qdt = "4";
            }

            try
            {
                cnnD.ConnectionString = connectionString;
                cnnD.Open();

                conswi = "1";

                cmdD.Connection = cnnD;

                if (swi == "1")
                {
                    if (qdt == "1")
                    {

                        cmdD.CommandText = "INSERT INTO [dbo].[QuantityDiscount]([Description],[HQID],[DiscountOddItems]" +
                                       ",[Quantity1],[Price1],[Price1A],[Price1B],[Price1C],[Quantity2],[Price2],[Price2A],[Price2B],[Price2C],[Quantity3]" +
                                       ",[Price3],[Price3A],[Price3B],[Price3C],[Quantity4],[Price4],[Price4A],[Price4B],[Price4C],[DBTimeStamp],[Type],[PercentOffPrice1]" +
                                       ",[PercentOffPrice1A],[PercentOffPrice1B],[PercentOffPrice1C],[PercentOffPrice2],[PercentOffPrice2A],[PercentOffPrice2B]" +
                                       ",[PercentOffPrice2C],[PercentOffPrice3],[PercentOffPrice3A],[PercentOffPrice3B],[PercentOffPrice3C],[PercentOffPrice4]" +
                                       ",[PercentOffPrice4A],[PercentOffPrice4B],[PercentOffPrice4C]) VALUES ('" + txtDescription.Text + "',0,0," + q1 + "," + p1 + "," +
                                       p1a + "," + p1b + "," + p1c + "," + q2 + "," + p2 + "," + p2a + "," + p2b +
                                       "," + p2c + "," + q3 + "," + p3 + "," + p3a + "," + p3b + "," + p3c +
                                       "," + q4 + "," + p4 + "," + p4a + "," + p4b + "," + p4c + ",DEFAULT," + qdt +
                                       ",0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00)";

                        cmdD.ExecuteNonQuery();


                        String msg;
                        msg = "Changes have been applied. Item has been updated";
                        ShowDialog(msg);

                        SqlConnection cnn = new SqlConnection();
                        SqlCommand cmd = new SqlCommand();


                        try
                        {
                            cnn.ConnectionString = connectionString;
                            cnn.Open();

                            cmd.Connection = cnn;
                            cmd.CommandText = "SELECT [ID] FROM [QuantityDiscount] order by ID asc";

                            SqlDataReader dtr = cmd.ExecuteReader();


                            if (dtr.HasRows)
                            {
                                while (dtr.Read())
                                {

                                    qdid = dtr[0].ToString();

                                }

                            }
                            else
                            {
                                //String msg = "";
                                //msg = "There are no Purchase Orders";
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


                        CollectionQDVM.QuantityList.Add(new Models.QuantityDiscounts()
                        {
                            Description = txtDescription.Text,
                            HQID = "0",
                            ID = qdid,
                            DiscountOddItems = "0",
                            Type = qdt,
                            Quantity1 = q1,
                            Price1 = p1,
                            Price1A = p1a,
                            Price1B = p1b,
                            Price1C = p1c,
                            Quantity2 = q2,
                            Price2 = p2,
                            Price2A = p2a,
                            Price2B = p2b,
                            Price2C = p2c,
                            Quantity3 = q3,
                            Price3 = p3,
                            Price3A = p3a,
                            Price3B = p3b,
                            Price3C = p3c,
                            Quantity4 = q4,
                            Price4 = p4,
                            Price4A = p4a,
                            Price4B = p4b,
                            Price4C = p4c,
                            PercentOffPrice1 = "0.00",
                            PercentOffPrice1A = "0.00",
                            PercentOffPrice1B = "0.00",
                            PercentOffPrice1C = "0.00",
                            PercentOffPrice2 = "0.00",
                            PercentOffPrice2A = "0.00",
                            PercentOffPrice2B = "0.00",
                            PercentOffPrice2C = "0.00",
                            PercentOffPrice3 = "0.00",
                            PercentOffPrice3A = "0.00",
                            PercentOffPrice3B = "0.00",
                            PercentOffPrice3C = "0.00",
                            PercentOffPrice4 = "0.00",
                            PercentOffPrice4A = "0.00",
                            PercentOffPrice4B = "0.00",
                            PercentOffPrice4C = "0.00"
                        });

                        txtQuantity1.Text = "";
                        txtQuantity2.Text = "";
                        txtQuantity3.Text = "";
                        txtQuantity4.Text = "";

                        txtRegPrice1.Text = "";
                        txtRegPrice2.Text = "";
                        txtRegPrice3.Text = "";
                        txtRegPrice4.Text = "";

                        txtLevelA1.Text = "";
                        txtLevelA2.Text = "";
                        txtLevelA3.Text = "";
                        txtLevelA4.Text = "";

                        txtLevelB1.Text = "";
                        txtLevelB2.Text = "";
                        txtLevelB3.Text = "";
                        txtLevelB4.Text = "";

                        txtLevelC1.Text = "";
                        txtLevelC2.Text = "";
                        txtLevelC3.Text = "";
                        txtLevelC4.Text = "";

                        txtQuantity1.IsEnabled = false;
                        txtQuantity2.IsEnabled = false;
                        txtQuantity3.IsEnabled = false;
                        txtQuantity4.IsEnabled = false;

                        txtRegPrice1.IsEnabled = false;
                        txtRegPrice2.IsEnabled = false;
                        txtRegPrice3.IsEnabled = false;
                        txtRegPrice4.IsEnabled = false;

                        txtLevelA1.IsEnabled = false;
                        txtLevelA2.IsEnabled = false;
                        txtLevelA3.IsEnabled = false;
                        txtLevelA4.IsEnabled = false;

                        txtLevelB1.IsEnabled = false;
                        txtLevelB2.IsEnabled = false;
                        txtLevelB3.IsEnabled = false;
                        txtLevelB4.IsEnabled = false;

                        txtLevelC1.IsEnabled = false;
                        txtLevelC2.IsEnabled = false;
                        txtLevelC3.IsEnabled = false;
                        txtLevelC4.IsEnabled = false;

                        txtQBF.Text = "";
                        txtQGD.Text = "";
                        txtDP.Text = "";

                        txtQBF.IsEnabled = false;
                        txtQGD.IsEnabled = false;
                        txtDP.IsEnabled = false;

                        pivotHeader.IsEnabled = true;
                        pivotProperty.IsEnabled = false;
                        QDPivot.SelectedItem = pivotHeader;

                    }
                    else if (qdt == "3")
                    {
                        cmdD.CommandText = "INSERT INTO [dbo].[QuantityDiscount]([Description],[HQID],[DiscountOddItems]" +
                                       ",[Quantity1],[Price1],[Price1A],[Price1B],[Price1C],[Quantity2],[Price2],[Price2A],[Price2B],[Price2C],[Quantity3]" +
                                       ",[Price3],[Price3A],[Price3B],[Price3C],[Quantity4],[Price4],[Price4A],[Price4B],[Price4C],[DBTimeStamp],[Type],[PercentOffPrice1]" +
                                       ",[PercentOffPrice1A],[PercentOffPrice1B],[PercentOffPrice1C],[PercentOffPrice2],[PercentOffPrice2A],[PercentOffPrice2B]" +
                                       ",[PercentOffPrice2C],[PercentOffPrice3],[PercentOffPrice3A],[PercentOffPrice3B],[PercentOffPrice3C],[PercentOffPrice4]" +
                                       ",[PercentOffPrice4A],[PercentOffPrice4B],[PercentOffPrice4C]) VALUES ('" + txtDescription.Text + "',0,0," + q1 + ",0.00," +
                                       "0.00,0.00,0.00," + q2 + ",0.00,0.00,0.00,0.00," + q3 + ",0.00,0.00,0.00,0.00" +
                                       "," + q4 + ",0.00,0.00,0.00,0.00,DEFAULT," + qdt + "," + p1 + "," +
                                       p1a + "," + p1b + "," + p1c + "," + p2 + "," + p2a + "," + p2b +
                                       "," + p2c + "," + p3 + "," + p3a + "," + p3b + "," + p3c +
                                       "," + p4 + "," + p4a + "," + p4b + "," + p4c + ")";

                        cmdD.ExecuteNonQuery();

                        String msg;
                        msg = "Changes have been applied. Item has been updated";
                        ShowDialog(msg);

                        SqlConnection cnn = new SqlConnection();
                        SqlCommand cmd = new SqlCommand();


                        try
                        {
                            cnn.ConnectionString = connectionString;
                            cnn.Open();

                            cmd.Connection = cnn;
                            cmd.CommandText = "SELECT [ID] FROM [QuantityDiscount] order by ID asc";

                            SqlDataReader dtr = cmd.ExecuteReader();


                            if (dtr.HasRows)
                            {
                                while (dtr.Read())
                                {

                                    qdid = dtr[0].ToString();

                                }

                            }
                            else
                            {
                                //String msg = "";
                                //msg = "There are no Purchase Orders";
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


                        CollectionQDVM.QuantityList.Add(new Models.QuantityDiscounts()
                        {
                            Description = txtDescription.Text,
                            HQID = "0",
                            ID = qdid,
                            DiscountOddItems = "0",
                            Type = qdt,
                            Quantity1 = q1,
                            Price1 = "0.00",
                            Price1A = "0.00",
                            Price1B = "0.00",
                            Price1C = "0.00",
                            Quantity2 = q2,
                            Price2 = "0.00",
                            Price2A = "0.00",
                            Price2B = "0.00",
                            Price2C = "0.00",
                            Quantity3 = q3,
                            Price3 = "0.00",
                            Price3A = "0.00",
                            Price3B = "0.00",
                            Price3C = "0.00",
                            Quantity4 = q4,
                            Price4 = "0.00",
                            Price4A = "0.00",
                            Price4B = "0.00",
                            Price4C = "0.00",
                            PercentOffPrice1 = p1,
                            PercentOffPrice1A = p1a,
                            PercentOffPrice1B = p1b,
                            PercentOffPrice1C = p1c,
                            PercentOffPrice2 = p2,
                            PercentOffPrice2A = p2a,
                            PercentOffPrice2B = p2b,
                            PercentOffPrice2C = p2c,
                            PercentOffPrice3 = p3,
                            PercentOffPrice3A = p3a,
                            PercentOffPrice3B = p3b,
                            PercentOffPrice3C = p3c,
                            PercentOffPrice4 = p4,
                            PercentOffPrice4A = p4a,
                            PercentOffPrice4B = p4b,
                            PercentOffPrice4C = p4c

                        });

                        txtQuantity1.Text = "";
                        txtQuantity2.Text = "";
                        txtQuantity3.Text = "";
                        txtQuantity4.Text = "";

                        txtRegPrice1.Text = "";
                        txtRegPrice2.Text = "";
                        txtRegPrice3.Text = "";
                        txtRegPrice4.Text = "";

                        txtLevelA1.Text = "";
                        txtLevelA2.Text = "";
                        txtLevelA3.Text = "";
                        txtLevelA4.Text = "";

                        txtLevelB1.Text = "";
                        txtLevelB2.Text = "";
                        txtLevelB3.Text = "";
                        txtLevelB4.Text = "";

                        txtLevelC1.Text = "";
                        txtLevelC2.Text = "";
                        txtLevelC3.Text = "";
                        txtLevelC4.Text = "";

                        txtQuantity1.IsEnabled = false;
                        txtQuantity2.IsEnabled = false;
                        txtQuantity3.IsEnabled = false;
                        txtQuantity4.IsEnabled = false;

                        txtRegPrice1.IsEnabled = false;
                        txtRegPrice2.IsEnabled = false;
                        txtRegPrice3.IsEnabled = false;
                        txtRegPrice4.IsEnabled = false;

                        txtLevelA1.IsEnabled = false;
                        txtLevelA2.IsEnabled = false;
                        txtLevelA3.IsEnabled = false;
                        txtLevelA4.IsEnabled = false;

                        txtLevelB1.IsEnabled = false;
                        txtLevelB2.IsEnabled = false;
                        txtLevelB3.IsEnabled = false;
                        txtLevelB4.IsEnabled = false;

                        txtLevelC1.IsEnabled = false;
                        txtLevelC2.IsEnabled = false;
                        txtLevelC3.IsEnabled = false;
                        txtLevelC4.IsEnabled = false;

                        txtQBF.Text = "";
                        txtQGD.Text = "";
                        txtDP.Text = "";

                        txtQBF.IsEnabled = false;
                        txtQGD.IsEnabled = false;
                        txtDP.IsEnabled = false;

                        pivotHeader.IsEnabled = true;
                        pivotProperty.IsEnabled = false;
                        QDPivot.SelectedItem = pivotHeader;
                    }
                    else if (qdt == "2")
                    {
                        cmdD.CommandText = "INSERT INTO [dbo].[QuantityDiscount]([Description],[HQID],[DiscountOddItems]" +
                                       ",[Quantity1],[Price1],[Price1A],[Price1B],[Price1C],[Quantity2],[Price2],[Price2A],[Price2B],[Price2C],[Quantity3]" +
                                       ",[Price3],[Price3A],[Price3B],[Price3C],[Quantity4],[Price4],[Price4A],[Price4B],[Price4C],[DBTimeStamp],[Type],[PercentOffPrice1]" +
                                       ",[PercentOffPrice1A],[PercentOffPrice1B],[PercentOffPrice1C],[PercentOffPrice2],[PercentOffPrice2A],[PercentOffPrice2B]" +
                                       ",[PercentOffPrice2C],[PercentOffPrice3],[PercentOffPrice3A],[PercentOffPrice3B],[PercentOffPrice3C],[PercentOffPrice4]" +
                                       ",[PercentOffPrice4A],[PercentOffPrice4B],[PercentOffPrice4C]) VALUES ('" + txtDescription.Text + "',0,0," + qbf + ",0.00," +
                                       "0.00,0.00,0.00," + qgd + "," + dp + ",0.00,0.00,0.00,0,0.00,0.00,0.00,0.00" +
                                       ",0,0.00,0.00,0.00,0.00,DEFAULT," + qdt + ",0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00)";

                        cmdD.ExecuteNonQuery();

                        String msg;
                        msg = "Changes have been applied. Item has been updated";
                        ShowDialog(msg);

                        SqlConnection cnn = new SqlConnection();
                        SqlCommand cmd = new SqlCommand();


                        try
                        {
                            cnn.ConnectionString = connectionString;
                            cnn.Open();

                            cmd.Connection = cnn;
                            cmd.CommandText = "SELECT [ID] FROM [QuantityDiscount] order by ID asc";

                            SqlDataReader dtr = cmd.ExecuteReader();


                            if (dtr.HasRows)
                            {
                                while (dtr.Read())
                                {

                                    qdid = dtr[0].ToString();

                                }

                            }
                            else
                            {
                                //String msg = "";
                                //msg = "There are no Purchase Orders";
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



                        CollectionQDVM.QuantityList.Add(new Models.QuantityDiscounts()
                        {
                            Description = txtDescription.Text,
                            HQID = "0",
                            ID = qdid,
                            DiscountOddItems = "0",
                            Type = qdt,
                            Quantity1 = qbf,
                            Price1 = "0.00",
                            Price1A = "0.00",
                            Price1B = "0.00",
                            Price1C = "0.00",
                            Quantity2 = qgd,
                            Price2 = dp,
                            Price2A = "0.00",
                            Price2B = "0.00",
                            Price2C = "0.00",
                            Quantity3 = "0.00",
                            Price3 = "0.00",
                            Price3A = "0.00",
                            Price3B = "0.00",
                            Price3C = "0.00",
                            Quantity4 = "0.00",
                            Price4 = "0.00",
                            Price4A = "0.00",
                            Price4B = "0.00",
                            Price4C = "0.00",
                            PercentOffPrice1 = "0.00",
                            PercentOffPrice1A = "0.00",
                            PercentOffPrice1B = "0.00",
                            PercentOffPrice1C = "0.00",
                            PercentOffPrice2 = "0.00",
                            PercentOffPrice2A = "0.00",
                            PercentOffPrice2B = "0.00",
                            PercentOffPrice2C = "0.00",
                            PercentOffPrice3 = "0.00",
                            PercentOffPrice3A = "0.00",
                            PercentOffPrice3B = "0.00",
                            PercentOffPrice3C = "0.00",
                            PercentOffPrice4 = "0.00",
                            PercentOffPrice4A = "0.00",
                            PercentOffPrice4B = "0.00",
                            PercentOffPrice4C = "0.00"
                        });

                        txtQuantity1.Text = "";
                        txtQuantity2.Text = "";
                        txtQuantity3.Text = "";
                        txtQuantity4.Text = "";

                        txtRegPrice1.Text = "";
                        txtRegPrice2.Text = "";
                        txtRegPrice3.Text = "";
                        txtRegPrice4.Text = "";

                        txtLevelA1.Text = "";
                        txtLevelA2.Text = "";
                        txtLevelA3.Text = "";
                        txtLevelA4.Text = "";

                        txtLevelB1.Text = "";
                        txtLevelB2.Text = "";
                        txtLevelB3.Text = "";
                        txtLevelB4.Text = "";

                        txtLevelC1.Text = "";
                        txtLevelC2.Text = "";
                        txtLevelC3.Text = "";
                        txtLevelC4.Text = "";

                        txtQuantity1.IsEnabled = false;
                        txtQuantity2.IsEnabled = false;
                        txtQuantity3.IsEnabled = false;
                        txtQuantity4.IsEnabled = false;

                        txtRegPrice1.IsEnabled = false;
                        txtRegPrice2.IsEnabled = false;
                        txtRegPrice3.IsEnabled = false;
                        txtRegPrice4.IsEnabled = false;

                        txtLevelA1.IsEnabled = false;
                        txtLevelA2.IsEnabled = false;
                        txtLevelA3.IsEnabled = false;
                        txtLevelA4.IsEnabled = false;

                        txtLevelB1.IsEnabled = false;
                        txtLevelB2.IsEnabled = false;
                        txtLevelB3.IsEnabled = false;
                        txtLevelB4.IsEnabled = false;

                        txtLevelC1.IsEnabled = false;
                        txtLevelC2.IsEnabled = false;
                        txtLevelC3.IsEnabled = false;
                        txtLevelC4.IsEnabled = false;

                        txtQBF.Text = "";
                        txtQGD.Text = "";
                        txtDP.Text = "";

                        txtQBF.IsEnabled = false;
                        txtQGD.IsEnabled = false;
                        txtDP.IsEnabled = false;

                        pivotHeader.IsEnabled = true;
                        pivotProperty.IsEnabled = false;
                        QDPivot.SelectedItem = pivotHeader;
                    }
                    else if (qdt == "4")
                    {
                        cmdD.CommandText = "INSERT INTO [dbo].[QuantityDiscount]([Description],[HQID],[DiscountOddItems]" +
                                       ",[Quantity1],[Price1],[Price1A],[Price1B],[Price1C],[Quantity2],[Price2],[Price2A],[Price2B],[Price2C],[Quantity3]" +
                                       ",[Price3],[Price3A],[Price3B],[Price3C],[Quantity4],[Price4],[Price4A],[Price4B],[Price4C],[DBTimeStamp],[Type],[PercentOffPrice1]" +
                                       ",[PercentOffPrice1A],[PercentOffPrice1B],[PercentOffPrice1C],[PercentOffPrice2],[PercentOffPrice2A],[PercentOffPrice2B]" +
                                       ",[PercentOffPrice2C],[PercentOffPrice3],[PercentOffPrice3A],[PercentOffPrice3B],[PercentOffPrice3C],[PercentOffPrice4]" +
                                       ",[PercentOffPrice4A],[PercentOffPrice4B],[PercentOffPrice4C]) VALUES ('" + txtDescription.Text + "',0,0," + qbf + ",0.00," +
                                       "0.00,0.00,0.00," + qgd + ",0.00,0.00,0.00,0.00,0,0.00,0.00,0.00,0.00" +
                                       ",0,0.00,0.00,0.00,0.00,DEFAULT," + qdt + ",0.00,0.00,0.00,0.00," + dp + ",0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00)";

                        cmdD.ExecuteNonQuery();

                        String msg;
                        msg = "Changes have been applied. Item has been updated";
                        ShowDialog(msg);


                        SqlConnection cnn = new SqlConnection();
                        SqlCommand cmd = new SqlCommand();


                        try
                        {
                            cnn.ConnectionString = connectionString;
                            cnn.Open();

                            cmd.Connection = cnn;
                            cmd.CommandText = "SELECT [ID] FROM [QuantityDiscount] order by ID asc";

                            SqlDataReader dtr = cmd.ExecuteReader();


                            if (dtr.HasRows)
                            {
                                while (dtr.Read())
                                {

                                    qdid = dtr[0].ToString();

                                }

                            }
                            else
                            {
                                //String msg = "";
                                //msg = "There are no Purchase Orders";
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


                        CollectionQDVM.QuantityList.Add(new Models.QuantityDiscounts()
                        {
                            Description = txtDescription.Text,
                            HQID = "0",
                            ID = qdid,
                            DiscountOddItems = "0",
                            Type = qdt,
                            Quantity1 = qbf,
                            Price1 = "0.00",
                            Price1A = "0.00",
                            Price1B = "0.00",
                            Price1C = "0.00",
                            Quantity2 = qgd,
                            Price2 = "0.00",
                            Price2A = "0.00",
                            Price2B = "0.00",
                            Price2C = "0.00",
                            Quantity3 = "0.00",
                            Price3 = "0.00",
                            Price3A = "0.00",
                            Price3B = "0.00",
                            Price3C = "0.00",
                            Quantity4 = "0.00",
                            Price4 = "0.00",
                            Price4A = "0.00",
                            Price4B = "0.00",
                            Price4C = "0.00",
                            PercentOffPrice1 = "0.00",
                            PercentOffPrice1A = "0.00",
                            PercentOffPrice1B = "0.00",
                            PercentOffPrice1C = "0.00",
                            PercentOffPrice2 = dp,
                            PercentOffPrice2A = "0.00",
                            PercentOffPrice2B = "0.00",
                            PercentOffPrice2C = "0.00",
                            PercentOffPrice3 = "0.00",
                            PercentOffPrice3A = "0.00",
                            PercentOffPrice3B = "0.00",
                            PercentOffPrice3C = "0.00",
                            PercentOffPrice4 = "0.00",
                            PercentOffPrice4A = "0.00",
                            PercentOffPrice4B = "0.00",
                            PercentOffPrice4C = "0.00"
                        });

                        txtQuantity1.Text = "";
                        txtQuantity2.Text = "";
                        txtQuantity3.Text = "";
                        txtQuantity4.Text = "";

                        txtRegPrice1.Text = "";
                        txtRegPrice2.Text = "";
                        txtRegPrice3.Text = "";
                        txtRegPrice4.Text = "";

                        txtLevelA1.Text = "";
                        txtLevelA2.Text = "";
                        txtLevelA3.Text = "";
                        txtLevelA4.Text = "";

                        txtLevelB1.Text = "";
                        txtLevelB2.Text = "";
                        txtLevelB3.Text = "";
                        txtLevelB4.Text = "";

                        txtLevelC1.Text = "";
                        txtLevelC2.Text = "";
                        txtLevelC3.Text = "";
                        txtLevelC4.Text = "";

                        txtQuantity1.IsEnabled = false;
                        txtQuantity2.IsEnabled = false;
                        txtQuantity3.IsEnabled = false;
                        txtQuantity4.IsEnabled = false;

                        txtRegPrice1.IsEnabled = false;
                        txtRegPrice2.IsEnabled = false;
                        txtRegPrice3.IsEnabled = false;
                        txtRegPrice4.IsEnabled = false;

                        txtLevelA1.IsEnabled = false;
                        txtLevelA2.IsEnabled = false;
                        txtLevelA3.IsEnabled = false;
                        txtLevelA4.IsEnabled = false;

                        txtLevelB1.IsEnabled = false;
                        txtLevelB2.IsEnabled = false;
                        txtLevelB3.IsEnabled = false;
                        txtLevelB4.IsEnabled = false;

                        txtLevelC1.IsEnabled = false;
                        txtLevelC2.IsEnabled = false;
                        txtLevelC3.IsEnabled = false;
                        txtLevelC4.IsEnabled = false;

                        txtQBF.Text = "";
                        txtQGD.Text = "";
                        txtDP.Text = "";

                        txtQBF.IsEnabled = false;
                        txtQGD.IsEnabled = false;
                        txtDP.IsEnabled = false;

                        pivotHeader.IsEnabled = true;
                        pivotProperty.IsEnabled = false;
                        QDPivot.SelectedItem = pivotHeader;


                    }

                    
                    
                   
                }
                else if (swi == "2")
                {
                    if (qdt == "1")
                    {
                        cmdD.CommandText = "UPDATE [rmsreal].[dbo].[QuantityDiscount] SET [Description] = '" + txtDescription.Text +
                                        "',[HQID] = 0,[DiscountOddItems] = 0,[Quantity1] = " + q1 + ", Price1 = " + p1 + "," +
                                       "Price1A = " + p1a + ",Price1B=" + p1b + ",Price1C=" + p1c + ",Quantity2=" + q2 + ",Price2=" + p2 + ",Price2A=" + p2a + ",Price2B=" + p2b +
                                       ",Price2C=" + p2c + ",Quantity3=" + q3 + ",Price3=" + p3 + ",Price3A=" + p3a + ",Price3B=" + p3b + ",Price3C=" + p3c +
                                       ",Quantity4=" + q4 + ",Price4=" + p4 + ",Price4A=" + p4a + ",Price4B=" + p4b + ",Price4C=" + p4c + ",Type=" + qdt +
                                       ",PercentOffPrice1=0.00,PercentOffPrice1A=0.00,PercentOffPrice1B=0.00,PercentOffPrice1C=0.00,PercentOffPrice2=0.00" +
                                       ",PercentOffPrice2A=0.00,PercentOffPrice2B=0.00,PercentOffPrice2C=0.00,PercentOffPrice3=0.00,PercentOffPrice3A=0.00" +
                                       ",PercentOffPrice3B=0.00,PercentOffPrice3C=0.00,PercentOffPrice4=0.00,PercentOffPrice4A=0.00,PercentOffPrice4B=0.00,PercentOffPrice4C=0.00 WHERE id = " + qdid;


                        cmdD.ExecuteNonQuery();

                        String msg;
                        msg = "Changes have been applied. Item has been updated";
                        ShowDialog(msg);

                        while (QDListView.SelectedItems.Count > 0)
                        {
                            CollectionQDVM.QuantityList.Remove((QuantityDiscounts)QDListView.SelectedItem);
                        }


                        CollectionQDVM.QuantityList.Add(new Models.QuantityDiscounts()
                        {
                            Description = txtDescription.Text,
                            HQID = "0",
                            ID = qdid,
                            DiscountOddItems = "0",
                            Type = qdt,
                            Quantity1 = q1,
                            Price1 = p1,
                            Price1A = p1a,
                            Price1B = p1b,
                            Price1C = p1c,
                            Quantity2 = q2,
                            Price2 = p2,
                            Price2A = p2a,
                            Price2B = p2b,
                            Price2C = p2c,
                            Quantity3 = q3,
                            Price3 = p3,
                            Price3A = p3a,
                            Price3B = p3b,
                            Price3C = p3c,
                            Quantity4 = q4,
                            Price4 = p4,
                            Price4A = p4a,
                            Price4B = p4b,
                            Price4C = p4c,
                            PercentOffPrice1 = "0.00",
                            PercentOffPrice1A = "0.00",
                            PercentOffPrice1B = "0.00",
                            PercentOffPrice1C = "0.00",
                            PercentOffPrice2 = "0.00",
                            PercentOffPrice2A = "0.00",
                            PercentOffPrice2B = "0.00",
                            PercentOffPrice2C = "0.00",
                            PercentOffPrice3 = "0.00",
                            PercentOffPrice3A = "0.00",
                            PercentOffPrice3B = "0.00",
                            PercentOffPrice3C = "0.00",
                            PercentOffPrice4 = "0.00",
                            PercentOffPrice4A = "0.00",
                            PercentOffPrice4B = "0.00",
                            PercentOffPrice4C = "0.00"
                        });

                        txtQuantity1.Text = "";
                        txtQuantity2.Text = "";
                        txtQuantity3.Text = "";
                        txtQuantity4.Text = "";

                        txtRegPrice1.Text = "";
                        txtRegPrice2.Text = "";
                        txtRegPrice3.Text = "";
                        txtRegPrice4.Text = "";

                        txtLevelA1.Text = "";
                        txtLevelA2.Text = "";
                        txtLevelA3.Text = "";
                        txtLevelA4.Text = "";

                        txtLevelB1.Text = "";
                        txtLevelB2.Text = "";
                        txtLevelB3.Text = "";
                        txtLevelB4.Text = "";

                        txtLevelC1.Text = "";
                        txtLevelC2.Text = "";
                        txtLevelC3.Text = "";
                        txtLevelC4.Text = "";

                        txtQuantity1.IsEnabled = false;
                        txtQuantity2.IsEnabled = false;
                        txtQuantity3.IsEnabled = false;
                        txtQuantity4.IsEnabled = false;

                        txtRegPrice1.IsEnabled = false;
                        txtRegPrice2.IsEnabled = false;
                        txtRegPrice3.IsEnabled = false;
                        txtRegPrice4.IsEnabled = false;

                        txtLevelA1.IsEnabled = false;
                        txtLevelA2.IsEnabled = false;
                        txtLevelA3.IsEnabled = false;
                        txtLevelA4.IsEnabled = false;

                        txtLevelB1.IsEnabled = false;
                        txtLevelB2.IsEnabled = false;
                        txtLevelB3.IsEnabled = false;
                        txtLevelB4.IsEnabled = false;

                        txtLevelC1.IsEnabled = false;
                        txtLevelC2.IsEnabled = false;
                        txtLevelC3.IsEnabled = false;
                        txtLevelC4.IsEnabled = false;

                        txtQBF.Text = "";
                        txtQGD.Text = "";
                        txtDP.Text = "";

                        txtQBF.IsEnabled = false;
                        txtQGD.IsEnabled = false;
                        txtDP.IsEnabled = false;

                        pivotHeader.IsEnabled = true;
                        pivotProperty.IsEnabled = false;
                        QDPivot.SelectedItem = pivotHeader;
                    }
                    else if (qdt == "3")
                    {
                        cmdD.CommandText = "UPDATE [rmsreal].[dbo].[QuantityDiscount] SET [Description] = '" + txtDescription.Text +
                                        "',[HQID] = 0,[DiscountOddItems] = 0,[Quantity1] = " + q1 + ", Price1 = 0.00," +
                                       "Price1A = 0.00,Price1B=0.00,Price1C=0.00,Quantity2=" + q2 + ",Price2=0.00,Price2A=0.00,Price2B=0.00" +
                                       ",Price2C=0.00,Quantity3=" + q3 + ",Price3=0.00,Price3A=0.00,Price3B=0.00,Price3C=0.00" +
                                       ",Quantity4=" + q4 + ",Price4=0.00,Price4A=0.00,Price4B=0.00,Price4C=0.00,Type=" + qdt +
                                       ",PercentOffPrice1=" + p1 + ",PercentOffPrice1A=" + p1a + ",PercentOffPrice1B=" + p1b + ",PercentOffPrice1C=" + p1c + ",PercentOffPrice2=" + p2 +
                                       ",PercentOffPrice2A=" + p2a + ",PercentOffPrice2B=" + p2b + ",PercentOffPrice2C=" + p2c + ",PercentOffPrice3=" + p3 + ",PercentOffPrice3A=" + p3a +
                                       ",PercentOffPrice3B=" + p3b + ",PercentOffPrice3C=" + p3c + ",PercentOffPrice4=" + p4 + ",PercentOffPrice4A=" + p4a + ",PercentOffPrice4B=" + p4b + ",PercentOffPrice4C=" + p4c + " WHERE id = " + qdid;

                        cmdD.ExecuteNonQuery();

                        String msg;
                        msg = "Changes have been applied. Item has been updated";
                        ShowDialog(msg);

                        while (QDListView.SelectedItems.Count > 0)
                        {
                            CollectionQDVM.QuantityList.Remove((QuantityDiscounts)QDListView.SelectedItem);
                        }

                        CollectionQDVM.QuantityList.Add(new Models.QuantityDiscounts()
                        {
                            Description = txtDescription.Text,
                            HQID = "0",
                            ID = qdid,
                            DiscountOddItems = "0",
                            Type = qdt,
                            Quantity1 = q1,
                            Price1 = "0.00",
                            Price1A = "0.00",
                            Price1B = "0.00",
                            Price1C = "0.00",
                            Quantity2 = q2,
                            Price2 = "0.00",
                            Price2A = "0.00",
                            Price2B = "0.00",
                            Price2C = "0.00",
                            Quantity3 = q3,
                            Price3 = "0.00",
                            Price3A = "0.00",
                            Price3B = "0.00",
                            Price3C = "0.00",
                            Quantity4 = q4,
                            Price4 = "0.00",
                            Price4A = "0.00",
                            Price4B = "0.00",
                            Price4C = "0.00",
                            PercentOffPrice1 = p1,
                            PercentOffPrice1A = p1a,
                            PercentOffPrice1B = p1b,
                            PercentOffPrice1C = p1c,
                            PercentOffPrice2 = p2,
                            PercentOffPrice2A = p2a,
                            PercentOffPrice2B = p2b,
                            PercentOffPrice2C = p2c,
                            PercentOffPrice3 = p3,
                            PercentOffPrice3A = p3a,
                            PercentOffPrice3B = p3b,
                            PercentOffPrice3C = p3c,
                            PercentOffPrice4 = p4,
                            PercentOffPrice4A = p4a,
                            PercentOffPrice4B = p4b,
                            PercentOffPrice4C = p4c

                        });

                        txtQuantity1.Text = "";
                        txtQuantity2.Text = "";
                        txtQuantity3.Text = "";
                        txtQuantity4.Text = "";

                        txtRegPrice1.Text = "";
                        txtRegPrice2.Text = "";
                        txtRegPrice3.Text = "";
                        txtRegPrice4.Text = "";

                        txtLevelA1.Text = "";
                        txtLevelA2.Text = "";
                        txtLevelA3.Text = "";
                        txtLevelA4.Text = "";

                        txtLevelB1.Text = "";
                        txtLevelB2.Text = "";
                        txtLevelB3.Text = "";
                        txtLevelB4.Text = "";

                        txtLevelC1.Text = "";
                        txtLevelC2.Text = "";
                        txtLevelC3.Text = "";
                        txtLevelC4.Text = "";

                        txtQuantity1.IsEnabled = false;
                        txtQuantity2.IsEnabled = false;
                        txtQuantity3.IsEnabled = false;
                        txtQuantity4.IsEnabled = false;

                        txtRegPrice1.IsEnabled = false;
                        txtRegPrice2.IsEnabled = false;
                        txtRegPrice3.IsEnabled = false;
                        txtRegPrice4.IsEnabled = false;

                        txtLevelA1.IsEnabled = false;
                        txtLevelA2.IsEnabled = false;
                        txtLevelA3.IsEnabled = false;
                        txtLevelA4.IsEnabled = false;

                        txtLevelB1.IsEnabled = false;
                        txtLevelB2.IsEnabled = false;
                        txtLevelB3.IsEnabled = false;
                        txtLevelB4.IsEnabled = false;

                        txtLevelC1.IsEnabled = false;
                        txtLevelC2.IsEnabled = false;
                        txtLevelC3.IsEnabled = false;
                        txtLevelC4.IsEnabled = false;

                        txtQBF.Text = "";
                        txtQGD.Text = "";
                        txtDP.Text = "";

                        txtQBF.IsEnabled = false;
                        txtQGD.IsEnabled = false;
                        txtDP.IsEnabled = false;

                        pivotHeader.IsEnabled = true;
                        pivotProperty.IsEnabled = false;
                        QDPivot.SelectedItem = pivotHeader;
                    }
                    else if (qdt == "2")
                    {
                        cmdD.CommandText = "UPDATE [rmsreal].[dbo].[QuantityDiscount] SET [Description] = '" + txtDescription.Text +
                                        "',[HQID] = 0,[DiscountOddItems] = 0,[Quantity1] = " + qbf + ", Price1 =0.00," +
                                       "Price1A = 0.00,Price1B=0.00,Price1C=0.00,Quantity2=" + qgd + ",Price2=" + dp + ",Price2A=0.00,Price2B=0.00" +
                                       ",Price2C=0.00,Quantity3=0.00,Price3=0.00,Price3A=0.00,Price3B=0.00,Price3C=0.00" +
                                       ",Quantity4=0.00,Price4=0.00,Price4A=0.00,Price4B=0.00,Price4C=0.00,Type=" + qdt +
                                       ",PercentOffPrice1=0.00,PercentOffPrice1A=0.00,PercentOffPrice1B=0.00,PercentOffPrice1C=0.00,PercentOffPrice2=0.00" +
                                       ",PercentOffPrice2A=0.00,PercentOffPrice2B=0.00,PercentOffPrice2C=0.00,PercentOffPrice3=0.00,PercentOffPrice3A=0.00" +
                                       ",PercentOffPrice3B=0.00,PercentOffPrice3C=0.00,PercentOffPrice4=0.00,PercentOffPrice4A=0.00,PercentOffPrice4B=0.00,PercentOffPrice4C=0.00 WHERE id = " + qdid;

                        cmdD.ExecuteNonQuery();

                        String msg;
                        msg = "Changes have been applied. Item has been updated";
                        ShowDialog(msg);

                        while (QDListView.SelectedItems.Count > 0)
                        {
                            CollectionQDVM.QuantityList.Remove((QuantityDiscounts)QDListView.SelectedItem);
                        }

                        CollectionQDVM.QuantityList.Add(new Models.QuantityDiscounts()
                        {
                            Description = txtDescription.Text,
                            HQID = "0",
                            ID = qdid,
                            DiscountOddItems = "0",
                            Type = qdt,
                            Quantity1 = qbf,
                            Price1 = "0.00",
                            Price1A = "0.00",
                            Price1B = "0.00",
                            Price1C = "0.00",
                            Quantity2 = qgd,
                            Price2 = dp,
                            Price2A = "0.00",
                            Price2B = "0.00",
                            Price2C = "0.00",
                            Quantity3 = "0.00",
                            Price3 = "0.00",
                            Price3A = "0.00",
                            Price3B = "0.00",
                            Price3C = "0.00",
                            Quantity4 = "0.00",
                            Price4 = "0.00",
                            Price4A = "0.00",
                            Price4B = "0.00",
                            Price4C = "0.00",
                            PercentOffPrice1 = "0.00",
                            PercentOffPrice1A = "0.00",
                            PercentOffPrice1B = "0.00",
                            PercentOffPrice1C = "0.00",
                            PercentOffPrice2 = "0.00",
                            PercentOffPrice2A = "0.00",
                            PercentOffPrice2B = "0.00",
                            PercentOffPrice2C = "0.00",
                            PercentOffPrice3 = "0.00",
                            PercentOffPrice3A = "0.00",
                            PercentOffPrice3B = "0.00",
                            PercentOffPrice3C = "0.00",
                            PercentOffPrice4 = "0.00",
                            PercentOffPrice4A = "0.00",
                            PercentOffPrice4B = "0.00",
                            PercentOffPrice4C = "0.00"
                        });

                        txtQuantity1.Text = "";
                        txtQuantity2.Text = "";
                        txtQuantity3.Text = "";
                        txtQuantity4.Text = "";

                        txtRegPrice1.Text = "";
                        txtRegPrice2.Text = "";
                        txtRegPrice3.Text = "";
                        txtRegPrice4.Text = "";

                        txtLevelA1.Text = "";
                        txtLevelA2.Text = "";
                        txtLevelA3.Text = "";
                        txtLevelA4.Text = "";

                        txtLevelB1.Text = "";
                        txtLevelB2.Text = "";
                        txtLevelB3.Text = "";
                        txtLevelB4.Text = "";

                        txtLevelC1.Text = "";
                        txtLevelC2.Text = "";
                        txtLevelC3.Text = "";
                        txtLevelC4.Text = "";

                        txtQuantity1.IsEnabled = false;
                        txtQuantity2.IsEnabled = false;
                        txtQuantity3.IsEnabled = false;
                        txtQuantity4.IsEnabled = false;

                        txtRegPrice1.IsEnabled = false;
                        txtRegPrice2.IsEnabled = false;
                        txtRegPrice3.IsEnabled = false;
                        txtRegPrice4.IsEnabled = false;

                        txtLevelA1.IsEnabled = false;
                        txtLevelA2.IsEnabled = false;
                        txtLevelA3.IsEnabled = false;
                        txtLevelA4.IsEnabled = false;

                        txtLevelB1.IsEnabled = false;
                        txtLevelB2.IsEnabled = false;
                        txtLevelB3.IsEnabled = false;
                        txtLevelB4.IsEnabled = false;

                        txtLevelC1.IsEnabled = false;
                        txtLevelC2.IsEnabled = false;
                        txtLevelC3.IsEnabled = false;
                        txtLevelC4.IsEnabled = false;

                        txtQBF.Text = "";
                        txtQGD.Text = "";
                        txtDP.Text = "";

                        txtQBF.IsEnabled = false;
                        txtQGD.IsEnabled = false;
                        txtDP.IsEnabled = false;

                        pivotHeader.IsEnabled = true;
                        pivotProperty.IsEnabled = false;
                        QDPivot.SelectedItem = pivotHeader;
                    }
                    else if (qdt == "4")
                    {
                        cmdD.CommandText = "UPDATE [rmsreal].[dbo].[QuantityDiscount] SET [Description] = '" + txtDescription.Text +
                                        "',[HQID] = 0,[DiscountOddItems] = 0,[Quantity1] = " + qbf + ", Price1 =0.00," +
                                       "Price1A = 0.00,Price1B=0.00,Price1C=0.00,Quantity2=" + qgd + ",Price2=0.00,Price2A=0.00,Price2B=0.00" +
                                       ",Price2C=0.00,Quantity3=0.00,Price3=0.00,Price3A=0.00,Price3B=0.00,Price3C=0.00" +
                                       ",Quantity4=0.00,Price4=0.00,Price4A=0.00,Price4B=0.00,Price4C=0.00,Type=" + qdt +
                                       ",PercentOffPrice1=0.00,PercentOffPrice1A=0.00,PercentOffPrice1B=0.00,PercentOffPrice1C=0.00,PercentOffPrice2=" + dp +
                                       ",PercentOffPrice2A=0.00,PercentOffPrice2B=0.00,PercentOffPrice2C=0.00,PercentOffPrice3=0.00,PercentOffPrice3A=0.00" +
                                       ",PercentOffPrice3B=0.00,PercentOffPrice3C=0.00,PercentOffPrice4=0.00,PercentOffPrice4A=0.00,PercentOffPrice4B=0.00,PercentOffPrice4C=0.00 WHERE id = " + qdid;

                        cmdD.ExecuteNonQuery();

                        String msg;
                        msg = "Changes have been applied. Item has been updated";
                        ShowDialog(msg);

                        while (QDListView.SelectedItems.Count > 0)
                        {
                            CollectionQDVM.QuantityList.Remove((QuantityDiscounts)QDListView.SelectedItem);
                        }

                        CollectionQDVM.QuantityList.Add(new Models.QuantityDiscounts()
                        {
                            Description = txtDescription.Text,
                            HQID = "0",
                            ID = qdid,
                            DiscountOddItems = "0",
                            Type = qdt,
                            Quantity1 = qbf,
                            Price1 = "0.00",
                            Price1A = "0.00",
                            Price1B = "0.00",
                            Price1C = "0.00",
                            Quantity2 = qgd,
                            Price2 = "0.00",
                            Price2A = "0.00",
                            Price2B = "0.00",
                            Price2C = "0.00",
                            Quantity3 = "0.00",
                            Price3 = "0.00",
                            Price3A = "0.00",
                            Price3B = "0.00",
                            Price3C = "0.00",
                            Quantity4 = "0.00",
                            Price4 = "0.00",
                            Price4A = "0.00",
                            Price4B = "0.00",
                            Price4C = "0.00",
                            PercentOffPrice1 = "0.00",
                            PercentOffPrice1A = "0.00",
                            PercentOffPrice1B = "0.00",
                            PercentOffPrice1C = "0.00",
                            PercentOffPrice2 = dp,
                            PercentOffPrice2A = "0.00",
                            PercentOffPrice2B = "0.00",
                            PercentOffPrice2C = "0.00",
                            PercentOffPrice3 = "0.00",
                            PercentOffPrice3A = "0.00",
                            PercentOffPrice3B = "0.00",
                            PercentOffPrice3C = "0.00",
                            PercentOffPrice4 = "0.00",
                            PercentOffPrice4A = "0.00",
                            PercentOffPrice4B = "0.00",
                            PercentOffPrice4C = "0.00"
                        });

                        ChkMixMatchU.IsChecked = false;
                        ChkMixMatchP.IsChecked = false;
                        ChkBuyXYZU.IsChecked = false;
                        ChkBuyXYZP.IsChecked = false;

                        txtQuantity1.Text = "";
                        txtQuantity2.Text = "";
                        txtQuantity3.Text = "";
                        txtQuantity4.Text = "";

                        txtRegPrice1.Text = "";
                        txtRegPrice2.Text = "";
                        txtRegPrice3.Text = "";
                        txtRegPrice4.Text = "";

                        txtLevelA1.Text = "";
                        txtLevelA2.Text = "";
                        txtLevelA3.Text = "";
                        txtLevelA4.Text = "";

                        txtLevelB1.Text = "";
                        txtLevelB2.Text = "";
                        txtLevelB3.Text = "";
                        txtLevelB4.Text = "";

                        txtLevelC1.Text = "";
                        txtLevelC2.Text = "";
                        txtLevelC3.Text = "";
                        txtLevelC4.Text = "";

                        txtQuantity1.IsEnabled = false;
                        txtQuantity2.IsEnabled = false;
                        txtQuantity3.IsEnabled = false;
                        txtQuantity4.IsEnabled = false;

                        txtRegPrice1.IsEnabled = false;
                        txtRegPrice2.IsEnabled = false;
                        txtRegPrice3.IsEnabled = false;
                        txtRegPrice4.IsEnabled = false;

                        txtLevelA1.IsEnabled = false;
                        txtLevelA2.IsEnabled = false;
                        txtLevelA3.IsEnabled = false;
                        txtLevelA4.IsEnabled = false;

                        txtLevelB1.IsEnabled = false;
                        txtLevelB2.IsEnabled = false;
                        txtLevelB3.IsEnabled = false;
                        txtLevelB4.IsEnabled = false;

                        txtLevelC1.IsEnabled = false;
                        txtLevelC2.IsEnabled = false;
                        txtLevelC3.IsEnabled = false;
                        txtLevelC4.IsEnabled = false;

                        txtQBF.Text = "";
                        txtQGD.Text = "";
                        txtDP.Text = "";

                        txtQBF.IsEnabled = false;
                        txtQGD.IsEnabled = false;
                        txtDP.IsEnabled = false;

                        pivotHeader.IsEnabled = true;
                        pivotProperty.IsEnabled = false;
                        QDPivot.SelectedItem = pivotHeader;


                    }

                }

                cnnD.Close();

                QuantityDiscountClass.AddData("","");
            }
            catch
            {
                if (conswi == "")
                {
                    ShowDialog("Verify conection!");
                } else if (conswi == "1") { ShowDialog("Information must be numeric");

                    txtQuantity1.Text = "";
                    txtQuantity2.Text = "";
                    txtQuantity3.Text = "";
                    txtQuantity4.Text = "";

                    txtRegPrice1.Text = "";
                    txtRegPrice2.Text = "";
                    txtRegPrice3.Text = "";
                    txtRegPrice4.Text = "";

                    txtLevelA1.Text = "";
                    txtLevelA2.Text = "";
                    txtLevelA3.Text = "";
                    txtLevelA4.Text = "";

                    txtLevelB1.Text = "";
                    txtLevelB2.Text = "";
                    txtLevelB3.Text = "";
                    txtLevelB4.Text = "";

                    txtLevelC1.Text = "";
                    txtLevelC2.Text = "";
                    txtLevelC3.Text = "";
                    txtLevelC4.Text = "";

                    txtQBF.Text = "";
                    txtQGD.Text = "";
                    txtDP.Text = "";
                }
            }
        }

        private void BtnCancelQD_Click(object sender, RoutedEventArgs e)
        {
            LblDiscount.Text = "New Quantity Discount";

            txtDescription.Text = "";
            ChkMixMatchU.IsChecked = false;
            ChkMixMatchP.IsChecked = false;
            ChkBuyXYZU.IsChecked = false;
            ChkBuyXYZP.IsChecked = false;

            txtQuantity1.Text = "";
            txtQuantity2.Text = "";
            txtQuantity3.Text = "";
            txtQuantity4.Text = "";

            txtRegPrice1.Text = "";
            txtRegPrice2.Text = "";
            txtRegPrice3.Text = "";
            txtRegPrice4.Text = "";

            txtLevelA1.Text = "";
            txtLevelA2.Text = "";
            txtLevelA3.Text = "";
            txtLevelA4.Text = "";

            txtLevelB1.Text = "";
            txtLevelB2.Text = "";
            txtLevelB3.Text = "";
            txtLevelB4.Text = "";

            txtLevelC1.Text = "";
            txtLevelC2.Text = "";
            txtLevelC3.Text = "";
            txtLevelC4.Text = "";

            txtQBF.Text = "";
            txtQGD.Text = "";
            txtDP.Text = "";

            QDListView.SelectedItem = null;
            pivotHeader.IsEnabled = true;
            QDPivot.SelectedItem = pivotHeader;

            QuantityDiscountClass.AddData("","");
        }

        private void ChkMixMatchU_Click(object sender, RoutedEventArgs e)
        {
            if (ChkMixMatchU.IsChecked == true)
            {

                ChkMixMatchP.IsChecked = false;
                ChkBuyXYZU.IsChecked = false;
                ChkBuyXYZP.IsChecked = false;
                txtQuantity1.IsEnabled = true;
                txtQuantity2.IsEnabled = true;
                txtQuantity3.IsEnabled = true;
                txtQuantity4.IsEnabled = true;
                txtRegPrice1.IsEnabled = true;
                txtRegPrice2.IsEnabled = true;
                txtRegPrice3.IsEnabled = true;
                txtRegPrice4.IsEnabled = true; 
                txtLevelA1.IsEnabled = true;
                txtLevelA2.IsEnabled = true;
                txtLevelA3.IsEnabled = true;
                txtLevelA4.IsEnabled = true;
                txtLevelB1.IsEnabled = true;
                txtLevelB2.IsEnabled = true;
                txtLevelB3.IsEnabled = true;
                txtLevelB4.IsEnabled = true;
                txtLevelC1.IsEnabled = true;
                txtLevelC2.IsEnabled = true;
                txtLevelC3.IsEnabled = true;
                txtLevelC4.IsEnabled = true;
                txtDP.IsEnabled = false;
                txtQBF.IsEnabled = false;
                txtQGD.IsEnabled = false;

                txtQuantity1.Text = "";
                txtQuantity2.Text = "";
                txtQuantity3.Text = "";
                txtQuantity4.Text = "";
                txtRegPrice1.Text = "";
                txtRegPrice2.Text = "";
                txtRegPrice3.Text = "";
                txtRegPrice4.Text = "";
                txtLevelA1.Text = "";
                txtLevelA2.Text = "";
                txtLevelA3.Text = "";
                txtLevelA4.Text = "";
                txtLevelB1.Text = "";
                txtLevelB2.Text = "";
                txtLevelB3.Text = "";
                txtLevelB4.Text = "";
                txtLevelC1.Text = "";
                txtLevelC2.Text = "";
                txtLevelC3.Text = "";
                txtLevelC4.Text = "";
                txtDP.Text = "";
                txtQBF.Text = "";
                txtQGD.Text = "";


                TBRegPrice.Text = "R. Price";
                TBLevelA.Text = "Level A";
                TBLevelB.Text = "Level B";
                TBLevelC.Text = "Level C";
            }
            
        }

        private void ChkMixMatchP_Click(object sender, RoutedEventArgs e)
        {
            if (ChkMixMatchP.IsChecked == true)
            {

                ChkMixMatchU.IsChecked = false;
                ChkBuyXYZU.IsChecked = false;
                ChkBuyXYZP.IsChecked = false;
                txtQuantity1.IsEnabled = true;
                txtQuantity2.IsEnabled = true;
                txtQuantity3.IsEnabled = true;
                txtQuantity4.IsEnabled = true;
                txtRegPrice1.IsEnabled = true;
                txtRegPrice2.IsEnabled = true;
                txtRegPrice3.IsEnabled = true;
                txtRegPrice4.IsEnabled = true;
                txtLevelA1.IsEnabled = true;
                txtLevelA2.IsEnabled = true;
                txtLevelA3.IsEnabled = true;
                txtLevelA4.IsEnabled = true;
                txtLevelB1.IsEnabled = true;
                txtLevelB2.IsEnabled = true;
                txtLevelB3.IsEnabled = true;
                txtLevelB4.IsEnabled = true;
                txtLevelC1.IsEnabled = true;
                txtLevelC2.IsEnabled = true;
                txtLevelC3.IsEnabled = true;
                txtLevelC4.IsEnabled = true;
                txtDP.IsEnabled = false;
                txtQBF.IsEnabled = false;
                txtQGD.IsEnabled = false;

                txtQuantity1.Text = "";
                txtQuantity2.Text = "";
                txtQuantity3.Text = "";
                txtQuantity4.Text = "";
                txtRegPrice1.Text = "";
                txtRegPrice2.Text = "";
                txtRegPrice3.Text = "";
                txtRegPrice4.Text = "";
                txtLevelA1.Text = "";
                txtLevelA2.Text = "";
                txtLevelA3.Text = "";
                txtLevelA4.Text = "";
                txtLevelB1.Text = "";
                txtLevelB2.Text = "";
                txtLevelB3.Text = "";
                txtLevelB4.Text = "";
                txtLevelC1.Text = "";
                txtLevelC2.Text = "";
                txtLevelC3.Text = "";
                txtLevelC4.Text = "";
                txtDP.Text = "";
                txtQBF.Text = "";
                txtQGD.Text = "";

                TBRegPrice.Text = "R. Price%";
                TBLevelA.Text = "Level A%";
                TBLevelB.Text = "Level B%";
                TBLevelC.Text = "Level C%";
            }
        }

        private void ChkBuyXYZU_Click(object sender, RoutedEventArgs e)
        {
            if (ChkBuyXYZU.IsChecked == true)
            {

                ChkMixMatchP.IsChecked = false;
                ChkMixMatchU.IsChecked = false;
                ChkBuyXYZP.IsChecked = false;
                txtQuantity1.IsEnabled = false;
                txtQuantity2.IsEnabled = false;
                txtQuantity3.IsEnabled = false;
                txtQuantity4.IsEnabled = false;
                txtRegPrice1.IsEnabled = false;
                txtRegPrice2.IsEnabled = false;
                txtRegPrice3.IsEnabled = false;
                txtRegPrice4.IsEnabled = false;
                txtLevelA1.IsEnabled = false;
                txtLevelA2.IsEnabled = false;
                txtLevelA3.IsEnabled = false;
                txtLevelA4.IsEnabled = false;
                txtLevelB1.IsEnabled = false;
                txtLevelB2.IsEnabled = false;
                txtLevelB3.IsEnabled = false;
                txtLevelB4.IsEnabled = false;
                txtLevelC1.IsEnabled = false;
                txtLevelC2.IsEnabled = false;
                txtLevelC3.IsEnabled = false;
                txtLevelC4.IsEnabled = false;

                txtQuantity1.Text = "";
                txtQuantity2.Text = "";
                txtQuantity3.Text = "";
                txtQuantity4.Text = "";
                txtRegPrice1.Text = "";
                txtRegPrice2.Text = "";
                txtRegPrice3.Text = "";
                txtRegPrice4.Text = "";
                txtLevelA1.Text = "";
                txtLevelA2.Text = "";
                txtLevelA3.Text = "";
                txtLevelA4.Text = "";
                txtLevelB1.Text = "";
                txtLevelB2.Text = "";
                txtLevelB3.Text = "";
                txtLevelB4.Text = "";
                txtLevelC1.Text = "";
                txtLevelC2.Text = "";
                txtLevelC3.Text = "";
                txtLevelC4.Text = "";
                txtDP.Text = "";
                txtQBF.Text = "";
                txtQGD.Text = "";

                txtDP.IsEnabled = true;
                txtQBF.IsEnabled = true;
                txtQGD.IsEnabled = true;
                tbDP.Text = "Discount Price: ";
            }
        }

        private void ChkBuyXYZP_Click(object sender, RoutedEventArgs e)
        {
            if (ChkBuyXYZP.IsChecked == true)
            {

                ChkMixMatchP.IsChecked = false;
                ChkMixMatchU.IsChecked = false;
                ChkBuyXYZU.IsChecked = false;
                txtQuantity1.IsEnabled = false;
                txtQuantity2.IsEnabled = false;
                txtQuantity3.IsEnabled = false;
                txtQuantity4.IsEnabled = false;
                txtRegPrice1.IsEnabled = false;
                txtRegPrice2.IsEnabled = false;
                txtRegPrice3.IsEnabled = false;
                txtRegPrice4.IsEnabled = false;
                txtLevelA1.IsEnabled = false;
                txtLevelA2.IsEnabled = false;
                txtLevelA3.IsEnabled = false;
                txtLevelA4.IsEnabled = false;
                txtLevelB1.IsEnabled = false;
                txtLevelB2.IsEnabled = false;
                txtLevelB3.IsEnabled = false;
                txtLevelB4.IsEnabled = false;
                txtLevelC1.IsEnabled = false;
                txtLevelC2.IsEnabled = false;
                txtLevelC3.IsEnabled = false;
                txtLevelC4.IsEnabled = false;

                txtQuantity1.Text = "";
                txtQuantity2.Text = "";
                txtQuantity3.Text = "";
                txtQuantity4.Text = "";
                txtRegPrice1.Text = "";
                txtRegPrice2.Text = "";
                txtRegPrice3.Text = "";
                txtRegPrice4.Text = "";
                txtLevelA1.Text = "";
                txtLevelA2.Text = "";
                txtLevelA3.Text = "";
                txtLevelA4.Text = "";
                txtLevelB1.Text = "";
                txtLevelB2.Text = "";
                txtLevelB3.Text = "";
                txtLevelB4.Text = "";
                txtLevelC1.Text = "";
                txtLevelC2.Text = "";
                txtLevelC3.Text = "";
                txtLevelC4.Text = "";
                txtDP.Text = "";
                txtQBF.Text = "";
                txtQGD.Text = "";

                txtDP.IsEnabled = true;
                txtQBF.IsEnabled = true;
                txtQGD.IsEnabled = true;
                tbDP.Text = "Discount Percent: ";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteQDPopup != null) { DeleteQDPopup.IsOpen = false; }
        }

        private void BtnOkDelete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            

            string connectionString;
            string srv = Class1.GetServer();
            string srvu = Class1.GetUser();
            string srvp = Class1.GetPass();
            string srvdb = Class1.GetDb();
            string qdid = QuantityDiscountClass.GetQuantityId();

            connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;

            try 
            { 
                cnn.ConnectionString = connectionString;
                cnn.Open();

                                                                     
                cmd.Connection = cnn;
                cmd.CommandText = "DELETE FROM [QuantityDiscount]  WHERE [ID] = '" + qdid + "'";
                cmd.ExecuteNonQuery();

                QuantityDiscountClass.AddData("", "");

                string msg = "Discount has been deleted";
                ShowDialog(msg);

                while (QDListView.SelectedItems.Count > 0)
                {
                    CollectionQDVM.QuantityList.Remove((QuantityDiscounts)QDListView.SelectedItem);
                }

                if (DeleteQDPopup != null) { DeleteQDPopup.IsOpen = false; }
            }
            catch (SqlException exc)
            {
                ShowDialog(exc.ToString());
            }

            cnn.Close();

            
        }

        private void txtQuantity1_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            
            //if (char.IsNumber((Char)e.Key)||char.IsControl((Char)e.Key))
            //{
                
            //    //e.Handled = false;
                
            //} else
            //{
            //    //e.Handled = true;

            //    txtQuantity1.Text = String.Empty;
                
            //    ShowDialog("This field must be numeric");
                
            //}
        }

        private void txtRegPrice1_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtRegPrice1.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtLevelA1_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtLevelA1.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtLevelC1_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtLevelC1.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtQuantity2_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtQuantity2.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtRegPrice2_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtRegPrice2.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtLevelA2_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtLevelA2.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtLevelB2_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtLevelB2.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtLevelC2_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtLevelC2.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtQuantity3_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtQuantity3.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtRegPrice3_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtRegPrice3.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtLevelA3_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtLevelA3.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtLevelB3_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtLevelB3.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtLevelC3_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtLevelC3.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtQuantity4_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtQuantity4.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtRegPrice4_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtRegPrice4.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtLevelA4_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtLevelA4.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtLevelB4_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtLevelB4.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtLevelC4_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtLevelC4.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtLevelB1_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtLevelB1.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtQBF_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                //e.Handled = false;

            }
            else
            {
                //e.Handled = true;

                txtQBF.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtQGD_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key))
            {

                e.Handled = false;

            }
            else
            {
                e.Handled = true;

                txtQGD.Text = String.Empty;

                ShowDialog("This field must be numeric");

            }
        }

        private void txtDP_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            //if (char.IsNumber((Char)e.Key) || char.IsControl((Char)e.Key) || (Char)e.Key == '.')
            //{

            //    e.Handled = false;

            //}
            //else
            //{
            //    e.Handled = true;

            //    txtDP.Text = String.Empty;

            //    ShowDialog("This field must be numeric");

            //}
        }

        private void txtQuantity1_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {

            args.Cancel = args.NewText.Any(c => !char.IsNumber(c) && !char.IsControl(c) );

        }

        private void txtRegPrice1_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c) && !char.IsControl(c) && c != '.');
        }
    }
}

