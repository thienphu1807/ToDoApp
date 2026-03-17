using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        = DateTime.Now;

        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int CategoriesId { get; set; }
        public Categories Categories { get; set; }
    }
}
