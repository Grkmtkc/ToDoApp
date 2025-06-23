namespace ToDoApp.API.DTOs
{
    public class ToDoTaskDto
    {
        public int Id { get; set; }                     
        public string Title { get; set; }               
        public string? Description { get; set; }       
        public DateTime? DueDate { get; set; }          
        public bool IsCompleted { get; set; }           
        public DateTime CreatedDate { get; set; }       
    }
}
