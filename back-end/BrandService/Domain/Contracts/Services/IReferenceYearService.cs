using MediatR;

namespace BrandService.Domain.Contracts.Services
{
    public interface IReferenceYearService
    {
        Task<IList<long>?> GetVehicleIdsByReferenceYear(long PriceReferenceNumber, long Year);
    }
}
