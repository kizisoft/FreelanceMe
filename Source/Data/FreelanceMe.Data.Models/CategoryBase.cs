namespace FreelanceMe.Data.Models
{
    using FreelanceMe.Data.Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class CategoryBase : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}