namespace LearnToLearn.Services
{
    using System.Collections.Generic;

    using Entities;

    public interface IService<T>
    {
        IEnumerable<T> Get();

        T GetById(object id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
