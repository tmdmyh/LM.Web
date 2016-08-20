using LM.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Data
{
   public class LMWriteContext : DbContextBase
    {
        public LMWriteContext()
            : base(GetConnectionStringName())
        {

        }
        protected override Type GetOnModelCreatingType()
        {
            return typeof(LMEntityTypeConfiguration<>);
        }
        private static string GetConnectionStringName()
        {
            return "name = ReadLMObjectContext";
        }
    }
}
