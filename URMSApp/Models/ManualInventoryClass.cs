using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URMSApp.Common;

namespace URMSApp.Models
{
    public class ManualInventoryClass:BindableBase
    {
        private string _invid;

        public string INVId
        {
            get { return _invid; }
            set
            {
                _invid = value;
                SetProperty(ref _invid, value);
            }
        }

        private string _invcode;

        public string INVCode
        {
            get { return _invcode; }
            set
            {
                _invcode = value;
                SetProperty(ref _invcode, value);
            }
        }

        private string _invstoreid;

        public string INVStoreId
        {
            get { return _invstoreid; }
            set
            {
                _invstoreid = value;
                SetProperty(ref _invstoreid, value);
            }
        }

        private string _invdescription;

        public string INVDescription
        {
            get { return _invdescription; }
            set
            {
                _invdescription = value;
                SetProperty(ref _invdescription, value);
            }
        }

        private string _openeddate;

        public string INVDate
        {
            get { return _openeddate; }
            set
            {
                _openeddate = value;
                SetProperty(ref _openeddate, value);
            }
        }

        private string _invstatus;

        public string INVStatus
        {
            get { return _invstatus; }
            set
            {
                _invstatus = value;
                SetProperty(ref _invstatus, value);
            }
        }
    }
}
