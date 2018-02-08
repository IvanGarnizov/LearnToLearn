namespace LearnToLearn.Data.Repositories
{
    using System.Data.Entity;
    using System.Linq;

    public class Repository<T> : IRepository<T>
        where T : class
    {
        private LearnToLearnContext context;
        private IDbSet<T> entitySet;

        public Repository(LearnToLearnContext context)
        {
            this.context = context;

            entitySet = context.Set<T>();
        }

        public IQueryable<T> All()
        {
            return entitySet;
        }

        public T Find(object id)
        {
            return entitySet.Find(id);
        }

        public void Add(T entity)
        {
            ChangeState(entity, EntityState.Added);
        }

        public void Update(T entity)
        {
            ChangeState(entity, EntityState.Modified);
        }

        public void Remove(T entity)
        {
            ChangeState(entity, EntityState.Deleted);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        private void ChangeState(T entity, EntityState state)
        {
            var entry = context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                entitySet.Attach(entity);
            }

            entry.State = state;
        }
    }
}
