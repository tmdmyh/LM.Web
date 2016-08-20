using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace LM.Data.Models.Mapping
{
    public class ApprovalTypeMap : LMEntityTypeConfiguration<ApprovalType>
    {
        public ApprovalTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("ApprovalType");
          
        }
    }
}
