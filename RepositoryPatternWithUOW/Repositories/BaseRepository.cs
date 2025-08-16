using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly ApplicationDbContext dbContext;

		public BaseRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await dbContext.Set<T>().ToListAsync();
		}

		public async Task<T> GetById(int id)
		{
			return await dbContext.Set<T>().FindAsync(id);
		}

		public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
		{
			IQueryable<T> query = dbContext.Set<T>();

			if (includes != null)
				foreach (var incluse in includes)
					query = query.Include(incluse);

			return await query.SingleOrDefaultAsync(criteria);
		}

		public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
		{
			IQueryable<T> query = dbContext.Set<T>();

			if (includes != null)
				foreach (var include in includes)
					query = query.Include(include);

			return await query.Where(criteria).ToListAsync();
		}

		public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take)
		{
			return await dbContext.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
		}

		public async Task<IEnumerable<T>> FindAllAsync(
			Expression<Func<T, bool>> criteria, int? skip, int? take,
			Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
		{
			IQueryable<T> query = dbContext.Set<T>().Where(criteria);

			if (take.HasValue)
				query = query.Take(take.Value);

			if (skip.HasValue)
				query = query.Take(skip.Value);

			if (orderBy != null)
			{
				if (orderByDirection == OrderBy.Ascending)
					query = query.OrderBy(orderBy);
				else
					query = query.OrderByDescending(orderBy);
			}

			return await query.ToListAsync();
		}

		public T Add(T entity)
		{
			dbContext.Add(entity);
			dbContext.SaveChanges();
			return entity;
		}
	}
}
