using BrandService.Domain.Contracts;

namespace BrandService.Domain.Models
{
    public abstract class BaseDomain : IBaseDomain
    {
        #region Properties
        public long Id { get; protected set; }
        #endregion

        #region Methods
        public virtual bool Validate() => true;
        #endregion
    }
}
