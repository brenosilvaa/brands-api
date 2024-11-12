using AutoMapper;
using MediatR;
using BrandService.Domain.Contracts;
using BrandService.Domain.ViewModels;

namespace BrandService.App.Commands.Brands
{
    public class RemoveBrandCommand : IRequest<RemoveResultVm>
    {
        public long Id { get; set; }

        public class RemoveBrandCommandHandler : IRequestHandler<RemoveBrandCommand, RemoveResultVm>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;
            public RemoveBrandCommandHandler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<RemoveResultVm> Handle(RemoveBrandCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var brand = await _uow.Brands.FindAsync(request.Id);

                    if (brand == null)
                        throw new Exception("Marca não encontrada.");

                    _uow.Brands.Remove(brand);

                    await _uow.Commit();

                    return new RemoveResultVm
                    {
                        Success = true
                    };
                }
                catch (Exception ex)
                {
                    return new RemoveResultVm
                    {
                        Success = false,
                        Message = ex.Message
                    };
                }
            }
        }
    }
}
