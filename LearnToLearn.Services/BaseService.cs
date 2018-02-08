namespace LearnToLearn.Services
{
    using System.Collections.Generic;

    using Data.Repositories;

    using Entities;

    public class BaseService<T> : IService<T>
    {
        protected IRepository<T> repository;

        public BaseService(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<T> Get()
        {
            return repository.All();
        }

        public T GetById(object id)
        {
            return repository.Find(id);
        }

        public void Insert(T entity)
        {
            repository.Add(entity);
            repository.SaveChanges();
        }

        public void Update(T entity)
        {
            repository.Update(entity);
            repository.SaveChanges();
        }

        public void Delete(T entity)
        {
            repository.Remove(entity);
            repository.SaveChanges();
        }
    }
}
