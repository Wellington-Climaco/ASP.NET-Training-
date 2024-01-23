using Microsoft.EntityFrameworkCore;
using TreinandoApi.Data;
using TreinandoApi.Models;
using TreinandoApi.Repository.Interface;
using TreinandoApi.ViewModels.Usuario;

namespace TreinandoApi.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbContexto _contexto;
        public UsuarioRepository(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Usuarios> Atualizar(EditarUsuarioViewModel editarUsuario)
        {
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(x=> x.Id == editarUsuario.Id);
            usuario.Nome = editarUsuario.nome;
            usuario.email = editarUsuario.email;

            _contexto.Update(usuario);
            await _contexto.SaveChangesAsync();
            return usuario;

        }

        public async Task<Usuarios> BuscarPorId(int Id)
        {
            var usuario = await _contexto.Usuarios.Include(x=>x.ListaTarefas).FirstOrDefaultAsync(x=>x.Id == Id);
            return usuario;
        }

        public async Task<Usuarios> Criar(CriarUsuarioViewModel criarUsuario)
        {
            var usuario = new Usuarios { Nome = criarUsuario.Nome,email = criarUsuario.Email,password = criarUsuario.Password};
            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();
            return usuario;
        }

        public async Task<IEnumerable<Usuarios>> ListarTudo()
        {
            return await _contexto.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task<string> Remover(int Id)
        {
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(x=> x.Id == Id);
            _contexto.Remove(usuario);
            await _contexto.SaveChangesAsync();
            return ("Usuario removido com sucesso!!");

        }
    }
}
