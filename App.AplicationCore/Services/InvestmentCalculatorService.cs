using ApiResult;
using ApiResult.Interfaces;
using App.AplicationCore.DTOs;
using App.AplicationCore.Interfaces;
using App.CrossCutting.Settings.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.AplicationCore.Services
{
    public class InvestmentCalculatorService : IInvestmentCalculatorService
    {
        private readonly FeePercentageOptions feePercentageOptions;
        private readonly TaxPercentageOptions taxPercentageOptions;
        private const string validationValuesMsg = "Valor monetário e meses não podem ser menores que 1 (Um)";
        public InvestmentCalculatorService(IOptions<FeePercentageOptions> feePercentageOptions, IOptions<TaxPercentageOptions> taxPercentageOptions)
        {
            this.feePercentageOptions = feePercentageOptions.Value;
            this.taxPercentageOptions = taxPercentageOptions.Value;
        }

        public async Task<IApiResult<CdbResultDTO>> CalculateCdbSimulation(decimal amount, int months)
        {
            if(!ValidateValues(amount, months))
                return await Task.FromResult(new ApiResult<CdbResultDTO>(false, new CdbResultDTO(), validationValuesMsg));

            decimal initialAmount = amount;
            decimal grossAmount = CalculateAmount(amount, months);
            decimal netAmount = DecreaseTaxes(initialAmount, grossAmount, months);

            return await Task.FromResult(new ApiResult<CdbResultDTO>(true, new CdbResultDTO (netAmount, grossAmount)));
        }

        
        private decimal CalculateAmount(decimal amount, int months)
        {
            //Fórmula por mês: VF = VI x [1 + CDI X TB]
            decimal initialAmount;
            for (int count = 0; count < months; count++)
            {
                initialAmount = Math.Round(amount * (1 + feePercentageOptions.CDI * feePercentageOptions.TB), 2, MidpointRounding.AwayFromZero);
                amount = initialAmount;
                initialAmount = 0;
            }
            return amount;
        }

        private decimal DecreaseTaxes(decimal initialAmount, decimal grossAmount, int months)
        {
            decimal profits = grossAmount - initialAmount;
            decimal netAmount;

            if (months >= 1 && months <= 6)
                netAmount = Math.Round(grossAmount - (profits * taxPercentageOptions.UpTo6Months),2, MidpointRounding.AwayFromZero);
            else if(months > 6 &&  months <= 12)
                netAmount = Math.Round(grossAmount - (profits * taxPercentageOptions.UpTo12Months), 2, MidpointRounding.AwayFromZero);
            else if(months > 12 && months <= 24)
                netAmount = Math.Round(grossAmount - (profits * taxPercentageOptions.UpTo24Months), 2, MidpointRounding.AwayFromZero);
            else
                netAmount = Math.Round(grossAmount - (profits * taxPercentageOptions.MoreThan24Months), 2, MidpointRounding.AwayFromZero);

            return netAmount;
        }

        private bool ValidateValues (decimal amount, int months) 
        { 
            if (amount < 1 || months < 1)
                return false;

            return true;
        }
    }
}
