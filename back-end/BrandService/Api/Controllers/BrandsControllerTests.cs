using AutoMapper;
using BrandService.App.Commands.Brands;
using BrandService.App.Queries.Brands;
using BrandService.Domain.ViewModels;
using BrandService.Domain.ViewModels.Brands;
using MediatR;
using Moq;
using Xunit;

namespace BrandService.Api.Controllers
{
    public class BrandsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BrandsController _controller;

        public BrandsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _controller = new BrandsController(_mediatorMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "Deve listar todas as marcas")]
        public async Task List_ShouldReturnListOfBrands()
        {
            // Arrange
            var brandsVm = new List<BrandVm>
        {
            new BrandVm { Id = 1, Name = "Apple", Owner = "Owner A", Description = "Description A" },
            new BrandVm { Id = 2, Name = "Avocado", Owner = "Owner B", Description = "Description B" }
        };

            _mediatorMock.Setup(m => m.Send(It.IsAny<ListBrandsQuery>(), default)).ReturnsAsync(brandsVm);

            // Act
            var result = await _controller.List();

            // Assert
            var okResult = Assert.IsType<List<BrandVm>>(result);
            Assert.Equal(2, okResult.Count);
            Assert.Equal("Apple", okResult[0].Name);
            Assert.Equal("Avocado", okResult[1].Name);
        }

        [Fact(DisplayName = "Deve encontrar uma marca pelo Id")]
        public async Task Find_ShouldReturnBrandById()
        {
            // Arrange
            var brandVm = new BrandVm { Id = 1, Name = "Apple", Owner = "Owner A", Description = "Description A" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<FindBrandByIdQuery>(), default)).ReturnsAsync(brandVm);

            // Act
            var result = await _controller.Find(1);

            // Assert
            var okResult = Assert.IsType<BrandVm>(result);
            Assert.Equal(1, okResult.Id);
            Assert.Equal("Apple", okResult.Name);
            Assert.Equal("Owner A", okResult.Owner);
        }

        [Fact(DisplayName = "Deve criar uma nova marca")]
        public async Task Create_ShouldReturnCreatedBrand()
        {
            // Arrange
            var createBrandVm = new CreateBrandVm
            {
                Name = "Apple",
                Owner = "Owner A",
                Description = "Description A"
            };
            var createdBrandVm = new BrandVm
            {
                Id = 1,
                Name = "Apple",
                Owner = "Owner A",
                Description = "Description A"
            };

            _mapperMock.Setup(m => m.Map<CreateBrandCommand>(It.IsAny<CreateBrandVm>())).Returns(new CreateBrandCommand());
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBrandCommand>(), default)).ReturnsAsync(createdBrandVm);

            // Act
            var result = await _controller.Create(createBrandVm);

            // Assert
            var createdResult = Assert.IsType<BrandVm>(result);
            Assert.Equal("Apple", createdResult.Name);
            Assert.Equal("Owner A", createdResult.Owner);
        }

        [Fact(DisplayName = "Deve atualizar uma marca existente")]
        public async Task Update_ShouldReturnUpdatedBrand()
        {
            // Arrange
            var updateBrandVm = new CreateBrandVm
            {
                Name = "Apple Updated",
                Owner = "Owner A Updated",
                Description = "Description Updated"
            };

            var updatedBrandVm = new BrandVm
            {
                Id = 1,
                Name = "Apple Updated",
                Owner = "Owner A Updated",
                Description = "Description Updated"
            };

            _mapperMock.Setup(m => m.Map<UpdateBrandCommand>(It.IsAny<CreateBrandVm>())).Returns(new UpdateBrandCommand());
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBrandCommand>(), default)).ReturnsAsync(updatedBrandVm);

            // Act
            var result = await _controller.Update(1, updateBrandVm);

            // Assert
            var okResult = Assert.IsType<BrandVm>(result);
            Assert.Equal("Apple Updated", okResult.Name);
            Assert.Equal("Owner A Updated", okResult.Owner);
        }

        [Fact(DisplayName = "Deve remover uma marca")]
        public async Task Remove_ShouldReturnRemoveResult()
        {
            // Arrange
            var removeResultVm = new RemoveResultVm
            {
                Success = true,
                Message = "Marca removida com sucesso"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<RemoveBrandCommand>(), default)).ReturnsAsync(removeResultVm);

            // Act
            var result = await _controller.Remove(1);

            // Assert
            var okResult = Assert.IsType<RemoveResultVm>(result);
            Assert.True(okResult.Success);
            Assert.Equal("Marca removida com sucesso", okResult.Message);
        }


    }
}
