using FarahStoreApplication.GenericRepository.IGenericRepository;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using FarahStoreWeb.Data;
using Microsoft.EntityFrameworkCore;
using FarahStoreModel.CommonModel;

namespace FarahStoreApplication.GenericRepository;


public class Repository<T> : IRepository<T> where T : class
{
    private readonly DatabaseContex _db;
    internal DbSet<T> dbSet;

    public Repository(DatabaseContex db)
    {
        _db = db;
        this.dbSet = db.Set<T>();
    }
    public async Task<ResultDto> Add(T entity)
    {
        try
        {
            dbSet.Add(entity);
            return new ResultDto
            {
                Message = "عملیات ثبت با موفقیت انجام شد",
                Status = true
            };
        }
        catch (Exception e)
        {
            return new ResultDto
            {
                Message = e.Message,
                Status = false
            };
        }

    }

    public async Task<ResultDto> AddRange(IEnumerable<T> entity)
    {
        try
        {
            dbSet.AddRange(entity);
            return new ResultDto
            {
                Message = "عملیات ثبت با موفقیت انجام شد",
                Status = true
            };
        }
        catch (Exception e)
        {
            return new ResultDto
            {
                Message = e.Message,
                Status = false
            };
        }
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null, string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (includeProperties != null)
        {
            //abc,,xyz -> abc xyz
            foreach (var includeProperty in includeProperties.Split(
                         new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        if (orderby != null)
        {
            return await orderby(query).ToListAsync();
        }
        return await query.ToListAsync();
    }

    public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (includeProperties != null)
        {
            //abc,,xyz -> abc xyz
            foreach (var includeProperty in includeProperties.Split(
                         new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        return query.FirstOrDefault();
    }

    public async Task<ResultDto> Remove(T entity)
    {
        try
        {
            dbSet.Remove(entity);
            return new ResultDto
            {
                Message = "عملیات ثبت با موفقیت انجام شد",
                Status = true
            };
        }
        catch (Exception e)
        {
            return new ResultDto
            {
                Message = e.Message,
                Status = false
            };
        }
    }

    public async Task<ResultDto> RemoveRange(IEnumerable<T> entity)
    {
        try
        {
            dbSet.RemoveRange(entity);
            return new ResultDto
            {
                Message = "عملیات ثبت با موفقیت انجام شد",
                Status = true
            };
        }
        catch (Exception e)
        {
            return new ResultDto
            {
                Message = e.Message,
                Status = false
            };
        }
    }
}