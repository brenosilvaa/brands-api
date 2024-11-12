using AutoMapper;
using BrandService.App.Commands.Brands;
using BrandService.App.Queries.Brands;
using BrandService.Domain.ViewModels;
using BrandService.Domain.ViewModels.Brands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrandService.Api.Controllers
{
    /// <summary>
    /// Rotas responsáveis pelo CRUD de Marcas
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public BrandsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Ação responsável por listar todas as marcas cadastradas
        /// </summary>
        /// <returns>Lista de marcas cadastradas</returns>
        [HttpGet]
        public async Task<IList<BrandVm>?> List()
        {
            return await _mediator.Send(new ListBrandsQuery());
        }

        /// <summary>
        /// Ação responsável por encontrar uma marca a partir de um Id
        /// </summary>
        /// <param name="id">Id da marca a ser encontrada</param>
        /// <returns>Marca encontrada</returns>
        [HttpGet("{id}")]
        public async Task<BrandVm?> Find(long id)
        {
            return await _mediator.Send(new FindBrandByIdQuery
            {
                Id = id
            });
        }

        /// <summary>
        /// Método responsável pela criação de uma nova marca
        /// </summary>
        /// <param name="brand">Marca a ser inserida</param>
        /// <returns>Marca inserida</returns>
        [HttpPost]
        public async Task<BrandVm> Create([FromBody] CreateBrandVm brand)
        {
            var command = _mapper.Map<CreateBrandCommand>(brand);

            return await _mediator.Send(command);
        }

        /// <summary>
        /// Método responsável pela edição de uma marca
        /// </summary>
        /// <param name="id">Id da marca a ser editada</param>
        /// <param name="brand">Marca a ser editada</param>
        /// <returns>Marca editada</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestResultVm))]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, "text/plain")]
        public async Task<BrandVm> Update(long id, [FromBody] CreateBrandVm brand)
        {
            var command = _mapper.Map<UpdateBrandCommand>(brand);
            command.Id = id;

            return await _mediator.Send(command);
        }

        /// <summary>
        /// Método responsável pela remoção de uma marca
        /// </summary>
        /// <param name="id">Id da marca a ser removida</param>
        /// <returns>Resultado de remoção</returns>
        [HttpDelete("{id}")]
        public async Task<RemoveResultVm> Remove(long id)
        {
            return await _mediator.Send(new RemoveBrandCommand
            {
                Id = id
            });
        }
    }
}
