using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics;
using TreinandoApi.Data;
using TreinandoApi.Models;
using TreinandoApi.Repository.Interface;
using TreinandoApi.ViewModels.Usuario;

namespace TreinandoApi.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        public readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet("v1/usuario/Listar")]
        public async Task<IActionResult> ListarTodos()
        {
            var usuario = await _usuarioRepository.ListarTudo();
            return Ok(usuario);
        }

        [HttpGet("v1/usuario/Listar/{Id:int}")]
        public async Task<IActionResult> ListarPorId([FromRoute] int Id)
        {
            var usuario = await _usuarioRepository.BuscarPorId(Id);
            if (usuario == null) return NotFound("Usuário nao encontrado!!");
            return Ok(usuario);
        }

        [HttpPost("v1/usuario/criar")]
        public async Task<IActionResult> Criar([FromBody] CriarUsuarioViewModel usuarioViewModel)
        {
            if (usuarioViewModel == null) return StatusCode(500, "Algum campo não foi preenchido!!");
            var usuario = await _usuarioRepository.Criar(usuarioViewModel);
            return Ok(usuario);            
        }

        [HttpPut("v1/usuario/atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] EditarUsuarioViewModel usuarioViewModel)
        {
            await _usuarioRepository.BuscarPorId(usuarioViewModel.Id);

            var usuario = await _usuarioRepository.Atualizar(usuarioViewModel);
            return Ok(usuario);           
        }

        [HttpDelete("v1/usuario/remover/{id:int}")]
        public async Task<IActionResult> Remover([FromRoute] int id)
        {
            if (await _usuarioRepository.BuscarPorId(id) == null) return NotFound("Usuario não encontrado!!");
            return Ok(await _usuarioRepository.Remover(id));           
        }

    }
}
