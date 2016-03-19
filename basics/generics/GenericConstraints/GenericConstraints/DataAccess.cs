namespace GenericConstraints
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class GenericConstraintsContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }

    public interface IReadOnlyRepository<out T> : IDisposable
    {
        T FindById(int Id);
        IQueryable<T> FindAll();
    }

    public interface IWriteOnlyRepository<in T> : IDisposable
    {
        void Add(T newEntity);
        void Delete(T entity);
        int Commit();
    }

    public interface IRepository<T> : IReadOnlyRepository<T>, IWriteOnlyRepository<T>
    {

    }

    public class SqlRepository<T> : IRepository<T> where T : class, IEntity
    {
        private DbContext _context;
        private DbSet<T> _set;
        public SqlRepository(DbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }
        public void Add(T newEntity)
        {
            if (newEntity.IsValid())
            {
                _set.Add(newEntity);
            }
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _set.Remove(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IQueryable<T> FindAll()
        {
            return _set;
        }

        public T FindById(int Id)
        {
            return _set.Find(Id);
        }
    }
}
