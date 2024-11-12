using AutoMapper;
using MediatR;
using BrandService.Domain.Contracts;
using BrandService.Domain.Models;
using BrandService.Domain.ViewModels.Brands;

namespace BrandService.App.Commands.Brands
{
    public class CreateBrandCommand : IRequest<BrandVm>
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }

        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandVm>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;
            public CreateBrandCommandHandler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<BrandVm> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                var brand = new Brand(request.Name, request.Owner, request.Description);

                if (!brand.Validate())
                    throw new Exception("Marca inválida.");

                await _uow.Brands.AddAsync(brand);

                await _uow.Commit();

                return _mapper.Map<BrandVm>(brand);
            }
        }
    }
}
