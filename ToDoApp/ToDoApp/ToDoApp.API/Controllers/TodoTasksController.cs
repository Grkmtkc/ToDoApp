using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.API.DTOs;
using ToDoApp.Core.Entities;


namespace ToDoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTaskController : ControllerBase
    {
        private readonly ITodoTaskService _todoTaskService;
        private readonly IMapper _mapper;

        public TodoTaskController(ITodoTaskService todoTaskService, IMapper mapper)
        {
            _todoTaskService = todoTaskService;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoTaskDto>>> GetAll()
        {
            var tasks = await _todoTaskService.GetAllAsync();
            var tasksDto = _mapper.Map<IEnumerable<ToDoTaskDto>>(tasks);
            return Ok(tasksDto);
        }

        // GET: id
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoTaskDto>> GetById(int id)
        {
            var task = await _todoTaskService.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(_mapper.Map<ToDoTaskDto>(task));
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<ToDoTaskDto>> Create(ToDoTaskCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = _mapper.Map<TodoTask>(createDto);
            var newTask = await _todoTaskService.AddAsync(entity);
            var newDto = _mapper.Map<ToDoTaskDto>(newTask);

            return CreatedAtAction(nameof(GetById), new { id = newTask.Id }, newDto);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ToDoTaskUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _todoTaskService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(updateDto, existing); // DTO'daki değeri entity aktar
            await _todoTaskService.UpdateAsync(existing);

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _todoTaskService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _todoTaskService.RemoveAsync(id);
            return NoContent();
        }
    }
}
