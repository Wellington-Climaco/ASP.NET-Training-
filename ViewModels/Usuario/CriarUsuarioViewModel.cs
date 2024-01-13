using System.ComponentModel.DataAnnotations;

namespace TreinandoApi.ViewModels.Usuario
{
    public class CriarUsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do usuário é obrigatório")]
        [Length(3, 30, ErrorMessage = "Tamanho minimo 3 e maximo 30 do nome")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string? Email { get; set; }

        [Length(6, 20, ErrorMessage = "Tamanho minimo 6 e maximo 20 da senha")]
        [Required(ErrorMessage = "a senha é obrigatório")]
        public string? Password { get; set; }

    }
}
