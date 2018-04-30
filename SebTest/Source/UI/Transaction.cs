using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SebTest.Source
{

    public class Transaction
    {
        //beneficiary name
        public string BeneficiaryName { get; set; }
        //beneficiaryAccount
        public string BeneficiaryAccount { get; set; }
        //details
        public string Details { get; set; }
        //amount
        public double Amount { get; set; }

    }
}