namespace FreelanceMe.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Project : Work
    {
        public Project()
            : base()
        {
        }

        [Required]
        public Category Category { get; set; }

        [Required]
        public SubCategory SubCategory { get; set; }

        public virtual ICollection<DoTask> DoTasks { get; set; }
    }
}