using LM.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LM.Data
{
   public class LMReadContext: DbContextBase
    {
        public LMReadContext()
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
