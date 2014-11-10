namespace FreelanceMe.Data.Models
{
    using System;

    using FreelanceMe.Data.Common.Models;

    public class Job : AuditInfo, IDeletableEntity
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}