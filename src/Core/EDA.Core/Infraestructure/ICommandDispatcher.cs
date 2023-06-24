using EDA.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Core.Infraestructure
{
    public interface ICommandDispatcher
    {
        void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand;
        Task SendAsync(BaseCommand command);
    }
}
