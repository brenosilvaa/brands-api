using Moq;
using Xunit;

namespace BrandService.IoC
{
    public class DependencyInjectionTests
    {
        private readonly IServiceCollection _services;
        private readonly Mock<IConfiguration> _configMock;

        public DependencyInjectionTests()
        {
            _services = new ServiceCollection();
            _configMock = new Mock<IConfiguration>();

            // Configuração para simular valores de configuração para o banco de dados
            _configMock.Setup(config => config["DB:HOST"]).Returns("localhost");
            _configMock.Setup(config => config["DB:PORT"]).Returns("3306");
            _configMock.Setup(config => config["DB:USER"]).Returns("root");
            _configMock.Setup(config => config["DB:PASSWORD"]).Returns("password");
            _configMock.Setup(config => config["DB:DATABASE"]).Returns("BrandDb");
        }


        [Fact(DisplayName = "Deve configurar o AutoMapper e o MediatR")]
        public void AddInfrastructure_ShouldConfigureAutoMapperAndMediatR()
        {
            // Act
            _services.AddInfrastructure(_configMock.Object);

            // Assert
            var provider = _services.BuildServiceProvider();

            Assert.NotNull(provider.GetService<AutoMapper.IMapper>());
            Assert.NotNull(provider.GetService<MediatR.IMediator>());
        }
    }
}
