using System.Text.Json.Serialization;
using TreinandoApi.Models;
using TreinandoApi.ViewModels.Tarefa;



namespace TreinandoApi.ViewModels.Usuario
{
    public class ListaPostsUsuario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string email { get; set; }
        [JsonIgnore]
        public string password { get; set; }

        public IEnumerable<Tarefa1> ListaTarefas { get; set; }

    }
}
