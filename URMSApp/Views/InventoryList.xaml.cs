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
using Windows.UI.ViewManagement;
using System.Diagnostics;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace URMSApp.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class InventoryList : Page
    {
        string ilstchose = "";
        string ipoidchose = "";
        string piid = "";
        public InventoryList()
        {
            this.InitializeComponent();
                        
            string ft = InventoryType.GetInventoryType();

            if (ft == "New")
            {
                InventoryItemListView.IsEnabled = true;
                TextTo.IsEnabled = true;
                TextINV.IsEnabled = true;
                BtnAddI.IsEnabled = true;
                BtnDeleteI.IsEnabled = true;
                TextDate.IsEnabled = false;
            }
            else if (ft == "Edit")
            {
                InventoryItemListView.IsEnabled = true;
                TextTo.IsEnabled = true;
                TextINV.IsEnabled = true;
                BtnAddI.IsEnabled = true;
                BtnDeleteI.IsEnabled = true;
                TextDate.IsEnabled = false;
            }
            

            if (ft != "New")
            {
                string invi = InventoryClass.GetInventoryId();
                
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
                    cmd.CommandText = "SELECT [ID],StoreID,OpenTime,Description,Code,Status FROM [PhysicalInventory] WHERE PhysicalInventory.ID = '" + invi + "'";

                    SqlDataReader dtr = cmd.ExecuteReader();
                                      
                    
                    if (dtr.HasRows)
                    {
                        while (dtr.Read())
                        {
                            string sta = "";
                            if (dtr[5].ToString() == "0") { sta = "Opened"; }
                            else
                            {
                                if (dtr[5].ToString() == "1")
                                {
                                    sta = "Calculated";
                                    BtnAddI.IsEnabled = false;
                                    BtnDeleteI.IsEnabled = false;

                                }
                                else
                                {
                                    if (dtr[5].ToString() == "2")
                                    {
                                        sta = "Closed"; 
                                        BtnAddI.IsEnabled = false;
                                        BtnDeleteI.IsEnabled = false;
                                    }
                                }
                            }
                            
                            TextDate.Text = dtr[2].ToString();
                            TextINV.Text = dtr[4].ToString();
                            CombStatus.Text = sta;
                            TextTo.Text = dtr[3].ToString();

                           

                        }

                    }
                    else
                    {
                        String msg = "";
                        msg = "Physical Inventory does not exist";
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

            } else
            {
                TextTo.Text = "";
                TextTo.IsEnabled = true;

                TextINV.Text = "";
                TextINV.IsEnabled = true;
                TextDate.Text = DateTime.Now.ToString();

                
                CollectionVM.InventoryitemList.Clear();
            }
        }

        public async void ShowDialog(string msg)
        {
            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }


        private void ItemListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void BtnAddI_Click(object sender, RoutedEventArgs e)
        {
            if (!AddIPopup.IsOpen)
            {
                Title.Text = "Add Item";
                TextBAddItem.InputScope = new InputScope()
                {
                    Names = { new InputScopeName(InputScopeNameValue.Number) }
                };


                AddIPopup.IsOpen = true;
                TextBAddItem.PreventKeyboardDisplayOnProgrammaticFocus = false;
                TextBAddItem.Focus(FocusState.Programmatic);
            }
           
        }

        private void BtnDeleteI_Click(object sender, RoutedEventArgs e)
        {
            YESNOIPopup.IsOpen = true;
        }

        private void BtnOkPO_Click(object sender, RoutedEventArgs e)
        {
            string ft = InventoryType.GetInventoryType();

            string sLine = "";
            int nc = 0;
            string tcn = "";
            string subcadena = "";

            if (ft == "New")
            {

                string srv = Class1.GetServer();
                string srvu = Class1.GetUser();
                string srvp = Class1.GetPass();
                string srvdb = Class1.GetDb();
                string inicio = "";
                string idesc = "";

                SqlConnection cnn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                SqlConnection cnninic = new SqlConnection();
                SqlCommand cmdinic = new SqlCommand();

                string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                
                int poinic = 1;

                try
                {

                    cnninic.ConnectionString = connectionString;
                    cnninic.Open();

                    cmdinic.Connection = cnninic;
                    cmdinic.CommandText = "SELECT [ID] FROM PhysicalInventory order by id asc";
                    SqlDataReader dtrinic = cmdinic.ExecuteReader();


                    if (dtrinic.HasRows)
                    {
                        while (dtrinic.Read())
                        {
                            poinic = Int32.Parse(dtrinic[0].ToString()) + 1;
                        }
                    }

                   
                }
                catch { }

                string sti = "";

                SqlConnection cnnsti = new SqlConnection();
                SqlCommand cmdsti = new SqlCommand();
                cnnsti.ConnectionString = connectionString;
                cnnsti.Open();

                cmdsti.Connection = cnnsti;
                cmdsti.CommandText = "SELECT [StoreID] FROM Configuration";
                SqlDataReader dtrsti = cmdsti.ExecuteReader();

                if (dtrsti.HasRows)
                {
                    while (dtrsti.Read())
                    {
                        sti = dtrsti[0].ToString();
                    }
                }

                string t = DateTime.Now.ToString("yyyy-MM-dd");
                string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
                string lastid = "";

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


                idesc = subcadena;



                SqlConnection cnn1 = new SqlConnection();
                SqlCommand cmd1 = new SqlCommand();

                try
                {
                    
                    cnn1.ConnectionString = connectionString;
                    cnn1.Open();

                    cmd1.Connection = cnn1;
                    cmd1.CommandText = "INSERT INTO [PhysicalInventory] ([StoreID],[OpenTime],[CloseTime]," +
                   @"[Status],[LastRefresh],[Description],[Code]) VALUES (" + sti + ",'" + now + "',NULL,0,NULL,'" + idesc + "'" +
                   @",'" + TextINV.Text + "')";

                    cmd1.ExecuteNonQuery();
                    cnn1.Close();



                    SqlConnection pocnn = new SqlConnection();
                    SqlCommand pocmd = new SqlCommand();
                    try
                    {
                        pocnn.ConnectionString = connectionString;
                        pocnn.Open();
                        pocmd.Connection = pocnn;
                        pocmd.CommandText = "SELECT [ID] FROM PhysicalInventory order by id asc";
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

                    /// Insert en PhysicalInventoryEntry
                    /// 
                    try
                    {
                        SqlConnection cnn2 = new SqlConnection();
                        SqlCommand cmd2 = new SqlCommand();
                        cnn2.ConnectionString = connectionString;
                        cnn2.Open();

                        cmd2.Connection = cnn2;
                        cmd2.CommandText = "SELECT PhysicalInventoryEntry.[ID], PhysicalInventoryEntry.ItemID FROM PhysicalInventoryEntry left join Item on" +
                            @" Item.ID = PhysicalInventoryEntry.ItemID where PhysicalInventoryEntry.[PhysicalInventoryID] = '0'";
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
                                    cmd3.CommandText = "UPDATE [PhysicalInventoryEntry] SET [PhysicalInventoryID] = '" + lastid + "'  Where ID = '" + dtr2[0].ToString() + "'";

                                    cmd3.ExecuteNonQuery();

                                    CollectionVM.InventoryList.Add(new ManualInventoryClass()
                                    {
                                        INVId = lastid,
                                        INVDescription = idesc,
                                        INVStoreId = sti,
                                        INVCode = TextINV.Text,
                                        INVDate = now,
                                        INVStatus = "0"
                                    });                                                                                                    


                                }
                                catch (SqlException ex)
                                {
                                    ShowDialog("Insert Error: " + ex.ToString());
                                }

                            }
                            cnn3.Close();
                            dtr2.Close();
                            cnn2.Close();
                        }


                    }
                    catch (SqlException ex2)
                    {

                        ShowDialog("Error inserting detail: " + ex2.ToString());

                    }

                    string msg = "Physical Inventory has been created";
                    ShowDialog(msg);

                }
                catch (SqlException ex1)
                {
                   
                        ShowDialog(ex1.ToString());
                   
                }

                 

                
            } else if (ft == "Edit")
            {

                string srv = Class1.GetServer();
                string srvu = Class1.GetUser();
                string srvp = Class1.GetPass();
                string srvdb = Class1.GetDb();
                string inicio = "";
                string idesc = "";

                SqlConnection cnn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                SqlConnection cnninic = new SqlConnection();
                SqlCommand cmdinic = new SqlCommand();

                string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;

                string invi = InventoryClass.GetInventoryId();

                
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


                idesc = subcadena;



                SqlConnection cnn1 = new SqlConnection();
                SqlCommand cmd1 = new SqlCommand();

                try
                {

                    cnn1.ConnectionString = connectionString;
                    cnn1.Open();

                    cmd1.Connection = cnn1;
                    cmd1.CommandText = "UPDATE [PhysicalInventory] SET [Description] = '" + idesc + "',[Code] = '" + TextINV.Text + "' WHERE ID = " + invi;

                    cmd1.ExecuteNonQuery();
                    cnn1.Close();

                    string msg = "Physical Inventory has been updated";
                    ShowDialog(msg);

                }
                catch (SqlException ex1)
                {

                    ShowDialog(ex1.ToString());

                }

            }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }

            InventoryClass.AddInventory("", "", "", "", "", "");
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
                cmd.CommandText = "DELETE from PhysicalInventoryEntry where PhysicalInventoryId = '0'";

                SqlDataReader dtr = cmd.ExecuteReader();
            }
            catch { }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string ft = InventoryType.GetInventoryType();
            string sLine = "";
            int nc = 0;
            string tcn = "";
            string subcadena = "";

            if (TextBAddItem.Text != "")
            {
                if (ft == "Edit")
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
                    string cant = "";
                    string oldcant = "";

                    String srv = Class1.GetServer();
                    String srvu = Class1.GetUser();
                    String srvp = Class1.GetPass();
                    String srvdb = Class1.GetDb();
                    string piid = InventoryClass.GetInventoryId();
                    string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
                    string ic = TextBAddItem.Text;

                    string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                    try
                    {
                        cnn.ConnectionString = connectionString;
                        cnn.Open();

                        cmd.Connection = cnn;
                        cmd.CommandText = "SELECT [ItemLookupCode],[Description],Item.[ID],[Price],[Cost],Item.[DepartmentID],[Department].Name" +
                            @",Item.[CategoryID],[Category].Name FROM Item left Join Department on Item.DepartmentID = Department.ID left Join Category on" +
                            @" Item.CategoryID = Category.ID Where [ItemLookupCode] = '" + ic + "'";
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
                                if (TextBCantAddItem.Text == "")
                                {
                                    cant = "1";
                                } else {  cant = TextBCantAddItem.Text; }
                                

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
                                    SqlConnection cnncons = new SqlConnection();
                                    SqlCommand cmdcons = new SqlCommand();
                                    string entryid = "";

                                    cnncons.ConnectionString = connectionString;
                                    cnncons.Open();

                                    cmdcons.Connection = cnncons;
                                    cmdcons.CommandText = "SELECT [ID],[QuantityCounted] FROM PhysicalInventoryEntry where itemid = " + dtr[2].ToString() + " and PhysicalInventoryId = " + piid + " order by ID asc";
                                    SqlDataReader dtrcons = cmdcons.ExecuteReader();

                                    if (dtrcons.HasRows)
                                    {
                                        while (dtrcons.Read())
                                        {
                                            entryid = dtrcons[0].ToString();
                                            oldcant = dtrcons[1].ToString();
                                        }
                                    }

                                    cnn2.ConnectionString = connectionString;
                                    cnn2.Open();

                                    cmd2.Connection = cnn2;

                                    if (entryid == "")
                                    {
                                           cmd2.CommandText = "INSERT INTO [PhysicalInventoryEntry] ([StoreID],[PhysicalInventoryID],[ReasonCodeID],[CountTime]" +
                                           @",[ItemID],[BinLocation],[Price],[Cost],[QuantityCounted],[QuantitySold],[QuantityReturned],[QuantityXferIn]" +
                                           @",[QuantityXferOut],[QuantityAdjusted],[QuantityToOffline],[QuantityFromOffline],[QuantityRefreshed]) VALUES " +
                                           @"(" + sti + "," + piid + ",0,'" + now + "'," + dtr[2].ToString() + ",''," + dtr[3].ToString() + "," + dtr[4].ToString() + "," + TextBCantAddItem.Text + "," +
                                           @"0,0,0,0,0,0,0,0)";

                                        cmd2.ExecuteNonQuery();


                                        SqlConnection cnnitem = new SqlConnection();
                                        SqlCommand cmditem = new SqlCommand();
                                        string lastitem = "";

                                        cnnitem.ConnectionString = connectionString;
                                        cnnitem.Open();

                                        cmditem.Connection = cnnitem;
                                        cmditem.CommandText = "SELECT [ID] FROM PhysicalInventoryEntry order by ID asc";
                                        SqlDataReader dtritem = cmditem.ExecuteReader();

                                        if (dtritem.HasRows)
                                        {
                                            while (dtritem.Read())
                                            {
                                                lastitem = dtritem[0].ToString();
                                            }
                                        }

                                        CollectionVM.InventoryitemList.Add(new Models.InventoryItemClass()
                                        {
                                            ItemLookupCode = dtr[0].ToString(),
                                            Description = subcadena,
                                            CategoryID = dtr[7].ToString(),
                                            Category = dtr[8].ToString(),
                                            DepartmentID = dtr[5].ToString(),
                                            Department = dtr[6].ToString(),
                                            Counted = TextBCantAddItem.Text,
                                            EntryID = lastitem

                                        });


                                        dtritem.Close();
                                    } else
                                    {
                                        int qc = 0;
                                        qc = Convert.ToInt16(TextBCantAddItem.Text) + Convert.ToInt16(oldcant);

                                        cmd2.CommandText = "UPDATE [PhysicalInventoryEntry] SET [QuantityCounted] = " + qc + " where id = " + entryid;

                                        cmd2.ExecuteNonQuery();

                                        var item = CollectionVM.InventoryitemList.FirstOrDefault(i => i.ItemLookupCode == dtr[0].ToString());
                                        if (item != null)
                                        {
                                            item.Counted = qc.ToString();
                                        }

                                    };                                 
                                                                  

                                    
                                }
                                catch (SqlException ex)
                                {
                                    ShowDialog("Error en insert" + ex.ToString());
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

                   
                }
                else if (ft == "New")
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
                    string cant = "";
                    string oldcant = "";

                    String srv = Class1.GetServer();
                    String srvu = Class1.GetUser();
                    String srvp = Class1.GetPass();
                    String srvdb = Class1.GetDb();
                    string piid = "0";
                    string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
                    string ic = TextBAddItem.Text;

                    string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                    try
                    {
                        cnn.ConnectionString = connectionString;
                        cnn.Open();

                        cmd.Connection = cnn;
                        cmd.CommandText = "SELECT [ItemLookupCode],[Description],Item.[ID],[Price],[Cost],Item.[DepartmentID],[Department].Name" +
                            @",Item.[CategoryID],[Category].Name FROM Item left Join Department on Item.DepartmentID = Department.ID left Join Category on" +
                            @" Item.CategoryID = Category.ID Where [ItemLookupCode] = '" + ic + "'";
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
                                if (TextBCantAddItem.Text == "")
                                {
                                    cant = "1";
                                }
                                else { cant = TextBCantAddItem.Text; }


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
                                    SqlConnection cnncons = new SqlConnection();
                                    SqlCommand cmdcons = new SqlCommand();
                                    string entryid = "";

                                    cnncons.ConnectionString = connectionString;
                                    cnncons.Open();

                                    cmdcons.Connection = cnncons;
                                    cmdcons.CommandText = "SELECT [ID],[QuantityCounted] FROM PhysicalInventoryEntry where itemid = " + dtr[2].ToString() + " and PhysicalInventoryId = " + piid + " order by ID asc";
                                    SqlDataReader dtrcons = cmdcons.ExecuteReader();

                                    if (dtrcons.HasRows)
                                    {
                                        while (dtrcons.Read())
                                        {
                                            entryid = dtrcons[0].ToString();
                                            oldcant = dtrcons[1].ToString();
                                        }
                                    }

                                    cnn2.ConnectionString = connectionString;
                                    cnn2.Open();

                                    cmd2.Connection = cnn2;

                                    if (entryid == "")
                                    {
                                        cmd2.CommandText = "INSERT INTO [PhysicalInventoryEntry] ([StoreID],[PhysicalInventoryID],[ReasonCodeID],[CountTime]" +
                                        @",[ItemID],[BinLocation],[Price],[Cost],[QuantityCounted],[QuantitySold],[QuantityReturned],[QuantityXferIn]" +
                                        @",[QuantityXferOut],[QuantityAdjusted],[QuantityToOffline],[QuantityFromOffline],[QuantityRefreshed]) VALUES " +
                                        @"(" + sti + "," + piid + ",0,'" + now + "'," + dtr[2].ToString() + ",''," + dtr[3].ToString() + "," + dtr[4].ToString() + "," + TextBCantAddItem.Text + "," +
                                        @"0,0,0,0,0,0,0,0)";

                                        cmd2.ExecuteNonQuery();


                                        SqlConnection cnnitem = new SqlConnection();
                                        SqlCommand cmditem = new SqlCommand();
                                        string lastitem = "";

                                        cnnitem.ConnectionString = connectionString;
                                        cnnitem.Open();

                                        cmditem.Connection = cnnitem;
                                        cmditem.CommandText = "SELECT [ID] FROM PhysicalInventoryEntry order by ID asc";
                                        SqlDataReader dtritem = cmditem.ExecuteReader();

                                        if (dtritem.HasRows)
                                        {
                                            while (dtritem.Read())
                                            {
                                                lastitem = dtritem[0].ToString();
                                            }
                                        }

                                        CollectionVM.InventoryitemList.Add(new Models.InventoryItemClass()
                                        {
                                            ItemLookupCode = dtr[0].ToString(),
                                            Description = subcadena,
                                            CategoryID = dtr[7].ToString(),
                                            Category = dtr[8].ToString(),
                                            DepartmentID = dtr[5].ToString(),
                                            Department = dtr[6].ToString(),
                                            Counted = TextBCantAddItem.Text,
                                            EntryID = lastitem

                                        });


                                        dtritem.Close();
                                    }
                                    else
                                    {
                                        int qc = 0;
                                        qc = Convert.ToInt16(TextBCantAddItem.Text) + Convert.ToInt16(oldcant);

                                        cmd2.CommandText = "UPDATE [PhysicalInventoryEntry] SET [QuantityCounted] = " + qc + " where id = " + entryid;

                                        cmd2.ExecuteNonQuery();

                                        var item = CollectionVM.InventoryitemList.FirstOrDefault(i => i.ItemLookupCode == dtr[0].ToString());
                                        if (item != null)
                                        {
                                            item.Counted = qc.ToString();
                                        }

                                    };


                                }
                                catch (SqlException ex)
                                {
                                    ShowDialog("Error en insert" + ex.ToString());
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

                }

            }
            else
            {
                String msg;
                msg = "Fill Barcode Field";
                ShowDialog(msg);
            }

            TextBAddItem.Text = "";
            TextBCantAddItem.Text = "";

            InputPane.GetForCurrentView().TryShow();

            TextBAddItem.InputScope = new InputScope()
            {
                Names = { new InputScopeName(InputScopeNameValue.Default) }
            };

            TextBAddItem.Focus(FocusState.Programmatic);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            if (AddIPopup != null) { AddIPopup.IsOpen = false; }
            TextBAddItem.Text = "";
            TextBCantAddItem.Text = "";
        }

        private void AddIPopup_Loaded(object sender, RoutedEventArgs e)
        {
            TextBAddItem.InputScope = new InputScope()
            {
                Names = { new InputScopeName(InputScopeNameValue.Number) }
            };

            TextBAddItem.Focus(FocusState.Programmatic);
        }
              
   
    private void TextBAddItem_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            string ft = InventoryType.GetInventoryType();
            string sLine = "";
            int nc = 0;
            string tcn = "";
            string subcadena = "";

            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (TextBAddItem.Text != "")
                {
                    if (ft == "Edit")
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
                        string piid = InventoryClass.GetInventoryId();
                        string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
                        string ic = TextBAddItem.Text;

                        string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                        try
                        {
                            cnn.ConnectionString = connectionString;
                            cnn.Open();

                            cmd.Connection = cnn;
                            cmd.CommandText = "SELECT [ItemLookupCode],[Description],Item.[ID],[Price],[Cost],Item.[DepartmentID],[Department].Name" +
                                @",Item.[CategoryID],[Category].Name FROM Item left Join Department on Item.DepartmentID = Department.ID left Join Category on" +
                                @" Item.CategoryID = Category.ID Where [ItemLookupCode] = '" + ic + "'";
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
                                    string folderPath = @"C:\Program Files\Common Files\Microsoft Shared\ink";
                                    string keyBPath = Path.Combine(folderPath, "TabTip.exe");
                                    Process.Start(keyBPath);

                                    TextBCantAddItem.InputScope = new InputScope()
                                    {
                                       Names = { new InputScopeName(InputScopeNameValue.Number) }
                                    };

                                        TextBCantAddItem.Focus(FocusState.Programmatic);


                                    //    string cant = TextBCantAddItem.Text;

                                    //    sLine = dtr[1].ToString();
                                    //    nc = sLine.Length;
                                    //    tcn = "";
                                    //    subcadena = "";

                                    //    for (int iline = 0; iline < nc; iline++)
                                    //    {
                                    //        tcn = sLine.Substring(iline, 1);
                                    //        if (tcn == "'")
                                    //        {
                                    //            tcn = "''";
                                    //            subcadena = subcadena + tcn;
                                    //        }
                                    //        else
                                    //        {
                                    //            subcadena = subcadena + tcn;
                                    //        }
                                    //    }
                                    //    subcadena = subcadena.TrimEnd();
                                    //    subcadena = subcadena.TrimStart();

                                    //    try
                                    //    {
                                    //        cnn2.ConnectionString = connectionString;
                                    //        cnn2.Open();

                                    //        cmd2.Connection = cnn2;

                                    //        cmd2.CommandText = "INSERT INTO [PhysicalInventoryEntry] ([StoreID],[PhysicalInventoryID],[ReasonCodeID],[CountTime]" +
                                    //        @",[ItemID],[BinLocation],[Price],[Cost],[QuantityCounted],[QuantitySold],[QuantityReturned],[QuantityXferIn]" +
                                    //        @",[QuantityXferOut],[QuantityAdjusted],[QuantityToOffline],[QuantityFromOffline],[QuantityRefreshed]) VALUES " +
                                    //        @"(" + sti + "," + piid + ",0,'" + now + "'," + dtr[2].ToString() + ",''," + dtr[3].ToString() + "," + dtr[4].ToString() + "," + TextBCantAddItem.Text + "," +
                                    //        @"0,0,0,0,0,0,0,0)";


                                    //        cmd2.ExecuteNonQuery();

                                    //        SqlConnection cnnitem = new SqlConnection();
                                    //        SqlCommand cmditem = new SqlCommand();
                                    //        string lastitem = "";

                                    //        cnnitem.ConnectionString = connectionString;
                                    //        cnnitem.Open();

                                    //        cmditem.Connection = cnnitem;
                                    //        cmditem.CommandText = "SELECT [ID] FROM PhysicalInventoryEntry order by ID asc";
                                    //        SqlDataReader dtritem = cmditem.ExecuteReader();

                                    //        if (dtritem.HasRows)
                                    //        {
                                    //            while (dtritem.Read())
                                    //            {
                                    //                lastitem = dtritem[0].ToString();
                                    //            }
                                    //        }

                                    //        CollectionVM.InventoryitemList.Add(new Models.InventoryItemClass()
                                    //        {
                                    //            ItemLookupCode = dtr[0].ToString(),
                                    //            Description = subcadena,
                                    //            CategoryID = dtr[7].ToString(),
                                    //            Category = dtr[8].ToString(),
                                    //            DepartmentID = dtr[5].ToString(),
                                    //            Department = dtr[6].ToString(),
                                    //            Counted = TextBCantAddItem.Text,
                                    //            EntryID = lastitem

                                    //        });

                                    //        dtritem.Close();
                                    //    }
                                    //    catch (SqlException ex)
                                    //    {
                                    //        ShowDialog("Error en insert" + ex.ToString());
                                    //    }
                                    //    cnn2.Close();
                                }
                            }
                            else
                            {
                                String msg;
                                msg = "Scan Barcode does not exist";
                                ShowDialog(msg);

                                TextBAddItem.Text = "";
                                TextBCantAddItem.Text = "";

                                TextBAddItem.InputScope = new InputScope()
                                {
                                    Names = { new InputScopeName(InputScopeNameValue.Default) }
                                };

                                TextBAddItem.Focus(FocusState.Programmatic);
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


                    }
                    else if (ft == "New")
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
                        string piid = "0";
                        string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
                        string ic = TextBAddItem.Text;

                        string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                        try
                        {
                            cnn.ConnectionString = connectionString;
                            cnn.Open();

                            cmd.Connection = cnn;
                            cmd.CommandText = "SELECT [ItemLookupCode],[Description],Item.[ID],[Price],[Cost],Item.[DepartmentID],[Department].Name" +
                                @",Item.[CategoryID],[Category].Name FROM Item left Join Department on Item.DepartmentID = Department.ID left Join Category on" +
                                @" Item.CategoryID = Category.ID Where [ItemLookupCode] = '" + ic + "'";
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
                                    TextBCantAddItem.InputScope = new InputScope()
                                    {
                                        Names = { new InputScopeName(InputScopeNameValue.Number) }
                                    };

                                    TextBCantAddItem.Focus(FocusState.Programmatic);


                                    //sLine = dtr[1].ToString();
                                    //nc = sLine.Length;
                                    //tcn = "";
                                    //subcadena = "";

                                    //for (int iline = 0; iline < nc; iline++)
                                    //{
                                    //    tcn = sLine.Substring(iline, 1);
                                    //    if (tcn == "'")
                                    //    {
                                    //        tcn = "''";
                                    //        subcadena = subcadena + tcn;
                                    //    }
                                    //    else
                                    //    {
                                    //        subcadena = subcadena + tcn;
                                    //    }
                                    //}
                                    //subcadena = subcadena.TrimEnd();
                                    //subcadena = subcadena.TrimStart();

                                    //try
                                    //{
                                    //    cnn2.ConnectionString = connectionString;
                                    //    cnn2.Open();

                                    //    cmd2.Connection = cnn2;
                                    //    cmd2.CommandText = "INSERT INTO [PhysicalInventoryEntry] ([StoreID],[PhysicalInventoryID],[ReasonCodeID],[CountTime]" +
                                    //   @",[ItemID],[BinLocation],[Price],[Cost],[QuantityCounted],[QuantitySold],[QuantityReturned],[QuantityXferIn]" +
                                    //   @",[QuantityXferOut],[QuantityAdjusted],[QuantityToOffline],[QuantityFromOffline],[QuantityRefreshed]) VALUES " +
                                    //   @"(" + sti + "," + piid + ",0,'" + now + "'," + dtr[2].ToString() + ",''," + dtr[3].ToString() + "," + dtr[4].ToString() + "," + TextBCantAddItem.Text + "," +
                                    //   @"0,0,0,0,0,0,0,0)";

                                    //    cmd2.ExecuteNonQuery();

                                    //    SqlConnection cnnitem = new SqlConnection();
                                    //    SqlCommand cmditem = new SqlCommand();
                                    //    string lastitem = "";

                                    //    cnnitem.ConnectionString = connectionString;
                                    //    cnnitem.Open();

                                    //    cmditem.Connection = cnnitem;
                                    //    cmditem.CommandText = "SELECT [ID] FROM PhysicalInventoryEntry order by ID asc";
                                    //    SqlDataReader dtritem = cmditem.ExecuteReader();

                                    //    if (dtritem.HasRows)
                                    //    {
                                    //        while (dtritem.Read())
                                    //        {
                                    //            lastitem = dtritem[0].ToString();
                                    //        }
                                    //    }

                                    //    CollectionVM.InventoryitemList.Add(new Models.InventoryItemClass()
                                    //    {
                                    //        ItemLookupCode = dtr[0].ToString(),
                                    //        Description = subcadena,
                                    //        CategoryID = dtr[7].ToString(),
                                    //        Category = dtr[8].ToString(),
                                    //        DepartmentID = dtr[5].ToString(),
                                    //        Department = dtr[6].ToString(),
                                    //        Counted = TextBCantAddItem.Text,
                                    //        EntryID = lastitem

                                    //    });
                                    //}
                                    //catch (SqlException ex)
                                    //{
                                    //    ShowDialog("Error en insert" + ex.ToString());
                                    //}

                                    //cnn2.Close();
                                }
                            }
                            else
                            {
                                String msg;
                                msg = "Scan Barcode does not exist";
                                ShowDialog(msg);

                                TextBAddItem.Text = "";
                                TextBCantAddItem.Text = "";

                                TextBAddItem.InputScope = new InputScope()
                                {
                                    Names = { new InputScopeName(InputScopeNameValue.Default) }
                                };

                                TextBAddItem.Focus(FocusState.Programmatic);
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

                    }

                }
                else
                {
                    String msg;
                    msg = "Fill Barcode Field";
                    ShowDialog(msg);
                }

                

                bool validPOSOpen = false;
                bool validKeyBoardOpen = false;
                Process cmdpr = new Process();
                cmdpr.StartInfo.FileName = "cmd.exe";
                cmdpr.StartInfo.RedirectStandardInput = true;
                cmdpr.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                cmdpr.StartInfo.RedirectStandardOutput = true;
                cmdpr.StartInfo.CreateNoWindow = true;
                cmdpr.StartInfo.UseShellExecute = false;
                string programFileRute = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles);
                string programFileRute86 = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
                string programPath = programFileRute86;
                if (String.IsNullOrEmpty(programFileRute86))
                {
                    programPath = programFileRute;
                }
                string raiz = System.Environment.SystemDirectory.Remove(3);
                //this.Hide();
                // this.WindowState = FormWindowState.Minimized;

                ShowDialog("AQUI ESTOY");
                        cmdpr.Start();
                        cmdpr.StandardInput.WriteLine("\"" + programPath + "\\MountFocus\\Keyboard\\Kbd.exe\" " + raiz + "\\KBD\\KBD.kbd");
                        cmdpr.StandardInput.Flush();
                        cmdpr.StandardInput.Close();
                        validKeyBoardOpen = true;
                    

                TextBCantAddItem.Text = "";
            }
        }

       
        private void InventoryItemListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var inventoryitem = (InventoryItemClass)e.ClickedItem;
                        
            ilstchose = inventoryitem.ItemLookupCode;
                 
            ipoidchose = inventoryitem.EntryID;    
                   
        }

        private void BtnY_Click(object sender, RoutedEventArgs e)
        {
            piid = InventoryClass.GetInventoryId();
            string ft = InventoryType.GetInventoryType();

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
                string ilc = ItemClass.GetItemBarcode();
                string ipoid = IPOid.GetIpoid();
                string iid = "";
                piid = "0";

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
                    cmd2.CommandText = "DELETE FROM [PhysicalInventoryEntry]  WHERE [PhysicalInventoryId] = '" + piid + "' and [ItemID] = '" + iid + "' and [ID]='" + ipoidchose + "'";
                    cmd2.ExecuteNonQuery();

                    while (InventoryItemListView.SelectedItems.Count > 0)
                    {
                        CollectionVM.InventoryitemList.Remove((Models.InventoryItemClass)InventoryItemListView.SelectedItem);
                    }

                }
                else
                {
                    string msg = "ItemLookupCode doesn't exist";
                    ShowDialog(msg);
                }

                
            }
            else if (ft == "Edit")
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
                    cmd2.CommandText = "DELETE FROM [PhysicalInventoryEntry]  WHERE [PhysicalInventoryId] = '" + piid + "' and [ItemID] = '" + iid + "' and [ID]='" + ipoidchose + "'";
                    cmd2.ExecuteNonQuery();

                    while (InventoryItemListView.SelectedItems.Count > 0)
                    {
                        CollectionVM.InventoryitemList.Remove((Models.InventoryItemClass)InventoryItemListView.SelectedItem);
                    }

                }
                else
                {
                    string msg = "ItemLookupCode doesn't exist";
                    ShowDialog(msg);
                }
                               
            }

            if (YESNOIPopup != null) { YESNOIPopup.IsOpen = false; }
        }

        private void BtnN_Click(object sender, RoutedEventArgs e)
        {
            if (YESNOIPopup != null) { YESNOIPopup.IsOpen = false; }
        }

        private void YESNOIPopup_Loaded(object sender, RoutedEventArgs e)
        {
            BtnN.Focus(FocusState.Programmatic);
        }

        private void TextBCantAddItem_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            string ft = InventoryType.GetInventoryType();
            string sLine = "";
            int nc = 0;
            string tcn = "";
            string subcadena = "";
            string oldcant = "";

            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (TextBAddItem.Text != "")
                {
                    if (ft == "Edit")
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
                        string cant = "";

                        String srv = Class1.GetServer();
                        String srvu = Class1.GetUser();
                        String srvp = Class1.GetPass();
                        String srvdb = Class1.GetDb();
                        string piid = InventoryClass.GetInventoryId();
                        string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
                        string ic = TextBAddItem.Text;

                        string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                        try
                        {
                            cnn.ConnectionString = connectionString;
                            cnn.Open();

                            cmd.Connection = cnn;
                            cmd.CommandText = "SELECT [ItemLookupCode],[Description],Item.[ID],[Price],[Cost],Item.[DepartmentID],[Department].Name" +
                                @",Item.[CategoryID],[Category].Name FROM Item left Join Department on Item.DepartmentID = Department.ID left Join Category on" +
                                @" Item.CategoryID = Category.ID Where [ItemLookupCode] = '" + ic + "'";
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
                                    if (TextBCantAddItem.Text == "")
                                    {
                                        cant = "1";
                                    }
                                    else { cant = TextBCantAddItem.Text; }


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
                                        SqlConnection cnncons = new SqlConnection();
                                        SqlCommand cmdcons = new SqlCommand();
                                        string entryid = "";

                                        cnncons.ConnectionString = connectionString;
                                        cnncons.Open();

                                        cmdcons.Connection = cnncons;
                                        cmdcons.CommandText = "SELECT [ID],[QuantityCounted] FROM PhysicalInventoryEntry where itemid = " + dtr[2].ToString() + " and PhysicalInventoryId = " + piid + " order by ID asc";
                                        SqlDataReader dtrcons = cmdcons.ExecuteReader();

                                        if (dtrcons.HasRows)
                                        {
                                            while (dtrcons.Read())
                                            {
                                                entryid = dtrcons[0].ToString();
                                                oldcant = dtrcons[1].ToString();
                                            }
                                        }

                                        cnn2.ConnectionString = connectionString;
                                        cnn2.Open();

                                        cmd2.Connection = cnn2;

                                        if (entryid == "")
                                        {
                                            cmd2.CommandText = "INSERT INTO [PhysicalInventoryEntry] ([StoreID],[PhysicalInventoryID],[ReasonCodeID],[CountTime]" +
                                            @",[ItemID],[BinLocation],[Price],[Cost],[QuantityCounted],[QuantitySold],[QuantityReturned],[QuantityXferIn]" +
                                            @",[QuantityXferOut],[QuantityAdjusted],[QuantityToOffline],[QuantityFromOffline],[QuantityRefreshed]) VALUES " +
                                            @"(" + sti + "," + piid + ",0,'" + now + "'," + dtr[2].ToString() + ",''," + dtr[3].ToString() + "," + dtr[4].ToString() + "," + TextBCantAddItem.Text + "," +
                                            @"0,0,0,0,0,0,0,0)";

                                            cmd2.ExecuteNonQuery();


                                            SqlConnection cnnitem = new SqlConnection();
                                            SqlCommand cmditem = new SqlCommand();
                                            string lastitem = "";

                                            cnnitem.ConnectionString = connectionString;
                                            cnnitem.Open();

                                            cmditem.Connection = cnnitem;
                                            cmditem.CommandText = "SELECT [ID] FROM PhysicalInventoryEntry order by ID asc";
                                            SqlDataReader dtritem = cmditem.ExecuteReader();

                                            if (dtritem.HasRows)
                                            {
                                                while (dtritem.Read())
                                                {
                                                    lastitem = dtritem[0].ToString();
                                                }
                                            }

                                            CollectionVM.InventoryitemList.Add(new Models.InventoryItemClass()
                                            {
                                                ItemLookupCode = dtr[0].ToString(),
                                                Description = subcadena,
                                                CategoryID = dtr[7].ToString(),
                                                Category = dtr[8].ToString(),
                                                DepartmentID = dtr[5].ToString(),
                                                Department = dtr[6].ToString(),
                                                Counted = TextBCantAddItem.Text,
                                                EntryID = lastitem

                                            });


                                            dtritem.Close();
                                        }
                                        else
                                        {
                                            int qc = 0;
                                            qc = Convert.ToInt16(TextBCantAddItem.Text) + Convert.ToInt16(oldcant);

                                            cmd2.CommandText = "UPDATE [PhysicalInventoryEntry] SET [QuantityCounted] = " + qc + " where id = " + entryid;

                                            cmd2.ExecuteNonQuery();

                                            var item = CollectionVM.InventoryitemList.FirstOrDefault(i => i.ItemLookupCode == dtr[0].ToString());
                                            if (item != null)
                                            {
                                                item.Counted = qc.ToString();
                                            }

                                        };

                                        
                                        TextBAddItem.Text = "";
                                        TextBCantAddItem.Text = "";

                                        TextBAddItem.InputScope = new InputScope()
                                        {
                                            Names = { new InputScopeName(InputScopeNameValue.Default) }
                                        };

                                        TextBAddItem.Focus(FocusState.Programmatic);

                                    }
                                    catch (SqlException ex)
                                    {
                                        ShowDialog("Error en insert" + ex.ToString());
                                    }
                                    cnn2.Close();
                                }
                            }
                            else
                            {
                                String msg;
                                msg = "Scan Barcode does not exist";
                                ShowDialog(msg);

                                TextBAddItem.Text = "";
                                TextBCantAddItem.Text = "";
                                                               
                                TextBAddItem.InputScope = new InputScope()
                                {
                                    Names = { new InputScopeName(InputScopeNameValue.Default) }
                                };
                                
                                TextBAddItem.Focus(FocusState.Programmatic);
                               
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


                    }
                    else if (ft == "New")
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
                        string cant = "";

                        String srv = Class1.GetServer();
                        String srvu = Class1.GetUser();
                        String srvp = Class1.GetPass();
                        String srvdb = Class1.GetDb();
                        string piid = "0";
                        string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
                        string ic = TextBAddItem.Text;

                        string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
                        try
                        {
                            cnn.ConnectionString = connectionString;
                            cnn.Open();

                            cmd.Connection = cnn;
                            cmd.CommandText = "SELECT [ItemLookupCode],[Description],Item.[ID],[Price],[Cost],Item.[DepartmentID],[Department].Name" +
                                @",Item.[CategoryID],[Category].Name FROM Item left Join Department on Item.DepartmentID = Department.ID left Join Category on" +
                                @" Item.CategoryID = Category.ID Where [ItemLookupCode] = '" + ic + "'";
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
                                    if (TextBCantAddItem.Text == "")
                                    {
                                        cant = "1";
                                    }
                                    else { cant = TextBCantAddItem.Text; }


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
                                        SqlConnection cnncons = new SqlConnection();
                                        SqlCommand cmdcons = new SqlCommand();
                                        string entryid = "";

                                        cnncons.ConnectionString = connectionString;
                                        cnncons.Open();

                                        cmdcons.Connection = cnncons;
                                        cmdcons.CommandText = "SELECT [ID],[QuantityCounted] FROM PhysicalInventoryEntry where itemid = " + dtr[2].ToString() + " and PhysicalInventoryId = " + piid + " order by ID asc";
                                        SqlDataReader dtrcons = cmdcons.ExecuteReader();

                                        if (dtrcons.HasRows)
                                        {
                                            while (dtrcons.Read())
                                            {
                                                entryid = dtrcons[0].ToString();
                                                oldcant = dtrcons[1].ToString();
                                            }
                                        }

                                        cnn2.ConnectionString = connectionString;
                                        cnn2.Open();

                                        cmd2.Connection = cnn2;

                                        if (entryid == "")
                                        {
                                            cmd2.CommandText = "INSERT INTO [PhysicalInventoryEntry] ([StoreID],[PhysicalInventoryID],[ReasonCodeID],[CountTime]" +
                                            @",[ItemID],[BinLocation],[Price],[Cost],[QuantityCounted],[QuantitySold],[QuantityReturned],[QuantityXferIn]" +
                                            @",[QuantityXferOut],[QuantityAdjusted],[QuantityToOffline],[QuantityFromOffline],[QuantityRefreshed]) VALUES " +
                                            @"(" + sti + "," + piid + ",0,'" + now + "'," + dtr[2].ToString() + ",''," + dtr[3].ToString() + "," + dtr[4].ToString() + "," + TextBCantAddItem.Text + "," +
                                            @"0,0,0,0,0,0,0,0)";

                                            cmd2.ExecuteNonQuery();


                                            SqlConnection cnnitem = new SqlConnection();
                                            SqlCommand cmditem = new SqlCommand();
                                            string lastitem = "";

                                            cnnitem.ConnectionString = connectionString;
                                            cnnitem.Open();

                                            cmditem.Connection = cnnitem;
                                            cmditem.CommandText = "SELECT [ID] FROM PhysicalInventoryEntry order by ID asc";
                                            SqlDataReader dtritem = cmditem.ExecuteReader();

                                            if (dtritem.HasRows)
                                            {
                                                while (dtritem.Read())
                                                {
                                                    lastitem = dtritem[0].ToString();
                                                }
                                            }

                                            CollectionVM.InventoryitemList.Add(new Models.InventoryItemClass()
                                            {
                                                ItemLookupCode = dtr[0].ToString(),
                                                Description = subcadena,
                                                CategoryID = dtr[7].ToString(),
                                                Category = dtr[8].ToString(),
                                                DepartmentID = dtr[5].ToString(),
                                                Department = dtr[6].ToString(),
                                                Counted = TextBCantAddItem.Text,
                                                EntryID = lastitem

                                            });


                                            dtritem.Close();
                                        }
                                        else
                                        {
                                            int qc = 0;
                                            qc = Convert.ToInt16(TextBCantAddItem.Text) + Convert.ToInt16(oldcant);

                                            cmd2.CommandText = "UPDATE [PhysicalInventoryEntry] SET [QuantityCounted] = " + qc + " where id = " + entryid;

                                            cmd2.ExecuteNonQuery();

                                            var item = CollectionVM.InventoryitemList.FirstOrDefault(i => i.ItemLookupCode == dtr[0].ToString());
                                            if (item != null)
                                            {
                                                item.Counted = qc.ToString();
                                            }

                                        };

                                        TextBAddItem.Text = "";
                                        TextBCantAddItem.Text = "";

                                        TextBAddItem.InputScope = new InputScope()
                                        {
                                            Names = { new InputScopeName(InputScopeNameValue.Default) }
                                        };

                                        TextBAddItem.Focus(FocusState.Programmatic);

                                    }
                                    catch (SqlException ex)
                                    {
                                        ShowDialog("Error en insert" + ex.ToString());
                                    }

                                    cnn2.Close();
                                }
                            }
                            else
                            {
                                String msg;
                                msg = "Scan Barcode does not exist";
                                ShowDialog(msg);

                                TextBAddItem.Text = "";
                                TextBCantAddItem.Text = "";

                                TextBAddItem.InputScope = new InputScope()
                                {
                                    Names = { new InputScopeName(InputScopeNameValue.Default) }
                                };

                                TextBAddItem.Focus(FocusState.Programmatic);
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

                    }

                }
                else
                {
                    String msg;
                    msg = "Fill Barcode Field";
                    ShowDialog(msg);
                }
                TextBAddItem.Text = "";
                TextBCantAddItem.Text = "";
            }
        }

        private void BtnPruebaLaunch_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        
    }
}

