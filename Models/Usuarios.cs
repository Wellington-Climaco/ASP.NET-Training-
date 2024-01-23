using System.Text.Json.Serialization;

namespace TreinandoApi.Models
{
    public class Usuarios
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string email { get; set; }

        [JsonIgnore]
        public string password { get; set; }

        public IList<Tarefa> ListaTarefas { get; set; }
    }
}
