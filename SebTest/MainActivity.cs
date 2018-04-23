using Android.App;
using Android.Widget;
using Android.OS;
using SebTest.Source;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace SebTest
{
    [Activity(Label = "SebTest", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private TextView amountText;
        private List<Transaction> transactionsList = new List<Transaction>();
        private List<string> names = new List<string>();
        ExpandableListViewAdapter adapter;
        ExpandableListView expandableListView;
        Dictionary<string, Transaction> mDictionary = new Dictionary<string, Transaction>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            amountText = FindViewById<TextView>(Resource.Id.Amount);
            
            setData(out adapter);
            amountText.Text = getAmount().ToString() + "$";

            expandableListView = FindViewById<ExpandableListView>(Resource.Id.expandableList);
            expandableListView.SetAdapter(adapter);
        }
        
        public double getAmount()
        {
            double sum = 0;
            for(int i =0; i<transactionsList.Count; i++)
            {
                sum = sum + transactionsList[i].amount;
            }
            return sum;
        }

        public void setData(out ExpandableListViewAdapter adapter)
        {


            List<Transaction> transaction = getData();

            for(int i =0; i<transaction.Count; i++)
            {
                transactionsList.Add(new Transaction
                {
                    beneficiaryName = transaction[i].beneficiaryName,
                    beneficiaryAccount = transaction[i].beneficiaryAccount,
                    details = transaction[i].details,
                    amount = transaction[i].amount
                });
            }
            
            for(int i = 0; i< transactionsList.Count; i++)
            {
                names.Add(transactionsList[i].beneficiaryName);
                mDictionary.Add(transactionsList[i].beneficiaryName, transactionsList[i]);
                
            }
            adapter = new ExpandableListViewAdapter(this, names, mDictionary);
           
        }
        public List<Transaction> getData()
        {
            string url = @"https://sheetsu.com/apis/v1.0su/979ef3ba5632";
            var json = new WebClient().DownloadString(url);
            List<Transaction> transaction = JsonConvert.DeserializeObject<List<Transaction>>(json);
            return transaction;

        }
    }
}

