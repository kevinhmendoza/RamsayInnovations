using System.ComponentModel.DataAnnotations;

namespace RamsayInnovations.Domain
{
    public abstract class BaseEntity
    {

    }

    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        [Key]
        public virtual T Id { get; set; }
    }
}
