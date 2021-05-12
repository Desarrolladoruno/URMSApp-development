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
using Windows.UI.Popups;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace URMSApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// Set the default navigation page
    /// </summary>
    public sealed partial class ItemGeneral : Page
    {
        public ItemGeneral()
        {
            this.InitializeComponent();
            
            ItemClass.AddData("", "", "", "", "", "", "", "", "", "", "");
            PricingClass.AddData("", "", "", "", "", "", "", "");
            DiscountClass.AddData("", "", "", "", "", "", "", "");
            MoreClass.AddData("", "", "");

            ItemMFrame.Navigate(typeof(General));
        }

        /// <summary>
        /// Show page depending on clicked optin in menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ItemGeneralOptions_Click(object sender, RoutedEventArgs e)
        {
            bool es = false;
            Btn_ItemGeneralOptions.IsChecked = !es;
            Btn_DiscountO.IsChecked = es;
            Btn_PricingO.IsChecked = es;
            Btn_MoreO.IsChecked = es;
            ItemMFrame.Navigate(typeof(General));
        }

        private void Btn_PricingO_Click(object sender, RoutedEventArgs e)
        {
            bool es = false;
            Btn_PricingO.IsChecked = !es;
            Btn_ItemGeneralOptions.IsChecked = es;
            Btn_DiscountO.IsChecked =es;
            Btn_MoreO.IsChecked = es;    
            ItemMFrame.Navigate(typeof(Pricing));
        }

        private void Btn_DiscountO_Click(object sender, RoutedEventArgs e)
        {
            bool es = false;
            Btn_DiscountO.IsChecked = !es;
            Btn_ItemGeneralOptions.IsChecked = es;
            Btn_PricingO.IsChecked = es;
            Btn_MoreO.IsChecked = es;
            ItemMFrame.Navigate(typeof(Discount));
        }

        private void Btn_MoreO_Click(object sender, RoutedEventArgs e)
        {
            bool es = false;
            Btn_MoreO.IsChecked = !es;
            Btn_ItemGeneralOptions.IsChecked = es;
            Btn_PricingO.IsChecked = es;
            Btn_DiscountO.IsChecked = es;
            ItemMFrame.Navigate(typeof(More));
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string q1 = "";
            string q2 = "";
            string q3 = "";
            string u1 = "";
            string u2 = "";
            string u3 = "";

            q1 = DiscountClass.GetQty1().ToString();
            q2 = DiscountClass.GetQty2().ToString();
            q3 = DiscountClass.GetQty3().ToString();
            u1 = DiscountClass.GetUnit1().ToString();
            u2 = DiscountClass.GetUnit2().ToString();
            u3 = DiscountClass.GetUnit3().ToString();

            string swn1 = "";
            string swn2 = "";
            string swn3 = "";

            if (q1 != "")
            {                
                if (u1 == "")
                {
                    swn1 = "1";
                }
            }
            if (q2 != "")
            {
                if (u2 == "")
                {
                    swn2 = "1";
                }
            }
            if (q3 != "")
            {
                if (u3 == "")
                {
                    swn3 = "1";
                }
            }

            if (swn1 == "1" || swn2 == "1" || swn3 == "1") {
                ItemMFrame.Navigate(typeof(Discount));
                ShowDialog("Unit Price can't be zero");
            }
            else
            {
                

                String swi = "";

                String srv = Class1.GetServer();
                String srvu = Class1.GetUser();
                String srvp = Class1.GetPass();
                String srvdb = Class1.GetDb();

                SqlConnection cnnT = new SqlConnection();
                SqlCommand cmdT = new SqlCommand();

                string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                string qdid = "";

                try
                {
                    cnnT.ConnectionString = connectionString;
                    cnnT.Open();

                    cmdT.Connection = cnnT;
                    cmdT.CommandText = "SELECT [Itemlookupcode], [QuantityDiscountID] FROM item Where [Itemlookupcode] = '" + ItemClass.GetItemBarcode().ToString() + "'";
                    SqlDataReader dtrT = cmdT.ExecuteReader();

                    

                    if (dtrT.HasRows)
                    {                        
                        swi = "0";
                        while (dtrT.Read())
                        {
                            qdid = dtrT[1].ToString();
                        }
                    }
                    else { swi = "1"; }

                    dtrT.Close();
                    cnnT.Close();
            
                }
                catch { }


                string sLine = "";
                int nc = 0;
                string tcn = "";
                string subcadena = "";
                string subcadena2 = "";

                if (swi == "0")
                {
                    
                    if (ItemClass.GetItemBarcode().ToString() != "")
                    {
                        if (ItemClass.GetDescription().ToString() != "")
                        { 
                    
                            SqlConnection cnn = new SqlConnection();
                            SqlConnection cnn1 = new SqlConnection();
                            SqlConnection cnn2 = new SqlConnection();
                            SqlConnection cnn3 = new SqlConnection();
                            SqlCommand cmd = new SqlCommand();
                            SqlCommand cmd1 = new SqlCommand();
                            SqlCommand cmd2 = new SqlCommand();
                            SqlCommand cmd3 = new SqlCommand();
                            SqlConnection cnnmi = new SqlConnection();
                            SqlCommand cmdmi = new SqlCommand();


                            String di = "0";
                            String ci = "0";
                            String ti = "0";
                            String mi = "0";

                            try
                            {
                                sLine = ItemClass.GetDepartment().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }

                            try
                            {
                                cnn1.ConnectionString = connectionString;
                                cnn1.Open();

                                cmd1.Connection = cnn1;
                                cmd1.CommandText = "SELECT [ID],[Name] FROM Department Where [Name] = '" + subcadena + "'";
                                SqlDataReader dtr1 = cmd1.ExecuteReader();


                                if (dtr1.HasRows)
                                {
                                    while (dtr1.Read())
                                    {
                                        di = dtr1[0].ToString();
                                    }
                                }
                                dtr1.Close();
                                cnn1.Close();
                            }
                            catch { }

                            try
                            {
                                sLine = ItemClass.GetCategory().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }

                            try
                            {
                                cnn2.ConnectionString = connectionString;
                                cnn2.Open();


                                cmd2.Connection = cnn2;
                                cmd2.CommandText = "SELECT [ID],[Name] FROM Category Where [Name] = '" + subcadena + "'";
                                SqlDataReader dtr2 = cmd2.ExecuteReader();


                                if (dtr2.HasRows)
                                {
                                    while (dtr2.Read())
                                    {
                                        ci = dtr2[0].ToString();
                                    }
                                }
                                dtr2.Close();
                                cnn2.Close();
                            }
                            catch { }

                            try
                            {
                                sLine = ItemClass.GetTax().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }

                            try
                            {
                                cnn3.ConnectionString = connectionString;
                                cnn3.Open();

                                cmd3.Connection = cnn3;
                                cmd3.CommandText = "SELECT [ID],[Description] FROM ItemTax Where [Description] = '" + subcadena + "'";
                                SqlDataReader dtr3 = cmd3.ExecuteReader();

                                if (dtr3.HasRows)
                                {
                                    while (dtr3.Read())
                                    {
                                        ti = dtr3[0].ToString();
                                    }
                                }
                                dtr3.Close();
                                cnn3.Close();
                            }
                            catch { }

                            try
                            {
                                sLine = MoreClass.GetMi().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }

                            try
                            {
                                cnnmi.ConnectionString = connectionString;
                                cnnmi.Open();

                                cmdmi.Connection = cnnmi;
                                cmdmi.CommandText = "SELECT [ID],[Title] FROM ItemMessage Where [Title] = '" + subcadena + "'";
                                SqlDataReader dtrmi = cmdmi.ExecuteReader();


                                if (dtrmi.HasRows)
                                {
                                    while (dtrmi.Read())
                                    {
                                        mi = dtrmi[0].ToString();
                                    }
                                }
                                dtrmi.Close();
                                cnnmi.Close();
                            }
                            catch { }


                            String desc = "";

                            try
                            {
                                sLine = ItemClass.GetDescription().ToString();
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

                                desc = subcadena;
                            }
                            catch
                            {
                                desc = "";
                                subcadena = "";
                            }

                            String price = "";

                            if (ItemClass.GetPrice().ToString() == "")
                            {
                                price = "0";
                            }
                            else
                            {
                                price = ItemClass.GetPrice().ToString();
                            }

                            String cost = "";

                            if (ItemClass.GetCost().ToString() == "")
                            {
                                cost = "0";
                            }
                            else
                            {
                                cost = ItemClass.GetCost().ToString();
                            }

                            try
                            {
                                sLine = PricingClass.GetSchedule().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }

                            String sch = "";
                            String st = "";
                            SqlConnection cnnsch = new SqlConnection();
                            SqlCommand cmdsch = new SqlCommand();

                            try
                            {
                                cnnsch.ConnectionString = connectionString;
                                cnnsch.Open();

                                cmdsch.Connection = cnnsch;
                                cmdsch.CommandText = "SELECT [ID],[Description] FROM Schedule Where [Description] = '" + subcadena + "'";
                                SqlDataReader dtrsch = cmdsch.ExecuteReader();


                                if (dtrsch.HasRows)
                                {
                                    while (dtrsch.Read())
                                    {
                                        sch = dtrsch[0].ToString();
                                    }
                                }
                                dtrsch.Close();
                                cnnsch.Close();
                            }
                            catch { }

                            st = PricingClass.GetSaleType().ToString();

                            String fas;
                            String fes;


                            fas = PricingClass.GetStar().ToString();

                            fes = PricingClass.GetEnd().ToString();

                            String fs = "0";
                            String ta = "0";
                            String pi = "0";

                            try
                            {
                                sLine = ItemClass.GetTag().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }


                            SqlConnection cnnta = new SqlConnection();
                            SqlCommand cmdta = new SqlCommand();

                            try
                            {
                                cnnta.ConnectionString = connectionString;
                                cnnta.Open();

                                cmdta.Connection = cnnta;
                                cmdta.CommandText = "SELECT [ID],[Description] FROM Item Where [Description] = '" + subcadena + "'";
                                SqlDataReader dtrta = cmdta.ExecuteReader();


                                if (dtrta.HasRows)
                                {
                                    while (dtrta.Read())
                                    {
                                        ta = dtrta[0].ToString();
                                    }
                                }
                                dtrta.Close();
                                cnnta.Close();
                            }
                            catch { }

                            try
                            {
                                sLine = MoreClass.GetPItem().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }

                            SqlConnection cnnpi = new SqlConnection();
                            SqlCommand cmdpi = new SqlCommand();

                            try
                            {
                                cnnpi.ConnectionString = connectionString;
                                cnnpi.Open();

                                cmdpi.Connection = cnnpi;
                                cmdpi.CommandText = "SELECT [ID],[Description] FROM Item Where [Description] = '" + subcadena + "'";
                                SqlDataReader dtrpi = cmdpi.ExecuteReader();

                                if (dtrpi.HasRows)
                                {
                                    while (dtrpi.Read())
                                    {
                                        pi = dtrpi[0].ToString();
                                    }
                                }
                                dtrpi.Close();
                                cnnpi.Close();
                            }
                            catch { }

                            fs = ItemClass.GetFoodstamp().ToString();
                            if (fs == "") { fs = "0"; }
                            String taq = ItemClass.GetTagq().ToString();
                            String chq = MoreClass.GetCQty().ToString();

                            String td = "0";

                            String qt1 = "";
                            String un1 = "";
                            String qt2 = "";
                            String un2 = "";
                            String qt3 = "";
                            String un3 = "";

                            try
                            {
                                sLine = DiscountClass.GetMMSchema().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }


                            try
                            {
                                sLine = DiscountClass.GetXYSchema().ToString();
                                nc = sLine.Length;
                                tcn = "";
                                subcadena2 = "";

                                for (int iline = 0; iline < nc; iline++)
                                {
                                    tcn = sLine.Substring(iline, 1);
                                    if (tcn == "'")
                                    {
                                        tcn = "''";
                                        subcadena2 = subcadena2 + tcn;
                                    }
                                    else
                                    {
                                        subcadena2 = subcadena2 + tcn;
                                    }
                                }
                                subcadena2 = subcadena2.TrimEnd();
                                subcadena2 = subcadena2.TrimStart();
                            }
                            catch
                            {
                                subcadena2 = "";
                            }


                            SqlConnection cnnqu = new SqlConnection();
                            SqlCommand cmdqu = new SqlCommand();
                            SqlConnection cnnmmd = new SqlConnection();
                            SqlCommand cmdmmd = new SqlCommand();
                            SqlConnection cnnbxy = new SqlConnection();
                            SqlCommand cmdbxy = new SqlCommand();

                            if (DiscountClass.GetMMSchema().ToString() != "")
                            {
                                try
                                {
                                    cnnmmd.ConnectionString = connectionString;
                                    cnnmmd.Open();

                                    cmdmmd.Connection = cnnmmd;
                                    cmdmmd.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount Where [Description] = '" + subcadena + "'";
                                    SqlDataReader dtrmmd = cmdmmd.ExecuteReader();


                                    if (dtrmmd.HasRows)
                                    {
                                        while (dtrmmd.Read())
                                        {
                                            td = dtrmmd[0].ToString();
                                        }
                                    }
                                    dtrmmd.Close();
                                    cnnmmd.Close();
                                }
                                catch { }
                            }
                            else
                            {
                                if (DiscountClass.GetXYSchema().ToString() != "")
                                {
                                    try
                                    {
                                        cnnbxy.ConnectionString = connectionString;
                                        cnnbxy.Open();

                                        cmdbxy.Connection = cnnbxy;
                                        cmdbxy.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount Where [Description] = '" + subcadena2 + "'";
                                        SqlDataReader dtrbxy = cmdbxy.ExecuteReader();


                                        if (dtrbxy.HasRows)
                                        {
                                            while (dtrbxy.Read())
                                            {
                                                td = dtrbxy[0].ToString();
                                            }
                                        }
                                        dtrbxy.Close();
                                        cnnbxy.Close();
                                    }
                                    catch { }
                                }
                                else
                                {
                                    if (DiscountClass.GetQty1().ToString() != "" || DiscountClass.GetUnit1().ToString() != "" || DiscountClass.GetQty2().ToString() != "" || DiscountClass.GetUnit2().ToString() != "" || DiscountClass.GetQty3().ToString() != "" || DiscountClass.GetUnit3().ToString() != "")
                                    {
                                        if (DiscountClass.GetQty1().ToString() == "")
                                        {
                                            qt1 = "0";
                                        }
                                        else { qt1 = DiscountClass.GetQty1().ToString(); }

                                        if (DiscountClass.GetQty2().ToString() == "")
                                        {
                                            qt2 = "0";
                                        }
                                        else { qt2 = DiscountClass.GetQty2().ToString(); }

                                        if (DiscountClass.GetQty3().ToString() == "")
                                        {
                                            qt3 = "0";
                                        }
                                        else { qt3 = DiscountClass.GetQty3().ToString(); }

                                        if (DiscountClass.GetUnit1().ToString() == "")
                                        {
                                            un1 = "0.00";
                                        }
                                        else { un1 = DiscountClass.GetUnit1().ToString(); }

                                        if (DiscountClass.GetUnit2().ToString() == "")
                                        {
                                            un2 = "0.00";
                                        }
                                        else { un2 = DiscountClass.GetUnit2().ToString(); }

                                        if (DiscountClass.GetUnit3().ToString() == "")
                                        {
                                            un3 = "0.00";
                                        }
                                        else { un3 = DiscountClass.GetUnit3().ToString(); }

                                       

                                        td = qdid;

                                        cnnqu.ConnectionString = connectionString;
                                        cnnqu.Open();
                                        cmdqu.Connection = cnnqu;
                                        cmdqu.CommandText = "UPDATE [dbo].[QuantityDiscount] SET [Description]='',[HQID]=0,[DiscountOddItems]=0,[Quantity1]=" + qt1 + ",Price1=" + un1 + "," +
                                       "Price1A=0.00,Price1B=0.00,Price1C=0.00,Quantity2=" + qt2 + " ,Price2=" + un2 + ",Price2A=0.00,Price2B=0.00,Price2C=0.00,Quantity3=" + qt3 + "," +
                                       "Price3="+un3+",Price3A=0.00,Price3B=0.00,Price3C=0.00,Quantity4=0,Price4=0.00,Price4A=0.00,Price4B=0.00,Price4C=0.00,[Type]=0" +
                                       ",PercentOffPrice1=0,PercentOffPrice1A=0,PercentOffPrice1B=0,PercentOffPrice1C=0,PercentOffPrice2=0,PercentOffPrice2A=0,PercentOffPrice2B=0" +
                                       ",PercentOffPrice2C=0,PercentOffPrice3=0,PercentOffPrice3A=0,PercentOffPrice3B=0,PercentOffPrice3C=0,PercentOffPrice4=0,PercentOffPrice4A=0" +
                                       ",PercentOffPrice4B=0,PercentOffPrice4C=0 where id = " + qdid;

                                        cmdqu.ExecuteNonQuery();
                                        cnnqu.Close();


                                        

                                    }


                                }

                            }


                            if (taq == "") { taq = "0"; }
                            if (chq == "") { chq = "0"; }

                   
                            String taxable = "0";
                            if (ti != "0") { taxable = "1"; }

                            if (st == "") { st = "0"; }

                            string pricea = "0.00";
                            string priceb = "0.00";
                            string pricec = "0.00";
                            string saleprice = "0.00";

                            if (PricingClass.GetPriceA().ToString() != "") { pricea = PricingClass.GetPriceA().ToString(); }
                            if (PricingClass.GetPriceB().ToString() != "") { priceb = PricingClass.GetPriceB().ToString(); }
                            if (PricingClass.GetPriceC().ToString() != "") { pricec = PricingClass.GetPriceC().ToString(); }
                            if (PricingClass.GetSalePrice().ToString() != "") { saleprice = PricingClass.GetSalePrice().ToString(); }

                            cnn.ConnectionString = connectionString;
                            cnn.Open();
                            cmd.Connection = cnn;
                            cmd.CommandText = "UPDATE Item SET [Description] = '" + desc + "',[DepartmentID] = '" + di + "'," +
                            "[CategoryID] = '" + ci + "',[TaxID] = '" + ti + "',[Cost] = " + cost + ",[Price] = " + price + ", [PriceA] = " + pricea + ",[PriceB] = " + priceb +
                            ",[PriceC] = " + pricec + " ,[SalePrice] = " + saleprice + " ,[SaleStartDate] = '" + fas + "'" + " ,[MessageID] = '" + mi + "'" +
                            ",[SaleEndDate] = '" + fes + "',[SaleScheduleID] = '" + sch + "',[SaleType] = '" + st + "', [Foodstampable] = " + fs + ",[TagAlongItem] = '" + ta + "'," +
                            " [TagAlongQuantity] = " + taq + ",[taxable] = '" + taxable + "',[ParentItem] = '" + pi + "',[ParentQuantity] = " + chq + ", [QuantityDiscountID] = '" + td + "' WHERE [ItemLookupCode] = '" + ItemClass.GetItemBarcode().ToString() + "'";
                            cmd.ExecuteNonQuery();

                            String msg;
                            msg = "Changes have been applied. Item has been updated";
                            ShowDialog(msg);


                            cnn.Close();

                            ItemClass.AddData("", "", "", "", "", "", "", "", "", "", "");
                            PricingClass.AddData("", "", "", "", "", "", "", "");
                            DiscountClass.AddData("", "", "", "", "", "", "", "");
                            MoreClass.AddData("", "", "");

                            bool es = false;
                            Btn_ItemGeneralOptions.IsChecked = !es;
                            Btn_DiscountO.IsChecked = es;
                            Btn_PricingO.IsChecked = es;
                            Btn_MoreO.IsChecked = es;
                            ItemMFrame.Navigate(typeof(General));

                        }
                        else
                        {
                            String mes1 = "";
                            mes1 = "Fill Description Field";
                            ShowDialog(mes1);
                        }

                    }
                    else
                    {
                        String mes = "";
                        mes = "Fill Barcode Field";
                        ShowDialog(mes);
                    }


                }
                else if (swi == "1")
                {
                    if (ItemClass.GetItemBarcode().ToString() != "")
                    {
                        if (ItemClass.GetDescription().ToString() != "")
                        { 

                            SqlConnection cnn = new SqlConnection();
                            SqlConnection cnn1 = new SqlConnection();
                            SqlConnection cnn2 = new SqlConnection();
                            SqlConnection cnn3 = new SqlConnection();
                            SqlCommand cmd = new SqlCommand();
                            SqlCommand cmd1 = new SqlCommand();
                            SqlCommand cmd2 = new SqlCommand();
                            SqlCommand cmd3 = new SqlCommand();



                            String di = "0";
                            String ci = "0";
                            String ti = "0";

                            try
                            {
                                sLine = ItemClass.GetDepartment().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }

                            try
                            {
                                cnn1.ConnectionString = connectionString;
                                cnn1.Open();

                                cmd1.Connection = cnn1;
                                cmd1.CommandText = "SELECT [ID],[Name] FROM Department Where [Name] = '" + subcadena + "'";
                                SqlDataReader dtr1 = cmd1.ExecuteReader();


                                if (dtr1.HasRows)
                                {
                                    while (dtr1.Read())
                                    {
                                        di = dtr1[0].ToString();
                                    }
                                }
                                dtr1.Close();
                                cnn1.Close();
                            }
                            catch { }

                            try
                            {
                                sLine = ItemClass.GetCategory().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }

                            try
                            {
                                cnn2.ConnectionString = connectionString;
                                cnn2.Open();


                                cmd2.Connection = cnn2;
                                cmd2.CommandText = "SELECT [ID],[Name] FROM Category Where [Name] = '" + subcadena + "'";
                                SqlDataReader dtr2 = cmd2.ExecuteReader();


                                if (dtr2.HasRows)
                                {
                                    while (dtr2.Read())
                                    {
                                        ci = dtr2[0].ToString();
                                    }
                                }
                                dtr2.Close();
                                cnn2.Close();
                            }
                            catch { }

                            try
                            {
                                sLine = ItemClass.GetTax().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }

                            try
                            {
                                cnn3.ConnectionString = connectionString;
                                cnn3.Open();

                                cmd3.Connection = cnn3;
                                cmd3.CommandText = "SELECT [ID],[Description] FROM ItemTax Where [Description] = '" + subcadena + "'";
                                SqlDataReader dtr3 = cmd3.ExecuteReader();

                                if (dtr3.HasRows)
                                {
                                    while (dtr3.Read())
                                    {
                                        ti = dtr3[0].ToString();
                                    }
                                }
                                dtr3.Close();
                                cnn3.Close();
                            }
                            catch { }


                            String desc = "";

                            try
                            {
                                sLine = ItemClass.GetDescription().ToString();
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

                                desc = subcadena;
                            }
                            catch
                            {
                                desc = "";
                                subcadena = "";
                            }

                            String price = "";

                            if (ItemClass.GetPrice().ToString() == "")
                            {
                                price = "0";
                            }
                            else
                            {
                                price = ItemClass.GetPrice().ToString();
                            }

                            String cost = "";

                            if (ItemClass.GetCost().ToString() == "")
                            {
                                cost = "0";
                            }
                            else
                            {
                                cost = ItemClass.GetCost().ToString();
                            }

                            try
                            {
                                sLine = PricingClass.GetSchedule().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }

                            String sch = "";
                            String st = "";
                            SqlConnection cnnsch = new SqlConnection();
                            SqlCommand cmdsch = new SqlCommand();

                            try
                            {
                                cnnsch.ConnectionString = connectionString;
                                cnnsch.Open();

                                cmdsch.Connection = cnnsch;
                                cmdsch.CommandText = "SELECT [ID],[Description] FROM Schedule Where [Description] = '" + subcadena + "'";
                                SqlDataReader dtrsch = cmdsch.ExecuteReader();


                                if (dtrsch.HasRows)
                                {
                                    while (dtrsch.Read())
                                    {
                                        sch = dtrsch[0].ToString();
                                    }
                                }
                                dtrsch.Close();
                                cnnsch.Close();
                            }
                            catch { }

                            st = PricingClass.GetSaleType().ToString();

                            String fas;
                            String fes;


                            fas = PricingClass.GetStar().ToString();

                            fes = PricingClass.GetEnd().ToString();

                            String fs = "0";
                            String ta = "0";
                            String pi = "0";

                            try
                            {
                                sLine = ItemClass.GetTag().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }


                            SqlConnection cnnta = new SqlConnection();
                            SqlCommand cmdta = new SqlCommand();

                            try
                            {
                                cnnta.ConnectionString = connectionString;
                                cnnta.Open();

                                cmdta.Connection = cnnta;
                                cmdta.CommandText = "SELECT [ID],[Description] FROM Item Where [Description] = '" + subcadena + "'";
                                SqlDataReader dtrta = cmdta.ExecuteReader();


                                if (dtrta.HasRows)
                                {
                                    while (dtrta.Read())
                                    {
                                        ta = dtrta[0].ToString();
                                    }
                                }
                                dtrta.Close();
                                cnnta.Close();
                            }
                            catch { }

                            try
                            {
                                sLine = MoreClass.GetPItem().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }

                            SqlConnection cnnpi = new SqlConnection();
                            SqlCommand cmdpi = new SqlCommand();

                            try
                            {
                                cnnpi.ConnectionString = connectionString;
                                cnnpi.Open();

                                cmdpi.Connection = cnnpi;
                                cmdpi.CommandText = "SELECT [ID],[Description] FROM Item Where [Description] = '" + subcadena + "'";
                                SqlDataReader dtrpi = cmdpi.ExecuteReader();

                                if (dtrpi.HasRows)
                                {
                                    while (dtrpi.Read())
                                    {
                                        pi = dtrpi[0].ToString();
                                    }
                                }
                                dtrpi.Close();
                                cnnpi.Close();
                            }
                            catch { }

                            fs = ItemClass.GetFoodstamp().ToString();
                            if (fs == "") { fs = "0"; }
                            String taq = ItemClass.GetTagq().ToString();
                            String chq = MoreClass.GetCQty().ToString();

                            String td = "0";

                            String qt1 = "";
                            String un1 = "";
                            String qt2 = "";
                            String un2 = "";
                            String qt3 = "";
                            String un3 = "";

                            try
                            {
                                sLine = DiscountClass.GetMMSchema().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }


                            try
                            {
                                sLine = DiscountClass.GetXYSchema().ToString();
                                nc = sLine.Length;
                                tcn = "";
                                subcadena2 = "";

                                for (int iline = 0; iline < nc; iline++)
                                {
                                    tcn = sLine.Substring(iline, 1);
                                    if (tcn == "'")
                                    {
                                        tcn = "''";
                                        subcadena2 = subcadena2 + tcn;
                                    }
                                    else
                                    {
                                        subcadena2 = subcadena2 + tcn;
                                    }
                                }
                                subcadena2 = subcadena2.TrimEnd();
                                subcadena2 = subcadena2.TrimStart();
                            }
                            catch
                            {
                                subcadena2 = "";
                            }


                            SqlConnection cnnqu = new SqlConnection();
                            SqlCommand cmdqu = new SqlCommand();
                            SqlConnection cnnmmd = new SqlConnection();
                            SqlCommand cmdmmd = new SqlCommand();
                            SqlConnection cnnbxy = new SqlConnection();
                            SqlCommand cmdbxy = new SqlCommand();
                    

                            if (DiscountClass.GetMMSchema().ToString() != "")
                            {
                                try
                                {
                                    cnnmmd.ConnectionString = connectionString;
                                    cnnmmd.Open();

                                    cmdmmd.Connection = cnnmmd;
                                    cmdmmd.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount Where [Description] = '" + subcadena + "'";
                                    SqlDataReader dtrmmd = cmdmmd.ExecuteReader();


                                    if (dtrmmd.HasRows)
                                    {
                                        while (dtrmmd.Read())
                                        {
                                            td = dtrmmd[0].ToString();
                                        }
                                    }
                                    dtrmmd.Close();
                                    cnnmmd.Close();
                                }
                                catch { }
                            }
                            else
                            {
                                if (DiscountClass.GetXYSchema().ToString() != "")
                                {
                                    try
                                    {
                                        cnnbxy.ConnectionString = connectionString;
                                        cnnbxy.Open();

                                        cmdbxy.Connection = cnnbxy;
                                        cmdbxy.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount Where [Description] = '" + subcadena2 + "'";
                                        SqlDataReader dtrbxy = cmdbxy.ExecuteReader();


                                        if (dtrbxy.HasRows)
                                        {
                                            while (dtrbxy.Read())
                                            {
                                                td = dtrbxy[0].ToString();
                                            }
                                        }
                                        dtrbxy.Close();
                                        cnnbxy.Close();
                                    }
                                    catch { }
                                }
                                else
                                {
                                    if (DiscountClass.GetQty1().ToString() != "" || DiscountClass.GetUnit1().ToString() != "" || DiscountClass.GetQty2().ToString() != "" || DiscountClass.GetUnit2().ToString() != "" || DiscountClass.GetQty3().ToString() != "" || DiscountClass.GetUnit3().ToString() != "")
                                    {
                                        if (DiscountClass.GetQty1().ToString() == "")
                                        {
                                            qt1 = "0";
                                        }
                                        else { qt1 = DiscountClass.GetQty1().ToString(); }

                                        if (DiscountClass.GetQty2().ToString() == "")
                                        {
                                            qt2 = "0";
                                        }
                                        else { qt2 = DiscountClass.GetQty2().ToString(); }

                                        if (DiscountClass.GetQty3().ToString() == "")
                                        {
                                            qt3 = "0";
                                        }
                                        else { qt3 = DiscountClass.GetQty3().ToString(); }

                                        if (DiscountClass.GetUnit1().ToString() == "")
                                        {
                                            un1 = "0.00";
                                        }
                                        else { un1 = DiscountClass.GetUnit1().ToString(); }

                                        if (DiscountClass.GetUnit2().ToString() == "")
                                        {
                                            un2 = "0.00";
                                        }
                                        else { un2 = DiscountClass.GetUnit2().ToString(); }

                                        if (DiscountClass.GetUnit3().ToString() == "")
                                        {
                                            un3 = "0.00";
                                        }
                                        else { un3 = DiscountClass.GetUnit3().ToString(); }


                                        cnnqu.ConnectionString = connectionString;
                                        cnnqu.Open();
                                        cmdqu.Connection = cnnqu;
                                        cmdqu.CommandText = "INSERT INTO [dbo].[QuantityDiscount]([Description],[HQID],[DiscountOddItems]" +
                                       ",[Quantity1],[Price1],[Price1A],[Price1B],[Price1C],[Quantity2],[Price2],[Price2A],[Price2B],[Price2C],[Quantity3]" +
                                       ",[Price3],[Price3A],[Price3B],[Price3C],[Quantity4],[Price4],[Price4A],[Price4B],[Price4C],[Type],[PercentOffPrice1]" +
                                       ",[PercentOffPrice1A],[PercentOffPrice1B],[PercentOffPrice1C],[PercentOffPrice2],[PercentOffPrice2A],[PercentOffPrice2B]" +
                                       ",[PercentOffPrice2C],[PercentOffPrice3],[PercentOffPrice3A],[PercentOffPrice3B],[PercentOffPrice3C],[PercentOffPrice4]" +
                                       ",[PercentOffPrice4A],[PercentOffPrice4B],[PercentOffPrice4C]) VALUES ('',0,0," + qt1 + "," + un1 + "," +
                                       "0.00,0.00,0.00," + qt2 + " ," + un2 + ",0.00,0.00,0.00," + qt3 + "," + un3 + "," +
                                       "0.00,0.00,0.00,0,0.00,0.00,0.00,0.00,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)";

                                        cmdqu.ExecuteNonQuery();
                                        cnnqu.Close();


                                        SqlConnection cnndisc = new SqlConnection();
                                        SqlCommand cmddisc = new SqlCommand();


                                        try
                                        {
                                            cnndisc.ConnectionString = connectionString;
                                            cnndisc.Open();

                                            cmddisc.Connection = cnndisc;
                                            cmddisc.CommandText = "SELECT [ID],[Description],[Type] FROM QuantityDiscount order by CAST(ID as int) asc";
                                            SqlDataReader dtrdisc = cmddisc.ExecuteReader();


                                            if (dtrdisc.HasRows)
                                            {
                                                while (dtrdisc.Read())
                                                {
                                                    td = dtrdisc[0].ToString();
                                                }
                                            }
                                            dtrdisc.Close();
                                            cnndisc.Close();

                                        }
                                        catch { }

                                    }


                                }

                            }

                            String mi = "";

                            try
                            {
                                sLine = MoreClass.GetMi().ToString();
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
                            }
                            catch
                            {
                                subcadena = "";
                            }

                            SqlConnection cnnmi = new SqlConnection();
                            SqlCommand cmdmi = new SqlCommand();

                            try
                            {
                                cnnmi.ConnectionString = connectionString;
                                cnnmi.Open();

                                cmdmi.Connection = cnnmi;
                                cmdmi.CommandText = "SELECT [ID],[Title] FROM ItemMessage Where [Title] = '" + subcadena + "'";
                                SqlDataReader dtrmi = cmdmi.ExecuteReader();


                                if (dtrmi.HasRows)
                                {
                                    while (dtrmi.Read())
                                    {
                                        mi = dtrmi[0].ToString();
                                    }
                                }
                                dtrmi.Close();
                                cnnmi.Close();
                            }
                            catch { }

                            if (taq == "") { taq = "0"; }
                            if (chq == "") { chq = "0"; }

                    
                            String taxable = "0";
                            if (ti != "0") { taxable = "1"; }

                            if (st == "") { st = "0"; }

                            string pricea = "0.00";
                            string priceb = "0.00";
                            string pricec = "0.00";
                            string saleprice = "0.00";

                            if (PricingClass.GetPriceA().ToString() != "") { pricea = PricingClass.GetPriceA().ToString(); }
                            if (PricingClass.GetPriceB().ToString() != "") { priceb = PricingClass.GetPriceB().ToString(); }
                            if (PricingClass.GetPriceC().ToString() != "") { pricec = PricingClass.GetPriceC().ToString(); }
                            if (PricingClass.GetSalePrice().ToString() != "") { saleprice = PricingClass.GetSalePrice().ToString(); }

                            cnn.ConnectionString = connectionString;
                            cnn.Open();
                            cmd.Connection = cnn;
                            cmd.CommandText = "INSERT INTO [dbo].[Item] ([BinLocation],[BuydownPrice],[BuydownQuantity],[CommissionAmount]" +
                            ",[CommissionMaximum],[CommissionMode],[CommissionPercentProfit],[CommissionPercentSale],[Description]" +
                            ",[FoodStampable],[HQID],[ItemNotDiscountable],[LastReceived],[LastUpdated],[Notes]" +
                            ",[QuantityCommitted],[SerialNumberCount],[TareWeightPercent],[ItemLookupCode],[DepartmentID]" +
                            ",[CategoryID],[MessageID],[Price],[PriceA],[PriceB],[PriceC],[SalePrice],[SaleStartDate],[SaleEndDate]" +
                            ",[QuantityDiscountID],[TaxID],[ItemType],[Cost],[Quantity],[ReorderPoint],[RestockLevel],[TareWeight]" +
                            ",[SupplierID],[TagAlongItem],[TagAlongQuantity],[ParentItem],[ParentQuantity],[BarcodeFormat],[PriceLowerBound]" +
                            ",[PriceUpperBound],[PictureName],[LastSold],[ExtendedDescription],[SubDescription1],[SubDescription2]" +
                            ",[SubDescription3],[UnitOfMeasure],[SubCategoryID],[QuantityEntryNotAllowed],[PriceMustBeEntered],[BlockSalesReason]" +
                            ",[BlockSalesAfterDate],[Weight],[Taxable],[BlockSalesBeforeDate],[LastCost],[ReplacementCost],[WebItem]" +
                            ",[BlockSalesType],[BlockSalesScheduleID],[SaleType],[SaleScheduleID],[Consignment],[Inactive],[LastCounted]" +
                            ",[DoNotOrder],[MSRP],[DateCreated],[Content],[UsuallyShip]) VALUES ('',0.00,0,0.00,0.00,0,0,0,'" + desc + "'," + fs + "," +
                            "0,0,'1900-01-01 00:00:01.000','1900-01-01 00:00:01.000',NULL,0,0,0,'" + ItemClass.GetItemBarcode().ToString() + "','" + di + "','" + ci + "'" +
                            ",'"+ mi +"'," + price + ","+pricea+ "," + priceb + "," + pricec + "," + saleprice + ",'"+fas+"','"+fes+"','"+td+"','" + ti + "',0," + cost + ",0,0,0," +
                            "0,0,"+ta+","+taq+","+pi+","+chq+",0,0.00,0.00,'',NULL,'','','','','',0,0,0" +
                            ",'',NULL,0,'" + taxable + "',NULL,0.00,0.00,0,0,0,'"+st+"','"+sch+"',0,0,NULL,0,0.00,'1900-01-01 00:00:01.000','','')";
                            cmd.ExecuteNonQuery();

                            String msg;
                            msg = "Changes have been applied. New item has been created.";
                            ShowDialog(msg);

                            ItemClass.AddData("", "", "", "", "", "", "", "", "", "", "");
                            PricingClass.AddData("", "", "", "", "", "", "", "");
                            DiscountClass.AddData("", "", "", "", "", "", "", "");
                            MoreClass.AddData("", "", "");

                            bool es = false;
                            Btn_ItemGeneralOptions.IsChecked = !es;
                            Btn_DiscountO.IsChecked = es;
                            Btn_PricingO.IsChecked = es;
                            Btn_MoreO.IsChecked = es;
                            ItemMFrame.Navigate(typeof(General));

                        }
                        else
                        {
                            String mensa1;
                            mensa1 = "Fill Description Field";
                            ShowDialog(mensa1);
                        }
                    }
                    else
                    {
                        String mensa;
                        mensa = "Fill Barcode Field";
                        ShowDialog(mensa);
                    }


                }

            }


        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
           
            ItemClass.AddData("", "", "", "", "", "", "", "", "", "", "");

            bool es = false;
            Btn_ItemGeneralOptions.IsChecked = !es;
            Btn_DiscountO.IsChecked = es;
            Btn_PricingO.IsChecked = es;
            Btn_MoreO.IsChecked = es;
            ItemMFrame.Navigate(typeof(General));
        }



        public async void ShowDialog(String msg)
        {

            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }

    }
}
