using TreinandoApi.Models;
using TreinandoApi.ViewModels.Usuario;

namespace TreinandoApi.Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuarios>> ListarTudo();

        Task<Usuarios> BuscarPorId(int Id);

        Task<Usuarios> Criar(CriarUsuarioViewModel criarUsuario);

        Task<Usuarios> Atualizar(EditarUsuarioViewModel editarUsuario);

        Task<string> Remover(int Id);
    }
}
