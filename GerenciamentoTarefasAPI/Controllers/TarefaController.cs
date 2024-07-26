using GerenciamentoTarefasAPI.Models;
using GerenciamentoTarefasAPI.Services.TarefaService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoTarefasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpPost("new")]
        public async Task<ActionResult<List<Tarefa>>> AddTask(Tarefa task)
        {
            var result = await _tarefaService.AddTask(task);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<List<Tarefa>>?> DeleteTask(int id)
        {
            var result = await _tarefaService.DeleteTask(id);

            if (result is null)
            {
                return NotFound("Tarefa não encontrada!");
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Tarefa>>> GetAllTasks()
        {
            var result = await _tarefaService.GetAllTasks();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa?>> GetTask(int id)
        {
            var result = await _tarefaService.GetTask(id);
            if (result is null)
            {
                return NotFound("Tarefa não encontrada!");
            }

            return Ok(result);
        }

        [HttpPatch("complete/{id}")]
        public async Task<ActionResult<List<Tarefa>?>> MarkTaskAsCompleted(int id)
        {
            var result = await _tarefaService.MarkTaskAsCompleted(id);

            if (result is null)
            {
                return NotFound("Tarefa não encontrada!");
            }

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult<List<Tarefa>?>> UpdateTask(Tarefa request)
        {
            var result = await _tarefaService.UpdateTask(request);
            if (result is null)
            {
                return NotFound("Tarefa não encontrada!");
            }

            return Ok(result);
        }
    }
}