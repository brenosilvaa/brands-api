using AutoMapper;
using BrandService.App.Queries.Brands;
using BrandService.Domain.Contracts;
using BrandService.Domain.Models;
using BrandService.Domain.ViewModels.Brands;
using Moq;
using Xunit;

namespace BrandService.App.Tests
{
    public class BrandQueryTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly FindBrandByIdQuery.FindBrandByIdQueryHandler _handlerFind;
        private readonly ListBrandsQuery.ListBrandsQueryHandler _handlerList;

        public BrandQueryTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _handlerFind = new FindBrandByIdQuery.FindBrandByIdQueryHandler(_uowMock.Object, _mapperMock.Object);
            _handlerList = new ListBrandsQuery.ListBrandsQueryHandler(_uowMock.Object, _mapperMock.Object);
        }

        #region Tests FindBrandById

        [Fact(DisplayName = "Deve retornar a marca corretamente quando a marca for encontrada")]
        [Trait("Query", "FindBrandById")]
        public async Task Handle_ShouldReturnBrand_WhenBrandExists()
        {
            // Arrange
            var brandId = 1L;
            var query = new FindBrandByIdQuery { Id = brandId };
            var brand = new Brand("Test Brand", "Test Owner", "Test Description");  // Simulando a marca
            var brandVm = new BrandVm { Name = "Test Brand", Owner = "Test Owner", Description = "Test Description" };

            _uowMock.Setup(u => u.Brands.FindAsync(brandId)).ReturnsAsync(brand);  // Retorna a marca simulada
            _mapperMock.Setup(m => m.Map<BrandVm>(brand)).Returns(brandVm);  // Mapeia para o ViewModel

            // Act
            var result = await _handlerFind.Handle(query, CancellationToken.None);

            // Assert
            _uowMock.Verify(u => u.Brands.FindAsync(brandId), Times.Once);
            _mapperMock.Verify(m => m.Map<BrandVm>(brand), Times.Once);
            Assert.Equal(brandVm.Name, result.Name);
            Assert.Equal(brandVm.Owner, result.Owner);
            Assert.Equal(brandVm.Description, result.Description);
        }

        [Fact(DisplayName = "Deve retornar null quando a marca não for encontrada")]
        [Trait("Query", "FindBrandById")]
        public async Task Handle_ShouldReturnNull_WhenBrandDoesNotExist()
        {
            // Arrange
            var brandId = 999L;
            var query = new FindBrandByIdQuery { Id = brandId };

            _uowMock.Setup(u => u.Brands.FindAsync(brandId)).ReturnsAsync((Brand)null);  // Marca não encontrada

            // Act
            var result = await _handlerFind.Handle(query, CancellationToken.None);

            // Assert
            _uowMock.Verify(u => u.Brands.FindAsync(brandId), Times.Once);
            Assert.Null(result);  // Deve retornar null quando não encontrar a marca
        }

        [Fact(DisplayName = "Deve lançar exceção quando ocorrer um erro inesperado")]
        [Trait("Query", "FindBrandById")]
        public async Task Handle_ShouldThrowException_WhenErrorOccurs()
        {
            // Arrange
            var brandId = 1L;
            var query = new FindBrandByIdQuery { Id = brandId };

            _uowMock.Setup(u => u.Brands.FindAsync(brandId)).ThrowsAsync(new System.Exception("Erro inesperado"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.Exception>(() => _handlerFind.Handle(query, CancellationToken.None));
            Assert.Equal("Erro inesperado", exception.Message);  // Verifica se a exceção foi lançada
        }
        #endregion

        #region Tests ListBrands

        [Fact(DisplayName = "Deve retornar uma lista de marcas quando existirem marcas")]
        [Trait("Query", "ListBrands")]
        public async Task Handle_ShouldReturnBrands_WhenBrandsExist()
        {
            // Arrange
            var brands = new List<Brand>
            {
                new Brand("Brand 1", "Owner 1", "Description 1"),
                new Brand("Brand 2", "Owner 2", "Description 2")
            };
            var brandVmList = new List<BrandVm>
            {
                new BrandVm { Name = "Brand 1", Owner = "Owner 1", Description = "Description 1" },
                new BrandVm { Name = "Brand 2", Owner = "Owner 2", Description = "Description 2" }
            };

            _uowMock.Setup(u => u.Brands.ListAsync()).ReturnsAsync(brands);  // Retorna a lista simulada de marcas
            _mapperMock.Setup(m => m.Map<IList<BrandVm>>(brands)).Returns(brandVmList);  // Mapeia as marcas para ViewModels

            var query = new ListBrandsQuery();

            // Act
            var result = await _handlerList.Handle(query, CancellationToken.None);

            // Assert
            _uowMock.Verify(u => u.Brands.ListAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IList<BrandVm>>(brands), Times.Once);
            Assert.Equal(2, result.Count);  // Verifica se retornou 2 marcas
            Assert.Equal("Brand 1", result[0].Name);
            Assert.Equal("Brand 2", result[1].Name);
        }

        [Fact(DisplayName = "Deve retornar uma lista vazia quando não existirem marcas")]
        [Trait("Query", "ListBrands")]
        public async Task Handle_ShouldReturnEmptyList_WhenNoBrandsExist()
        {
            // Arrange
            var brands = new List<Brand>();  // Nenhuma marca
            var brandVmList = new List<BrandVm>();  // Lista vazia de ViewModels

            _uowMock.Setup(u => u.Brands.ListAsync()).ReturnsAsync(brands);  // Retorna lista vazia de marcas
            _mapperMock.Setup(m => m.Map<IList<BrandVm>>(brands)).Returns(brandVmList);  // Mapeia para lista vazia de ViewModels

            var query = new ListBrandsQuery();

            // Act
            var result = await _handlerList.Handle(query, CancellationToken.None);

            // Assert
            _uowMock.Verify(u => u.Brands.ListAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IList<BrandVm>>(brands), Times.Once);
            Assert.Empty(result);  // Verifica se a lista retornada está vazia
        }

        [Fact(DisplayName = "Deve lançar exceção quando ocorrer um erro inesperado")]
        [Trait("Query", "ListBrands")]
        public async Task Handle_ShouldThrowException_WhenErrorOccursList()
        {
            // Arrange
            // A configuração do mock deve lançar uma exceção
            _uowMock.Setup(u => u.Brands.ListAsync()).ThrowsAsync(new System.Exception("Erro inesperado"));

            var query = new ListBrandsQuery();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.Exception>(() => _handlerList.Handle(query, CancellationToken.None));
            Assert.Equal("Erro inesperado", exception.Message);  // Verifica se a exceção foi lançada
        }
        #endregion
    }
}
