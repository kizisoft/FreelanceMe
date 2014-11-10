namespace FreelanceMe.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using FreelanceMe.Data.Common.Models;

    public class Profile : AuditInfo, IDeletableEntity
    {
        public Profile()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public ApplicationUser User { get; set; }

        public UserType Type { get; set; }

        public int Rating { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}