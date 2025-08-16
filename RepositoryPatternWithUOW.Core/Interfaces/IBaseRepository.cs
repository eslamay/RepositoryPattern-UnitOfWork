using RepositoryPatternWithUOW.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
	public interface IBaseRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAll();
		Task<T> GetById(int id);

		Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
		Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);
		Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
			Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);

		T Add(T entity);
	}
}
