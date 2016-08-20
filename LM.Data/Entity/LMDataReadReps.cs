using LM.Core.Data;
using LM.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Data
{
    public class LMDataReadReps : DbSessionBase, ILMDataReps
    {
        protected override void SetDbContext()
        {
            _db = EngineContext.Current.ResolveKeyed<DbContext>("LMDataReadDbContext");
        }
    }
}
