using GerenciamentoTarefasAPI.Models;

namespace GerenciamentoTarefasAPI.Services.TarefaService
{
    public interface ITarefaService
    {
        Task<List<Tarefa>> GetAllTasks();

        Task<Tarefa?> GetTask(int id);

        Task<List<Tarefa>> AddTask(Tarefa task);

        Task<List<Tarefa>?> UpdateTask(int id, Tarefa request);

        Task<List<Tarefa>?> DeleteTask(int id);

        Task<Tarefa?> MarkTaskAsCompleted(int id);
    }
}
