﻿// <autogenerated>
//   This file was generated by T4 code generator Reps.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using LM.Core.Data.Initializers;
using LM.Core.Infrastructure;

namespace LM.Data.Infrastructure
{
	 public partial class LMReadContextInit :DbContextInitializerBase<LMReadContext>, IStartupTask
    {
       
        public LMReadContextInit()
        {
            CreateDatabaseInitializer = new CreateDatabaseIfNotExistsWithSeedLMReadContext();
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
           