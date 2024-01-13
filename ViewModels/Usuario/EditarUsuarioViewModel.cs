using System.ComponentModel.DataAnnotations;

namespace TreinandoApi.ViewModels.Usuario
{
    public class EditarUsuarioViewModel
    {
        public int Id { get; set; }
        
        [Length(3, 30, ErrorMessage = "Tamanho minimo 3 e maximo 30 do nome")]
        [Required(ErrorMessage = "Nome do usuário é obrigatório")]
        public string nome { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string email { get; set; }

    }
}
