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
    public class CollectionItemViewModel:BindableBase
    {
        private ItemCollection _itemlist = new ItemCollection();

        public ItemCollection ItemList
        {
            get { return _itemlist; }
            set { SetProperty(ref _itemlist, value); }
        }

        public async void ShowDialog(String msg)
        {

            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }

        public CollectionItemViewModel()
        {
            string poi = PurchaseClass.GetPurchaseId();
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
                cmd.CommandText = "SELECT [ItemLookupCode],[ItemDescription],[QuantityOrdered],PurchaseOrderEntry.[Price]," +
                    @"([QuantityOrdered]*PurchaseOrderEntry.[Price]) as ExtCost,[TaxRate] " +
                    @"as Tax, [PurchaseOrderEntry].[ID] FROM [PurchaseOrderEntry] left join Item on Item.ID = PurchaseOrderEntry.ItemID WHERE PurchaseOrderID = '" + poi +"'";

                SqlDataReader dtr = cmd.ExecuteReader();

               
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        _itemlist.Add(new Item()
                        {
                            ItemLookupCode = dtr[0].ToString(),
                            Description = dtr[1].ToString(),
                            Qtyorder = dtr[2].ToString(),
                            Cost = dtr[3].ToString(),
                            Extcost = dtr[4].ToString(),
                            Tax = dtr[5].ToString(),
                            IPOid = dtr[6].ToString()
                        });
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


        }
    }
}
