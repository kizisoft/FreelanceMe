namespace FreelanceMe.Data.Models
{
    using System;

    public abstract class Work
    {
        public Work()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}