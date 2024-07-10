using System.ComponentModel.DataAnnotations;

namespace GerenciamentoTarefasAPI.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set;} = string.Empty;

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime DueDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; } = false;
    }
}
