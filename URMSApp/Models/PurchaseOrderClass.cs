using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using URMSApp.Common;

namespace URMSApp
{
    public class PurchaseOrderClass: BindableBase
    {

        private string _ponumber;

        public string PONumber
        {
            get { return _ponumber; }
            set
            {
                _ponumber = value;
                SetProperty(ref _ponumber,value);
            }
        }

        private string _poto;

        public string POTo
        {
            get { return _poto; }
            set
            {
                _poto = value;
                SetProperty(ref _poto, value);
            }
        }

        private string _podate;

        public string PODate
        {
            get { return _podate; }
            set
            {
                _podate = value;
                SetProperty(ref _podate, value);
            }
        }

        private string _postatus;

        public string POStatus
        {
            get { return _postatus; }
            set
            {
                _postatus = value;
                SetProperty(ref _postatus, value);
            }
        }
    }
}
