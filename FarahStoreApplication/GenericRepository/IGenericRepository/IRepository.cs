using FarahStoreModel.CommonModel;
using System.Linq.Expressions;

namespace FarahStoreApplication.GenericRepository.IGenericRepository;

public interface IRepository<T> where T : class
{
    //GET ALL, GET By ID FIRST OR DEFAULT, ADD, REMOVE, REMOVERANGE

    Task<ResultDto> Add(T entity);
    Task<ResultDto> Remove(T entity);
    Task<ResultDto> RemoveRange(IEnumerable<T> entity);
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null,
        string? includeProperties = null);
    Task<T> GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
}