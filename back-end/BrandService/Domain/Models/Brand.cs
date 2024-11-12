namespace BrandService.Domain.Models
{
    public sealed class Brand : BaseDomain
    {
        #region Proprerties
        public string Name { get; private set; }
        public string Owner { get; private set; }
        public string Description { get; private set; }
        #endregion

        #region Constructors
        public Brand() { }

        public Brand(string name, string owner, string description) => Update(name, owner, description);
        #endregion

        #region Methods
        public override bool Validate() => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Owner) && !string.IsNullOrWhiteSpace(Description);

        public void Update(string name, string owner, string description)
        {
            Name = name;
            Owner = owner;
            Description = description;
        }

        public override string ToString() => Name;
        #endregion
    }
}
