using Xunit;
using BrandService.Domain.Models;

namespace BrandService.Tests.Domain.Models
{
    public class BaseDomainTests
    {
        // Classe mock para testar BaseDomain
        private class MockBaseDomain : BaseDomain
        {
            // Permite definir o Id para testar as propriedades
            public void SetId(long id)
            {
                Id = id; // Agora, a propriedade Id pode ser acessada diretamente
            }
        }

        [Fact(DisplayName = "Propriedade Id deve ser inicializada como zero")]
        public void BaseDomain_Id_ShouldInitializeToZero()
        {
            // Arrange
            var baseDomain = new MockBaseDomain();

            // Act
            var id = baseDomain.Id;

            // Assert
            Assert.Equal(0, id);
        }

        [Fact(DisplayName = "Método Validate deve retornar true por padrão")]
        public void BaseDomain_Validate_ShouldReturnTrue()
        {
            // Arrange
            var baseDomain = new MockBaseDomain();

            // Act
            var isValid = baseDomain.Validate();

            // Assert
            Assert.True(isValid);
        }

        [Fact(DisplayName = "Propriedade Id deve ser atribuída corretamente")]
        public void BaseDomain_Id_ShouldSetIdCorrectly()
        {
            // Arrange
            var baseDomain = new MockBaseDomain();
            baseDomain.SetId(10);

            // Act
            var id = baseDomain.Id;

            // Assert
            Assert.Equal(10, id);
        }
    }
}
