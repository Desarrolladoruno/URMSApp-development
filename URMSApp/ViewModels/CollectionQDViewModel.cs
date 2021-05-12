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
    public class CollectionQDViewModel:BindableBase
    {
        private QuantityDiscountCollection _quantityList = new QuantityDiscountCollection();
        public QuantityDiscountCollection QuantityList
        {
            get { return _quantityList; }
            set { SetProperty(ref _quantityList, value); }
        }
        public async void ShowDialog(String msg)
        {

            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }
        public CollectionQDViewModel()
        {
            string qdi = QuantityDiscountClass.GetQuantityId();

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
                cmd.CommandText = "SELECT [Description],[HQID],[ID],[DiscountOddItems],[Type],[Quantity1],[Price1],[Price1A],[Price1B],[Price1C]" +
                ",[Quantity2],[Price2],[Price2A],[Price2B],[Price2C],[Quantity3],[Price3],[Price3A],[Price3B],[Price3C]" +
                ",[Quantity4],[Price4],[Price4A],[Price4B],[Price4C],[PercentOffPrice1],[PercentOffPrice1A]" +
                ",[PercentOffPrice1B],[PercentOffPrice1C],[PercentOffPrice2],[PercentOffPrice2A],[PercentOffPrice2B]" +
                ",[PercentOffPrice2C],[PercentOffPrice3],[PercentOffPrice3A],[PercentOffPrice3B],[PercentOffPrice3C],[PercentOffPrice4]" +
                ",[PercentOffPrice4A],[PercentOffPrice4B],[PercentOffPrice4C] FROM [QuantityDiscount]";

                SqlDataReader dtr = cmd.ExecuteReader();


                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        _quantityList.Add(new QuantityDiscounts()
                        {
                            Description = dtr[0].ToString(),
                            HQID = dtr[1].ToString(),
                            ID = dtr[2].ToString(),
                            DiscountOddItems = dtr[3].ToString(),
                            Type = dtr[4].ToString(),
                            Quantity1 = dtr[5].ToString(),
                            Price1=dtr[6].ToString(),
                            Price1A=dtr[7].ToString(),
                            Price1B=dtr[8].ToString(),
                            Price1C=dtr[9].ToString(),
                            Quantity2=dtr[10].ToString(),
                            Price2=dtr[11].ToString(),
                            Price2A=dtr[12].ToString(),
                            Price2B=dtr[13].ToString(),
                            Price2C=dtr[14].ToString(),
                            Quantity3=dtr[15].ToString(),
                            Price3=dtr[16].ToString(),
                            Price3A=dtr[17].ToString(),
                            Price3B=dtr[18].ToString(),
                            Price3C=dtr[19].ToString(),
                            Quantity4=dtr[20].ToString(),
                            Price4=dtr[21].ToString(),
                            Price4A=dtr[22].ToString(),
                            Price4B=dtr[23].ToString(),
                            Price4C=dtr[24].ToString(),
                            PercentOffPrice1=dtr[25].ToString(),
                            PercentOffPrice1A=dtr[26].ToString(),
                            PercentOffPrice1B=dtr[27].ToString(),
                            PercentOffPrice1C=dtr[28].ToString(),
                            PercentOffPrice2=dtr[29].ToString(),
                            PercentOffPrice2A=dtr[30].ToString(),
                            PercentOffPrice2B=dtr[31].ToString(),
                            PercentOffPrice2C=dtr[32].ToString(),
                            PercentOffPrice3=dtr[33].ToString(),
                            PercentOffPrice3A=dtr[34].ToString(),
                            PercentOffPrice3B=dtr[35].ToString(),
                            PercentOffPrice3C=dtr[36].ToString(),
                            PercentOffPrice4=dtr[37].ToString(),
                            PercentOffPrice4A=dtr[38].ToString(),
                            PercentOffPrice4B=dtr[39].ToString(),
                            PercentOffPrice4C=dtr[40].ToString()
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
