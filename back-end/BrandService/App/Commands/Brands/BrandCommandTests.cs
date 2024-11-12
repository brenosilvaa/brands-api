using AutoMapper;
using BrandService.Domain.Contracts;
using BrandService.Domain.Models;
using BrandService.Domain.ViewModels.Brands;
using Moq;
using System.Reflection.Metadata;
using Xunit;
using static BrandService.App.Commands.Brands.CreateBrandCommand;
using static BrandService.App.Commands.Brands.RemoveBrandCommand;
using static BrandService.App.Commands.Brands.UpdateBrandCommand;

namespace BrandService.App.Commands.Brands
{
    public class BrandCommandTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CreateBrandCommandHandler _handlerCreate;
        private readonly RemoveBrandCommandHandler _handlerRemove;
        private readonly UpdateBrandCommandHandler _handlerUpdate;

        public BrandCommandTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _handlerCreate = new CreateBrandCommandHandler(_uowMock.Object, _mapperMock.Object);
            _handlerRemove = new RemoveBrandCommandHandler(_uowMock.Object, _mapperMock.Object);
            _handlerUpdate = new UpdateBrandCommandHandler(_uowMock.Object, _mapperMock.Object);
        }

        #region Tests Create

        [Fact(DisplayName = "Deve adicionar a marca e retornar BrandVm quando os dados são válidos")]
        [Trait("Controller", "CreateBrand")]
        public async Task Handle_ShouldAddBrandAndReturnBrandVm_WhenBrandIsValid()
        {
            // Arrange
            var command = new CreateBrandCommand
            {
                Name = "Test Brand",
                Owner = "Test Owner",
                Description = "Test Description"
            };

            var brand = new Brand(command.Name, command.Owner, command.Description);
            var brandVm = new BrandVm { Name = command.Name, Owner = command.Owner, Description = command.Description };

            _uowMock.Setup(u => u.Brands.AddAsync(It.IsAny<Brand>())).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.Commit()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<BrandVm>(It.IsAny<Brand>())).Returns(brandVm);

            // Act
            var result = await _handlerCreate.Handle(command, CancellationToken.None);

            // Assert
            _uowMock.Verify(u => u.Brands.AddAsync(It.IsAny<Brand>()), Times.Once);
            _uowMock.Verify(u => u.Commit(), Times.Once);
            _mapperMock.Verify(m => m.Map<BrandVm>(It.IsAny<Brand>()), Times.Once);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Owner, result.Owner);
            Assert.Equal(command.Description, result.Description);
        }

        [Fact(DisplayName = "Deve lançar exceção quando a marca for inválida")]
        [Trait("Controller", "CreateBrand")]
        public async Task Handle_ShouldThrowException_WhenBrandIsInvalid()
        {
            // Arrange
            var command = new CreateBrandCommand
            {
                Name = "", // Invalid name
                Owner = "Test Owner",
                Description = "Test Description"
            };

            var brand = new Brand(command.Name, command.Owner, command.Description);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handlerCreate.Handle(command, CancellationToken.None));
        }
        #endregion

        #region Tests Remove

        [Fact(DisplayName = "Deve retornar resultado de sucesso quando a marca existir")]
        [Trait("Controller", "RemoveBrand")]
        public async Task Handle_ShouldReturnSuccessResult_WhenBrandExists()
        {
            // Arrange
            var command = new RemoveBrandCommand { Id = 1 };
            var brand = new Brand("Test Brand", "Test Owner", "Test Description");

            _uowMock.Setup(u => u.Brands.FindAsync(command.Id)).ReturnsAsync(brand);
            _uowMock.Setup(u => u.Brands.Remove(brand));
            _uowMock.Setup(u => u.Commit()).Returns(Task.CompletedTask);

            // Act
            var result = await _handlerRemove.Handle(command, CancellationToken.None);

            // Assert
            _uowMock.Verify(u => u.Brands.FindAsync(command.Id), Times.Once);
            _uowMock.Verify(u => u.Brands.Remove(brand), Times.Once);
            _uowMock.Verify(u => u.Commit(), Times.Once);
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Deve retornar resultado de falha quando a marca não existir")]
        [Trait("Controller", "RemoveBrand")]
        public async Task Handle_ShouldReturnFailureResult_WhenBrandDoesNotExist()
        {
            // Arrange
            var command = new RemoveBrandCommand { Id = 99 };

            _uowMock.Setup(u => u.Brands.FindAsync(command.Id)).ReturnsAsync((Brand)null);

            // Act
            var result = await _handlerRemove.Handle(command, CancellationToken.None);

            // Assert
            _uowMock.Verify(u => u.Brands.FindAsync(command.Id), Times.Once);
            _uowMock.Verify(u => u.Brands.Remove(It.IsAny<Brand>()), Times.Never);
            _uowMock.Verify(u => u.Commit(), Times.Never);
            Assert.False(result.Success);
            Assert.Equal("Marca não encontrada.", result.Message);
        }

        [Fact(DisplayName = "Deve retornar resultado de falha quando ocorrer uma exceção")]
        [Trait("Controller", "RemoveBrand")]
        public async Task Handle_ShouldReturnFailureResult_WhenExceptionOccurs()
        {
            // Arrange
            var command = new RemoveBrandCommand { Id = 1 };
            var brand = new Brand("Test Brand", "Test Owner", "Test Description");

            _uowMock.Setup(u => u.Brands.FindAsync(command.Id)).ReturnsAsync(brand);
            _uowMock.Setup(u => u.Brands.Remove(brand));
            _uowMock.Setup(u => u.Commit()).ThrowsAsync(new Exception("Erro ao remover marca."));

            // Act
            var result = await _handlerRemove.Handle(command, CancellationToken.None);

            // Assert
            _uowMock.Verify(u => u.Brands.FindAsync(command.Id), Times.Once);
            _uowMock.Verify(u => u.Brands.Remove(brand), Times.Once);
            _uowMock.Verify(u => u.Commit(), Times.Once);
            Assert.False(result.Success);
            Assert.Equal("Erro ao remover marca.", result.Message);
        }
        #endregion

        #region Tests Update

        [Fact(DisplayName = "Deve atualizar a marca e retornar BrandVm quando os dados são válidos")]
        [Trait("Controller", "UpdateBrand")]
        public async Task Handle_ShouldUpdateBrandAndReturnBrandVm_WhenBrandIsValid()
        {
            // Arrange
            var command = new UpdateBrandCommand
            {
                Id = 1,
                Name = "Updated Brand",
                Owner = "Updated Owner",
                Description = "Updated Description"
            };

            var brand = new Brand("Old Brand", "Old Owner", "Old Description");
            var updatedBrandVm = new BrandVm { Name = command.Name, Owner = command.Owner, Description = command.Description };

            _uowMock.Setup(u => u.Brands.FindAsync(command.Id)).ReturnsAsync(brand);
            _uowMock.Setup(u => u.Brands.Update(brand));
            _uowMock.Setup(u => u.Commit()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<BrandVm>(brand)).Returns(updatedBrandVm);

            // Act
            var result = await _handlerUpdate.Handle(command, CancellationToken.None);

            // Assert
            _uowMock.Verify(u => u.Brands.FindAsync(command.Id), Times.Once);
            _uowMock.Verify(u => u.Brands.Update(brand), Times.Once);
            _uowMock.Verify(u => u.Commit(), Times.Once);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Owner, result.Owner);
            Assert.Equal(command.Description, result.Description);
        }

        [Fact(DisplayName = "Deve lançar exceção quando a marca não existir")]
        [Trait("Controller", "UpdateBrand")]
        public async Task Handle_ShouldThrowException_WhenBrandDoesNotExist()
        {
            // Arrange
            var command = new UpdateBrandCommand
            {
                Id = 99, // Non-existent brand ID
                Name = "New Brand",
                Owner = "New Owner",
                Description = "New Description"
            };

            _uowMock.Setup(u => u.Brands.FindAsync(command.Id)).ReturnsAsync((Brand)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handlerUpdate.Handle(command, CancellationToken.None));
            Assert.Equal("Marca não encontrada.", exception.Message);
            _uowMock.Verify(u => u.Brands.Update(It.IsAny<Brand>()), Times.Never);
            _uowMock.Verify(u => u.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Deve lançar exceção quando os dados da marca forem inválidos")]
        [Trait("Controller", "UpdateBrand")]
        public async Task Handle_ShouldThrowException_WhenBrandIsInvalidUpdate()
        {
            // Arrange
            var command = new UpdateBrandCommand
            {
                Id = 1,
                Name = "", // Invalid data
                Owner = "Updated Owner",
                Description = "Updated Description"
            };

            var brand = new Brand("Old Brand", "Old Owner", "Old Description");
            brand.Update(command.Name, command.Owner, command.Description);

            _uowMock.Setup(u => u.Brands.FindAsync(command.Id)).ReturnsAsync(brand);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handlerUpdate.Handle(command, CancellationToken.None));
            Assert.Equal("Marca inválida.", exception.Message);
        }
        #endregion
    }
}
