using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketDb;

namespace MarketCore
{
  public  class MarektPriceUpdater
    {



      public  void priceTableUpdate(string vendorid,string productid,string searchProudctName,string searchProductPrice)
        {
          // some time in product list comes ' which bongs every thing
            string removeDollarFromString = searchProductPrice.Replace("$", "").Replace("US","").Replace("Today:","").Replace("Sale:","");
            string pricetoinsert = removeDollarFromString;
            int l = removeDollarFromString.IndexOf("-");
            if (l > 0)
            {
              pricetoinsert=removeDollarFromString.Substring(0, l);
            }

            string tempProductName = searchProudctName.Replace("'", "");
            string tempprd = productid.Replace("'", "");
            string tempMasterRecordsQuery = @"Insert into PriceTable (vendorid,productid,productname,price) values ('vendoridto','productidto','productnameto','priceto')";
            MarketDatabaseOperations db = new MarketDatabaseOperations("No operations");
            string insertMasterRecordsQuery1 = tempMasterRecordsQuery.Replace("vendoridto", vendorid);
            string insertMasterRecordsQuery2 = insertMasterRecordsQuery1.Replace("productidto", tempprd);
            string insertMasterRecordsQuery3 = insertMasterRecordsQuery2.Replace("productnameto", tempProductName);
            string insertMasterRecordsQuery4 = insertMasterRecordsQuery3.Replace("priceto", pricetoinsert);

            if (!insertMasterRecordsQuery4.Contains("Exception"))
                db.DataBaseExecuteCommand(insertMasterRecordsQuery4);

        }

      public void priceTableUpdate(string vendorid, string productid, string searchProudctName, string searchProductPrice,string selleranking)
      {
            // some time in product list comes ' which bongs every thing
            string removeDollarFromString = searchProductPrice.Replace("$", "").Replace("US", "").Replace("Today:", "").Replace("Sale:", "");
            string pricetoinsert = removeDollarFromString;
          int l = removeDollarFromString.IndexOf("-");
          if (l > 0)
          {
              pricetoinsert = removeDollarFromString.Substring(0, l);
          }
          string tempProductName = searchProudctName.Replace("'", "");
          string tempprd = productid.Replace("'", "");
          string tempMasterRecordsQuery = @"Insert into PriceTable (vendorid,productid,productname,price,sellerranking) values ('vendoridto','productidto','productnameto','priceto','sellerrankings')";
          MarketDatabaseOperations db = new MarketDatabaseOperations("No operations");
          string insertMasterRecordsQuery1 = tempMasterRecordsQuery.Replace("vendoridto", vendorid);
          string insertMasterRecordsQuery2 = insertMasterRecordsQuery1.Replace("productidto", tempprd);
          string insertMasterRecordsQuery3 = insertMasterRecordsQuery2.Replace("productnameto", tempProductName);
          string insertMasterRecordsQuery4 = insertMasterRecordsQuery3.Replace("priceto", pricetoinsert);
          string insertMasterRecordsQuery5 = insertMasterRecordsQuery4.Replace("sellerrankings", selleranking);
         // if (!insertMasterRecordsQuery4.Contains("Exception"))
              db.DataBaseExecuteCommand(insertMasterRecordsQuery5);

      }

     public void MasterProductUpdater(string masterProductName)
      {
                 MarketDatabaseOperations db = new MarketDatabaseOperations("No Operations");
                 string tempMasterRecordsQuery = @"Insert into MasterProductTable (MasterProductName) values ('+item.masterproductName+')";
                //@"Insert into MasterProductTable (MasterProductName) values ('+item.masterproductName+')";
                 string temproductName=masterProductName.Replace("'","");
                string insertMasterRecordsQuery = tempMasterRecordsQuery.Replace("+item.masterproductName+", temproductName);
                if(!insertMasterRecordsQuery.Contains("Exception"))
                db.DataBaseExecuteCommand(insertMasterRecordsQuery);


      }

    }
}
