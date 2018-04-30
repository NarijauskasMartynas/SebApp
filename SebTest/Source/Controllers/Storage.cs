using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Newtonsoft.Json;

namespace SebTest.Source.Controllers
{
    class Storage
    {
        public List<Transaction> getTransactions()
        {
            try
            {
                string url = @"https://sheetsu.com/apis/v1.0su/979ef3ba5632";
                var json = new WebClient().DownloadString(url);
                List<Transaction> transaction = JsonConvert.DeserializeObject<List<Transaction>>(json);
                return transaction;
            }
            catch
            {
                return null;
            }
        }
    }
}