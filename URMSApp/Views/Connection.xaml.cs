using Microsoft.Data.Sqlite;
using System.ComponentModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Globalization;
using Windows.UI.Popups;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZXing.Mobile;
using System.Collections.ObjectModel;
using SqliteData;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace URMSApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// Confirm if saved connection parameters are valids.
    /// </summary>
    public sealed partial class Connection : Page
    {
        public Connection()
        {
            this.InitializeComponent();


            //SqlCommand cmd1 = new SqlCommand();
            //SqlConnection cnn1 = new SqlConnection();

            //string srv = Class1.GetServer();
            //string srvu = Class1.GetUser();
            //string srvp = Class1.GetPass();
            //string srvdb = Class1.GetDb();

            //if (srv != "" & srvu != "" & srvp != "" & srvdb != "") {
            //    String connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";Integrated Security=False;Persist Security Info=True;User Id=" + srvu + ";Password=" + srvp;

            //    try
            //    {
            //        cnn1.ConnectionString = connectionString;
            //        cnn1.Open();
            //        txtInput_Server.Text = srv;
            //        Input_User.Text = srvu;
            //        Input_Pass.Password = srvp;
            //        Input_Db.Text = srvdb;

            //    }
            //    catch
            //    {
            //        string msg = "Establish a valid connection";
            //        ShowDialog(msg);
            //    }
            //}   
        }

        /// <summary>
        /// Button ConnectSQL's Click Method to verify connection parameters
        /// setting for the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectSQL(object sender, RoutedEventArgs e)
        {
            SqlConnection cnn = new SqlConnection();

            String pass = Input_Pass.Password.ToString();

            //String connectionString = "Data Source="+ txtInput_Server.Text +";Initial Catalog="+ Input_Db.Text + ";Integrated Security=False;User ID=" + Input_User.Text +";Password=" + pass;
            String connectionString = @"Data Source=" + txtInput_Server.Text + ";Initial Catalog=" + Input_Db.Text + ";User ID=" + Input_User.Text + ";Password=" + pass + ";Application Name=URMSApp_5ba67krkkkhdw";
            try
            {
                cnn.ConnectionString = connectionString;
                cnn.Open();
                String msg = "Connection has been stablished";
                ShowDialog(msg);
            }
            catch (SqlException ex) {
                String msg = "Connection Error, " + ex.ToString();
                ShowDialog(msg);
                Input_Pass.Password = "";
                Input_Db.Text = "";
                Input_User.Text = "";
                txtInput_Server.Text = "";

            }
        }

        /// <summary>
        /// Clear connection parameters to the page and the 
        /// embedded database - sqlite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearCon_Click(object sender, RoutedEventArgs e)
        {
            txtInput_Server.Text = "";
            Input_User.Text = "";
            Input_Pass.Password = "";
            Input_Db.Text = "";

            Class1.AddData("", "", "", "");
        }

        /// <summary>
        /// Button save's Click method
        /// Save connection parameters to embedded sqlite
        /// for next ussage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Onclick_save(object sender, RoutedEventArgs e)
        {
            //txtInput_Server.Text
            //Input_User.Text
            
            String msg;
                        
            String srv = txtInput_Server.Text;
            String srvu = Input_User.Text;
            String srvp = Input_Pass.Password.ToString();
            String srvdb = Input_Db.Text;

            
            SqlConnection cnnT = new SqlConnection();
            
            string connectionString = "Data Source=" + srv + ";Initial Catalog=" + srvdb + ";Integrated Security=False;Persist Security Info=True;User Id=" + srvu + ";Password=" + srvp;
            
            try
            {
                cnnT.ConnectionString = connectionString;
                cnnT.Open();
                
               cnnT.Close();
                         
                                
                String pas = Input_Pass.Password.ToString();
                Class1.AddData(txtInput_Server.Text, Input_User.Text, pas, Input_Db.Text);

                msg = "Server Configuration Saved";
                ShowDialog(msg);
            }
            catch {
                msg = "Wrong Server Configuration";
                ShowDialog(msg);
            }


            
        }

        /// <summary>
        /// Asyncronic method to show string messages 
        /// </summary>
        /// <param name="msg">message to show in popup</param>
         public async void ShowDialog(string msg) {
            MessageDialog md = new MessageDialog(msg);
            await md.ShowAsync();
        }
    }
}
