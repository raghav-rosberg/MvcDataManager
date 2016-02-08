using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MvcDataManager
{
    #region Respository Base

    public interface IRepository<T>
        where T : class
    {
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
    }

    public class RepositoryBase<T> : Disposable,
        IRepository<T>
        where T : class
    {
        private readonly DbContext _dataContext;

        private IDbSet<T> Dbset
        {
            get { return _dataContext.Set<T>(); }
        }

        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            if (unitOfWork._dbContext == null) throw new Exception("Please intialize 'DBContext' of the Unit of Work object before using rapository base.");
            _dataContext = unitOfWork._dbContext;
            _dataContext.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
        }

        public virtual T Create(T entity)
        {
            Dbset.Add(entity);
            return entity;
        }

        public virtual void Update(T entity)
        {
            Dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            Dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = Dbset.Where(where).AsEnumerable();
            foreach (var obj in objects)
                Dbset.Remove(obj);
        }

        public virtual T GetById(long id)
        {
            return Dbset.Find(id);
        }

        public virtual T GetById(string id)
        {
            return Dbset.Find(id);
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return Dbset.FirstOrDefault(where);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Dbset.ToList();
        }
    }

    #endregion

    #region Disposable

    public class Disposable : IDisposable
    {
        private bool _mIsDisposed;

        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_mIsDisposed && disposing)
            {
                DisposeCore();
            }

            _mIsDisposed = true;
        }

        protected virtual void DisposeCore()
        {
        }
    }

    #endregion

    #region Unit of Work

    public interface IUnitOfWork: IDisposable
    {
        DbContext _dbContext { get; set; }
        void Commit();
    }

    public class UnitOfWork : Disposable, IUnitOfWork
    {
        public virtual void Commit()
        {
            _dbContext.SaveChanges();
        }

        public DbContext _dbContext { get; set; }

        public UnitOfWork(DbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            _dbContext = dbContext;
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }

    #endregion
}
