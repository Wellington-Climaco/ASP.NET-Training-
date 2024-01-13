namespace TreinandoApi.ViewModels.Tarefa
{
    public class ListaTarefasViewModel
    {
        public int Id { get; set; }

        public string? NomeTarefa { get; set; }

        public string? Descricao { get; set; }

        public DateTime DataCriacao { get; set; }

        public string? Usuario { get; set; }

    }
}
