using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketDb;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MarketCore
{
    public class Costco
    {
        // as of now i think to pass the control from outside 
        // as if their id changes i dont have to recompile the whole stuff 
        private IWebDriver iwebdriver;
        public string CostcoSearchBoxControl { get; set; }
        public string CostcoSearchBoxClick { get; set; }
        public string CostcoProductNameControl { get; set; }
        public string CostcoProductPriceControl { get; set; }
        public string CostcoMasterProductNameControl { get; set; }
        public string CostcoMasterProductPriceControl { get; set; }
        public string pageUrl { get; set; }
        public string pagelenght { get; set; }
        public List<SearchResults> CostcoSearchResults = new List<SearchResults>();
        public List<MasterProductList> CostcoMasterProductList = new List<MasterProductList>();

        public Costco(string url)
        {
            // url comes from the u
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("costco");
            CostcoSearchBoxControl = mCoreControlReader.searchButton;
            CostcoSearchBoxClick = mCoreControlReader.searchClick;
            CostcoProductNameControl = mCoreControlReader.productName;
            CostcoProductPriceControl = mCoreControlReader.productPrice;
            CostcoMasterProductNameControl = mCoreControlReader.productMasterName;
            CostcoMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            pagelenght = mCoreControlReader.pageLength;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(url);
          
        }



        public Costco()
        {
            // url comes from the ui
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("costco");
            CostcoSearchBoxControl = mCoreControlReader.searchButton;
            CostcoSearchBoxClick = mCoreControlReader.searchClick;
            CostcoProductNameControl = mCoreControlReader.productName;
            CostcoProductPriceControl = mCoreControlReader.productPrice;
            CostcoMasterProductNameControl = mCoreControlReader.productMasterName;
            CostcoMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(mCoreControlReader.pageUrl);
        }


        bool actionEnterProductName(string name)
        {
            try
            {
                var findsearchbox = iwebdriver.FindElement(By.Id(this.CostcoSearchBoxControl));
                findsearchbox.SendKeys(name);
                findsearchbox.SendKeys(Keys.Enter);
            }
            catch (NoSuchElementException)
            {
            }
            catch(StaleElementReferenceException)
            {
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

            }

        }

        string getProductNameFromSearchResults()
        {

            try
            {
                var resultTitle = iwebdriver.FindElement(By.XPath(this.CostcoProductNameControl));

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
                var price = iwebdriver.FindElement(By.XPath(this.CostcoProductPriceControl));

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
            SearchResults tempSearchResult = new SearchResults(name, getProductNameFromSearchResults(), getProductPrice());
            CostcoSearchResults.Add(tempSearchResult);
            MarektPriceUpdater obj = new MarektPriceUpdater();
            obj.priceTableUpdate("costco", name, tempSearchResult.searchResultName, tempSearchResult.searchResultPrice);
            return true;
        }

        bool getProducts()
        {
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
                string newProductLink = this.CostcoMasterProductNameControl.Replace("replace",i.ToString());
                string newPriceLink = this.CostcoMasterProductPriceControl.Replace("replace", i.ToString());
                MasterProductList mp = new MasterProductList(i, getmasterProductname(newProductLink), getMasterProductPrice(newPriceLink));
                CostcoMasterProductList.Add(mp);
            }


            MarketDatabaseOperations db = new MarketDatabaseOperations();


            foreach (var item in CostcoMasterProductList)
            {
                MarektPriceUpdater obj = new MarektPriceUpdater();
                obj.MasterProductUpdater(item.masterproductName);
                obj.priceTableUpdate("Costco", item.masterproductName, item.masterproductName, item.masterproductPrice);
            }
            closeWebDriver();
        }



        public void closeWebDriver()
        {
            iwebdriver.Quit();
        }
    }
}
