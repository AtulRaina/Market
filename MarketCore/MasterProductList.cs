using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketCore
{
   public class MasterProductList
    {
        public int id { get; set; }
        public string masterproductName { get; set; }
        public string masterproductPrice { get; set; }
        public MasterProductList(int vendorid,string MasterProductName,string MasterProuctPrice)
        {
            id = vendorid;
            masterproductName = MasterProductName;
            masterproductPrice = MasterProuctPrice;

        }
    }
}
