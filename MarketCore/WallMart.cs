﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MarketDb;
using MarketLogger;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MarketCore
{
    class WallMart
    {

         private IWebDriver iwebdriver;
        public string wallMartSearchBoxControl { get; set; }
        public string wallMartSearchBoxClick {get;set; }
        public string wallMartProductNameControl { get; set; }
        public string wallMartProductPriceControl { get; set; }
        public string wallMartMasterProductNameControl { get; set; }
        public string wallMartMasterProductPriceControl { get; set; }
        public string pageUrl { get; set; }
        public string pagelenght { get; set; }
        public List<SearchResults> wallMartSearchResults= new List<SearchResults>();
        public List<MasterProductList> wallMartMasterProductList = new List<MasterProductList>();
       
        public WallMart(string url)
        {
        // url comes from the ui

            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("WallMart");
            wallMartSearchBoxControl = mCoreControlReader.searchButton;
            wallMartSearchBoxClick = mCoreControlReader.searchClick;
            wallMartProductNameControl = mCoreControlReader.productName;
            wallMartProductPriceControl = mCoreControlReader.productPrice;
            wallMartMasterProductNameControl = mCoreControlReader.productMasterName;
            wallMartMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            pagelenght = mCoreControlReader.pageLength;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(url);
             Logger.filePath = @"MarketLog.log";
        }



        public WallMart()
        {
            // url comes from the ui
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("WallMart");
            wallMartSearchBoxControl = mCoreControlReader.searchButton;
            wallMartSearchBoxClick = mCoreControlReader.searchClick;
            wallMartProductNameControl = mCoreControlReader.productName;
            wallMartProductPriceControl = mCoreControlReader.productPrice;
            wallMartMasterProductNameControl = mCoreControlReader.productMasterName;
            wallMartMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(mCoreControlReader.pageUrl);
            Logger.filePath = @"MarketLog.log";
        }
        ~WallMart()
        {
         //   iwebdriver.Quit();
        }
        // name is going to come from the db 
        /* control the search bar with product */
        /* get results name and price */
        //------------------------------------------------------------------------------
        /// <summary>
        ///  each contol should be individual function for reuse
        ///  then can be called in the method
        ///  // xml 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool actionEnterProductName(string name)
        {
            try
            {
                var findsearchbox = iwebdriver.FindElement(By.Id(this.wallMartSearchBoxControl));
                findsearchbox.Clear();
                findsearchbox.SendKeys(name);

                System.Threading.Thread.Sleep(1000);
                findsearchbox.SendKeys(Keys.Enter);
                System.Threading.Thread.Sleep(1000);
            }
            catch (NoSuchElementException)
            {
                try
                {
                    var findsearchbox = iwebdriver.FindElement(By.Id("search"));
                    findsearchbox.Clear();
                    findsearchbox.SendKeys(name);
                //    System.Threading.Thread.Sleep(1000);
                    findsearchbox.SendKeys(Keys.Enter);
                   System.Threading.Thread.Sleep(1000);
                }
                catch (NoSuchElementException)
                {
                    
               
                }
         
              
            }

            
          
            return true;
        }

        void actionClickSearchBox()
        {
            try
            {
                bool checkForthepopupwindow = iwebdriver.FindElement(By.CssSelector("#abt-email-modal > div > div > div.modal-header ")).Displayed;
                if (checkForthepopupwindow)
                {
                    var clickTheWindow = iwebdriver.FindElement(By.CssSelector("#abt-email-modal > div > div > div.modal-header > button > span:nth-child(1)"));
                    clickTheWindow.Click();
                }
            }
                catch (TimeoutException)
            {

                }
            catch (NoSuchElementException)
            {


            }
            finally
            {
             //   var clicksearch = iwebdriver.FindElement(By.CssSelector(this.wallMartSearchBoxClick));
              //  clicksearch.Click();
            }
           
        }

        string getProductNameFromSearchResults()
        {

            try
            {
                var resultTitle = iwebdriver.FindElement(By.CssSelector(this.wallMartProductNameControl));
               
                return resultTitle.Text;
                
            }
            catch (NoSuchElementException)
            {

             return "Exception Product Name";
            }
         
        }
        string getProductPrice()
        {
            try
            {
                var price = iwebdriver.FindElement(By.CssSelector(this.wallMartProductPriceControl));

                return (price.Text.Split('.')[0]);
            }
            catch (NoSuchElementException)
            {

                return "Excpetion In Price";
            }
        }
        

      public  bool searchProducts(string name)
        {
           
            actionEnterProductName(name);
            //actionClickSearchBox();
           
        

            SearchResults tempSearchResult = new SearchResults(name,getProductNameFromSearchResults(),getProductPrice());
         /* Now call the Page functions above */

            wallMartSearchResults.Add(tempSearchResult);

            //@"PriceTable(id INT PRIMARY KEY,vendorid int,productid int,productname string,price string)";')";
            MarektPriceUpdater obj = new MarektPriceUpdater();
            obj.priceTableUpdate("WallMart", tempSearchResult.searchMasterString, tempSearchResult.searchResultName, tempSearchResult.searchResultPrice);
            Logger.log("--------------------"); 
            Logger.log(name);
            Logger.log("--------------------"); 
            Logger.log(tempSearchResult.searchResultName);
            Logger.log("--------------------"); 
          return true;
        }

        bool getProducts()
        {
            /* get products name and price from the grid view */
            return true;
        }
        string getmasterProductname(string temp)
        {
            try
            {
                var resultTitle = iwebdriver.FindElement(By.CssSelector(temp));
                Logger.log(resultTitle.Text);
                return resultTitle.Text;

            }
            catch (NoSuchElementException)
            {

                return "Exception Product Name";
            }
        }
        string getMasterProductPrice(string tempr)
        {
            try
            {
                var resultTitle = iwebdriver.FindElement(By.CssSelector(tempr));
                Logger.log(resultTitle.Text);
                //s.Split('-')[0]);
                return (resultTitle.Text.Split('.')[0]);

            }
            catch (NoSuchElementException)
            {
                try
                {
                    string replace = tempr.Replace("child(5)", "child(3)");
                    var resultTitle = iwebdriver.FindElement(By.CssSelector(tempr));
                    Logger.log(resultTitle.Text);
                    return resultTitle.Text;
                }
                catch (NoSuchElementException)
                {

                    return "Exception Product price";
                }
                
           
            }
        }

        public void createmasterlist()
        {
            // first determine the loop
            //pass the control from outside aftergeneration
             // add the returned to the mastertable
           /*
            var determineLoopCounter = iwebdriver.FindElement(By.XPath(this.pagelenght));
            string temp = determineLoopCounter.Text;
            int pFrom = determineLoopCounter.Text.IndexOf("1 -") + "1 - ".Length;
            int pTo = determineLoopCounter.Text.LastIndexOf("of");
            String result = determineLoopCounter.Text.Substring(pFrom, pTo - pFrom);
            int loopvalue = Convert.ToInt32(result);
            * */
            for (int i = 1; i < Convert.ToInt32(this.pagelenght); i++)
            {
                string newProductLink = this.wallMartMasterProductNameControl.Replace("child(x)", "child(" + i+")");
                string newPriceLink = this.wallMartMasterProductPriceControl.Replace("child(x)", "child(" + i+")");
                MasterProductList mp = new MasterProductList(i, getmasterProductname(newProductLink), getMasterProductPrice(newPriceLink));
                wallMartMasterProductList.Add(mp);

            }


            MarketDatabaseOperations db = new MarketDatabaseOperations();
            
                //productid Int PRIMARY KEY,MasterProductName string
            foreach (var item in wallMartMasterProductList)
            {
              

                /* its also good idea to insert it into the price table */

                MarektPriceUpdater obj = new MarektPriceUpdater();
                obj.MasterProductUpdater(item.masterproductName);
                obj.priceTableUpdate("WallMart", item.masterproductName, item.masterproductName, item.masterproductPrice);
                
            }
           
            closeWebDriver();
        }

  
        
        public  void closeWebDriver()
        {
            iwebdriver.Quit();
        }

        public DataTable bringMasterRecords()
        {
          
            DataTable temp = new DataTable();
            /*
            temp.Columns.Add("Product id");
            temp.Columns.Add("Product Name");
         
            
            string getRecords = @"select * MasterProductTable";
            while (db.DataBaseGetResults(getRecords).read())
            {
                DataRow toInsert = temp.NewRow();
                toInsert[0] = 
                toInsert[1] = "ProductName";
                temp.Rows.Add(toInsert);
            }
            */
            return temp;
        }
    }
}
