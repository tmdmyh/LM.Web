using Autofac;
using Autofac.Core;
using LM.Core.Data;
using LM.Core.Infrastructure;
using LM.Core.Infrastructure.DependencyManagement;
using System.Data.Entity;

namespace LM.Data
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
         
            builder.Register(c => new LMReadContext()).Keyed<DbContext>("LMDataReadDbContext");
            builder.Register(c => new LMReadContext()).Keyed<DbContext>("LMDataWriteDbContext");
           
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 2; }
        }
    }
}
