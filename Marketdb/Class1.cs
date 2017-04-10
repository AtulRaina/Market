using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using MarketLogger;
using System.Data;

namespace MarketDb
{
    public class MarketDatabaseOperations
    {
      static  SQLiteConnection m_dbConnection;
        public MarketDatabaseOperations()
        {
            CreateDatabase();
            ConnectToDataBase();
            DatabaseCreateOperationLTables();
        }
     
        public MarketDatabaseOperations(string dummy)
        {
            // this is created to avoid no operations
        }
        void CreateDatabase()
        {
            if (File.Exists(@"MarketDataBaseFile.sqlite"))
            {
                File.Delete(@"MarketDataBaseFile.sqlite");
                SQLiteConnection.CreateFile("MarketDataBaseFile.sqlite");
            }
           
        }

        void ConnectToDataBase()
        {

            m_dbConnection = new SQLiteConnection("Data Source=MarketDataBaseFile.sqlite;Version=3;");
            m_dbConnection.Open();
        }
        void DatabaseCreateOperationLTables()
        {
            string createMasterProductTable = "create table MasterProductTable(productid INTEGER PRIMARY KEY,MasterProductName string)";
            string createPriceTable = "create table PriceTable(id INTEGER PRIMARY KEY,vendorid string,productid string,productname string,price string,sellerranking string)";
            string createVendorIdTable = "create table VendorTable(id INTEGER PRIMARY KEY,VendorName string)";
            DataBaseExecuteCommand(createMasterProductTable);
            DataBaseExecuteCommand(createPriceTable);
            DataBaseExecuteCommand(createVendorIdTable);
            m_dbConnection.Close();
        }

     public   void DataBaseExecuteCommand(string commandToExecute)
        {
            ConnectToDataBase();
            Logger.filePath = @"MarketLog.log";
            try
            {
                ConnectToDataBase();
                Logger.log(commandToExecute);
                SQLiteCommand command = new SQLiteCommand(commandToExecute, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
            }
            catch (SQLiteException)
            {

                
            }

           

        }

    public  DataTable DataBaseGetResults(string commandToExecute)
        {
         DataTable dt = new DataTable();
            try
            {
                ConnectToDataBase();
               
                SQLiteCommand command = new SQLiteCommand(commandToExecute, m_dbConnection);

                SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                da.Fill(dt);
                m_dbConnection.Close();
                return dt;
            }
            catch (Exception)
            {

                return dt;
            }
           finally
            {
               
            }
            
        }
    }
}
