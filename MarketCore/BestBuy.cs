using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MarketDb;
using MarketLogger;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
/* The client is looking for these websites to control 
http://www.bestbuy.com
http://www.walmart.com
http://www.target.com
http://www.kmart.com
http://www.toysrus.com
 * amazon.com
 */
namespace MarketCore
{
    public class BestBuy
    {
        // as of now i think to pass the control from outside 
        // as if their id changes i dont have to recompile the whole stuff 
        private IWebDriver iwebdriver;
        public string bestBuySearchBoxControl { get; set; }
        public string bestBuySearchBoxClick {get;set; }
        public string bestBuyProductNameControl { get; set; }
        public string bestBuyProductPriceControl { get; set; }
        public string bestBuyMasterProductNameControl { get; set; }
        public string bestBuyMasterProductPriceControl { get; set; }
        public string pageUrl { get; set; }
        public string pagelenght { get; set; }
        public List<SearchResults> bestBuySearchResults= new List<SearchResults>();
        public List<MasterProductList> bestBuyMasterProductList = new List<MasterProductList>();
        
        public BestBuy(string url)
        {
        // url comes from the ui
           
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("BestBuy");
            bestBuySearchBoxControl = mCoreControlReader.searchButton;
            bestBuySearchBoxClick = mCoreControlReader.searchClick;
            bestBuyProductNameControl = mCoreControlReader.productName;
            bestBuyProductPriceControl = mCoreControlReader.productPrice;
            bestBuyMasterProductNameControl = mCoreControlReader.productMasterName;
            bestBuyMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            pagelenght = mCoreControlReader.pageLength;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(url);
        }



        public BestBuy()
        {
            // url comes from the ui
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("BestBuy");
            bestBuySearchBoxControl = mCoreControlReader.searchButton;
            bestBuySearchBoxClick = mCoreControlReader.searchClick;
            bestBuyProductNameControl = mCoreControlReader.productName;
            bestBuyProductPriceControl = mCoreControlReader.productPrice;
            bestBuyMasterProductNameControl = mCoreControlReader.productMasterName;
            bestBuyMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(mCoreControlReader.pageUrl);
        }
        ~BestBuy()
        {
          //  iwebdriver.Close();
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
            
           
            

            var findsearchbox = iwebdriver.FindElement(By.Id(this.bestBuySearchBoxControl));
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
             //   var clicksearch = iwebdriver.FindElement(By.CssSelector(this.bestBuySearchBoxClick));
              //  clicksearch.Click();
            }
           
        }

        string getProductNameFromSearchResults()
        {

            try
            {
                var resultTitle = iwebdriver.FindElement(By.CssSelector(this.bestBuyProductNameControl));
               
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
                var price = iwebdriver.FindElement(By.CssSelector(this.bestBuyProductPriceControl));
               
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
            actionClickSearchBox();
           
        

            SearchResults tempSearchResult = new SearchResults(name,getProductNameFromSearchResults(),getProductPrice());
            bestBuySearchResults.Add(tempSearchResult);
            MarektPriceUpdater obj = new MarektPriceUpdater();
            obj.priceTableUpdate("BestBuy", name, tempSearchResult.searchResultName, tempSearchResult.searchResultPrice);
           
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
                string newProductLink=this.bestBuyMasterProductNameControl.Replace("(1)","("+ i+")");
                string newPriceLink = this.bestBuyMasterProductPriceControl.Replace("(1)", "(" + i + ")");

                MasterProductList mp = new MasterProductList(i, getmasterProductname(newProductLink), getMasterProductPrice(newPriceLink));
                bestBuyMasterProductList.Add(mp);
            }


            MarketDatabaseOperations db = new MarketDatabaseOperations();

                //productid Int PRIMARY KEY,MasterProductName string
            foreach (var item in bestBuyMasterProductList)
            {
                MarektPriceUpdater obj = new MarektPriceUpdater();
                obj.MasterProductUpdater(item.masterproductName);
                obj.priceTableUpdate("BestBuy", item.masterproductName, item.masterproductName, item.masterproductPrice);
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
