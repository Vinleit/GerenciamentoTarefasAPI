using GerenciamentoTarefasAPI.Data;
using GerenciamentoTarefasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoTarefasAPI.Services.TarefaService
{
    public class TarefaService : ITarefaService
    {
        private readonly DataContext _context;

        public TarefaService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Tarefa>> AddTask(Tarefa task)
        {
            task.CreationDate = DateTime.Now;

            _context.Tarefas.Add(task);
            await _context.SaveChangesAsync();
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<List<Tarefa>?> DeleteTask(int id)
        {
            var task = await _context.Tarefas.FindAsync(id);
            if (task is null)
            {
                return null;
            }

            _context.Tarefas.Remove(task);
            await _context.SaveChangesAsync();

            return await _context.Tarefas.ToListAsync();
        }

        public async Task<List<Tarefa>> GetAllTasks()
        {
            var tasks = await _context.Tarefas.ToListAsync();
            return tasks;
        }

        public async Task<Tarefa?> GetTask(int id)
        {
            var task = await _context.Tarefas.FindAsync(id);
            if (task is null)
            {
                return null;
            }

            return task;
        }

        public async Task<List<Tarefa>?> MarkTaskAsCompleted(int id)
        {
            var task = await _context.Tarefas.FindAsync(id);

            if (task is null)
            {
                return null;
            }

            task.IsCompleted = true;

            await _context.SaveChangesAsync();
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<List<Tarefa>?> UpdateTask(Tarefa request)
        {
            var task = await _context.Tarefas.FindAsync(request.Id);

            if (task is null)
            {
                return null;
            }

            task.Title = request.Title;
            task.Description = request.Description;
            task.DueDate = request.DueDate;

            await _context.SaveChangesAsync();
            return await _context.Tarefas.ToListAsync();
        }
    }
}
