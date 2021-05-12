using SqliteData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URMSApp.Common;
using URMSApp.Models;
using Windows.UI.Popups;

namespace URMSApp.ViewModels
{
    public class CollectionViewModel : BindableBase
    {
        private INVCollection _inventoryList = new INVCollection();

        public INVCollection InventoryList
        {
            get { return _inventoryList; }
            set { SetProperty(ref _inventoryList, value); }
        }


        private InventoryItemCollection _inventoryitemList = new InventoryItemCollection();

        public InventoryItemCollection InventoryitemList
        {
            get { return _inventoryitemList; }
            set { SetProperty(ref _inventoryitemList, value); }
        }


        private POCollection _purchaseList = new POCollection();

        public POCollection PurchaseList
        {
            get { return _purchaseList; }
            set { SetProperty(ref _purchaseList, value); }
        }

        public async void ShowDialog(String msg)
        {

            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }


        private ItemCollection _itemlist = new ItemCollection();

        public ItemCollection ItemList
        {
            get { return _itemlist; }
            set { SetProperty(ref _itemlist, value); }
        }

       
        public CollectionViewModel()
        {
            //Create inventory's view model
            
            string srv = Class1.GetServer();
            string srvu = Class1.GetUser();
            string srvp = Class1.GetPass();
            string srvdb = Class1.GetDb();
            string invi = InventoryClass.GetInventoryId();

            SqlConnection cnninv = new SqlConnection();
            SqlCommand cmdinv = new SqlCommand();
            SqlConnection cnn1inv = new SqlConnection();
            SqlCommand cmd1inv = new SqlCommand();
            

            string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";User Id=" + srvu + ";Password=" + srvp;
            try
            {
                cnninv.ConnectionString = connectionString;
                cnninv.Open();

                cmdinv.Connection = cnninv;
                cmdinv.CommandText = "SELECT ID,Code,Description,OpenTime,Status,StoreID FROM PhysicalInventory where [status] = 0 ORDER BY ID";

                SqlDataReader dtrinv = cmdinv.ExecuteReader();
                DateTime fa;
                if (dtrinv.HasRows)
                {
                    while (dtrinv.Read())
                    {
                        string sta = "";
                        if (dtrinv[4].ToString() == "0") { sta = "Open"; }
                        else
                        {
                            if (dtrinv[4].ToString() == "1") { sta = "Partial"; }
                            else { if (dtrinv[4].ToString() == "2") { sta = "Close"; } }
                        }

                        fa = DateTime.Parse(dtrinv[3].ToString());
                        int faM = fa.Month;
                        int fad = fa.Day;
                        int fay = fa.Year;
                        int fah = fa.Hour;
                        int fam = fa.Minute;
                        int fas = fa.Second;

                        string fech = fay + "-" + faM + "-" + fad + " " + fah + ":" + fam + ":" + fas + ".000";

                        _inventoryList.Add(new ManualInventoryClass()
                        {
                            INVId = dtrinv[0].ToString(),
                            INVDescription = dtrinv[2].ToString(),
                            INVStoreId = dtrinv[5].ToString(),
                            INVCode = dtrinv[1].ToString(),
                            INVDate = fech,
                            INVStatus = sta
                        });
                    }

                }
                else
                {
                    //String msg = "";
                    //msg = "There are no Open Manual Inventory";
                    //ShowDialog(msg);
                }

                dtrinv.Close();
                cnninv.Close();
            }
            catch
            {
                //String msg = "";
                //msg = "Verify Connection";
                //ShowDialog(msg);
            }


            //Create inventory item's view model

            SqlConnection cnninvi = new SqlConnection();
            SqlCommand cmdinvi = new SqlCommand();

            try
            {
                cnninvi.ConnectionString = connectionString;
                cnninvi.Open();

                cmdinvi.Connection = cnninvi;
                cmdinvi.CommandText = "SELECT [ItemLookupCode],[Description],[Item].DepartmentID,[Department].Name,[Item].CategoryID,[Category].Name,[QuantityCounted],PhysicalInventoryEntry.ID" +
                    @" FROM [PhysicalInventoryEntry] left join Item on Item.ID = PhysicalInventoryEntry.ItemID left join Department on Item.DepartmentID = Department.ID" +
                    @" left Join Category on Item.CategoryID = Category.ID WHERE PhysicalInventoryID = '" + invi + "'";

                SqlDataReader dtrinvi = cmdinvi.ExecuteReader();


                if (dtrinvi.HasRows)
                {
                    while (dtrinvi.Read())
                    {
                        _inventoryitemList.Add(new InventoryItemClass()
                        {
                            ItemLookupCode = dtrinvi[0].ToString(),
                            Description = dtrinvi[1].ToString(),
                            DepartmentID = dtrinvi[2].ToString(),
                            Department = dtrinvi[3].ToString(),
                            CategoryID = dtrinvi[4].ToString(),
                            Category = dtrinvi[5].ToString(),
                            Counted = dtrinvi[6].ToString(),
                            EntryID = dtrinvi[7].ToString()
                        });
                    }

                }
                else
                {
                    //String msg = "";
                    //msg = "There are no Purchase Orders";
                    //ShowDialog(msg);
                }

                dtrinvi.Close();
                cnninvi.Close();
            }
            catch
            {
                //String msg = "";
                //msg = "Verify Connection";
                //ShowDialog(msg);
            }


            //Create po's view model
            string poi = PurchaseClass.GetPurchaseId();

            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlConnection cnn1 = new SqlConnection();
            SqlCommand cmd1 = new SqlCommand();

            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();

                cmd.Connection = cnn;
                cmd.CommandText = "SELECT [Status],PurchaseOrder.[ID],[PONumber],[SupplierName],PurchaseOrder.[DateCreated] FROM [PurchaseOrder] left join Supplier on Supplier.ID = PurchaseOrder.SupplierID where [status] = 0 ORDER BY PurchaseOrder.[ID]";

                SqlDataReader dtr = cmd.ExecuteReader();

                DateTime fa;

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        string sta = "";
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

                        _purchaseList.Add(new PurchaseOrderClass()
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
                    //String msg = "";
                    //msg = "There are no Open Purchase Orders";
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


            //Create item's view model

            SqlConnection cnn2 = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand();

            try
            {
                cnn2.ConnectionString = connectionString;
                cnn2.Open();

                cmd2.Connection = cnn2;
                cmd2.CommandText = "SELECT [ItemLookupCode],[ItemDescription],[QuantityOrdered],PurchaseOrderEntry.[Price]," +
                    @"([QuantityOrdered]*PurchaseOrderEntry.[Price]) as ExtCost,[TaxRate] " +
                    @"as Tax, [PurchaseOrderEntry].[ID] FROM [PurchaseOrderEntry] left join Item on Item.ID = PurchaseOrderEntry.ItemID WHERE PurchaseOrderID = '" + poi + "'";

                SqlDataReader dtr2 = cmd2.ExecuteReader();


                if (dtr2.HasRows)
                {
                    while (dtr2.Read())
                    {
                        _itemlist.Add(new Item()
                        {
                            ItemLookupCode = dtr2[0].ToString(),
                            Description = dtr2[1].ToString(),
                            Qtyorder = dtr2[2].ToString(),
                            Cost = Convert.ToDecimal(dtr2[3].ToString()).ToString("0.00"),
                            Extcost = Convert.ToDecimal(dtr2[4].ToString()).ToString("0.00"),
                            Tax = Convert.ToDecimal(dtr2[5].ToString()).ToString("0.00"),
                            IPOid = dtr2[6].ToString()
                            
                        });
                    }

                }
                else
                {
                    //String msg = "";
                    //msg = "There are no Purchase Orders";
                    //ShowDialog(msg);
                }

                dtr2.Close();
                cnn.Close();
            }
            catch
            {
                //String msg = "";
                //msg = "Verify Connection";
                //ShowDialog(msg);
            }

        }
    }
}
