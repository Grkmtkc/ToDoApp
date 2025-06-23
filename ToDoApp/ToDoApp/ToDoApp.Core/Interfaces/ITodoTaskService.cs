using ToDoApp.Core.Entities;

public interface ITodoTaskService
{
    Task<IEnumerable<TodoTask>> GetAllAsync();
    Task<TodoTask> GetByIdAsync(int id);
    Task<TodoTask> AddAsync(TodoTask task);
    Task UpdateAsync(TodoTask task);
    Task RemoveAsync(int id);
}
