namespace FreelanceMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FreelanceMe.Data.Common.Models;

    public class UserProfile : AuditInfo, IDeletableEntity
    {
        public UserProfile()
        {
            this.PostedProjects = new HashSet<Project>();
            this.WorkOnProjects = new HashSet<Project>();
        }

        [Key, ForeignKey("User")]
        public string Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(30)]
        public string City { get; set; }

        public string Avatar { get; set; }

        public virtual ICollection<Project> PostedProjects { get; set; }

        public virtual ICollection<Project> WorkOnProjects { get; set; }

        public int Rating { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}