using LM.Core.Data;
using System;
using System.Collections.Generic;

namespace LM.Data.Models
{
    public partial class ApprovalType: BaseEntity
    {
        public ApprovalType()
        {
           
        }

        public int ID { get; set; }
        public string Name { get; set; }
     

    }
}
