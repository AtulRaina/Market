using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketDb;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MarketCore
{
    class Disney
    {

         private IWebDriver iwebdriver;
        public string disneySearchBoxControl { get; set; }
        public string disneySearchBoxClick {get;set; }
        public string disneyProductNameControl { get; set; }
        public string disneyProductPriceControl { get; set; }
        public string disneyMasterProductNameControl { get; set; }
        public string disneyMasterProductPriceControl { get; set; }
        public string pageUrl { get; set; }
        public string pagelenght { get; set; }
        public List<SearchResults> disneySearchResults= new List<SearchResults>();
        public List<MasterProductList> disneyMasterProductList = new List<MasterProductList>();
        
        public Disney(string url)
        {
        // url comes from the ui
           
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("disney");
            disneySearchBoxControl = mCoreControlReader.searchButton;
            disneySearchBoxClick = mCoreControlReader.searchClick;
            disneyProductNameControl = mCoreControlReader.productName;
            disneyProductPriceControl = mCoreControlReader.productPrice;
            disneyMasterProductNameControl = mCoreControlReader.productMasterName;
            disneyMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            pagelenght = mCoreControlReader.pageLength;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(url);
             iwebdriver.Navigate().GoToUrl(url);
        }



        public Disney()
        {
            // url comes from the ui
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("disney");
            disneySearchBoxControl = mCoreControlReader.searchButton;
            disneySearchBoxClick = mCoreControlReader.searchClick;
            disneyProductNameControl = mCoreControlReader.productName;
            disneyProductPriceControl = mCoreControlReader.productPrice;
            disneyMasterProductNameControl = mCoreControlReader.productMasterName;
            disneyMasterProductPriceControl = mCoreControlReader.productMasterPrice;
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
           
            var findsearchbox = iwebdriver.FindElement(By.Id(this.disneySearchBoxControl));
            findsearchbox.Clear();
            findsearchbox.SendKeys(name);
            
            System.Threading.Thread.Sleep(1000);
            findsearchbox.SendKeys(Keys.Enter);
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
             //   var clicksearch = iwebdriver.FindElement(By.CssSelector(this.disneySearchBoxClick));
              //  clicksearch.Click();
            }
           
        }

        string getProductNameFromSearchResults()
        {

            try
            {
                var resultTitle = iwebdriver.FindElement(By.CssSelector(this.disneyProductNameControl));
               
                return resultTitle.Text;
                
            }
            catch (NoSuchElementException)
            {

                try
                {
                    var resultTitle = iwebdriver.FindElement(By.CssSelector("#main > section.pageHeader > h1"));

                    return resultTitle.Text;
                }
                catch (NoSuchElementException)
                {

                    return "Exception Product Name";
                }
           
            }
         
        }
        string getProductPrice()
        {
            try
            {
                var price = iwebdriver.FindElement(By.CssSelector(this.disneyProductPriceControl));
                string temp = price.Text;
                return temp.Replace("now", "");
            }
            catch (NoSuchElementException)
            {
                try
                {
                    var price = iwebdriver.FindElement(By.CssSelector("main > section.endecaProductRight.productDescription > div > div.productPriceBar > p.productPrice.sale.textSale > span"));
                    string temp = price.Text;
                    return temp.Replace("now", "");
                }
                catch (NoSuchElementException)
                {

                    return "Excpetion In Price";
                }
           
            }
        }
        

      public  bool searchProducts(string name)
        {
           
            actionEnterProductName(name);
          //  actionClickSearchBox();
            SearchResults tempSearchResult = new SearchResults(name,getProductNameFromSearchResults(),getProductPrice());
            disneySearchResults.Add(tempSearchResult);
            MarektPriceUpdater obj = new MarektPriceUpdater();
            obj.priceTableUpdate("disney", name, tempSearchResult.searchResultName, tempSearchResult.searchResultPrice);
           
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
                string temp = resultTitle.Text;
                return temp.Replace("now", "");
               

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
                string newProductLink = this.disneyMasterProductNameControl.Replace("child(x)", "child(" + i + ")");
                string newPriceLink = this.disneyMasterProductPriceControl.Replace("child(x)", "child(" + i+")");

                MasterProductList mp = new MasterProductList(i, getmasterProductname(newProductLink), getMasterProductPrice(newPriceLink));
                disneyMasterProductList.Add(mp);
            }


            MarketDatabaseOperations db = new MarketDatabaseOperations();

                //productid Int PRIMARY KEY,MasterProductName string
            foreach (var item in disneyMasterProductList)
            {
                MarektPriceUpdater obj = new MarektPriceUpdater();
                obj.MasterProductUpdater(item.masterproductName);
                obj.priceTableUpdate("disney", item.masterproductName, item.masterproductName, item.masterproductPrice);
            }
            closeWebDriver();
        }

  
       
        public  void closeWebDriver()
        {
            iwebdriver.Quit();
        }
    }
}
