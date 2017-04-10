using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketDb;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MarketCore
{
    public class Kmart
    {
         // as of now i think to pass the control from outside 
        // as if their id changes i dont have to recompile the whole stuff 
        private IWebDriver iwebdriver;
        public string kmartSearchBoxControl { get; set; }
        public string kmartSearchBoxClick {get;set; }
        public string kmartProductNameControl { get; set; }
        public string kmartProductPriceControl { get; set; }
        public string kmartMasterProductNameControl { get; set; }
        public string kmartMasterProductPriceControl { get; set; }
        public string pageUrl { get; set; }
        public string pagelenght { get; set; }
        public List<SearchResults> kmartSearchResults= new List<SearchResults>();
        public List<MasterProductList> kmartMasterProductList = new List<MasterProductList>();
        
        public Kmart(string url)
        {
        // url comes from the ui
           
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("kmart");
            kmartSearchBoxControl = mCoreControlReader.searchButton;
            kmartSearchBoxClick = mCoreControlReader.searchClick;
            kmartProductNameControl = mCoreControlReader.productName;
            kmartProductPriceControl = mCoreControlReader.productPrice;
            kmartMasterProductNameControl = mCoreControlReader.productMasterName;
            kmartMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            pagelenght = mCoreControlReader.pageLength;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(url);
             iwebdriver.Navigate().GoToUrl(url);
        }



        public Kmart()
        {
            // url comes from the ui
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("kmart");
            kmartSearchBoxControl = mCoreControlReader.searchButton;
            kmartSearchBoxClick = mCoreControlReader.searchClick;
            kmartProductNameControl = mCoreControlReader.productName;
            kmartProductPriceControl = mCoreControlReader.productPrice;
            kmartMasterProductNameControl = mCoreControlReader.productMasterName;
            kmartMasterProductPriceControl = mCoreControlReader.productMasterPrice;
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
            /*
             try
             {
                
                 bool isStartUpDisplayed = iwebdriver.FindElement(By.CssSelector("#intl_english > div > div > select")).Displayed;
                 if (isStartUpDisplayed)
                 {
                     var startupform = iwebdriver.FindElement(By.CssSelector("#intl_english > div > div > select > option:nth-child(2)"));
                  
                    startupform.Click();

                    var gobutton = iwebdriver.FindElement(By.CssSelector("#intl_english > div > div > div.go_button"));

                    gobutton.Click();
                 }
             }
             catch (NoSuchElementException)
             {

                // return false;
             }
            
           
             */
            try
            {
                var findsearchbox = iwebdriver.FindElement(By.Id(this.kmartSearchBoxControl));
                findsearchbox.Clear();
                findsearchbox.SendKeys(name);

                System.Threading.Thread.Sleep(1000);
                findsearchbox.SendKeys(Keys.Enter);
            }
            catch (NoSuchElementException)
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
             //   var clicksearch = iwebdriver.FindElement(By.CssSelector(this.kmartSearchBoxClick));
              //  clicksearch.Click();
            }
           
        }

        string getProductNameFromSearchResults()
        {

            try
            {
                var resultTitle = iwebdriver.FindElement(By.CssSelector(this.kmartProductNameControl));
               
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
                var price = iwebdriver.FindElement(By.CssSelector(this.kmartProductPriceControl));
               
                return price.Text;
            }
            catch (NoSuchElementException)
            {

                return "Excpetion In Price";
            }
        }
        

      public  bool searchProducts(string name)
        {
           
            actionEnterProductName(name);
          //  actionClickSearchBox();
            SearchResults tempSearchResult = new SearchResults(name,getProductNameFromSearchResults(),getProductPrice());
            kmartSearchResults.Add(tempSearchResult);
            MarektPriceUpdater obj = new MarektPriceUpdater();
            obj.priceTableUpdate("kmart", name, tempSearchResult.searchResultName, tempSearchResult.searchResultPrice);
           
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
                string newProductLink = this.kmartMasterProductNameControl.Replace("child(x)", "child(" + i + ")");
                string newPriceLink = this.kmartMasterProductPriceControl.Replace("child(x)", "child(" + i+")");

                MasterProductList mp = new MasterProductList(i, getmasterProductname(newProductLink), getMasterProductPrice(newPriceLink));
                kmartMasterProductList.Add(mp);
            }


            MarketDatabaseOperations db = new MarketDatabaseOperations();

                //productid Int PRIMARY KEY,MasterProductName string
            foreach (var item in kmartMasterProductList)
            {
                MarektPriceUpdater obj = new MarektPriceUpdater();
                obj.MasterProductUpdater(item.masterproductName);
                obj.priceTableUpdate("kmart", item.masterproductName, item.masterproductName, item.masterproductPrice);
            }
            closeWebDriver();
        }

  
       
        public  void closeWebDriver()
        {
            iwebdriver.Quit();
        }
    }
}
