using EDA.Core.Commands;
using EDA.Core.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Post.Cmd.Infraestructure.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new();
        public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand
        {
            if(_handlers.ContainsKey(typeof(T))) 
                throw new IndexOutOfRangeException("You cannot register the same command handler twice!");

            _handlers.Add(typeof(T), x => handler((T)x));
        }

        public async Task SendAsync(BaseCommand command)
        {
            if (!_handlers.TryGetValue(command.GetType(), out Func<BaseCommand, Task> handler))
                throw new ArgumentNullException(nameof(handler), "No command handler was registered!");

            await handler(command);

        }
    }
}
