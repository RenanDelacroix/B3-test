using ApiResult.Interfaces;
using App.AplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.AplicationCore.Interfaces
{
    public interface IInvestmentCalculatorService
    {
        Task<IApiResult<CdbResultDTO>> CalculateCdbSimulation(decimal amount, int months);
    }
}
