using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class TaskItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public int CategoriesId { get; set; }
    }
}
