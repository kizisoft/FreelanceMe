using System;
namespace FreelanceMe.Data.Models
{
    public abstract class Work
    {
        public Work()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}