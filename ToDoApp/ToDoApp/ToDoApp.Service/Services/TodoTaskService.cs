using ToDoApp.Core.Entities;
using ToDoApp.Core.Interfaces;
using ToDoApp.Core.UnitOfWorks;

namespace ToDoApp.Service.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly IGenericRepository<TodoTask> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TodoTaskService(IGenericRepository<TodoTask> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TodoTask>> GetAllAsync()
        {
            return await _repository.GetAllAsync(); 
        }

        public async Task<TodoTask> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TodoTask> AddAsync(TodoTask task)
        {
            await _repository.AddAsync(task);
            await _unitOfWork.CommitAsync();
            return task;
        }

        public async Task UpdateAsync(TodoTask task)
        {
            _repository.Update(task);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task != null)
            {
                _repository.Remove(task);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
