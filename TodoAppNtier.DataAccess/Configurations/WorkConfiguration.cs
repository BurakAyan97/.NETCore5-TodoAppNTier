using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNTier.Entities.Domains;

namespace TodoAppNtier.DataAccess.Configurations
{
    public class WorkConfiguration : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(x=>x.Definition)
                   .HasMaxLength(300)
                   .IsRequired();

            builder.Property(x=>x.IsCompleted)
                   .IsRequired();
            
        }
    }
}
