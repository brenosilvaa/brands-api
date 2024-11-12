using BrandService.Data.Context;
using BrandService.Data.Repos;
using BrandService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BrandService.Tests
{
    public class BrandRepoTests
    {
        private readonly BrandRepo _brandRepo;
        private readonly BrandsContext _context;

        public BrandRepoTests()
        {
            // Criação do DbContext em memória
            var options = new DbContextOptionsBuilder<BrandsContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;

            _context = new BrandsContext(options);
            _brandRepo = new BrandRepo(_context);
            _context.Database.EnsureDeleted();  // Garante que o banco de dados seja limpo antes de cada teste
            _context.Database.EnsureCreated();  // Cria o banco de dados
        }

        [Fact(DisplayName = "Deve listar marcas ordenadas por nome")]
        public async Task ListAsync_ShouldReturnOrderedBrands()
        {
            // Arrange
            var brands = new List<Brand>
    {
        new Brand("Apple", "Owner A", "Description A"),
        new Brand("Avocado", "Owner B", "Description B"),
        new Brand("Google", "Owner C", "Description C")
    };

            // Adicionar marcas ao contexto em memória
            _context.AddRange(brands);
            await _context.SaveChangesAsync();  // Certifique-se de salvar os dados

            // Act
            var result = await _brandRepo.ListAsync();

            // Assert
            Assert.Equal(3, result.Count); // Espera-se 3 marcas no resultado
            Assert.Equal("Apple", result[0].Name); // A primeira marca deve ser "Apple"
            Assert.Equal("Avocado", result[1].Name); // A segunda marca deve ser "Avocado"
            Assert.Equal("Google", result[2].Name); // A terceira marca deve ser "Google"
        }



        [Fact(DisplayName = "Deve buscar marcas pelo nome")]
        public async Task SearchAsync_ShouldReturnFilteredBrands()
        {
            // Arrange
            var brands = new List<Brand>
    {
        new Brand("Apple", "Owner A", "Description A"),
        new Brand("Google", "Owner B", "Description B"),
        new Brand("Microsoft", "Owner C", "Description C")
    };

            // Adicionar as marcas ao contexto
            _context.AddRange(brands);
            await _context.SaveChangesAsync();

            // Termo de pesquisa
            var searchTerms = "Apple";  // Vamos buscar por "Apple"

            // Act
            var result = await _brandRepo.SearchAsync(searchTerms);

            // Assert
            Assert.Single(result); // Espera 1 marca com o nome "Apple"
            Assert.Equal("Apple", result[0].Name); // Verifica se a marca é a "Apple"
        }

    }
}
