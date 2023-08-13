using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CrossCutting.Settings.Options
{
    public class TaxPercentageOptions
    {
        public decimal UpTo6Months { get; set; }
        public decimal UpTo12Months { get; set; }
        public decimal UpTo24Months { get; set; }
        public decimal MoreThan24Months { get; set; }
    }
}
