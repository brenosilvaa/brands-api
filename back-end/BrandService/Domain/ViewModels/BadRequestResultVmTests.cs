using Xunit;

namespace BrandService.Domain.ViewModels
{
    public class BadRequestResultVmTests
    {
        [Fact]
        public void ErrorMessage_ShouldBeInitializedAsNull()
        {
            // Arrange & Act
            var badRequestResult = new BadRequestResultVm();

            // Assert
            Assert.Null(badRequestResult.ErrorMessage);
        }

        [Fact]
        public void ErrorMessage_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var badRequestResult = new BadRequestResultVm();
            var errorMessage = "Invalid request data";

            // Act
            badRequestResult.ErrorMessage = errorMessage;

            // Assert
            Assert.Equal(errorMessage, badRequestResult.ErrorMessage);
        }
    }
}
