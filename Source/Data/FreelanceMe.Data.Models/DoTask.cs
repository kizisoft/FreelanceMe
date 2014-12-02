namespace FreelanceMe.Data.Models
{
    public class DoTask : Work
    {
        public DoTask()
            : base()
        {
        }

        public virtual Project Project { get; set; }
    }
}