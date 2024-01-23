using System.ComponentModel.DataAnnotations;

namespace TreinandoApi.ViewModels.Tarefa2
{
    public class CriarTarefasViewModel
    {
        [Required(ErrorMessage = "Nome da tarefa é obrigatório")]
        [Length(3, 30, ErrorMessage = "Tamanho minimo 3 e maximo 30")]
        public string? NomeTarefa { get; set; }

        [Required(ErrorMessage = "Nome da descricao é obrigatório")]
        [Length(3, 30, ErrorMessage = "Tamanho minimo 3 e maximo 30")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Id usuario é obrigatório")]
        public int? Id_Usuario { get; set; }
    }
}
