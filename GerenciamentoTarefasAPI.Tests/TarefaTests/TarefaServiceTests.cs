using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using GerenciamentoTarefasAPI.Models;
using GerenciamentoTarefasAPI.Services.TarefaService;
using GerenciamentoTarefasAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoTarefasAPI.Tests
{
    public class TarefaServiceTests
    {
        private readonly DataContext _context;
        private readonly TarefaService _service;

        public TarefaServiceTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TarefasTestDb")
                .Options;

            _context = new DataContext(options);
            _context.Database.EnsureDeleted();  // Garantir que o banco de dados em memória seja limpo
            _context.Database.EnsureCreated();

            _service = new TarefaService(_context);
        }

        [Fact]
        public async Task GetAllTasks_ShouldReturnAllTasks()
        {
            // Arrange
            _context.Tarefas.AddRange(
                new Tarefa { Title = "Tarefa 1", Description = "Descrição 1", CreationDate = DateTime.Now, DueDate = DateTime.Now.AddDays(1), IsCompleted = false },
                new Tarefa { Title = "Tarefa 2", Description = "Descrição 2", CreationDate = DateTime.Now, DueDate = DateTime.Now.AddDays(2), IsCompleted = false }
            );
            _context.SaveChanges();

            // Act
            var result = await _service.GetAllTasks();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AddTask_ShouldAddTask()
        {
            // Arrange
            var tarefa = new Tarefa { Title = "Nova Tarefa", Description = "Nova Descrição", CreationDate = DateTime.Now, DueDate = DateTime.Now.AddDays(1), IsCompleted = false };

            // Act
            var result = await _service.AddTask(tarefa);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, t => t.Title == "Nova Tarefa");
        }

        [Fact]
        public async Task AddTask_WithFutureCreationDate_ShouldSetCurrentDate()
        {
            // Arrange
            var futureDate = DateTime.Now.AddDays(1);
            var tarefa = new Tarefa { Title = "Data Futura", Description = "Descrição", CreationDate = futureDate, DueDate = futureDate, IsCompleted = false };

            // Act
            var result = await _service.AddTask(tarefa);

            // Assert
            Assert.NotNull(result);
            var addedTask = result.FirstOrDefault(t => t.Title == "Data Futura");
            Assert.NotNull(addedTask);
            Assert.True(addedTask.CreationDate <= DateTime.Now);
        }

        [Fact]
        public async Task GetTask_ShouldReturnTask()
        {
            // Arrange
            _context.Tarefas.Add(new Tarefa { Title = "Tarefa 1", Description = "Descrição 1", CreationDate = DateTime.Now, DueDate = DateTime.Now.AddDays(1), IsCompleted = false });
            _context.SaveChanges();

            // Act
            var result = await _service.GetTask(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tarefa 1", result.Title);
        }

        [Fact]
        public async Task GetTask_NonExistent_ShouldReturnNull()
        {
            // Act
            var result = await _service.GetTask(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateTask_ShouldUpdateTask()
        {
            // Arrange
            _context.Tarefas.Add(new Tarefa { Title = "Tarefa 1", Description = "Descrição 1", CreationDate = DateTime.Now, DueDate = DateTime.Now.AddDays(1), IsCompleted = false });
            _context.SaveChanges();

            var updatedTarefa = new Tarefa { Title = "Tarefa Atualizada", Description = "Descrição Atualizada", CreationDate = DateTime.Now, DueDate = DateTime.Now.AddDays(1), IsCompleted = false };

            // Act
            var result = await _service.UpdateTask(1, updatedTarefa);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, t => t.Title == "Tarefa Atualizada");
        }

        [Fact]
        public async Task UpdateTask_NonExistent_ShouldReturnNull()
        {
            // Arrange
            var updatedTarefa = new Tarefa { Title = "Tarefa Atualizada", Description = "Descrição Atualizada", CreationDate = DateTime.Now, DueDate = DateTime.Now.AddDays(1), IsCompleted = false };

            // Act
            var result = await _service.UpdateTask(1, updatedTarefa);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteTask_ShouldDeleteTask()
        {
            // Arrange
            _context.Tarefas.Add(new Tarefa { Title = "Tarefa 1", Description = "Descrição 1", CreationDate = DateTime.Now, DueDate = DateTime.Now.AddDays(1), IsCompleted = false });
            _context.SaveChanges();

            // Act
            var result = await _service.DeleteTask(1);

            // Assert
            Assert.NotNull(result);
            Assert.DoesNotContain(result, t => t.Id == 1);
        }

        [Fact]
        public async Task DeleteTask_NonExistent_ShouldReturnNull()
        {
            // Act
            var result = await _service.DeleteTask(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task MarkTaskAsCompleted_ShouldMarkTaskAsCompleted()
        {
            // Arrange
            _context.Tarefas.Add(new Tarefa { Title = "Tarefa 1", Description = "Descrição 1", CreationDate = DateTime.Now, DueDate = DateTime.Now.AddDays(1), IsCompleted = false });
            _context.SaveChanges();

            // Act
            var result = await _service.MarkTaskAsCompleted(1);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsCompleted);
        }

        [Fact]
        public async Task MarkTaskAsCompleted_NonExistent_ShouldReturnNull()
        {
            // Act
            var result = await _service.MarkTaskAsCompleted(1);

            // Assert
            Assert.Null(result);
        }
    }
}
