using AutoMapper;
using MediatR;
using BrandService.Domain.Contracts;
using BrandService.Domain.ViewModels.Brands;

namespace BrandService.App.Queries.Brands
{
    /// <summary>
    /// Responsável por encontrar uma marca a partir de um Id
    /// </summary>
    public class FindBrandByIdQuery : IRequest<BrandVm>
    {
        public long Id { get; set; }

        public class FindBrandByIdQueryHandler : IRequestHandler<FindBrandByIdQuery, BrandVm>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;
            public FindBrandByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<BrandVm> Handle(FindBrandByIdQuery request, CancellationToken cancellationToken)
            {
                var brand = await _uow.Brands.FindAsync(request.Id);

                return _mapper.Map<BrandVm>(brand);
            }
        }
    }
}
