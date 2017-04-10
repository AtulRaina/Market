using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MarketDb;
using MarketLogger;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MarketCore
{

    public class amazonSeachList
    {
    public string searchMasterString { get; set; }
        public string searchResultName { get; set; }
        public string searchResultPrice { get; set; }
        public string sellerRanking { get; set; }
        public amazonSeachList(string a, string b, string c,string d)
        {
            searchMasterString = a;
            searchResultName = b;
            searchResultPrice = c;
            sellerRanking = d;
        }

    }
   public class Amazon
    {

          // as of now i think to pass the control from outside 
        // as if their id changes i dont have to recompile the whole stuff 
      
        private IWebDriver iwebdriver;
        public string amazonSearchBoxControl { get; set; }
        public string amazonSearchBoxClick {get;set; }
        public string amazonProductNameControl { get; set; }
        public string amazonProductPriceControl { get; set; }
        public string amazonMasterProductNameControl { get; set; }
        public string amazonMasterProductPriceControl { get; set; }
        public string pageUrl { get; set; }
        public string pagelenght { get; set; }
        public List<amazonSeachList> amazonSearchResults = new List<amazonSeachList>();
        static bool disposePopUp = true;
        public bool fetchSellerInformation = false;
     public   static string productPriceBeforeClick="Exception In Price";
        public List<MasterProductList> amazonMasterProductList = new List<MasterProductList>();
       
        public Amazon(string url)
        {
        // url comes from the ui
           
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("amazon");
            amazonSearchBoxControl = mCoreControlReader.searchButton;
            amazonSearchBoxClick = mCoreControlReader.searchClick;
            amazonProductNameControl = mCoreControlReader.productName;
            amazonProductPriceControl = mCoreControlReader.productPrice;
            amazonMasterProductNameControl = mCoreControlReader.productMasterName;
            amazonMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            pagelenght = mCoreControlReader.pageLength;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(url);
            Logger.filePath = @"MarketLog.log";
        }



        public Amazon(bool flag)
        {
            // url comes from the ui
            MarketCoreControlReader mCoreControlReader = new MarketCoreControlReader("amazon");
            amazonSearchBoxControl = mCoreControlReader.searchButton;
            amazonSearchBoxClick = mCoreControlReader.searchClick;
            amazonProductNameControl = mCoreControlReader.productName;
            amazonProductPriceControl = mCoreControlReader.productPrice;
            amazonMasterProductNameControl = mCoreControlReader.productMasterName;
            amazonMasterProductPriceControl = mCoreControlReader.productMasterPrice;
            iwebdriver = new ChromeDriver();
            iwebdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            iwebdriver.Navigate().GoToUrl(mCoreControlReader.pageUrl);
            Logger.filePath = @"MarketLog.log";
            fetchSellerInformation = flag;
        }
        ~Amazon()
        {
          //  iwebdriver.Quit();
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
            

            var findsearchbox = iwebdriver.FindElement(By.Id(this.amazonSearchBoxControl));
            findsearchbox.Clear();
            actionClickSearchBox();
            findsearchbox.SendKeys(name);
            
            System.Threading.Thread.Sleep(1000);
            findsearchbox.SendKeys(Keys.Enter);
            return true;
        }

        void actionClickSearchBox()
        {
            if (disposePopUp)
            {
                disposePopUp = false;
            try
            {
                bool checkForthepopupwindow = iwebdriver.FindElement(By.CssSelector("#redir-stay-at-www > span.redir-a-button-sec-center")).Displayed;
                if (checkForthepopupwindow)
                {
                    var clickTheWindow = iwebdriver.FindElement(By.CssSelector("#redir-stay-at-www > span.redir-a-button-sec-center"));
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
             //   var clicksearch = iwebdriver.FindElement(By.CssSelector(this.amazonSearchBoxClick));
              //  clicksearch.Click();
            }
            }
        }

        string getProductNameFromSearchResults()
        {

            try
            {
               // System.Threading.Thread.Sleep(1000);
                try
                {
                    productPriceBeforeClick = "Exception In Price";
                    var temprice = iwebdriver.FindElement(By.CssSelector("#result_0 > div > div > div > div.a-fixed-left-grid-col.a-col-right > div:nth-child(3) > div.a-column.a-span7 > div > div > a > span.a-size-base.a-color-price"));
                    //#result_0 > div > div > div > div.a-fixed-left-grid-col.a-col-right > div:nth-child(3) > div.a-column.a-span7 > div > div:nth-child(1) > a > span.a-size-base.a-color-base
                    //#result_0 > div > div > div > div.a-fixed-left-grid-col.a-col-right > div:nth-child(3) > div.a-column.a-span7 > div.a-row.a-spacing-none > a > span > span > span
                    //#result_0 > div > div > div > div.a-fixed-left-grid-col.a-col-right > div:nth-child(3) > div.a-column.a-span7 > div > div:nth-child(1) > a > span.a-size-base.a-color-base
                    productPriceBeforeClick = temprice.Text;
                }
                catch (NoSuchElementException)
                {
                    
                    try
                    {
                        var temprice = iwebdriver.FindElement(By.CssSelector("#result_0 > div > div > div > div.a-fixed-left-grid-col.a-col-right > div:nth-child(3) > div.a-column.a-span7 > div > div:nth-child(1) > a > span.a-size-base.a-color-base"));
                        productPriceBeforeClick = temprice.Text;
                    }
                    catch (NoSuchElementException)
                    {
                        try
                        {
                            var temprice = iwebdriver.FindElement(By.CssSelector("#result_0 > div > div > div > div.a-fixed-left-grid-col.a-col-right > div:nth-child(3) > div.a-column.a-span7 > div.a-row.a-spacing-none > a > span > span > span"));
                       
                        productPriceBeforeClick = temprice.Text;
                        }
                        catch (Exception)
                        {
                            productPriceBeforeClick = "Exception In Price";
                           
                        }
                        
                    }
                }
               

                var resultTitle = iwebdriver.FindElement(By.CssSelector(this.amazonProductNameControl));
                string productname = resultTitle.Text;
                if(fetchSellerInformation)
                resultTitle.Click();
                return productname;
                
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
               // System.Threading.Thread.Sleep(1000);
                if (fetchSellerInformation)
                {
                    var price = iwebdriver.FindElement(By.CssSelector(this.amazonProductPriceControl));
                    String temp = price.Text;
                    string output = Regex.Replace(temp, @"(?<= .*) ", ".");
                    return output;
                }
                else
                {
                    return Regex.Replace(productPriceBeforeClick, @"(?<= .*) ", ".");
                }
              
            }
            catch (NoSuchElementException)
            {
                return Regex.Replace(productPriceBeforeClick, @"(?<= .*) ", ".");
            }
        }
        

      public  bool searchProducts(string name)
        {
           
            actionEnterProductName(name);
            //actionClickSearchBox();



            amazonSeachList tempSearchResult = new amazonSeachList(name, getProductNameFromSearchResults(), getProductPrice(), getSellerRanking());
         /* Now call the Page functions above */

            amazonSearchResults.Add(tempSearchResult);

            //@"PriceTable(id INT PRIMARY KEY,vendorid int,productid int,productname string,price string)";')";
            MarektPriceUpdater obj = new MarektPriceUpdater();
            obj.priceTableUpdate("Amazon", name, tempSearchResult.searchResultName, tempSearchResult.searchResultPrice,tempSearchResult.sellerRanking);

          return true;
        }

      private string getSellerRanking()
      {
          if (fetchSellerInformation)
          {
              try
              {

                  var baseTable = iwebdriver.FindElement(By.Id("productDetails_detailBullets_sections1"));

                  if (baseTable.Text.Contains("Best Sellers Rank"))
                  {
                      string tobesearched = "Best Sellers Rank";
                      string code = baseTable.Text.Substring(baseTable.Text.IndexOf(tobesearched) + tobesearched.Length);



                      String temp = code;
                      int index = temp.IndexOf(',');
                      if (index > 0)
                      {
                          return temp.Substring(0, index).Replace("#", "");
                      }
                      else
                      {

                          string replace = temp.Replace(",", "");
                          string temper = replace.Replace("#", "");
                          return temper;
                      }
                  }
                  else
                  {
                      return "Oops NoSellerInformation Caught";
                  }


              }
              //  tableRows.get(index).getText();



              catch (NoSuchElementException)
              {
                  return "Oops!";
              }
          }
          else
          {
              return "Disabled Seller info";
          }
       
       
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
                return resultTitle.Text.Replace(",","");

            }
            catch (NoSuchElementException)
            {
                try
                {
                    string replace = tempr.Replace("child(3)", "child(5)");
                    var resultTitle = iwebdriver.FindElement(By.CssSelector(replace));
                    Logger.log(resultTitle.Text);
                    return resultTitle.Text.Replace(",", "");
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
            for (int i = 0; i < Convert.ToInt32(this.pagelenght); i++)
            {
                string newProductLink=this.amazonMasterProductNameControl.Replace("_0","_"+ i);
                string newPriceLink = this.amazonMasterProductPriceControl.Replace("_0", "_" + i);
                MasterProductList mp = new MasterProductList(i, getmasterProductname(newProductLink), getMasterProductPrice(newPriceLink));
                amazonMasterProductList.Add(mp);
            }



            MarketDatabaseOperations db = new MarketDatabaseOperations();
                //productid Int PRIMARY KEY,MasterProductName string
            foreach (var item in amazonMasterProductList)
            {
            

                /* its also good idea to insert it into the price table */
                MarektPriceUpdater obj = new MarektPriceUpdater();
                obj.MasterProductUpdater(item.masterproductName);
                obj.priceTableUpdate("Amazon", item.masterproductName, item.masterproductName, item.masterproductPrice);
           
                
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
