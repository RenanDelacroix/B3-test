using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.AplicationCore.DTOs
{
    public class CdbResultDTO
    {
        public CdbResultDTO(decimal netAmount, decimal grossAmount)
        {
            this.NetAmount = netAmount;
            this.GrossAmount = grossAmount;
        }
        public CdbResultDTO(){}

        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
    }
}
