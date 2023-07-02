using EDA.Core.Commands;
using EDA.Core.Infraestructure;
using EDA.Core.Queries;
using EDA.Post.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EDA.Post.Query.Infraestructure.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher<PostEntity>
    {
        private readonly Dictionary<Type, Func<BaseQuery, Task<List<PostEntity>>>> _handlers = new(); 
        public void RegisterHandler<TQuery>(Func<TQuery, Task<List<PostEntity>>> handler) where TQuery : BaseQuery
        {
            if (_handlers.ContainsKey(typeof(TQuery)))
                throw new IndexOutOfRangeException("You cannot register the same query handler twice!");

            _handlers.Add(typeof(TQuery), x => handler((TQuery)x));
        }

        public async Task<List<PostEntity>> SendAsync(BaseQuery query)
        {
            if (!_handlers.TryGetValue(query.GetType(), out Func<BaseQuery, Task<List<PostEntity>>> handler))
                throw new ArgumentNullException(nameof(handler), "No query handler was registered!");

            return await handler(query);
        }
    }
}
