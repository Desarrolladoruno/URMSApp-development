using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URMSApp.Common;

namespace URMSApp.Models
{
    public class QuantityDiscounts:BindableBase
    {

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { SetProperty(ref _Description, value); }
        }

        private string _HQID;
        public string HQID
        {
            get { return _HQID; }
            set { SetProperty(ref _HQID, value); }
        }

        private string _ID;
        public string ID
        {
            get { return _ID; }
            set { SetProperty(ref _ID, value); }
        }

        private string _DiscountOddItems;
        public string DiscountOddItems
        {
            get { return _DiscountOddItems; }
            set { SetProperty(ref _DiscountOddItems, value); }
        }

        private string _Quantity1;
        public string Quantity1
        {
            get { return _Quantity1; }
            set { SetProperty(ref _Quantity1, value); }
        }

        private string _Price1;
        public string Price1
        {
            get { return _Price1; }
            set { SetProperty(ref _Price1, value); }
        }

        private string _Price1A;
        public string Price1A
        {
            get { return _Price1A; }
            set { SetProperty(ref _Price1A, value); }
        }

        private string _Price1B;
        public string Price1B
        {
            get { return _Price1B; }
            set { SetProperty(ref _Price1B, value); }
        }

        private string _Price1C;
        public string Price1C
        {
            get { return _Price1C; }
            set { SetProperty(ref _Price1C, value); }
        }

        private string _Quantity2;
        public string Quantity2
        {
            get { return _Quantity2; }
            set { SetProperty(ref _Quantity2, value); }
        }

        private string _Price2;
        public string Price2
        {
            get { return _Price2; }
            set { SetProperty(ref _Price2, value); }
        }

        private string _Price2A;
        public string Price2A
        {
            get { return _Price2A; }
            set { SetProperty(ref _Price2A, value); }
        }

        private string _Price2B;
        public string Price2B
        {
            get { return _Price2B; }
            set { SetProperty(ref _Price2B, value); }
        }

        private string _Price2C;
        public string Price2C
        {
            get { return _Price2C; }
            set { SetProperty(ref _Price2C, value); }
        }

        private string _Quantity3;
        public string Quantity3
        {
            get { return _Quantity3; }
            set { SetProperty(ref _Quantity3, value); }
        }

        private string _Price3;
        public string Price3
        {
            get { return _Price3; }
            set { SetProperty(ref _Price3, value); }
        }

        private string _Price3A;
        public string Price3A
        {
            get { return _Price3A; }
            set { SetProperty(ref _Price3A, value); }
        }

        private string _Price3B;
        public string Price3B
        {
            get { return _Price3B; }
            set { SetProperty(ref _Price3B, value); }
        }

        private string _Price3C;
        public string Price3C
        {
            get { return _Price3C; }
            set { SetProperty(ref _Price3C, value); }
        }

        private string _Quantity4;
        public string Quantity4
        {
            get { return _Quantity4; }
            set { SetProperty(ref _Quantity4, value); }
        }

        private string _Price4;
        public string Price4
        {
            get { return _Price4; }
            set { SetProperty(ref _Price4, value); }
        }

        private string _Price4A;
        public string Price4A
        {
            get { return _Price4A; }
            set { SetProperty(ref _Price4A, value); }
        }

        private string _Price4B;
        public string Price4B
        {
            get { return _Price4B; }
            set { SetProperty(ref _Price4B, value); }
        }

        private string _Price4C;
        public string Price4C
        {
            get { return _Price4C; }
            set { SetProperty(ref _Price4C, value); }
        }

        private string _DBTimeStamp;
        public string DBTimeStamp
        {
            get { return _DBTimeStamp; }
            set { SetProperty(ref _DBTimeStamp, value); }
        }

        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { SetProperty(ref _Type, value); }
        }

        private string _PercentOffPrice1;
        public string PercentOffPrice1
        {
            get { return _PercentOffPrice1; }
            set { SetProperty(ref _PercentOffPrice1, value); }
        }

        private string _PercentOffPrice1A;
        public string PercentOffPrice1A
        {
            get { return _PercentOffPrice1A; }
            set { SetProperty(ref _PercentOffPrice1A, value); }
        }

        private string _PercentOffPrice1B;
        public string PercentOffPrice1B
        {
            get { return _PercentOffPrice1B; }
            set { SetProperty(ref _PercentOffPrice1B, value); }
        }

        private string _PercentOffPrice1C;
        public string PercentOffPrice1C
        {
            get { return _PercentOffPrice1C; }
            set { SetProperty(ref _PercentOffPrice1C, value); }
        }

        private string _PercentOffPrice2;
        public string PercentOffPrice2
        {
            get { return _PercentOffPrice2; }
            set { SetProperty(ref _PercentOffPrice2, value); }
        }

        private string _PercentOffPrice2A;
        public string PercentOffPrice2A
        {
            get { return _PercentOffPrice2A; }
            set { SetProperty(ref _PercentOffPrice2A, value); }
        }

        private string _PercentOffPrice2B;
        public string PercentOffPrice2B
        {
            get { return _PercentOffPrice2B; }
            set { SetProperty(ref _PercentOffPrice2B, value); }
        }

        private string _PercentOffPrice2C;
        public string PercentOffPrice2C
        {
            get { return _PercentOffPrice2C; }
            set { SetProperty(ref _PercentOffPrice2C, value); }
        }

        private string _PercentOffPrice3;
        public string PercentOffPrice3
        {
            get { return _PercentOffPrice3; }
            set { SetProperty(ref _PercentOffPrice3, value); }
        }

        private string _PercentOffPrice3A;
        public string PercentOffPrice3A
        {
            get { return _PercentOffPrice3A; }
            set { SetProperty(ref _PercentOffPrice3A, value); }
        }

        private string _PercentOffPrice3B;
        public string PercentOffPrice3B
        {
            get { return _PercentOffPrice3B; }
            set { SetProperty(ref _PercentOffPrice3B, value); }
        }

        private string _PercentOffPrice3C;
        public string PercentOffPrice3C
        {
            get { return _PercentOffPrice3C; }
            set { SetProperty(ref _PercentOffPrice3C, value); }
        }

        private string _PercentOffPrice4;
        public string PercentOffPrice4
        {
            get { return _PercentOffPrice4; }
            set { SetProperty(ref _PercentOffPrice4, value); }
        }

        private string _PercentOffPrice4A;
        public string PercentOffPrice4A
        {
            get { return _PercentOffPrice4A; }
            set { SetProperty(ref _PercentOffPrice4A, value); }
        }

        private string _PercentOffPrice4B;
        public string PercentOffPrice4B
        {
            get { return _PercentOffPrice4B; }
            set { SetProperty(ref _PercentOffPrice4B, value); }
        }

        private string _PercentOffPrice4C;
        public string PercentOffPrice4C
        {
            get { return _PercentOffPrice4C; }
            set { SetProperty(ref _PercentOffPrice4C, value); }
        }
        public QuantityDiscounts()
        {

        }
    }
}
