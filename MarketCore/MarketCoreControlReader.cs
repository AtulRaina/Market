using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace MarketCore
{
  
    /// <summary>
    ///Based on the object that you would pass to me 
    // I will update my properties with the values of object from xml
    // Then u can use my property where ever you want these values
    //<pageurl></pageurl>
    //  <searchButton></searchButton>
    //  <searchclick></searchclick>
    //  <productname></productname>
    //  <productprice></productprice>
    //  <pagelength></pagelength>
     // <productmaster></productmaster>
     // <productmasterprice></productmasterprice>
    /// </summary>
    public class MarketCoreControlReader
    {
        public string pageUrl { get; set; }
        public string searchButton { get; set; }
        public string searchClick { get; set; }
        public string productName { get; set; }
        public string productPrice { get; set; }
        public string pageLength { get; set; }
        public string productMasterName { get; set; }
        public string productMasterPrice { get; set; }
      
        public MarketCoreControlReader(string vendor)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"webcontrol.xml");

            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/root/pageControls");
            foreach (XmlNode node in nodeList)
            {
                if (vendor == node.SelectSingleNode("vendor").InnerText)
                {
                    pageUrl = node.SelectSingleNode("pageurl").InnerText;
                    searchButton = node.SelectSingleNode("searchButton").InnerText;
                    searchClick = node.SelectSingleNode("searchclick").InnerText;
                    productName = node.SelectSingleNode("productname").InnerText;
                    productPrice = node.SelectSingleNode("productprice").InnerText;
                    pageLength = node.SelectSingleNode("pagelength").InnerText;
                    productMasterName = node.SelectSingleNode("productmaster").InnerText;
                    productMasterPrice = node.SelectSingleNode("productmasterprice").InnerText;

                }
            }

        }

    }
}
