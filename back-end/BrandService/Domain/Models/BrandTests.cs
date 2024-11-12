using Xunit;

namespace BrandService.Domain.Models
{
    public class BrandTests
    {
        [Fact(DisplayName = "Deve criar uma marca corretamente usando o construtor")]
        public void Brand_Constructor_ShouldCreateBrand()
        {
            // Arrange
            var name = "Apple";
            var owner = "Owner A";
            var description = "Description A";

            // Act
            var brand = new Brand(name, owner, description);

            // Assert
            Assert.NotNull(brand);
            Assert.Equal(name, brand.Name);
            Assert.Equal(owner, brand.Owner);
            Assert.Equal(description, brand.Description);
        }

        [Fact(DisplayName = "Deve atualizar os valores da marca corretamente")]
        public void Brand_Update_ShouldUpdateBrandProperties()
        {
            // Arrange
            var brand = new Brand("Apple", "Owner A", "Description A");

            // Act
            brand.Update("Google", "Owner B", "Description B");

            // Assert
            Assert.Equal("Google", brand.Name);
            Assert.Equal("Owner B", brand.Owner);
            Assert.Equal("Description B", brand.Description);
        }

        [Fact(DisplayName = "Deve validar uma marca válida")]
        public void Brand_Validate_ShouldReturnTrue_WhenValidBrand()
        {
            // Arrange
            var brand = new Brand("Apple", "Owner A", "Description A");

            // Act
            var isValid = brand.Validate();

            // Assert
            Assert.True(isValid);
        }

        [Fact(DisplayName = "Deve invalidar uma marca com nome vazio")]
        public void Brand_Validate_ShouldReturnFalse_WhenInvalidBrand()
        {
            // Arrange
            var brand = new Brand("", "Owner A", "Description A");

            // Act
            var isValid = brand.Validate();

            // Assert
            Assert.False(isValid);
        }

        [Fact(DisplayName = "Deve gerar a string corretamente com o nome da marca")]
        public void Brand_ToString_ShouldReturnBrandName()
        {
            // Arrange
            var brand = new Brand("Apple", "Owner A", "Description A");

            // Act
            var result = brand.ToString();

            // Assert
            Assert.Equal("Apple", result);
        }
    }
}
