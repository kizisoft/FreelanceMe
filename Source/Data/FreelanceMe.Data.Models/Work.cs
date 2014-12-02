namespace FreelanceMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FreelanceMe.Data.Common.Models;

    public abstract class Work : AuditInfo, IDeletableEntity
    {
        public Work()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(5000)]
        public string Description { get; set; }

        public DateTime? StartedOn { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int? Duration { get; set; }

        public int? Progress { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}