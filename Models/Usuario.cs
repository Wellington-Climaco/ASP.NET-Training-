namespace TreinandoApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public IList<Tarefa> ListaTarefas { get; set; }
    }
}
