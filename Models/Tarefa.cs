namespace TreinandoApi.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        public string? NomeTarefa { get; set; }

        public string? Descricao { get; set; }

        public DateTime DataCriacao { get; set; }

        public Usuarios Usuario { get; set; }
    }
}
