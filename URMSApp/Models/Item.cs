using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URMSApp.Common;

namespace URMSApp.Models
{
    public class Item:BindableBase
    {
        private string _ItemLookupCode;

        public string ItemLookupCode
        {
            get { return _ItemLookupCode; }
            set { SetProperty(ref _ItemLookupCode, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _qtyorder;

        public string Qtyorder
        {
            get { return _qtyorder; }
            set { SetProperty(ref _qtyorder, value); }
        }

        private string _cost;

        public string Cost
        {
            get { return _cost; }
            set { SetProperty(ref _cost, value); }
        }

        private string _extcost;

        public string Extcost
        {
            get { return _extcost; }
            set { SetProperty(ref _extcost, value); }
        }

        private string _tax;

        public string Tax
        {
            get { return _tax; }
            set { SetProperty(ref _tax, value); }
        }

        private string _ipoid;
        public string IPOid
        {
            get { return _ipoid; }
            set { SetProperty(ref _ipoid, value); }
        }
        public Item()
        {

        }
    }
}
