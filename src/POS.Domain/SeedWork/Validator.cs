namespace POS.Domain.SeedWork
{
    public abstract class Validator<TEntity>
    {
        public abstract void Validate(Notification note, TEntity entity);

        protected void CheckRule(Notification note, TEntity entity, ISpecification<TEntity> specification, string error)
        {
            var isSatisfied = specification.IsSatisfiedBy(entity);

            if (!isSatisfied)
            {
                note.AddError(error);
            }
        }

        protected void CheckRule<TSpecification>(Notification note, TEntity entity, string error) where TSpecification : CompositeSpecification<TEntity>, new()
        {
            var spec = new TSpecification();

            CheckRule(note, entity, spec, error);
        }
    }
}
