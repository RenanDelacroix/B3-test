using Xunit;
using Moq;
using Microsoft.Extensions.Options;
using App.AplicationCore.Interfaces;
using App.AplicationCore.Services;
using App.CrossCutting.Settings.Options;
using ApiResult;
using App.AplicationCore.DTOs;

namespace App.Tests
{
    public class InvestmentCalculatorServiceTests
    {
        private const string validationValuesMsg = "Valor monetário e meses não podem ser menores que 1 (Um)";
        private const double TB = 1.08;
        private const double CDI = 0.009;
        private const double UpTo6Months = 0.2;
        private const double UpTo12Months = 0.2;
        private const double UpTo24Months = 0.175;
        private const double MoreThan24Months = 0.15;


        [Fact]
        public async Task CalculateCdbSimulation_ValidValues_ReturnsSuccessResult()
        {
            // Arrange
            var feePercentageOptions = Options.Create(new FeePercentageOptions { CDI = (decimal)CDI, TB = (decimal)TB });
            var taxPercentageOptions = Options.Create(new TaxPercentageOptions { UpTo6Months = (decimal)UpTo6Months, UpTo12Months = (decimal)UpTo12Months, UpTo24Months = (decimal)UpTo24Months, MoreThan24Months = (decimal)MoreThan24Months });
            var investmentCalculatorService = new InvestmentCalculatorService(feePercentageOptions, taxPercentageOptions);

            // Act
            var result = await investmentCalculatorService.CalculateCdbSimulation(1000, 12);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            
        }

        [Fact]
        public async Task CalculateCdbSimulation_InvalidValues_ReturnsFailureResult()
        {
            // Arrange
            var feePercentageOptions = Options.Create(new FeePercentageOptions { CDI = (decimal)CDI, TB = (decimal)TB });
            var taxPercentageOptions = Options.Create(new TaxPercentageOptions { UpTo6Months = (decimal)UpTo6Months, UpTo12Months = (decimal)UpTo12Months, UpTo24Months = (decimal)UpTo24Months, MoreThan24Months = (decimal)MoreThan24Months });
            var investmentCalculatorService = new InvestmentCalculatorService(feePercentageOptions, taxPercentageOptions);

            // Act
            var result = await investmentCalculatorService.CalculateCdbSimulation(-100, 0);

            // Assert
            Assert.False(result.Success);
            Assert.IsType<CdbResultDTO>(result.Data);
            Assert.Equal(validationValuesMsg, result.Message);
        }


        [Theory]
        [InlineData(1000, 12, 1098.46)]
        public async Task CalculateCdbSimulation_ValidValues_ReturnsExpectedNetAmount(decimal amount, int months, decimal expectedNetAmount)
        {
            // Arrange
            var feePercentageOptions = Options.Create(new FeePercentageOptions { CDI = (decimal)CDI, TB = (decimal)TB});
            var taxPercentageOptions = Options.Create(new TaxPercentageOptions { UpTo6Months = (decimal)UpTo6Months, UpTo12Months = (decimal)UpTo12Months, UpTo24Months = (decimal)UpTo24Months, MoreThan24Months = (decimal)MoreThan24Months});
            var investmentCalculatorService = new InvestmentCalculatorService(feePercentageOptions, taxPercentageOptions);

            // Act
            var result = await investmentCalculatorService.CalculateCdbSimulation(amount, months);

            // Assert
            Assert.Equal(expectedNetAmount, result.Data.NetAmount, precision: 2);
        }



    }
}
