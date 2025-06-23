
namespace ToDoApp.Core.Entities
{
    public class TodoTask
    {
        public int Id { get; set; }                     // Primary Key
        public string Title { get; set; }               
        public string Description { get; set; }        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  
        public bool IsCompleted { get; set; } = false;  
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
