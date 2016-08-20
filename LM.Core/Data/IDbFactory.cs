

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;



namespace LM.Core.Data
{
        public interface IDbFactory
        {
        DbContext GetDbContext();


        }
    }
