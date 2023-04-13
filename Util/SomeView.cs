using System;
using System.Collections.Generic;
using System.Text;

namespace appSGSales2.Util
{
    public class SomeView
    {
        public SomeView()
        { }

        private decimal _decimalConverter;
        public decimal DecimalConverter
        {
            get => _decimalConverter;
            set
            {
                _decimalConverter = value;
                OnPropertyChanged(nameof(DecimalConverter));
            }
        }

        private void OnPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }
    }


}
