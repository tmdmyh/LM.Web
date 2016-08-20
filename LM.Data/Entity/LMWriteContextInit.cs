using LM.Core.Data.Initializers;
using LM.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Data.Entity
{
    public sealed class LMWriteContextInit : DbContextInitializerBase<LMReadContext>, IStartupTask
    {
        public LMWriteContextInit()
        {
            CreateDatabaseInitializer = new CreateDatabaseIfNotExistsWithSeed();
        }

        public int Order
        {
            get
            {
                return 1;
            }
        }

        public void Execute()
        {
            this.ContextInitialize();
        }
    }
}
