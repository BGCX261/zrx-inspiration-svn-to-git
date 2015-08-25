using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Inspiration.Core.Data;

namespace Inspiration.Model
{
    public class Group : BaseEntity
    {
        [Display(Name = "角色名")]
        [Required(ErrorMessage = "角色名称为必填项")]
        public string GroupName { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
    public class GroupMap : EntityTypeConfiguration<Group>
    {
        public GroupMap()
        {
            this.ToTable("Group");
            this.HasKey(item => item.ID);
            //this.Property(bp => bp.Title).IsRequired().IsMaxLength();
            //this.Property(bp => bp.Body).IsRequired().IsMaxLength();
            //this.Property(bp => bp.Tags).IsMaxLength();

            //this.HasRequired(bp => bp.Language)
            //    .WithMany()
            //    .HasForeignKey(bp => bp.LanguageId).WillCascadeOnDelete(true);
        }
    }
}
