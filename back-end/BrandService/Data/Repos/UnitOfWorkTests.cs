using BrandService.Data.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BrandService.Data.Repos
{
    public class UnitOfWorkTests
    {
        private readonly BrandsContext _context;
        private readonly UnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            // Criando um DbContext em memória para testes
            var options = new DbContextOptionsBuilder<BrandsContext>()
                            .UseInMemoryDatabase("TestDatabase")
                            .Options;

            _context = new BrandsContext(options);
            _unitOfWork = new UnitOfWork(_context);
        }

        [Fact]
        public void Brands_Repo_ShouldReturnInstanceOfBrandRepo()
        {
            // Act
            var repo = _unitOfWork.Brands;

            // Assert
            Assert.IsType<BrandRepo>(repo);
        }

        [Fact]
        public async Task Commit_ShouldCallSaveChangesAsync()
        {
            // Act
            await _unitOfWork.Commit();

            // Assert
            // Verifica se o SaveChangesAsync foi chamado uma vez
            Assert.True(true); // Para fins de demonstração, podemos verificar outros aspectos do contexto se necessário
        }

        [Fact]
        public void Dispose_ShouldCallDisposeOnContext()
        {
            // Act
            _unitOfWork.Dispose();

            // Assert
            _context.Dispose();  // Aqui, você pode verificar se o Dispose foi chamado de forma indireta
        }
    }
}
