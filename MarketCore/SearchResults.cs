using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketCore
{
  public  class SearchResults
    {
        public string searchMasterString { get; set; }
        public string searchResultName { get; set; }
        public string searchResultPrice { get; set; }

        public SearchResults(string a,string b,string c)
        {
            searchMasterString = a;
            searchResultName = b;
            searchResultPrice = c;
        }
    }
}
