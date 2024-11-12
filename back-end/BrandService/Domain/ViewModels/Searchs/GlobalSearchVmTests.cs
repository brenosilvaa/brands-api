using BrandService.Domain.ViewModels.Brands;
using Xunit;

namespace BrandService.Domain.ViewModels.Searchs
{
    public class GlobalSearchVmTests
    {
        [Fact]
        public void Constructor_ShouldInitializeWithEmptyList_WhenNoBrandsProvided()
        {
            // Arrange & Act
            var globalSearchVm = new GlobalSearchVm();

            // Assert
            Assert.Null(globalSearchVm.Brands);
        }

        [Fact]
        public void Constructor_ShouldInitializeWithProvidedBrandsList()
        {
            // Arrange
            var brands = new List<BrandVm>
            {
                new BrandVm { Id = 1, Name = "Brand 1" },
                new BrandVm { Id = 2, Name = "Brand 2" }
            };

            // Act
            var globalSearchVm = new GlobalSearchVm(brands);

            // Assert
            Assert.NotNull(globalSearchVm.Brands);
            Assert.Equal(2, globalSearchVm.Brands?.Count);
            Assert.Equal("Brand 1", globalSearchVm.Brands?[0].Name);
            Assert.Equal("Brand 2", globalSearchVm.Brands?[1].Name);
        }

        [Fact]
        public void BrandsProperty_ShouldAllowModificationAfterInitialization()
        {
            // Arrange
            var brands = new List<BrandVm>
            {
                new BrandVm { Id = 1, Name = "Brand 1" }
            };
            var globalSearchVm = new GlobalSearchVm(brands);

            // Act
            var newBrands = new List<BrandVm>
            {
                new BrandVm { Id = 2, Name = "Brand 2" }
            };
            globalSearchVm.Brands = newBrands;

            // Assert
            Assert.NotNull(globalSearchVm.Brands);
            Assert.Single(globalSearchVm.Brands);
            Assert.Equal("Brand 2", globalSearchVm.Brands?[0].Name);
        }
    }
}
