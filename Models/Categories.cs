using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public ICollection<TaskItem> TaskItems { get; set; }

    }
}
