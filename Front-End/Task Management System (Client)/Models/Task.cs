using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Task_Management_System__Client_.Models
{
    public enum TaskStatuses
    {
        [Display(Name = "To Do")]
        ToDo,

        [Display(Name = "In Progress")]
        InProgress,

        [Display(Name = "Completed")]
        Completed
    }

    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; } = "";

        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = "";

        // Enum property for internal use
        [Column(TypeName = "nvarchar(50)")]
        public TaskStatuses StatusEnum { get; set; }

        // String property for API communication
        [NotMapped]
        public string Status
        {
            get => StatusEnum.ToString();
            set => StatusEnum = Enum.Parse<TaskStatuses>(value);
        }
    }
}
