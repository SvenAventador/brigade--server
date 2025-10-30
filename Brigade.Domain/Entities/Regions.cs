using Brigade.Domain.Exceptions;
using Brigade.Domain.ValueObjects;

namespace Brigade.Domain.Entities
{
    /// <summary>
    /// Регионы.
    /// </summary>
    public class Regions
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; private set;  }

        /// <summary>
        /// Наименование региона.
        /// </summary>
        public Name Name { get; private set; } = null!;

        public Regions(Name name)
        {
            Guard.AgainstNull(name, nameof(name));

            Id = Guid.NewGuid();
            Name = name;
        }
    }
}