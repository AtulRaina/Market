using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MarketCore;
using MarketDb;
using MarketLogger;

namespace MarketAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Logger.filePath = @"MarketLog.log";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            bool isMultiTaskingEnable = false;
            if (multiTaskingCkBx.IsChecked == true)
            {
                isMultiTaskingEnable = true;
            }
            if (textBox2.Text.Length > 1)
            {
                Logger.log("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                PageController pagecontroller = new PageController(textBox2.Text.Trim());
                pagecontroller.creatingProduct();
            }
            else
            {
                bool flag = false;
                if(amazonCkbx.IsChecked==true)
                {
                    PageController amazon = new PageController("amazon");
                    flag = true;
                    if(isMultiTaskingEnable )
                    {
                        Thread oThread = new Thread(new ThreadStart(amazon.creatingProduct));

                        oThread.Start();
                    }
                    else
                    {
                        amazon.creatingProduct();
                    }
                  
                  

                }

                if(bestbuyckbk.IsChecked==true)
                {
                    flag = true;
                    PageController bestbuy = new PageController("bestbuy");
                    flag = true;
                    if (isMultiTaskingEnable)
                    {
                        Thread oThread = new Thread(new ThreadStart(bestbuy.creatingProduct));

                        oThread.Start();
                    }
                    else
                    {
                        bestbuy.creatingProduct();
                    }
                }

                if (overstockCkBbk.IsChecked == true)
                {
                    flag = true;
                    PageController costco = new PageController("OverStock");
                    flag = true;
                    if (isMultiTaskingEnable)
                    {
                        Thread oThread = new Thread(new ThreadStart(costco.creatingProduct));

                        oThread.Start();
                    }
                    else
                    {
                        costco.creatingProduct();
                    }
                }


                if (wallmartCkBbk.IsChecked==true)
                {
                    flag = true;
                    PageController wallmart = new PageController("walmart");
                    flag = true;
                    if (isMultiTaskingEnable)
                    {
                        Thread oThread = new Thread(new ThreadStart(wallmart.creatingProduct));

                        oThread.Start();
                    }
                    else
                    {
                        wallmart.creatingProduct();
                    }
                }

                if (kmartCkBbk.IsChecked == true)
                {
                    flag = true;
                    PageController kmart = new PageController("kmart");
                    flag = true;
                    if (isMultiTaskingEnable)
                    {
                        Thread oThread = new Thread(new ThreadStart(kmart.creatingProduct));

                        oThread.Start();
                    }
                    else
                    {
                        kmart.creatingProduct();
                    }
                }
                if (disneyCkBbk.IsChecked==true)
                {
                    flag = true;
                    PageController disney = new PageController("disney");
                    flag = true;
                    if (isMultiTaskingEnable)
                    {
                        Thread oThread = new Thread(new ThreadStart(disney.creatingProduct));

                        oThread.Start();
                    }
                    else
                    {
                        disney.creatingProduct();
                    }
                }

                if (targetCkBbk.IsChecked == true)
                {
                    flag = true;
                    PageController target = new PageController("target");
                    flag = true;
                    if (isMultiTaskingEnable)
                    {
                        Thread oThread = new Thread(new ThreadStart(target.creatingProduct));

                        oThread.Start();
                    }
                    else
                    {
                        target.creatingProduct();
                    }
                }

                if (toyrusCkBbk.IsChecked == true)
                {
                    flag = true;
                    PageController homeDepot = new PageController("homedepot");
                    flag = true;
                    if (isMultiTaskingEnable)
                    {
                        Thread oThread = new Thread(new ThreadStart(homeDepot.creatingProduct));

                        oThread.Start();
                    }
                    else
                    {
                        homeDepot.creatingProduct();
                    }
                }


                if (aliexpressCkBk.IsChecked == true)
                {
                    PageController ali = new PageController("AliExpres");
                    flag = true;
                    if (isMultiTaskingEnable)
                    {
                        Thread oThread = new Thread(new ThreadStart(ali.creatingProduct));

                        oThread.Start();
                    }
                    else
                    {
                        ali.creatingProduct();
                    }

                }




                if (overstockCkBbk.IsChecked == true)
                {
                    PageController ali = new PageController("overstock");
                    flag = true;
                    if (isMultiTaskingEnable)
                    {
                        Thread oThread = new Thread(new ThreadStart(ali.creatingProduct));

                        oThread.Start();
                    }
                    else
                    {
                        ali.creatingProduct();
                    }
                }


                if (costcoCkbk.IsChecked == true)
                {
                    PageController ali = new PageController("costco");
                    flag = true;
                    if (isMultiTaskingEnable)
                    {
                        Thread oThread = new Thread(new ThreadStart(ali.creatingProduct));

                        oThread.Start();
                    }
                    else
                    {
                        ali.creatingProduct();
                    }
                }

                if (!flag)
                    MessageBox.Show("Enter the Link or select a check Box , Make sure you have a master created", "Lost Boys Says:");
                Logger.log("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            }
            /*BestBuy bestbuy = new BestBuy();


            foreach (var item in bestbuy.bestBuyMasterProductList)
            {
                bestbuy.searchProducts(item.masterproductName);
            }
            // updates research database with best buy key and results
        
             */ }

      
        private void button1_Click(object sender, RoutedEventArgs e)
        {/* This is how i created master product list for best buy
            BestBuy bestbuy = new BestBuy(textBox1.Text.Trim());
            bestbuy.createmasterlist();
          * 
          * */
            bool isMultiTaskingEnable = false;
            if(multiTaskingCkBx.IsChecked==true)
            {
                isMultiTaskingEnable = true;
            }
           
            Logger.log("__________________________________________________________________________________________");
            PageController pagecontroller = new PageController(textBox1.Text.Trim());
            Logger.log("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Logger.log("_____________________" + textBox1.Text.Trim()+"__________________________________________");
            Logger.log("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Logger.log("__________________________________________________________________________________________");
            pagecontroller.cratingMaster();

            if (amazonCkbx.IsChecked == true)
            {
                bool resellerInfoFlag=false;
                if(sellerInfoCkBx.IsChecked==true)
                {
                    resellerInfoFlag = true;
                }
                PageController pagecontroller1 = new PageController("amazon",resellerInfoFlag);

               
                if (isMultiTaskingEnable)
                {
                    Thread oThread = new Thread(new ThreadStart(pagecontroller1.creatingProduct));

                    oThread.Start();
                }
                else
                {
                    pagecontroller1.creatingProduct();
                }

            }

            if (bestbuyckbk.IsChecked == true)
            {
                PageController bestbuy = new PageController("bestbuy");
                if (isMultiTaskingEnable)
                {
                    Thread oThread = new Thread(new ThreadStart(bestbuy.creatingProduct));

                    oThread.Start();
                }
                else
                {
                    bestbuy.creatingProduct();
                }
            }

            if (overstockCkBbk.IsChecked == true)
            {
                PageController Costco= new PageController("OverStock");
                if (isMultiTaskingEnable)
                {
                    Thread oThread = new Thread(new ThreadStart(Costco.creatingProduct));

                    oThread.Start();
                }
                else
                {
                    Costco.creatingProduct();
                }
            }


            if (wallmartCkBbk.IsChecked == true)
            {
                PageController wallmart = new PageController("walmart");
                if (isMultiTaskingEnable)
                {
                    Thread oThread = new Thread(new ThreadStart(wallmart.creatingProduct));

                    oThread.Start();
                }
                else
                {
                    wallmart.creatingProduct();
                }
            }

            if (kmartCkBbk.IsChecked == true)
            {
                PageController kmart = new PageController("kmart");
                if (isMultiTaskingEnable)
                {
                    Thread oThread = new Thread(new ThreadStart(kmart.creatingProduct));

                    oThread.Start();
                }
                else
                {
                    kmart.creatingProduct();
                }
            }

            if (disneyCkBbk.IsChecked == true)
            {
                PageController disney = new PageController("disney");
                if (isMultiTaskingEnable)
                {
                    Thread oThread = new Thread(new ThreadStart(disney.creatingProduct));

                    oThread.Start();
                }
                else
                {
                    disney.creatingProduct();
                }
            }

            if(targetCkBbk.IsChecked==true)
            {

                PageController target = new PageController("target");
                if (isMultiTaskingEnable)
                {
                    Thread oThread = new Thread(new ThreadStart(target.creatingProduct));

                    oThread.Start();
                }
                else
                {
                    target.creatingProduct();
                }
            }

            if (toyrusCkBbk.IsChecked == true)
            {
                
                PageController homeDepot = new PageController("homedepot");
                
                if (isMultiTaskingEnable)
                {
                    Thread oThread = new Thread(new ThreadStart(homeDepot.creatingProduct));

                    oThread.Start();
                }
                else
                {
                    homeDepot.creatingProduct();
                }
            }


            if (costcoCkbk.IsChecked == true)
            {

                PageController costco = new PageController("costco");

                if (isMultiTaskingEnable)
                {
                    Thread oThread = new Thread(new ThreadStart(costco.creatingProduct));

                    oThread.Start();
                }
                else
                {
                    costco.creatingProduct();
                }
            }

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //string createMasterProductTable = "create table MasterProductTable(productid INTEGER PRIMARY KEY,MasterProductName string)";
           //  string createPriceTable = "create table PriceTable(id INTEGER PRIMARY KEY,vendorid string,productid string,productname string,price string,sellerranking string)";

                MarketDatabaseOperations db = new MarketDatabaseOperations("No Operations");
                string insertMasterRecordsQuery = @"select p.vendorid as Vendor,t.productid as ProductId,p.productid as MasterProductname,p.productname as SearchResultProductName,p.price as PriceIn$,p.sellerranking as SellerRanking from PriceTable as p inner join MasterProductTable as t on p.productid=t.MasterProductName";//  PriceTable";  //MasterProductTable
                dataGrid1.DataContext = db.DataBaseGetResults(insertMasterRecordsQuery).DefaultView;
        }

    /*    private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow Myrow in dataGrid1.Rows)
            {            //Here 2 cell is target value and 1 cell is Volume
                if (Convert.ToInt32(Myrow.Cells[2].Value) < Convert.ToInt32(Myrow.Cells[1].Value))// Or your condition 
                {
                    Myrow.DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    Myrow.DefaultCellStyle.BackColor = Color.Green;
                }
            }
        }*/
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        

    
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


      private double Compare(string a, string b)
        {
            var aWords = a.Split(' ');
            var bWords = b.Split(' ');
            double matches = (double)aWords.Count(x => bWords.Contains(x));
            return matches / (double)aWords.Count();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string query = @"select p.productid,m.vendorid,m.productname,m.price from PriceTable as m inner join MasterProductTable as p on m.productid=p.MasterProductName where m.productid in(select productid from PriceTable where vendorid='Amazon' and productname!='Exception Product Name' and price!='Exception In Price') order by p.productid;";

            DataTable table = new DataTable("Orders");
            table.Columns.Add("ProductID", typeof(Int32));
            table.Columns.Add("Vendor", typeof(string));
            table.Columns.Add("ProductName", typeof(string));
            table.Columns.Add("Price", typeof(string));
            table.Columns.Add("ProductName@Amazon", typeof(string));
            table.Columns.Add("AmazonPrice", typeof(string));
            table.Columns.Add("AlgoMatch", typeof(string));



         MarketDatabaseOperations db = new MarketDatabaseOperations("No Operations");
                string insertMasterRecordsQuery = query;//  PriceTable";  //MasterProductTable
           //     dataGrid1.DataContext = db.DataBaseGetResults(insertMasterRecordsQuery).DefaultView;
              int margin=50;
             
           
            try
            {
                margin = Convert.ToInt32(marginTextbox.Text.Trim());
                if (margin > 0 && margin <100)
                {

                }
                else
                {
                    margin = 50;
                }
            }
            catch  (Exception)
            {
                margin = 50;
            }

                string expression = "vendorid= 'Amazon'";
                // string expression = "OrderQuantity = 2 and OrderID = 2";

                // Sort descending by column named CompanyName.
                string sortOrder = "productid ASC";
                DataRow[] foundRows;

                // Use the Select method to find all rows matching the filter.
                foundRows = db.DataBaseGetResults(insertMasterRecordsQuery).Select(expression, sortOrder);

                // Print column 0 of each returned row.
                for (int i = 0; i < foundRows.Length; i++)
                {
                    // now you have found the element with amazon find out the id and price 
                    /// then look for items with the same id and not amazon and then find its price and compare with the price that you have found 
                    /// and if this increase the criteria then you have to add this to the data table of must buy .
                    double priceatamazon = 0.0;
                    int id = Convert.ToInt32(foundRows[i][0]);
                    try
                    {
                     priceatamazon = Convert.ToDouble(foundRows[i][3]);
                    }
                    catch (System.FormatException)
                    {

                        Logger.log("Fomatting exception for" + foundRows[i][3].ToString());
                    }
                   
                    DataRow[] resultsWithSameItemId;
                    string express = Convert.ToString(id);
                    string querytoRun = "productid=" + "'" + express + "'" + "and vendorid <> 'Amazon'";
                    resultsWithSameItemId = db.DataBaseGetResults(insertMasterRecordsQuery).Select(querytoRun, sortOrder);
                    for (int j = 0; j < resultsWithSameItemId.Length; j++)
                    {
                        double pricewithvendor = 0;
                        try
                        {
                           pricewithvendor = Convert.ToDouble(resultsWithSameItemId[j][3]);
                        }
                        catch (System.FormatException)
                        {
                            Logger.log("Fomatting exception for" + resultsWithSameItemId[j][3].ToString());
                         
                        }
                       
                        if (pricewithvendor > priceatamazon || pricewithvendor==0)
                        {
                            continue;
                        }
                        else
                        {

                            if (((pricewithvendor / priceatamazon) * 100) <= margin)
                            {
                               
                                    DataRow newRow = table.NewRow();
                                    newRow["ProductID"] = resultsWithSameItemId[j][0];
                                    newRow["Vendor"] = resultsWithSameItemId[j][1];
                                    newRow["ProductName"] = resultsWithSameItemId[j][2];
                                    newRow["Price"] = resultsWithSameItemId[j][3];
                                    newRow["ProductName@Amazon"] = foundRows[i][2];
                                    newRow["AmazonPrice"] = foundRows[i][3];
                                    newRow["AlgoMatch"] = (((Math.Round(Compare(resultsWithSameItemId[j][2].ToString(), foundRows[i][2].ToString()), 2) + Math.Round(Compare(foundRows[i][2].ToString(), resultsWithSameItemId[j][2].ToString()), 2)) / 2) * 100).ToString();
                                    // Add the row to the rows collection.
                                    table.Rows.Add(newRow);
                                /*
                                    DataRow anewRow = table.NewRow();
                                    anewRow["ItemID"] = foundRows[i][0];
                                    anewRow["Vendor"] = foundRows[i][1];
                                    anewRow["ProductName"] = foundRows[i][2];
                                    anewRow["Price"] = foundRows[i][3];
                                    anewRow["AlgoMatch"] = (Math.Round(Compare(resultsWithSameItemId[j][2].ToString(), foundRows[i][2].ToString()), 2) * 100).ToString();
                                    // Add the row to the rows collection.
                                    table.Rows.Add(anewRow);
                                */

                            }

                        }

                    }

                }

                dataGrid1.DataContext = table.DefaultView;
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void checkBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
