using BrandService.Domain.ViewModels.Brands;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace BrandService.Tests
{
    public class BrandVmTests
    {
        [Fact(DisplayName = "Deve ser válido quando todas as propriedades obrigatórias estiverem preenchidas")]
        public void BrandVm_Valid_WhenAllRequiredPropertiesAreFilled()
        {
            // Arrange
            var brandVm = new BrandVm
            {
                Id = 1,
                Name = "Apple",
                Owner = "Owner A",
                Description = "Description A"
            };

            // Act
            var validationResults = ValidateModel(brandVm);

            // Assert
            Assert.Empty(validationResults); // Não deve ter erros de validação
        }

        [Fact(DisplayName = "Deve ser inválido quando a propriedade 'Name' estiver vazia")]
        public void BrandVm_Invalid_WhenNameIsEmpty()
        {
            // Arrange
            var brandVm = new BrandVm
            {
                Id = 1,
                Name = "", // Propriedade obrigatória vazia
                Owner = "Owner A",
                Description = "Description A"
            };

            // Act
            var validationResults = ValidateModel(brandVm);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Name"));
        }

        [Fact(DisplayName = "Deve ser inválido quando a propriedade 'Owner' estiver vazia")]
        public void BrandVm_Invalid_WhenOwnerIsEmpty()
        {
            // Arrange
            var brandVm = new BrandVm
            {
                Id = 1,
                Name = "Apple",
                Owner = "", // Propriedade obrigatória vazia
                Description = "Description A"
            };

            // Act
            var validationResults = ValidateModel(brandVm);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Owner"));
        }

        [Fact(DisplayName = "Deve ser inválido quando a propriedade 'Description' estiver vazia")]
        public void BrandVm_Invalid_WhenDescriptionIsEmpty()
        {
            // Arrange
            var brandVm = new BrandVm
            {
                Id = 1,
                Name = "Apple",
                Owner = "Owner A",
                Description = "" // Propriedade obrigatória vazia
            };

            // Act
            var validationResults = ValidateModel(brandVm);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Description"));
        }

        [Fact(DisplayName = "Deve ser inválido quando a propriedade 'Id' for zero")]
        public void BrandVm_Invalid_WhenIdIsZero()
        {
            // Arrange
            var brandVm = new BrandVm
            {
                Id = 0, // Propriedade obrigatória com valor inválido
                Name = "Apple",
                Owner = "Owner A",
                Description = "Description A"
            };

            // Act
            var validationResults = ValidateModel(brandVm);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Id"));
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact(DisplayName = "Deve ser válido quando todas as propriedades obrigatórias estiverem preenchidas")]
        public void CreateBrandVm_Valid_WhenAllRequiredPropertiesAreFilled()
        {
            // Arrange
            var createBrandVm = new CreateBrandVm
            {
                Name = "Apple",
                Owner = "Owner A",
                Description = "Description A"
            };

            // Act
            var validationResults = ValidateModel(createBrandVm);

            // Assert
            Assert.Empty(validationResults); // Não deve ter erros de validação
        }

        public class CreateBrandVmTests
        {
            [Fact(DisplayName = "Deve ser válido quando todas as propriedades obrigatórias estiverem preenchidas")]
            public void CreateBrandVm_Valid_WhenAllRequiredPropertiesAreFilled()
            {
                // Arrange
                var createBrandVm = new CreateBrandVm
                {
                    Name = "Apple",
                    Owner = "Owner A",
                    Description = "Description A"
                };

                // Act
                var validationResults = ValidateModel(createBrandVm);

                // Assert
                Assert.Empty(validationResults); // Não deve ter erros de validação
            }

            [Fact(DisplayName = "Deve ser inválido quando a propriedade 'Name' estiver vazia")]
            public void CreateBrandVm_Invalid_WhenNameIsEmpty()
            {
                // Arrange
                var createBrandVm = new CreateBrandVm
                {
                    Name = "", // Propriedade obrigatória vazia
                    Owner = "Owner A",
                    Description = "Description A"
                };

                // Act
                var validationResults = ValidateModel(createBrandVm);

                // Assert
                Assert.Contains(validationResults, v => v.MemberNames.Contains("Name"));
            }

            [Fact(DisplayName = "Deve ser inválido quando a propriedade 'Owner' estiver vazia")]
            public void CreateBrandVm_Invalid_WhenOwnerIsEmpty()
            {
                // Arrange
                var createBrandVm = new CreateBrandVm
                {
                    Name = "Apple",
                    Owner = "", // Propriedade obrigatória vazia
                    Description = "Description A"
                };

                // Act
                var validationResults = ValidateModel(createBrandVm);

                // Assert
                Assert.Contains(validationResults, v => v.MemberNames.Contains("Owner"));
            }

            [Fact(DisplayName = "Deve ser inválido quando a propriedade 'Description' estiver vazia")]
            public void CreateBrandVm_Invalid_WhenDescriptionIsEmpty()
            {
                // Arrange
                var createBrandVm = new CreateBrandVm
                {
                    Name = "Apple",
                    Owner = "Owner A",
                    Description = "" // Propriedade obrigatória vazia
                };

                // Act
                var validationResults = ValidateModel(createBrandVm);

                // Assert
                Assert.Contains(validationResults, v => v.MemberNames.Contains("Description"));
            }

            // Método auxiliar para validação do modelo
            private static IList<ValidationResult> ValidateModel(object model)
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(model);
                Validator.TryValidateObject(model, validationContext, validationResults, true);
                return validationResults;
            }
        }
    }
}
