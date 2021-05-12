using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URMSApp.Common;

namespace URMSApp.Models
{
    public class InventoryItemClass:BindableBase
    {
        private string _itemLookupCode;

        public string ItemLookupCode
        {
            get { return _itemLookupCode; }
            set { SetProperty(ref _itemLookupCode, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _departmentid;

        public string DepartmentID
        {
            get { return _departmentid; }
            set { SetProperty(ref _departmentid, value); }
        }
        private string _department;

        public string Department
        {
            get { return _department; }
            set { SetProperty(ref _department, value); }
        }

        private string _categoryid;

        public string CategoryID
        {
            get { return _categoryid; }
            set { SetProperty(ref _categoryid, value); }
        }

        private string _category;

        public string Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private string _counted;

        public string Counted
        {
            get { return _counted; }
            set { SetProperty(ref _counted, value); }
        }


        private string _entryid;

        public string EntryID
        {
            get { return _entryid; }
            set { SetProperty(ref _entryid, value); }
        }

        public InventoryItemClass()
        {

        }
    }
}
