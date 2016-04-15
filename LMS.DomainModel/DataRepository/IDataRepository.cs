using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.DataRepository
{
    public interface IDataRepository<T> : IDisposable
    {
        IEnumerable<T> List();
        T GetById(int? id);
        T GetById(string id);
        void Insert(T entity);
        void Delete(int? id);
        void Delete(string id);
        void Update(T entity);
        void Save();
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        IQueryable<T> Fetch(Func<T, bool> predicate);

        IQueryable<T> Fetch(Func<T, bool> predicate, string includeProperties);
        bool Any(Func<T, bool> predicate);
        bool All(Func<T, bool> predicate);

    }
}
