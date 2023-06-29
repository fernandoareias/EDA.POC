using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Post.Query.Infraestructure.Data
{
    public class DatabaseContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _optionsBuilder;

        public DatabaseContextFactory(Action<DbContextOptionsBuilder> optionsBuilder)
        {
            _optionsBuilder = optionsBuilder;
        }


        public ApplicationDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();

            _optionsBuilder(optionsBuilder);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
