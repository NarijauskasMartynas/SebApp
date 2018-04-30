using Android.App;
using Android.Widget;
using Android.OS;
using SebTest.Source;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using SebTest.Source.Controllers;

namespace SebTest
{
    [Activity(Label = "SebTest", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private TextView amountText;
        private List<Transaction> transactionsList = new List<Transaction>();
        private List<string> names = new List<string>();
        private ExpandableListViewAdapter adapter;
        private ExpandableListView expandableListView;
        private Dictionary<string, Transaction> mDictionary = new Dictionary<string, Transaction>();
        private Storage storage = new Storage();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            transactionsList = storage.getTransactions();
            amountText = FindViewById<TextView>(Resource.Id.Amount);
            if(transactionsList == null)
            {
                amountText.Text = GetString(Resource.String.error);
            }
            else
            {
                amountText.Text = GetString(Resource.String.amount) + GetAmount().ToString() + "$";
                SetData(out adapter, transactionsList);
                expandableListView = FindViewById<ExpandableListView>(Resource.Id.expandableList);
                expandableListView.SetAdapter(adapter);
            }
      
           
        }
        
        public double GetAmount()
        {
            double sum = 0;
            for(int i =0; i<transactionsList.Count; i++)
            {
                sum = sum + transactionsList[i].Amount;
            }
            return sum;
        }

        public void SetData(out ExpandableListViewAdapter adapter, List<Transaction> transaction)
        {

            for(int i = 0; i< transaction.Count; i++)
            {
                names.Add(transaction[i].BeneficiaryName);
                mDictionary.Add(transaction[i].BeneficiaryName, transaction[i]);
                
            }
            adapter = new ExpandableListViewAdapter(this, names, mDictionary);
           
        }
    }
}

