using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MarketDb;
using MarketLogger;

namespace MarketCore
{
   public class PageController
    {
        string urlname;
        bool fetchFlag = false;
        public PageController(string urlName)
        {
            urlname = urlName;
        }
        public PageController(string urlName, bool flag)
        {
            urlname = urlName;
            fetchFlag = flag;
        }

    public    void cratingMaster()
        {
            int num=0;
            if (this.urlname.Contains("amazon"))
                num = 1;
            if (this.urlname.Contains("bestbuy"))
                num = 2;
            if (this.urlname.Contains("walmart"))
                num = 3;
            if (this.urlname.Contains("kmart"))
                num = 4;
            if (this.urlname.Contains("disney"))
                num = 5;
            if (this.urlname.Contains("target"))
                num = 6;
            if (this.urlname.Contains("homedepot"))
                num = 7;
            if (this.urlname.Contains("overstock"))
                num = 8;
            if (this.urlname.Contains("aliexpress"))
                num = 9;
            if (this.urlname.Contains("costco"))
                num = 10;
            switch (num)
                     {
                         case 1:
                             Logger.log("-----------------------------------");
                             Logger.log("----Master Amazon------");
                             Logger.log("-----------------------------------");
                              Amazon amazon = new Amazon(urlname);
                                 amazon.createmasterlist();
                             break;

                         case 2:

                               Logger.log("-----------------------------------");
                             Logger.log("----Master Bestbuy-----");
                             Logger.log("-----------------------------------");
                             BestBuy bestBuy = new BestBuy(urlname);
                             bestBuy.createmasterlist();
                             break;
                         case 3:

                               Logger.log("-----------------------------------");
                             Logger.log("----Master Wallmart-----");
                             Logger.log("-----------------------------------");
                             WallMart wallMart = new WallMart(urlname);
                             wallMart.createmasterlist();
                             break;

                         case 4:
                                 Logger.log("-----------------------------------");
                             Logger.log("----Master Kmart-----");
                             Logger.log("-----------------------------------");

                             Kmart kmart = new Kmart(urlname);
                             kmart.createmasterlist();
                             break;

                         case 5:

                                 Logger.log("-----------------------------------");
                             Logger.log("----Master Disney----");
                             Logger.log("-----------------------------------");
                             Disney disney = new Disney(urlname);
                             disney.createmasterlist();
                             break;
                      

                         case 6:

                                 Logger.log("-----------------------------------");
                             Logger.log("----Master Target----");
                             Logger.log("-----------------------------------");
                             Target target = new Target(urlname);
                             target.createmasterlist();
                             break;

                         case 7:

                                 Logger.log("-----------------------------------");
                             Logger.log("----Master HomeDepot-----");
                             Logger.log("-----------------------------------");
                             HomeDepot homeDepot = new HomeDepot(urlname);
                             homeDepot.createmasterlist();
                             break;

                case 8:

                    Logger.log("-----------------------------------");
                    Logger.log("----Master OVER STOCK-----");
                    Logger.log("-----------------------------------");
                    OverStock overstock = new OverStock(urlname);
                    overstock.createmasterlist();
                    break;

                case 9:

                    Logger.log("-----------------------------------");
                    Logger.log("----Master Ali express----");
                    Logger.log("-----------------------------------");
                      AliExpress ali = new AliExpress(urlname);
                    ali.createmasterlist();
                    break;

                case 10:

                    Logger.log("-----------------------------------");
                    Logger.log("----Master Ali express----");
                    Logger.log("-----------------------------------");
                    Costco costco= new Costco(urlname);
                    costco.createmasterlist();

                    break;

                default:
                             break;


            }       
                     
	
                
        }

     public   void creatingProduct()
        {
            MarketDatabaseOperations db = new MarketDatabaseOperations("No Operations");
            string insertMasterRecordsQuery = @"select * from MasterProductTable";
            // db.DataBaseExecuteCommand(insertMasterRecordsQuery);
            DataTable records = new DataTable();
            records = db.DataBaseGetResults(insertMasterRecordsQuery);
            int num = 0;
            if (this.urlname.Contains("amazon"))
                num = 1;
            if (this.urlname.Contains("bestbuy"))
                num = 2;
            if (this.urlname.Contains("walmart"))
                num = 3;
            if (this.urlname.Contains("kmart"))
                num = 4;
            if (this.urlname.Contains("disney"))
                num = 5;
            if (this.urlname.Contains("target"))
                num = 6;
            if (this.urlname.Contains("homedepot"))
                num = 7;
            if (this.urlname.Contains("overstock"))
                num = 8;
            if (this.urlname.Contains("AliExpres"))
                num = 9;
            if (this.urlname.Contains("costco"))
                num = 10;
            switch (num)
            {
                case 1:
                        Logger.log("-----------------------------------");
                             Logger.log("----Search Amazon-----");
                             Logger.log("-----------------------------------");
                    Amazon amazon = new Amazon(fetchFlag);
                    Logger.log("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxamazonxxxxxxxxxxxxxxxxxxx");
                    for (int i = 0; i < records.Rows.Count; i++)
                    {
                        
                        amazon.searchProducts(records.Rows[i]["MasterProductName"].ToString());
                    }
                    amazon.closeWebDriver();
                    break;

                case 2:

                     Logger.log("-----------------------------------");
                      Logger.log("----Search bestbuy-----");
                      Logger.log("-----------------------------------");
                   BestBuy bestbuy = new BestBuy();
                   for (int i = 0; i < records.Rows.Count; i++)
                   {
                       bestbuy.searchProducts(records.Rows[i]["MasterProductName"].ToString());
                   }
                   bestbuy.closeWebDriver();
     
                    break;

                case 3:

                     Logger.log("-----------------------------------");
                             Logger.log("----Search wallmart-----");
                             Logger.log("-----------------------------------");
                     WallMart wallMart = new WallMart();
                   for (int i = 0; i < records.Rows.Count; i++)
                   {
                       wallMart.searchProducts(records.Rows[i]["MasterProductName"].ToString());
                   }
                   wallMart.closeWebDriver();

                    break;

                case 4:

                     Logger.log("-----------------------------------");
                             Logger.log("----Search Kmart----");
                             Logger.log("-----------------------------------");
                    Kmart kmart = new Kmart();
                   for (int i = 0; i < records.Rows.Count; i++)
                   {
                       kmart.searchProducts(records.Rows[i]["MasterProductName"].ToString());
                   }
                   kmart.closeWebDriver();

                    break;
                 case 5:

                     Logger.log("-----------------------------------");
                             Logger.log("----Search disney----");
                             Logger.log("-----------------------------------");
                  Disney disney = new Disney();
                    for (int i = 0; i < records.Rows.Count; i++)
                    {
                        disney.searchProducts(records.Rows[i]["MasterProductName"].ToString());
                    }
                   disney.closeWebDriver();
                   break;

                 case 6:

                     Logger.log("-----------------------------------");
                             Logger.log("----Search Target-----");
                             Logger.log("-----------------------------------");
                   Target target = new Target();
                   for (int i = 0; i < records.Rows.Count; i++)
                   {
                       target.searchProducts(records.Rows[i]["MasterProductName"].ToString());
                   }
                   target.closeWebDriver();
                   break;

                 case 7:

                     Logger.log("-----------------------------------");
                             Logger.log("----Search HomeDepot----");
                             Logger.log("-----------------------------------");
                   HomeDepot homeDepot= new HomeDepot();
                   for (int i = 0; i < records.Rows.Count; i++)
                   {
                       homeDepot.searchProducts(records.Rows[i]["MasterProductName"].ToString());
                   }
                   homeDepot.closeWebDriver();
                   break;


                case 8:

                    Logger.log("-----------------------------------");
                    Logger.log("----Search Overstock----");
                    Logger.log("-----------------------------------");
                     OverStock costco = new OverStock();
                    for (int i = 0; i < records.Rows.Count; i++)
                    {
                        costco.searchProducts(records.Rows[i]["MasterProductName"].ToString());
                    }
                    costco.closeWebDriver();
                    break;

                case 9:

                    Logger.log("-----------------------------------");
                    Logger.log("----Search Ali Express----");
                    Logger.log("-----------------------------------");
                    AliExpress ali = new AliExpress();
                    for (int i = 0; i < records.Rows.Count; i++)
                    {
                        ali.searchProducts(records.Rows[i]["MasterProductName"].ToString());
                    }
                    ali.closeWebDriver();
                    break;

                case 10:

                    Logger.log("-----------------------------------");
                    Logger.log("----Search costco----");
                    Logger.log("-----------------------------------");
                    Costco atul = new Costco();
                    for (int i = 0; i < records.Rows.Count; i++)
                    {
                        atul.searchProducts(records.Rows[i]["MasterProductName"].ToString());
                    }
                    atul.closeWebDriver();
                    break;
                default:
                  
                    break;
            }       

        }
    }
}
