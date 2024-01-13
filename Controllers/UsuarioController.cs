using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreinandoApi.Data;
using TreinandoApi.Models;
using TreinandoApi.ViewModels.Usuario;

namespace TreinandoApi.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly DbContexto _contexto;
        public UsuarioController(DbContexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet("v1/user")]
        public async Task<IActionResult> ListarTodosUsuarios()
        {
            var usuarios = _contexto.Usuarios.AsNoTracking().ToList();

            return Ok(usuarios);
        }

        [HttpGet("v1/user/{Id:int}")]
        public async Task<IActionResult> ListarUmUsuario([FromRoute] int Id)
        {
            var usuario = _contexto.Usuarios.AsNoTracking().FirstOrDefault(x => x.Id == Id);
            if (usuario == null) return NotFound("Usuario nao encontrado!!");

            return Ok(usuario);
        }

        [HttpPost("v1/user/create")]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioViewModel usuario)
        {
            try
            {
                var NovoUser = new Usuario { Nome = usuario.Nome, email = usuario.Email, password = usuario.Password };

                await _contexto.Usuarios.AddAsync(NovoUser);
                await _contexto.SaveChangesAsync();

                return Ok($"Conta criada com sucesso - {NovoUser.Nome}");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possivel criar usuario");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno");
            }
        }

        [HttpPut("v1/user/update")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] EditarUsuarioViewModel usuario)
        {
            var UpdateUser = _contexto.Usuarios.FirstOrDefault(x => x.Id == usuario.Id);
            if (UpdateUser == null) return NotFound("Usuario nao encontrado");
            try
            {
                UpdateUser.Nome = usuario.nome;
                UpdateUser.email = usuario.email;

                _contexto.Usuarios.Update(UpdateUser);
                await _contexto.SaveChangesAsync();

                return Ok($"Atualizado com sucesso!");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possivel atualizar usuario");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno");
            }

        }

        [HttpDelete("v1/user/delete")]
        public async Task<IActionResult> RemoverUsuario([FromRoute] int Id)
        {
            var usuario = _contexto.Usuarios.FirstOrDefault(x => x.Id == Id);
            if (usuario == null) return NotFound("Usuario nao encontrado");

            try
            {
                _contexto.Usuarios.Remove(usuario);
                await _contexto.SaveChangesAsync();
                return Ok("Usuario removido!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno");
            }
        }



    }
}
