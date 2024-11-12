using AutoMapper;
using MediatR;
using BrandService.Domain.Contracts;
using BrandService.Domain.ViewModels.Brands;

namespace BrandService.App.Commands.Brands
{
    public class UpdateBrandCommand : IRequest<BrandVm>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }

        public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BrandVm>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;
            public UpdateBrandCommandHandler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<BrandVm> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
            {
                var brand = await _uow.Brands.FindAsync(request.Id);

                if (brand == null)
                    throw new Exception("Marca não encontrada.");

                brand.Update(request.Name, request.Owner, request.Description);

                if (!brand.Validate())
                    throw new Exception("Marca inválida.");

                _uow.Brands.Update(brand);

                await _uow.Commit();

                return _mapper.Map<BrandVm>(brand);
            }
        }
    }
}
