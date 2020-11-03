namespace POS.Domain.SeedWork
{
    public abstract class BaseEntity<T> where T: struct
    {
        public virtual T Id { get; protected set; }
    }
}
