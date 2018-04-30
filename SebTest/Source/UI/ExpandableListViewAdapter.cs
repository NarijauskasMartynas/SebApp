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
using Java.Lang;

namespace SebTest.Source
{
    public class ExpandableListViewAdapter : BaseExpandableListAdapter
    {
        private Context context;
        private List<string> names;
        private Dictionary<string, Transaction> firstChild;

        public ExpandableListViewAdapter(Context context, List<string> names, Dictionary<string, Transaction> firstChild)
        {
            this.context = context;
            this.names = names;
            this.firstChild = firstChild;
        }

        public override int GroupCount
        {
            get
            {
                return names.Count;
            }
        }
        public override bool HasStableIds
        {
            get
            {
                return false;
            }
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            var result = new Transaction();
            firstChild.TryGetValue(names[groupPosition], out result);
            return result.BeneficiaryName + "*" + result.BeneficiaryAccount + "*" + result.Details + "*" + result.Amount;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return 1;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
           if(convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.transaction_layout, null);
            }
            TextView textViewName = convertView.FindViewById<TextView>(Resource.Id.transactionName);
            TextView textViewNr = convertView.FindViewById<TextView>(Resource.Id.transactionNr);
            TextView textViewDetails = convertView.FindViewById<TextView>(Resource.Id.transactionDetails);
            TextView textViewAmount = convertView.FindViewById<TextView>(Resource.Id.transactionAmount);
            string content = (string)GetChild(groupPosition, childPosition);
            string[] strings = content.Split('*');
            textViewName.Text = strings[0];
            textViewNr.Text = strings[1];
            textViewDetails.Text = strings[2];
            textViewAmount.Text = strings[3];
            return convertView;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            var result = new Transaction();
            firstChild.TryGetValue(names[groupPosition], out result);
            return names[groupPosition] + "*" + result.Amount;
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            if(convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.groupTransactions, null);
            }
            string textGroup = (string)GetGroup(groupPosition);
            string[] strings = textGroup.Split('*');
            TextView textViewGroup = convertView.FindViewById<TextView>(Resource.Id.groupName);
            TextView textViewGroupAmount = convertView.FindViewById<TextView>(Resource.Id.groupAmount);
            textViewGroup.Text = strings[0];
            textViewGroupAmount.Text = strings[1];
            double amount = Convert.ToDouble(strings[1]);
            if (amount >= 0)
            {
                textViewGroupAmount.SetTextColor(Android.Graphics.Color.Green);
            }
            else
            {
                textViewGroupAmount.SetTextColor(Android.Graphics.Color.Red);
            }
            
            return convertView;

        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}