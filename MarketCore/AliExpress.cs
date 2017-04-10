using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketDb;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MarketCore
{
    public class AliExpress
    {
         // as of now i think to pass the control from outside 
        // as if their id changes i dont have to recompile the whole stuff 
        private IWebDriver iwebdriver;
        public string AliExpressSearchBoxControl { get; set; }
        public string AliExpressSearchBoxClick {get;set; }
        public string AliExpressProductNameControl { get; set; }
        public string AliExpressProductPriceControl { get; set; }
        public string AliExpressMasterProductNameControl { get; set; }
        public string AliExpressMasterProductPriceControl { get; set; }
        public string pageUrl { get; set; }
        public string pagelenght { get; set; }
        public List<SearchResults> AliExpressSearchResults= new List<SearchResults>();
        public List<MasterProductList> AliExpressMasterProductList = new List<MasterProductList>();
        
        public AliExpress(string url)
        {
        // url comes from the ui
           
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("AliExpress");
            AliExpressSearchBoxControl = mCoreControlReader.searchButton;
            AliExpressSearchBoxClick = mCoreControlReader.searchClick;
            AliExpressProductNameControl = mCoreControlReader.productName;
            AliExpressProductPriceControl = mCoreControlReader.productPrice;
            AliExpressMasterProductNameControl = mCoreControlReader.productMasterName;
            AliExpressMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            pagelenght = mCoreControlReader.pageLength;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(url);
             iwebdriver.Navigate().GoToUrl(url);
        }



        public AliExpress()
        {
            // url comes from the ui
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("AliExpress");
            AliExpressSearchBoxControl = mCoreControlReader.searchButton;
            AliExpressSearchBoxClick = mCoreControlReader.searchClick;
            AliExpressProductNameControl = mCoreControlReader.productName;
            AliExpressProductPriceControl = mCoreControlReader.productPrice;
            AliExpressMasterProductNameControl = mCoreControlReader.productMasterName;
            AliExpressMasterProductPriceControl = mCoreControlReader.productMasterPrice;
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
                var findsearchbox = iwebdriver.FindElement(By.Id(this.AliExpressSearchBoxControl));
                findsearchbox.Clear();
                findsearchbox.SendKeys(name);

                System.Threading.Thread.Sleep(1000);
                findsearchbox.SendKeys(Keys.Enter);
            }
            catch (NoSuchElementException)
            {
                
                
            }
            catch (UnhandledAlertException)
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
             //   var clicksearch = iwebdriver.FindElement(By.CssSelector(this.AliExpressSearchBoxClick));
              //  clicksearch.Click();
            }
           
        }

        string getProductNameFromSearchResults()
        {

            try
            {
                var resultTitle = iwebdriver.FindElement(By.CssSelector(this.AliExpressProductNameControl));
               
                return resultTitle.Text;
                
            }
            catch (NoSuchElementException)
            {

             return "Exception Product Name";
            }
            catch(UnhandledAlertException)
                 {
                return "Alert thrown";
            }
         
        }
        string getProductPrice()
        {
            try
            {
                var price = iwebdriver.FindElement(By.CssSelector(this.AliExpressProductPriceControl));

                return price.Text;
            }
            catch (NoSuchElementException)
            {

                return "Excpetion In Price";
            }

            catch (UnhandledAlertException)
            {
                return "Alert thrown";
            }
        }

      public  bool searchProducts(string name)
        {
           
            actionEnterProductName(name);
          //  actionClickSearchBox();
            SearchResults tempSearchResult = new SearchResults(name,getProductNameFromSearchResults(),getProductPrice());
            AliExpressSearchResults.Add(tempSearchResult);
            MarektPriceUpdater obj = new MarektPriceUpdater();
            obj.priceTableUpdate("AliExpress", name, tempSearchResult.searchResultName, tempSearchResult.searchResultPrice);
           
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
                string newProductLink = this.AliExpressMasterProductNameControl.Replace("child(x)", "child(" + i + ")");
                string newPriceLink = this.AliExpressMasterProductPriceControl.Replace("child(x)", "child(" + i+")");

                MasterProductList mp = new MasterProductList(i, getmasterProductname(newProductLink), getMasterProductPrice(newPriceLink));
                AliExpressMasterProductList.Add(mp);
            }


            MarketDatabaseOperations db = new MarketDatabaseOperations();

                //productid Int PRIMARY KEY,MasterProductName string
            foreach (var item in AliExpressMasterProductList)
            {
                MarektPriceUpdater obj = new MarektPriceUpdater();
                obj.MasterProductUpdater(item.masterproductName);
                obj.priceTableUpdate("AliExpress", item.masterproductName, item.masterproductName, item.masterproductPrice);
            }
            closeWebDriver();
        }

  
       
        public  void closeWebDriver()
        {
            iwebdriver.Quit();
        }
    }
}
