using App.Api.Controllers.V1;
using App.AplicationCore.Interfaces;
using ApiResult;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using App.AplicationCore.DTOs;

namespace App.Tests.Controllers
{
    public class CalculatorControllerTests
    {
        [Fact]
        public async Task GetCdbSimulation_ValidValues_ReturnsOkResult()
        {
            // Arrange
            var investmentCalculatorServiceMock = new Mock<IInvestmentCalculatorService>();
            var expectedResult = new ApiResult<CdbResultDTO>(true, new CdbResultDTO());
            investmentCalculatorServiceMock.Setup(s => s.CalculateCdbSimulation(It.IsAny<decimal>(), It.IsAny<int>()))
                                          .ReturnsAsync(expectedResult);

            var controller = new CalculatorController(investmentCalculatorServiceMock.Object);

            // Act
            var result = await controller.GetCdbSimulation(1000, 12);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResult = Assert.IsType<ApiResult<CdbResultDTO>>(okResult.Value);
            Assert.True(apiResult.Success);
        }

        [Fact]
        public async Task GetCdbSimulation_InvalidValues_ReturnsBadRequestResult()
        {
            // Arrange
            var investmentCalculatorServiceMock = new Mock<IInvestmentCalculatorService>();
            var expectedResult = new ApiResult<CdbResultDTO>(false, new CdbResultDTO(), "Validation error");
            investmentCalculatorServiceMock.Setup(s => s.CalculateCdbSimulation(It.IsAny<decimal>(), It.IsAny<int>()))
                                          .ReturnsAsync(expectedResult);

            var controller = new CalculatorController(investmentCalculatorServiceMock.Object);

            // Act
            var result = await controller.GetCdbSimulation(-100, 0);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var apiResult = Assert.IsType<ApiResult<CdbResultDTO>>(badRequestResult.Value);
            Assert.False(apiResult.Success);
            Assert.Equal("Validation error", apiResult.Message);
        }
    }
}
