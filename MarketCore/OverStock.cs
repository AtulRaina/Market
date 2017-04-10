using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketDb;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MarketCore
{
    public class OverStock
    {
        // as of now i think to pass the control from outside 
        // as if their id changes i dont have to recompile the whole stuff 
        private IWebDriver iwebdriver;
        public string OverStockSearchBoxControl { get; set; }
        public string OverStockSearchBoxClick { get; set; }
        public string OverStockProductNameControl { get; set; }
        public string OverStockProductPriceControl { get; set; }
        public string OverStockMasterProductNameControl { get; set; }
        public string OverStockMasterProductPriceControl { get; set; }
        public string pageUrl { get; set; }
        public string pagelenght { get; set; }
        public List<SearchResults> OverStockSearchResults = new List<SearchResults>();
        public List<MasterProductList> OverStockMasterProductList = new List<MasterProductList>();

        public OverStock(string url)
        {
            // url comes from the ui

            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("OverStock");
            OverStockSearchBoxControl = mCoreControlReader.searchButton;
            OverStockSearchBoxClick = mCoreControlReader.searchClick;
            OverStockProductNameControl = mCoreControlReader.productName;
            OverStockProductPriceControl = mCoreControlReader.productPrice;
            OverStockMasterProductNameControl = mCoreControlReader.productMasterName;
            OverStockMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            pagelenght = mCoreControlReader.pageLength;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(url);
           // iwebdriver.Navigate().GoToUrl(url);
        }



        public OverStock()
        {
            // url comes from the ui
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("OverStock");
            OverStockSearchBoxControl = mCoreControlReader.searchButton;
            OverStockSearchBoxClick = mCoreControlReader.searchClick;
            OverStockProductNameControl = mCoreControlReader.productName;
            OverStockProductPriceControl = mCoreControlReader.productPrice;
            OverStockMasterProductNameControl = mCoreControlReader.productMasterName;
            OverStockMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(mCoreControlReader.pageUrl);
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
                var findsearchbox = iwebdriver.FindElement(By.Id(this.OverStockSearchBoxControl));
                findsearchbox.Clear();
                findsearchbox.SendKeys(name);
                findsearchbox.SendKeys(Keys.Enter);
            }
            catch (NoSuchElementException)
            {


            }

            return true;
        }

    

        string getProductNameFromSearchResults()
        {

            try
            {
                var resultTitle = iwebdriver.FindElement(By.XPath(this.OverStockProductNameControl));

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
                var price = iwebdriver.FindElement(By.XPath(this.OverStockProductPriceControl));

                return price.Text;
            }
            catch (NoSuchElementException)
            {

                return "Excpetion In Price";
            }
        }


        public bool searchProducts(string name)
        {

            actionEnterProductName(name);
            //  actionClickSearchBox();
            SearchResults tempSearchResult = new SearchResults(name, getProductNameFromSearchResults(), getProductPrice());
            OverStockSearchResults.Add(tempSearchResult);
            MarektPriceUpdater obj = new MarektPriceUpdater();
            obj.priceTableUpdate("OverStock", name, tempSearchResult.searchResultName, tempSearchResult.searchResultPrice);

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
                var resultTitle = iwebdriver.FindElement(By.XPath(temp));

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
                var resultTitle = iwebdriver.FindElement(By.XPath(tempr));

                return resultTitle.Text;

            }
            catch (NoSuchElementException)
            {

                return "Exception Product price";
            }
        }

        public void createmasterlist()
        {

            for (int i = 1; i < Convert.ToInt32(this.pagelenght); i++)
            {
                string newProductLink = this.OverStockMasterProductNameControl.Replace("replace",i.ToString());
                string newPriceLink = this.OverStockMasterProductPriceControl.Replace("replace", i.ToString());

                MasterProductList mp = new MasterProductList(i, getmasterProductname(newProductLink), getMasterProductPrice(newPriceLink));
                OverStockMasterProductList.Add(mp);
            }


            MarketDatabaseOperations db = new MarketDatabaseOperations();

            //productid Int PRIMARY KEY,MasterProductName string
            foreach (var item in OverStockMasterProductList)
            {
                MarektPriceUpdater obj = new MarektPriceUpdater();
                obj.MasterProductUpdater(item.masterproductName);
                obj.priceTableUpdate("OverStock", item.masterproductName, item.masterproductName, item.masterproductPrice);
            }
            closeWebDriver();
        }



        public void closeWebDriver()
        {
            iwebdriver.Quit();
        }
    }
}
